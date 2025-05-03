using System;
using System.Collections.Generic;

namespace BiztositasKezelo;

public partial class Biztosito
{
    public int BiztositoId { get; set; }

    public string Nev { get; set; } = null!;

    public string Tipus { get; set; } = null!;

    public ICollection<Szerzodes> Szerzodes { get; set; } = new List<Szerzodes>();
}
