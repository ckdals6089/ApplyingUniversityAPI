using System;
using System.Collections.Generic;

namespace ApplyingUniversityWeb.Models
{
    public partial class University
    {
        public University()
        {
            Application = new HashSet<Application>();
        }

        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Link { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual ICollection<Application> Application { get; set; }
    }
}
