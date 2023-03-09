using System;
namespace Models;
public class Ticket
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int ClientId { get; set; }
    public DateTime SubmissionDate { get; set; } = DateTime.Today;
    public DateTime DamageDate { get; set; }
    public string Description { get; set; }
    public int DamagerId { get; set; }

    public Ticket(int Id, decimal Amount, int ClientId, DateTime DamageDate, string Description, int DamagerId)
    {
        this.Id = Id;
        this.Amount = Amount;
        this.ClientId = ClientId;
        this.DamageDate = DamageDate;
        this.Description = Description;
        this.DamagerId = DamagerId;
    }
    public Ticket(int Id, decimal Amount, int ClientId, DateTime SubmissionDate, DateTime DamageDate, string Description, int DamagerId)
    {
        this.Id = Id;
        this.Amount = Amount;
        this.ClientId = ClientId;
        this.SubmissionDate = SubmissionDate;
        this.DamageDate = DamageDate;
        this.Description = Description;
        this.DamagerId = DamagerId;
    }

}
