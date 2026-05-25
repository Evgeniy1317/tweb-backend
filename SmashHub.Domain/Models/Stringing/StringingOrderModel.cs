namespace SmashHub.Domain.Models.Stringing
{
    public class StringingOrderModel
    {
        public int Id { get; set; }
        public string RacketModel { get; set; } = string.Empty;
        public string Tension { get; set; } = string.Empty;
        public string StringType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int ClientUserId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public decimal TotalLei { get; set; }
    }
}
