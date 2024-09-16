using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace A_AAsesoresAPI.JWT
{
    public class Security
    {
        public string GenerateToken(string identification, int expiryMinutes = 15)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: new[]
                {
                new Claim(ClaimTypes.Name, Encrypt(identification))
                },
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string Encrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["UserKey"]);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(texto);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public string Decrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(texto);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["UserKey"]);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        /*public static bool ValidateToken(string token, out string username)
        {
            username = null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["SecretKey"]);

            try
            {
                // Configuración de parámetros de validación
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                // Validación del token
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
                username = claim?.Value;

                // Validaciones adicionales (por ejemplo, verificar si el usuario existe en tu sistema)
                return !string.IsNullOrEmpty(username);
            }
            catch (Exception ex)
            {
                // Captura de excepciones para manejo de errores
                Console.WriteLine("Error al validar el token: " + ex.Message);
                return false;
            }
        }*/
    }
}