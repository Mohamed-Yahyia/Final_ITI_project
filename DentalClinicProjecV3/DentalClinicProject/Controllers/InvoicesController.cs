using DentalClinicProject.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DentalClinicProject.DataContext;
using System.Net.Http.Json;
using System.Linq;

namespace DentalClinicProject.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly Context _Context;

        public InvoicesController(Context Context)
        {
            _Context = Context;
        }
        string Baseurl = "https://localhost:44308/";
        HttpClientHandler _clienthandler = new HttpClientHandler();
        InvoicesVM invoice = new InvoicesVM();



        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Invoices/Getall");

            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                List<InvoicesVM>? invoices = JsonConvert.DeserializeObject<List<InvoicesVM>>(Res);
                return View(invoices);

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
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Invoices/Getall");

            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                List<InvoicesVM>? invoices = JsonConvert.DeserializeObject<List<InvoicesVM>>(Res);

                if (!String.IsNullOrEmpty(searchString))
                {
                    //invoices = invoices.Where(a => a.Number.ToString().Contains(searchString)).ToList();
                    invoices = invoices.Where(a => a.inviocDate.ToString().Contains(searchString)).ToList();

                }

                return View("Index", invoices);

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
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Invoices/GetInvoicesId/" + Id);
            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                InvoicesVM invoices = JsonConvert.DeserializeObject<InvoicesVM>(Res);
                return View(invoices);
            }
            else
            {
                return View("Error");
            }

        }
        [HttpGet, ActionName("DeleteInvoices")]
        public async Task<IActionResult> DeleteinvoiceAsync(int id)
        {
            InvoicesVM invoice = new InvoicesVM();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Invoices/GetInvoicesId/" + id);

                if (res.IsSuccessStatusCode)
                {
                    var resulte = res.Content.ReadAsStringAsync().Result;
                    invoice = JsonConvert.DeserializeObject<InvoicesVM>(resulte);
                }
            }
            return View(invoice);

        }
        [HttpPost]
        public IActionResult DeleteInvoices(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.DeleteAsync($"{Baseurl}api/Invoices/DeleteInvoices/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    TempData["DeleteInvoices"] = "تم حذف المحتوى بنجاح";
                    return RedirectToAction("Index", "Invoices");
                }
                else
                {
                    ViewBag.msg = "some thing went wrong";
                }
                return View();
            }
        }




        public async Task<IActionResult> Createinvoice()
        {
            return View();
        }

        [HttpPost, ActionName("Createinvoice")]

        public async Task<IActionResult> CreateInvoices(InvoicesVM invoice)
        {
            HttpClient Client = new HttpClient();
            HttpResponseMessage Response =
                await Client.PostAsJsonAsync($"{Baseurl}api/Invoices/PostInvoices", invoice);
            if (Response.IsSuccessStatusCode)
            {
                TempData["CreateInvoices"] = "تم إضافة الموظف بنجاح";
                return RedirectToAction("Index", "Invoices");
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
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Invoices/GetInvoicesId/" + id);
            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                InvoicesVM? invoice = JsonConvert.DeserializeObject<InvoicesVM>(Res);
                return View(invoice);
            }
            else
            {
                return View("Error");
            }
        }
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(InvoicesVM invo)

        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.PutAsJsonAsync("https://localhost:44308/api/Invoices/PutInvoices", invo).Result;
                if (res.IsSuccessStatusCode)
                {
                    TempData["UpdateInvoices"] = "تم تعديل المحتوى بنجاح";
                    return RedirectToAction("Index", "Invoices");
                }
                else
                {
                    return View();
                }


            }
        }
    }
}
        
    

 
