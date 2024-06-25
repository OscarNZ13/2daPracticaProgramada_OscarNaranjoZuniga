using System.ComponentModel.DataAnnotations.Schema;

namespace _2daPracticaProgramada_OscarNaranjoZuniga.Models
{
    public class Lista
    {
        public int ListaId { get; set; }

        public int UsuarioId { get; set; }

        public required string Titulo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime FechaCreacion { get; set; }

        // Propiedad de navegación a Tareas
        public List<Tarea> Tareas { get; set; } = new List<Tarea>();

    }
}
