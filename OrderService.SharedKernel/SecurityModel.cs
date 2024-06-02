using System.Security.Cryptography;
using System.Text;

namespace OrderService.SharedKernel;

public class SecurityModel
{
    public static byte[] Hash(string s)
    {
        HashAlgorithm Hasher = new SHA512CryptoServiceProvider();
        var strBytes = Encoding.UTF8.GetBytes(s);
        var strHash = Hasher.ComputeHash(strBytes);
        return strHash;
    }
}
