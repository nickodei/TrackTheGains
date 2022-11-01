using TrackTheGains.MAUI.ViewModels;

namespace TrackTheGains.MAUI.Views;

public partial class WorkoutOverviewPage : ContentPage
{
    public WorkoutOverviewViewModel ViewModel { get; set; }
    public WorkoutOverviewPage(WorkoutOverviewViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ViewModel.GetWorkoutsAsync();
    }
}