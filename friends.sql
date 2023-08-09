
drop database friends_users_db;

create database friends_users_db;

use friends_users_db;

CREATE TABLE Users (
    UserID VARCHAR(10) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    MobileNumber VARCHAR(10) NOT NULL,
    BloodGroup NVARCHAR(10) NOT NULL,
    State NVARCHAR(50) NOT NULL,
    District NVARCHAR(50) NOT NULL,
    City NVARCHAR(50) NOT NULL,
    Availability BIT NOT NULL,
    EmailID NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(128) NOT NULL
);

select * from Users;

