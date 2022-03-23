from os import system
from argparse import ArgumentParser

parser = ArgumentParser()

parser.add_argument('--name',
                    help='Name of migration.',
                    required=True)

args = parser.parse_args()

system(f'dotnet ef migrations add {args.name} --project ./src/Data/Data.csproj --startup-project ./src/Presentation/Presentation.csproj')
