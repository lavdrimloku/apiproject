using Data.Privileges;
using Moq;
using NUnit.Framework;
using Repository;
using Services.Privileges;
using ServicesTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesTest.Privileges
{
    public class UserActionRootRestRightTest
    {
        private Mock<IRepository<UserActionRootRestRight>> _getById;
        private Mock<IRepository<UserActionRootRestRight>> _getAll;
        private Mock<IRepository<UserActionRootRestRight>> _getAllByRoleId;
        private Mock<IRepository<UserActionRootRestRight>> _getByUserId;
        private Mock<IRepository<UserActionRootRestRight>> _getInsert;
        private Mock<IRepository<UserActionRootRestRight>> _getInsertRang;
        private Mock<IRepository<UserActionRootRestRight>> _getUpdate;
        private Mock<IRepository<UserActionRootRestRight>> _getUpdateRang;

        [SetUp]
        public void SetUp()
        {
            _getById = UserActionRootRestRightMocks.GetById(UserActionRootRestRightMocks.Id);

            _getInsert = UserActionRootRestRightMocks.Insert();
            _getUpdate = UserActionRootRestRightMocks.Update();
            _getInsertRang = UserActionRootRestRightMocks.InsertRang();
            _getUpdateRang = UserActionRootRestRightMocks.UpdateRang();
            _getAll = UserActionRootRestRightMocks.GetAll();
            _getAllByRoleId = UserActionRootRestRightMocks.GetByRoleId(Guid.Parse(UserActionRootRestRightMocks.IdentityRoleId));
            _getByUserId = UserActionRootRestRightMocks.GetByUserId(UserActionRootRestRightMocks.IdentityRoleId, UserActionRootRestRightMocks.ActionRootId);
           

        }
        [Test]
        public void getAll()
        {
            var service = new UserActionRootRestRightService(_getAll.Object);
            var res = service.GetAll();
            Assert.AreEqual(10000, res.Count);
        }
        [Test]
        public void getById()
        {
            var service = new UserActionRootRestRightService(_getById.Object);
            var res = service.GetById(UserActionRootRestRightMocks.Id.ToString());
            Assert.AreEqual(UserActionRootRestRightMocks.Id, res.Id);
        }
        [Test]
        public void GetAllByRoleId()
        {
            //GetByRoleId
            var service = new UserActionRootRestRightService(_getAllByRoleId.Object);
            var res = service.GetAll(UserActionRootRestRightMocks.IdentityRoleId.ToString());
            Assert.IsTrue(res.Count() == 0);
            //TODO: duhet me kqyre 
        }
        [Test]
        public void GetByUserId()
        {
            //GetByRoleId
            var service = new UserActionRootRestRightService(_getByUserId.Object);
            var res = service.GetByUserId(UserActionRootRestRightMocks.IdentityRoleId.ToString(), UserActionRootRestRightMocks.ActionRootId);
            Assert.IsTrue(res.ActionRootId == UserActionRootRestRightMocks.ActionRootId);
        }
        [Test]
        public void Inser()
        {
            var service = new UserActionRootRestRightService(_getInsert.Object);
            var ua = new UserActionRootRestRight()
            {
                Id = Guid.NewGuid(),
                CanDelete = false,
                CanRead = true,
                CanWrite = false,
                IsDeleted = true
            };

            var res = service.Create(ua);

            Assert.AreEqual(res.Id, ua.Id);
            Assert.IsTrue(service.GetAll().Count > 1);
        }

        [Test]
        public void Update()
        {
            var service = new UserActionRootRestRightService(_getUpdate.Object);

            var objecedb = UserActionRootRestRightMocks.GetUserActionRootRestRights().ToList().FirstOrDefault();

            objecedb.CanDelete = objecedb.CanRead = objecedb.CanWrite = true;

            var res = service.Update(objecedb);


            Assert.IsTrue(res);
        }
        [Test]
        public void InsertRang()
        {
            var service = new UserActionRootRestRightService(_getInsertRang.Object);

            var uas = new List<UserActionRootRestRight>()
            { new UserActionRootRestRight()  {
                    Id = Guid.NewGuid(),
                    CanDelete = false,
                    CanRead = true,
                    CanWrite = false,
                    IsDeleted = true   },
                new UserActionRootRestRight()  {
                    Id = Guid.NewGuid(),
                    CanDelete = false,
                    CanRead = true,
                    CanWrite = true,
                    IsDeleted = true  }
            };

            var res = service.InsertRange(uas);

            Assert.AreEqual(true, res);

        }
        [Test]
        public void UpdateRang()
        {
            var service = new UserActionRootRestRightService(_getUpdateRang.Object);

            var uas = new List<UserActionRootRestRight>()
            { new UserActionRootRestRight()  {
                    Id = Guid.NewGuid(),
                    CanDelete = false,
                    CanRead = true,
                    CanWrite = false,
                    IsDeleted = true   },
                new UserActionRootRestRight()  {
                    Id = Guid.NewGuid(),
                    CanDelete = false,
                    CanRead = true,
                    CanWrite = true,
                    IsDeleted = true  }
            };

            var res = service.UpdateRange(uas);

            Assert.AreEqual(true, res);

        }


    }
}
