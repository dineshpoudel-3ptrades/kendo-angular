./build-image.ps1 -ProjectPath "../../src/abp.DbMigrator/abp.DbMigrator.csproj" -ImageName abp/dbmigrator
./build-image.ps1 -ProjectPath "../../src/abp.HttpApi.Host/abp.HttpApi.Host.csproj" -ImageName abp/httpapihost
./build-image.ps1 -ProjectPath "../../angular" -ImageName abp/angular -ProjectType "angular"
