﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionUI.Custom;

public class RefreshTokenRequest
{
    public string TokenExpirado { get; set; }
    public string RefreshToken { get; set; }
}
