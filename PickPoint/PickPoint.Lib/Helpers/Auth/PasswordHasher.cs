using System.Security.Cryptography;
using Serilog;

namespace PickPoint.Lib.Helpers.Auth;

public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 20;
    private const int DefaultIterationsCount = 10000;

    private const string SupportedFlag = "PickPointHASH";

    public static string Hash(string password) => Hash(password, DefaultIterationsCount);

    private static string Hash(string password, int iterations)
    {
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
        var hash   = pbkdf2.GetBytes(HashSize);

        var hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        var base64Hash = Convert.ToBase64String(hashBytes);

        return string.Format($"{SupportedFlag}${iterations}${base64Hash}");
    }

    public static bool Verify(string password, string hashedPassword)
    {
        if (!IsHashSupported(hashedPassword))
        {
            Log.Error("Hash is not supported: ");

            return false;
        }

        var slittedHashString = hashedPassword.Substring(SupportedFlag.Length + 1).Split('$');
        var iterations        = int.Parse(slittedHashString[0]);
        var base64Hash        = slittedHashString[1];

        var hashBytes = Convert.FromBase64String(base64Hash);

        var salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
        var hash   = pbkdf2.GetBytes(HashSize);

        for (var i = 0; i < HashSize; i++)
        {
            if (hashBytes[i + SaltSize] != hash[i])
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsHashSupported(string hashString) => hashString.Contains(SupportedFlag);
}