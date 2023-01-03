using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalManager.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime TimePerformance { get; set; }
        public double Score { get; set; }

        public int FestId { get; set; }
        public Fest Fest { get; set; }


    }
}
