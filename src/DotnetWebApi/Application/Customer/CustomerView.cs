namespace Application.Customer;

public class CustomerView
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public required string Phone { get; set; }

    public string? Email { get; set; }

    public required string Address { get; set; }

    public required string Location { get; set; }

    public required string State { get; set; }

    public required int PostCode { get; set; }
}
