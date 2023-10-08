
namespace BookingManagementApp.Handlers;
public class HashingHandler
{
    public static string GetRandomSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt(workFactor: 12); 
    }

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
    }

    public static bool VerifyPassword(string password, string hashPassword)
    =>
         BCrypt.Net.BCrypt.Verify(password, hashPassword);
    
}
