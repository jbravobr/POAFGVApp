using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SQLiteNetExtensions.Extensions;

namespace POAFGVApp
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        public object _lock = new object();

        public BaseRepository()
        {
            if (App.AppSQLiteConn == null)
                App.AppSQLiteConn = DBContext.Instance;

            SetupDB();
        }

        void SetupDB()
        {
            try
            {
                App.AppSQLiteConn.CreateTable<User>(SQLite.CreateFlags.None);
                App.AppSQLiteConn.CreateTable<Address>(SQLite.CreateFlags.None);
                App.AppSQLiteConn.CreateTable<Order>(SQLite.CreateFlags.None);
                App.AppSQLiteConn.CreateTable<OrderDetail>(SQLite.CreateFlags.None);
                App.AppSQLiteConn.CreateTable<Product>(SQLite.CreateFlags.None);

                if (!App.AppSQLiteConn.Table<Product>().Any())
                {
                    var calabresa = new Product { Description = "Calabresa", Price = 30.00M };
                    var portuguesa = new Product { Description = "Portuguesa", Price = 35.00M };
                    var mucarela = new Product { Description = "Muçarela", Price = 20.00M };

                    App.AppSQLiteConn.InsertOrReplaceWithChildren(calabresa);
                    App.AppSQLiteConn.InsertOrReplaceWithChildren(portuguesa);
                    App.AppSQLiteConn.InsertOrReplaceWithChildren(mucarela);
                }

                if (!App.AppSQLiteConn.Table<User>().Any())
                {
                    var admin = new User
                    {
                        Login = "admin",
                        Password = "admin",
                        Name = "Rodrigo",
                        Address = new Address() { Burgh = "Estácio", Number = 186, Street = "Rua Maia de Lacerda", Zipcode = "20250-000" }
                    };
                    App.AppSQLiteConn.InsertOrReplaceWithChildren(admin);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Delete(T TEntity)
        {
            try
            {
                lock (_lock)
                    App.AppSQLiteConn.Delete(TEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetAll()
        {
            try
            {
                lock (_lock)
                    return App.AppSQLiteConn.GetAllWithChildren<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetAllWithChildrenByPredicate(Expression<Func<T, bool>> predicate)
        {
            try
            {
                lock (_lock)
                    return App.AppSQLiteConn.GetAllWithChildren<T>(predicate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetById(int pkId)
        {
            try
            {
                lock (_lock)
                    return App.AppSQLiteConn.GetWithChildren<T>(pkId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetWithChildrenByPredicate(int pk)
        {
            try
            {
                lock (_lock)
                    return App.AppSQLiteConn.GetWithChildren<T>(pk);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetWithChildrenByPredicate(Func<T, bool> predicate)
        {
            try
            {
                lock (_lock)
                {
                    var all = App.AppSQLiteConn.GetAllWithChildren<T>(null).FirstOrDefault<T>(predicate);
                    return all;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(T TEntity)
        {
            try
            {
                lock (_lock)
                    App.AppSQLiteConn.InsertOrReplaceWithChildren(TEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(T TEntity)
        {
            try
            {
                lock (_lock)
                    App.AppSQLiteConn.UpdateWithChildren(TEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}