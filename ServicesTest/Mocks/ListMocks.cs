using Data;
using Moq;
using Repository;
using Services.Enum;
using Services.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesTest.Mocks
{
    public class ListMocks
    {
 
        public static List<List> getData()
        {
            var lists = new List<List>
            {
                new List {
                    Id = 1,
                    HasRTF = false,
                    HasValidity = true,
                    IsActive = true,
                    OrderIndex = 1,
                    CanLinkToNodes = false,
                    ListTypeId = 1,
                    IsDeleted = false,
                    ListType = new ListType
                    {
                        Id = 1,
                        Name = "Type 1",
                        IsDeleted = false
                    },
                    ListTranslations = new List<ListTranslation>()
                    {
                        new ListTranslation
                        {
                            Id = 1,
                            Name = "Al",
                            LanguageId = 1,
                            IsDeleted = false,
                            Language = new Language {
                                Id = 1,
                                Name = "al",
                                  IsDeleted = false
                            }
                        }
                    }
                    
                },
                new List{
                    Id = 2,
                    HasRTF = false,
                    HasValidity = true,
                    IsActive = true,
                    OrderIndex = 2,
                    CanLinkToNodes = false,
                    ListTypeId = 1,
                        IsDeleted = false,
                    ListType = new ListType
                    {
                        Id = 2,
                        Name = "Type 2",
                          IsDeleted = false
                    },
                    ListTranslations = new List<ListTranslation>()
                    {
                        new ListTranslation
                        {
                            Id = 2,
                            Name = "Al",
                            LanguageId = 1,
                              IsDeleted = false,
                             Language = new Language{
                                Id = 1,
                                Name = "al",
                                  IsDeleted = false
                            }
                        }
                    }
                },
                new List{
                    Id = 3,
                    HasRTF = false,
                    HasValidity = true,
                    IsActive = true,
                    OrderIndex = 3,
                    CanLinkToNodes = false,
                    ListTypeId = 2,
                    IsDeleted = false,
                    ListType = new ListType
                    {
                        Id = 3,
                        Name = "Type 3"
                    },
                    ListTranslations = new List<ListTranslation>()
                    {
                        new ListTranslation
                        {
                            Id = 3,
                            Name = "Al",
                            LanguageId = 1,
                             Language = new Language{
                                Id = 1,
                                Name = "al"
                            }
                        }
                    }
                }
            };
            return lists;
        }

        public static Mock<IRepository<List>> GetAll()
        {
            var lists = getData();
            var listmock = new Mock<IRepository<List>>();
            listmock.Setup(repo => repo.GetAll()).Returns(lists);
            return listmock;
        }

        public static Mock<IRepository<List>> GetById(int id)
        {
            var lists = getData();
            var listmock = new Mock<IRepository<List>>();
            var firstItem = lists.FirstOrDefault(x=>x.Id == id);
            listmock.Setup(repo => repo.Get(id)).Returns(firstItem);
            return listmock;
        }

        public static Mock<IRepository<List>> GetByIds(List<int> Ids)
        {
            var lists = getData();
            var listmock = new Mock<IRepository<List>>();
            listmock.Setup(repo => repo.Get(x => Ids.Contains(x.Id),null, null)).Returns(lists);
            return listmock;
        }

        public static Mock<IRepository<List>> GetListsByListTypeId(int ListTypeId)
        {
            var lists = getData().Where(x=>x.ListTypeId == ListTypeId).AsEnumerable();
            var listmock = new Mock<IRepository<List>>();
            var includes = new string[] { "ListType", "ListTranslations", "ListTranslations.Language" };
            listmock.Setup(repo => repo.Get(x => x.IsActive == true, null, includes)).Returns(lists);
            return listmock;
        }

        public static Mock<IRepository<List>> GetAllNodeTypes()
        {
            var lists = getData().AsQueryable().Where(c => c.ListTypeId == ListTypeEnum.NodeType && c.ListTranslations.Any(x => x.LanguageId == LanguagesEnum.AL));
            var listmock = new Mock<IRepository<List>>();
            string[] includes = new[] { "ListType", "ListTranslations", "ListTranslations.Language" };
            listmock.Setup(repo => repo.GetAsQueryable(null, null, includes, false)).Returns(lists);
            return listmock;
        }

        public static Mock<IRepository<List>> Insert()
        {
            var lists = getData();

            var listmock = new Mock<IRepository<List>>();
            listmock.Setup(repo => repo.Insert(It.IsAny<List>())).Callback((List list) =>
            {
                list.Id = list.Id + 1;
                lists.Add(list);
            }).Verifiable();

            listmock.Setup(repo => repo.GetAll()).Returns(lists);
            return listmock;
        }


        public static Mock<IRepository<List>> Update(List item)
        {
            var lists = getData().FirstOrDefault();
            var listmock = new Mock<IRepository<List>>();

            listmock.Setup(repo => repo.Update(It.IsAny<List>())).Callback((List list) =>
            {
                lists.OrderIndex = item.OrderIndex;
                lists.IsActive = false;
            }).Verifiable();

            listmock.Setup(repo => repo.Get(lists.Id)).Returns(lists);
            return listmock;
        }


        internal static Mock<IRepository<List>> UpdateList(List<List> data)
        {
            var lists = getData();
            var listmock = new Mock<IRepository<List>>();

                listmock.Setup(repo => repo.Update(It.IsAny<List>())).Callback((List list) =>
                {
                    foreach(var item in data)
                    {
                        lists.FirstOrDefault(x => x.Id == item.Id).OrderIndex = item.OrderIndex;
                        lists.FirstOrDefault(x => x.Id == item.Id).IsActive = item.IsActive;
                    }
                }).Verifiable();
            
            listmock.Setup(repo => repo.GetAll()).Returns(lists);
            return listmock;
        }
    }
}
