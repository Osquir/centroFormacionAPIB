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
  public class CursoController : ControllerBase
  {
    private readonly DbcentroformacionContext _dbContext;

    public CursoController(DbcentroformacionContext dbContext)
    {
      _dbContext = dbContext;
    }

    //Listar Cursos
    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista()
    {
      var responseApi = new ResponseAPI<List<CursoDTO>>();
      var listaCursosDTO = new List<CursoDTO>();
      try
      {
        foreach (var item in await _dbContext.Cursos.ToListAsync())
        {
          listaCursosDTO.Add(new CursoDTO
          {
            IdCurso = item.IdCurso,
            Codigo = item.Codigo,
            Nombre = item.Nombre,
            Creditos = item.Creditos,
            Descripcion = item.Descripcion,
            Temario = item.Temario,
          });
        }
        responseApi.Success = true;
        responseApi.Data = listaCursosDTO;
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Buscar un Curso
    [HttpGet]
    [Route("Buscar/{id}")]
    public async Task<IActionResult> Buscar(int id)
    {
      var responseApi = new ResponseAPI<CursoDTO>();
      var CursosDTO = new CursoDTO();
      try
      {
        var dbCurso = await _dbContext.Cursos.FirstOrDefaultAsync(x => x.IdCurso == id);

        if (dbCurso != null)
        {
          CursosDTO.IdCurso = dbCurso.IdCurso;
          CursosDTO.Codigo = dbCurso.Codigo;
          CursosDTO.Nombre = dbCurso.Nombre;
          CursosDTO.Creditos = dbCurso.Creditos;
          CursosDTO.Descripcion = dbCurso.Descripcion;
          CursosDTO.Temario = dbCurso.Temario;
          
          responseApi.Success = true;
          responseApi.Data = CursosDTO;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Alumno no encontrado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Guardar curso
    [HttpPost]
    [Route("Guardar")]
    public async Task<IActionResult> Guardar(CursoDTO curso)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbCurso = new Curso
        {
          Codigo = curso.Codigo,
          Nombre = curso.Nombre,
          Creditos = curso.Creditos,
          Descripcion = curso.Descripcion,
          Temario = curso.Temario,
        };
        _dbContext.Cursos.Add(dbCurso);
        await _dbContext.SaveChangesAsync();

        if (dbCurso.IdCurso != 0)
        {
          responseApi.Success = true;
          responseApi.Data = dbCurso.IdCurso;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Curso no creado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Editar un curso
    [HttpPut]
    [Route("Editar/{id}")]
    public async Task<IActionResult> Editar(CursoDTO curso, int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbCurso = await _dbContext.Cursos.FirstOrDefaultAsync(c => c.IdCurso == id);

        if (dbCurso != null)
        {
          dbCurso.Codigo = curso.Codigo;
          dbCurso.Nombre = curso.Nombre;
          dbCurso.Creditos = curso.Creditos;
          dbCurso.Descripcion = curso.Descripcion;
          dbCurso.Temario = curso.Temario;

          _dbContext.Cursos.Update(dbCurso);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
          responseApi.Data = dbCurso.IdCurso;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Curso no encontrado";
        }
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Eliminar un Curso
    [HttpDelete]
    [Route("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbCurso = await _dbContext.Cursos.FirstOrDefaultAsync(c => c.IdCurso == id);

        if (dbCurso != null)
        {
          _dbContext.Cursos.Remove(dbCurso);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
        }
        else
        {
          responseApi.Success = false;
          responseApi.Message = "Curso no encontrado";
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
