using Data;
using Moq;
using NUnit.Framework;
using Repository;
using Services.Documents;
using ServicesTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesTest.Documents
{
    public class DocumentsTests
    {
        private Mock<IRepository<Data.Document>> _mockGetByIdRepo;
        private Mock<IRepository<Data.Document>> _mockGetAllRepo;
        private Mock<IRepository<Data.Document>> _mockGetAllAsQuerableRepo;
        private Mock<IRepository<Data.Document>> _insert;
        private Mock<IRepository<Data.Document>> _update;
        //private Mock<IRepository<Data.Document>> _delete;
      
        private Guid RootId = Guid.Parse("cc78beb4-9458-46b5-b28d-1db93ab5142d");
        private Guid Id = Guid.Parse("cc78beb4-9458-46b5-b28d-1db93ab5142d");

        [SetUp]
        public void Setup()
        {
          
            _mockGetByIdRepo = DocumentsMocks.GetById(Id);
            _mockGetAllRepo = DocumentsMocks.GetAll();
            _mockGetAllAsQuerableRepo = DocumentsMocks.GetAllAsQuerable("Document2", RootId);
            _insert = DocumentsMocks.Insert();
            _update = DocumentsMocks.Update(getData().FirstOrDefault());
        }

        public static List<Document> getData()
        {
            var documentLists = new List<Document>
            {
                new Document {
                    Id = Guid.Parse("cc78beb4-9458-46b5-b28d-1db93ab5142d"),
                    Path = "/Document/Doc1",
                    DocumentName = "Document1",
                    Date =  DateTime.Now.AddDays(-5),
                    },
                new Document {
                Id = Guid.Parse("A41D64A0-E2B0-4B6D-BA89-08D9266D2AF5"),
                Path = "/Document/Doc2",
                DocumentName = "Document2",
                Date = DateTime.Now.AddDays(-6),

                },
                new Document {
                Id = Guid.Parse("cc78beb4-9458-46b5-b28d-1db93ab5142d"),
                Path = "/Document/LawDoc",
                DocumentName = "LawDoc",
                Date = DateTime.Now.AddDays(-5),
                 }
             };
            return documentLists;
        }

        [SetUp]
        public void GetAll()
        {
            var handler = new DocumentService(_mockGetAllRepo.Object);
            var result = handler.GetAll();
            Assert.AreEqual(result.Count, 3);
        }

        [Test]
        public void GetAllAsQuerable()
        {
           
            var handler = new DocumentService(_mockGetAllAsQuerableRepo.Object);
            var result = handler.GetAllAsQuerable("Document1", RootId);
            Assert.IsTrue(result.ToList().Count > 1);
        }

        [Test]
        public void GetById()
        {
            var handler = new DocumentService(_mockGetByIdRepo.Object);
            var result = handler.GetById(Id);
            Assert.AreEqual(result.Id, Id);

        }

        [Test]
        public void Insert()
        {
            // Arrange
            var handler = new DocumentService(_insert.Object);
            var newItem = new Data.Document
            {
                Id = Guid.Parse("cc78beb4-9458-46b5-b28d-1db93ab5142d"),
                Path = "",
                DocumentName = "LawDoc",
                Date = DateTime.Now.AddDays(-5),
            };
            // Act
            handler.Insert(newItem);

            // Assert
            Assert.IsTrue(handler.GetAll().ToList().Count() > 1);
        }

        [Test]
        public void Update()
        {
           var handler = new DocumentService(_update.Object);
            var toUpdate = getData().FirstOrDefault();
            // Act
            handler.Update(toUpdate);
            var test = handler.GetById(toUpdate.Id);
            // Assert
            Assert.IsTrue(handler.GetById(toUpdate.Id).Path == "/Document/Doc1");
            Assert.IsTrue(handler.GetById(toUpdate.Id).DocumentName == "Document1");
        }

        //[Test]
        //public void Delete()
        //{
        //    // Arrange
        //    var handler = new DocumentService(_delete.Object);

        //    var toDelete = new Document
        //    {
        //        Id = Guid.Parse("cc78beb4-9458-46b5-b28d-1db93ab5142d"),
        //        Path = "",
        //        DocumentName = "bs",
        //        Date = DateTime.Now.AddDays(-5),
        //    };
        //    // Act
        //    handler.Delete(toDelete);
        //    // Assert
        //    var result = handler.GetById(toDelete.Id);
        //    Assert.IsTrue(result.IsDeleted == true);
        //    Assert.IsTrue(result.DeletedDate != null);
        //}
    }
}
