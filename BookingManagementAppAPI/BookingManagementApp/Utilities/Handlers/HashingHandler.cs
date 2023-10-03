
namespace BookingManagementApp.Handlers;
public class HashingHandler
{
    public static string GetRandomSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt(workFactor: 12); // Default 11
    }

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(inputKey: password, salt: GetRandomSalt());
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(text: password, hash: hashedPassword);
    }
}
