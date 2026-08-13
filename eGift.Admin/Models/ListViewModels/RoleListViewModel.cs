using eGift.Admin.Models.ViewModels;

namespace eGift.Admin.Models.ListViewModels;

public class RoleListViewModel
{
    #region List View Model Properties

    public List<RoleViewModel> RoleList { get; set; } = new List<RoleViewModel>();

    #endregion
}