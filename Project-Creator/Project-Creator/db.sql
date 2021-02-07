-- Basic SQL table creation...

/* Creates the account table. */
DROP TABLE IF EXISTS account;
CREATE TABLE account(
	accountID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,                       /* accountID: Unique Account ID per user. Auto incremented */
    accountimage IMAGE,                                                     /* accountImage: An image for this account. Optional. */
    firstname VARCHAR(255) NOT NULL,                                        /* firstname: The first name for this account. */
    lastname VARCHAR(255) NOT NULL,                                         /* lastname: The last name for this account. */
    username VARCHAR(255) NOT NULL,                                         /* username: The username for this account. */
    password VARCHAR(255) NOT NULL,                                         /* password: The password for this account. */
    email VARCHAR(255) NOT NULL,                                            /* email: The email for this account. */
    projects INT FOREIGN KEY REFERENCES project(projectID)                  /* projects: All projects owned by this account. */
);

/* Creates the project table. */
DROP TABLE IF EXISTS project;
CREATE TABLE project(
    projectID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,                       /* projectID: Unique Project ID per project. Auto incremented. */
    projectname VARCHAR(255) NOT NULL,                                      /* projectname: Name of this project. */
    projectdesc VARCHAR(255),                                               /* projectdesc: Description of this project. */
    projectowner INT FOREIGN KEY REFERENCES account(accountID),             /* projectowner: Owner of this project. */
    timelines INT FOREIGN KEY REFERENCES timeline(timelineID)               /* timelines: All timelines part of this project. */
);

/* Creates the timeline table. */
DROP TABLE IF EXISTS timeline;
CREATE TABLE timeline(
    timelineID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,                      /* timelineID: Unique Timeline ID per timeline. Auto incremented. */
    timelinedate DATE NOT NULL,                                             /* timelinedate: Date this timeline was posted. */
    projectowner INT NOT NULL FOREIGN KEY REFERENCES project(projectID),    /* projectowner: Owner Project of this timeline */
    comments INT FOREIGN KEY REFERENCES comment(commentID)                  /* comments: All comments part of this timeline. */
);

/* Creates the comment table. */
DROP TABLE IF EXISTS comment;
CREATE TABLE comment(
    commentID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,                       /* commentID: Unique Comment ID per comment. Auto incremented. */
    commentdate DATE NOT NULL,                                              /* commentdate: Date this comment was posted. */
    commentowner INT NOT NULL FOREIGN KEY REFERENCES account(accountID),    /* commentowner: Owner of this comment. */
    commenttext VARCHAR(255) NOT NULL,                                      /* commenttext: The text in this comment. */
);