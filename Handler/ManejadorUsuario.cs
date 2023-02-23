using PF_MamaniL.Models;
using System.Data;
using System.Data.SqlClient;

namespace PF_MamaniL.Handler
{
    public static class ManejadorUsuario
    {
        private static string connectionString = "Data Source=DESKTOP-K1HOTLS;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //*********************************************************************************************************************************************//
        public static Usuario IniciarSesión(string nombreUsuario, string contraseña)
        {
            Usuario usuarioLogin = new Usuario();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contraseña", conn);

                comando.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = nombreUsuario });
                comando.Parameters.Add(new SqlParameter("@Contraseña", SqlDbType.VarChar) { Value = contraseña });

                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        usuarioLogin.Id1 = Convert.ToInt64(reader["Id"]);
                        usuarioLogin.Nombre1 = reader["Nombre"].ToString();
                        usuarioLogin.Apellido1 = reader["Apellido"].ToString();
                        usuarioLogin.NombreUsuario1 = reader["NombreUsuario"].ToString();
                        usuarioLogin.Contraseña1 = reader["Contraseña"].ToString();
                        usuarioLogin.Mail1 = reader["Mail"].ToString();
                    }
                }
            }
            return usuarioLogin;
        }
        //*********************************************************************************************************************************************//
        public static string Crear(Usuario usuario)
        {
            int n = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Usuario usuarioT = new Usuario();
                usuarioT = Obtener(usuario.NombreUsuario1);
                if (usuarioT.NombreUsuario1 != usuario.NombreUsuario1)
                {
                    SqlCommand comando = new SqlCommand("INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES (@nombre, " +
                    "@apellido, @nombreUsuario, @contraseña, @mail)", conn);
                    comando.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre1 });
                    comando.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido1 });
                    comando.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario1 });
                    comando.Parameters.Add(new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña1 });
                    comando.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail1 });

                    conn.Open();
                    comando.ExecuteNonQuery();
                    return "Usuario creado con éxito";
                }
                else
                {
                    return "Npw se puede repetir el nombre de usuario";
                }
            }
        }
        //*********************************************************************************************************************************************//
        public static int Modificar(Usuario usuario)
        {
            int n = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand("UPDATE Usuario SET Nombre = @nombre, " +
                    "Apellido = @apellido, NombreUsuario = @nombreUsuario, Contraseña = @contraseña, " +
                    "Mail = @mail WHERE Id = @idUsuario", conn);

                comando.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre1 });
                comando.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido1 });
                comando.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario1 });
                comando.Parameters.Add(new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña1 });
                comando.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail1 });
                comando.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = usuario.Id1 });
                conn.Open();
                n = comando.ExecuteNonQuery();
            }
            return n;
        }
        //*********************************************************************************************************************************************//
        public static Usuario Obtener(string nombreUsuario)
        {
            Usuario usuarioSeleccionado = new Usuario();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario", conn);
                comando.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = nombreUsuario });

                conn.Open();
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        usuarioSeleccionado.Id1 = Convert.ToInt64(reader["ID"]);
                        usuarioSeleccionado.Nombre1 = reader["Nombre"].ToString();
                        usuarioSeleccionado.Apellido1 = reader["Apellido"].ToString();
                        usuarioSeleccionado.NombreUsuario1 = reader["NombreUsuario"].ToString();
                        usuarioSeleccionado.Contraseña1 = reader["Contraseña"].ToString();
                        usuarioSeleccionado.Mail1 = reader["Mail"].ToString();
                    }
                }
            }
            return usuarioSeleccionado;
        }
        //*********************************************************************************************************************************************//
        public static int Eliminar(long idUsuario)
        {
            int n = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand("DELETE FROM Usuario WHERE Id = @IdUsuario", conn);
                comando.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = idUsuario });

                conn.Open();
                n = comando.ExecuteNonQuery();
            }
            return n;
        }
    }
}
