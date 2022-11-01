using TrackTheGains.MAUI.ViewModels;

namespace TrackTheGains.MAUI.Views;

public partial class WorkoutEditPage : ContentPage
{
	public WorkoutEditViewModel ViewModel { get; set; }
	public WorkoutEditPage(WorkoutEditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel = viewModel;
		ExcerciseView.ChildrenReordered += (obj, e) => ViewModel.RecalculateOrderNumberCommand.Execute(this);
    }

	private async void ToolbarItem_Clicked(object sender, EventArgs e)
	{
		if (await DisplayAlert("Remove Workout", "Do you want to remove this Workout?", "yes", "no") == false)
			return;

        await ViewModel.RemoveWorkout();
        await Shell.Current.GoToAsync("..");
    }
}