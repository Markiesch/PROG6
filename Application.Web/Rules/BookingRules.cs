using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Web.Rules;

public static class BookingRules
{
    // Main validation method
    public static bool Validate(AnimalDto animalToAdd, List<AnimalDto> selectedAnimals, DateOnly date,
        CustomerCardType? customerCard, out string errorMessage)
    {
        errorMessage = string.Empty;
        
        return NoLionOrPolarBearWithFarmAnimal(animalToAdd, selectedAnimals, out errorMessage)
               && NoPenguinOnWeekend(animalToAdd, date, out errorMessage)
               && NoDesertAnimalOctToFeb(animalToAdd, date, out errorMessage)
               && NoSnowAnimalJunToAug(animalToAdd, date, out errorMessage)
               && ValidateCustomerCard(animalToAdd, selectedAnimals, customerCard, out errorMessage);
    }
    
    // Combinations
    private static bool NoLionOrPolarBearWithFarmAnimal(AnimalDto animalToAdd, List<AnimalDto> selectedAnimals, out string errorMessage)
    {
        errorMessage = "Je mag geen beestje boeken met het type ‘Leeuw’ of ‘IJsbeer’ als je ook een beestje boekt van het type ‘Boerderijdier";
        var isValid = !selectedAnimals.Any(a => a.Name.Equals("leeuw", StringComparison.CurrentCultureIgnoreCase) 
                                                || a.Name.Equals("ijsbeer", StringComparison.CurrentCultureIgnoreCase))
               || animalToAdd.Type != AnimalType.Farm;
        
        if (isValid) errorMessage = string.Empty;
        return isValid;
    }
    
    // Time and season based
    private static bool NoPenguinOnWeekend(AnimalDto animalToAdd, DateOnly date, out string errorMessage)
    {
        errorMessage = "Je mag geen beestje boeken met de naam ‘Pinguïn’ in het weekend";
        var isValid = !animalToAdd.Name.Equals(
            "pinguin", StringComparison.CurrentCultureIgnoreCase) 
               || (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday);
        
        if (isValid) errorMessage = string.Empty;
        return isValid;
    }
    
    private static bool NoDesertAnimalOctToFeb(AnimalDto animalToAdd, DateOnly date, out string errorMessage)
    {
        errorMessage = "Je mag geen beestje boeken van het type ‘Woestijn’ in de maanden oktober t/m februari";
        var isValid = animalToAdd.Type != AnimalType.Desert 
               || (date.Month >= 3 && date.Month <= 9);
        
        if (isValid) errorMessage = string.Empty;
        return isValid;
    }
    
    private static bool NoSnowAnimalJunToAug(AnimalDto animalToAdd, DateOnly date, out string errorMessage)
    {
        errorMessage = "Je mag geen beestje boeken van het type ‘Sneeuw’ in de maanden juni t/m augustus";
        var isValid = animalToAdd.Type != AnimalType.Snow 
               || (date.Month >= 9 && date.Month <= 5);
        
        if (isValid) errorMessage = string.Empty;
        return isValid;
    }
    
    // Customer card based
    private static bool ValidateCustomerCard(AnimalDto animalToAdd, List<AnimalDto> selectedAnimals, CustomerCardType? customerCard, out string errorMessage)
    {
        errorMessage = string.Empty;
        bool isValid;

        switch (customerCard)
        {
            case null:
                isValid = IsAllowedForNoCard(animalToAdd, selectedAnimals, out errorMessage);
                break;
            case CustomerCardType.Silver:
                isValid = IsAllowedForSilverCard(animalToAdd, selectedAnimals, out errorMessage);
                break;
            case CustomerCardType.Gold:
                isValid = IsAllowedForGoldCard(animalToAdd, out errorMessage);
                break;
            case CustomerCardType.Platinum:
                isValid = IsAllowedForPlatinumCard(out errorMessage);
                break;
            default:
                errorMessage = "Onbekende klantkaart";
                isValid = false;
                break;
        }
        return isValid;
    }
    private static bool IsAllowedForNoCard(AnimalDto animalToAdd, List<AnimalDto> selectedAnimals, out string errorMessage)
    {
        errorMessage = "Klanten zonder klantenkaart mogen maximaal 3 dieren boeken, behalve dieren van het type VIP";
        var isValid = animalToAdd.Type != AnimalType.Vip
            && selectedAnimals.Count <= 3;
        
        if (isValid) errorMessage = string.Empty;
        return isValid;
    }

    private static bool IsAllowedForSilverCard(AnimalDto animalToAdd, List<AnimalDto> selectedAnimals, out string errorMessage)
    {
        errorMessage = "Klanten met een zilveren klantenkaart mogen maximaal 4 dieren boeken, behalve dieren van het type VIP";
        var isValid = animalToAdd.Type != AnimalType.Vip
               && selectedAnimals.Count <= 4;
        
        if (isValid) errorMessage = string.Empty;
        return isValid;
    }

    private static bool IsAllowedForGoldCard(AnimalDto animalToAdd, out string errorMessage)
    {
        errorMessage = "Klanten met een gouden kaart mogen zoveel dieren boeken als ze willen, behalve dieren van het type VIP";
        var isValid = animalToAdd.Type != AnimalType.Vip;
        
        if (isValid) errorMessage = string.Empty;
        return isValid;
    }
    
    private static bool IsAllowedForPlatinumCard(out string errorMessage)
    {
        errorMessage = string.Empty;
        return true;
    }
}