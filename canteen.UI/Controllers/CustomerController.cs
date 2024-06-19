using canteen.Data.Models.Domain;
using canteen.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace canteen.UI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IcustomerRepository _customerRepo;

        public CustomerController(IcustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(customer);
                bool addCustomerResult = await _customerRepo.AddAsync(customer);
                if (addCustomerResult)
                    TempData["msg"] = "Successfully added";
                else
                    TempData["msg"] = "Could not be added";
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not added";
            }
            return RedirectToAction(nameof(Add));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerRepo.GetByIdAsync(id);
            //if (person == null)
            //  return NotFound();
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(customer);
                var updateResult = await _customerRepo.UpdateAsync(customer);
                if (updateResult)
                    TempData["msg"] = "Updated succesfully";
                else
                    TempData["msg"] = "Could not be updated";

            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could not be updated";
            }
            return View(customer);
        }





        public async Task<IActionResult> DisplayAll()
        {
            var cust = await _customerRepo.GetAllAsync();
            return View(cust);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _customerRepo.DeleteAsync(id);
            return RedirectToAction(nameof(DisplayAll));
        }


    }
}