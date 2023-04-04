using Data;
using Moq;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ServicesTest.Mocks
{
    //class DocumentsMocks
    //{

    //}
    public class DocumentsMocks
    {
        public static List<Document> getData()
        {
            var lists = new List<Document>
            {
                new Document {
                    Id = Guid.Parse("cc78beb4-9458-46b5-b28d-1db93ab5142d"),
                    Path = "/Document/Doc1",
                    DocumentName = "Document1",
                    Date =  DateTime.Now.AddDays(-5),
                    RootId = Guid.Parse("cc78beb4-9458-46b5-b28d-1db93ab5142d")
                    },
                 new Document {
                   Id =  Guid.Parse("A41D64A0-E2B0-4B6D-BA89-08D9266D2AF5"),
                    Path = "/Document/Doc1",
                    DocumentName = "Document1",
                    Date = DateTime.Now.AddDays(-6),
                    RootId = Guid.Parse("cc78beb4-9458-46b5-b28d-1db93ab5142d")
                    },
                  new Document {
                  Id =  Guid.Parse("ED9FBFB3-379F-4C42-4461-08D9266F84FE"),
                    Path = "/Document/Doc3",
                    DocumentName = "Document3",
                    Date = DateTime.Now.AddDays(-7),
                    RootId = Guid.Parse("ED9FBFB3-379F-4C42-4461-08D9266F84FE")

                    }

                };

            return lists;
        }

        public static Mock<IRepository<Document>> GetAll()
        {
            var documents = getData();
            var documentsmock = new Mock<IRepository<Document>>();
            documentsmock.Setup(repo => repo.GetAll()).Returns(documents);
            return documentsmock;
        }

        public static Mock<IRepository<Document>> GetById(Guid id)
        {
            var documents = getData();
            var documentsmock = new Mock<IRepository<Document>>();
            var firstItem = documents.FirstOrDefault(x => x.Id == id);
            documentsmock.Setup(repo => repo.GetById(It.IsAny<string>())).Returns(firstItem);
            return documentsmock;
        }
        public static Mock<IRepository<Document>> Insert()
        {
            var documents = getData();
            var documentsmock = new Mock<IRepository<Document>>();
            documentsmock.Setup(repo => repo.Insert(It.IsAny<Document>())).Callback((Document document) =>
            {
                document.Id = Guid.NewGuid();
                documents.Add(document);
            }).Verifiable();

            documentsmock.Setup(repo => repo.GetAll()).Returns(documents);
            return documentsmock;
        }
        public static Mock<IRepository<Document>> Update(Document item)
        {
            var documents = getData().FirstOrDefault();
            var documentsmock = new Mock<IRepository<Document>>();

            documentsmock.Setup(repo => repo.Update(It.IsAny<Document>())).Callback((Document document) =>
            {
                documents.DocumentName = item.DocumentName;
                documents.Path = item.Path;
            }).Verifiable();

            documentsmock.Setup(repo => repo.GetById(documents.Id.ToString())).Returns(documents);
            return documentsmock;
        }
        public static Mock<IRepository<Document>> GetAllAsQuerable(string Document, Guid? RootId)
        {

            var list = getData().Where(x => x.RootId == RootId);
            var documents = list.AsQueryable();
            var documentsmock = new Mock<IRepository<Document>>();
            var includes = new string[] { "Type.ListTranslations", "Access.ListTranslations", "IssuingAuthority.ListTranslations" };
            documentsmock.Setup(repo => repo.GetAsQueryable(x => x.RootId == RootId, null, includes, false)).Returns(documents);
            return documentsmock;
        }
        public static Mock<IRepository<Document>> Delete()
        {
            var documents = getData().FirstOrDefault();
            var documentsmock = new Mock<IRepository<Document>>();

            documentsmock.Setup(repo => repo.Delete(It.IsAny<Document>())).Callback((Document list) =>
            {
                documents.IsDeleted = true;
                documents.DeletedDate = DateTime.Now;
            }).Verifiable();

            documentsmock.Setup(repo => repo.GetById(documents.Id.ToString())).Returns(documents);
            return documentsmock;
        }


    }
}
