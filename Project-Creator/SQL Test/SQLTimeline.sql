SET IDENTITY_INSERT project ON
INSERT INTO project(projectID)VALUES(1);
SET IDENTITY_INSERT project OFF
SET IDENTITY_INSERT timeline ON
INSERT INTO timeline(timelineID, timeline_creation, timeline_name, timeline_desc, timeline_image_path, timeline_file_path)VALUES
(1, CURRENT_TIMESTAMP, 'Project Update #1', 'This is a test description...', '../Images/Test.png', ''),
(2, CURRENT_TIMESTAMP, 'Project Update #2', 'This is a test description...', '../Images/Test.png', ''),
(3, CURRENT_TIMESTAMP, 'Project Update #3', 'This is a test description...', '../Images/Test.png', ''),
(4, CURRENT_TIMESTAMP, 'Project Update #4', 'This is a test description...', '../Images/Test.png', ''),
(5, CURRENT_TIMESTAMP, 'Project Update #5', 'This is a test description...', '../Images/Test.png', ''),
(6, CURRENT_TIMESTAMP, 'Project Update #6', 'This is a test description...', '../Images/Test.png', '');
SET IDENTITY_INSERT timeline OFF
INSERT INTO timeline_link(timelineID, project_owner_projectID)VALUES(1, 1);
INSERT INTO timeline_link(timelineID, project_owner_projectID)VALUES(1, 2);
INSERT INTO timeline_link(timelineID, project_owner_projectID)VALUES(1, 3);
INSERT INTO timeline_link(timelineID, project_owner_projectID)VALUES(1, 4);
INSERT INTO timeline_link(timelineID, project_owner_projectID)VALUES(1, 5);
INSERT INTO timeline_link(timelineID, project_owner_projectID)VALUES(1, 6);