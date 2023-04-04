using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository
{
    public interface IRepository<T>  where T : BaseEntity
    {
        List<T> GetAll();
        T GetSingle(Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           IEnumerable<string> includeProperties = null);
        T Get(int id);
        T GetById(string id);
        void Insert(T entity);
        void InsertRange(List<T> entity);

        void Update(T entity);
        void UpdateRange(List<T> entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();


        IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        IEnumerable<string> includeProperties = null);

        IEnumerable<T> GetAsNoTracking(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            IEnumerable<string> includeProperties = null);

        IQueryable<T> GetAsQueryable(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            IEnumerable<string> includeProperties = null, bool distincts = false);

        bool Any(Expression<Func<T, bool>> filter = null); 
    }
    public interface IUserRepository<T>  where T : class
    {

        //UserManager<AppUser> UserManager { get; }
        //RoleStore<IdentityRole> RoleStore { get; }
        //RoleManager<IdentityRole> RoleManager { get; }

        List<T> GetAll();
        T GetSingle(Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           IEnumerable<string> includeProperties = null);
        T Get(int id);
        T GetById(string id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();


        IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        IEnumerable<string> includeProperties = null);

        IEnumerable<T> GetAsNoTracking(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            IEnumerable<string> includeProperties = null);

        IQueryable<T> GetAsQueryable(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            IEnumerable<string> includeProperties = null, bool distincts = false);

        bool Any(Expression<Func<T, bool>> filter = null); 
    }
}
