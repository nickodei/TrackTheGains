namespace TrackTheGains.WebApi.WorkoutSessions;

public record WorkoutSessionCreateDTO(
    Guid WorkoutId,
    DateTime StartingTime,
    DateTime EndingTime,
    List<FinishedExerciseCreateDTO> FinishedExercises
);

public record FinishedExerciseCreateDTO(
    Guid ExerciseId,
    List<SetCreateDTO> Sets
);

public record SetCreateDTO(
    int Order,
    List<RepetitionCreateDTO> Repetitions
);

public record RepetitionCreateDTO(
    int Order,
    int WeightsInKg
);