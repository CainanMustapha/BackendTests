using Moq;
using UKParliament.CodeTest.Data.Domain;
using UKParliament.CodeTest.Services.Models;
using UKParliament.CodeTest.Services.Services;
using UKParliament.CodeTest.Test.Fixtures;
using Xunit;

namespace UKParliament.CodeTest.Test
{
    public class PersonServiceTests : IClassFixture<PersonServiceFixture>
    {
        private readonly PersonServiceFixture _personServiceFixture;

        public PersonServiceTests(PersonServiceFixture personServiceFixture)
        {
            _personServiceFixture = personServiceFixture;
        }

        [Fact]
        public void GetAsync_WhenPersonIdExist_ShouldReturnPersonInfo()
        {
            var personInfoMock = Mock.Of<PersonInfo>();
            var personMock = Mock.Of<Person>();
            _personServiceFixture.PersonDataStoreRepo.Setup(repo => repo.GetPersonByIdAsync(It.IsAny<int>()).Result).Returns(new PersonRepoActionResult() {Success = true, Person = personMock });
            _personServiceFixture.Mapper.Setup(mapper => mapper.Map<PersonInfo>(It.IsAny<Person>())).Returns(personInfoMock);
            var personService = new PersonService(_personServiceFixture.PersonDataStoreRepo.Object, _personServiceFixture.Mapper.Object);

            var outcome = personService.GetAsync(0).Result;

            Assert.IsAssignableFrom<PersonInfo>(outcome);
        }
    }
}
