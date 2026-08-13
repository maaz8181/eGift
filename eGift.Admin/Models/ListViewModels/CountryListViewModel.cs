using eGift.Admin.Models.ViewModels;

namespace eGift.Admin.Models.ListViewModels;

public class CountryListViewModel
{
    #region List View Model Properties

    public List<CountryViewModel> CountryList { get; set; } = new List<CountryViewModel>();

    #endregion
}