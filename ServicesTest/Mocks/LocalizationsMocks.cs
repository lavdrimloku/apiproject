using Data;
using Moq;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesTest.Mocks
{
     public class LocalizationsMocks
    {
        public static List<Localization> getData()
        {
            var lists = new List<Localization>
            {
                new Localization {
                    Id = 1,
                    ResourceKey = "Layout",
                    KeyName = "from",
                    KeyValue = "from",
                    LanguageId=1
                    },
                 new Localization {
                    Id = 2,
                    ResourceKey = "Layout",
                    KeyName = "from",
                    KeyValue = "prej",
                    LanguageId=2
                 },
                  new Localization {
                    Id = 3,
                    ResourceKey = "Layout",
                    KeyName = "from",
                    KeyValue = "de",
                    LanguageId=1

                    }

                };

            return lists;
        }
        public static Mock<IRepository<Localization>> GetAll()
        {
    
            var localization = getData();
            var localizationmock = new Mock<IRepository<Localization>>();
            localizationmock.Setup(repo => repo.GetAll()).Returns(localization);
            return localizationmock;
        }

        public static Mock<IRepository<Localization>> GetById(int id)
        {
       
          var localization = getData();
        var localizationmock = new Mock<IRepository<Localization>>();
        var firstItem = localization[id - 1];
        localizationmock.Setup(repo => repo.Get(id)).Returns(firstItem);
            return localizationmock;
        }
        public static Mock<IRepository<Localization>> Insert()
        {
            var localization = getData();

            var localizationmock = new Mock<IRepository<Localization>>();
            localizationmock.Setup(repo => repo.Insert(It.IsAny<Localization>())).Callback((Localization localizations) =>
            {
                localizations.Id = localizations.Id + 1;
                localization.Add(localizations);
            }).Verifiable();

            localizationmock.Setup(repo => repo.GetAll()).Returns(localization);
            return localizationmock;
        }
        public static Mock<IRepository<Localization>> Update(Localization item)
        {
            var localizations = getData().FirstOrDefault();
            var localizationmock = new Mock<IRepository<Localization>>();

            localizationmock.Setup(repo => repo.Update(It.IsAny<Localization>())).Callback((Localization list) =>
            {
                localizations.Id = item.Id;
                localizations.ResourceKey = item.ResourceKey;
            
            }).Verifiable();

            localizationmock.Setup(repo => repo.Get(localizations.Id)).Returns(localizations);
            return localizationmock;
        }
        public static Mock<IRepository<Localization>> GetAllAsQuerable(string searchName, int? LanguageId)
        {
            var list = getData().Where(x => x.LanguageId == LanguageId).AsQueryable();
            var localization = list.AsQueryable();
            var includes = new string[] { "Language" };
            var listmock = new Mock<IRepository<Localization>>();
            listmock.Setup(repo => repo.GetAsQueryable(x => x.LanguageId == LanguageId, null, includes, false)).Returns(localization);
            return listmock;
        }
        public static Mock<IRepository<Localization>> GetAllWithParamaters(int? LanguageId)
        {
            var localization = getData().Where(x => x.LanguageId == LanguageId).AsQueryable();
            var includes = new string[] { "Language" };
            var localizationmock = new Mock<IRepository<Localization>>();
            localizationmock.Setup(repo => repo.GetAsQueryable(x => x.LanguageId == LanguageId, null,includes, false)).Returns(localization);
            return localizationmock;
        }
    
    }
}
