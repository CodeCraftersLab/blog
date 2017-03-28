MigrationName=$1;
pushd "Directorio de proyecto de datos"
dotnet ef migrations add $MigrationName
dotnet ef database update
popd
