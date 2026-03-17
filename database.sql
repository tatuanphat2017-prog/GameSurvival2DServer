USE GameSurvival2D;
GO

-- 1. Account
CREATE TABLE Accounts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100),
    PasswordHash NVARCHAR(255),
    IsBanned BIT DEFAULT 0,
    Role NVARCHAR(20) DEFAULT 'User',
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 2. PlayerProfile
CREATE TABLE PlayerProfiles (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT,
    DisplayName NVARCHAR(50),
    Level INT DEFAULT 1,
    TotalPlayTime INT DEFAULT 0,
    FOREIGN KEY (AccountId) REFERENCES Accounts(Id)
);

-- 3. SaveGame
CREATE TABLE SaveGames (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT,
    Slot INT,
    PosX FLOAT,
    PosY FLOAT,
    PlayerHP INT,
    PlayerStamina INT,
    SavedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (AccountId) REFERENCES Accounts(Id)
);

-- 4. Inventory
CREATE TABLE Inventory (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT,
    ItemName NVARCHAR(100),
    Quantity INT,
    FOREIGN KEY (AccountId) REFERENCES Accounts(Id)
);

-- 5. Leaderboard
CREATE TABLE Leaderboards (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT,
    Score INT,
    SurvivalTime INT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (AccountId) REFERENCES Accounts(Id)
);

-- 6. AdminLog
CREATE TABLE AdminLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AdminId INT,
    TargetAccountId INT,
    Reason NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE()
);