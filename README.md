# 📘 Employee Management System

A full-stack employee management system with a .NET Core Web API backend and an Angular frontend.

---

## 📁 Project Structure

```
/employee-management-backend   ← ASP.NET Core Web API
/employee-management-frontend  ← Angular 19 (Bootstrap + Standalone Components)
```

---

## 🚀 Prerequisites

Make sure you have the following installed:

- **.NET 8 SDK**  
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
   - Set your connection string in `DefaultConnection`

3. **Run EF Core Migrations:**
   ```bash
   dotnet ef database update
   ```

4. **Run the API:**
   ```bash
   dotnet run
   ```
   The API will start on `https://localhost:5001` (or the port you’ve configured).

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
   ng serve
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