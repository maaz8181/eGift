using eGift.Admin.Models.ViewModels;

namespace eGift.Admin.Models.ListViewModels;

public class CustomerListViewModel
{
    #region List View Model Properties

    public List<CustomerViewModel> CustomerList { get; set; } = new List<CustomerViewModel>();

    #endregion
}