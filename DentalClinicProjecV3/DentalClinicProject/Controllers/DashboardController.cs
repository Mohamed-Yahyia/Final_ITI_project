using DentalClinicProject.DataContext;
using DentalClinicProject.Models;
using DentalClinicProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DentalClinicProject.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Context _Context;

        public DashboardController(Context Context)
        {
            _Context = Context;
        }

        string Baseurl = "https://localhost:44308/";
        HttpClientHandler _clienthandler = new HttpClientHandler();
        AppointmentssVM appoint = new AppointmentssVM();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetEvents()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Appointments/Getall");
            string Res = await response.Content.ReadAsStringAsync();
            List<AppointmentssVM>? events = JsonConvert.DeserializeObject<List<AppointmentssVM>>(Res);
            return new JsonResult(events);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEvent(AppointmentssVM e)
        {
            HttpClient Client = new HttpClient();
            HttpResponseMessage Response =
                await Client.PostAsJsonAsync($"{Baseurl}api/Appointments/Postappoint", e);
            return new JsonResult(e);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            var status = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.DeleteAsync($"{Baseurl}api/Appointments/Deleteappoint/" + eventId).Result;
                status = true;
            }
            return new JsonResult(status);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}