IF OBJECT_ID(N'[dbo].[FK_OBK_AssessmentDeclaration_Procunts_Series]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OBK_Procunts_Series] DROP CONSTRAINT [FK_OBK_AssessmentDeclaration_Procunts_Series];
GO
IF OBJECT_ID(N'[dbo].[FK_OBK_AssessmentDeclaration_RS_Products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OBK_RS_Products] DROP CONSTRAINT [FK_OBK_AssessmentDeclaration_RS_Products];
GO
IF OBJECT_ID(N'[dbo].[FK_OBK_RS_Products_Procunts_Series]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OBK_Procunts_Series] DROP CONSTRAINT [FK_OBK_RS_Products_Procunts_Series];
GO
IF OBJECT_ID(N'[dbo].[OBK_AssessmentDeclaration]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OBK_AssessmentDeclaration];
GO
IF OBJECT_ID(N'[dbo].[OBK_AssessmentDeclarationFieldHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OBK_AssessmentDeclarationFieldHistory];
GO
IF OBJECT_ID(N'[dbo].[OBK_Contract]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OBK_Contract];
GO
IF OBJECT_ID(N'[dbo].[OBK_Organization]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OBK_Organization];
GO
IF OBJECT_ID(N'[dbo].[OBK_Procunts_Series]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OBK_Procunts_Series];
GO
IF OBJECT_ID(N'[dbo].[OBK_Ref_Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OBK_Ref_Status];
GO
IF OBJECT_ID(N'[dbo].[OBK_Ref_Type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OBK_Ref_Type];
GO
IF OBJECT_ID(N'[dbo].[OBK_RS_Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OBK_RS_Products];
GO
-- Creating table 'OBK_AssessmentDeclaration'
CREATE TABLE [dbo].[OBK_AssessmentDeclaration] (
    [Id] uniqueidentifier  NOT NULL,
    [Declarant] nvarchar(max)  NULL,
    [DeclarantAddress] nvarchar(max)  NULL,
    [CertificateGMP] nvarchar(max)  NULL,
    [CertificateNumber] nvarchar(max)  NULL,
    [AssuranceCheck] bit  NOT NULL,
    [OrderCheck] bit  NOT NULL,
    [StabilityCheck] bit  NOT NULL,
    [PaymentCheck] bit  NOT NULL,
    [OwnerId] uniqueidentifier  NOT NULL,
    [Type_Id] int  NOT NULL,
    [Contract_Id] uniqueidentifier  NOT NULL,
    [CertificateDate] datetime  NULL,
    [CreatedDate] datetime  NOT NULL,
    [CertificateGMPCheck] bit  NULL,
    [StatusId] int  NOT NULL,
    [InvoiceRu] nvarchar(max)  NULL,
    [InvoiceKz] nvarchar(max)  NULL,
    [InvoiceDate] datetime  NULL,
    [InvoiceContractRu] nvarchar(max)  NULL,
    [InvoiceContractKz] nvarchar(max)  NULL,
    [InvoiceAgentLastName] nvarchar(max)  NULL,
    [InvoiceAgentFirstName] nvarchar(max)  NULL,
    [InvoiceAgentMiddelName] nvarchar(max)  NULL,
    [InvoiceAgentPositionName] nvarchar(max)  NULL,
    [Number] nvarchar(500)  NULL,
    [SendDate] datetime  NULL,
    [ExecuterId] uniqueidentifier  NULL,
    [IsDeleted] bit  NULL,
    [DesignDate] datetime  NULL
);
GO

-- Creating table 'OBK_Ref_Type'
CREATE TABLE [dbo].[OBK_Ref_Type] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [NameRu] nvarchar(max)  NOT NULL,
    [NameKz] nvarchar(max)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'OBK_Contract'
CREATE TABLE [dbo].[OBK_Contract] (
    [Id] uniqueidentifier  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [Type] int  NOT NULL,
    [Status] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [OwnerId] uniqueidentifier  NOT NULL,
    [OrganizationId] int  NOT NULL
);
GO

-- Creating table 'OBK_RS_Products'
CREATE TABLE [dbo].[OBK_RS_Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameRu] nvarchar(max)  NULL,
    [NameKz] nvarchar(max)  NULL,
    [ProducerNameRu] nvarchar(max)  NULL,
    [ProducerNameKz] nvarchar(max)  NULL,
    [CountryNameRu] nvarchar(max)  NULL,
    [CountryNameKZ] nvarchar(max)  NULL,
    [TnvedCode] nvarchar(20)  NULL,
    [KpvedCode] nvarchar(20)  NULL,
    [Price] nvarchar(20)  NULL,
    [OBK_AssessmentDeclarationId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'OBK_Procunts_Series'
CREATE TABLE [dbo].[OBK_Procunts_Series] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Series] nvarchar(max)  NULL,
    [SeriesStartdate] nvarchar(max)  NULL,
    [SeriesEndDate] nvarchar(max)  NULL,
    [SeriesParty] nvarchar(max)  NULL,
    [OBK_RS_ProductsId] int  NOT NULL,
    [OBK_AssessmentDeclarationId] uniqueidentifier  NULL
);
GO

-- Creating table 'OBK_AssessmentDeclarationFieldHistory'
CREATE TABLE [dbo].[OBK_AssessmentDeclarationFieldHistory] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ControlId] nvarchar(max)  NULL,
    [UserId] uniqueidentifier  NULL,
    [ValueField] nvarchar(max)  NULL,
    [DisplayField] nvarchar(max)  NULL,
    [CreateDate] datetime  NOT NULL,
    [AssessmentDeclarationId] uniqueidentifier  NULL
);
GO

-- Creating table 'OBK_Organization'
CREATE TABLE [dbo].[OBK_Organization] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] int  NULL,
    [NameKz] nvarchar(max)  NULL,
    [NameRu] nvarchar(max)  NULL,
    [NameEn] nvarchar(max)  NULL,
    [AddressLegal] nvarchar(max)  NULL,
    [AddressFact] nvarchar(max)  NULL,
    [Phone] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL,
    [BankSwift] nvarchar(max)  NULL,
    [BankBik] nvarchar(max)  NULL,
    [СhiefLastName] nvarchar(max)  NULL,
    [СhiefFirstName] nvarchar(max)  NULL,
    [СhiefMiddleName] nvarchar(max)  NULL,
    [СhiefPosition] nvarchar(max)  NULL,
    [RegCertificateNumber] nvarchar(max)  NULL,
    [OrgForm] nvarchar(max)  NULL,
    [CountryId] uniqueidentifier  NULL,
    [BankName] nvarchar(max)  NULL,
    [BankAccount] nvarchar(max)  NULL,
    [Declarant] nvarchar(max)  NULL,
    [CurrencyId] uniqueidentifier  NULL
);
GO

-- Creating table 'OBK_Ref_Status'
CREATE TABLE [dbo].[OBK_Ref_Status] (
    [Id] int  NOT NULL,
    [Code] nvarchar(50)  NULL,
    [NameRu] nvarchar(2000)  NULL,
    [NameKz] nvarchar(2000)  NULL,
    [DateCreate] datetime  NULL,
    [IsDeleted] bit  NULL,
    [DateEdit] datetime  NULL
);
GO
-- Creating primary key on [Id] in table 'OBK_AssessmentDeclaration'
ALTER TABLE [dbo].[OBK_AssessmentDeclaration]
ADD CONSTRAINT [PK_OBK_AssessmentDeclaration]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OBK_Ref_Type'
ALTER TABLE [dbo].[OBK_Ref_Type]
ADD CONSTRAINT [PK_OBK_Ref_Type]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OBK_Contract'
ALTER TABLE [dbo].[OBK_Contract]
ADD CONSTRAINT [PK_OBK_Contract]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OBK_RS_Products'
ALTER TABLE [dbo].[OBK_RS_Products]
ADD CONSTRAINT [PK_OBK_RS_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OBK_Procunts_Series'
ALTER TABLE [dbo].[OBK_Procunts_Series]
ADD CONSTRAINT [PK_OBK_Procunts_Series]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OBK_AssessmentDeclarationFieldHistory'
ALTER TABLE [dbo].[OBK_AssessmentDeclarationFieldHistory]
ADD CONSTRAINT [PK_OBK_AssessmentDeclarationFieldHistory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OBK_Organization'
ALTER TABLE [dbo].[OBK_Organization]
ADD CONSTRAINT [PK_OBK_Organization]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OBK_Ref_Status'
ALTER TABLE [dbo].[OBK_Ref_Status]
ADD CONSTRAINT [PK_OBK_Ref_Status]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO
-- Creating foreign key on [Type_Id] in table 'OBK_AssessmentDeclaration'
ALTER TABLE [dbo].[OBK_AssessmentDeclaration]
ADD CONSTRAINT [FK_OBK_Ref_Type_AssessmentDeclaration]
    FOREIGN KEY ([Type_Id])
    REFERENCES [dbo].[OBK_Ref_Type]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OBK_Ref_Type_AssessmentDeclaration'
CREATE INDEX [IX_FK_OBK_Ref_Type_AssessmentDeclaration]
ON [dbo].[OBK_AssessmentDeclaration]
    ([Type_Id]);
GO

-- Creating foreign key on [OBK_RS_ProductsId] in table 'OBK_Procunts_Series'
ALTER TABLE [dbo].[OBK_Procunts_Series]
ADD CONSTRAINT [FK_OBK_RS_Products_Procunts_Series]
    FOREIGN KEY ([OBK_RS_ProductsId])
    REFERENCES [dbo].[OBK_RS_Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OBK_RS_Products_Procunts_Series'
CREATE INDEX [IX_FK_OBK_RS_Products_Procunts_Series]
ON [dbo].[OBK_Procunts_Series]
    ([OBK_RS_ProductsId]);
GO

-- Creating foreign key on [OBK_AssessmentDeclarationId] in table 'OBK_RS_Products'
ALTER TABLE [dbo].[OBK_RS_Products]
ADD CONSTRAINT [FK_OBK_AssessmentDeclaration_RS_Products]
    FOREIGN KEY ([OBK_AssessmentDeclarationId])
    REFERENCES [dbo].[OBK_AssessmentDeclaration]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OBK_AssessmentDeclaration_RS_Products'
CREATE INDEX [IX_FK_OBK_AssessmentDeclaration_RS_Products]
ON [dbo].[OBK_RS_Products]
    ([OBK_AssessmentDeclarationId]);
GO

-- Creating foreign key on [StatusId] in table 'OBK_AssessmentDeclaration'
ALTER TABLE [dbo].[OBK_AssessmentDeclaration]
ADD CONSTRAINT [FK_OBK_AssessmentDeclaration_Ref_Status]
    FOREIGN KEY ([StatusId])
    REFERENCES [dbo].[OBK_Ref_Status]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OBK_AssessmentDeclaration_Ref_Status'
CREATE INDEX [IX_FK_OBK_AssessmentDeclaration_Ref_Status]
ON [dbo].[OBK_AssessmentDeclaration]
    ([StatusId]);
GO

-- Creating foreign key on [OBK_AssessmentDeclarationId] in table 'OBK_Procunts_Series'
ALTER TABLE [dbo].[OBK_Procunts_Series]
ADD CONSTRAINT [FK_OBK_AssessmentDeclaration_Procunts_Series]
    FOREIGN KEY ([OBK_AssessmentDeclarationId])
    REFERENCES [dbo].[OBK_AssessmentDeclaration]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OBK_AssessmentDeclaration_Procunts_Series'
CREATE INDEX [IX_FK_OBK_AssessmentDeclaration_Procunts_Series]
ON [dbo].[OBK_Procunts_Series]
    ([OBK_AssessmentDeclarationId]);
GO