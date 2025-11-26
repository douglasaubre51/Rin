using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace Rin.PageModels;

public partial class MainPageModel : BasePageModel
{
    [RelayCommand]
    async Task SetFirstLocation()
    {
        try
        {
            var location = await Geolocation.Default.GetLastKnownLocationAsync();
            if (location is null)
            {
                Debug.WriteLine("location is null!");
                return;
            }
            Debug.WriteLine($"Lattitude: {location.Latitude}");
            Debug.WriteLine($"Lattitude: {location.Longitude}");

            if (await Map.Default.TryOpenAsync(location.Latitude, location.Longitude) is false)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Api error",
                    "Error loading map!",
                    "Ok");

                return;
            }

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync(
                "Rin experienced an error!",
                "Something went wrong!",
                "Ok");
            Debug.WriteLine($"MainPageModel error: {ex.Message}");
        }
    }

    [RelayCommand]
    async Task SetSecondLocation()
    {

    }

    [RelayCommand]
    async Task FindDistanceBetween()
    {

    }
}
