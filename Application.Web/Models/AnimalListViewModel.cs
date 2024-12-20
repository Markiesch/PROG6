using Application.Data.Dto;

namespace Application.Web.Models;

public class AnimalListViewModel
{
    public required IEnumerable<AnimalDto> Animals { get; set; }
}