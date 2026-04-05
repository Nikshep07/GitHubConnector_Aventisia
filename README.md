# 🚀 GitHub Cloud Connector (ASP.NET Core)

## 📌 Overview

This project is a **GitHub Cloud Connector** built using ASP.NET Core Web API.
It integrates with GitHub APIs to perform operations like fetching repositories and creating issues.

The application demonstrates:

* External API integration
* Authentication handling using Personal Access Token (PAT)
* Clean backend architecture using Controller-Service pattern

---

## ✨ Features

* 🔐 Secure authentication using GitHub PAT (via User Secrets)
* 📂 Fetch public repositories of a user
* 🐞 Create issues in a GitHub repository
* 🧩 Clean and modular code structure

---

## 🛠 Tech Stack

* ASP.NET Core Web API (.NET 6/7)
* C#
* HttpClient (API integration)
* Newtonsoft.Json (JSON handling)

---

## ⚙️ Setup Instructions

### 1. Clone Repository

```bash
git clone https://github.com/your-username/GitHubConnector_Aventisia.git
cd GitHubConnector_Aventisia
```

---

### 2. Configure GitHub Token (Secure Way)

This project uses **.NET User Secrets** to store sensitive data securely.

Run the following commands:

```bash
dotnet user-secrets init
dotnet user-secrets set "GitHub:Token" "your_personal_access_token_here"
```

> ⚠️ Make sure your token has `repo` permissions.

---

### 3. Run the Application

Open in Visual Studio 2022 and run:

```
F5 or Click Run
```

Application will start on:

```
https://localhost:7267
```

---

## 🔗 API Endpoints

### 📥 Get Repositories

**GET** `/api/github/repos?username={username}`

Example:

```
https://localhost:7267/api/github/repos?username=octocat
```

Response:

```json
[
  {
    "name": "repo-name",
    "url": "https://github.com/user/repo",
    "isPrivate": false
  }
]
```

---

### 🐞 Create Issue

**POST** `/api/github/create-issue`

Request Body:

```json
{
  "owner": "your-username",
  "repo": "your-repo",
  "title": "Test Issue",
  "body": "Created using API"
}
```

---

## ⚠️ Security Note

* GitHub tokens are **not stored in source code**
* Secrets are managed using **.NET User Secrets**
* Any previously exposed tokens have been revoked

---

## 🎯 Project Highlights

* Real-time GitHub API integration
* RESTful API design
* Secure configuration handling
* Error handling for API responses

---

## 📌 Future Improvements (Optional)

* OAuth 2.0 authentication (instead of PAT)
* Add endpoint to list issues
* Add Swagger UI for API testing

---

## 👨‍💻 Author

Developed as part of a backend assignment to demonstrate API integration and secure coding practices.

---
