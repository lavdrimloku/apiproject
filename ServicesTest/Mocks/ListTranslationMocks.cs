using Data;
using Moq;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesTest.Mocks
{
    public class ListTranslationMocks
    {
        public static List<ListTranslation> getData()
        {
            var returnedList = new List<ListTranslation>()
            {
                new ListTranslation
                {
                    Id = 1,
                    Name= "Translation 1",
                    ListId = 1,
                    LanguageId = 1,
                    IsDeleted = false
                },
                new ListTranslation
                {
                    Id = 2,
                    Name= "Translation 2",
                    ListId = 1,
                    LanguageId = 2,
                    IsDeleted = false
                },
                new ListTranslation
                {
                    Id = 3,
                    Name= "Translation 3",
                    ListId = 2,
                    LanguageId = 3,
                    IsDeleted = false
                }
            };

            return returnedList;
        }

        public static Mock<IRepository<ListTranslation>> GetAll()
        {
            var lists = getData();
            var listmock = new Mock<IRepository<ListTranslation>>();
            listmock.Setup(repo => repo.GetAll()).Returns(lists);
            return listmock;
        }

        public static Mock<IRepository<ListTranslation>> GetById(int id)
        {
            var lists = getData();
            var listmock = new Mock<IRepository<ListTranslation>>();
            var firstItem = lists.FirstOrDefault(x => x.Id == id);
            listmock.Setup(repo => repo.Get(id)).Returns(firstItem);
            return listmock;
        }

        public static Mock<IRepository<ListTranslation>> GetByListId(int Listid, int LangId)
        {
            var lists = getData();
            var listmock = new Mock<IRepository<ListTranslation>>();
            var firstItem = lists.FirstOrDefault(x => x.ListId == Listid && x.LanguageId == LangId);
            listmock.Setup(repo => repo.GetSingle(x => x.ListId == Listid && x.LanguageId == LangId, null, null)).Returns(firstItem);
            return listmock;
        }

        public static Mock<IRepository<ListTranslation>> GetListTranslationByListId(int Listid)
        {
            var lists = getData();
            var listmock = new Mock<IRepository<ListTranslation>>();
            var items = lists.Where(x => x.ListId == Listid).ToList();
            IEnumerable<string> includes = new string[] { "Language" };
            listmock.Setup(repo => repo.GetAsNoTracking(x => x.ListId == Listid, null, includes)).Returns(items);
            return listmock;
        }

        public static Mock<IRepository<ListTranslation>> Insert()
        {
            var lists = getData();

            var listmock = new Mock<IRepository<ListTranslation>>();
            listmock.Setup(repo => repo.Insert(It.IsAny<ListTranslation>())).Callback((ListTranslation list) =>
            {
                list.Id = list.Id + 1;
                lists.Add(list);
            }).Verifiable();

            listmock.Setup(repo => repo.GetAll()).Returns(lists);
            return listmock;
        }

        public static Mock<IRepository<ListTranslation>> TranslationExists(int? listid, int? languageid)
        {
            var lists = getData();
            var listmock = new Mock<IRepository<ListTranslation>>();
            var firstItem = lists.Where(x => x.ListId == listid && x.LanguageId == languageid && x.IsDeleted == false);
            listmock.Setup(repo => repo.Get(x => x.ListId == listid && x.LanguageId == languageid && x.IsDeleted == false, null, null)).Returns(firstItem);
            return listmock;
        }

        public static Mock<IRepository<ListTranslation>> Update(ListTranslation item)
        {
            var lists = getData().FirstOrDefault();
            var listmock = new Mock<IRepository<ListTranslation>>();

            listmock.Setup(repo => repo.Update(It.IsAny<ListTranslation>())).Callback((ListTranslation list) =>
            {
                lists.Name = item.Name;
                lists.ListId = item.ListId;
            }).Verifiable();

            listmock.Setup(repo => repo.Get(lists.Id)).Returns(lists);
            return listmock;
        }

        public static Mock<IRepository<ListTranslation>> Delete()
        {
            var lists = getData().FirstOrDefault();
            var listmock = new Mock<IRepository<ListTranslation>>();

            listmock.Setup(repo => repo.Delete(It.IsAny<ListTranslation>())).Callback((ListTranslation list) =>
            {
                lists.IsDeleted = true;
                lists.DeletedDate = DateTime.Now;
            }).Verifiable();

            listmock.Setup(repo => repo.Get(lists.Id)).Returns(lists);
            return listmock;
        }

    }
}
