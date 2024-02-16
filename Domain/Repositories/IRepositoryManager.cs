using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IYeastRepository YeastRepository { get; }
        IBatchRepository BatchRepository { get; }
        
        IUnitOfWork UnitOfWork {get;}
    }
}
