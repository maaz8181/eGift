using eGift.Admin.Models.ViewModels;

namespace eGift.Admin.Models.ListViewModels;

public class AddressListViewModel
{
    #region List View Model Properties
    public List<AddressViewModel> AddressList { get; set; } = new List<AddressViewModel>();
    #endregion
}