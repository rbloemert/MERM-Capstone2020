DELETE FROM project;
DELETE FROM timeline;
DELETE FROM timeline_link;
SET IDENTITY_INSERT project ON
INSERT INTO project(projectID, project_creation, project_name, project_desc, project_image_path)VALUES(1, CURRENT_TIMESTAMP, 'SampleProject', 'This is a sample project...', '');
SET IDENTITY_INSERT project OFF
SET IDENTITY_INSERT timeline ON
INSERT INTO timeline(timelineID, timeline_creation, timeline_name, timeline_desc, timeline_image_path, timeline_file_path)VALUES
(1, CURRENT_TIMESTAMP, 'Project Update #1', 'This is a test description...', '/Images/Test.png', ''),
(2, CURRENT_TIMESTAMP, 'Project Update #2', 'This is a test description...', '/Images/Test.png', ''),
(3, CURRENT_TIMESTAMP, 'Project Update #3', 'This is a test description...', '/Images/Test.png', ''),
(4, CURRENT_TIMESTAMP, 'Project Update #4', 'This is a test description...', '/Images/Test.png', ''),
(5, CURRENT_TIMESTAMP, 'Project Update #5', 'This is a test description...', '/Images/Test.png', ''),
(6, CURRENT_TIMESTAMP, 'Project Update #6', 'This is a test description...', '/Images/Test.png', '');
SET IDENTITY_INSERT timeline OFF
INSERT INTO timeline_link(timelineID, project_owner_projectID)VALUES(1, 1);
INSERT INTO timeline_link(timelineID, project_owner_projectID)VALUES(2, 1);
INSERT INTO timeline_link(timelineID, project_owner_projectID)VALUES(3, 1);
INSERT INTO timeline_link(timelineID, project_owner_projectID)VALUES(4, 1);
INSERT INTO timeline_link(timelineID, project_owner_projectID)VALUES(5, 1);
INSERT INTO timeline_link(timelineID, project_owner_projectID)VALUES(6, 1);

UPDATE timeline SET timeline_image_path = 'https://i.imgur.com/EwkODXj.png' WHERE timelineID <= 6;

SELECT * FROM timeline;
SELECT * FROM comment;
SELECT * FROM comment_link;