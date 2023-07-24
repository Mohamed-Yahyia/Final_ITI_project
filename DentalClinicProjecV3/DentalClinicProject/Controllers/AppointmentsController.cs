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
    public class AppointmentsController : Controller
    {
        private readonly Context _Context;

        public AppointmentsController(Context Context)
        {
            _Context = Context;
        }

        string Baseurl = "https://localhost:44308/";
        HttpClientHandler _clienthandler = new HttpClientHandler();
        AppointmentssVM appoint = new AppointmentssVM();

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Appointments/Getall");

            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                List<AppointmentssVM>? appoints = JsonConvert.DeserializeObject<List<AppointmentssVM>>(Res);
                return View(appoints);

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
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Appointments/Getall");

            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                List<AppointmentssVM>? appoints = JsonConvert.DeserializeObject<List<AppointmentssVM>>(Res);

                if (!String.IsNullOrEmpty(searchString))
                {
                    //appoints = appoints.Where(a => a.AppointDate.Contains(searchString)).ToList();
                    appoints = appoints.Where(a => a.AppointDate.ToString().Contains(searchString)).ToList();

                }

                return View("Index", appoints);

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
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Appointments/GetappointbyId/" + Id);
            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                AppointmentssVM appoints = JsonConvert.DeserializeObject<AppointmentssVM>(Res);
                return View(appoints);
            }
            else
            {
                return View("Error");
            }

        }

        [HttpGet, ActionName("Deleteappoint")]
        public async Task<IActionResult> DeleteEmpAsync(int id)
        {
            AppointmentssVM appoint = new AppointmentssVM();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Appointments/GetappointbyId/" + id);
                //await client.GetAsync("api/Employee/GetEmployeebyId/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var resulte = res.Content.ReadAsStringAsync().Result;
                    appoint = JsonConvert.DeserializeObject<AppointmentssVM>(resulte);
                }
            }
            return View(appoint);

        }

        [HttpPost]
        public IActionResult DeleteAppoint(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.DeleteAsync($"{Baseurl}api/Appointments/Deleteappoint/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    TempData["DeleteAppoint"] = "تم حذف الموعد بنجاح";
                    return RedirectToAction("Index", "Appointments");
                }
                else
                {
                    ViewBag.msg = "some thing went wrong";
                }
                return View();
            }
        }

        public async Task<IActionResult> CreateAppoint()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateAppoint(AppointmentssVM appoint)
        {
            HttpClient Client = new HttpClient();
            HttpResponseMessage Response =
                await Client.PostAsJsonAsync($"{Baseurl}api/Appointments/Postappoint", appoint);
            if (Response.IsSuccessStatusCode)
            {
                TempData["CreateAppointt"] = "تم إضافة الموعد بنجاح";
                return RedirectToAction("Index", "Appointments");
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
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Appointments/GetappointbyId/" + id);
            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                AppointmentssVM? appoint = JsonConvert.DeserializeObject<AppointmentssVM>(Res);
                return View(appoint);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(AppointmentssVM appoint)

        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.PutAsJsonAsync("https://localhost:44308/api/Appointments/Putappoint", appoint).Result;
                if (res.IsSuccessStatusCode)
                {
                    TempData["UpdateAppoint"] = "تم تعديل الموعد بنجاح";
                    return RedirectToAction("Index", "Appointments");
                }
                else
                {
                    return View();
                }
            }
        }
    }
}
