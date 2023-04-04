using Data.Privileges;
using Moq;
using NUnit.Framework;
using Repository;
using Services.Privileges;
using ServicesTest.Mocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesTest.Privileges
{
    public class ActionRootTest
    {
        private Mock<IRepository<ActionRoot>> _getAll;
        private Mock<IRepository<ActionRoot>> _getById;
        private Mock<IRepository<ActionRoot>> _getByStringId;

        [SetUp]
        public void SetUp()
        {
            _getAll = ActionRootsMocks.GetAll();
            _getById = ActionRootsMocks.GetById(ActionRootsMocks.Id);
            _getByStringId = ActionRootsMocks.GetById(ActionRootsMocks.Id.ToString());
        }

        [Test]
        public void GetAll()
        {
            var service = new ActionRootService(_getAll.Object);
            var res = service.GetAll();

            Assert.AreEqual(100, res.Count);

        }
        [Test]
        public void GetById()
        {
            var service = new ActionRootService(_getById.Object);
            var res = service.GetById(ActionRootsMocks.Id);

            Assert.AreEqual(ActionRootsMocks.Id, res.Id);
        }
        [Test]
        public void GetByStringId()
        {
            var service = new ActionRootService(_getById.Object);
            var res = service.GetById(ActionRootsMocks.Id.ToString());

            Assert.AreEqual(ActionRootsMocks.Id, res.Id);
        }


    }
}
