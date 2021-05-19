using AutoMapper;
using Moq;
using System;
using UKParliament.CodeTest.Services.Interfaces;

namespace UKParliament.CodeTest.Test.Fixtures
{
    public class PersonServiceFixture : IDisposable
    {
        public Mock<IPersonDataStoreRepo> PersonDataStoreRepo { get; private set; }
        public Mock<IMapper> Mapper { get; private set; }

        public PersonServiceFixture()
        {
            PersonDataStoreRepo = new Mock<IPersonDataStoreRepo>();
            Mapper = new Mock<IMapper>();
        }

        public void Dispose()
        {
            // Nothing to do
        }
    }
}
