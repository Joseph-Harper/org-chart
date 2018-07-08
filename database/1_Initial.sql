USE OrgChart;
GO

DROP TABLE IF EXISTS Settings;
GO
DROP TABLE IF EXISTS Person;
GO
DROP TABLE IF EXISTS Organization;
GO
DROP INDEX IF EXISTS [AspNetUsers].[UserNameIndex];
GO
DROP INDEX IF EXISTS [AspNetUsers].[EmailIndex];
GO
DROP INDEX IF EXISTS [AspNetUserRoles].[IX_AspNetUserRoles_UserId];
GO
DROP INDEX IF EXISTS [AspNetUserRoles].[IX_AspNetUserRoles_RoleId];
GO
DROP INDEX IF EXISTS [AspNetUserLogins].[IX_AspNetUserLogins_UserId];
GO
DROP INDEX IF EXISTS [AspNetUserClaims].[IX_AspNetUserClaims_UserId];
GO
DROP INDEX IF EXISTS [AspNetUserClaims].[IX_AspNetRoleClaims_RoleId];
GO
DROP INDEX IF EXISTS [AspNetRoles].[RoleNameIndex];
GO
DROP TABLE IF EXISTS [AspNetUserRoles];
GO
DROP TABLE IF EXISTS [AspNetUserLogins];
GO
DROP TABLE IF EXISTS [AspNetUserClaims];
GO
DROP TABLE IF EXISTS [AspNetRoleClaims];
GO
DROP TABLE IF EXISTS [AspNetUsers];
GO
DROP TABLE IF EXISTS [AspNetUserTokens];
GO
DROP TABLE IF EXISTS [AspNetRoles];
GO
CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO
CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name])
);
GO
CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Email] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [LockoutEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [PasswordHash] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [UserName] nvarchar(256) NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO
CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO
CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]);
GO
CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO
CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO
CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO
CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO
CREATE INDEX [IX_AspNetUserRoles_UserId] ON [AspNetUserRoles] ([UserId]);
GO
CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO
CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]);
GO
CREATE TABLE [Settings] (
	[DatabaseVersion] int NOT NULL
);
GO
CREATE TABLE [Organization] (
    [OrganizationId] int NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
	[UserId] nvarchar(450) NOT NULL,
	CONSTRAINT [PK_Organization] PRIMARY KEY ([OrganizationId])
);
GO
CREATE TABLE [Person] (
    [PersonId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(255) NOT NULL,
    [LastName] nvarchar(255) NOT NULL,
    [EmailAddress] nvarchar(255) NULL,
    [PhoneNumber] nvarchar(20) NULL,
    [Title] nvarchar(100) NULL,
    [ReportsToId] int FOREIGN KEY REFERENCES [Person]([PersonId]) NULL,
    [OrganizationId] int FOREIGN KEY REFERENCES [Organization]([OrganizationId]) NULL,
	[UserId] nvarchar(450) NOT NULL,
	CONSTRAINT [PK_Person] PRIMARY KEY ([PersonId])
);
GO
INSERT INTO [Settings] ([DatabaseVersion])
VALUES (1);