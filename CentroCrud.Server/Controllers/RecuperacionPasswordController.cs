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
  public class RecuperacionPasswordController : ControllerBase
  {
    private readonly DbcentroformacionContext _dbContext;

    public RecuperacionPasswordController(DbcentroformacionContext dbContext)
    {
      _dbContext = dbContext;
    }

    //Listar
    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista()
    {
      var responseApi = new ResponseAPI<List<RecuperacionPasswordDTO>>();
      var listaRecPassDTO = new List<RecuperacionPasswordDTO>();

      try
      {
        foreach (var item in await _dbContext.RecuperacionPasswords.ToListAsync())
        {
          listaRecPassDTO.Add(new RecuperacionPasswordDTO
          {
            IdRecPass = item.IdRecPass,
            UsuarioId = item.UsuarioId,
            Token = item.Token
          });
        }
        responseApi.Success = true;
        responseApi.Data = listaRecPassDTO;
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Buscar
    [HttpGet]
    [Route("Buscar/{id}")]
    public async Task<IActionResult> Buscar(int id)
    {
      var responseApi = new ResponseAPI<RecuperacionPasswordDTO>();
      var recPassDTO = new RecuperacionPasswordDTO();

      try
      {
        var dbRecPass = await _dbContext.RecuperacionPasswords.FirstOrDefaultAsync(x => x.IdRecPass == id);

        if (dbRecPass != null)
        {
          recPassDTO.IdRecPass = dbRecPass.IdRecPass;
          recPassDTO.UsuarioId = dbRecPass.UsuarioId;
          recPassDTO.Token = dbRecPass.Token;

          responseApi.Success = true;
          responseApi.Data = recPassDTO;
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

    //Guardar
    [HttpPost]
    [Route("Guardar")]
    public async Task<IActionResult> Guardar(RecuperacionPasswordDTO recPass)
    {
      var responseApi = new ResponseAPI<int>();

      try
      {
        var dbRecPass = new RecuperacionPassword
        {
          UsuarioId = recPass.UsuarioId,
          Token = recPass.Token,
        };
        _dbContext.RecuperacionPasswords.Add(dbRecPass);
        await _dbContext.SaveChangesAsync();

        if (dbRecPass.IdRecPass != 0)
        {
          responseApi.Success = true;
          responseApi.Data = dbRecPass.IdRecPass;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Dato no creado";
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


