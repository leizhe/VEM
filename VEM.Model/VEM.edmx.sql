
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/03/2016 16:35:03
-- Generated from EDMX file: E:\SvnCode\VEM\VEM.Model\VEM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VEM];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CityDistrict]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DistrictSet] DROP CONSTRAINT [FK_CityDistrict];
GO
IF OBJECT_ID(N'[dbo].[FK_CityMachine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MachineSet] DROP CONSTRAINT [FK_CityMachine];
GO
IF OBJECT_ID(N'[dbo].[FK_DistrictMachine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MachineSet] DROP CONSTRAINT [FK_DistrictMachine];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoleSet] DROP CONSTRAINT [FK_UserUserRole];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleUserRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoleSet] DROP CONSTRAINT [FK_RoleUserRole];
GO
IF OBJECT_ID(N'[dbo].[FK_MenuButton]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ButtonSet] DROP CONSTRAINT [FK_MenuButton];
GO
IF OBJECT_ID(N'[dbo].[FK_CommodContainerRoad]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContainerRoadSet] DROP CONSTRAINT [FK_CommodContainerRoad];
GO
IF OBJECT_ID(N'[dbo].[FK_CommodSalesHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesHistorySet] DROP CONSTRAINT [FK_CommodSalesHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_MachineModelMachine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MachineSet] DROP CONSTRAINT [FK_MachineModelMachine];
GO
IF OBJECT_ID(N'[dbo].[FK_MachineContainerRoad]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContainerRoadSet] DROP CONSTRAINT [FK_MachineContainerRoad];
GO
IF OBJECT_ID(N'[dbo].[FK_MachineSalesHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SalesHistorySet] DROP CONSTRAINT [FK_MachineSalesHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMachine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MachineSet] DROP CONSTRAINT [FK_UserMachine];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberPayRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayRecordSet] DROP CONSTRAINT [FK_MemberPayRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_MemberMemberCoupon]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberCouponSet] DROP CONSTRAINT [FK_MemberMemberCoupon];
GO
IF OBJECT_ID(N'[dbo].[FK_CityUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_User] DROP CONSTRAINT [FK_CityUser];
GO
IF OBJECT_ID(N'[dbo].[FK_DistrictUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_User] DROP CONSTRAINT [FK_DistrictUser];
GO
IF OBJECT_ID(N'[dbo].[FK_MachineShipmentStatusRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShipmentStatusRecordSet] DROP CONSTRAINT [FK_MachineShipmentStatusRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_CouponMemberCoupon]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MemberCouponSet] DROP CONSTRAINT [FK_CouponMemberCoupon];
GO
IF OBJECT_ID(N'[dbo].[FK_CommodShipmentStatusRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShipmentStatusRecordSet] DROP CONSTRAINT [FK_CommodShipmentStatusRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_MachineTemMachine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MachineTemSet] DROP CONSTRAINT [FK_MachineTemMachine];
GO
IF OBJECT_ID(N'[dbo].[FK_MachineError]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FaultSet] DROP CONSTRAINT [FK_MachineError];
GO
IF OBJECT_ID(N'[dbo].[FK_User_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_User] DROP CONSTRAINT [FK_User_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_Member_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_Member] DROP CONSTRAINT [FK_Member_inherits_Person];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[MachineModelSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MachineModelSet];
GO
IF OBJECT_ID(N'[dbo].[CitySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CitySet];
GO
IF OBJECT_ID(N'[dbo].[DistrictSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DistrictSet];
GO
IF OBJECT_ID(N'[dbo].[UserRoleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRoleSet];
GO
IF OBJECT_ID(N'[dbo].[RoleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleSet];
GO
IF OBJECT_ID(N'[dbo].[PrivilegeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PrivilegeSet];
GO
IF OBJECT_ID(N'[dbo].[MenuSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MenuSet];
GO
IF OBJECT_ID(N'[dbo].[ButtonSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ButtonSet];
GO
IF OBJECT_ID(N'[dbo].[CommodSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommodSet];
GO
IF OBJECT_ID(N'[dbo].[SalesHistorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SalesHistorySet];
GO
IF OBJECT_ID(N'[dbo].[ContainerRoadSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContainerRoadSet];
GO
IF OBJECT_ID(N'[dbo].[IconSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IconSet];
GO
IF OBJECT_ID(N'[dbo].[MachineSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MachineSet];
GO
IF OBJECT_ID(N'[dbo].[PersonSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet];
GO
IF OBJECT_ID(N'[dbo].[PayRecordSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayRecordSet];
GO
IF OBJECT_ID(N'[dbo].[CouponSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CouponSet];
GO
IF OBJECT_ID(N'[dbo].[MemberCouponSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MemberCouponSet];
GO
IF OBJECT_ID(N'[dbo].[ShipmentStatusRecordSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShipmentStatusRecordSet];
GO
IF OBJECT_ID(N'[dbo].[MachineTemSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MachineTemSet];
GO
IF OBJECT_ID(N'[dbo].[FaultSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FaultSet];
GO
IF OBJECT_ID(N'[dbo].[BrowserPrivilegeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BrowserPrivilegeSet];
GO
IF OBJECT_ID(N'[dbo].[PersonSet_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet_User];
GO
IF OBJECT_ID(N'[dbo].[PersonSet_Member]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet_Member];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MachineModelSet'
CREATE TABLE [dbo].[MachineModelSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ContainerRoadCount] int  NOT NULL,
    [ModelCode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CitySet'
CREATE TABLE [dbo].[CitySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CityName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DistrictSet'
CREATE TABLE [dbo].[DistrictSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DistrictName] nvarchar(max)  NOT NULL,
    [City_Id] int  NOT NULL
);
GO

-- Creating table 'UserRoleSet'
CREATE TABLE [dbo].[UserRoleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [User_Id] int  NOT NULL,
    [Role_Id] int  NOT NULL
);
GO

-- Creating table 'RoleSet'
CREATE TABLE [dbo].[RoleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(max)  NOT NULL,
    [CreateUserID] int  NULL,
    [CreateDate] datetime  NULL,
    [ModifyUserID] int  NULL,
    [ModifyDate] datetime  NULL,
    [IsEnable] bit  NULL
);
GO

-- Creating table 'PrivilegeSet'
CREATE TABLE [dbo].[PrivilegeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PrivilegeMaster] int  NOT NULL,
    [PrivilegeMasterValue] int  NOT NULL,
    [PrivilegeAccess] int  NOT NULL,
    [PrivilegeAccessValue] int  NOT NULL
);
GO

-- Creating table 'MenuSet'
CREATE TABLE [dbo].[MenuSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MenuNo] nvarchar(max)  NOT NULL,
    [MenuParentNo] nvarchar(max)  NOT NULL,
    [MenuUrl] nvarchar(max)  NOT NULL,
    [MenuName] nvarchar(max)  NOT NULL,
    [MenuOrder] int  NOT NULL,
    [IsVisible] bit  NOT NULL,
    [IsLeaf] bit  NOT NULL,
    [MenuPic] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ButtonSet'
CREATE TABLE [dbo].[ButtonSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BtnName] nvarchar(max)  NOT NULL,
    [BtnNo] nvarchar(max)  NOT NULL,
    [InitStatus] int  NOT NULL,
    [BtnIcon] nvarchar(max)  NULL,
    [Menu_Id] int  NOT NULL
);
GO

-- Creating table 'CommodSet'
CREATE TABLE [dbo].[CommodSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Pic] nvarchar(max)  NULL,
    [CreateDate] datetime  NULL,
    [Describle] nvarchar(max)  NULL,
    [Code] nvarchar(max)  NULL,
    [CreateUserID] int  NULL,
    [ModifyDate] datetime  NULL,
    [ModifyUserID] int  NULL,
    [Unit] nvarchar(max)  NULL,
    [Capacity] nvarchar(max)  NULL,
    [Price] float  NULL
);
GO

-- Creating table 'SalesHistorySet'
CREATE TABLE [dbo].[SalesHistorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [SalesDate] datetime  NOT NULL,
    [SalePrice] float  NOT NULL,
    [Commod_Id] int  NOT NULL,
    [Machine_Id] int  NOT NULL
);
GO

-- Creating table 'ContainerRoadSet'
CREATE TABLE [dbo].[ContainerRoadSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GroupNumber] int  NOT NULL,
    [IsEnable] bit  NOT NULL,
    [Number] int  NOT NULL,
    [ReamainderCount] int  NULL,
    [MaxCount] int  NULL,
    [MotorStatus] int  NULL,
    [Commod_Id] int  NOT NULL,
    [Machine_Id] int  NOT NULL
);
GO

-- Creating table 'IconSet'
CREATE TABLE [dbo].[IconSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Css] nvarchar(max)  NULL
);
GO

-- Creating table 'MachineSet'
CREATE TABLE [dbo].[MachineSet] (
    [Address] nvarchar(max)  NULL,
    [CreateUserID] int  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [ModifyUserID] int  NULL,
    [ModifyDate] datetime  NULL,
    [MachineCode] nvarchar(max)  NOT NULL,
    [IsOnline] bit  NULL,
    [SoftwareId] nvarchar(max)  NULL,
    [HardwareId] nvarchar(max)  NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [HighRefriTem] float  NULL,
    [LowRefriTem] float  NULL,
    [RentOrSell] bit  NULL,
    [City_Id] int  NOT NULL,
    [District_Id] int  NOT NULL,
    [MachineModel_Id] int  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LoginName] nvarchar(max)  NOT NULL,
    [LoginPassword] nvarchar(max)  NOT NULL,
    [IsEnabled] bit  NULL,
    [Email] nvarchar(max)  NULL,
    [Tel] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NULL,
    [PicUrl] nvarchar(max)  NULL
);
GO

-- Creating table 'PayRecordSet'
CREATE TABLE [dbo].[PayRecordSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Money] decimal(18,0)  NOT NULL,
    [CreateDate] datetime  NULL,
    [IncomeOrExpenses] int  NULL,
    [Member_Id] int  NOT NULL
);
GO

-- Creating table 'CouponSet'
CREATE TABLE [dbo].[CouponSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] int  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL,
    [Begintime] datetime  NOT NULL,
    [EndTime] datetime  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CreateID] int  NOT NULL
);
GO

-- Creating table 'MemberCouponSet'
CREATE TABLE [dbo].[MemberCouponSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PayStatus] bit  NOT NULL,
    [Member_Id] int  NOT NULL,
    [Coupon_Id] int  NOT NULL
);
GO

-- Creating table 'ShipmentStatusRecordSet'
CREATE TABLE [dbo].[ShipmentStatusRecordSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IdentityNumber] nvarchar(max)  NULL,
    [ShipmentStatus] int  NULL,
    [FailedCode] int  NULL,
    [CreateDate] datetime  NULL,
    [Machine_Id] int  NOT NULL,
    [Commod_Id] int  NOT NULL
);
GO

-- Creating table 'MachineTemSet'
CREATE TABLE [dbo].[MachineTemSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Tem1] float  NULL,
    [Tem2] float  NULL,
    [Tem3] float  NULL,
    [Tem4] float  NULL,
    [Machine_Id] int  NOT NULL
);
GO

-- Creating table 'FaultSet'
CREATE TABLE [dbo].[FaultSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] int  NULL,
    [Cause] int  NULL,
    [CreateTime] datetime  NULL,
    [SucceedTime] datetime  NULL,
    [Machine_Id] int  NOT NULL
);
GO

-- Creating table 'BrowserPrivilegeSet'
CREATE TABLE [dbo].[BrowserPrivilegeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MachinePrivilege] bit  NOT NULL,
    [CommodPrivilege] bit  NOT NULL,
    [MemberPrivilege] bit  NOT NULL,
    [PrivilegeMaster] int  NOT NULL,
    [PrivilegeMasterValue] int  NOT NULL,
    [CouponPrivilege] bit  NOT NULL
);
GO

-- Creating table 'PersonSet_User'
CREATE TABLE [dbo].[PersonSet_User] (
    [CompanyName] nvarchar(max)  NULL,
    [CreateUserID] int  NULL,
    [CreateDate] datetime  NULL,
    [ModifyUserID] int  NULL,
    [ModifyDate] datetime  NULL,
    [Id] int  NOT NULL,
    [City_Id] int  NOT NULL,
    [District_Id] int  NOT NULL
);
GO

-- Creating table 'PersonSet_Member'
CREATE TABLE [dbo].[PersonSet_Member] (
    [CreateDate] datetime  NULL,
    [CreateUserID] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'MachineModelSet'
ALTER TABLE [dbo].[MachineModelSet]
ADD CONSTRAINT [PK_MachineModelSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CitySet'
ALTER TABLE [dbo].[CitySet]
ADD CONSTRAINT [PK_CitySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DistrictSet'
ALTER TABLE [dbo].[DistrictSet]
ADD CONSTRAINT [PK_DistrictSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserRoleSet'
ALTER TABLE [dbo].[UserRoleSet]
ADD CONSTRAINT [PK_UserRoleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoleSet'
ALTER TABLE [dbo].[RoleSet]
ADD CONSTRAINT [PK_RoleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PrivilegeSet'
ALTER TABLE [dbo].[PrivilegeSet]
ADD CONSTRAINT [PK_PrivilegeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MenuSet'
ALTER TABLE [dbo].[MenuSet]
ADD CONSTRAINT [PK_MenuSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ButtonSet'
ALTER TABLE [dbo].[ButtonSet]
ADD CONSTRAINT [PK_ButtonSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CommodSet'
ALTER TABLE [dbo].[CommodSet]
ADD CONSTRAINT [PK_CommodSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SalesHistorySet'
ALTER TABLE [dbo].[SalesHistorySet]
ADD CONSTRAINT [PK_SalesHistorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContainerRoadSet'
ALTER TABLE [dbo].[ContainerRoadSet]
ADD CONSTRAINT [PK_ContainerRoadSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IconSet'
ALTER TABLE [dbo].[IconSet]
ADD CONSTRAINT [PK_IconSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MachineSet'
ALTER TABLE [dbo].[MachineSet]
ADD CONSTRAINT [PK_MachineSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [PK_PersonSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PayRecordSet'
ALTER TABLE [dbo].[PayRecordSet]
ADD CONSTRAINT [PK_PayRecordSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CouponSet'
ALTER TABLE [dbo].[CouponSet]
ADD CONSTRAINT [PK_CouponSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MemberCouponSet'
ALTER TABLE [dbo].[MemberCouponSet]
ADD CONSTRAINT [PK_MemberCouponSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ShipmentStatusRecordSet'
ALTER TABLE [dbo].[ShipmentStatusRecordSet]
ADD CONSTRAINT [PK_ShipmentStatusRecordSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MachineTemSet'
ALTER TABLE [dbo].[MachineTemSet]
ADD CONSTRAINT [PK_MachineTemSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FaultSet'
ALTER TABLE [dbo].[FaultSet]
ADD CONSTRAINT [PK_FaultSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BrowserPrivilegeSet'
ALTER TABLE [dbo].[BrowserPrivilegeSet]
ADD CONSTRAINT [PK_BrowserPrivilegeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet_User'
ALTER TABLE [dbo].[PersonSet_User]
ADD CONSTRAINT [PK_PersonSet_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet_Member'
ALTER TABLE [dbo].[PersonSet_Member]
ADD CONSTRAINT [PK_PersonSet_Member]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [City_Id] in table 'DistrictSet'
ALTER TABLE [dbo].[DistrictSet]
ADD CONSTRAINT [FK_CityDistrict]
    FOREIGN KEY ([City_Id])
    REFERENCES [dbo].[CitySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityDistrict'
CREATE INDEX [IX_FK_CityDistrict]
ON [dbo].[DistrictSet]
    ([City_Id]);
GO

-- Creating foreign key on [City_Id] in table 'MachineSet'
ALTER TABLE [dbo].[MachineSet]
ADD CONSTRAINT [FK_CityMachine]
    FOREIGN KEY ([City_Id])
    REFERENCES [dbo].[CitySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityMachine'
CREATE INDEX [IX_FK_CityMachine]
ON [dbo].[MachineSet]
    ([City_Id]);
GO

-- Creating foreign key on [District_Id] in table 'MachineSet'
ALTER TABLE [dbo].[MachineSet]
ADD CONSTRAINT [FK_DistrictMachine]
    FOREIGN KEY ([District_Id])
    REFERENCES [dbo].[DistrictSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DistrictMachine'
CREATE INDEX [IX_FK_DistrictMachine]
ON [dbo].[MachineSet]
    ([District_Id]);
GO

-- Creating foreign key on [User_Id] in table 'UserRoleSet'
ALTER TABLE [dbo].[UserRoleSet]
ADD CONSTRAINT [FK_UserUserRole]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[PersonSet_User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserRole'
CREATE INDEX [IX_FK_UserUserRole]
ON [dbo].[UserRoleSet]
    ([User_Id]);
GO

-- Creating foreign key on [Role_Id] in table 'UserRoleSet'
ALTER TABLE [dbo].[UserRoleSet]
ADD CONSTRAINT [FK_RoleUserRole]
    FOREIGN KEY ([Role_Id])
    REFERENCES [dbo].[RoleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleUserRole'
CREATE INDEX [IX_FK_RoleUserRole]
ON [dbo].[UserRoleSet]
    ([Role_Id]);
GO

-- Creating foreign key on [Menu_Id] in table 'ButtonSet'
ALTER TABLE [dbo].[ButtonSet]
ADD CONSTRAINT [FK_MenuButton]
    FOREIGN KEY ([Menu_Id])
    REFERENCES [dbo].[MenuSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MenuButton'
CREATE INDEX [IX_FK_MenuButton]
ON [dbo].[ButtonSet]
    ([Menu_Id]);
GO

-- Creating foreign key on [Commod_Id] in table 'ContainerRoadSet'
ALTER TABLE [dbo].[ContainerRoadSet]
ADD CONSTRAINT [FK_CommodContainerRoad]
    FOREIGN KEY ([Commod_Id])
    REFERENCES [dbo].[CommodSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommodContainerRoad'
CREATE INDEX [IX_FK_CommodContainerRoad]
ON [dbo].[ContainerRoadSet]
    ([Commod_Id]);
GO

-- Creating foreign key on [Commod_Id] in table 'SalesHistorySet'
ALTER TABLE [dbo].[SalesHistorySet]
ADD CONSTRAINT [FK_CommodSalesHistory]
    FOREIGN KEY ([Commod_Id])
    REFERENCES [dbo].[CommodSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommodSalesHistory'
CREATE INDEX [IX_FK_CommodSalesHistory]
ON [dbo].[SalesHistorySet]
    ([Commod_Id]);
GO

-- Creating foreign key on [MachineModel_Id] in table 'MachineSet'
ALTER TABLE [dbo].[MachineSet]
ADD CONSTRAINT [FK_MachineModelMachine]
    FOREIGN KEY ([MachineModel_Id])
    REFERENCES [dbo].[MachineModelSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MachineModelMachine'
CREATE INDEX [IX_FK_MachineModelMachine]
ON [dbo].[MachineSet]
    ([MachineModel_Id]);
GO

-- Creating foreign key on [Machine_Id] in table 'ContainerRoadSet'
ALTER TABLE [dbo].[ContainerRoadSet]
ADD CONSTRAINT [FK_MachineContainerRoad]
    FOREIGN KEY ([Machine_Id])
    REFERENCES [dbo].[MachineSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MachineContainerRoad'
CREATE INDEX [IX_FK_MachineContainerRoad]
ON [dbo].[ContainerRoadSet]
    ([Machine_Id]);
GO

-- Creating foreign key on [Machine_Id] in table 'SalesHistorySet'
ALTER TABLE [dbo].[SalesHistorySet]
ADD CONSTRAINT [FK_MachineSalesHistory]
    FOREIGN KEY ([Machine_Id])
    REFERENCES [dbo].[MachineSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MachineSalesHistory'
CREATE INDEX [IX_FK_MachineSalesHistory]
ON [dbo].[SalesHistorySet]
    ([Machine_Id]);
GO

-- Creating foreign key on [User_Id] in table 'MachineSet'
ALTER TABLE [dbo].[MachineSet]
ADD CONSTRAINT [FK_UserMachine]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[PersonSet_User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMachine'
CREATE INDEX [IX_FK_UserMachine]
ON [dbo].[MachineSet]
    ([User_Id]);
GO

-- Creating foreign key on [Member_Id] in table 'PayRecordSet'
ALTER TABLE [dbo].[PayRecordSet]
ADD CONSTRAINT [FK_MemberPayRecord]
    FOREIGN KEY ([Member_Id])
    REFERENCES [dbo].[PersonSet_Member]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberPayRecord'
CREATE INDEX [IX_FK_MemberPayRecord]
ON [dbo].[PayRecordSet]
    ([Member_Id]);
GO

-- Creating foreign key on [Member_Id] in table 'MemberCouponSet'
ALTER TABLE [dbo].[MemberCouponSet]
ADD CONSTRAINT [FK_MemberMemberCoupon]
    FOREIGN KEY ([Member_Id])
    REFERENCES [dbo].[PersonSet_Member]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberMemberCoupon'
CREATE INDEX [IX_FK_MemberMemberCoupon]
ON [dbo].[MemberCouponSet]
    ([Member_Id]);
GO

-- Creating foreign key on [City_Id] in table 'PersonSet_User'
ALTER TABLE [dbo].[PersonSet_User]
ADD CONSTRAINT [FK_CityUser]
    FOREIGN KEY ([City_Id])
    REFERENCES [dbo].[CitySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityUser'
CREATE INDEX [IX_FK_CityUser]
ON [dbo].[PersonSet_User]
    ([City_Id]);
GO

-- Creating foreign key on [District_Id] in table 'PersonSet_User'
ALTER TABLE [dbo].[PersonSet_User]
ADD CONSTRAINT [FK_DistrictUser]
    FOREIGN KEY ([District_Id])
    REFERENCES [dbo].[DistrictSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DistrictUser'
CREATE INDEX [IX_FK_DistrictUser]
ON [dbo].[PersonSet_User]
    ([District_Id]);
GO

-- Creating foreign key on [Machine_Id] in table 'ShipmentStatusRecordSet'
ALTER TABLE [dbo].[ShipmentStatusRecordSet]
ADD CONSTRAINT [FK_MachineShipmentStatusRecord]
    FOREIGN KEY ([Machine_Id])
    REFERENCES [dbo].[MachineSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MachineShipmentStatusRecord'
CREATE INDEX [IX_FK_MachineShipmentStatusRecord]
ON [dbo].[ShipmentStatusRecordSet]
    ([Machine_Id]);
GO

-- Creating foreign key on [Coupon_Id] in table 'MemberCouponSet'
ALTER TABLE [dbo].[MemberCouponSet]
ADD CONSTRAINT [FK_CouponMemberCoupon]
    FOREIGN KEY ([Coupon_Id])
    REFERENCES [dbo].[CouponSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CouponMemberCoupon'
CREATE INDEX [IX_FK_CouponMemberCoupon]
ON [dbo].[MemberCouponSet]
    ([Coupon_Id]);
GO

-- Creating foreign key on [Commod_Id] in table 'ShipmentStatusRecordSet'
ALTER TABLE [dbo].[ShipmentStatusRecordSet]
ADD CONSTRAINT [FK_CommodShipmentStatusRecord]
    FOREIGN KEY ([Commod_Id])
    REFERENCES [dbo].[CommodSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommodShipmentStatusRecord'
CREATE INDEX [IX_FK_CommodShipmentStatusRecord]
ON [dbo].[ShipmentStatusRecordSet]
    ([Commod_Id]);
GO

-- Creating foreign key on [Machine_Id] in table 'MachineTemSet'
ALTER TABLE [dbo].[MachineTemSet]
ADD CONSTRAINT [FK_MachineTemMachine]
    FOREIGN KEY ([Machine_Id])
    REFERENCES [dbo].[MachineSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MachineTemMachine'
CREATE INDEX [IX_FK_MachineTemMachine]
ON [dbo].[MachineTemSet]
    ([Machine_Id]);
GO

-- Creating foreign key on [Machine_Id] in table 'FaultSet'
ALTER TABLE [dbo].[FaultSet]
ADD CONSTRAINT [FK_MachineError]
    FOREIGN KEY ([Machine_Id])
    REFERENCES [dbo].[MachineSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MachineError'
CREATE INDEX [IX_FK_MachineError]
ON [dbo].[FaultSet]
    ([Machine_Id]);
GO

-- Creating foreign key on [Id] in table 'PersonSet_User'
ALTER TABLE [dbo].[PersonSet_User]
ADD CONSTRAINT [FK_User_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'PersonSet_Member'
ALTER TABLE [dbo].[PersonSet_Member]
ADD CONSTRAINT [FK_Member_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------