using Microsoft.EntityFrameworkCore;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public List<T> GetAll()
        {
            return entities.ToList();
        }

        public T Get(int id)
        {
            //return entities.FirstOrDefault(s => s. == id);
            var entity = this.entities.Find(id);

            if (entity != null && entity.IsDeleted)
            {
                return null;
            }
            else
            {
                return entity;
            }
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void InsertRange(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.AddRange(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.LastChangedDate = DateTime.Now;
            context.SaveChanges();
        }
        public void UpdateRange(List<T> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            foreach (var item in entity)
            {
                item.LastChangedDate = DateTime.Now;
            }

            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;

            context.SaveChanges();
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public T GetById(string id)
        {
            Guid gid = Guid.Parse(id);
            var entity = this.entities.Find(gid);

            if (entity != null && entity.IsDeleted)
            {
                return null;
            }
            else
            {
                return entities.Find(gid);
            }
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IEnumerable<string> includeProperties = null)
        {
            IQueryable<T> query = this.entities.Where(x => !x.IsDeleted);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                includeProperties.ToList().ForEach(property =>
                {
                    query = query.Include(property);
                });
            }
            var returnobj = orderBy != null ? orderBy(query).ToList() : query.ToList();

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public IEnumerable<T> GetAsNoTracking(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IEnumerable<string> includeProperties = null)
        {
            IQueryable<T> query = this.entities.Where(x => !x.IsDeleted);

            if (includeProperties != null)
            {
                includeProperties.ToList().ForEach(property => query = query.Include(property));
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return orderBy != null ? orderBy(query).AsNoTracking().ToList() : query.AsNoTracking().ToList();
        }

        public IQueryable<T> GetAsQueryable(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IEnumerable<string> includeProperties = null, bool disticts = false)
        {
            IQueryable<T> query = this.entities.Where(x => !x.IsDeleted);

            if (includeProperties != null)
            {
                includeProperties.ToList().ForEach(property => query = query.Include(property));
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (disticts)
            {
                query = query.Distinct();
            }

            //return query;
            return orderBy != null ? orderBy(query) : query;
        }

        public bool Any(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = this.entities.Where(x => !x.IsDeleted);
            if (filter != null)
            {
                return query.Any(filter);
            }

            return query.Any();
        }


        public T GetSingle(
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          IEnumerable<string> includeProperties = null)
        {
            IQueryable<T> query = this.entities.Where(x => !x.IsDeleted);

            if (includeProperties != null)
            {
                includeProperties.ToList().ForEach(property =>
                {
                    query = query.Include(property);
                });
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return orderBy != null ? orderBy(query).FirstOrDefault() : query.FirstOrDefault();
        }


    }
    public class UserRepository<T> : IUserRepository<T> where T : class
    {
        private readonly ApplicationContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        private UserStore<AppUser> userStore;
        private UserManager<AppUser> userManager;

        private RoleStore<ApplicationRole> roleStore;
        private RoleManager<ApplicationRole> roleManager;

        public UserRepository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<T>();

            userStore = new UserStore<AppUser>(context);
            //userManager = new UserManager<AppUser>(userStore);
            roleStore = new RoleStore<ApplicationRole>(context);

        }
        public List<T> GetAll()
        {
            return entities.ToList();
        }

        public T Get(int id)
        {
            //return entities.FirstOrDefault(s => s. == id);
            var entity = this.entities.Find(id);

            if (entity == null)
            {
                return null;
            }
            else
            {
                return entity;
            }
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }



            context.SaveChanges();
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public T GetById(string id)
        {
            Guid gid = Guid.Parse(id);
            var entity = this.entities.Find(gid);

            if (entity != null)
            {
                return null;
            }
            else
            {
                return entities.Find(gid);
            }
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IEnumerable<string> includeProperties = null)
        {
            IQueryable<T> query = this.entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                includeProperties.ToList().ForEach(property =>
                {
                    query = query.Include(property);
                });
            }
            var returnobj = orderBy != null ? orderBy(query).ToList() : query.ToList();

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public IEnumerable<T> GetAsNoTracking(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IEnumerable<string> includeProperties = null)
        {
            IQueryable<T> query = this.entities;

            if (includeProperties != null)
            {
                includeProperties.ToList().ForEach(property => query = query.Include(property));
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return orderBy != null ? orderBy(query).AsNoTracking().ToList() : query.AsNoTracking().ToList();
        }

        public IQueryable<T> GetAsQueryable(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IEnumerable<string> includeProperties = null, bool disticts = false)
        {
            IQueryable<T> query = this.entities;

            if (includeProperties != null)
            {
                includeProperties.ToList().ForEach(property => query = query.Include(property));
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (disticts)
            {
                query = query.Distinct();
            }

            //return query;
            return orderBy != null ? orderBy(query) : query;
        }

        public bool Any(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = this.entities;
            if (filter != null)
            {
                return query.Any(filter);
            }

            return query.Any();
        }


        public T GetSingle(
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          IEnumerable<string> includeProperties = null)
        {
            IQueryable<T> query = this.entities;

            if (includeProperties != null)
            {
                includeProperties.ToList().ForEach(property =>
                {
                    query = query.Include(property);
                });
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return orderBy != null ? orderBy(query).FirstOrDefault() : query.FirstOrDefault();
        }


    }
}
