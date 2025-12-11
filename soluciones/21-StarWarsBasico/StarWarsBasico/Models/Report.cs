namespace StarWarsBasico.Models;

public record Report(
    int MapSize,
    int NumberOfEnemies,
    int TimeMax,
    int NumberOfShots,
    int NumberOfHits,
    double Performance,
    int NumberOfLeftEnemies,
    int NumberOfDeadEnemies,
    Droid[] OrderedEnemiesByEnergy
);