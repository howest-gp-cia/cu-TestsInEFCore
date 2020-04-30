# EFBlog

## Summary
* **Course**: Continuous Integration Advanced
* **Chapter**: CIA Testing Entity Framework Blogs
* **Example #**: 4

## Branches
* **master**: working example

## Use

* Visual Studio startup project: **EFCore.Mvc**
* Nuget Package Manager console:
   *   default project src\EFBlog.Domain

* You can start from scratch: remove **Migrations** folder in **Domain** project

  ````
  Add-Migration InitialCreate
  ````
* Create and seed database
  ````
  Update-Database
  ````

*  Or use equivalent Powershell commands
  ````
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ````





&copy; Graduaat Programmeren / Associate Degree Programming
**Howest**
  