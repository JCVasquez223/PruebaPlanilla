using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.AppWebMVC.Models
{
    public partial class Planilla
    {
        public Planilla()
        {
            EmpleadoPlanillas = new HashSet<EmpleadoPlanilla>(); 
        }


        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la planilla es obligatorio.")] 
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")] 
        [Display(Name = "Nombre de la Planilla")] 
        public string NombrePlanilla { get; set; } = null!;

        [Required(ErrorMessage = "El tipo de planilla es obligatorio.")] 
        [Display(Name = "Tipo de Planilla")] 
        public int TipoPlanillaId { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [Display(Name = "Fecha de Inicio")] 
        [DataType(DataType.Date)] 
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")] 
        [Display(Name = "Fecha de Fin")] 
        [DataType(DataType.Date)] 
        public DateTime FechaFin { get; set; }

        [Display(Name = "Autorización")] //
        public byte Autorizacion { get; set; } = 0; 

        [Required(ErrorMessage = "El total de pago es obligatorio.")] 
        [Display(Name = "Total de Pago")] 
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPago { get; set; }

        [NotMapped]
        [Display(Name = "Fecha de Creación")] 
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [NotMapped]
        [Display(Name = "Fecha de Última Modificación")]
        public DateTime? FechaModificacion { get; set; } 

        
        public virtual ICollection<EmpleadoPlanilla> EmpleadoPlanillas { get; set; }

        [ForeignKey("TipoPlanillaId")]
        public virtual TipoPlanilla TipoPlanilla { get; set; } = null!;

       
        [NotMapped] 
        [Display(Name = "Cantidad de Empleados")] 
        public int CantidadEmpleados => EmpleadoPlanillas?.Count ?? 0;

     
        [NotMapped]
        [Display(Name = "Estado de Autorización")]
        public string EstadoAutorizacion => Autorizacion == 1 ? "Autorizada" : "No Autorizada";

       
        public void Autorizar()
        {
            Autorizacion = 1; 
            FechaModificacion = DateTime.UtcNow; 
        }

        public void Desautorizar()
        {
            Autorizacion = 0; 
            FechaModificacion = DateTime.UtcNow;
        }

        public bool EstaActiva()
        {
            var fechaActual = DateTime.UtcNow;
            return fechaActual >= FechaInicio && fechaActual <= FechaFin;
        }
    }
}