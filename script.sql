IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Skill] (
    [Id] bigint NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.0000000+00:00',
    [UpdatedAt] datetimeoffset NULL DEFAULT '0001-01-01T00:00:00.0000000+00:00',
    [Deleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_Skill] PRIMARY KEY ([Id])
);

CREATE TABLE [User] (
    [Id] bigint NOT NULL IDENTITY,
    [FullName] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [BirthDate] datetime2 NOT NULL,
    [Active] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.0000000+00:00',
    [UpdatedAt] datetimeoffset NULL DEFAULT '0001-01-01T00:00:00.0000000+00:00',
    [Deleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);

CREATE TABLE [Project] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [IdClient] bigint NOT NULL,
    [IdFreelancer] bigint NOT NULL,
    [TotalCost] DECIMAL(10,2) NOT NULL,
    [StartedAt] datetime2 NULL,
    [CompletedAt] datetime2 NULL,
    [Status] int NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.0000000+00:00',
    [UpdatedAt] datetimeoffset NULL DEFAULT '0001-01-01T00:00:00.0000000+00:00',
    [Deleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_Project] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Project_User_IdClient] FOREIGN KEY ([IdClient]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Project_User_IdFreelancer] FOREIGN KEY ([IdFreelancer]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [UserSkill] (
    [Id] bigint NOT NULL IDENTITY,
    [IdUser] bigint NOT NULL,
    [IdSkill] bigint NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.0000000+00:00',
    [UpdatedAt] datetimeoffset NULL DEFAULT '0001-01-01T00:00:00.0000000+00:00',
    [Deleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_UserSkill] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserSkill_Skill_IdSkill] FOREIGN KEY ([IdSkill]) REFERENCES [Skill] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UserSkill_User_IdUser] FOREIGN KEY ([IdUser]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [ProjectComment] (
    [Id] bigint NOT NULL IDENTITY,
    [Content] nvarchar(max) NOT NULL,
    [IdProject] bigint NOT NULL,
    [IdUser] bigint NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL DEFAULT '0001-01-01T00:00:00.0000000+00:00',
    [UpdatedAt] datetimeoffset NULL DEFAULT '0001-01-01T00:00:00.0000000+00:00',
    [Deleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    CONSTRAINT [PK_ProjectComment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectComment_Project_IdProject] FOREIGN KEY ([IdProject]) REFERENCES [Project] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProjectComment_User_IdUser] FOREIGN KEY ([IdUser]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);

CREATE INDEX [IX_Project_IdClient] ON [Project] ([IdClient]);

CREATE INDEX [IX_Project_IdFreelancer] ON [Project] ([IdFreelancer]);

CREATE INDEX [IX_ProjectComment_IdProject] ON [ProjectComment] ([IdProject]);

CREATE INDEX [IX_ProjectComment_IdUser] ON [ProjectComment] ([IdUser]);

CREATE INDEX [IX_UserSkill_IdSkill] ON [UserSkill] ([IdSkill]);

CREATE INDEX [IX_UserSkill_IdUser] ON [UserSkill] ([IdUser]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241115032136_Initial', N'9.0.0');

COMMIT;
GO

