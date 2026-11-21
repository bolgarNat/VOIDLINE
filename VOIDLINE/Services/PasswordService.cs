using System.Security.Cryptography;
using System.Text;

namespace VOIDLINE.Services;

public class PasswordService
{
    private readonly byte[] _key;

    public PasswordService(byte[] key)
    {
        _key = key;
    }

    public byte[] PasswordConvert(string password)
    {
        byte[] passwd = Encoding.UTF8.GetBytes(password);

        return HMACSHA256.HashData(_key, passwd);
    }

    public bool VerifyPassword(string password, byte[] passwordHash)
    {
        byte[] hash = PasswordConvert(password);

        return CryptographicOperations.FixedTimeEquals(
            hash,
            passwordHash
        );
    }
}
