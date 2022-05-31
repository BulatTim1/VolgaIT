FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
RUN apk add --no-cache icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
	ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine-amd64 AS build
WORKDIR /src
COPY ["VolgaIT.csproj", "."]
RUN dotnet restore "./VolgaIT.csproj"
COPY . .
WORKDIR "/src/."
#RUN dotnet ef migrations add Initial
#RUN dotnet ef database update
RUN dotnet build "VolgaIT.csproj" -c Release -o /app/build
CMD 

FROM build AS publish
RUN dotnet publish "VolgaIT.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VolgaIT.dll"]