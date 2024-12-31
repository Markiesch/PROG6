using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Web.Rules;

public static class BookingRules
{
    // Main validation method
    public static JsonContent Validate(AnimalDto animalToAdd, List<AnimalDto> selectedAnimals, DateOnly date,
        CustomerCardType? customerCard)
    {
        var result = new { isValid = true, reason = "" };

        if (!NoLionOrPolarBearWithFarmAnimal(animalToAdd, selectedAnimals))
        {
            result = new { isValid = false, reason = "Je mag geen beestje boeken met het type 'Leeuw' of 'IJsbeer' als je ook een beestje boekt van het type 'Boerderijdier'" };
        }
        else if (!NoPenguinOnWeekend(animalToAdd, date))
        {
            result = new { isValid = false, reason = "Je mag geen beestje boeken met de naam 'Pinguïn' in het weekend" };
        }
        else if (!NoDesertAnimalOctToFeb(animalToAdd, date))
        {
            result = new { isValid = false, reason = "Je mag geen beestje boeken van het type 'Woestijn' in de maanden oktober t/m februari" };
        }
        else if (!NoSnowAnimalJunToAug(animalToAdd, date))
        {
            result = new { isValid = false, reason = "Je mag geen beestje boeken van het type 'Sneeuw' in de maanden juni t/m augustus" };
        }
        else
        {
            result = customerCard switch
            {
                null when !IsAllowedForNoCard(animalToAdd, selectedAnimals) 
                    => new { isValid = false, reason = "Klanten zonder klantenkaart mogen maximaal 3 dieren boeken" },
                CustomerCardType.Silver when !IsAllowedForSilverCard(animalToAdd, selectedAnimals) 
                    => new { isValid = false, reason = "Klanten met een zilveren klantenkaart mogen 1 dier extra boeken" },
                CustomerCardType.Gold when !IsAllowedForGoldCard(animalToAdd) 
                    => new { isValid = false, reason = "Klanten met een gouden kaart mogen zoveel dieren boeken als ze willen" },
                CustomerCardType.Platinum when !IsAllowedForPlatinumCard(animalToAdd) 
                    => new { isValid = false, reason = "Klanten met een platina kaart mogen daarnaast ook nog de VIP dieren boeken." },
                _ => result
            };
        }

        return JsonContent.Create(result);
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