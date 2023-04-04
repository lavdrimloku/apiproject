using Data;
using Moq;
using NUnit.Framework;
using Repository;
using Services.Lists;
using ServicesTest.Mocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesTest.Lists
{
    public class ListTypeTests
    {

        private Mock<IRepository<ListType>> _getListTypes;
        private Mock<IRepository<ListType>> _getListType;
        private Mock<IRepository<ListType>> _getListTypesByIds;
        private List<int?> ids = new List<int?> { 1, 2, 3 };

        [SetUp]
        public void Setup()
        {
            _getListTypes = ListTypeMocks.GetAll();
            _getListType = ListTypeMocks.GetListType(1);
            _getListTypesByIds = ListTypeMocks.GetListTypesByIds(ids);
        }


        [Test]
        public void GetListTypesTest()
        {
            var handler = new ListTypeService(_getListTypes.Object);
            var result = handler.GetListTypes();
            Assert.AreEqual(result.Count, 3);
        }

        [Test]
        public void GetListType()
        {
            var handler = new ListTypeService(_getListType.Object);
            var result = handler.GetListType(1);
            Assert.AreEqual(result.Id, 1);
        }


        [Test]
        public void GetByIds()
        {
            var handler = new ListTypeService(_getListTypesByIds.Object);
            var result = handler.GetListTypesByIds(ids);
            Assert.AreEqual(result.Count, 3);
        }

        public static List<ListType> getData()
        {
            var lists = new List<ListType>
            {
                new ListType
                {
                    Id = 1,
                    Name = "Type 1",
                    IsActive = true
                },
                new ListType
                {
                    Id = 2,
                    Name = "Type 2",
                    IsActive = true
                },
                new ListType
                {
                    Id = 3,
                    Name = "Type 3",
                    IsActive = true
                }
            };
            return lists;
        }
    }
}
