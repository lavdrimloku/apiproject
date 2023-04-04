using Bogus;
using Data;
using Data.Privileges;
using Moq;
using Repository;
using Services.Enum;
using ServicesTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace ServicesTest.Mocks
{
    public class ActionRootsMocks : QueryableDbSetMock
    {
        public static Guid Id = Guid.Parse("2A93CE72-6E7F-45B4-CE12-08D8FA615C82");
        private static IEnumerable<ActionRoot> GetActionRoots()
        {

            Randomizer.Seed = new Random(123456);

            var actionrootgeneration = new Faker<ActionRoot>()
                .RuleFor(o => o.Id, Guid.NewGuid)
                .RuleFor(c => c.CodeName, f => "Action_" + f.UniqueIndex + "")
                .RuleFor(c => c.NameAl, f => f.Name.Random.ToString())
                .RuleFor(c => c.NameEn, f => f.Name.Random.ToString())
                .RuleFor(c => c.NameSr, f => f.Name.Random.ToString())
                .RuleFor(c => c.IsDeleted, false);

            var rez = actionrootgeneration.Generate(99);

            rez.Add(new ActionRoot { Id = Id, NameAl = "teeterter", NameEn = "teeterter", NameSr = "teeterter", CodeName = "Action" });
            return rez;

        }
        public static IQueryable<ActionRoot> GetActionRootsIQ()
        {
            var actonRoots = GetActionRoots();
            return actonRoots.AsQueryable();
        }
        public static Mock<IRepository<ActionRoot>> GetAllIQ()
        {
            var actionResults = GetActionRoots();
            var actionRootsMockSet = GetQueryableMockDbSet<ActionRoot>(actionResults.ToArray());

            var Mock = new Mock<IRepository<ActionRoot>>();

            Mock.Setup(r => r.GetAsQueryable(It.IsAny<Expression<Func<ActionRoot, bool>>>(),
                                                            It.IsAny<Func<IQueryable<ActionRoot>, IOrderedQueryable<ActionRoot>>>(),
                                                            It.IsAny<string[]>(),
                                                            It.IsAny<bool>())).Returns(actionRootsMockSet);

            return Mock;
            
        }
        public static Mock<IRepository<ActionRoot>> GetAll()
        {
            var actionResults = GetActionRoots().ToList(); 
            var Mock = new Mock<IRepository<ActionRoot>>();

            Mock.Setup(r => r.GetAll()).Returns(actionResults);

            return Mock;
            
        }

        public static Mock<IRepository<ActionRoot>> GetById(Guid Id)
        {
            var actionRoot = GetActionRoots().FirstOrDefault(c => c.Id == Id);

            var actonRootmoke = new Mock<IRepository<ActionRoot>>();
            actonRootmoke.Setup(r => r.GetById(Id.ToString())).Returns(actionRoot);

            return actonRootmoke;

        }
        public static Mock<IRepository<ActionRoot>> GetById(string Id)
        {
            var actionRoot = GetActionRoots().FirstOrDefault(c => c.Id.ToString() == Id);

            var actonRootmoke = new Mock<IRepository<ActionRoot>>();
            actonRootmoke.Setup(r => r.GetById(Id.ToString())).Returns(actionRoot);

            return actonRootmoke;

        }

      
    }

}
