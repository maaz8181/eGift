using eGift.Admin.Models.ViewModels;

namespace eGift.Admin.Models.ListViewModels;

public class ProductListViewModel
{
    #region List View Model Properties

    public List<ProductViewModel> ProductList { get; set; } = new List<ProductViewModel>();

    #endregion
}