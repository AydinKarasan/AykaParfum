using AppCoreV2.DataAccess.Entityframework.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCoreV2.DataAccess.Entityframework
{
    public class Repo<TEntity, TDbContext> : RepoBase<TEntity, TDbContext> where TEntity : class, new() where TDbContext : DbContext, new()
    {
        public Repo() : base()
        {

        }
        public Repo(TDbContext dbContext) : base(dbContext)
        {

        }
    }
}
