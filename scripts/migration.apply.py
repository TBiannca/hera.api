from os import system

system('dotnet ef database update --project ./src/Data/Data.csproj --startup-project ./src/Presentation/Presentation.csproj')
