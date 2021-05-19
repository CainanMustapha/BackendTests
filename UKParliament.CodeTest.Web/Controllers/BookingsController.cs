using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingsService _bookingsService;

        public BookingsController(IBookingsService bookingsService)
        {
            _bookingsService = bookingsService;
        }

        [HttpPost()]
        [Route("Add")]
        [ProducesResponseType(201, Type = typeof(RoomBooking))]
        [ProducesResponseType(400)]
        [ProducesResponseType(412)]
        public async Task<ActionResult<RoomBooking>> AddBookingAsync([FromBody] RoomBookingInfo roomBookingInfo)
        {
            if (roomBookingInfo == null)
            {
                return new BadRequestObjectResult(Constants.ErrorMessages.InsufficientInformation);
            }

            var createdRoomBooking = await _bookingsService.AddRoomBookingAsync(roomBookingInfo);

            if (createdRoomBooking != null)
            {
                return new ObjectResult(createdRoomBooking) { StatusCode = 201 };
            }

            return new UnprocessableEntityObjectResult(Constants.ErrorMessages.NotAdded);
        }

        [HttpDelete()]
        [Route("Remove")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PersonInfo>> RemoveBookingAsync([FromBody] RoomBookingInfo roomBookingInfo)
        {
            if (roomBookingInfo == null || string.IsNullOrWhiteSpace(roomBookingInfo.RoomName))
            {
                return new BadRequestObjectResult(Constants.ErrorMessages.InformationError);
            }

            var result = await _bookingsService.RemoveRoomBookingAsync(roomBookingInfo);

            return new ObjectResult(result) { StatusCode = 200 };
        }

        [HttpPost()]
        [Route("Search")]
        [ProducesResponseType(200, Type = typeof(ICollection<RoomInfo>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ICollection<RoomInfo>>> Search(RoomBookingPeriod roomBookingSearch)
        {
            var availableRooms = await _bookingsService.GetAvailableRoomsAsync(roomBookingSearch);

            if (availableRooms?.Count > 0)
            {
                return new OkObjectResult(availableRooms);
            }

            return new ObjectResult(null) { StatusCode = 204 };
        }
    }
}
