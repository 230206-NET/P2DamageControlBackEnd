namespace Models;
public class NewTicketModel
{
    public int ClientId { get; set; }
    public decimal Amount { get; set; }
    public string DamageDate { get; set; }
    public int DamagerId { get; set; }
    public string Description { get; set; }
    public NewTicketModel()
    {

    }
    public NewTicketModel(int ClientId, decimal Amount, string DamageDate, int DamagerId, string Description)
    {
        this.ClientId = ClientId;
        this.Amount = Amount;
        this.DamageDate = DamageDate;
        this.DamagerId = DamagerId;
        this.Description = Description;
    }
}