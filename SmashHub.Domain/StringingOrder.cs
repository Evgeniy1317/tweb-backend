using SmashHub.Domain;
using System.ComponentModel.DataAnnotations;

public class StringingOrder
{
    public int Id { get; set; }

    [Required]
    public string RacketModel { get; set; } = string.Empty;

    [Required]
    public string Tension { get; set; } = string.Empty;

    [Required]
    public string StringType { get; set; } = string.Empty;

    public string Status { get; set; } = "handover";
    public string CreatedAt { get; set; } = string.Empty;

    public int ClientUserId { get; set; }

    // Добавить это:
    public User? Client { get; set; }

    public string ClientName { get; set; } = string.Empty;
    public decimal TotalLei { get; set; }
}