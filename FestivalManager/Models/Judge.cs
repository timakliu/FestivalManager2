using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FestivalManager.Models
{
    public class Judge
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int FestId { get; set; }

        [XmlIgnore]
        public Fest Fest { get; set; }  
    }
}
