
/* Uses the cloud database. */
USE heroku_57d61cda850cb7a;

/* Creates the creators table. */
DROP TABLE IF EXISTS users;
CREATE TABLE users(
	id INT PRIMARY KEY auto_increment,
    username TINYTEXT NOT NULL,
    firstname TINYTEXT NOT NULL,
    lastname TINYTEXT NOT NULL,
    password TINYTEXT NOT NULL,
    email TINYTEXT NOT NULL
);