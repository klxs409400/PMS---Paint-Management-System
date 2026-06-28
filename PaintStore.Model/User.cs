namespace PaintStore.Model;

public class User
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public User(int id, string name, string email, string phone)
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
        CreatedDate = DateTime.Now;
    }

    public User()
    {
        
    }
}
