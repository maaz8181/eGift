using eGift.Admin.Models.ViewModels;

namespace eGift.Admin.Models.ListViewModels;

public class EmployeeListViewModel
{
    #region List View Model Properties

    public List<EmployeeViewModel> EmployeeList { get; set; } = new List<EmployeeViewModel>();

    #endregion
}