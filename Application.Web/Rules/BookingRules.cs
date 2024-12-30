using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Web.Rules;

public static class BookingRules
{
    // Main validation method
    public static bool Validate(AnimalDto animalToAdd, List<AnimalDto> selectedAnimals, DateOnly date,
        CustomerCardType? customerCard)
    {
        bool allowedSelected = NoLionOrPolarBearWithFarmAnimal(animalToAdd, selectedAnimals) &&
                               NoPenguinOnWeekend(animalToAdd, date) &&
                               NoDesertAnimalOctToFeb(animalToAdd, date) &&
                               NoSnowAnimalJunToAug(animalToAdd, date);
        
        bool allowedCustomerCard = customerCard switch
        {
            null => IsAllowedForNoCard(animalToAdd, selectedAnimals),
            CustomerCardType.Silver => IsAllowedForSilverCard(animalToAdd, selectedAnimals),
            CustomerCardType.Gold => IsAllowedForGoldCard(animalToAdd),
            _ => customerCard != CustomerCardType.Platinum || IsAllowedForPlatinumCard(animalToAdd)
        };
        
        return allowedSelected && allowedCustomerCard;
    }
    
    // Combinations
    private static bool NoLionOrPolarBearWithFarmAnimal(AnimalDto animalToAdd, List<AnimalDto> selectedAnimals)
    {
        return !selectedAnimals.Any(
                   a => a.Name.Equals("leeuw", StringComparison.CurrentCultureIgnoreCase) 
                        || a.Name.Equals("ijsbeer", StringComparison.CurrentCultureIgnoreCase))
               || animalToAdd.Type != AnimalType.Farm;
    }
    
    // Time and season based
    private static bool NoPenguinOnWeekend(AnimalDto animalToAdd, DateOnly date)
    {
        return !animalToAdd.Name.Equals(
            "pinguin", StringComparison.CurrentCultureIgnoreCase) 
               || (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday);
    }
    
    private static bool NoDesertAnimalOctToFeb(AnimalDto animalToAdd, DateOnly date)
    {
        return animalToAdd.Type != AnimalType.Desert 
               || (date.Month >= 3 && date.Month <= 9);
    }
    
    private static bool NoSnowAnimalJunToAug(AnimalDto animalToAdd, DateOnly date)
    {
        return animalToAdd.Type != AnimalType.Snow 
               || (date.Month >= 9 && date.Month <= 5);
    }
    
    // Customer card based
    private static bool IsAllowedForNoCard(AnimalDto animalToAdd, List<AnimalDto> selectedAnimals)
    {
        return animalToAdd.Type != AnimalType.Vip
            && selectedAnimals.Count < 3;
    }

    private static bool IsAllowedForSilverCard(AnimalDto animalToAdd, List<AnimalDto> selectedAnimals)
    {
        return animalToAdd.Type != AnimalType.Vip
               && selectedAnimals.Count < 4;
    }

    private static bool IsAllowedForGoldCard(AnimalDto animalToAdd)
    {
        return animalToAdd.Type != AnimalType.Vip;
    }
    
    private static bool IsAllowedForPlatinumCard(AnimalDto animalToAdd)
    {
        return true;
    }
}