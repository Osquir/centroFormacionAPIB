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
  public class AlumnoCursoController : ControllerBase
  {
    private readonly DbcentroformacionContext _dbContext;

    public AlumnoCursoController(DbcentroformacionContext dbContext)
    {
      _dbContext = dbContext;
    }

    //Listar AlumnoCurso
    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista()
    {
      var responseApi = new ResponseAPI<List<AlumnoCursoDTO>>();
      var listaAlumnosCursoDTO = new List<AlumnoCursoDTO>();

      try
      {
        foreach (var item in await _dbContext.AlumnoCursos.ToListAsync())
        {
          listaAlumnosCursoDTO.Add(new AlumnoCursoDTO
          {
            IdAlumnoCurso = item.IdAlumnoCurso,
            AlumnoId = item.AlumnoId,
            CursoId = item.CursoId,
            Curso1 = item.Curso1,
            Curso2 = item.Curso2,
            Curso3 = item.Curso3,
            Curso5 = item.Curso5,
            Curso4 = item.Curso4,   
          });
        }
        responseApi.Success = true;
        responseApi.Data = listaAlumnosCursoDTO;
      }
      catch (Exception ex)
      {
        responseApi.Success = false;
        responseApi.Message = ex.Message;
      }
      return Ok(responseApi);
    }

    //Buscar un AlumnoCurso
    [HttpGet]
    [Route("Buscar/{id}")]
    public async Task<IActionResult> Buscar(int id)
    {
      var responseApi = new ResponseAPI<AlumnoCursoDTO>();
      var AlumnosCursosDTO = new AlumnoCursoDTO();
      try
      {
        var dbAlumnoCurso = await _dbContext.AlumnoCursos.FirstOrDefaultAsync(x => x.IdAlumnoCurso == id);

        if (dbAlumnoCurso != null)
        {
          AlumnosCursosDTO.IdAlumnoCurso = dbAlumnoCurso.IdAlumnoCurso;
          AlumnosCursosDTO.AlumnoId = dbAlumnoCurso.AlumnoId;
          AlumnosCursosDTO.CursoId = dbAlumnoCurso.CursoId;
          AlumnosCursosDTO.Curso1=  dbAlumnoCurso.Curso1;
          AlumnosCursosDTO.Curso2 = dbAlumnoCurso.Curso2;
          AlumnosCursosDTO.Curso3 = dbAlumnoCurso.Curso3;
          AlumnosCursosDTO.Curso4 = dbAlumnoCurso.Curso4;          
          AlumnosCursosDTO.Curso5 = dbAlumnoCurso.Curso5;


          responseApi.Success = true;
          responseApi.Data = AlumnosCursosDTO;
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

    //Guardar un AlumnoCurso
    [HttpPost]
    [Route("Guardar")]
    public async Task<IActionResult> Guardar(AlumnoCursoDTO AlumnoCurso)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbAlumnoCurso = new AlumnoCurso
        {
          IdAlumnoCurso = AlumnoCurso.IdAlumnoCurso,
          AlumnoId = AlumnoCurso.AlumnoId,
          CursoId = AlumnoCurso.CursoId,
          Curso1 = AlumnoCurso.Curso1,
          Curso2 = AlumnoCurso.Curso2,
          Curso3 = AlumnoCurso.Curso3,
          Curso4 = AlumnoCurso.Curso4,
          Curso5 = AlumnoCurso.Curso5,
        };
        _dbContext.AlumnoCursos.Add(dbAlumnoCurso);
        await _dbContext.SaveChangesAsync();

        if (dbAlumnoCurso.IdAlumnoCurso != 0)
        {
          responseApi.Success = true;
          responseApi.Data = dbAlumnoCurso.IdAlumnoCurso;
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

    //Editar un AlumnoCurso
    [HttpPut]
    [Route("Editar/{id}")]
    public async Task<IActionResult> Editar(AlumnoCursoDTO AlumnoCurso, int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbAlumnoCurso = await _dbContext.AlumnoCursos.FirstOrDefaultAsync(ac => ac.IdAlumnoCurso == id);

        if (dbAlumnoCurso != null)
        {
          dbAlumnoCurso.AlumnoId = AlumnoCurso.AlumnoId;
          dbAlumnoCurso.CursoId = AlumnoCurso.CursoId;

          _dbContext.AlumnoCursos.Update(dbAlumnoCurso);
          await _dbContext.SaveChangesAsync();

          responseApi.Success = true;
          responseApi.Data = dbAlumnoCurso.IdAlumnoCurso;
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

    //Eliminar un AlumnoCurso (Registro)
    [HttpDelete]
    [Route("Eliminar/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
      var responseApi = new ResponseAPI<int>();
      try
      {
        var dbAlumnoCurso = await _dbContext.AlumnoCursos.FirstOrDefaultAsync(ac => ac.IdAlumnoCurso == id);

        if (dbAlumnoCurso != null)
        {
          _dbContext.AlumnoCursos.Remove(dbAlumnoCurso);
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
