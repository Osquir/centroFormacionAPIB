using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//Referencias
using CentroCrud.Server.Models;
using CentroCrud.Shared;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;


namespace CentroCrud.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsuarioController : ControllerBase
  {
    private readonly DbcentroformacionContext _dbContext;

    public UsuarioController(DbcentroformacionContext dbContext)
    {
      _dbContext = dbContext;
    }

    //Listar Usuarios
    [Authorize]
    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista()
    {
      var responseApi = new ResponseAPI<List<UsuarioDTO>>();
      var listaUsuariosDTO = new List<UsuarioDTO>();
      try
      {
        foreach (var item in await _dbContext.Usuarios.ToListAsync())
        {
          listaUsuariosDTO.Add(new UsuarioDTO
          {
            IdUsuario = item.IdUsuario,
            Login = item.Login,
            Password = item.Password,
            Nombre = item.Nombre,
            Email = item.Email,
          });
        }
        responseApi.Success = true;
        responseApi.Data = listaUsuariosDTO;
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Buscar un Usuario
    [HttpGet]
    [Route("Buscar/{id}")]
    public async Task<IActionResult> Buscar(int id)
    {
      var responseApi = new ResponseAPI<UsuarioDTO>();
      var UsuariosDTO = new UsuarioDTO();
      try
      {
        var item = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);
        if (item != null)
        {
          UsuariosDTO.IdUsuario = item.IdUsuario;
          UsuariosDTO.Login = item.Login;
          UsuariosDTO.Password = item.Password;
          UsuariosDTO.Nombre = item.Nombre;
          UsuariosDTO.Email = item.Email;

          responseApi.Success = true;
          responseApi.Data = UsuariosDTO;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "No se encontro el Usuario";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Guardar Usuario
    [HttpPost]
    [Route("Guardar")]


    public async Task<IActionResult> Guardar(UsuarioDTO usuario)
    { 
      EncryptMD5 encr = new EncryptMD5();
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbUsuario = new Usuario
        {
          IdUsuario = usuario.IdUsuario,
          Login = usuario.Login,
          Password = encr.Encrypt(usuario.Password),
          Nombre = usuario.Nombre,
          Email = usuario.Email,
        };
        _dbContext.Usuarios.Add(dbUsuario);
        await _dbContext.SaveChangesAsync();

        if (dbUsuario.IdUsuario != 0)
        {
          responseApi.Success = true;
          responseApi.Data = dbUsuario.IdUsuario;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Usuario no creado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Editar un Usuario
    [HttpPut]
    [Route("Editar/{id}")]
    public async Task<IActionResult> Editar(UsuarioDTO usuario, int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

        if (dbUsuario != null)
        {
          dbUsuario.IdUsuario = usuario.IdUsuario;
          dbUsuario.Login = usuario.Login;
          dbUsuario.Password = usuario.Password;
          dbUsuario.Nombre = usuario.Nombre;
          dbUsuario.Email = usuario.Email;

          _dbContext.Usuarios.Update(dbUsuario);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
          responseApi.Data = dbUsuario.IdUsuario;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Usuario no encontrado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Eliminar un Usuario
    [HttpDelete]
    [Route("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbUsuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

        if (dbUsuario != null)
        {
          _dbContext.Usuarios.Remove(dbUsuario);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Usuario no encontrado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }
  }
}
