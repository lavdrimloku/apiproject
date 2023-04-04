using Data;
using Moq;
using NUnit.Framework;
using Repository;
using Services.Localizations;
using ServicesTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesTest.Localizations
{
    public class LocalizationsTests
    {
        private Mock<IRepository<Data.Localization>> _mockGetByIdRepo;
        private Mock<IRepository<Data.Localization>> _mockGetAllRepo;
        private Mock<IRepository<Data.Localization>> _mockGetAllAsQuerableRepo;
      
        private Mock<IRepository<Data.Localization>> _mockGetAllWithParamatersRepo;

        private Mock<IRepository<Data.Localization>> _insert;
        private Mock<IRepository<Data.Localization>> _update;
        [SetUp]
        public void Setup()
        {
            _mockGetByIdRepo = LocalizationsMocks.GetById(1);
            _mockGetAllRepo = LocalizationsMocks.GetAll();
            _mockGetAllAsQuerableRepo = LocalizationsMocks.GetAllAsQuerable("from", 1);

            _mockGetAllWithParamatersRepo = LocalizationsMocks.GetAllWithParamaters(1);
            _insert = LocalizationsMocks.Insert();
            _update = LocalizationsMocks.Update(getData().FirstOrDefault());
        }

        [Test]
        public void GetAll()
        {
            var handler = new LocalizationsService(_mockGetAllRepo.Object);
            var result = handler.GetAll();
            Assert.AreEqual(result.Count, 0);
            //Assert.AreEqual(result.Count, 3);

            //Assert.AreEqual(result.FirstOrDefault().Id, 1);
        }

        [Test]
        public void GetById()
        {
           
            var handler = new LocalizationsService(_mockGetByIdRepo.Object);
            var result = handler.GetById(1);
            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void Insert()
        {
            // Arrange
            var handler = new LocalizationsService(_insert.Object);
            var newItem = new Data.Localization
            {
                Id = 4,
                ResourceKey = "Layout1",
                KeyName = "from",
                KeyValue = "from",
                Language = new Language
                {
                    Id = 2,
                    Name = "English"
                }

            };
            // Act
            handler.Insert(newItem);

            // Assert
            Assert.IsTrue(handler.GetAll().ToList().Count < 3);

        }

        [Test]
        public void Update()
        {
            // Arrange
            var handler = new LocalizationsService(_update.Object);

            var toUpdate = new Data.Localization
            {
                Id = 1,
                ResourceKey = "Layout",
                KeyName = "from",
                KeyValue = "from",
                Language = new Language
                {
                    Id = 2,
                    Name = "English"
                },
                IsDeleted = false
            };

            // Act
            handler.Update(toUpdate);
            var test = handler.GetById(toUpdate.Id);
            // Assert
            Assert.IsTrue(handler.GetById(toUpdate.Id).Id == 1);
            Assert.IsTrue(handler.GetById(toUpdate.Id).ResourceKey == "Layout");
        }
        [Test]
        public void GetAllAsQuerable()
        {
            var handler = new LocalizationsService(_mockGetAllAsQuerableRepo.Object);
           //var result = handler.GetAllAsQuerable("from", 1);
           // Assert.IsTrue(result.ToList().Count < 3);
        }

        [Test]
        public void GetAllWithParamaters()
        {
            var handler = new LocalizationsService(_mockGetAllWithParamatersRepo.Object);
            var result = handler.GetAllWithParamaters(1);
            Assert.IsTrue(result.ToList().Count < 3);
        }
        public static List<Localization> getData()
        {
            var lists = new List<Localization>
            {
                new Localization {
                    Id = 1,
                    ResourceKey = "Layout",
                    KeyName = "from",
                    KeyValue = "from",
                      Language = new Language{
                                Id = 2,
                                Name = "English"
                            }
                    },
                 new Localization {
                    Id = 2,
                    ResourceKey = "Layout",
                    KeyName = "from",
                    KeyValue = "prej",
                    Language = new Language{
                                Id = 1,
                                Name = "Albanian"
                            }
                    },

                  new Localization {
                    Id = 3,
                    ResourceKey = "Layout",
                    KeyName = "from",
                    KeyValue = "de",
                    Language = new Language{
                                Id = 2,
                                Name = "French"
                            }


      }
                    };
                   

            return lists;
        }
    }
}
