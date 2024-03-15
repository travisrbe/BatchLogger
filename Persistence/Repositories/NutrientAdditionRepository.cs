using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal class NutrientAdditionRepository : RepositoryBase<NutrientAddition>, INutrientAdditionRepository
    {
        public NutrientAdditionRepository(DataContext context) : base(context) { }
    }
}
