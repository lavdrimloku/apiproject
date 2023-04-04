using Data;
using Moq;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesTest.Mocks
{
    public class LanguagesMocks
    {
        public static List<Language> getData()
        {
            var lists = new List<Language>
            {
                new Language {
                    Id = 1,
                    Name = "Bosnian",
                    Code = "bs",
                    Active = true,

                    },
                 new Language {
                    Id = 2,
                    Name = "English",
                    Code = "en",
                    Active = true,

                    },
                  new Language {
                    Id = 3,
                    Name = "Shqip",
                    Code = "sq",
                    Active = false,

                    }

                };
              
            return lists;
        }

        public static Mock<IRepository<Language>> GetAll()
        {
            var languages = getData();
            var languagesmock = new Mock<IRepository<Language>>();
            languagesmock.Setup(repo => repo.GetAll()).Returns(languages);
            return languagesmock;
        }

        public static Mock<IRepository<Language>> GetById(int id)
        {
            var languages = getData();
            var languagesmock = new Mock<IRepository<Language>>();
            var firstItem = languages[id - 1];
            languagesmock.Setup(repo => repo.Get(id)).Returns(firstItem);
            return languagesmock;
        }
        public static Mock<IRepository<Language>> Insert()
        {
            var lists = getData();

            var languagesmock = new Mock<IRepository<Language>>();
            languagesmock.Setup(repo => repo.Insert(It.IsAny<Language>())).Callback((Language language) =>
            {
                language.Id = language.Id + 1;
                lists.Add(language);
            }).Verifiable();

            languagesmock.Setup(repo => repo.GetAll()).Returns(lists);
            return languagesmock;
        }
        public static Mock<IRepository<Language>> Update(Language item)
        {
            var languages = getData().FirstOrDefault();
            var listmock = new Mock<IRepository<Language>>();

            listmock.Setup(repo => repo.Update(It.IsAny<Language>())).Callback((Language list) =>
            {
                languages.Name = item.Name;
                languages.Code = item.Code;
                languages.Active = false;
            }).Verifiable();

            listmock.Setup(repo => repo.Get(languages.Id)).Returns(languages);
            return listmock;
        }
        public static Mock<IRepository<Language>> GetAllAsQuerable(string searchName, string searchCode, bool Active)
        {
            var languages = getData().Where(x => x.Name == searchName || x.Code == searchCode || x.Active == Active).AsQueryable();
            var listmock = new Mock<IRepository<Language>>();
            listmock.Setup(repo => repo.GetAsQueryable(null, null, null, false)).Returns(languages);
            return listmock;
        }
        public static Mock<IRepository<Language>> GetByIds(List<int> Ids)
        {
            var languages = getData().Where(x => !Ids.Contains(x.Id)).ToList().AsEnumerable();
            var languagesmock = new Mock<IRepository<Language>>();
            languagesmock.Setup(repo => repo.GetAsNoTracking(x => !Ids.Contains(x.Id) && x.Active == true, null, null)).Returns(languages);
            return languagesmock;
        }
        public static Mock<IRepository<Language>> GetAllActive()
        {
            var languages = getData().Where(x => x.Active).ToList(); 
            var languagesmock = new Mock<IRepository<Language>>();
            languagesmock.Setup(repo => repo.Get(x=> x.Active, null, null)).Returns(languages);
            return languagesmock;
        }

    }
}
