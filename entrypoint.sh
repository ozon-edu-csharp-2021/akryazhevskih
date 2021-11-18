#!/bin/bash

dotnet MerchandiseService.Migrator.dll --no-build -v d -- --dryrun

dotnet MerchandiseService.Migrator.dll --no-build -v d

>&2 echo "MerchandiseService DB Migrations complete, starting app."
>&2 echo "Run MerchandiseService."

dotnet MerchandiseService.API.dll --no-build -v d