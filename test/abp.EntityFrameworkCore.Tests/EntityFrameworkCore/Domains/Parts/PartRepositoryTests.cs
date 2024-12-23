using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using abp.Parts;
using abp.EntityFrameworkCore;
using Xunit;

namespace abp.EntityFrameworkCore.Domains.Parts
{
    public class PartRepositoryTests : abpEntityFrameworkCoreTestBase
    {
        private readonly IPartRepository _partRepository;

        public PartRepositoryTests()
        {
            _partRepository = GetRequiredService<IPartRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _partRepository.GetListAsync(
                    name: "73e299d3069d4"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("6d83767d-76d1-4ba2-afb6-b147db00adf0"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _partRepository.GetCountAsync(
                    name: "64a080eb127b4e598ab61c43cb072649ebe11c394f4e46f49304353b1d77526215d80efd94f44b75b9d47b0"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}