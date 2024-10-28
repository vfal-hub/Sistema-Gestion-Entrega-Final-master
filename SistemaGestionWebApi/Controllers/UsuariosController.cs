using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaGestionBussiness.Services;
using SistemaGestionEntities;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;


namespace SistemaGestionWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly ILogger<UsuariosController> _logger;
    private readonly UsuariosService _usuariosService;
    private readonly string _secretKey;
 
    public UsuariosController(ILogger<UsuariosController> logger, UsuariosService usuariosService, IConfiguration config)
    {
        _logger = logger;
        _usuariosService = usuariosService;
        _secretKey = config.GetSection("JwtSettings").GetSection("secretKey").ToString();
    }


    [HttpGet(Name = "Get Usuarios")]
    public ActionResult<List<Usuario>> GetUsuarios([FromQuery(Name = "filtro")] string? filtro)
    {
        if (filtro == null)
        {
            return _usuariosService.ListUsers();
        }
        return _usuariosService.GetUserBy(filtro);
    }

    [HttpGet("{id}")]
    public ActionResult<Usuario> GetOneUser(int id)
    {     
        var usuario = _usuariosService.GetOneUser(id);
        if (usuario is null)
        {
            return NotFound();
        }
        return usuario;
    }

    [HttpPost]
    public ActionResult<Usuario> CreateUser([FromBody] Usuario usuario)
    {
        var usuarioCreado = _usuariosService.CreateUser(usuario);
        return CreatedAtAction(nameof(GetOneUser), new { id = usuarioCreado.Id }, usuario);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUser([FromRoute(Name = "id")] int id, [FromBody] Usuario usuario)
    {
        _usuariosService.UpdateUser(id, usuario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUser([FromRoute(Name = "id")] int id)
    {
        _usuariosService.DeleteUser(id);
        return NoContent();
    }


    [HttpPost("Login")]
    public async Task<ActionResult<SesionDTO>> Login([FromBody] LoginDTO login)
    {
        var usuario = _usuariosService.ValidateUser(login.Email, login.Contraseña);
        if (usuario is null)
        {
            return Unauthorized(new { message = "Credenciales incorrectas." });
        }

        var keyBytes = Encoding.ASCII.GetBytes(_secretKey);
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.Email));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

        string tokenString = tokenHandler.WriteToken(tokenConfig);
       
        var sesionDTO = new SesionDTO
        {
            Nombre = usuario.Nombre,
            Correo = usuario.Email,
            Token = tokenString
        };

        return Ok(sesionDTO);
    }


    [HttpGet]
    [Route("Register")]
    public ActionResult<Usuario> Register(int id)
    {
        var usuario = _usuariosService.GetOneUser(id);
        if (usuario is null)
        {
            return NotFound();
        }
        return usuario;
    }

    [HttpPost]
    [Route("GetUserName")]
    public ActionResult<string> GetUserName([FromBody] LoginDTO login)
    {
        var usuario = _usuariosService.ValidateUser(login.Email, login.Contraseña);

        if (usuario is null)
        {
            return NotFound(new { message = "No se encontró el nombre del usuario." });
        }

        var nombreUsuario = usuario.NombreUsuario;

        return Ok(nombreUsuario);
    }


    [HttpGet("GetUserNameById/{idUsuario}")]
    public ActionResult<string> GetUserNameById(int idUsuario)
    {
        var usuario = _usuariosService.GetOneUser(idUsuario);

        if (usuario == null)
        {
            return NotFound("Usuario no encontrado.");
        }

        return Ok(usuario.NombreUsuario);
    }

}
