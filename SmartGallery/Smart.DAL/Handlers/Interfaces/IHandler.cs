using Smart.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.DAL.Handlers.Interfaces
{
    public interface IHandler<T> where T : BaseEntity
    {
        T HandleEntityAsync(T entity);
    }
}
