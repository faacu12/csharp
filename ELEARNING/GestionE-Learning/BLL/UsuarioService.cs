using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace BLL
{
    public class UsuarioService
    {
       DAL.MP_usuario mapperu = new DAL.MP_usuario();
        private const int Iterations = 10000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        // Para REGISTRAR/CREAR: esto es lo que guardas en DB (o mandas al SP)
        public string HashPassword(string password)
        {
            // 1. Crea el Salt aleatorio automáticamente
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // 2. Crea el Hash usando PBKDF2
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // 3. Combinamos Salt y Hash en un solo arreglo para guardarlo fácil en la BD
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
            string a = Convert.ToBase64String(hashBytes);
            // 4. Lo convertimos a Base64 para guardarlo como un simple texto (string)
            return a;
        }

        public bool VerifyPassword(string password, string savedHash)
        {
            // 1. Convertimos el string de la BD de nuevo a bytes
            byte[] hashBytes = Convert.FromBase64String(savedHash);

            // 2. Extraemos el Salt (los primeros 16 bytes)
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // 3. Hasheamos la contraseña que el usuario puso en el Login usando ESE Salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // 4. Comparamos los resultados
            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i]) return false;
            }
            return true;
        }
        public int Insertar(BE.Usuario u)
        {
            return mapperu.Agregar(u);
        }
        public string TraerHash(string user)
        {
            Acceso acceso = new Acceso();
            acceso.Abrir();
            DataTable table = new DataTable();
            List<SqlParameter> ps = new List<SqlParameter>();
            ps.Add(acceso.CrearParameter("@u", user));
            table = acceso.Leer("TRAERHASH",ps);
            string hashp = "";
            foreach (DataRow r in table.Rows) {
                hashp = r[0].ToString();
            }
            string saved = hashp;
            acceso.Cerrar();
            return saved;
        }
    }
}