﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AuthDto
{
    public class AuthResponseDto
    {
        public string? Token { get; set; }

        public string? RefreshToken { get; set; }

        public Guid UserId { get; set; }
    }
}
