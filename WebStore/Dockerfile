FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /app
EXPOSE 80

# copy csproj and restore as distinct layers
COPY *.sln .

COPY BLL.App/*.csproj ./BLL.App/
COPY BLL.App.DTO/*.csproj ./BLL.App.DTO/
COPY BLL.Base/*.csproj ./BLL.Base/
COPY Contracts.BLL.App/*.csproj ./Contracts.BLL.App/
COPY Contracts.BLL.Base/*.csproj ./Contracts.BLL.Base/

COPY Contracts.DAL.App/*.csproj ./Contracts.DAL.App/
COPY DAL.App.DTO/*.csproj ./DAL.App.DTO/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/

COPY Domain/*.csproj ./Domain/
COPY PublicApi.DTO/*.csproj ./PublicApi.DTO/
COPY Contracts.PublicApi.DTO/*.csproj ./Contracts.PublicApi.DTO/
COPY Extensions/*.csproj ./Extensions/
COPY Resources/*.csproj ./Resources/

COPY WebApp/*.csproj ./WebApp/

RUN dotnet restore


# copy everything else and build app
COPY BLL.App/. ./BLL.App/
COPY BLL.App.DTO/. ./BLL.App.DTO/
COPY BLL.Base/. ./BLL.Base/
COPY Contracts.BLL.App/. ./Contracts.BLL.App/
COPY Contracts.BLL.Base/. ./Contracts.BLL.Base/

COPY Contracts.DAL.App/. ./Contracts.DAL.App/
COPY DAL.App.DTO/. ./DAL.App.DTO/
COPY DAL.App.EF/. ./DAL.App.EF/

COPY Contracts.PublicApi.DTO/. ./Contracts.PublickApi.DTO/
COPY Domain/. ./Domain/
COPY PublicApi.DTO/. ./PublicApi.DTO/
COPY Extensions/. ./Extensions/
COPY Resources/. ./Resources/

COPY WebApp/. ./WebApp/

WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:latest AS runtime
WORKDIR /app
EXPOSE 80
ENV ConnectionStrings:SqlServerConnection="server=alpha.akaver.com;database=student2018_Nipetu_WebStore;user=student2018;password=student2018"
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]

