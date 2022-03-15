using System.Threading.Tasks;
using WebApiDesafioFinal.Domain;

namespace WebApiDesafioFinal.Contratos
{
    public interface ICursoPersist
    {
        Task<Curso[]> GetAllCursosByStatusAsync(int status);
        Task<Curso[]> GetAllCursosAsync();
        Task<Curso> GetCursoByIdAsync(int eventoId);
    }
}
