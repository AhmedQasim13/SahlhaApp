using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SahlhaApp.Models.DTOs.Request;
using System.Security.Claims;

namespace SahlhaApp.Areas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskAssignmentsController(IUnitOfWork uow, UserManager<ApplicationUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<TaskAssignmentDto>>> GetMyAssignments()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var tasks = await _uow.TaskAssignment
                .GetAll(t => t.Job.ApplicationUserId == user.Id,
                        includes: [t => t.Provider, t => t.Job])
                .OrderByDescending(t => t.AssignedAt)
                .Select(t => new TaskAssignmentDto
                {
                    Id = t.Id,
                    JobTitle = t.Job.Name,
                    ProviderName = t.Provider.ApplicationUser.FirstName + " " +
                                   t.Provider.ApplicationUser.LastName,
                    FinalPrice = t.FinalPrice,
                    AssignedAt = t.AssignedAt,
                    IsCompleted = t.IsCompleted
                })
                .ToListAsync();

            return Ok(tasks);
        }
    }
}
