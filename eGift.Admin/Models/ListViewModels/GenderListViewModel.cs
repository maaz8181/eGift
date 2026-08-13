using eGift.Admin.Models.ViewModels;

namespace eGift.Admin.Models.ListViewModels;

public class GenderListViewModel
{
    #region List View Model Properties

    public List<GenderViewModel> GenderList { get; set; } = new List<GenderViewModel>();

    #endregion
}