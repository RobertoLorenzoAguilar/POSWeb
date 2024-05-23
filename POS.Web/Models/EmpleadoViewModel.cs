using System.ComponentModel.DataAnnotations;

namespace POS.Web.Models
{
    public class EmpleadoViewModel
    {
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El Nombre del Empleado es obligatorio.")]
        public string NombreEmpleado { get; set; } = null!;

        [Required(ErrorMessage = "El Apellido Paterno del Empleado es obligatorio.")]
        public string ApellidoPaterno { get; set; } = null!;

        [Required(ErrorMessage = "El Apellido Materno del Empleado es obligatorio.")]
        public string ApellidoMaterno { get; set; } = null!;

        [Required(ErrorMessage = "El Usuario del Sistema Empleado es obligatorio.")]
        public string UsuarioSistema { get; set; } = null!;

        [Required(ErrorMessage = "La Contraseña del Sistema Empleado es obligatorio.")]
        public string ContrasenaSistema { get; set; } = null!;

        [Required(ErrorMessage = "El NSS del  Empleado es obligatorio.")]
        [StringLength(11, ErrorMessage = "El NSS del Empleado no puede tener más de 11 caracteres.")]
        public string Nss { get; set; } = null!;

        public int IdSucursal { get; set; }
        public string? NombreSucursal { get; set; } 

        public string? TipoUsuario { get; set; } = null!;
    }
}
