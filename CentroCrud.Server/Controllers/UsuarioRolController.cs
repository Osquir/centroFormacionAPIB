using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Referencias
using CentroCrud.Server.Models;
using CentroCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace CentroCrud.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsuarioRolController : ControllerBase
  {
    private readonly DbcentroformacionContext _dbContext;

    public UsuarioRolController(DbcentroformacionContext dbContext)
    {
      _dbContext = dbContext;
    }

    //Listar UsuarioRol
    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista()
    {
      var responseApi = new ResponseAPI<List<UsuarioRolDTO>>();
      var listaUsuarioRolDTO = new List<UsuarioRolDTO>();

      try
      {
        foreach (var item in await _dbContext.UsuarioRols.ToListAsync())
        {
          listaUsuarioRolDTO.Add(new UsuarioRolDTO
          {
            IdUsuarioRol = item.IdUsuarioRol,
            UsuarioId = item.UsuarioId,
            RolId = item.RolId
          });
        }
        responseApi.Success = true;
        responseApi.Data = listaUsuarioRolDTO;
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Buscar UsuarioRol
    [HttpGet]
    [Route("Buscar/{id}")]
    public async Task<IActionResult> Buscar(int id)
    {
      var responseApi = new ResponseAPI<UsuarioRolDTO>();
      var UsuarioRolsDTO = new UsuarioRolDTO();

      try
      {
        var dbUsuarioRol = await _dbContext.UsuarioRols.FirstOrDefaultAsync(x => x.IdUsuarioRol == id);

        if (dbUsuarioRol != null)
        {
          UsuarioRolsDTO.IdUsuarioRol = dbUsuarioRol.IdUsuarioRol;
          UsuarioRolsDTO.UsuarioId = dbUsuarioRol.UsuarioId;
          UsuarioRolsDTO.RolId = dbUsuarioRol.RolId;

          responseApi.Success = true;
          responseApi.Data = UsuarioRolsDTO;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Item no encontrado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Guardar UsuarioRol
    [HttpPost]
    [Route("Guardar")]
    public async Task<IActionResult> Guardar(UsuarioRolDTO UsuarioRol)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbUsuarioRol = new UsuarioRol
        {
          IdUsuarioRol = UsuarioRol.IdUsuarioRol,
          UsuarioId = UsuarioRol.UsuarioId,
          RolId = UsuarioRol.RolId
        };
        _dbContext.UsuarioRols.Add(dbUsuarioRol);
        await _dbContext.SaveChangesAsync();

        if (dbUsuarioRol.IdUsuarioRol != 0)
        {
          responseApi.Success = true;
          responseApi.Data = dbUsuarioRol.IdUsuarioRol;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "No se pudo guardar";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Editar UsuarioRol
    [HttpPut]
    [Route("Editar/{id}")]
    public async Task<IActionResult> Editar(UsuarioRolDTO UsuarioRol, int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbUsuarioRol = await _dbContext.UsuarioRols.FirstOrDefaultAsync(ur => ur.IdUsuarioRol == id);

        if (dbUsuarioRol != null)
        {
          dbUsuarioRol.UsuarioId = UsuarioRol.UsuarioId;
          dbUsuarioRol.RolId = UsuarioRol.RolId;

          _dbContext.UsuarioRols.Update(dbUsuarioRol);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
          responseApi.Data = dbUsuarioRol.IdUsuarioRol;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Item no encontrado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Eliminar UsuarioRol
    [HttpDelete]
    [Route("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbUsuarioRol = await _dbContext.UsuarioRols.FirstOrDefaultAsync(ur => ur.IdUsuarioRol == id);

        if (dbUsuarioRol != null)
        {
          _dbContext.UsuarioRols.Remove(dbUsuarioRol);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Item no encontrado";
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
