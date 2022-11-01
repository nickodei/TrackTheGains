using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TrackTheGains.MAUI.Models;
using TrackTheGains.MAUI.Services;

namespace TrackTheGains.MAUI.ViewModels
{
    [QueryProperty("Id", "Id")]
    public partial class WorkoutEditViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        Workout workout = new();

        [ObservableProperty]
        bool isEdit = false;

        private readonly IWorkoutsClient _workoutClient;
        public WorkoutEditViewModel(IWorkoutsClient workoutClient)
        {
            _workoutClient = workoutClient;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if(query.TryGetValue("Id", out var id))
            {
                var result = await _workoutClient.GetWorkoutAsync(Guid.Parse(id.ToString()));
                if(result != null)
                {
                    Workout = new Workout()
                    {
                        Name = result.Name,
                        Excercises = result.Exercises.Select(ex => new Excercise { Name = ex.Name, OrderNr = ex.OrderNr}).ToObservableCollection()
                    };
                }

                IsEdit = true;
                Title = "Edit Workout";
            }
            else
            {
                IsEdit = false;
                Title = "New Workout";
            }
        }

        [RelayCommand]
        public void AddExcercise(string name)
        {
            Workout.Excercises.Add(new Excercise() { Name = name, OrderNr = Workout.Excercises.Count + 1});
        }

        [RelayCommand]
        public void RecalculateOrderNumber()
        {
            List<Excercise> list = Workout.Excercises.ToList();

            Workout.Excercises.Clear();
            foreach (Excercise excercise in list)
            {
                excercise.OrderNr = list.IndexOf(excercise) + 1;
                Workout.Excercises.Add(excercise);
            }
        }

        [RelayCommand]
        public async Task SaveWorkout()
        {
            if (!App.Workouts.Contains(Workout))
                App.Workouts.Add(Workout);

            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task RemoveWorkout()
        {
            if (App.Workouts.Contains(Workout))
                App.Workouts.Remove(Workout);
        }
    }
}
