using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Desafio.Infrastructure.Security
{
    public static partial class Cryptography
    {
        const string key = "mPorwQB8kMDNQeeYO35KOrMMFn6rFVmbIohBphJPnp4=";

        /// <summary>
        ///     The Password Based Key Derivation Function 2 iteration counter.
        /// </summary>
        private const int Pbkdf2IterCount = 1000;

        /// <summary>
        ///     The Password Based Key Derivation Function 2 subkey length.
        /// </summary>
        private const int Pbkdf2SubkeyLength = 256 / 8;

        /// <summary>
        ///     The salt size. 256 bits
        /// </summary>
        private const int SaltSize = 128 / 8;

        /// <summary>
        /// Simple string Hash
        /// </summary>
        /// <param name="password">string to hash</param>
        /// <returns>Hashed string</returns>
        public static string GetPasswordHash(this string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password", "password is null or empty.");
            }

            // Produce a version 0 (see comment above) password hash.
            byte[] salt;
            byte[] subkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, Pbkdf2IterCount))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(Pbkdf2SubkeyLength);
            }

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

        /// <summary>
        /// The verify hashed password.
        ///     <para>
        /// hashedPassword must be of the format of HashWithPassword (salt + Hash(salt+input))
        ///         Version 0:
        ///         PBKDF2 with HMAC-SHA1, 128-bit salt, 256-bit subkey, 1000 iterations.
        ///         (See also: SDL crypto guidelines v5.1, Part III)
        ///         Format: { 0x00, salt, subkey }
        ///     </para>
        /// </summary>
        /// <param name="hashedPassword">
        /// The hashed password.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The verify hashed password.
        /// </returns>
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentNullException("hashedPassword", "hashedPassword is null or empty.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password", "password is null or empty.");
            }

            byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            // Verify a version 0 (see comment above) password hash.
            if (hashedPasswordBytes.Length != (1 + SaltSize + Pbkdf2SubkeyLength) || hashedPasswordBytes[0] != 0x00)
            {
                // Wrong length or version header.
                return false;
            }

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);
            var storedSubkey = new byte[Pbkdf2SubkeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, Pbkdf2SubkeyLength);

            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Pbkdf2IterCount))
            {
                generatedSubkey = deriveBytes.GetBytes(Pbkdf2SubkeyLength);
            }

            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }

        /// <summary>
        /// Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// The byte arrays equal.
        /// </returns>
        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            bool areSame = true;
            for (int i = 0; i <= a.Length - 1; i++)
            {
                areSame = areSame & (a[i] == b[i]);
            }

            return areSame;
        }

        public static string CreateJwt(int id, string email)
        {
            var keyBytes = Convert.FromBase64String(key);
            var ssk = new InMemorySymmetricSecurityKey(keyBytes);

            var signCredentials = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));

            var token = new JwtSecurityToken(issuer: "desafio.net", claims: claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: signCredentials);

            var handler = new JwtSecurityTokenHandler();

            var result = $"{handler.WriteToken(token)}";

            return result;
        }

        public static IList<Claim> ValidateJwt(string encodedString)
        {
            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var keyBytes = Convert.FromBase64String(key);
                var ssk = new InMemorySymmetricSecurityKey(keyBytes);

                var tvp = new TokenValidationParameters()
                {
                    ValidateActor = false,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    IssuerSigningKey = ssk
                };


                SecurityToken token;
                handler.ValidateToken(encodedString, tvp, out token);

                var jwtToken = (JwtSecurityToken)handler.ReadToken(encodedString);

                return jwtToken.Claims.ToList();
            }
            catch (SecurityTokenValidationException ex)
            {
                throw new UnauthorizedAccessException("Sessão inválida.", ex);
            }
        }
    }
}
