using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahlhaApp.Models.DTOs.Request
{
    public class TaskAssignmentDto
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string ProviderName { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime AssignedAt { get; set; }
        public bool IsCompleted { get; set; }
    }
}
