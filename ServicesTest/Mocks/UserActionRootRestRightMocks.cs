using Bogus;
using Data.Privileges;
using Moq;
using Repository;
using ServicesTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesTest.Mocks
{
    public class UserActionRootRestRightMocks : QueryableDbSetMock
    {
        public static Guid Id = Guid.Parse("2A93CE72-6E7F-45B4-CE12-08D8FA615C82");
        public static Guid ActionRootId = Guid.Parse("2A93CE73-6E7F-45B4-CE12-08D8FA615C82");
        public static string IdentityRoleId = Guid.Parse("2A93CE74-6E7F-45B4-CE12-08D8FA615C82") + "";

        public static IEnumerable<UserActionRootRestRight> GetUserActionRootRestRights()
        {
            Randomizer.Seed = new Random(123456);

            var actionrootgeneration = new Faker<ActionRoot>()
             .RuleFor(o => o.Id, ActionRootId)
             .RuleFor(c => c.CodeName, f => "Action_" + f.UniqueIndex + "")
             .RuleFor(c => c.NameAl, f => f.Name.Random.ToString())
             .RuleFor(c => c.NameEn, f => f.Name.Random.ToString())
             .RuleFor(c => c.NameSr, f => f.Name.Random.ToString())
             .RuleFor(c => c.IsDeleted, false);

            var uarrr = new Faker<UserActionRootRestRight>()
              .RuleFor(o => o.Id, Guid.NewGuid)
              .RuleFor(o => o.ActionRootId, ActionRootId)
             .RuleFor(c => c.ActionRoot, actionrootgeneration)
             .RuleFor(c => c.IdentityRoleId, IdentityRoleId)
             .RuleFor(c => c.IsDeleted, false);

            var rez = uarrr.Generate(9999);
            rez.Add(new UserActionRootRestRight { Id = Id, ActionRoot = actionrootgeneration });

            return rez;
        }

        public static IQueryable<UserActionRootRestRight> GetUserActionRootRestRightIQ()
        {
            var actonRoots = GetUserActionRootRestRights();
            return actonRoots.AsQueryable();
        }

        public static Mock<IRepository<UserActionRootRestRight>> GetAll()
        {
            var actionResults = GetUserActionRootRestRights().ToList();
            var Mock = new Mock<IRepository<UserActionRootRestRight>>();

            Mock.Setup(r => r.GetAll()).Returns(actionResults);

            return Mock;

        }
        /*    
        private Mock<IRepository<UserActionRootRestRight>> _getInsert;
        private Mock<IRepository<UserActionRootRestRight>> _getInsertRang;
        private Mock<IRepository<UserActionRootRestRight>> _getUpdate;
        private Mock<IRepository<UserActionRootRestRight>> _getUpdateRang;
          
         */

        public static Mock<IRepository<UserActionRootRestRight>> Insert()
        {
            var list = GetUserActionRootRestRights().ToList();
            var mock = new Mock<IRepository<UserActionRootRestRight>>();

            mock.Setup(r => r.Insert(It.IsAny<UserActionRootRestRight>())).Callback((UserActionRootRestRight ua) =>
            {
                ua.Id = Guid.NewGuid();
                list.Add(ua);
            }
            ).Verifiable();

            mock.Setup(r => r.GetAll()).Returns(list);

            return mock;
        }
        public static Mock<IRepository<UserActionRootRestRight>> Update()
        {
            var list = GetUserActionRootRestRights().ToList();
            var mock = new Mock<IRepository<UserActionRootRestRight>>();

            mock.Setup(r => r.Update(It.IsAny<UserActionRootRestRight>())).Callback((UserActionRootRestRight ua) =>
            {
                ua.Id = Guid.NewGuid();
                list.Add(ua);
            }
            ).Verifiable();

            mock.Setup(r => r.GetAll()).Returns(list);

            return mock;
        }
        public static Mock<IRepository<UserActionRootRestRight>> InsertRang()
        {
            var list = GetUserActionRootRestRights().ToList();
            var mock = new Mock<IRepository<UserActionRootRestRight>>();
            var o = new UserActionRootRestRight
            {
                Id = Guid.NewGuid(),
            };

            mock.Setup(r => r.InsertRange(It.IsAny<List<UserActionRootRestRight>>())).Callback<List<UserActionRootRestRight>>(p => { p.Add(o); p.Add(o); }).Verifiable();

            mock.Setup(r => r.GetAll()).Returns(list);

            return mock;
        }
        public static Mock<IRepository<UserActionRootRestRight>> UpdateRang()
        {
            var list = GetUserActionRootRestRights().ToList();
            var mock = new Mock<IRepository<UserActionRootRestRight>>();
            var o = new UserActionRootRestRight
            {
                Id = Guid.NewGuid(),
            };

            mock.Setup(r => r.UpdateRange(It.IsAny<List<UserActionRootRestRight>>())).Callback<List<UserActionRootRestRight>>(p => { p.Add(o); p.Add(o); }).Verifiable();

            mock.Setup(r => r.GetAll()).Returns(list);

            return mock;
        }

        public static Mock<IRepository<UserActionRootRestRight>> GetByRoleId(Guid arId)
        {
            var actionRoot = GetUserActionRootRestRights().Where(c => c.IdentityRoleId == arId+"").ToList();

            var mockSet = GetQueryableMockDbSet<UserActionRootRestRight>(actionRoot.ToArray());
            string[] includes = { "ActionRoot" };

            var actonRootmoke = new Mock<IRepository<UserActionRootRestRight>>();
            actonRootmoke.Setup( repo => repo.Get(x => x.IdentityRoleId == arId+"", null, includes)).Returns(actionRoot); 

            return actonRootmoke;

        }
        public static Mock<IRepository<UserActionRootRestRight>> GetById(Guid Id)
        {
            var actionRoot = GetUserActionRootRestRights().FirstOrDefault(c => c.Id == Id);

            var actonRootmoke = new Mock<IRepository<UserActionRootRestRight>>();
            actonRootmoke.Setup(r => r.GetById(Id.ToString())).Returns(actionRoot);

            return actonRootmoke;

        }
        public static Mock<IRepository<UserActionRootRestRight>> GetByUserId(string RoleId, Guid ActionRootId)
        {
            var actionRoot = GetUserActionRootRestRights().Where(c => c.ActionRootId == ActionRootId && c.IdentityRoleId == RoleId+"");

            var actonRootmoke = new Mock<IRepository<UserActionRootRestRight>>();
            actonRootmoke.Setup(r => r.Get(x => x.IdentityRoleId == RoleId && x.ActionRootId == ActionRootId, null,null)).Returns(actionRoot);

            return actonRootmoke;

        }
    }
}
