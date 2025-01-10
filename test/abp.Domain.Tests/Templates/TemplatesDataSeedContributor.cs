using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using abp.Templates;

namespace abp.Templates
{
    public class TemplatesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ITemplateRepository _templateRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TemplatesDataSeedContributor(ITemplateRepository templateRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _templateRepository = templateRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _templateRepository.InsertAsync(new Template
            (
                id: Guid.Parse("d522fb29-5bad-4c8a-ad31-ebc04f55f712"),
                name: "bb00e989970a4548a4f8995",
                number: 1509152648,
                description: "be4062a"
            ));

            await _templateRepository.InsertAsync(new Template
            (
                id: Guid.Parse("a4d2bf10-b775-4c3d-836c-404667ba5da2"),
                name: "ddce9a9ed68f4a39851dad9968a9fd3d4a4b9ee0177746cc8bbb774696",
                number: 1832457458,
                description: "2bfe1a06791b44baaac7955bdb2e09fa6934f52e963844dba2fe9ab6780ae6c65e52fc2fe6b14a159ce31"
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}