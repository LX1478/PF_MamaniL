using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PF_MamaniL.Handler;
using PF_MamaniL.Models;

namespace PF_MamaniL.Controllers
{
    [Route("api/ProductoVendido")]
    [ApiController]
    public class ControladorProductoVenta : ControllerBase
    {
        [HttpGet("{idUsuario}")]
        public List<Producto> TraerProductosVendidos([FromRoute] long idUsuario)
        {
            return ManejadorProductoVendido.ObtenerProductos(idUsuario);
        }

    }
}
