namespace Models;
public class NewTicketModel
{
    public int ClientId { get; set; }
    public decimal Amount { get; set; }
    public string DamageDate { get; set; }
    public int DamagerId { get; set; }
    public string Description { get; set; }
}