﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Authentication.Application.Features.Login.Queries.IniciarSesion
{
    public class IniciarSesionResponse
    {
        //public string Message { get; set; }
        public string JWToken { get; set; }
        public string Correo { get; set; }
    }
}
