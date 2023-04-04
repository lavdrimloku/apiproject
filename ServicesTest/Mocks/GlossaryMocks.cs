using Data;
using Moq;
using Repository;
using ServicesTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesTest.Mocks
{
    public class GlossaryMocks : QueryableDbSetMock
    {

        public static List<Glossary> getData()
        {
            var glossy = new List<Glossary>
            {
                new Glossary {
                  Id = Guid.Parse("E427FC54-A995-4696-5BD9-08DA43C371F5"),
                    Title = "Ligji",
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
        public static Mock<IRepository<Glossary>> GetAll()
        {
            var GlossaryList = getData();
            var glossarymock = new Mock<IRepository<Glossary>>();
            glossarymock.Setup(repo => repo.GetAll()).Returns(GlossaryList);
            return glossarymock;
        }

        public static Mock<IRepository<Glossary>> GetById(Guid id)
        {
            var GlossaryList = getData();
            var glossarymock = new Mock<IRepository<Glossary>>();
            var firstItem = GlossaryList.FirstOrDefault(x => x.Id == id);
            //nodelistmock.Setup(repo => repo.GetSingle(null,null,null)).Returns(firstItem);
            glossarymock.Setup(repo => repo.GetById(It.IsAny<string>())).Returns(firstItem);
            return glossarymock;
        }



        public static Mock<IRepository<Glossary>> Insert()
        {
            var GlossaryList = getData();

            var glossarymock = new Mock<IRepository<Glossary>>();
            glossarymock.Setup(repo => repo.Insert(It.IsAny<Glossary>())).Callback((Glossary glossary) =>
            {
                glossary.Id = Guid.NewGuid();
                GlossaryList.Add(glossary);
            }).Verifiable();

            glossarymock.Setup(repo => repo.GetAll()).Returns(GlossaryList);
            return glossarymock;
        }

        public static Mock<IRepository<Glossary>> Update(Glossary item)
        {
            //var GlossaryList = getData().FirstOrDefault();
            var glossarymock = new Mock<IRepository<Glossary>>();

            glossarymock.Setup(repo => repo.Update(It.IsAny<Glossary>())).Callback((Glossary glossary) =>
            {
                glossary.Title = item.Title;
                glossary.LanguageId = item.LanguageId;
            }).Verifiable();

            glossarymock.Setup(repo => repo.GetById(item.Id.ToString())).Returns(item);
            return glossarymock;
        }

        public static Mock<IRepository<Glossary>> Delete()
        {
            var lists = getData().FirstOrDefault();
            var listmock = new Mock<IRepository<Glossary>>();

            listmock.Setup(repo => repo.Delete(It.IsAny<Glossary>())).Callback((Glossary list) =>
            {
                lists.IsDeleted = true;
                lists.DeletedDate = DateTime.Now;
            }).Verifiable();

            listmock.Setup(repo => repo.GetById(lists.Id.ToString())).Returns(lists);
            return listmock;
        }


        public static Mock<IRepository<Glossary>> GetByPrefix(String pre)
        {
            var GlossaryList = getData().Where(x => x.Title.Contains(pre)).AsQueryable();
            var glossarymock = new Mock<IRepository<Glossary>>();
            glossarymock.Setup(repo => repo.GetAsQueryable(x=>x.Title.StartsWith(pre), null, null, false)).Returns(GlossaryList);

            return glossarymock;
        }

    }
}
