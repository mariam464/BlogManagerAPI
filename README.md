
# BlogManagerAPI

Welcome to the **BlogManager API!** This is a robust and secure backend service for managing a blog, built using ASP.NET Core. It provides a comprehensive set of features for handling blog posts, comments, and categories, with emphasis on role-based access control.

---

##  Features

**Role-Based Access Control:** Secure endpoints based on user roles.

-**Admin:** Manages categories

-**Author:** Can create, update, and delete their own blog posts and comments.

-**Reader:** Can view blog posts and add comments.

**JWT Authentication** 

**Blog Post Management:**

-Create, retrieve, update, and delete blog posts.

-Filter posts by category

**Comment Management:**

-All roles can post comments on blog posts and only who commented is the one able to delete of edit tteir comment.

**Category Management:**

-Admins can create, update, and delete blog categories.

-Allows for easy organization and filtering of content.

---

##  Technologies Used

- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **ASP.NET Identity**
- **JWT Authentication**
- **SQL Server**
- **Swagger (API Documentation)**

---



## Authentication

- **Register**: `POST /api/Auth/Register`
- **Login**: `POST /api/Auth/Login`  
Returns **JWT token** which must be added to Authorization header as:
```
Authorization: Bearer <token>
```

---

## 📖 API Endpoints

### **Blog Posts**
- `GET /api/BlogPost` → Get all posts
- `GET /api/BlogPost/{id}` → Get post by ID
- `POST /api/BlogPost` → Create new post
- `PUT /api/BlogPost/{id}` → Update post
- `DELETE /api/BlogPost/{id}` → Delete post (deletes comments too)

### **Categories**
- `GET /api/Category` → Get all categories
- `GET /api/Category/{id}` → Get category by id
- `POST /api/Category` → Create category
- `PUT /api/Category` → Update category
- `DELETE /api/Category/{id}` → Delete category (deletes related posts)

### **Comments**
-`GET /api/Comment/{blogPsstId}` → get comments to a post
- `POST /api/Comment` → Add comment to a post
- `PUT /api/Comment` → Update comment 
- `DELETE /api/Comment/{id}` → Delete comment

---

## ▶ How to Run

1. Clone the repository:
```bash
git clone https://github.com/mariam464/BlogManagerAPI.git
```

2. Navigate to the project directory:
```bash
cd BlogManagerAPI
```

3. Apply migrations & update database:
```bash
dotnet ef database update
```

4. Run the API:
```bash
dotnet run
```

5. Open Swagger UI at:
```
https://localhost:7171/swagger
```

---



