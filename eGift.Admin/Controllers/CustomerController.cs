

using eGift.Admin.Common;
using eGift.Admin.Helpers;
using eGift.Admin.Models.ListViewModels;
using eGift.Admin.Models.ResponseViewModel;
using eGift.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eGift.Admin.Controllers;

public class CustomerController : Controller
{
    #region Fields

    private readonly WebClientHelper _webClient;
    private readonly ILogger<CustomerController> _logger;

    #endregion

    #region Constructors

    public CustomerController(WebClientHelper webClient, ILogger<CustomerController> logger)
    {
        _webClient = webClient;
        _logger = logger;
    }

    #endregion

    #region Customer Default CRUD

    // GET: Customer
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var customers = new CustomerListViewModel();

            customers.CustomerList = await _webClient
                .GetAsync<List<CustomerResponseViewModel>>(
                    "/api/customer") ?? new List<CustomerResponseViewModel>();

            return View(customers);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Exception in CustomerController Index: {Message}",
                ex.Message);

            TempData["ToastrType"] = ToastrType.Error.ToString();
            TempData["ToastrMessage"] = ex.Message;

            return View();
        }
    }

    // GET: Customer/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var customer = await _webClient
                .GetAsync<CustomerResponseViewModel>(
                    $"/api/customer/{id}");

            if (customer == null)
            {
                TempData["ToastrType"] = ToastrType.Error.ToString();
                TempData["ToastrMessage"] = "Customer not found.";

                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Exception in CustomerController Details/{id}: {Message}",
                id,
                ex.Message);

            TempData["ToastrType"] = ToastrType.Error.ToString();
            TempData["ToastrMessage"] = ex.Message;

            return RedirectToAction(nameof(Index));
        }
    }

    // GET: Customer/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            var model = new CustomerViewModel();

            // Load dropdown data here
            await LoadDropdowns(model);

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Exception in CustomerController Create GET: {Message}",
                ex.Message);

            TempData["ToastrType"] = ToastrType.Error.ToString();
            TempData["ToastrMessage"] = ex.Message;

            return RedirectToAction(nameof(Index));
        }
    }

    // POST: Customer/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustomerViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns(model);

                return View(model);
            }

            model.CreatedBy =
                Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            model.CreatedDate = DateTime.Now;

            var response = await _webClient
                .PostAsync<CustomerViewModel, CustomerViewModel>(
                    "/api/customer",
                    model);

            if (response == null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to create customer.");

                TempData["ToastrType"] =
                    ToastrType.Error.ToString();

                TempData["ToastrMessage"] =
                    "Unable to create customer.";

                await LoadDropdowns(model);

                return View(model);
            }

            TempData["ToastrType"] =
                ToastrType.Success.ToString();

            TempData["ToastrMessage"] =
                "Customer created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Exception in CustomerController Create POST: {Message}",
                ex.Message);

            TempData["ToastrType"] =
                ToastrType.Error.ToString();

            TempData["ToastrMessage"] = ex.Message;

            await LoadDropdowns(model);

            return View(model);
        }
    }

    // GET: Customer/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var model = await _webClient
                .GetAsync<CustomerViewModel>(
                    $"/api/customer/{id}");

            if (model == null)
            {
                TempData["ToastrType"] =
                    ToastrType.Error.ToString();

                TempData["ToastrMessage"] =
                    "Customer not found.";

                return RedirectToAction(nameof(Index));
            }

            // Load dropdown data here
            await LoadDropdowns(model);

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Exception in CustomerController Edit GET: {Message}",
                ex.Message);

            TempData["ToastrType"] =
                ToastrType.Error.ToString();

            TempData["ToastrMessage"] = ex.Message;

            return RedirectToAction(nameof(Index));
        }
    }

    // POST: Customer/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        CustomerViewModel model)
    {
        try
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                foreach (var item in ModelState)
                {
                    foreach (var error in item.Value.Errors)
                    {
                        Console.WriteLine(
                            $"{item.Key} : {error.ErrorMessage}");
                    }
                }
                await LoadDropdowns(model);

                return View(model);
            }

            model.UpdatedBy =
                Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            model.UpdatedDate = DateTime.Now;

            var success = await _webClient
                .PutAsync<CustomerViewModel, CustomerViewModel>(
                    $"/api/customer/{id}",
                    model);

            if (!success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to update customer.");

                TempData["ToastrType"] =
                    ToastrType.Error.ToString();

                TempData["ToastrMessage"] =
                    "Unable to update customer.";

                await LoadDropdowns(model);

                return View(model);
            }

            TempData["ToastrType"] =
                ToastrType.Success.ToString();

            TempData["ToastrMessage"] =
                "Customer updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Exception in CustomerController Edit POST: {Message}",
                ex.Message);

            TempData["ToastrType"] =
                ToastrType.Error.ToString();

            TempData["ToastrMessage"] =
                ex.Message;

            await LoadDropdowns(model);

            return View(model);
        }
    }

    // POST: Customer/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            int loginUserId =
                Convert.ToInt32(
                    HttpContext.Session.GetInt32("UserId"));

            await _webClient.DeleteAsync(
                $"/api/customer/{id}" +
                $"?loginUserId={loginUserId}" +
                $"&deletedDate={DateTime.Now.ToDateTimeString()}");

            TempData["ToastrType"] =
                ToastrType.Success.ToString();

            TempData["ToastrMessage"] =
                "Customer deleted successfully.";
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Exception in CustomerController Delete POST: {Message}",
                ex.Message);

            TempData["ToastrType"] =
                ToastrType.Error.ToString();

            TempData["ToastrMessage"] =
                ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Private Methods

    private async Task LoadDropdowns(CustomerViewModel model)
    {
        var addresses = await _webClient
            .GetAsync<List<AddressViewModel>>(
                "/api/address");

        var genders = await _webClient
            .GetAsync<List<GenderViewModel>>(
                "/api/gender");

        var roles = await _webClient
            .GetAsync<List<RoleViewModel>>(
                "/api/role");

        model.Addresses = new SelectList(
            addresses ?? [],
            "Id",
            "FullAddress",
            model.AddressId);

        model.Genders = new SelectList(
            genders ?? [],
            "Id",
            "GenderName",
            model.GenderId);

        model.Roles = new SelectList(
            roles ?? [],
            "Id",
            "RoleName",
            model.RoleId);
    }

    #endregion
}