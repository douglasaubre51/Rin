using Rin.PageModels;
using System.Diagnostics;

namespace Rin;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    private async void SearchBarFirst_TextChanged(object sender, TextChangedEventArgs e)
    {
        var context = BindingContext as MainPageModel;
        if (context is null)
        {
            Debug.WriteLine("context is null!");
            return;
        }

        try
        {
            Debug.WriteLine("SearchBarFirst_TextChanged: " + e.NewTextValue);
            context.FirstLocationName = e.NewTextValue;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("SearchBarFirst_TextChanged error: " + ex.Message);
        }
    }
    private async void SearchBarSecond_TextChanged(object sender, TextChangedEventArgs e)
    {
        var context = BindingContext as MainPageModel;
        if (context is null)
        {
            Debug.WriteLine("context is null!");
            return;
        }

        try
        {
            Debug.WriteLine("SearchBarFirst_TextChanged: " + e.NewTextValue);
            context.SecondLocationName = e.NewTextValue;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("SearchBarSecond_TextChanged error: " + ex.Message);
        }
    }
}
