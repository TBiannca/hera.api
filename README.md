# Hera
The backend part of the Hera platform.

## Requirements
Stuff needed to be met before starting developing the current project.
* docker
* docker-compose


## Run
Run
Stuff needed to be done in order to run the project in dev environment.

* Clone the project
  * `git clone ...`
* Enter into the cloned directory
  * `cd ...`
* Install the dependencies
  * `docker-compose up`

## Database operations
Stuff needed to be done in order to perform code-first db operations.

* Apply migrations:
  * python3 scripts/migration.apply.py
* Undo migrations:
  * python3 scripts/migration.undo.py --name THE_NAME_OF_THE_MIGRATION_TO_RETURN_TO
* Add new migration:
  * python3 scripts/migration.add.py --name THE_NAME_OF_THE_NEW_MIGRATION
* Remove last migration(make sure to undo it firstly):
  * python3 scripts/migration.remove.py