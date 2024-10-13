using olss.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olss.Data.Infrastructure
{
    public interface IDatabaseFactoryOlss
    {
        OlssEntities Get();
    }
}
