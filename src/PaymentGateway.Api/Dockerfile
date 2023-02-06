ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
ARG BUILDCONFIG
COPY src/ .
WORKDIR /PaymentGateway.Api
RUN dotnet restore PaymentGateway.Api.csproj
RUN dotnet build PaymentGateway.Api.csproj -c $BUILDCONFIG -o /app/build

FROM build AS publish
ARG BUILDCONFIG
ARG VERSION
RUN dotnet publish PaymentGateway.Api.csproj -c $BUILDCONFIG -o /app/publish /p:Version=$VERSION

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentGateway.Api.dll"]