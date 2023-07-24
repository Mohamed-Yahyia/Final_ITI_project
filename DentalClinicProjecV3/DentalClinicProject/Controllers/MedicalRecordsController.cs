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
    public class MedicalRecordsController : Controller
    {
        private readonly Context _Context;

        public MedicalRecordsController(Context Context)
        {
            _Context = Context;
        }

        string Baseurl = "https://localhost:44308/";
        HttpClientHandler _clienthandler = new HttpClientHandler();
        MedicalRecordsVMcs record = new MedicalRecordsVMcs();

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/MedicalRecords/Getall");

            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                List<MedicalRecordsVMcs>? records = JsonConvert.DeserializeObject<List<MedicalRecordsVMcs>>(Res);
                return View(records);

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
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/MedicalRecords/Getall");

            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                List<MedicalRecordsVMcs>? records = JsonConvert.DeserializeObject<List<MedicalRecordsVMcs>>(Res);

                if (!String.IsNullOrEmpty(searchString))
                {
                    //invoices = invoices.Where(a => a.Number.ToString().Contains(searchString)).ToList();
                    records = records.Where(a => a.VisitDate.ToString().Contains(searchString)).ToList();

                }

                return View("Index", records);

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
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/MedicalRecords/GetMedicalbyId/" + Id);
            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                MedicalRecordsVMcs records = JsonConvert.DeserializeObject<MedicalRecordsVMcs>(Res);
                return View(records);
            }
            else
            {
                return View("Error");
            }

        }


        [HttpGet, ActionName("DeleteMedical")]
        public async Task<IActionResult> DeleteEmpAsync(int id)
        {
            MedicalRecordsVMcs record = new MedicalRecordsVMcs();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/MedicalRecords/GetMedicalbyId/" + id);
                //await client.GetAsync("api/Employee/GetEmployeebyId/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var resulte = res.Content.ReadAsStringAsync().Result;
                    record = JsonConvert.DeserializeObject<MedicalRecordsVMcs>(resulte);
                }
            }
            return View(record);

        }


        [HttpPost]
        public IActionResult DeleteMedical(int id)
        { 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.DeleteAsync($"{Baseurl}api/MedicalRecords/DeleteMedical/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    TempData["DeleteMedical"] = "تم حذف  بنجاح";
                    return RedirectToAction("Index", "MedicalRecords");
                }
                else
                {
                    ViewBag.msg = "some thing went wrong";
                }
                return View();
            }
        }

        public async Task<IActionResult> CreateMedical()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateMedical(MedicalRecordsVMcs record)
        {
            HttpClient Client = new HttpClient();
            HttpResponseMessage Response =
                await Client.PostAsJsonAsync($"{Baseurl}api/MedicalRecords/PostMedical", record);
            if (Response.IsSuccessStatusCode)
            {
                TempData["CreateMedical"] = "تم إضافة  بنجاح";
                return RedirectToAction("Index", "MedicalRecords");
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
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/MedicalRecords/GetMedicalbyId/" + id);
            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                MedicalRecordsVMcs? record = JsonConvert.DeserializeObject<MedicalRecordsVMcs>(Res);
                return View(record);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(MedicalRecordsVMcs record)

        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.PutAsJsonAsync("https://localhost:44308/api/MedicalRecords/PutMedical", record).Result;
                if (res.IsSuccessStatusCode)
                {
                    TempData["UpdateMedical"] = "تم تعديل  بنجاح";
                    return RedirectToAction("Index", "MedicalRecords");
                }
                else
                {
                    return View();
                }
            }
        }


    }
}
