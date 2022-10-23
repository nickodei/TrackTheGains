using CommunityToolkit.Mvvm.Input;
using DailyManager.Web.Client.Services;
using System.Diagnostics;
using TrackTheGains.MAUI.Models;
using TrackTheGains.Shared.Queries;

namespace TrackTheGains.MAUI.ViewModels
{
    public partial class WorkoutOverviewViewModel : BaseViewModel
    {
        public List<WorkoutOverviewVM> Workouts => new();

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
        public async Task GoToEditWorkoutPage(Workout workout)
        {
            await Shell.Current.GoToAsync("workout-template/edit", true, new Dictionary<string, object>
            {
                {"Workout", workout }
            });
        }

        [RelayCommand]
        public async Task GoToNewWorkoutPage()
        {
            await Shell.Current.GoToAsync("workout-template/edit");
        }
    }
}
