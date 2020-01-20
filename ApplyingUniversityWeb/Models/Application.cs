using System;
using System.Collections.Generic;

namespace ApplyingUniversityWeb.Models
{
    public partial class Application
    {
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public int UniversityId { get; set; }
        public DateTime? AppliedDate { get; set; }

        public University University{ get; set; }
        public Users Username { get; set; }
    }
}
