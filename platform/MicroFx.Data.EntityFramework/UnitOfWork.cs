using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace MicroFx.Data.EntityFramework
{
    public class UnitOfWork : DbContext, IQueryableUnitOfWork
    {
        private readonly IDbConnectionProvider dbConnectionProvider;

        static UnitOfWork()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<UnitOfWork>());

        }

        public UnitOfWork(IDbConnectionProvider dbConnectionProvider) 
            : base(dbConnectionProvider.GetConnectionString())
        {
            this.dbConnectionProvider = dbConnectionProvider;
        }

        #region IQueryableUnitOfWork Members

        public DbSet<T> CreateSet<T>()
            where T : class
        {
            return Set<T>();
        }

        public void Attach<T>(T item)
            where T : class
        {
            //attach and set as unchanged
            Entry(item).State = EntityState.Unchanged;
        }

        public void SetModified<T>(T item)
            where T : class
        {
            //this operation also attach item in object state manager
            Entry(item).State = EntityState.Modified;
        }

        public void SetAdded<T>(T item)
            where T : class
        {
            Set<T>().Add(item);
        }

        public void SetDeleted<T>(T item)
            where T : class
        {
            Set<T>().Remove(item);
        }

        public void ApplyCurrentValues<T>(T original, T current)
            where T : class
        {
            //if it is not attached, attach original and set current values
            Entry(original).CurrentValues.SetValues(current);
        }

        public void Commit()
        {
            SaveChanges();
        }

       
        public void Rollback()
        {
            // set all entities in change tracker as 'unchanged state'
            ChangeTracker.Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public IEnumerable<T> ExecuteQuery<T>(string sqlQuery, params object[] parameters)
        {
            return Database.SqlQuery<T>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var assembly = dbConnectionProvider.GetType().Assembly;
            modelBuilder.Configurations.AddFromAssembly(assembly);
        }

        public DbTransaction CreateTransaction(IsolationLevel level)
        {
            var transaction = Database.BeginTransaction(level);
            return transaction.UnderlyingTransaction;
        }

        
        public bool InTransaction
        {
            get { return Database.CurrentTransaction!=null; }
        }
    }
}