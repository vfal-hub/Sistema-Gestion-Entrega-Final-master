﻿using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionUI.Custom;

public class HistorialRefreshToken
{
    public int IdHistorialToken { get; set; }

    public int? IdUsuario { get; set; }

    public string Token { get; set; }

    public string RefreshToken { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public bool? EsActivo { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; }
}
