using System;
using System.Threading.Tasks;
using WebApiDesafioFinal.Contratos;
using WebApiDesafioFinal.Domain;

namespace WebApiDesafioFinal.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoPersist _cursoPersist;
        private readonly IGeralPersist _geralPersist;
        public CursoService(ICursoPersist cursoPersist, IGeralPersist geralPersist)
        {
            _cursoPersist = cursoPersist;
            _geralPersist = geralPersist;
        }

        public async Task<Curso> AddCurso(Curso curso)
        {
            try
            {
                _geralPersist.Add(curso);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _cursoPersist.GetCursoByIdAsync(curso.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCurso(int cursoId)
        {
            try
            {
                var curso = await _cursoPersist.GetCursoByIdAsync(cursoId);
                if (curso == null) throw new Exception("Curso para delete não foi encontrado");

                _geralPersist.Delete(curso);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Curso[]> GetAllCursosByStatusAsync(int status)
        {

            try
            {
                var cursos = await _cursoPersist.GetAllCursosByStatusAsync(status);
                if (cursos == null) return null;

                return cursos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<Curso[]> GetAllCursosAsync()
        {
            try
            {
                var cursos = await _cursoPersist.GetAllCursosAsync();
                if (cursos == null) return null;

                return cursos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Curso> GetAllCursosByIdAsync(int cursoId)
        {
            try
            {
                var curso = await _cursoPersist.GetCursoByIdAsync(cursoId);
                if (curso == null) return null;

                return curso;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Curso> UpdateCurso(int eventoId, Curso model)
        {
            try
            {
                var curso = await _cursoPersist.GetCursoByIdAsync(eventoId);
                if (curso == null) return null;

                model.Id = curso.Id;

                _geralPersist.Update(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _cursoPersist.GetCursoByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
