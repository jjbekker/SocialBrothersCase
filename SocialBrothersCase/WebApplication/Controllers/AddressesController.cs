using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API.Database;
using Domain.Model;
using WebApplication.Helpers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace WebApplication.Controllers
{
    public class AddressesController : Controller
    {
        private ApiHelper _api;
        public AddressesController()
        {
            _api = new ApiHelper();
        }

        // GET: Addresses
        public async Task<IActionResult> Index(string searchString, string sort)
        {
            List<Address> adresses = new List<Address>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("/api/Addresses");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                adresses = JsonConvert.DeserializeObject<List<Address>>(result);
            }
            if (!String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(sort))
            {
                adresses = adresses.Where(s => s.Street.Contains(searchString)
                || s.Country.Contains(searchString)
                || s.City.Contains(searchString)
                || s.PostalCode.Contains(searchString)
                || s.HouseNumber.ToString().Contains(searchString))
                    .ToList();
            }
            else if (!String.IsNullOrEmpty(sort))
            {
                adresses = adresses.OrderBy(s => s.Street).ToList();
            }
            return View(adresses);
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            Address addresses = new Address();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("/api/Addresses/" + id);

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                addresses = JsonConvert.DeserializeObject<Address>(result);
            }

            if (addresses == null)
            {
                return NotFound();
            }

            return View(addresses);
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Street,HouseNumber,PostalCode,City,Country")] Address address)
        {
            Address newAddress = new Address();
            using (var httpClient = _api.Initial())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(address), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("/api/Addresses/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    newAddress = JsonConvert.DeserializeObject<Address>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            Address address = new Address();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("/api/Addresses/" + id);

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                address = JsonConvert.DeserializeObject<Address>(result);
            }

            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Street,HouseNumber,PostalCode,City,Country")] Address address)
        {
            Address newAddress = new Address();
            using (var httpClient = _api.Initial())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(address.Id.ToString()), "Id");
                content.Add(new StringContent(address.Street), "Street");
                content.Add(new StringContent(address.HouseNumber.ToString()), "HouseNumber");
                content.Add(new StringContent(address.PostalCode), "PostalCode");
                content.Add(new StringContent(address.City), "City");
                content.Add(new StringContent(address.Country), "Country");
                var json = JsonConvert.SerializeObject(content);

                using (var response = await httpClient.PutAsync("/api/Addresses/" + address.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    address = JsonConvert.DeserializeObject<Address>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            Address address = new Address();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("/api/Addresses/" + id);

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                address = JsonConvert.DeserializeObject<Address>(result);
            }

            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using (var httpClient = _api.Initial())
            {
                using (var response = await httpClient.DeleteAsync("api/Addresses/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}