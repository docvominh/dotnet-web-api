namespace Web.Data.Entity;

public class CustomerEntity
{
    public int Id { get; set; }

    public required string FirstName { get; set; }    
    
    public required string LastName { get; set; }

    public string Fullname { get; set; }

    public required string Phone { get; set; }

    public string? Email { get; set; }

    public required string Address { get; set; }
    
    public required string Location { get; set; }

    public required string State { get; set; }

    public required int PostCode { get; set; }
}
