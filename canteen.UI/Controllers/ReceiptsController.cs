using canteen.Data.DataAccess;
using canteen.Data.Models.Domain;
using canteen.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace canteen.UI.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly IreceiptsRepository _receiptsRepo;
        private readonly IcustomerRepository _customerRepo;
        private readonly ISqlDataAccess _db;

        public ReceiptsController(IreceiptsRepository receiptsRepo, IcustomerRepository customerRepo, ISqlDataAccess db)
        {
            _receiptsRepo = receiptsRepo;
            _customerRepo = customerRepo;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var customerDescriptions = await _customerRepo.GetDescriptionsAsync();
                var descriptionOptions = customerDescriptions.Select(description => new SelectListItem
                {
                    Text = description,
                    Value = description
                }).ToList();

                ViewData["DescriptionOptions"] = descriptionOptions;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Could not retrieve description options.";
                return RedirectToAction("Add");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Receipts receipt)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var customerDescriptions = await _customerRepo.GetDescriptionsAsync();
                    var descriptionOptions = customerDescriptions.Select(description => new SelectListItem
                    {
                        Text = description,
                        Value = description
                    }).ToList();

                    ViewData["DescriptionOptions"] = descriptionOptions;

                    return View(receipt);
                }

                string description = receipt.description; // Assuming you have a property like SelectedDescription in the Receipts model.
                string itemNumber = await _customerRepo.GetItemNumberByDescriptionAsync(description);
                string unit = await _customerRepo.GetUnitByDescriptionAsync(description);

                receipt.item_number = itemNumber;
                receipt.unit = unit;

                bool addReceiptResult = await _receiptsRepo.AddAsync(receipt);

                if (addReceiptResult)
                {
                    TempData["SuccessMessage"] = "Successfully added";
                    return RedirectToAction("DisplayAll", new
                    {
                        item_number = receipt.item_number,
                        rect_date =receipt.rect_date,
                        rate = receipt.rate,
                        quantity = receipt.quantity,
                        unit = receipt.unit
                    });
                }
                else
                {
                    TempData["ErrorMessage"] = "Could not be added";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Could not be added";
            }
            return View(receipt);
        }

       
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                Receipts receipts = await _receiptsRepo.GetByIdAsync(id);
                var customerDescriptions = await _customerRepo.GetDescriptionsAsync();
                var descriptionOptions = customerDescriptions.Select(description => new SelectListItem
                {
                    Text = description,
                    Value = description
                }).ToList();

                ViewData["DescriptionOptions"] = descriptionOptions;

                return View(receipts);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Could not retrieve description options.";
                return RedirectToAction("Edit");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Receipts receipt)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var customerDescriptions = await _customerRepo.GetDescriptionsAsync();
                    var descriptionOptions = customerDescriptions.Select(description => new SelectListItem
                    {
                        Text = description,
                        Value = description
                    }).ToList();

                    ViewData["DescriptionOptions"] = descriptionOptions;

                    return View(receipt);
                }

                string description = receipt.description; // Assuming you have a property like SelectedDescription in the Receipts model.
                string itemNumber = await _customerRepo.GetItemNumberByDescriptionAsync(description);
                string unit = await _customerRepo.GetUnitByDescriptionAsync(description);

                receipt.item_number = itemNumber;
                receipt.unit = unit;

                bool editReceiptResult = await _receiptsRepo.UpdateAsync(receipt);

                if (editReceiptResult)
                {
                    TempData["SuccessMessage"] = "Successfully edited";
                    return RedirectToAction("DisplayTable", new
                    {
                        item_number = receipt.item_number,
                        rect_date = receipt.rect_date,
                        rate = receipt.rate,
                        quantity =receipt.quantity,
                        unit = receipt.unit
                    });
                }
                else
                {
                    TempData["ErrorMessage"] = "Could not be edited";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Could not be edited";
            }
            return View(receipt);
        }




        [HttpGet]
        public async Task<IActionResult> GetReceiptById(int id)
        {
            try
            {
                var query = "sp_get_Receipts";
                var parameters = new { id }; // Assuming 'id' is the parameter for the stored procedure

                var result = await _db.LoadData<Receipts, dynamic>(query, id);

                // Check if a receipt was found
                if (result != null && result.Any())
                {
                    var data = result.First(); // Assuming you're expecting a single receipt
                    return View(data); // Return a view to display the receipt details
                }
                else
                {
                    return View("NotFound"); // Return a "Not Found" view if the receipt is not found
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or return an error view
                return View("Error");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetItemNumberAndUnit(string description)
        {
            try
            {
                // Retrieve item_number and unit based on the selected description
                string itemNumber = await _customerRepo.GetItemNumberByDescriptionAsync(description);
                string unit = await _customerRepo.GetUnitByDescriptionAsync(description);

                // Create an anonymous object with the retrieved data and return it as JSON
                var result = new
                {
                    item_number = itemNumber,
                    unit = unit
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Handle errors and return an appropriate response if needed
                return BadRequest("Error occurred while fetching data.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DisplayTable(string item_number, string description, string unit, DateTime rect_date, decimal rate, decimal quantity)
        {
            try
            {
                // Fetch data from the repository based on the specified parameters
                var data = await _receiptsRepo.GetAllAsync();

                // Pass the retrieved data to the view
                return View(data);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                TempData["ErrorMessage"] = "Error occurred while fetching data.";
                return RedirectToAction("Add");
            }

        }
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _receiptsRepo.DeleteAsync(id);
            return RedirectToAction(nameof(DisplayTable));
        }
    }
}