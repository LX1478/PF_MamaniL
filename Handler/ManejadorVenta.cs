using System.Data.SqlClient;
using System.Data;
using PF_MamaniL.Models;

namespace PF_MamaniL.Handler
{
    public class ManejadorVenta
    {
        private static string connectionString = "Data Source=DESKTOP-K1HOTLS;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //*********************************************************************************************************************************************//
        public static List<Venta> Obtener(long idUsuario)
        {
            List<Venta> ventas = new List<Venta>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand("SELECT Venta.id, Venta.Comentarios, Venta.IdUsuario, ProductoVendido.IdProducto, " +
                    "ProductoVendido.Stock FROM Venta INNER JOIN ProductoVendido ON Venta.Id = ProductoVendido.IdVenta " +
                    "WHERE IdUsuario = @IdUsuario", conn);

                comando.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = idUsuario });

                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Venta temporal = new Venta();
                        temporal.Id1 = Convert.ToInt64(reader["Id"]);
                        temporal.Comentarios1 = reader["Comentarios"].ToString();
                        temporal.IdUsuario1 = Convert.ToInt64(reader["IdUsuario"]);

                        ventas.Add(temporal);
                    }
                }
            }
            return ventas;
        }
        //*********************************************************************************************************************************************//
        public static long Insertar(Venta venta)
        {
            long n = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand("INSERT INTO Venta(Comentarios, IdUsuario)" +
                    "VALUES (@Comentario, @IdUsuario) SELECT @@IDENTITY", conn);

                comando.Parameters.Add(new SqlParameter("Comentario", SqlDbType.VarChar) { Value = venta.Comentarios1 });
                comando.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = venta.IdUsuario1 });

                conn.Open();
                n = Convert.ToInt64(comando.ExecuteScalar());
            }
            return n;
        }

        public static void Cargar(long idUsuario, List<Producto> productos)
        {
            Venta venta = new Venta();
            venta.IdUsuario1 = idUsuario;
            venta.Comentarios1 = "Venta exitosa3";

            long idVenta = Insertar(venta);

            foreach (Producto producto in productos)
            {
                ProductoVenta productoVenta = new ProductoVenta();
                productoVenta.Stock1 = producto.Stock1;
                productoVenta.IdProducto1 = producto.Id1;
                productoVenta.IdVenta1 = idVenta;

                ManejadorProductoVendido.Insertar(productoVenta);
                ManejadorProducto.RestarStock(producto.Id1, producto.Stock1);
            }
        }
    }
}
