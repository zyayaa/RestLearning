using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestLearning.Data.Repositories {
    public class BaseRepository<TEntity> where TEntity : class {

        internal UsersContext context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(UsersContext context) {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public void Insert(TEntity entity) {
            dbSet.Add(entity);
        }
    }
}
