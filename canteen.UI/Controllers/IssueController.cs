using canteen.Data.DataAccess;
using canteen.Data.Models.Domain;
using canteen.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace canteen.UI.Controllers
{
    public class IssueController : Controller
    {
        private readonly IissueRepository _issueRepo;
        private readonly ISqlDataAccess _db;

        public IssueController(IissueRepository issueRepo, ISqlDataAccess db)
        {
            _issueRepo = issueRepo;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var receiptDescriptions = await _issueRepo.GetReceiptsDescriptionsAsync();
                var descriptionOptions = receiptDescriptions.Select(description => new SelectListItem
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
        public async Task<IActionResult> Add(Issue issue)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var receiptDescriptions = await _issueRepo.GetReceiptsDescriptionsAsync();
                    var descriptionOptions = receiptDescriptions.Select(description => new SelectListItem
                    {
                        Text = description,
                        Value = description
                    }).ToList();

                    ViewData["DescriptionOptions"] = descriptionOptions;

                    return View(issue);
                }

                string description = issue.description;

                Issue receiptData = await _issueRepo.GetReceiptParametersByDescription(description);

                if (receiptData != null)
                {
                    // Map the values from receiptData to the corresponding properties of the issue
                    issue.item_number = receiptData.item_number;
                    issue.rect_date = receiptData.rect_date;
                    issue.rate = receiptData.rate;

                    // Parse the quantity value from the text input
                    if (decimal.TryParse(Request.Form["quantity"], out decimal quantity))
                    {
                        issue.quantity = quantity;

                        bool addIssueResult = await _issueRepo.AddAsync(issue);

                        if (addIssueResult)
                        {
                            TempData["SuccessMessage"] = "Successfully added";
                            return RedirectToAction("DisplayTable", new
                            {
                                item_number = issue.item_number,
                                rect_date = issue.rect_date,
                                rate = issue.rate,
                                quantity = issue.quantity,
                            });
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Could not be added";
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid quantity format.";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Receipt data not found for the selected description.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Could not be added";
            }
            return View(issue);
        }
        public async Task<IActionResult> GetItemsForDropdown(string category)
        {
            try
            {
                var data = await _issueRepo.GetItemsForDropdownAsync(category);

                if (data.Count == 0)
                    return NotFound();

                return Json(data);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (log or return a specific error response)
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> GetByItemMenu(string category)
        //{
        //    var customer = await _issueRepo.GetByIdMenus(category);
        //    //if (person == null)
        //    //  return NotFound();
        //    return View(GetByItemMenu);
        //}

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                Issue issue = await _issueRepo.GetByIdAsync(id);
                var receiptDescriptions = await _issueRepo.GetReceiptsDescriptionsAsync();
                var descriptionOptions = receiptDescriptions.Select(description => new SelectListItem
                {
                    Text = description,
                    Value = description
                }).ToList();

                ViewData["DescriptionOptions"] = descriptionOptions;

                return View(issue);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Could not retrieve description options.";
                return RedirectToAction("Edit");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Issue issue)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var receiptDescriptions = await _issueRepo.GetReceiptsDescriptionsAsync();
                    var descriptionOptions = receiptDescriptions.Select(description => new SelectListItem
                    {
                        Text = description,
                        Value = description
                    }).ToList();

                    ViewData["DescriptionOptions"] = descriptionOptions;

                    return View(issue);
                }

                // Ensure you correctly handle the quantity input just as in the "Add" action

                string description = issue.description;
                Issue receiptData = await _issueRepo.GetReceiptParametersByDescription(description);

                if (receiptData != null)
                {
                    issue.item_number = receiptData.item_number;
                    issue.rect_date = receiptData.rect_date;
                    issue.rate = receiptData.rate;

                    // Ensure that you correctly handle the quantity input
                    if (decimal.TryParse(Request.Form["quantity"], out decimal quantity))
                    {
                        issue.quantity = quantity;
                    }
                    else
                    {
                        // Handle the case where quantity is not a valid decimal
                        ModelState.AddModelError("quantity", "Please enter a valid quantity.");
                        var customerDescriptions = await _issueRepo.GetReceiptsDescriptionsAsync();
                        var descriptionOptions = customerDescriptions.Select(d => new SelectListItem
                        {
                            Text = d,
                            Value = d
                        }).ToList();

                        ViewData["DescriptionOptions"] = descriptionOptions;

                        return View(issue);
                    }

                    bool editIssueResult = await _issueRepo.UpdateAsync(issue);

                    if (editIssueResult)
                    {
                        TempData["SuccessMessage"] = "Successfully edited";
                        return RedirectToAction("DisplayTable", new
                        {
                            item_number = issue.item_number,
                            rect_date = issue.rect_date,
                            rate = issue.rate,
                            quantity = issue.quantity,
                        });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Could not be edited";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Receipt data not found for the selected description.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Could not be edited";
            }
            return View(issue);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _issueRepo.DeleteAsync(id);
            return RedirectToAction(nameof(DisplayTable));
        }

        [HttpGet]
        public async Task<IActionResult> GetReceiptsDescriptionsAsync()
        {
            try
            {
                // Fetch Receipts data based on the selected description
                var data = await _issueRepo.GetReceiptsDescriptionsAsync();

                // Use the DisplayTable view to render the data
                return View("Add", data);
            }
            catch (Exception ex)
            {
                // Handle errors and return an appropriate response
                return BadRequest("Error occurred while fetching data.");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetReceiptParametersByDescription(string description)
        {
            try
            {
                // Fetch data based on the selected description
                var data = await _issueRepo.GetReceiptParametersByDescription(description);

                if (data != null)
                {
                    // Assuming data is a single Issue object
                    return Json(data); // Return the data as JSON
                }
                else
                {
                    // Return a 404 Not Found response if the data is not found
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex.Message);

                // Return a 500 Internal Server Error response
                return StatusCode(500);
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetReceiptsDataByDescription(string description)
        {
            try
            {
                // Example: SELECT * FROM Receipts WHERE description = @description
                string query = "SELECT item_number, description, rect_date, rate, quantity FROM Receipts WHERE description = @Description";
                var parameters = new { Description = description };
                var data = await _db.LoadData<Issue, dynamic>(query, parameters);

                // Build an HTML table with the retrieved data
                var tableRows = "";
                foreach (var item in data)
                {
                    tableRows += $@"
                <tr>
                    <td><input type='radio' name='selectedReceipt' value='{item.item_number}' data-rect-date='{item.rect_date}' data-rate='{item.rate}' data-quantity='{item.quantity}' /></td>
                    <td>{item.item_number}</td>
                    <td>{item.description}</td>
                    <td>{item.rect_date}</td>
                    <td>{item.rate}</td>
                    <td>{item.quantity}</td>
                </tr>";
                }

                return Content(tableRows, "text/html"); // Return HTML content for the table rows
            }
            catch (Exception ex)
            {
                // Handle exceptions or return an empty table to indicate failure
                return Content(string.Empty, "text/html");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DisplayTable(string item_number, string description, DateTime rect_date, decimal rate, decimal quantity, DateTime issd_date, string category)
        {
            try
            {
                var data = await _issueRepo.GetAllAsync();

                return View(data);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error occurred while fetching data.";
                return RedirectToAction("Add");
            }
        }
    }
}
