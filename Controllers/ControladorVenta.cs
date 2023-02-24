using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PF_MamaniL.Handler;
using PF_MamaniL.Models;

namespace PF_MamaniL.Controllers
{
    [Route("api/Venta")]
    [ApiController]
    public class ControladorVenta : ControllerBase
    {
        [HttpGet("{idUsuario}")]
        public List<Venta> TraerVentas([FromRoute] long idUsuario)
        {
            return ManejadorVenta.Obtener(idUsuario);
        }

        [HttpPost("{idUsuario}")]
        public string CargarVenta([FromRoute] long idUsuario, [FromBody] List<Producto> productos)
        {
            ManejadorVenta.Cargar(idUsuario, productos);
            return "Venta cargada";
        }
    }
}
