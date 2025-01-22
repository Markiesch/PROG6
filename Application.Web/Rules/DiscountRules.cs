using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Web.Rules;

public static class DiscountRules
{
    private const int MaxDiscount = 60;
    private const int ThreeSameTypePercentage = 10;
    private const int DuckChangePercentage = 50;
    private const int MondayOrTuesdayPercentage = 15;
    private const int AlphabeticalPercentage = 2;
    private const int CustomerCardPercentage = 10;
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
    
    // Main calculation methods
    public static decimal CalculateSubTotalPrice(List<AnimalDto> animals)
    {
        return animals.Sum(a => a.Price);
    }
    
    public static decimal CalculateTotalPrice(decimal subtotalPrice, Dictionary<string, int>? discounts)
    {
        return subtotalPrice - discounts?.Sum(d => d.Value) ?? subtotalPrice;
    }
    
    public static Dictionary<string, int>? GetDiscounts(List<AnimalDto> animals, DateOnly date, CustomerCardType? customerCard)
    {
        var discounts = new Dictionary<string, int>
        {
            { "3 Zelfde types", ThreeSameTypeDiscount(animals) },
            { "Eend 1-op-6 kans", DuckChangeDiscount(animals) },
            { "Maandag of dinsdag", MondayOrTuesdayDiscount(date) },
            { "Alfabetisch", AlphabeticalDiscount(animals) },
            { "Klantenkaart", CustomerCardDiscount(customerCard) }
        }.Where(d => d.Value > 0)
            .ToDictionary(d => d.Key, d => d.Value);

        if (discounts.Sum(d => d.Value) > MaxDiscount) 
            return new Dictionary<string, int> { { "Maximale korting", MaxDiscount } };
        
        return discounts.Count != 0 ? discounts : null;
    }
    
    // Three same type discount
    private static int ThreeSameTypeDiscount(List<AnimalDto> animals)
    {
        var animalGroups = animals.GroupBy(a => a.Type);
        return animalGroups.Any(g => g.Count() >= 3) ? ThreeSameTypePercentage : 0;
    }
    
    // Duck change discount
    private static int DuckChangeDiscount(List<AnimalDto> animals)
    {
        if (!animals.Any(a => a.Name.Equals("eend", StringComparison.CurrentCultureIgnoreCase)))
            return 0;
        
        var random = new Random(); 
        return random.Next(1, 7) == 1 ? DuckChangePercentage : 0;
    }
    
    // Monday or Tuesday discount
    private static int MondayOrTuesdayDiscount(DateOnly date)
    {
        return date.DayOfWeek is DayOfWeek.Monday or DayOfWeek.Tuesday ? MondayOrTuesdayPercentage : 0;
    }
    
    // Alphabetical discount
    private static int AlphabeticalDiscount(List<AnimalDto> animals)
    {
        return animals.Select(animal =>
            { 
                return Alphabet
                    .TakeWhile(letter => animal.Name.ToLower().Contains(letter))
                    .Sum(_ => AlphabeticalPercentage);
            })
            .Sum();
    }
    
    // Customer card discount
    private static int CustomerCardDiscount(CustomerCardType? customerCard)
    {
        return customerCard.HasValue ? CustomerCardPercentage : 0;
    }
}