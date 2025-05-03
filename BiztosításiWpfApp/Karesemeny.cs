using System;
using System.Collections.Generic;

namespace BiztositasKezelo;

public partial class Karesemeny
{
    public int KaresemenyId { get; set; }

    public int SzerzId { get; set; }

    public string Megnevezes { get; set; } = null!;

    public Szerzodes Szerz { get; set; } = null!;
}
