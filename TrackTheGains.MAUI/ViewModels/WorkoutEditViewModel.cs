using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TrackTheGains.MAUI.Models;

namespace TrackTheGains.MAUI.ViewModels
{
    [QueryProperty(nameof(Workout), "Workout")]
    public partial class WorkoutEditViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        Workout workout = new();

        [ObservableProperty]
        bool isEdit = false;

        public WorkoutEditViewModel()
        {
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if(query.Count == 0)
            {
                IsEdit = false;
                Title = "New Workout";
            }
            else
            {
                IsEdit = true;
                Title = "Edit Workout";
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
