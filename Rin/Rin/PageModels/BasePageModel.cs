using CommunityToolkit.Mvvm.ComponentModel;

namespace Rin.PageModels;

public partial class BasePageModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    public bool IsNotBusy => !IsBusy;
}
