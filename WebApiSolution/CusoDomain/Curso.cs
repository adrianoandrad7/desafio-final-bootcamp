using WebApiDesafioFinal.Domain.Enums;

namespace WebApiDesafioFinal.Domain
{
    public class Curso
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Semestres { get; set; }
        public CursoStatus Status { get; set; }

        public Curso(string titulo, int semestres, CursoStatus status)
        {
            Titulo = titulo;
            Semestres = semestres;
            Status = status;
        }
    }
}
