using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalManager.Models
{
    public class Fest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime FestDate { get; set; }

        public List<Participant> Participants { get; set; }
        public List<Judge> Judges { get; set; }
    }
}
