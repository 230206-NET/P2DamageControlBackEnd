using System;
namespace Models;
public class Ticket
{
    public int Id { get; set; } = 0;
    public decimal Amount { get; set; }
    public int ClientId { get; set; }
    public int EmployeeId { get; set; } = 0;
    public DateTime SubmissionDate { get; set; } = DateTime.Today;
    public DateTime DamageDate { get; set; }
    public string Description { get; set; }
    public string DamagerId { get; set; }
    public string TicketJustification { get; set; } = " ";
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
    public Ticket(int Id, decimal Amount, int ClientId, int EmployeeId, DateTime SubmissionDate, DateTime DamageDate, string Description, string DamagerId, string TicketJustification, int TicketStatus)
    {
        this.Id = Id;
        this.Amount = Amount;
        this.ClientId = ClientId;
        this.EmployeeId = EmployeeId;
        this.SubmissionDate = SubmissionDate;
        this.DamageDate = DamageDate;
        this.Description = Description;
        this.DamagerId = DamagerId;
        this.TicketJustification = TicketJustification;
        this.TicketStatus = TicketStatus;
    }
    public Ticket(decimal Amount, int ClientId, string Description, string DamagerId, string DamageDate)
    {
        this.Amount = Amount;
        this.ClientId = ClientId;
        this.SubmissionDate = DateTime.Today;
        this.DamageDate = DateTime.Parse(DamageDate);
        this.Description = Description;
        this.DamagerId = DamagerId;
    }
    public Ticket()
    {
        SubmissionDate = DateTime.Today;
    }

}
