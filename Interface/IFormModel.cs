namespace ReadEase_C_.Interface
{
    public interface IFormModel
    {
        int Id { get; set; }
        string Password { get; set; }
        string? Role { get; set; }
        string Username { get; set; }
    }
}