# ThoughtHub

**ThoughtHub** is a demo project inspired by Medium.com.  
It consists of two separate parts:

1. **API Project** — Provides authentication, article, and publication endpoints.
2. **Frontend Project** — A Razor Pages frontend that consumes the API.

**Disclaimer:** This project is created **for educational and learning purposes only**.

---

## Solution Structure
- ThoughtHub
  - backend → ASP.NET Core Web API
  - frontend → ASP.NET Core Razor Pages frontend


## Features
- User registration & login with JWT authentication.
- Article feed with claps, comments, and publication support.
- Medium-inspired UI built with Bootstrap.
- Clear separation between frontend and backend for flexibility.

---

## Getting Started
### 1. Clone the Repository
```
git clone https://github.com/mt3d/publishing_platform.git
cd publishing_platform
```

### 2. Run the api
```
cd backend
dotnet run
```
API will be available at: https://localhost:5000

### 3.Run the frontend
```
cd ../frontend
dotnet run
```
The frontend will be available at: https://localhost:5100