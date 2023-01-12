using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProject.Domain.Repositories
{


    public class BaseRepository
    {
        protected readonly WebAPIProjectContext context;

        public BaseRepository(WebAPIProjectContext context)
        {
            this.context = context;
        }

    }
}
