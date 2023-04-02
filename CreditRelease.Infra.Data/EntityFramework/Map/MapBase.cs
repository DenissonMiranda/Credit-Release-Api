using CreditRelease.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditRelease.Infra.Data.EntityFramework.Map
{
    public abstract class MapBase<T> : IEntityTypeConfiguration<T>
       where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
        }
    }
}
