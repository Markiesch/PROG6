namespace Application.Data.Models;

public enum AnimalType
{
    Farm,
    Jungle,
    Snow,
    Desert,
    Vip
}

public static class AnimalTypeTranslation
{
    public static string ToDisplayName(this AnimalType type)
    {
        return type switch
        {
            AnimalType.Farm => "Boerderij",
            AnimalType.Jungle => "Jungle",
            AnimalType.Snow => "Sneeuw",
            AnimalType.Desert => "Woestijn",
            AnimalType.Vip => "VIP",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
