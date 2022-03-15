using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApiDesafioFinal.Contratos;

namespace WebApiDesafioFinal.Data
{
    public class GeralPersist : IGeralPersist
    {
        private readonly ApiContext _context;

        public GeralPersist(ApiContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
