using System.ComponentModel.DataAnnotations.Schema;

namespace _2daPracticaProgramada_OscarNaranjoZuniga.Models.ViewModels
{
    public class LoginVM
    {
        public required string Email { get; set; }

        public required string Contrasena { get; set; }

    }
}
