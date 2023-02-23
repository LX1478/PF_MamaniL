using PF_MamaniL.Models;
using System.Data.SqlClient;
using System.Data;

namespace PF_MamaniL.Handler
{
    public class ManejadorProductoVendido
    {
        private static string connectionString = "Data Source=DESKTOP-K1HOTLS;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //*********************************************************************************************************************************************//
        public static List<Producto> ObtenerProductos(long idUsuario)
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand("SELECT Producto.Id, Producto.Descripciones, Producto.Costo, Producto.PrecioVenta, " +
                    "ProductoVendido.Stock, Producto.IdUsuario FROM dbo.Producto INNER JOIN ProductoVendido ON Producto.Id = " +
                    "ProductoVendido.IdProducto WHERE Producto.IdUsuario = @IdUsuario", conn);
                comando.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = idUsuario });

                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto temporal = new Producto();
                        temporal.Id1 = Convert.ToInt64(reader["Id"]);
                        temporal.Descripciones1 = reader["Descripciones"].ToString();
                        temporal.Costo1 = Convert.ToDecimal(reader["Costo"]);
                        temporal.PrecioVenta1 = Convert.ToDecimal(reader["PrecioVenta"]);
                        temporal.Stock1 = Convert.ToInt32(reader["Stock"]);
                        temporal.IdUsuario1 = Convert.ToInt64(reader["IdUsuario"]);

                        productos.Add(temporal);
                    }
                }
            }
            return productos;
        }
        //*********************************************************************************************************************************************//
        public static void Insertar(ProductoVenta productoVendido)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand("INSERT INTO ProductoVendido(Stock, IdProducto, IdVenta)" +
                    " VALUES(@Stock, @IdProducto, @IdVenta)", conn);
                comando.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = productoVendido.Stock1 });
                comando.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt) { Value = productoVendido.IdProducto1 });
                comando.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = productoVendido.IdVenta1 });

                conn.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}
