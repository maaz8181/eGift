using eGift.Admin.Models.ViewModels;

namespace eGift.Admin.Models.ListViewModels;

public class OrderListViewModel
{
    #region List View Model Properties

    public List<OrderViewModel> OrderList { get; set; } = new List<OrderViewModel>();

    #endregion
}