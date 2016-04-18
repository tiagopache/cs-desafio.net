using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Desafio.Infrastructure.Security
{
    public static partial class Cryptography
    {
        public static string GetSHA512Hash(this string input)
        {
            byte[] data = Encoding.ASCII.GetBytes(input);
            data = new SHA512Managed().ComputeHash(data);
            var hash = Encoding.ASCII.GetString(data);

            return hash;
        }
    }
}
