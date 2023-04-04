using Data;
using Moq;
using NUnit.Framework;
using Repository;
using Services.Lists;
using ServicesTest.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Linq;
using List = Data.List;

namespace ServicesTest.Lists
{
    public class ListTests
    {
        private Mock<IRepository<Data.List>> _GetAllNodeTypes;
        private Mock<IRepository<Data.List>> _insert;
        private Mock<IRepository<Data.List>> _update;
        private Mock<IRepository<Data.List>> _updateList;
        private Mock<IRepository<Data.List>> _mockListRepo;
        private Mock<IRepository<Data.List>> _mockGetOneRepo;
        private Mock<IRepository<Data.List>> _mockGetByIdsRepo;
        private Mock<IRepository<Data.List>> _GetListsByListTypeId;
        private List<int> ids = new List<int> { 1, 2, 3 };

        [SetUp]
        public void Setup()
        {
            _mockListRepo = ListMocks.GetAll();
            _mockGetOneRepo = ListMocks.GetById(1);
            _mockGetByIdsRepo = ListMocks.GetByIds(ids);
            _GetListsByListTypeId = ListMocks.GetListsByListTypeId(1);
            _GetAllNodeTypes = ListMocks.GetAllNodeTypes();
            _insert = ListMocks.Insert();
            _update = ListMocks.Update(getData().FirstOrDefault());
            _updateList = ListMocks.UpdateList(getData());
        }

        [Test]
        public void GetListsTest()
        {
            var handler = new ListService(_mockListRepo.Object);
            var result = handler.GetAll();
            Assert.AreEqual(result.Count, 3);

            Assert.AreEqual(result.FirstOrDefault().Id, 1);
        }

        [Test]
        public void GetById()
        {
            var handler = new ListService(_mockGetOneRepo.Object);
            var result = handler.GetById(1);
            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void GetByIds()
        {
            var handler = new ListService(_mockGetByIdsRepo.Object);
            var result = handler.GetByIds(ids);
            Assert.AreEqual(result.Count, 3);
        }

        [Test]
        public void GetListsByListTypeId()
        {
            var handler = new ListService(_GetListsByListTypeId.Object);
            var result = handler.GetListsByListTypeId(1);
            Assert.IsTrue(result.ToList().Count > 0);
        }

        [Test]
        public void GetAllNodeTypes()
        {
            var handler = new ListService(_GetAllNodeTypes.Object);
            var result = handler.GetAllNoteTypes();
            Assert.IsTrue(result.ToList().Count > 0);
        }

        [Test]
        public void Insert()
        {
            // Arrange
            var handler = new ListService(_insert.Object);
            var newItem = new Data.List
            {
                Id = 4,
                HasRTF = false,
                HasValidity = true,
                IsActive = true,
                OrderIndex = 4,
                CanLinkToNodes = false,
                ListTypeId = 2,
                IsDeleted = false,
                ListType = new ListType
                {
                    Id = 4,
                    Name = "Type 3"
                },
                ListTranslations = new List<ListTranslation>()
                    {
                        new ListTranslation
                        {
                            Id = 4,
                            Name = "Al",
                            LanguageId = 1,
                             Language = new Language{
                                Id = 1,
                                Name = "al"
                            }
                        }
                    }
            };

            // Act
            handler.Insert(newItem);

            // Assert
            Assert.IsTrue(handler.GetAll().ToList().Count > 3);
        }


        [Test]
        public void Update()
        {
            // Arrange
            var handler = new ListService(_update.Object);

            var toUpdate = new Data.List
            {
                Id = 1,
                HasRTF = false,
                HasValidity = true,
                IsActive = false,
                OrderIndex = 10,
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
            };

            // Act
            handler.Update(toUpdate);
            var test = handler.GetById(toUpdate.Id);
            // Assert
            Assert.IsTrue(handler.GetById(toUpdate.Id).IsActive == false);
            Assert.IsTrue(handler.GetById(toUpdate.Id).OrderIndex == 10);
        }


        [Test]
        public void UpdateList()
        {
            // Arrange
            var handler = new ListService(_updateList.Object);
            var itemToUpdate = getData();

            // Act
            handler.UpdateList(itemToUpdate);
            var itemUpdated = handler.GetAll();

            // Assert
            for(int i = 1; i <= itemUpdated.Count; i++)
            {
                Assert.IsTrue(itemUpdated.FirstOrDefault(x=>x.Id == i).OrderIndex == itemToUpdate.FirstOrDefault(x => x.Id == i).OrderIndex);
                Assert.IsTrue(itemUpdated.FirstOrDefault(x=>x.Id == i).IsActive == itemToUpdate.FirstOrDefault(x => x.Id == i).IsActive);
            }
            
        }




        public static List<List> getData()
        {
            var lists = new List<List>
            {
                new List{
                    Id = 1,
                    HasRTF = false,
                    HasValidity = true,
                    IsActive = false,
                    OrderIndex = 10,
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
                    IsActive = false,
                    OrderIndex =11,
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
                    IsActive = false,
                    OrderIndex = 12,
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
    }
}
