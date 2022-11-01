using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TrackTheGains.MAUI.Services;
using TrackTheGains.Shared.Models.Workouts;

namespace TrackTheGains.MAUI.ViewModels
{
    public partial class WorkoutOverviewViewModel : BaseViewModel
    {
        public ObservableCollection<WorkoutOverviewVm> Workouts { get; private set; } = new();

        private readonly IWorkoutsClient _workoutClient;
        public WorkoutOverviewViewModel(IWorkoutsClient workoutClient)
        {
            _workoutClient = workoutClient;
        }

        public async Task GetWorkoutsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var workouts = await _workoutClient.GetWorkoutsAsync();

                if (Workouts.Count != 0)
                    Workouts.Clear();

                foreach(var workout in workouts)
                {
                    Workouts.Add(workout);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get workouts: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task GoToEditWorkoutPage(WorkoutOverviewVm workout)
        {
            await Shell.Current.GoToAsync($"workout-template/edit?Id={workout.Id}");
        }

        [RelayCommand]
        public async Task GoToNewWorkoutPage()
        {
            await Shell.Current.GoToAsync("workout-template/edit");
        }
    }
}
