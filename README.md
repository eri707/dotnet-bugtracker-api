# BugTracker API
This API is a middle tier application meant to help you log bugs against projects. It’s a simple implementation of bug tracking features found in software like Jira, Asana and the like.

# Motivation
The purpose of this project is to practice building RESTful APIs using ASP.NET and SQL, and learning about Microsoft Azure.

## Table of Contents
### General Info 
The code for this project is available for your perusal here. The project itself is hosted in Azure at https://bug-tracker.azurewebsites.net and can be interacted with via the Swagger UI.

### Technologies
Project is created with:
* C# ASP.NET
* In-memory cache
* SQL
### SetUp
To run this project, clone the repository, then...

```
$ cd BugTracker
$ dotnet install
$ dotnet start

```
### Features
The API will have two categories; Projects and Bugs. You can have multiple projects, and each project can have multiple bugs. Bugs are linked to projects via the ProjectId property. Users must add a project first, before they can add bugs. Adding bugs to non-existing projects will return an error.

