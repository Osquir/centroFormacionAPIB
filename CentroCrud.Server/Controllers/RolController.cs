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
  public class RolController : ControllerBase
  {
    private readonly DbcentroformacionContext _dbContext;
    public RolController(DbcentroformacionContext dbContext)
    {
      _dbContext = dbContext;
    }

    //Listar Roles
    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista()
    {
      var responseApi = new ResponseAPI<List<RolDTO>>();
      var listaRolesDTO = new List<RolDTO>();
      try
      {
        foreach (var item in await _dbContext.Rols.ToListAsync())
        {
          listaRolesDTO.Add(new RolDTO
          {
            IdRol = item.IdRol,
            Nombre = item.Nombre,
          });
        }
        responseApi.Success = true;
        responseApi.Data = listaRolesDTO;
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Buscar un Rol
    [HttpGet]
    [Route("Buscar/{id}")]
    public async Task<IActionResult> Buscar(int id)
    {
      var responseApi = new ResponseAPI<RolDTO>();
      var RolesDTO = new RolDTO();
      try
      {
        var item = await _dbContext.Rols.FirstOrDefaultAsync(x => x.IdRol == id);
        if (item != null)
        {
          RolesDTO.IdRol = item.IdRol;
          RolesDTO.Nombre = item.Nombre;
          
          responseApi.Success = true;
          responseApi.Data = RolesDTO;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "No se encontro el rol";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Guardar un Rol
    [HttpPost]
    [Route("Guardar")]
    public async Task<IActionResult> Guardar(RolDTO rol)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbRol = new Rol
        {
          IdRol = rol.IdRol,
          Nombre = rol.Nombre,
        };
        _dbContext.Rols.Add(dbRol);
        await _dbContext.SaveChangesAsync();

        if (dbRol.IdRol != 0)
        {
          responseApi.Success = true;
          responseApi.Data = dbRol.IdRol;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Rol no creado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Editar un Rol
    [HttpPut]
    [Route("Editar/{id}")]
    public async Task<IActionResult> Editar(RolDTO rol, int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbRol = await _dbContext.Rols.FirstOrDefaultAsync(r => r.IdRol == id);

        if (dbRol != null)
        {
          dbRol.IdRol = rol.IdRol;
          dbRol.Nombre = rol.Nombre;

          _dbContext.Rols.Update(dbRol);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
          responseApi.Data = dbRol.IdRol;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Rol no encontrado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Eliminar un Rol
    [HttpDelete]
    [Route("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbRol = await _dbContext.Rols.FirstOrDefaultAsync(r => r.IdRol == id);

        if (dbRol != null)
        {
          _dbContext.Rols.Remove(dbRol);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Rol no encontrado";
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
