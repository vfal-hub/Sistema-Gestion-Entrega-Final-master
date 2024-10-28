using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness.Services;

public interface IAutorizacionService
{
    Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion);
    Task<AutorizacionResponse> DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest, int idUsuario);
}

