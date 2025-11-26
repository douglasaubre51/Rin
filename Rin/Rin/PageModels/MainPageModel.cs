using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using System.Diagnostics;

namespace Rin.PageModels;

public partial class MainPageModel : BasePageModel
{
    [ObservableProperty]
    private bool initialLoad = false;

    [ObservableProperty]
    private string firstLocationName = string.Empty;
    [ObservableProperty]
    private string secondLocationName = string.Empty;

    [ObservableProperty]
    private string totalDistance = string.Empty;

    [ObservableProperty]
    private ObservableRangeCollection<Location> locationsFirst = [];
    [ObservableProperty]
    private ObservableRangeCollection<Location> locationsSecond = [];

    private async Task ShowWarningToast(string msg)
    {
        var toast = Toast.Make(
            msg,
            ToastDuration.Short,
            14);
        await toast.Show();
    }

    private Location? GetLatitudeAndLongitude(IEnumerable<Location> locations)
    {
        var place = locations.FirstOrDefault();
        if (place is null)
            return null;

        Debug.WriteLine($"Latitude: {place.Latitude}");
        Debug.WriteLine($"Latitude: {place.Longitude}");

        return new Location
        {
            Latitude = place.Latitude,
            Longitude = place.Longitude,
        };
    }


    [RelayCommand]
    async Task FindDistanceBetween()
    {
        try
        {
            var locations = await Geocoding.Default.GetLocationsAsync(FirstLocationName);
            Location? firstLocation = GetLatitudeAndLongitude(locations);
            if (firstLocation is null)
            {
                string msg = "First location doesn't exist in map !";
                Debug.WriteLine(msg);
                await ShowWarningToast(msg);
                return;
            }

            locations = await Geocoding.Default.GetLocationsAsync(SecondLocationName);
            Location? secondLocation = GetLatitudeAndLongitude(locations);
            if (secondLocation is null)
            {
                string msg = "Second location doesn't exist in map !";
                Debug.WriteLine(msg);
                await ShowWarningToast(msg);
                return;
            }

            double distance = Location.CalculateDistance(
                firstLocation,
                secondLocation,
                DistanceUnits.Kilometers);

            InitialLoad = true;
            TotalDistance = distance.ToString("#.##");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("FindDistanceBetween error: " + ex.Message);
            await ShowWarningToast("Enter all fields !");
        }
    }
}
