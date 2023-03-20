namespace Models;

public record TicketStatusChange(int UserId, int Status, string Justification, int TicketId, int AccessLevel);