using Data;
using Moq;
using NUnit.Framework;
using Repository;
using Services.Lists;
using ServicesTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesTest.Lists
{
    public class ListTranslationTests
    {
        private Mock<IRepository<Data.ListTranslation>> _getAll;
        private Mock<IRepository<Data.ListTranslation>> _getByid;
        private Mock<IRepository<Data.ListTranslation>> _getByListId;
        private Mock<IRepository<Data.ListTranslation>> _getListTranslationByListId;
        private Mock<IRepository<Data.ListTranslation>> _insert;
        private Mock<IRepository<Data.ListTranslation>> _translationExists;
        private Mock<IRepository<Data.ListTranslation>> _update;
        private Mock<IRepository<Data.ListTranslation>> _delete;
        private int ListId = 1;
        private int LangId = 1;
        private int ItemId = 1;

        [SetUp]
        public void Setup()
        {
            _getAll = ListTranslationMocks.GetAll();
            _getByid = ListTranslationMocks.GetById(ItemId);
            _getByListId = ListTranslationMocks.GetByListId(ListId, LangId);
            _getListTranslationByListId = ListTranslationMocks.GetListTranslationByListId(ListId);
            _insert = ListTranslationMocks.Insert();
            _translationExists = ListTranslationMocks.TranslationExists(ListId, LangId);
            _update = ListTranslationMocks.Update(getData().FirstOrDefault());
            _delete = ListTranslationMocks.Delete();
        }

        [Test]
        public void GetAllTest()
        {
            var handler = new ListTranslationService(_getAll.Object);
            var result = handler.GetAll();
            Assert.AreEqual(result.Count, 3);
        }


        [Test]
        public void GetById()
        {
            var handler = new ListTranslationService(_getByid.Object);
            var result = handler.GetById(1);
            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void GetByListId()
        {
            var handler = new ListTranslationService(_getByListId.Object);
            var result = handler.GetByListId(ListId, LangId);
            Assert.AreEqual(result.Id, 1);
        }

        [Test]
        public void GetListTranslationByListId()
        {
            var handler = new ListTranslationService(_getListTranslationByListId.Object);
            var result = handler.GetListTranslationByListId(ListId);
            Assert.AreEqual(result.Count, 2);
        }


        [Test]
        public void Insert()
        {
            // Arrange
            var handler = new ListTranslationService(_insert.Object);
            var newItem = new Data.ListTranslation
            {
                    Id = 4,
                    Name= "Translation 4",
                    ListId = 1,
                    LanguageId = 1
            };

            // Act
            handler.Insert(newItem);

            // Assert
            Assert.IsTrue(handler.GetAll().ToList().Count > 3);
        }

        [Test]
        public void TranslationExists()
        {
            var handler = new ListTranslationService(_translationExists.Object);
            var result = handler.TranslationExists(ListId, LangId);
            Assert.AreEqual(result.Id, 1);
            Assert.AreEqual(result.IsDeleted, false);
        }
        

        [Test]
        public void Update()
        {
            // Arrange
            var handler = new ListTranslationService(_update.Object);

            var toUpdate = getData().FirstOrDefault();

            // Act
            handler.Update(toUpdate);
            var test = handler.GetById(toUpdate.Id);
            // Assert
            Assert.IsTrue(handler.GetById(toUpdate.Id).Name == "Translation 111111");
            Assert.IsTrue(handler.GetById(toUpdate.Id).ListId == toUpdate.ListId);
        }


        [Test]
        public void Delete()
        {
            // Arrange
            var handler = new ListTranslationService(_delete.Object);

            var toDelete = new ListTranslation
            {
                Id = 1,
                Name = "Translation 111111",
                ListId = 2,
                LanguageId = 1,
                IsDeleted = false,
                DeletedDate = null
            };

            // Act
            handler.Delete(toDelete);
            // Assert
            var result = handler.GetById(toDelete.Id);
            Assert.IsTrue(result.IsDeleted == true);
            Assert.IsTrue(result.DeletedDate != null);
        }

        public static List<ListTranslation> getData()
        {
            var returnedList = new List<ListTranslation>()
            {
                new ListTranslation
                {
                    Id = 1,
                    Name= "Translation 111111",
                    ListId = 2,
                    LanguageId = 1
                },
                new ListTranslation
                {
                    Id = 2,
                    Name= "Translation 2",
                    ListId = 1,
                    LanguageId = 2
                },
                new ListTranslation
                {
                    Id = 3,
                    Name= "Translation 3",
                    ListId = 2,
                    LanguageId = 3
                }
            };

            return returnedList;
        }
    }
}
