﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Authentication.Application.Features.Login.Commands.Eliminar
{
    public class EliminarCommandValidator : AbstractValidator<EliminarUsuarioCommand>
    {
        public EliminarCommandValidator()
        {
            
        }
    }
}
