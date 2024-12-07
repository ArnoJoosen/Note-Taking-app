CREATE USER 'apiuser'@'%' IDENTIFIED BY 'apipassword';
GRANT ALL PRIVILEGES ON appdatabase.* TO 'apiuser'@'%';
FLUSH PRIVILEGES;
