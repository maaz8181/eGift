using eGift.Admin.Models.ViewModels;

namespace eGift.Admin.Models.ListViewModels;

public class CityListViewModel
{
    #region List View Model Properties

    public List<CityViewModel> CityList { get; set; } = new List<CityViewModel>();

    #endregion
}