using SahlhaApp.Models.DTOs.Request;


namespace SahlhaApp.Areas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {

        private IUnitOfWork _unitOfWork;
        public CheckOutController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPut]
        public async Task<IActionResult> AddDifferentLocationToTaskAssigned([FromBody] UpdateTaskAssignedLocationRequestDTO updateTaskAssignedLocationRequestDTO)

        {
            var task = await _unitOfWork.TaskAssignment.GetOne(e => e.Id == updateTaskAssignedLocationRequestDTO.TaskAssignedId);
            if (task == null)
            {
                return NotFound("Task not found");
            }
            task.Street = updateTaskAssignedLocationRequestDTO.Street;
            task.City = updateTaskAssignedLocationRequestDTO.City;
            task.Province = updateTaskAssignedLocationRequestDTO.Province;
            task.BuildingNumber = updateTaskAssignedLocationRequestDTO.BuildingNumber;

            await _unitOfWork.TaskAssignment.Edit(task);

            return Ok();
        }

    }
}
