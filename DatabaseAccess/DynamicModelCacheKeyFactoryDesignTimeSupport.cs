using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ExtendFieldDemo.DatabaseAccess
{
    public class DynamicModelCacheKeyFactoryDesignTimeSupport : IModelCacheKeyFactory
    {
        public object Create(DbContext context, bool designTime)
        {
            if (context is DefaultDbContext && !DefaultDbContext.ExtendFieldChanged)
            {
                return (context.GetType(), DefaultDbContext.ExtendFieldChanged, designTime);
            }
            else
            {
                DefaultDbContext.ExtendFieldChanged = false;
                return (object)context.GetType();
            }
        }

        public object Create(DbContext context)
            => Create(context, false);
    }
}
