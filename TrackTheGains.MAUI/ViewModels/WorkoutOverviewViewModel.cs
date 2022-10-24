using CommunityToolkit.Mvvm.Input;
using DailyManager.Web.Client.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TrackTheGains.MAUI.Models;
using TrackTheGains.Shared.Queries;

namespace TrackTheGains.MAUI.ViewModels
{
    public partial class WorkoutOverviewViewModel : BaseViewModel
    {
        public ObservableCollection<WorkoutOverviewVM> Workouts { get; private set; } = new();

        private readonly IWorkoutClient _workoutClient;
        public WorkoutOverviewViewModel(IWorkoutClient workoutClient)
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
        public async Task GoToEditWorkoutPage(WorkoutOverviewVM workout)
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
