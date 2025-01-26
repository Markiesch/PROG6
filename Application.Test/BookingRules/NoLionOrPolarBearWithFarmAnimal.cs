using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Test.BookingRules
{
    public class NoLionOrPolarBearWithFarmAnimal
    {
        [Test]
        public void False_When_LionAndFarmAnimalAreBookedTogether()
        {
            // Arrange
            var animalToAdd = new AnimalDto
            {
                Id = 1,
                Name = "leeuw",
                Type = AnimalType.Jungle,
                Price = 100,
                Image = "leeuw.jpg",
                IsAvailable = true
            };
            var selectedAnimals = new List<AnimalDto>
            {
                new()
                {
                    Id = 2,
                    Name = "koe",
                    Type = AnimalType.Farm,
                    Price = 100,
                    Image = "koe.jpg",
                    IsAvailable = true
                }
            };

            // Act
            var result = Web.Rules.BookingRules.NoLionOrPolarBearWithFarmAnimal(animalToAdd, selectedAnimals, out _);

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void True_When_LionAndNotFarmAnimalAreBookedTogether()
        {
            // Arrange
            var animalToAdd = new AnimalDto
            {
                Id = 1,
                Name = "leeuw",
                Type = AnimalType.Jungle,
                Price = 100,
                Image = "leeuw.jpg",
                IsAvailable = true
            };
            var selectedAnimals = new List<AnimalDto>
            {
                new()
                {
                    Id = 2,
                    Name = "olifant",
                    Type = AnimalType.Jungle,
                    Price = 100,
                    Image = "olifant.jpg",
                    IsAvailable = true
                }
            };

            // Act
            var result = Web.Rules.BookingRules.NoLionOrPolarBearWithFarmAnimal(animalToAdd, selectedAnimals, out _);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
