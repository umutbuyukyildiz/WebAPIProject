using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProject.Domain.UnitOfWork
{

    public class UnitOfWork : IUnitOfWork
    {

        private readonly WebAPIProjectContext context;

        public UnitOfWork(WebAPIProjectContext context)
        {
            this.context = context;
        }

        public void Complete()
        {
            this.context.SaveChanges();
        }

        public async Task CompleteAsync()
        {

            await this.context.SaveChangesAsync();
        }
    }
}
