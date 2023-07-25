using DentalClinicProject.DataContext;
using DentalClinicProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DentalClinicProject.Controllers
{
    public class PatientsController : Controller
    {
        private readonly Context _Context;

        public PatientsController(Context Context)
        {
            _Context = Context;
        }

        string Baseurl = "https://localhost:44308/";
        HttpClientHandler _clienthandler = new HttpClientHandler();
        PatientsVM pat = new PatientsVM();

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Patients/Getall");

            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                List<PatientsVM>? pats = JsonConvert.DeserializeObject<List<PatientsVM>>(Res);
                return View(pats);

            }
            else
            {
                ViewBag.response = "Error";
            }
            return View();
        }

        public async Task<IActionResult> Print(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Patients/GetrecordsPatient/" + id);

            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                List<MedicalRecordsVMcs>? reps = JsonConvert.DeserializeObject<List<MedicalRecordsVMcs>>(Res);
                return View(reps);


            }
            else
            {
                ViewBag.response = "Error";
            }
            return View();
        }
        public async Task<IActionResult> Search(string searchString)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Patients/Getall");

            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                List<PatientsVM>? pats = JsonConvert.DeserializeObject<List<PatientsVM>>(Res);

                if (!String.IsNullOrEmpty(searchString))
                {
                    pats = pats.Where(a => a.Number.ToString().Contains(searchString)).ToList();
                    //appoints = appoints.Where(a => a.AppointDate.ToString().Contains(searchString)).ToList();

                }

                return View("Index", pats);

            }
            else
            {
                ViewBag.response = "Error";
                return View("Index");
            }
        }
        public async Task<IActionResult> Details(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Patients/GetPatientsbyId/" + Id);
            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                PatientsVM pats = JsonConvert.DeserializeObject<PatientsVM>(Res);
                return View(pats);
            }
            else
            {
                return View("Error");
            }

        }

        [HttpGet, ActionName("DeletePatient")]
        public async Task<IActionResult> DeleteEmpAsync(int id)
        {
            PatientsVM pat = new PatientsVM();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Patients/GetPatientsbyId/" + id);
                //await client.GetAsync("api/Employee/GetEmployeebyId/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var resulte = res.Content.ReadAsStringAsync().Result;
                    pat = JsonConvert.DeserializeObject<PatientsVM>(resulte);
                }
            }
            return View(pat);

        }


        [HttpPost]
        public IActionResult DeletePatient(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.DeleteAsync($"{Baseurl}api/Patients/DeletePatients/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    TempData["DeletePatient"] = "تم حذف المحتوى بنجاح";
                    return RedirectToAction("Index", "Patients");
                }
                else
                {
                    ViewBag.msg = "some thing went wrong";
                }
                return View();
            }
        }

        public async Task<IActionResult> CreatePatient()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreatePatient(PatientsVM pat)
        {
            if (ModelState.IsValid)
            {
                HttpClient Client = new HttpClient();
                HttpResponseMessage Response =
                    await Client.PostAsJsonAsync($"{Baseurl}api/Patients/PostPatients", pat);

                if (Response.IsSuccessStatusCode)
                
                    TempData["CreatePatient"] = "تم إضافة المريض بنجاح";
                    return RedirectToAction("Index", "Patients");
                
                




            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Patients/GetPatientsbyId/" + id);
            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                PatientsVM? pat = JsonConvert.DeserializeObject<PatientsVM>(Res);
                return View(pat);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(PatientsVM patient)

        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.PutAsJsonAsync("https://localhost:44308/api/Patients/PutPatients", patient).Result;
                if (res.IsSuccessStatusCode)
                {
                    TempData["UpdatePatient"] = "تم تعديل المحتوى بنجاح";
                    return RedirectToAction("Index", "Patients");
                }
                else
                {
                    return View();
                }
            }
        }

        public async Task<IActionResult> SearchByNumber()
        {
            return View();
        }

    }
}












