
# Authors
``jupetr`` : ``Jüri Petrotšenko 223166IABD`` \
``jepole`` : `` Jegor Poletaev 223294IADB``

## Install commands

~~~bash
dotnet tool update -g dotnet-ef
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet tool update -g dotnet-aspnet-codegenerator
~~~


### EF Migrations / DataBase
Run from solution folder.
~~~bash
dotnet ef migrations --project Core.DAL.EF --startup-project WebApp add Initial
dotnet ef database --project Core.DAL.EF --startup-project WebApp update
dotnet ef database --project Core.DAL.EF --startup-project WebApp drop
~~~

### MVC Controllers

#### Install from nuget:
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Microsoft.EntityFrameWorkCore.SqlServer

From Web folder.

!!!NOTE: / for Unix, \ for Windows.!!!

~~~bash
cd VirtualMe
cd WebApp
dotnet aspnet-codegenerator controller -name ActivityController        -actions -m  Core.Domain.Entities.Activity        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AvatarController        -actions -m  Core.Domain.Entities.Avatar        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ChatController        -actions -m  Core.Domain.Entities.Chat        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ItemController        -actions -m  Core.Domain.Entities.Item        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MessageController        -actions -m  Core.Domain.Entities.Message        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AvatarsActivityController        -actions -m  Core.Domain.AddressTables.AvatarsActivity        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AvatarsTasksController        -actions -m  Core.Domain.AddressTables.AvatarsTasks        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
# use area
dotnet aspnet-codegenerator controller -name LogsController        -actions -m  Core.Domain.Entities.Logs        -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ActivityTypeController        -actions -m  Core.Domain.Entities.ActivityType        -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name InteriorController        -actions -m  Core.Domain.Entities.Interior        -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AvatarOwnsInteriorController         -actions -m Core.Domain.AddressTables.AvatarOwnsInterior         -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TaskQuestController        -actions -m  Core.Domain.Entities.TaskQuest        -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TaskTypeController        -actions -m  Core.Domain.Entities.TaskType        -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OwnsController        -actions -m  Core.Domain.AddressTables.Owns        -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RewardController        -actions -m  Core.Domain.AddressTables.Reward        -dc AppDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
cd ..
~~~

~~~bash
dotnet aspnet-codegenerator controller -name AssignmentController -actions -m  Core.Domain.Entities.Assignment -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AttendantController -actions -m  Core.Domain.Entities.Attendant -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SubjectController -actions -m  Core.Domain.Entities.Subject -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
# use area
~~~

## Api Controllers
~~~bash
cd WebApp

#dotnet aspnet-codegenerator controller -name ActivityController    -m  Core.Domain.Entities.Activity        -dc AppDbContext -outDir ApiControllers --api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name AssignmentController     -m Core.Domain.Entities.Assignment      -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name AttendantController     -m Core.Domain.Entities.Attendant      -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name SubjectController     -m Core.Domain.AddressTables.Subject      -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

cd ..
~~~

## Docker

~~~bash
docker build -t virtualme:latest .
docker buildx build --progress=plain --force-rm --push -t jepole/virtualme:latest . 
# multiplatform build on apple silicon
# https://docs.docker.com/build/building/multi-platform/
docker buildx create --name mybuilder --bootstrap --use
docker buildx build --platform linux/amd64 -t jepole/virtualme:latest --push .

~~~

## Generate login views

~~~bash
cd WebApp
dotnet aspnet-codegenerator identity -f --userClass=Core.Domain.Identity.AppUser -gl
dotnet aspnet-codegenerator identity -dc Code.DAL.EF.AppDbContext -f
cd ..
~~~

## Three.js
Docs: [Three.js docs](https://threejs.org/docs/index.html#manual/en/introduction/Creating-a-scene)
