using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using abp.Parts;

namespace abp.Parts
{
    public class PartsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IPartRepository _partRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public PartsDataSeedContributor(IPartRepository partRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _partRepository = partRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _partRepository.InsertAsync(new Part
            (
                id: Guid.Parse("6d83767d-76d1-4ba2-afb6-b147db00adf0"),
                name: "73e299d3069d4",
                number: 2098693121
            ));

            await _partRepository.InsertAsync(new Part
            (
                id: Guid.Parse("0a7126b1-ecbe-41d0-ab25-46bc97feebcb"),
                name: "64a080eb127b4e598ab61c43cb072649ebe11c394f4e46f49304353b1d77526215d80efd94f44b75b9d47b0",
                number: 129706725
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}