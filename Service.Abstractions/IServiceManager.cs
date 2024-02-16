using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface IServiceManager
    {
        IYeastService YeastService { get; }
        IBatchService BatchService { get; }
        IUserService UserService { get; }
    }
}
