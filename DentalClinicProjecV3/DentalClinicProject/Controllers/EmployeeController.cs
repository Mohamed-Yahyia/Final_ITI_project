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
    public class EmployeeController : Controller
    {
        private readonly Context _Context;

        public EmployeeController(Context Context)
        {
            _Context = Context;
        }
        string Baseurl = "https://localhost:44308/";
        HttpClientHandler _clienthandler = new HttpClientHandler();
        EmployeeVM emp = new EmployeeVM();
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Employee/Getall");

            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                List<EmployeeVM>? emps = JsonConvert.DeserializeObject<List<EmployeeVM>>(Res);
                return View(emps);

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
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Employee/Getall");

            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                List<EmployeeVM>? emps = JsonConvert.DeserializeObject<List<EmployeeVM>>(Res);

                if (!String.IsNullOrEmpty(searchString))
                {
                    emps = emps.Where(a => a.Number.ToString().Contains(searchString)).ToList();
                    //appoints = appoints.Where(a => a.AppointDate.ToString().Contains(searchString)).ToList();

                }

                return View("Index", emps);

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
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Employee/GetEmployeebyId/" + Id);
            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                EmployeeVM emps = JsonConvert.DeserializeObject<EmployeeVM>(Res);
                return View(emps);
            }
            else
            {
                return View("Error");
            }

        }
        [HttpGet , ActionName("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmpAsync(int id)
        {
            EmployeeVM emp = new EmployeeVM();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Employee/GetEmployeebyId/" + id);
                //await client.GetAsync("api/Employee/GetEmployeebyId/" + id);
                if (res.IsSuccessStatusCode)
                {
                    var resulte = res.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<EmployeeVM>(resulte);
                }
            }
            return View(emp);

        }
        [HttpPost]
        public IActionResult DeleteEmployee(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.DeleteAsync($"{Baseurl}api/Employee/DeleteEmployee/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    TempData["DeleteEmployee"] = "تم حذف المحتوى بنجاح";
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ViewBag.msg = "some thing went wrong";
                }
                return View();
            }
        }




        public async Task<IActionResult> Createemployee()
        {
            return View();
        }       

        [HttpPost]

        public async Task<IActionResult> CreateEmployee(EmployeeVM Emp)
        {
            HttpClient Client = new HttpClient();
            HttpResponseMessage Response =
                await Client.PostAsJsonAsync($"{Baseurl}api/Employee/PostEmployee", Emp);
            if (Response.IsSuccessStatusCode)
            {
                TempData["CreateEmployee"] = "تم إضافة الموظف بنجاح";
                return RedirectToAction("Index", "Employee");
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
            HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Employee/GetEmployeebyId/" + id);
            if (response.IsSuccessStatusCode)
            {
                string Res = await response.Content.ReadAsStringAsync();
                EmployeeVM? emp = JsonConvert.DeserializeObject<EmployeeVM>(Res);
                return View(emp);
            }
            else
            {
                return View("Error");
            }
        }
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(EmployeeVM employee)

        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = client.PutAsJsonAsync("https://localhost:44308/api/Employee/PutEmployee", employee).Result;
                if (res.IsSuccessStatusCode)
                {
                    TempData["UpdateEmployee"] = "تم تعديل المحتوى بنجاح";
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    return View();
                }
            }
        }

        #region MyRegion
        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{

        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync($"{Baseurl}api/Contents/ContentByIDWithAllDetails/" + id);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string Res = await response.Content.ReadAsStringAsync();
        //        ContentWithDetails? contents = JsonConvert.DeserializeObject<ContentWithDetails>(Res);
        //        return View(contents);
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(ContentWithDetails contentWithDetails)

        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(Baseurl);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage res = client.PutAsJsonAsync("http://localhost:5295/api/Contents/NewUpdateContent", contentWithDetails).Result;
        //        if (res.IsSuccessStatusCode)
        //        {
        //            TempData["UpdateContent"] = "تم تعديل المحتوى بنجاح";
        //            return RedirectToAction("Index", "First");
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //}
        #endregion
    }
}
