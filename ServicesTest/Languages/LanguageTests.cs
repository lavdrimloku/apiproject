using Repository;
using Services.Languages;
using ServicesTest.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using List = Data.List;
using NUnit.Framework;
using Moq;
using Data;

namespace ServicesTest.Languages
{
    public class LanguagesTests
    {
        private Mock<IRepository<Data.Language>> _mockGetByIdRepo;
        private Mock<IRepository<Data.Language>> _mockGetAllRepo;
        private Mock<IRepository<Data.Language>> _mockGetAllAsQuerableRepo;
        private Mock<IRepository<Data.Language>> _mockGetAllActiveRepo;
        private Mock<IRepository<Data.Language>> _mockGetByIdsRepo;

        private Mock<IRepository<Data.Language>> _insert;
        private Mock<IRepository<Data.Language>> _update;
        private List<int> ids = new List<int> { 1 };
        [SetUp]
        public void Setup()
        {
            _mockGetByIdRepo = LanguagesMocks.GetById(1);
            _mockGetAllRepo = LanguagesMocks.GetAll();
            _mockGetAllAsQuerableRepo = LanguagesMocks.GetAllAsQuerable("Bosnian", "bs", true);
            _mockGetAllActiveRepo = LanguagesMocks.GetAllActive();
            _mockGetByIdsRepo = LanguagesMocks.GetByIds(ids);
            _insert = LanguagesMocks.Insert();
            _update = LanguagesMocks.Update(getData().FirstOrDefault());
        }

        [Test]
        public void GetAll()
        {
            var handler = new LanguageService(_mockGetAllRepo.Object);
            var result = handler.GetAll();
            Assert.AreEqual(result.Count, 3);
        }

        [Test]
        public void GetById()
        {
            var handler = new LanguageService(_mockGetByIdRepo.Object);
            var result = handler.GetById(1);
            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void Insert()
        {
            // Arrange
            var handler = new LanguageService(_insert.Object);
            var newItem = new Data.Language
            {
                Id = 4,
                Name = "Bosnian",
                Code = "sq",
                Active = true,
                IsDeleted = false

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
            var handler = new LanguageService(_update.Object);

            var toUpdate = new Data.Language
            {

                Id = 1,
                Name = "Bosnian",
                Code = "bs",
                Active = false,
                IsDeleted = false
            };

            // Act
            handler.Update(toUpdate);
            var test = handler.GetById(toUpdate.Id);
            // Assert
            Assert.IsTrue(handler.GetById(toUpdate.Id).Active == false);
            Assert.IsTrue(handler.GetById(toUpdate.Id).Id == 1);
        }


        [Test]
        public void GetAllAsQuerable()
        {
            var handler = new LanguageService(_mockGetAllAsQuerableRepo.Object);
            var result = handler.GetAllAsQuerable("Bosnian","bs",true);
            Assert.IsTrue(result.ToList().Count > 0);
        }

        [Test]
        public void GetByIds()
        {
            var handler = new LanguageService(_mockGetByIdsRepo.Object);
            var result = handler.GetAllByIds(ids);
            Assert.AreEqual(result.Count, 2);
        }

        [Test]
        public void GetAllActive()
        {
            var handler = new LanguageService(_mockGetAllActiveRepo.Object);
            var result = handler.GetAllActive();
            Assert.AreEqual(result.Count,2);
        }

       
        public static List<Language> getData()
        {
            var lists = new List<Language>
            {
                new Language {
                    Id = 1,
                    Name = "Bosnian",
                    Code = "bs",
                    Active = true,
                     IsDeleted = false,
                      ListTranslations = new List<ListTranslation>()
                    {
                        new ListTranslation
                        {
                            Id = 4,
                            Name = "Al",
                            LanguageId = 1,
                             Language = new Language{
                                Id = 1,
                                Name = "Bosnian"
                            }
                        }
                    }
                    },
                 new Language {
                    Id = 2,
                    Name = "English",
                    Code = "en",
                    Active = true,
                      IsDeleted = false,
                       ListTranslations = new List<ListTranslation>()
                    {
                        new ListTranslation
                        {
                            Id = 2,
                            Name = "English",
                            LanguageId = 2,
                             Language = new Language{
                                Id = 2,
                                Name = "English"
                            }
                        }
                    }
                    },
                  new Language {
                    Id = 3,
                    Name = "Shqip",
                    Code = "sq",
                    Active = false,
                      IsDeleted = false,
                       ListTranslations = new List<ListTranslation>()
                    {
                        new ListTranslation
                        {
                            Id = 3,
                            Name = "Shqip",
                            LanguageId = 3,
                             Language = new Language{
                                Id = 3,
                                Name = "Shqip"
                            }
                        }
                    }
                    },
                    

                };
            return lists;
        }
    }
}