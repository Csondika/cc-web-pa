# Full stack website for a fake restaurant chain (in progress)

This is an unfinished project made with combining MVC and SPA. The page supports login, registration, user details modification (CRUD - without Delete) and some standard admin priviliges. Dynamic DOM-building happens under the Menu and Menu Controls (admin only) tabs, anything else is ASP.Net Core MVC.

Setup:
- PostgreSQL 11 or 12 (not tested on other versions).
- Root folder contains a .sql file to load test data into a PostgreSQL. Simply create and name the database "healthbar".
- You may need to change connection string in Startup class if you host your db on another host/port.
- Run webapp with your favourite IDE
- Users to test the app (you can registrate your own users - except admin), both passwords are "asdasd": 
  - test@mail.com
  - admin@mail.com (has admin privilages)
  
 Admin privilage:
 - admin can activate/deactivate menus that users can choose from (there is no cart yet, Add button has no funcionality for users)
