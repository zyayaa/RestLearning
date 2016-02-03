using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestLearning.Data.Repositories;

namespace RestLearning.Data {
    public class UnitOfWork : IDisposable {

        private readonly UsersContext context = new UsersContext();

        private UsersRepository usersRepository;
        public UsersRepository UsersRepository
        {
            get
            {
                if(usersRepository == null) {
                    usersRepository = new UsersRepository(context);
                }
                return usersRepository;
            }
        }

        public void Save() {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing) {
            if(!this.disposed) {
                if(disposing) {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
