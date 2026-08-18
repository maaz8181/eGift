
using eGift.Admin.Common;
using eGift.Admin.Helpers;
using eGift.Admin.Models.ListViewModels;
using eGift.Admin.Models.ResponseViewModel;
using eGift.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eGift.Admin.Controllers;

public class EmployeeController : Controller
{
    #region Fields
    private readonly WebClientHelper _webClient;
    private readonly ILogger<EmployeeController> _logger;
    #endregion

    #region Constructors
    public EmployeeController(
        WebClientHelper webClient,
        ILogger<EmployeeController> logger)
    {
        _webClient = webClient;
        _logger = logger;
    }
    #endregion

    #region Employee Default CRUD
    // GET: Employee
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var employees = new EmployeeListViewModel();
            employees.EmployeeList = await _webClient
                .GetAsync<List<EmployeeResponseViewModel>>("/api/employee") ?? new List<EmployeeResponseViewModel>();

            return View(employees);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in EmployeeController Index: {Message}", ex.Message);

            TempData["ToastrType"] = ToastrType.Error.ToString();
            TempData["ToastrMessage"] = ex.Message;

            return View();
        }
    }

    // GET: Employee/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var employee = await _webClient
                .GetAsync<EmployeeResponseViewModel>(
                    $"/api/employee/{id}");

            if (employee == null)
            {
                TempData["ToastrType"] = ToastrType.Error.ToString();
                TempData["ToastrMessage"] = "Employee not found.";

                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in EmployeeController Details/{id}: {Message}", id, ex.Message);

            TempData["ToastrType"] = ToastrType.Error.ToString();
            TempData["ToastrMessage"] = ex.Message;

            return RedirectToAction(nameof(Index));
        }
    }

    // GET: Employee/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            var model = new EmployeeViewModel();
            
            // Load dropdown data here
            await LoadDropdowns(model);

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in EmployeeController Create GET: {Message}", ex.Message);

            TempData["ToastrType"] = ToastrType.Error.ToString();
            TempData["ToastrMessage"] = ex.Message;

            return RedirectToAction(nameof(Index));
        }
    }

    // POST: Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                // await LoadDropdowns();
                return View(model);
            }

            model.CreatedBy= Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            model.CreatedDate= DateTime.Now;

            var response = await _webClient.PostAsync<EmployeeViewModel, EmployeeViewModel>(
                    "/api/employee", model);

            if (response == null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to create employee.");

                TempData["ToastrType"] = ToastrType.Error.ToString();
                TempData["ToastrMessage"] = "Unable to create employee.";

                // await LoadDropdowns();
                return View(model);
            }

            TempData["ToastrType"] = ToastrType.Success.ToString();
            TempData["ToastrMessage"] = "Employee created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in EmployeeController Create POST: {Message}", ex.Message);

            TempData["ToastrType"] = ToastrType.Error.ToString();
            TempData["ToastrMessage"] = ex.Message;

            // await LoadDropdowns();
            return View(model);
        }
    }

    // GET: Employee/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var model = await _webClient
                .GetAsync<EmployeeViewModel>(
                    $"/api/employee/{id}");

            if (model == null)
            {
                TempData["ToastrType"] = ToastrType.Error.ToString();
                TempData["ToastrMessage"] = "Employee not found.";

                return RedirectToAction(nameof(Index));
            }

            // Load dropdown data here
            await LoadDropdowns(model);

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in EmployeeController Edit GET: {Message}", ex.Message);

            TempData["ToastrType"] = ToastrType.Error.ToString();
            TempData["ToastrMessage"] = ex.Message;

            return RedirectToAction(nameof(Index));
        }
    }

    // POST: Employee/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
    {
        try
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // Load dropdown data here
                await LoadDropdowns(model);

                return View(model);
            }

            model.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            model.UpdatedDate = DateTime.Now;

            var success = await _webClient.PutAsync<EmployeeViewModel, EmployeeViewModel>(
                    $"/api/employee/{id}", model);

            if (!success)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to update employee.");

                TempData["ToastrType"] = ToastrType.Error.ToString();
                TempData["ToastrMessage"] = "Unable to update employee.";

                // Load dropdown data here
                await LoadDropdowns(model);

                return View(model);
            }

            TempData["ToastrType"] = ToastrType.Success.ToString();
            TempData["ToastrMessage"] = "Employee updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in EmployeeController Edit POST: {Message}", ex.Message);

            TempData["ToastrType"] = ToastrType.Error.ToString();
            TempData["ToastrMessage"] = ex.Message;

            // Load dropdown data here
            await LoadDropdowns(model);

            return View(model);
        }
    }

    // POST: Employee/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            int loginUserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            await _webClient.DeleteAsync(
                $"/api/employee/{id}?loginUserId={loginUserId}&deletedDate={DateTime.Now.ToDateTimeString()}");

            TempData["ToastrType"] = ToastrType.Success.ToString();
            TempData["ToastrMessage"] = "Employee deleted successfully.";
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in EmployeeController Delete POST: {Message}", ex.Message);

            TempData["ToastrType"] = ToastrType.Error.ToString();
            TempData["ToastrMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
    #endregion

    #region Private Methods
    private async Task LoadDropdowns(EmployeeViewModel model)
    {
        var addresses = await _webClient
        .GetAsync<List<AddressViewModel>>("/api/address");

        var genders = await _webClient
            .GetAsync<List<GenderViewModel>>("/api/gender");

        var roles = await _webClient
            .GetAsync<List<RoleViewModel>>("/api/role");

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