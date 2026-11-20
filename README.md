# Library Management System (.NET 10 & PostgreSQL)

A full-stack web application built with **ASP.NET Core 10 MVC** and **Entity Framework Core (Database-First)** connected to a **PostgreSQL** relational database. Designed to manage library operations including authors, genres/categories, book inventory, registered members, and active/returned loans.

**Author:** Gabriel Omar Paredes Iturralde

---

## Tech Stack

- **Framework:** .NET 10 (ASP.NET Core MVC)
- **ORM:** Entity Framework Core 10 (`Npgsql.EntityFrameworkCore.PostgreSQL`)
- **Database:** PostgreSQL 12+
- **Frontend:** Razor Views, Bootstrap 5, Bootstrap Icons

---

## Database Architecture

The application maps five relational tables from the `library_management` PostgreSQL schema:

- **`authors`**: Stores author profiles (`author_id`, `name`, `bio`).
- **`categories`**: Manages literary genres (`genre_id`, `genre_name`).
- **`books`**: Catalog of books linked to authors and categories (`book_id`, `title`, `author_id`, `isbn`, `publication_date`, `genre_id`, `availability_status`).
- **`members`**: Library patrons (`member_id`, `name`, `membership_id`, `contact_info`, `join_date`).
- **`loans`**: Tracks book checkouts and returns (`loan_id`, `book_id`, `member_id`, `checkout_date`, `due_date`, `return_date`, `return_status`).

---

## Key Features

- **Executive Dashboard:** Real-time summary counters for all five entities and active pending loans.
- **Complete CRUD Operations:** Full Create, Read, Update, and Delete functionality across all modules.
- **Relational Data Binding:** Human-readable dropdown selectors (`SelectList`) for foreign keys (`Author`, `Category`, `Book`, `Member`).
- **Server-Side Pagination:** Clean pagination (`5` records per page) implemented via `.Skip()` and `.Take()` with `AsNoTracking()` optimization.
- **Visual Status Indicators:** Bootstrap badges for book availability (`Available` / `Checked Out`) and loan status (`Returned` / `Active / Pending`).

---

## Project Structure

```text
Paredes_Gabriel_LibraryManagement/
├── Controllers/          # MVC Controllers with CRUD & pagination logic
├── Data/                 # Scaffolded LibraryManagementContext (DbContext)
├── Database/             # PostgreSQL database backup (library_management_final.backup)
├── Models/               # Entity classes generated from PostgreSQL schema
├── Views/                # Razor views styled with Bootstrap 5 & Bootstrap Icons
├── appsettings.json      # Database connection configuration
└── Program.cs            # Service registration & dependency injection
```

---

## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) & pgAdmin 4

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/YOUR_USERNAME/Paredes_Gabriel_LibraryManagement.git](https://github.com/YOUR_USERNAME/Paredes_Gabriel_LibraryManagement.git)
   cd Paredes_Gabriel_LibraryManagement
   ```

2. **Restore the PostgreSQL database:**
   - Create an empty database named `library_management` in pgAdmin 4.
   - Right-click `library_management` -> **Restore...** and select `Database/library_management_final.backup`.

3. **Configure the connection string:**
   Update `appsettings.json` with your PostgreSQL credentials:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=library_management;Username=postgres;Password=YOUR_PASSWORD"
   }
   ```

4. **Run the application:**
   ```bash
   dotnet restore
   dotnet run
   ```
   Open the local URL displayed in the terminal (e.g., `http://localhost:5000`).