using System;
using System.Collections.Generic;

namespace Prueba.AppWebMVC.Models;

public partial class ControlAsistencium
{
    public int Id { get; set; }

    public int EmpleadosId { get; set; }

    public string Dia { get; set; } = null!;

    public string Entrada { get; set; } = null!;

    public string Salida { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public string Asistencia { get; set; } = null!;

    public int? HoraTardia { get; set; }

    public int? HorasExtra { get; set; }

    public virtual Empleado Empleados { get; set; } = null!;
}
