
using System;

namespace guia8._1.Models
{
    [Serializable]
    public class Cuenta : IComparable<Cuenta>
    {
        public string Nombre { get; set; }
        public int Dni { get; set; }
        public double Importe { get; set; }

        public Cuenta() { }
        public Cuenta(string nombre, int dni, double importe)
        {
            this.Nombre = nombre;
            this.Dni = dni;
            this.Importe = importe;
        }

        override public string ToString()
        {
            return $@"Nombre: {Nombre} - Dni :{Dni} - Importe: {Importe:f2}";
        }

        public int CompareTo(Cuenta other)
        {
            if (other !=null)
            {
                return this.Dni.CompareTo(other.Dni);
            }
            return -1;
        }
    }
}
