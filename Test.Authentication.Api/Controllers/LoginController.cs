using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Authentication.Application.Features.Login.Commands.Eliminar;
using Test.Authentication.Application.Features.Login.Commands.Registrar;
using Test.Authentication.Application.Features.Login.Queries.IniciarSesion;
using Test.Authentication.Application.Features.Login.Queries.ObtenerUsuario;
using Test.Authentication.Domain.Entities;

namespace Test.Authentication.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IMediator mediator;

        public LoginController(IConfiguration configuration, IMediator mediator)
        {
            this.configuration = configuration;
            this.mediator = mediator;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<ActionResult> Registrar([FromBody] RegistrarCommand command)
        {
            return Ok(await mediator.Send(command));
           
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> Auth([FromBody]  IniciarSesionQuery command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<ActionResult> Eliminar(EliminarUsuarioCommand command)
        {
            return Ok(await mediator.Send(command));

        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerUsuarios()
        {
            var usuarios = await mediator.Send(new ObtenerUsuariosQuery());

            return Ok(usuarios);
        }



    }
}
