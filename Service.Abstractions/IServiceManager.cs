using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        IYeastService YeastService { get; }
        IBatchService BatchService { get; }
        INutrientService NutrientService { get; }
        IUserBatchService UserBatchService { get; }
        IBatchLogEntryService BatchLogEntryService { get; }
        INutrientAdditionService NutrientAdditionService { get; }
    }
}
