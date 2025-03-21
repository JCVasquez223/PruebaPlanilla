﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prueba.AppWebMVC.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public int? JefeInmediatoId { get; set; } = null!;

    public int? TipoDeHorarioId { get; set; }

    public int? Dui { get; set; }

    public string? Nombre { get; set; } = null!;

    public string? Apellido { get; set; } = null!;

    public string? Telefono { get; set; } = null!;

    public string? Correo { get; set; }

    public byte? Estado { get; set; }

    public DateTime FechaContraInicial { get; set; }

    public DateTime FechaContraFinal { get; set; }

    public string? Usuario { get; set; } = null!;

    public string? Contraseña { get; set; } = null!;

    public decimal? SalarioBase { get; set; }

    [Display(Name = "Puesto trabajo")]
    public int? PuestoTrabajoId { get; set; }

    public virtual ICollection<AsignacionBono>? AsignacionBonos { get; set; } = new List<AsignacionBono>();

    public virtual ICollection<AsignacionDescuento>? AsignacionDescuentos { get; set; } = new List<AsignacionDescuento>();

    public virtual ICollection<ControlAsistencium>? ControlAsistencia { get; set; } = new List<ControlAsistencium>();

    public virtual ICollection<EmpleadoPlanilla>? EmpleadoPlanillas { get; set; } = new List<EmpleadoPlanilla>();

    public virtual ICollection<Empleado>? InverseJefeInmediato { get; set; } = new List<Empleado>();

    public virtual Empleado? JefeInmediato { get; set; }

    public virtual ICollection<JefeInmediato>? JefeInmediatos { get; set; } = new List<JefeInmediato>();

    public virtual PuestoTrabajo? PuestoTrabajo { get; set; } = null!;

    public virtual TipodeHorario? TipoDeHorario { get; set; } = null!;

    public virtual ICollection<Vacacion>? Vacacions { get; set; } = new List<Vacacion>();
}
