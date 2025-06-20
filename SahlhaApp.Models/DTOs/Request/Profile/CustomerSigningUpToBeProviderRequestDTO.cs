using SahlhaApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahlhaApp.Models.DTOs.Request.Profile
{
    public class CustomerSigningUpToBeProviderRequestDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<SubService> SubServices { get; set; }
        public double Hourlyrate { get; set; }
        public string Description { get; set; }


    }
}
