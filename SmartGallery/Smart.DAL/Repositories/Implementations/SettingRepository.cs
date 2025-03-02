using App.DAL.Presistence;
using Smart.Core.Entities;
using Smart.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.DAL.Repositories.Implementations
{
    public class SettingRepository : Repository<Setting>, ISettingRepository
    {
        protected SettingRepository(AppDbContext context) : base(context) { }
    }
}
