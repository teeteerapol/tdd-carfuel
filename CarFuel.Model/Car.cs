using CNX.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.Model
{
    public class Car
    {
        public Car()
        {
            ListFillup = new HashSet<Fillup>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public virtual ICollection<Fillup> ListFillup { get; set; }
        public Decimal? AverageKmL
        {
            get
            {
                //var count = this.ListFillup.Count(x => x.KML != null);
                //return this.ListFillup.Sum(x => x.KML) / count;
                if (ListFillup.Count > 2)
                {
                    var distance = ListFillup.Last().Odometer - ListFillup.First().Odometer;
                    var totalLiters = ListFillup.Sum(x => x.Liters) - ListFillup.First().Liters;
                    return Math.Round(distance / totalLiters, 2, MidpointRounding.AwayFromZero);
                }
                return null;
            }
        }

        public Fillup AddFillUp(int odometer, decimal liters)
        {
            Fillup f = new Fillup(odometer, liters);
            f.Date = SystemTime.Now();

            if (this.ListFillup.Count > 0)
            {
                this.ListFillup.Last().NextFillup = f;
                //this.ListFillup[this.ListFillup.Count - 1].NextFillup = f;
            }
            //var lastOne = this.ListFillup.SingleOrDefault(x => x.NextFillup == null);
            //if (lastOne != null)
            //{
            //    lastOne.NextFillup = f;
            //}

            this.ListFillup.Add(f);
            return f;
        }
    }
}
