using Application.Data.Models;

namespace Application.Web.Helpers;

public class BookingRules
{
    // Combinations
    public bool NoLionOrPolarBearWithFarmAnimal(Animal animalToAdd, List<Animal> selectedAnimals)
    {
        return !selectedAnimals.Any(
                   a => a.Name.Equals("leeuw", StringComparison.CurrentCultureIgnoreCase) 
                        || a.Name.Equals("ijsbeer", StringComparison.CurrentCultureIgnoreCase))
               || animalToAdd.Type != AnimalType.Farm;
    }
    
    // Time and season based
    public bool NoPenguinOnWeekend(Animal animalToAdd, DateOnly date)
    {
        return !animalToAdd.Name.Equals(
            "pinguin", StringComparison.CurrentCultureIgnoreCase) 
               || (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday);
    }
    
    public bool NoDesertAnimalOctToFeb(Animal animalToAdd, DateOnly date)
    {
        return animalToAdd.Type != AnimalType.Desert 
               || (date.Month >= 3 && date.Month <= 9);
    }
    
    public bool NoSnowAnimalJunToAug(Animal animalToAdd, DateOnly date)
    {
        return animalToAdd.Type != AnimalType.Snow 
               || (date.Month >= 9 && date.Month <= 5);
    }
    
    // Customer card based
    public bool IsAllowedForNoCard(Animal animalToAdd, List<Animal> selectedAnimals)
    {
        return animalToAdd.Type != AnimalType.Vip
            && selectedAnimals.Count < 3;
    }

    public bool IsAllowedForSilverCard(Animal animalToAdd, List<Animal> selectedAnimals)
    {
        return animalToAdd.Type != AnimalType.Vip
               && selectedAnimals.Count < 4;
    }

    public bool IsAllowedForGoldCard(Animal animalToAdd)
    {
        return animalToAdd.Type != AnimalType.Vip;
    }
    
    public bool IsAllowedForPlatinumCard(Animal animalToAdd)
    {
        return true;
    }
}