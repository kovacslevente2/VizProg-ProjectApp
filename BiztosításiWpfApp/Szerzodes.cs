using System;
using System.Collections.Generic;

namespace BiztositasKezelo;

public partial class Szerzodes
{
    public int SzerzodesId { get; set; }

    public int FelhId { get; set; }

    public int BiztId { get; set; }

    public string Osszeg { get; set; } = null!;

    public DateOnly Datum { get; set; }

    public int Honap { get; set; }

    public Biztosito Bizt { get; set; } = null!;

    public Felhasznalo Felh { get; set; } = null!;

    public ICollection<Karesemeny> Karesemeny { get; set; } = new List<Karesemeny>();
}
