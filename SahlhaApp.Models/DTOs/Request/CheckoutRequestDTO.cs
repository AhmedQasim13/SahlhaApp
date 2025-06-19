namespace SahlhaApp.Models.DTOs.Request
{
    public class CheckoutRequestDTO
    {
        public int TaskAssignmentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // "Stripe" or "CashOnDelivery"
        public string Currency { get; set; } = "usd";
        public string? City { get; set; } = "Cairo"; // Default city, can be overridden
        public string? Province { get; set; }
        public string? Street { get; set; }
        public string? BuildingNumber { get; set; }
    }
}
