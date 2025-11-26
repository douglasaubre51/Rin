using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using System.Diagnostics;

namespace Rin.PageModels;

public partial class MainPageModel : BasePageModel
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFirstBtnNotBusy))]
    private bool isFirstBtnBusy;
    public bool IsFirstBtnNotBusy => !IsFirstBtnBusy;

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
                Debug.WriteLine($"First location doesn't exist in map!");
                return;
            }

            locations = await Geocoding.Default.GetLocationsAsync(SecondLocationName);
            Location? secondLocation = GetLatitudeAndLongitude(locations);
            if (secondLocation is null)
            {
                Debug.WriteLine($"Second location doesn't exist in map!");
                return;
            }

            double distance = Location.CalculateDistance(
                firstLocation,
                secondLocation,
                DistanceUnits.Kilometers);
            TotalDistance = distance.ToString("#.##");
            await Shell.Current.DisplayAlertAsync(
                "Result",
                $"Distance between {FirstLocationName} and {SecondLocationName} : {TotalDistance} km",
                "Thanks");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("FindDistanceBetween error: " + ex.Message);
        }
    }
}
