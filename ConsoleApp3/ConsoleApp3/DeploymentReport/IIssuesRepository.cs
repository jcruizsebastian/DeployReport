using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentReport
{
    public interface IIssuesRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity Get(TKey key);
    }
}
