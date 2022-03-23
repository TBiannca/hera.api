from os import system

system(f'dotnet ef migrations remove --project ./src/Data/Data.csproj --startup-project ./src/Presentation/Presentation.csproj')
