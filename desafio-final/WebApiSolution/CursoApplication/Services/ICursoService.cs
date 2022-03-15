using System.Threading.Tasks;
using WebApiDesafioFinal.Domain;


namespace WebApiDesafioFinal.Services
{
    public interface ICursoService
    {
        Task<Curso> AddCurso(Curso model);
        Task<Curso> UpdateCurso(int eventoId, Curso model );
        Task<bool> DeleteCurso(int eventoId);
        Task<Curso[]> GetAllCursosAsync();
        Task<Curso[]> GetAllCursosByStatusAsync(int tema);
        Task<Curso> GetAllCursosByIdAsync(int cursoId);
    }
}
