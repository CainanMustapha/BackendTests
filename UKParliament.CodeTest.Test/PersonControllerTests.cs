using Microsoft.AspNetCore.Mvc;
using Moq;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models;
using UKParliament.CodeTest.Web.Controllers;
using Xunit;

namespace UKParliament.CodeTest.Test
{
    public class PersonControllerTests
    {
        [Fact]
        public void Get_WhenPersonIdExist_ShouldReturnPersonInfo()
        {
            var expected = Mock.Of<PersonInfo>();
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(service => service.GetAsync(It.IsAny<int>()).Result).Returns(expected);
            var personController = new PersonController(personServiceStub.Object);

            var outcome = personController.Get(2).Result;
            var result = ((ObjectResult)outcome.Result).Value;

            Assert.IsAssignableFrom<PersonInfo>(result);
        }

        [Fact]
        public void Search_WhenPersonIdExist_ShouldReturnPersonInfo()
        {
            var expected = Mock.Of<PersonInfo>();
            var personSearchMock = Mock.Of<PersonSearch>();
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(service => service.SearchPersonAsync(It.IsAny<PersonSearch>()).Result).Returns(expected);
            var personController = new PersonController(personServiceStub.Object);

            var outcome = personController.Search(personSearchMock).Result;
            var result = ((ObjectResult)outcome.Result).Value;

            Assert.IsAssignableFrom<PersonInfo>(result);
        }

        [Fact]
        public void AddPersonAsync_WhenPersonIdExist_ShouldReturnPersonInfo()
        {
            var expected = Mock.Of<PersonInfo>();
            var personCreationInfoMock = Mock.Of<PersonCreationInfo>();
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(service => service.AddPersonAsync(It.IsAny<PersonCreationInfo>()).Result).Returns(expected);
            var personController = new PersonController(personServiceStub.Object);

            var outcome = personController.AddPersonAsync(personCreationInfoMock).Result;
            var result = ((ObjectResult)outcome.Result).Value;

            Assert.IsAssignableFrom<PersonInfo>(result);
        }

        [Fact]
        public void UpdatePersonAsync_WhenPersonIdExist_ShouldReturnPersonInfo()
        {
            var expected = Mock.Of<PersonInfo>();
            expected.Id = 3;
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(service => service.UpdatePersonAsync(It.IsAny<PersonInfo>()).Result).Returns(expected);
            var personController = new PersonController(personServiceStub.Object);

            var outcome = personController.UpdatePersonAsync(expected).Result;
            var result = ((ObjectResult)outcome.Result).Value;

            Assert.IsAssignableFrom<PersonInfo>(result);
        }

        [Fact]
        public void DeletePersonAsync_WhenPersonIdExist_ShouldReturnPersonInfo()
        {
            var personInfoMock = Mock.Of<PersonInfo>();
            personInfoMock.Id = 10;
            var personServiceStub = new Mock<IPersonService>();
            personServiceStub.Setup(service => service.DeletePersonAsync(It.IsAny<PersonInfo>()).Result).Returns(true);
            var personController = new PersonController(personServiceStub.Object);

            var outcome = personController.DeletePersonAsync(personInfoMock).Result;
            var result = ((ObjectResult)outcome.Result).Value;

            Assert.Equal(true, result);
        }
    }
}
