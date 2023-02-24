using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PF_MamaniL.Handler;
using PF_MamaniL.Models;

namespace PF_MamaniL.Controllers
{
    [Route("api/Producto")]
    [ApiController]
    public class ControladorProducto : ControllerBase
    {
        [HttpPost]
        public string CrearProducto([FromBody] Producto producto)
        {
            return (ManejadorProducto.Crear(producto) != 0) ? "Producto creado" : "Producto no creado";
        }

        [HttpPut]
        public string ModificarProducto([FromBody] Producto producto)
        {
            return (ManejadorProducto.Modificar(producto) != 0) ? "Producto modificado" : "Producto no modificado";
        }

        [HttpDelete("{id}")]
        public string EliminarProducto([FromRoute] long id)
        {
            return (ManejadorProducto.Eliminar(id) != 0) ? "Producto eliminado" : "Producto no eliminado";
        }

        [HttpGet("{idUsuario}")]
        public List<Producto> TraerProductos([FromRoute] long idUsuario)
        {
            return ManejadorProducto.Obtener(idUsuario);
        }
    }
}
