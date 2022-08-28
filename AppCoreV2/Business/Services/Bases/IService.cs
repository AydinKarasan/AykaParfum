using AppCoreV2.Business.Models;
using AppCoreV2.DataAccess.Entityframework.Bases;
using AppCoreV2.Records.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCoreV2.Business.Services.Bases
{
    public interface IService<TModel, TEntity, TDbContext> : IDisposable where TModel : RecordBase, new() where TEntity : RecordBase, new() where TDbContext : DbContext, new()
    {
        RepoBase<TEntity, TDbContext> Repo { get; set; }
        IQueryable<TModel> Query();
        Result Add(TModel model);
        Result Update(TModel model);
        Result Delete(int id);
    }
}
