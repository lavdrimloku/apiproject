using Data;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using Moq;
using Repository;

namespace ServicesTest.Mocks
{
    public class ListTypeMocks
    {
        public static Mock<IRepository<ListType>> GetAll()
        {
            var lists = getData();
            var listmock = new Mock<IRepository<ListType>>();
            listmock.Setup(repo => repo.GetAll()).Returns(lists);
            return listmock;
        }

        public static Mock<IRepository<ListType>> GetListType(int id)
        {
            var lists = getData();
            var listmock = new Mock<IRepository<ListType>>();
            var firstItem = lists[id - 1];
            listmock.Setup(repo => repo.Get(id)).Returns(firstItem);
            return listmock;
        }

        public static Mock<IRepository<ListType>> GetListTypesByIds(List<int?> Ids)
        {
            var lists = getData();
            var listmock = new Mock<IRepository<ListType>>();
            listmock.Setup(repo => repo.GetAsNoTracking(x => Ids.Contains(x.Id), null, null)).Returns(lists);
            return listmock;
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
