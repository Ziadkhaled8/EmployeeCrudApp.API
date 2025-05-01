# 📘 Employee Management System

A full-stack employee management system with a .NET Core Web API backend and an Angular frontend.

---

## 📁 Project Structure

```
/employee-management-backend   ← ASP.NET Core Web API
/employee-management-frontend  ← Angular 19 (Bootstrap + Standalone Components)
```


## 🚀 Prerequisites

Make sure you have the following installed:

- **.NET 9 SDK**  
- **MySQL** or your DB of choice  
- **Node.js** (v18 or higher recommended)  
- **Angular CLI**  
- **Visual Studio / Rider** for backend  
- **VS Code / WebStorm** for frontend  

---

## ⚙️ Backend Setup (ASP.NET Core Web API)

1. **Navigate to the backend project:**
   ```bash
   cd employee-management-backend
   ```

2. **Configure your DB connection string:**
   - Go to `appsettings.json`
   - Set your connection string in `con`

3. **Run EF Core Migrations:**
   ```bash
   dotnet ef database update
   ```

4. **Run the API:**
   ```bash
   dotnet run --launch-profile "https"
   ```
   The API will start on `https://localhost:7240` (or the port you’ve configured).

---

## 🌐 Frontend Setup (Angular 19)

1. **Navigate to the frontend project:**
   ```bash
   cd employee-management-frontend
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Run the app:**
   ```bash
   npm start
   ```

4. Open your browser at `http://localhost:4200`

---

## ✅ Features

- Add / Edit / Delete Employees
- Validation with feedback messages
- Pagination and search
- Bootstrap responsive UI
- Success/error handling and user feedback

---

---

## 🔧 Backend Architecture

The backend follows **Clean Architecture** principles with the following layers:

- **Domain**: Contains core business models and interfaces.
- **Application**: Holds business logic, DTOs, interfaces, and use cases.
- **Infrastructure**: Deals with external concerns (e.g., MySQL database using EF Core).
- **API (Presentation)**: ASP.NET Core Web API project that exposes endpoints and wires everything together.

Benefits:
- Separation of concerns
- Testability
- Scalability and maintainability

---

## 📡 API Endpoints

Base URL: `https://localhost:5001/api/employees` (or your deployed API base)

#### 🚀 Employee Endpoints

| Method | Endpoint                  | Description                      |
|--------|---------------------------|----------------------------------|
| GET    | `/api/employees?page=1`   | Get paginated list of employees |
| GET    | `/api/employees/{id}`     | Get an employee by ID           |
| POST   | `/api/employees`          | Create a new employee           |
| PUT    | `/api/employees/{id}`     | Update an existing employee     |
| DELETE | `/api/employees/{id}`     | Delete an employee              |
| GET    | `/api/employees/search?query=name&page=1` | Search employees by name |

All endpoints return a standard API response:
```json
{
  "statusCode": 200,
  "isSuccess": true,
  "error": null,
  "result": {}
}
```

Errors return `statusCode` 400+ with `isSuccess: false` and an `error` message.

---

## 📦 Build for Production

**Frontend:**
```bash
ng build
```

**Backend:**
```bash
dotnet publish -c Release
```

---

## 📄 License

MIT License.
