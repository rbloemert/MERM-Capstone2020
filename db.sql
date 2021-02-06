
/* Uses the cloud database. */
USE heroku_57d61cda850cb7a;

/* Creates the creators table. */
DROP TABLE IF EXISTS users;
CREATE TABLE users(
	u_id INT PRIMARY KEY auto_increment,
    u_username TINYTEXT NOT NULL,
    u_firstname TINYTEXT NOT NULL,
    u_lastname TINYTEXT NOT NULL,
    u_password TINYTEXT NOT NULL,
    u_email TINYTEXT NOT NULL
);