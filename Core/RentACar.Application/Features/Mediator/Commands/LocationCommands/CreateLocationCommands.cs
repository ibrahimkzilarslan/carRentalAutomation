﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Features.Mediator.Commands.LocationCommands
{
    public class CreateLocationCommands : IRequest
    {
        public string Name { get; set; }
    }
}
