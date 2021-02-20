-- Basic SQL table creation...

/* Drop foreign key lookup tables */
DROP TABLE IF EXISTS project_link;
DROP TABLE IF EXISTS timeline_link;
DROP TABLE IF EXISTS comment_link;
DROP TABLE IF EXISTS follower_link;

/* Creates the account table. */
DROP TABLE IF EXISTS account;
CREATE TABLE account(
	accountID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,                       /* accountID: Unique Account ID per user. Auto incremented */
    account_creation DATE NOT NULL,                                         /* accountCreation: When this account was created. */
    firstname VARCHAR(255) NOT NULL,                                        /* firstname: The first name for this account. */
    lastname VARCHAR(255) NOT NULL,                                         /* lastname: The last name for this account. */
    username VARCHAR(255) NOT NULL,                                         /* username: The username for this account. */
    password VARCHAR(255) NOT NULL,                                         /* password: The password for this account. */
    email VARCHAR(255) NOT NULL,                                            /* email: The email for this account. */
    isSiteAdministrator BIT NOT NULL,                                       /* isSiteAdministrator: Is this user an administrator? */
);

/* Creates the project table. */
DROP TABLE IF EXISTS project;
CREATE TABLE project(
    projectID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,                       /* projectID: Unique Project ID per project. Auto incremented. */
    project_creation DATE NOT NULL,                                         /* project_creation: When this project was created. */
    project_name VARCHAR(255) NOT NULL,                                     /* project_name: Name of this project. */
    project_desc VARCHAR(255),                                              /* project_desc: Description of this project. */
);

/* Creates the timeline table. */
DROP TABLE IF EXISTS timeline;
CREATE TABLE timeline(
    timelineID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,                      /* timelineID: Unique Timeline ID per timeline. Auto incremented. */
    timeline_creation DATE NOT NULL,                                        /* timeline_creation: Date this timeline was posted. */
    timeline_name VARCHAR(255) NOT NULL,                                    /* timeline_name: Title of this timeline. */
    timeline_desc VARCHAR(255) NOT NULL,                                    /* timeline_desc: Description of this timeline. */
);

/* Creates the comment table. */
DROP TABLE IF EXISTS comment;
CREATE TABLE comment(
    commentID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,                       /* commentID: Unique Comment ID per comment. Auto incremented. */
    comment_creation DATE NOT NULL,                                         /* comment_creation: Date this comment was posted. */
    comment_text VARCHAR(255) NOT NULL,                                     /* commenttext: The text in this comment. */
);

/* Creates the project_link table. Used to link a project to the account owner. */
CREATE TABLE project_link(
    projectID INT NOT NULL FOREIGN KEY REFERENCES project(projectID),                       /* projectID: This project ID. */
    project_owner_accountID INT NOT NULL FOREIGN KEY REFERENCES account(accountID),         /* project_owner_accountID: Owner account of this project. */
)

/* Creates the timeline_link table. Used to link a timeline to a project owner. */
CREATE TABLE timeline_link(
    timelineID INT NOT NULL FOREIGN KEY REFERENCES timeline(timelineID),                    /* timelineID: This timeline ID. */
    project_owner_projectID INT NOT NULL FOREIGN KEY REFERENCES project(projectID),         /* project_owner_projectID: Owner Project of this timeline */
)

/* Creates the comment_link table. Used to link a comment to the account owner and timeline owner. */
CREATE TABLE comment_link(
    commentID INT NOT NULL FOREIGN KEY REFERENCES timeline(timelineID),                     /* commentID: This comment ID. */
    timeline_owner_timelineID INT NOT NULL FOREIGN KEY REFERENCES timeline(timelineID),     /* timeline_owner_timelineID: The timeline this comment was posted on */
    comment_owner_accountID INT NOT NULL FOREIGN KEY REFERENCES account(accountID),         /* comment_owner_accountID: Owner account of this comment. */
)

/* Creates the follower_link table. Used to keep track of followers of a project. */
CREATE TABLE follower_link(
    projectID INT NOT NULL FOREIGN KEY REFERENCES account(accountID),                       /* projectID: The followed project ID. */
    follower_accountID INT NOT NULL FOREIGN KEY REFERENCES account(accountID),              /* follower_accountID: The account ID of the follower. */
    follow_date DATE NOT NULL                                                               /* follow_date: The date this follow occured. */
)