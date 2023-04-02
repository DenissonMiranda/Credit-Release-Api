using CreditRelease.Utility.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CreditRelease.Infra.Data.Contexts
{
    public partial class Context : DbContext
    {
        public IDbContextTransaction? Transaction { get; private set; }

        public Context(DbContextOptions<Context> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            CarregaMapeamentoDasTabelas(modelBuilder);

        }

        private void CarregaMapeamentoDasTabelas(ModelBuilder modelBuilder)
        {
            var classeTypeList = NamespaceTypeHelper.
                 ObtemTypesQueFacamParteDoNamespaceInformado(new NamespaceTypeHelper
                 .NamespaceInfo("CreditRelease.Infra.Data",
                                "CreditRelease.Infra.Data.EntityFramework.Map"));
            foreach (var typeClass in classeTypeList)
            {
                var constructorParams = new Type[]
                {
                    typeof(Context)
                };
                var construtor = typeClass.GetConstructor(constructorParams);
                dynamic instantiatedClass;
                if (construtor != null)
                    instantiatedClass = Activator.CreateInstance(typeClass, this);
                else
                    instantiatedClass = Activator.CreateInstance(typeClass);
                modelBuilder.ApplyConfiguration(instantiatedClass);
            }

        }


        public async Task<IDbContextTransaction> IniTransacao()
        {
            if (Transaction == null)
                Transaction = await this.Database.BeginTransactionAsync();

            return Transaction;
        }

        private async Task RollBack()
        {
            if (Transaction != null)
            {
                await Transaction.RollbackAsync();
            }
        }

        private async Task Salvar()
        {
            try
            {
                ChangeTracker.DetectChanges();
                await SaveChangesAsync();
            }
            catch
            {
                await RollBack();
                throw;
            }
        }

        private async Task Commit()
        {
            if (Transaction != null)
            {
                await Transaction.CommitAsync();
                Transaction.Dispose();
                Transaction = null;
            }
        }

        public async Task SendChanges()
        {
            await Salvar();
            await Commit();
        }
    }
}
