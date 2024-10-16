using AutoMapper;
using FluentValidation;
using MediatR;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Authentication.Application.Features.Login.Commands.Registrar;
using Test.Authentication.Application.Persistence;
using Test.Authentication.Application.Wrappers;

namespace Test.Authentication.Application.Features.Login.Commands.Eliminar
{
    public class EliminarHandler : IRequestHandler<EliminarUsuarioCommand, ResultResponse<bool>>
    {
        private readonly IMapper mapper;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IValidator<EliminarUsuarioCommand> validator;
        public EliminarHandler(IMapper mapper,
            IUsuarioRepository usuarioRepository, IValidator<EliminarUsuarioCommand> validator)
        {
            this.mapper = mapper;
            this.usuarioRepository = usuarioRepository;
            this.validator = validator;
        }

        public async Task<ResultResponse<bool>> Handle(EliminarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var result = new ResultResponse<bool>();

            try
            {
                // Verifica si el usuario existe usando el repositorio
                var usuario = await usuarioRepository.ObtenerUsuario(request.Correo);

                if (usuario == null)
                {
                    result.Data = false;
                    result.Message = "El usuario no existe";
                    return result; // Retorna con el mensaje correspondiente
                }

                // Cambia el estado del usuario a eliminado a través del repositorio
                bool eliminacionExitosa = await usuarioRepository.EliminarUsuario(request.Correo);

                if (eliminacionExitosa)
                {
                    result.Data = true;
                    result.Message = "Eliminación lógica exitosa";
                }
                else
                {
                    result.Data = false;
                    result.Message = "Error al eliminar el usuario";
                }
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result; // Retorna el resultado final
        }
    }
}
