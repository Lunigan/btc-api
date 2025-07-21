# BTC Rate Snapshot Service — Backend API

This backend provides RESTful endpoints for storing, retrieving, and managing Bitcoin rate snapshots. It also supports checking for new data and batch deletion of saved rates.

## 🔧 Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / SQLite / any EF-compatible DB
- Code-first DB with migrations
- Serilog for structured logging
- AutoMapper

## 🚀 Endpoints

- `POST /api/bitcoin/rates/latest` — Get latest BTC-CZK records
- `POST /api/currency/rates/latest` — get latest currency rates
- Other endpoints for saving/retrieving/removing BTC snapshots (not shown)
- Project contains OpenAPI/Swagger

### Sample request - get latest currency rates
```
GET https://localhost:44391/api/currency/rates/latest
```

Response:
```json
[
  {
    "sourceCurrencyCode": "AUD",
    "targetCurrencyCode": "CZK",
    "rate": 13.797,
    "amount": 1,
    "validFor": "2025-07-18T00:00:00"
  },
  {
    "sourceCurrencyCode": "EUR",
    "targetCurrencyCode": "CZK",
    "rate": 24.625,
    "amount": 1,
    "validFor": "2025-07-18T00:00:00"
  },
  {
    "sourceCurrencyCode": "JPY",
    "targetCurrencyCode": "CZK",
    "rate": 14.235,
    "amount": 100,
    "validFor": "2025-07-18T00:00:00"
  },
  ...
]
```

## 🗃️ Logging

Uses **Serilog** with:
- Console logging
- Rolling file logging (`logs/btc-log.txt`)
- Enrichment via `FromLogContext` and machine name

## 📦 Setup

Ensure connection string is set in `appsettings.json`. 

Then: Run Update-Database in Package Manager Console to setup local SQL database.
