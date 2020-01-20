using System;
using System.Collections.Generic;

namespace ApplyingUniversityWebMVC.Models
{
    public partial class Application
    {
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public int UniversityId { get; set; }
        public DateTime? AppliedDate { get; set; }

        public University University { get; set; }
        public Users UserName { get; set; }
        

    }
}
