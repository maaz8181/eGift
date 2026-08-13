using eGift.Admin.Models.ViewModels;

namespace eGift.Admin.Models.ListViewModels;

public class CategoryListViewModel
{
    #region List View Model Properties

    public List<CategoryViewModel> CategoryList { get; set; } = new List<CategoryViewModel>();

    #endregion
}