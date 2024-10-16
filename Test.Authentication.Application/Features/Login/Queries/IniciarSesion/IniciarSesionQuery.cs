﻿
using MediatR;
using Test.Authentication.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Authentication.Application.Features.Login.Queries.IniciarSesion
{
    public record IniciarSesionQuery(string Correo, string Password) : IRequest<ResultResponse<IniciarSesionResponse?>>;
}
