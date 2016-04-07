using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.Model
{
    public class Fillup
    {
        public Fillup(int odometer, decimal liters)
        {
            Odometer = odometer;
            Liters = liters;
        }

        public Fillup()
        {
        }

        public decimal? KML
        {
            get
            {
                if (this.NextFillup == null)
                    return null;

                return (this.NextFillup.Odometer - this.Odometer) / this.NextFillup.Liters;
            }
        }
        public decimal Liters { get; set; }
        public int Odometer { get; set; }
        public Fillup NextFillup { get; set;}
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}
