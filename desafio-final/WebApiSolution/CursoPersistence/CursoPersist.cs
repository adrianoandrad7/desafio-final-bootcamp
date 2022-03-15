using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApiDesafioFinal.Contratos;
using WebApiDesafioFinal.Domain;

namespace WebApiDesafioFinal.Data
{
    public class CursoPersist : ICursoPersist
    {
        private readonly ApiContext _context;

        public CursoPersist(ApiContext context)
        {
            _context = context;
        }

        public async Task<Curso[]> GetAllCursosAsync()
        {
            IQueryable<Curso> query = _context.Cursos.AsNoTracking();

            query = query.OrderBy(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Curso> GetCursoByIdAsync(int eventoId)
        {
            IQueryable<Curso> query = _context.Cursos.AsNoTracking();

            query = query.OrderBy(c => c.Id).Where(c => c.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Curso[]> GetAllCursosByStatusAsync(int status)
        {
            IQueryable<Curso> query = _context.Cursos.AsNoTracking();

            query = query.OrderBy(c => c.Id).Where(c => ((int)c.Status) == status);

            return await query.ToArrayAsync();
        }
    }
}
