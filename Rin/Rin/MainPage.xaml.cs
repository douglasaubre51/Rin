using Rin.PageModels;

namespace Rin;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

}
