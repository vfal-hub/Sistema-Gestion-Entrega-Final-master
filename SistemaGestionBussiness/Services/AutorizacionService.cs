using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SistemaGestionData.Context;
using SistemaGestionEntities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBussiness.Services;

public class AutorizacionService : IAutorizacionService
{
    private readonly CoderhouseContext _context;
    private readonly IConfiguration _configuration;

    public AutorizacionService(CoderhouseContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    private string GenerarToken(string idUsuario)
    {

        var key = _configuration.GetValue<string>("JwtSettings:secretkey");
        var keyBytes = Encoding.ASCII.GetBytes(key);

        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

        var credencialesToken = new SigningCredentials(
            new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha256Signature
            );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = credencialesToken
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

        string tokenCreado = tokenHandler.WriteToken(tokenConfig);

        return tokenCreado;

    }

    private string GenerarRefreshToken()
    {

        var byteArray = new byte[64];
        var refreshToken = "";

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(byteArray);
            refreshToken = Convert.ToBase64String(byteArray);
        }
        return refreshToken;
    }

    private async Task<AutorizacionResponse> GuardarHistorialRefreshToken(
        int idUsuario,
        string token,
        string refreshToken
        )
    {

        var tokenExistente = _context.HistorialRefreshTokens
        .FirstOrDefault(x => x.IdUsuario == idUsuario);

        if (tokenExistente != null)
        {
            _context.HistorialRefreshTokens.Remove(tokenExistente);
        }

        var historialRefreshToken = new HistorialRefreshToken
        {
            IdUsuario = idUsuario,
            UsuarioId = idUsuario,
            Token = token,
            RefreshToken = refreshToken,
            FechaCreacion = DateTime.UtcNow,
            FechaExpiracion = DateTime.UtcNow.AddMinutes(1)
        };


        await _context.HistorialRefreshTokens.AddAsync(historialRefreshToken);
        await _context.SaveChangesAsync();

        return new AutorizacionResponse { Token = token, RefreshToken = refreshToken, Resultado = true, Msg = "Ok" };

    }


    public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
    {
        var usuario_encontrado = _context.Usuarios.FirstOrDefault(x =>
            x.Email == autorizacion.Email &&
            x.Contraseña == autorizacion.Contraseña
        );

        if (usuario_encontrado == null)
        {
            return await Task.FromResult<AutorizacionResponse>(null);
        }


        string tokenCreado = GenerarToken(usuario_encontrado.Id.ToString());

        string refreshTokenCreado = GenerarRefreshToken();
   
        return await GuardarHistorialRefreshToken(usuario_encontrado.Id, tokenCreado, refreshTokenCreado);

    }

    public async Task<AutorizacionResponse> DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest, int idUsuario)
    {
        var refreshTokenEncontrado = _context.HistorialRefreshTokens.FirstOrDefault(x =>
        x.Token == refreshTokenRequest.TokenExpirado &&
        x.RefreshToken == refreshTokenRequest.RefreshToken &&
        x.IdUsuario == idUsuario);

        if (refreshTokenEncontrado == null)
            return new AutorizacionResponse { Resultado = false, Msg = "No existe refreshToken" };

        var refreshTokenCreado = GenerarRefreshToken();
        var tokenCreado = GenerarToken(idUsuario.ToString());

        return await GuardarHistorialRefreshToken(idUsuario, tokenCreado, refreshTokenCreado);

    }
}
