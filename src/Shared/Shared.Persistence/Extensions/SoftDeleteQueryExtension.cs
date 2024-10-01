using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using Shared.Core.Domain.Interfaces;


namespace Shared.Persistence.Extensions;

public static class SoftDeleteQueryExtension
{
    public static void AddSoftDeleteQueryFilter(
        IMutableEntityType entityData)
    {
        var methodToCall = typeof(SoftDeleteQueryExtension)
            .GetMethod(nameof(GetSoftDeleteFilter),
                BindingFlags.NonPublic | BindingFlags.Static)
            .MakeGenericMethod(entityData.ClrType);
        var filter = methodToCall.Invoke(null, new object[] { });
        entityData.SetQueryFilter((LambdaExpression)filter);
        entityData.AddIndex(entityData.
             FindProperty(nameof(ISoftDelete.IsDeleted)));
    }

    private static LambdaExpression GetSoftDeleteFilter<TEntity>()
        where TEntity : class, ISoftDelete
    {
        Expression<Func<TEntity, bool>> filter = x => !(x.IsDeleted ?? false) && (x.IsActive);
        return filter;
    }
}