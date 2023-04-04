using Data;
using Moq;
using NUnit.Framework;
using Repository;
using Services.Glossaries;
using Services.Lists;
using ServicesTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesTest.Glossaries
{
    public class GlossaryTest
    {

        private Mock<IRepository<Data.Glossary>> _getAll;
        private Mock<IRepository<Data.Glossary>> _insert;
        private Mock<IRepository<Data.Glossary>> _update;
        private Mock<IRepository<Data.Glossary>> _getById;
        private Mock<IRepository<Data.Glossary>> _getByPrefix;
        private Mock<IRepository<Data.Glossary>> _delete;

        private Guid Id = Guid.Parse("E427FC54-A995-4696-5BD9-08DA43C371F5");
        private string pre = "Li";

        [SetUp]
        public void Setup()
        {
            _getAll = GlossaryMocks.GetAll();
            _getById = GlossaryMocks.GetById(Id);
            _getByPrefix = GlossaryMocks.GetByPrefix(pre);
            _delete = GlossaryMocks.Delete();
            _insert = GlossaryMocks.Insert();
            _update = GlossaryMocks.Update(getData().FirstOrDefault());
        }

        [Test]
        public void GetAll()
        {
            var handler = new GlossaryService(_getAll.Object);
            var result = handler.GetAll();
            Assert.AreEqual(result.Count, 3);
        }


        [Test]
        public void GetById()
        {
            var handler = new GlossaryService(_getById.Object);
            var result = handler.GetById(Id);
            Assert.AreEqual(result.Id, Id);
        }
        
        [Test]
        public void GetByPrefix()
        {
            var handler = new GlossaryService(_getByPrefix.Object);
            var result = handler.GetByPrefix(pre);
            Assert.AreEqual(result.Count, 3);
        }


        [Test]
        public void Insert()
        {
            // Arrange
            var handler = new GlossaryService(_insert.Object);
            var newItem = new Data.Glossary
            {
                Id = Guid.Parse("9527CFC5-A089-4DA5-E55C-08D8FA6BC922"),
                Title = "Testi1",
                LanguageId = 1,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };
            // Act
            handler.Insert(newItem);

            // Assert
            Assert.IsTrue(handler.GetAll().ToList().Count() > 0);
        }



        [Test]
        public void Update()
        {
            // Arrange
            var handler = new GlossaryService(_update.Object);

            var itemToUpdate = getData().FirstOrDefault();


            var toUpdate =  new Glossary
            {
                Id = Guid.Parse("9523cfc5-a089-4da5-e55c-08d8fa6bc922"),
                Title = "Ligji i ndryshuar",
                IsDeleted = false,
                LanguageId = 2,
            };


                handler.Update(toUpdate);
            var itemUpdated = handler.GetById(toUpdate.Id);


            Assert.IsTrue(itemUpdated.Title == toUpdate.Title);
        }

        [Test]
        public void Delete()
        {
            // Arrange
            var handler = new GlossaryService(_delete.Object);

            var toDelete = new Glossary
            {
                Id = Guid.Parse("E427FC54-A995-4696-5BD9-08DA43C371F5"),
                Title = "Ligji",
                LanguageId = 1
            };

            // Act
            handler.Delete(toDelete);
            // Assert
            var result = handler.GetById(toDelete.Id);
            Assert.IsTrue(result.IsDeleted == true);
            Assert.IsTrue(result.DeletedDate != null);
        }



        public static List<Glossary> getData()
        {
            var glossy = new List<Glossary>
            {
                new Glossary {
                  Id = Guid.Parse("9523cfc5-a089-4da5-e55c-08d8fa6bc922"),
                    Title = "Ligjiet",
                    IsDeleted = false,
                    LanguageId = 1,


                },
                  new Glossary {
                  Id = Guid.Parse("E427FC54-A995-4696-5BD9-08DA243C71F5"),
                    Title = "Ligji222",
                    IsDeleted = false,
                    LanguageId = 1,


                },
                    new Glossary {
                  Id = Guid.Parse("E427FC54-A995-4696-5BD9-08DA433371F5"),
                    Title = "Ligjiaa",
                    IsDeleted = false,
                    LanguageId = 1,


                }

            };
            return glossy;
        }
    }

}




