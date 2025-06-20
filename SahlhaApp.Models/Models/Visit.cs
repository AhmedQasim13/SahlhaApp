using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahlhaApp.Models.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; } = DateTime.UtcNow;
        public string ApplicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; }

    }
}
