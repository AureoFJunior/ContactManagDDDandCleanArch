FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContactManag.Web/ContactManag.Web.csproj", "ContactManag.Web/"]
COPY ["ContactManag.Application/ContactManag.Application.csproj", "ContactManag.Application/"]
COPY ["ContactManag.Domain/ContactManag.Domain.csproj", "ContactManag.Domain/"]
COPY ["ContactManag.Infra/ContactManag.Infra.csproj", "ContactManag.Infra/"]
RUN dotnet restore "ContactManag.Web/ContactManag.Web.csproj"
COPY . .
WORKDIR "/src/ContactManag.Web"
RUN dotnet build "ContactManag.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactManag.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactManag.Web/ContactManag.Web.dll"]