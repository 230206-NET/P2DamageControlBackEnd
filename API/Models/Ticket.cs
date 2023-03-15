using System;
namespace Models;
public class Ticket
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int ClientId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime SubmissionDate { get; set; } = DateTime.Today;
    public DateTime DamageDate { get; set; }
    public string Description { get; set; }
    public string DamagerId { get; set; }
    public string TicketJustification { get; set; }
    public int TicketStatus { get; set; } = 0;

    public Ticket(int Id, decimal Amount, int ClientId, DateTime DamageDate, string Description, string DamagerId)
    {
        this.Id = Id;
        this.Amount = Amount;
        this.ClientId = ClientId;
        this.DamageDate = DamageDate;
        this.Description = Description;
        this.DamagerId = DamagerId;
    }
    public Ticket(int Id, decimal Amount, int ClientId, DateTime SubmissionDate, DateTime DamageDate, string Description, string DamagerId)
    {
        this.Id = Id;
        this.Amount = Amount;
        this.ClientId = ClientId;
        this.SubmissionDate = SubmissionDate;
        this.DamageDate = DamageDate;
        this.Description = Description;
        this.DamagerId = DamagerId;
    }
    public Ticket(decimal Amount, string Description, string DamagerId)
    {
        this.Amount = Amount;
        this.ClientId = 1;
        this.SubmissionDate = DateTime.Today;
        this.DamageDate = DateTime.Today;
        this.Description = Description;
        this.DamagerId = DamagerId;
    }
    public Ticket()
    {
        SubmissionDate = DateTime.Today;
        DamageDate = DateTime.Today;
        this.ClientId = 1;
    }

}
