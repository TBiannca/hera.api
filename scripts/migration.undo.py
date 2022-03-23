from os import system
from argparse import ArgumentParser

parser = ArgumentParser()

parser.add_argument('--name',
                    help='Name of previous migration',
                    required=True)

args = parser.parse_args()

system(f'dotnet ef database update {args.name} --project ./src/Data/Data.csproj --startup-project ./src/Presentation/Presentation.csproj')
