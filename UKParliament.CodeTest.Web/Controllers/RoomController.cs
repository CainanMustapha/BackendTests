using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("{roomId}")]
        [ProducesResponseType(200, Type = typeof(RoomInfo))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RoomInfo>> Get(int roomId)
        {
            if (roomId == 0)
            {
                return new BadRequestObjectResult(Constants.ErrorMessages.IdIncorrect);
            }

            var roomInfo = await _roomService.GetAsync(roomId);
            if (roomInfo != null)
            {
                return new OkObjectResult(roomInfo);
            }

            return new ObjectResult(null) { StatusCode = 204 };
        }

        [HttpDelete()]
        [Route("Delete")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<bool>> DeleteRoomAsync([FromBody] RoomSearch roomSearch)
        {
            if (roomSearch == null || string.IsNullOrWhiteSpace(roomSearch.Name))
            {
                return new BadRequestObjectResult(Constants.ErrorMessages.InformationError);
            }

            var result = await _roomService.DeleteRoomAsync(roomSearch);

            return new ObjectResult(result) { StatusCode = 200 };
        }

        [HttpPost()]
        [Route("Search")]
        [ProducesResponseType(200, Type = typeof(RoomInfo))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RoomInfo>> Search(RoomSearch roomSearch)
        {
            var roomInfo = await _roomService.SearchRoomAsync(roomSearch);

            if (roomInfo != null)
            {
                return new OkObjectResult(roomInfo);
            }

            return new ObjectResult(null) { StatusCode = 204 };
        }

        [HttpPost()]
        [Route("Add")]
        [ProducesResponseType(201, Type = typeof(RoomInfo))]
        [ProducesResponseType(400)]
        [ProducesResponseType(412)]
        public async Task<ActionResult<RoomInfo>> AddRoomAsync([FromBody] RoomCreationInfo roomCreationInfo)
        {
            if (roomCreationInfo == null)
            {
                return new BadRequestObjectResult(Constants.ErrorMessages.InsufficientInformation);
            }

            var createdRoomInfo = await _roomService.AddRoomAsync(roomCreationInfo);

            if (createdRoomInfo != null)
            {
                return new ObjectResult(createdRoomInfo) { StatusCode = 201 };
            }

            return new UnprocessableEntityObjectResult(Constants.ErrorMessages.NotAdded);
        }

        [HttpPut()]
        [Route("Update")]
        [ProducesResponseType(201, Type = typeof(RoomInfo))]
        [ProducesResponseType(400)]
        [ProducesResponseType(412)]
        public async Task<ActionResult<RoomInfo>> UpdateRoomAsync([FromBody] RoomInfo roomInfo)
        {
            if (roomInfo == null || roomInfo.Id == 0)
            {
                return new BadRequestObjectResult(Constants.ErrorMessages.InformationError);
            }

            var updatedRoomInfo = await _roomService.UpdateRoomAsync(roomInfo);

            if (updatedRoomInfo != null)
            {
                return new ObjectResult(updatedRoomInfo) { StatusCode = 201 };
            }

            return new UnprocessableEntityObjectResult(Constants.ErrorMessages.NotUpdated);
        }
    }
}
