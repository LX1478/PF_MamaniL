using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PF_MamaniL.Handler;
using PF_MamaniL.Models;

namespace PF_MamaniL.Controllers
{
    [Route("api/Usuario")]
    [ApiController]
    public class ControladorUsuario : ControllerBase
    {
        [HttpGet("{usuario}/{contraseña}")]
        public Usuario IniciarSesion([FromRoute] string usuario, [FromRoute] string contraseña)
        {
            return ManejadorUsuario.IniciarSesión(usuario, contraseña);
        }

        [HttpGet("{usuario}")]
        public Usuario ObtenerUsuario([FromRoute] string usuario)
        {
            return ManejadorUsuario.Obtener(usuario);
        }

        [HttpPost]
        public string CrearUsuario([FromBody] Usuario usuario)
        {
            return ManejadorUsuario.Crear(usuario);
        }

        [HttpPut]
        public string ModificarUsuario([FromBody] Usuario usuario)
        {
            return (ManejadorUsuario.Modificar(usuario) != 0) ? "Usuario modificado" : "Usuario no modificado";
        }

        [HttpDelete("{id}")]
        public string EliminarUsuario([FromRoute] long id)
        {
            return (ManejadorUsuario.Eliminar(id) != 0) ? "Usuario eliminado" : "Usuario no eliminado";
        }
    }
}
