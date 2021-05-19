using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{personId}")]
        [ProducesResponseType(200, Type = typeof(PersonInfo))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PersonInfo>> Get(int personId)
        {
            if (personId == 0)
            {
                return new BadRequestObjectResult(Constants.ErrorMessages.IdIncorrect);
            }

            var person = await _personService.GetAsync(personId);
            if (person != null)
            {
                return new OkObjectResult(person);
            }

            return new ObjectResult(null) { StatusCode = 204 };
        }

        [HttpDelete()]
        [Route("Delete")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PersonInfo>> DeletePersonAsync([FromBody] PersonInfo personInfo)
        {
            if (personInfo == null || personInfo.Id == null || personInfo.Id == 0)
            {
                return new BadRequestObjectResult(Constants.ErrorMessages.InformationError);
            }

            var result = await _personService.DeletePersonAsync(personInfo);

            return new ObjectResult(result) { StatusCode = 200 };
        }

        [HttpPost()]
        [Route("Search")]
        [ProducesResponseType(200, Type = typeof(PersonInfo))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PersonInfo>> Search(PersonSearch personSearch)
        {
            var person = await _personService.SearchPersonAsync(personSearch);

            if (person != null)
            {
                return new OkObjectResult(person);
            }

            return new ObjectResult(null) { StatusCode = 204 };
        }

        [HttpPost()]
        [Route("Add")]
        [ProducesResponseType(201, Type = typeof(PersonInfo))]
        [ProducesResponseType(400)]
        [ProducesResponseType(412)]
        public async Task<ActionResult<PersonInfo>> AddPersonAsync([FromBody] PersonCreationInfo personCreationInfo)
        {
            if (personCreationInfo == null)
            {
                return new BadRequestObjectResult(Constants.ErrorMessages.InsufficientInformation);
            }

            var createdPerson = await _personService.AddPersonAsync(personCreationInfo);

            if(createdPerson != null)
            {
                return new ObjectResult(createdPerson) { StatusCode = 201 };
            }

            return new UnprocessableEntityObjectResult(Constants.ErrorMessages.NotAdded);
        }

        [HttpPut()]
        [Route("Update")]
        [ProducesResponseType(201, Type = typeof(PersonInfo))]
        [ProducesResponseType(400)]
        [ProducesResponseType(412)]
        public async Task<ActionResult<PersonInfo>> UpdatePersonAsync([FromBody] PersonInfo personInfo)
        {
            if (personInfo == null || personInfo.Id ==null || personInfo.Id == 0)
            {
                return new BadRequestObjectResult(Constants.ErrorMessages.InformationError);
            }

            var updatedPerson = await _personService.UpdatePersonAsync(personInfo);

            if (updatedPerson != null)
            {
                return new ObjectResult(updatedPerson) { StatusCode = 201 };
            }

            return new UnprocessableEntityObjectResult(Constants.ErrorMessages.NotUpdated);
        }
    }
}
