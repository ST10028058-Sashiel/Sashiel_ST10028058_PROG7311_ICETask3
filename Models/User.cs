
public enum UserRole { Admin, User }

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UserRole Role { get; set; }
}
