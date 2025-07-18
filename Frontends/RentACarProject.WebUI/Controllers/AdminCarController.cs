﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RentACarProject.DTO.BrandDTOs;
using RentACarProject.DTO.CarDTOs;

namespace RentACarProject.WebUI.Controllers
{
    public class AdminCarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7290/api/Cars/GetCarWithBrand");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarWithBrandDTO>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7290/api/Brands");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBrandDTO>>(jsonData);
            List<SelectListItem> brandValues = (from x in values
                                                select new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.BrandID.ToString()
                                                }).ToList();

            ViewBag.BrandValues = brandValues;  
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarDTO createCarDTO)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCarDTO);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7290/api/Cars/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> RemoveCar(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7290/api/Cars/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> UpdateCar(int id)
        {

            var client = _httpClientFactory.CreateClient();
            var responseMessage1 = await client.GetAsync("https://localhost:7290/api/Brands");
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            var values1 = JsonConvert.DeserializeObject<List<ResultBrandDTO>>(jsonData1);
            List<SelectListItem> brandValues = (from x in values1
                                                select new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.BrandID.ToString()
                                                }).ToList();

            ViewBag.BrandValues = brandValues;
            var responseMessage = await client.GetAsync($"https://localhost:7290/api/Cars/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCarDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(UpdateCarDTO updateCarDTO)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCarDTO);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7290/api/Cars/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
