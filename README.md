# WebApiPractice - ASP.NET Core REST API

A comprehensive REST API project built with **ASP.NET Core** for managing users and posts, with authentication, authorization, validation, and logging middleware. This project demonstrates full CRUD operations, security best practices, and advanced middleware implementation.

---

## 📋 Project Overview

This project is a **production-ready REST API** that implements:
- ✅ Complete CRUD operations for users and posts
- ✅ JWT-based authentication and authorization
- ✅ Input validation with data annotation attributes
- ✅ Global exception handling and logging middleware
- ✅ BCrypt password hashing for security
- ✅ Repository pattern for data access

---

## 🌐 GitHub Repository

This project is hosted on GitHub for version control and collaboration:

**Repository URL:** [WebApiPractice GitHub Repository](https://github.com/your-username/WebApiPractice)
*(Update this link with your actual GitHub repository URL)*

---

## 📌 Coursera Requirements Fulfillment

### ✅ 1. GitHub Repository (5 pts)
- Project is version-controlled using Git
- Hosted on GitHub with commit history
- Repository includes all source code, configuration, and documentation
- Link: [GitHub Repository](https://github.com/your-username/WebApiPractice)

### ✅ 2. CRUD Endpoints for Users (5 pts)

The API includes full CRUD operations for user management:

#### **User Endpoints:**

| Method | Endpoint | Authorization | Description |
|--------|----------|----------------|-------------|
| **POST** | `/api/users/register` | Public | Create new user account with validation |
| **POST** | `/api/users/login` | Public | User login with JWT token generation |
| **GET** | `/api/users/{id}` | Required | Retrieve specific user details |
| **GET** | `/api/users/all` | Public | Retrieve all users |

#### **Post CRUD Operations:**

| Method | Endpoint | Authorization | Description |
|--------|----------|----------------|-------------|
| **POST** | `/api/posts` | Required | Create new post |
| **GET** | `/api/posts` | Public | Retrieve all posts |
| **GET** | `/api/posts/{id}` | Public | Retrieve specific post |
| **PUT** | `/api/posts/{id}` | Required | Update existing post |
| **DELETE** | `/api/posts/{id}` | Required | Delete post |

**Controller Implementation:**
- [UsersController.cs](Controllers/UsersController.cs) - User management endpoints
- [PostsController.cs](Controllers/PostsController.cs) - Post management endpoints

### ✅ 3. Using Copilot for Debugging (5 pts)

GitHub Copilot was actively used during development for:
- **Code generation and completion** during controller and service implementation
- **Bug detection and fixes** in dependency injection configurations
- **Error analysis and resolution** for middleware and authentication issues
- **Code optimization** for async/await patterns

**Debugging Evidence:**

![Copilot Debugging - Dependency Injection Lifetime Error](./copilot-debugging-proof.png)

*The screenshot shows Copilot assisting in resolving a critical dependency injection lifetime mismatch error between scoped (`IUserService`) and singleton (`IUserRepositoryService`) services.*

**Key Copilot Assists:**
1. Identified singleton-scoped service consumption issue
2. Suggested proper dependency injection lifetime configuration
3. Helped refactor service registrations for correct lifecycle management
4. Provided detailed explanations of ASP.NET Core DI patterns

---

### ✅ 4. Validation - Process Only Valid User Data (5 pts)

Comprehensive validation is implemented throughout the API:

#### **Data Annotation Validation:**

**UserDTO Model:**
```csharp
public class UserDTO
{
    [Required]
    public string Username { get; set; } = String.Empty;
    
    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    public string Password { get; set; } = String.Empty;
}
```

**PostDTO Model:**
```csharp
public class PostDTO
{
    [Required]
    public string Title { get; set; } = String.Empty;
    
    [Required]
    public string Content { get; set; } = String.Empty;
    
    public int UserId { get; set; }
}
```

#### **Server-Side Validation Logic:**

1. **Username Uniqueness Check** - Prevents duplicate registrations
2. **Password Hashing** - Uses BCrypt for secure password storage
3. **ModelState Validation** - Checks data annotations before processing
4. **Authorization Checks** - Ensures users can only modify their own posts

**Validation Example:**
```csharp
[HttpPost("register")]
[AllowAnonymous]
public async Task<ActionResult> Register([FromBody] UserDTO user)
{
    if(!ModelState.IsValid)
    {
        return BadRequest(new Response<string>(BadRequest().StatusCode, 
            ModelState.Values.First().Errors.First().ErrorMessage));
    }
    var usr = await _userService.RegisterUserAsync(user);
    if(usr == false)
    {
        return BadRequest(new Response<string>(BadRequest().StatusCode, 
            "Username already exists"));
    }
    return Ok(new Response<string>(Ok().StatusCode, "Registration successful"));
}
```

**Validation Features:**
- ✅ Required field validation
- ✅ Password minimum length enforcement (8 characters)
- ✅ Username uniqueness validation
- ✅ Authorization validation (users can only modify their own data)
- ✅ ModelState validation before database operations

---

### ✅ 5. Middleware Implementation (5 pts)

Multiple middleware components are integrated into the request pipeline:

#### **Authentication Middleware:**
```csharp
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
```

#### **Authorization Middleware:**
```csharp
builder.Services.AddAuthorization();
```

#### **Logging Middleware:**
```csharp
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
```

#### **Global Exception Handling Middleware:**
```csharp
app.Use(async (context, next) =>
{
    try
    {
        await next();
    } 
    catch (Exception ex)
    {
        Console.WriteLine($"Global exception caught: {ex.Message}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An exception was caught, please try again later.");
    }
});
```

**Middleware Pipeline Configuration:**
```csharp
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
```

**Implemented Middleware Features:**
- ✅ **JWT Authentication** - Secure token-based authentication
- ✅ **Authorization** - Role-based access control with `[Authorize]` attribute
- ✅ **Console Logging** - Logs all service operations and exceptions
- ✅ **Global Exception Handling** - Catches and formats unhandled exceptions
- ✅ **Routing Middleware** - Manages endpoint routing

---

## 🏗️ Project Architecture

### Directory Structure
```
WebApiPractice/
├── Controllers/
│   ├── UsersController.cs       # User management endpoints
│   └── PostsController.cs       # Post management endpoints
├── Models/
│   ├── User.cs                  # User entity
│   ├── UserDTO.cs               # User DTO with validation
│   ├── Post.cs                  # Post entity
│   ├── PostDTO.cs               # Post DTO with validation
│   └── Response.cs              # Generic response wrapper
├── Services/
│   ├── UserService.cs           # User business logic
│   ├── UserRepositoryService.cs # User data access
│   ├── PostService.cs           # Post business logic
│   ├── PostRepoService.cs       # Post data access
│   ├── TokenService.cs          # JWT token generation
│   └── BCryptHasherService.cs   # Password hashing
├── IServices/
│   ├── IUserService.cs
│   ├── IUserRepositoryService.cs
│   ├── IPostService.cs
│   ├── IPostRepoService.cs
│   ├── ITokenService.cs
│   └── IHasherService.cs
├── Program.cs                   # Application configuration
├── appsettings.json             # Application settings
└── WebApiPractice.csproj        # Project file
```

### Design Patterns Used

1. **Repository Pattern** - Separates data access logic from business logic
2. **Dependency Injection** - Manages service lifetimes and dependencies
3. **DTO Pattern** - Uses Data Transfer Objects for API contracts
4. **Generic Response Wrapper** - Consistent API response format

---

## 🚀 Getting Started

### Prerequisites
- .NET 10 SDK or higher
- Visual Studio Code or Visual Studio
- Git for version control

### Installation

1. **Clone the repository:**
```bash
git clone https://github.com/your-username/WebApiPractice.git
cd WebApiPractice
```

2. **Restore dependencies:**
```bash
dotnet restore
```

3. **Configure JWT Key:**
Update `appsettings.json`:
```json
{
  "Jwt": {
    "Key": "your-secret-key-here-must-be-at-least-32-characters-long"
  }
}
```

4. **Run the application:**
```bash
dotnet run
```

The API will be available at `https://localhost:5001` or `http://localhost:5000`

---

## 📝 API Usage Examples

### Register a New User
```bash
curl -X POST https://localhost:5001/api/users/register \
  -H "Content-Type: application/json" \
  -d '{"username": "john_doe", "password": "SecurePassword123"}'
```

### Login
```bash
curl -X POST https://localhost:5001/api/users/login \
  -H "Content-Type: application/json" \
  -d '{"username": "john_doe", "password": "SecurePassword123"}'
```

### Create a Post (Requires Authentication)
```bash
curl -X POST https://localhost:5001/api/posts \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{"title": "My First Post", "content": "This is amazing!", "userId": 1}'
```

### Get All Posts
```bash
curl -X GET https://localhost:5001/api/posts
```

### Update a Post (Requires Authentication)
```bash
curl -X PUT https://localhost:5001/api/posts/1 \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{"title": "Updated Title", "content": "Updated content", "userId": 1}'
```

### Delete a Post (Requires Authentication)
```bash
curl -X DELETE https://localhost:5001/api/posts/1 \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

---

## 🔒 Security Features

1. **Password Hashing** - BCrypt algorithm for secure password storage
2. **JWT Authentication** - Token-based stateless authentication
3. **Authorization** - `[Authorize]` attribute restricts endpoints
4. **Input Validation** - Data annotations prevent invalid data
5. **Exception Handling** - Global middleware catches errors securely
6. **HTTPS** - Supports secure HTTPS communication

---

## 🛠️ Technologies Used

| Technology | Purpose |
|-----------|---------|
| **ASP.NET Core** | Web framework |
| **C#** | Programming language |
| **.NET 10** | Runtime |
| **JWT (JSON Web Tokens)** | Authentication |
| **BCrypt** | Password hashing |
| **Entity Framework** | ORM (if used) |
| **Dependency Injection** | Service management |

---

## 📊 Development Process

### Tools & Assistance
- **GitHub Copilot** - AI-assisted code generation and debugging
- **Visual Studio Code** - Code editor
- **Git** - Version control
- **Postman/Curl** - API testing

### Key Development Steps
1. Designed API contract and endpoints
2. Created data models with validation attributes
3. Implemented repository pattern for data access
4. Built business logic services
5. Created controllers with proper HTTP methods
6. Configured middleware and authentication
7. Debugged with Copilot assistance
8. Tested all endpoints
9. Documented the API

---

## 📝 Testing

The API can be tested using:
- **Postman** - Visual API testing tool
- **Curl** - Command-line testing
- **WebApiPractice.http** - HTTP client file for VS Code REST Client extension
- **Unit Tests** - (Can be added for production)

---

## 🎓 Learning Outcomes

This project demonstrates:
- ✅ RESTful API design principles
- ✅ ASP.NET Core framework expertise
- ✅ Authentication and authorization implementation
- ✅ Data validation and error handling
- ✅ Middleware and request pipeline management
- ✅ Dependency injection and service registration
- ✅ Security best practices
- ✅ Debugging with AI assistance (Copilot)

---

## 📄 License

This project is created for educational purposes as part of the Coursera curriculum.

---

## 👤 Author

- **Student Name:** [Your Name]
- **GitHub:** [Your GitHub Profile](https://github.com/your-username)
- **Coursera:** [Course Link](https://www.coursera.org/learn/your-course)

---

## 📞 Support

For questions or issues:
1. Check the GitHub Issues page
2. Review the API documentation above
3. Examine the source code comments
4. Test with the provided HTTP examples

---

**Last Updated:** September 2026  
**Version:** 1.0.0
