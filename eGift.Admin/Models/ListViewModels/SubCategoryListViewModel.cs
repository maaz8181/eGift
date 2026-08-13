using eGift.Admin.Models.ViewModels;

namespace eGift.Admin.Models.ListViewModels;

public class SubCategoryListViewModel
{
    #region List View Model Properties

    public List<SubCategoryViewModel> SubCategoryList { get; set; } = new List<SubCategoryViewModel>();

    #endregion
}