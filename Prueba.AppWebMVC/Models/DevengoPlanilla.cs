﻿using System;
using System.Collections.Generic;

namespace Prueba.AppWebMVC.Models;

public partial class DevengoPlanilla
{
    public int Id { get; set; }

    public int AsignacionBonosId { get; set; }

    public int EmpleadoPlanillaId { get; set; }

    public virtual AsignacionBono AsignacionBonos { get; set; } = null!;

    public virtual Vacacion EmpleadoPlanilla { get; set; } = null!;
}
