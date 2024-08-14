FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
# copy ALL the projects
COPY Core.BLL/*.csproj ./Core.BLL/
COPY Core.BLL.DTO/*.csproj ./Core.BLL.DTO/
COPY Core.Contracts.BLL/*.csproj ./Core.Contracts.BLL/
COPY Core.Contracts.DAL/*.csproj ./Core.Contracts.DAL/
COPY Core.DAL.DTO/*.csproj ./Core.DAL.DTO/
COPY Core.DAL.EF/*.csproj ./Core.DAL.EF/
COPY Core.Domain/*.csproj ./Core.Domain/
COPY Core.DTO/*.csproj ./Core.DTO/
COPY Core.Resources/*.csproj ./Core.Resources/
COPY Core.Test/*.csproj ./Core.Test/
COPY Helpers/*.csproj ./Helpers/
COPY Shared.Helpers/*.csproj ./Shared.Helpers/
COPY Shared.BLL/*.csproj ./Shared.BLL/
COPY Shared.Contracts.BLL/*.csproj ./Shared.Contracts.BLL/
COPY Shared.Contracts.DAL/*.csproj ./Shared.Contracts.DAL/
COPY Shared.Contracts.Domain/*.csproj ./Shared.Contracts.Domain/
COPY Shared.DAL.EF/*.csproj ./Shared.DAL.EF/
COPY Shared.Domain/*.csproj ./Shared.Domain/
COPY Shared.Resources/*.csproj ./Shared.Resources/
COPY Shared.Test/*.csproj ./Shared.Test/

COPY WebApp/*.csproj ./WebApp/
RUN dotnet restore


# copy everything else and build app
# copy all the projects
COPY Core.BLL/. ./Core.BLL/
COPY Core.BLL.DTO/. ./Core.BLL.DTO/
COPY Core.Contracts.BLL/. ./Core.Contracts.BLL/
COPY Core.Contracts.DAL/. ./Core.Contracts.DAL/
COPY Core.DAL.DTO/. ./Core.DAL.DTO/
COPY Core.DAL.EF/. ./Core.DAL.EF/
COPY Core.Domain/. ./Core.Domain/
COPY Core.DTO/. ./Core.DTO/
COPY Core.Resources/. ./Core.Resources/
COPY Core.Test/. ./Core.Test/
COPY Helpers/. ./Helpers/
COPY Shared.Helpers/. ./Shared.Helpers/
COPY Shared.BLL/. ./Shared.BLL/
COPY Shared.Contracts.BLL/. ./Shared.Contracts.BLL/
COPY Shared.Contracts.DAL/. ./Shared.Contracts.DAL/
COPY Shared.Contracts.Domain/. ./Shared.Contracts.Domain/
COPY Shared.DAL.EF/. ./Shared.DAL.EF/
COPY Shared.Domain/. ./Shared.Domain/
COPY Shared.Resources/. ./Shared.Resources/
COPY Shared.Test/. ./Shared.Test/

COPY WebApp/. ./WebApp/

# Run tests
RUN dotnet test Shared.Test
RUN dotnet test Core.Test

# build output files
WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out

# switch to runtime image
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
EXPOSE 80
EXPOSE 8080
WORKDIR /app
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]