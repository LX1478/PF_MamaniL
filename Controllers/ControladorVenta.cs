using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PF_MamaniL.Handler;
using PF_MamaniL.Models;

namespace PF_MamaniL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorVenta : ControllerBase
    {
        [HttpGet("{id}")]
        public List<Venta> TraerVentas([FromRoute] long id)
        {
            return ManejadorVenta.Obtener(id);
        }

        [HttpPost("{id}")]
        public string CargarVenta([FromRoute] long id, [FromBody] List<Producto> productos)
        {
            ManejadorVenta.Cargar(id, productos);
            return "Venta cargada";
        }
    }
}
