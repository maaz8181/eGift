using eGift.Admin.Models.ViewModels;

namespace eGift.Admin.Models.ListViewModels;

public class StateListViewModel
{
    #region List View Model Properties

    public List<StateViewModel> StateList { get; set; } = new List<StateViewModel>();

    #endregion
}