namespace MSP.Courses.API.Features.Category.DTOs;

public record GetCategoryResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}