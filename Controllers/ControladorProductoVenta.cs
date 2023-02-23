using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PF_MamaniL.Handler;
using PF_MamaniL.Models;

namespace PF_MamaniL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorProductoVenta : ControllerBase
    {
        [HttpGet("{id}")]
        public List<Producto> TraerProductosVendidos([FromRoute] long id)
        {
            return ManejadorProductoVendido.ObtenerProductos(id);
        }

    }
}
