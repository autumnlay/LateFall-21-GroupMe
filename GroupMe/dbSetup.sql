CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS groups(
  id int NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'group Name',
  picture varchar(255) COMMENT 'group Picture',
  description varchar(255) COMMENT 'group Picture',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: AccountId',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS groupMembers(
  id INT AUTO_INCREMENT NOT NULL PRIMARY Key,
  accountId VARCHAR(255) NOT NULL,
  groupId INT NOT NULL,
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (groupId) REFERENCES groups(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
SELECT
  g.*,
  a.*
FROM
  groups g
  JOIN accounts a ON g.creatorId = a.id
WHERE
  g.name LIKE '%first%';