using CommunityToolkit.Mvvm.ComponentModel;

namespace Rin.PageModels;

public partial class BasePageModel : ObservableObject
{
    [ObservableProperty]
    private bool isBusy;
}
