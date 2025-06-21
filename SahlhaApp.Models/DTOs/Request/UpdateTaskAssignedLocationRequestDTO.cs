namespace SahlhaApp.Models.DTOs.Request
{
    public class UpdateTaskAssignedLocationRequestDTO
    {
        public int TaskAssignedId { get; set; }
        public string? City { get; set; } = "Cairo";
        public string? Province { get; set; }
        public string? Street { get; set; }
        public string? BuildingNumber { get; set; }
    }
}
