# Squares API

Squares API is a .NET Core Web API that detects squares from 2D point coordinates. Import, manage, and query square formations easily.

---

## ✨ Features

- Import, add, and delete points
- Detect unique squares
- RESTful API with Swagger UI
- Fully tested and Dockerized

---

## 🚀 Run Locally

```bash
dotnet build
dotnet run

---

Open Swagger: http://localhost:8080/swagger


## ------------ ## -----------------

docker build -t squares-api .
docker run -d -p 5000:8080 --name squares-api-container squares-api

---

Open: http://localhost:5000/swagger

# 📌 API Endpoints
Method	Endpoint	Description
POST	/points/import	Import list of points
POST	/points	Add a single point
GET	/points	Get all points
DELETE	/points/{id}	Delete point by ID
GET	/squares	Get detected squares

