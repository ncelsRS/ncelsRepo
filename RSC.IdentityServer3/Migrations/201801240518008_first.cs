namespace RSC.IdentityServer3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessDocuments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ObjectId = c.Guid(nullable: false),
                        PropertyName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.ObjectId, cascadeDelete: true)
                .Index(t => t.ObjectId);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsAdministrativeUse = c.Boolean(nullable: false),
                        IsAwaitingResponse = c.Boolean(nullable: false),
                        IsTradeSecret = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        AwaitingResponseDate = c.DateTime(),
                        DocumentDate = c.DateTime(),
                        OutgoingDate = c.DateTime(),
                        ProtocolDate = c.DateTime(),
                        MonitoringDate = c.DateTime(),
                        ApplicantType = c.Int(nullable: false),
                        DocumentType = c.Int(nullable: false),
                        MonitoringType = c.Int(nullable: false),
                        PriorityType = c.Int(nullable: false),
                        StateType = c.Int(nullable: false),
                        AppendixCount = c.Int(nullable: false),
                        CopiesCount = c.Int(nullable: false),
                        PageCount = c.Int(nullable: false),
                        RepeatCount = c.Int(nullable: false),
                        ApplicantAddress = c.String(maxLength: 512),
                        ApplicantEmail = c.String(maxLength: 4000),
                        ApplicantName = c.String(maxLength: 512),
                        ApplicantPhone = c.String(maxLength: 512),
                        BlankNumber = c.String(maxLength: 512),
                        CorrespondentsInfo = c.String(maxLength: 512),
                        Number = c.String(maxLength: 512),
                        OutgoingNumber = c.String(maxLength: 512),
                        SortingNumber = c.String(maxLength: 512),
                        SortingOutgoingNumber = c.String(maxLength: 512),
                        Note = c.String(maxLength: 4000),
                        Summary = c.String(maxLength: 4000),
                        AdministrativeTypeDictionaryId = c.String(maxLength: 4000),
                        AdministrativeTypeDictionaryValue = c.String(maxLength: 4000),
                        ApplicantCategoryDictionaryId = c.String(maxLength: 4000),
                        ApplicantCategoryDictionaryValue = c.String(maxLength: 4000),
                        CauseCitizenDictionaryId = c.String(maxLength: 4000),
                        CauseCitizenDictionaryValue = c.String(maxLength: 4000),
                        CitizenCategoryDictionaryId = c.String(maxLength: 4000),
                        CitizenCategoryDictionaryValue = c.String(maxLength: 4000),
                        CitizenResultDictionaryId = c.String(maxLength: 4000),
                        CitizenResultDictionaryValue = c.String(maxLength: 4000),
                        CitizenTypeDictionaryId = c.String(maxLength: 4000),
                        CitizenTypeDictionaryValue = c.String(maxLength: 4000),
                        DocumentKindDictionaryId = c.String(maxLength: 4000),
                        DocumentKindDictionaryValue = c.String(maxLength: 4000),
                        FormDeliveryDictionaryId = c.String(maxLength: 4000),
                        FormDeliveryDictionaryValue = c.String(maxLength: 4000),
                        FormSendingDictionaryId = c.String(maxLength: 4000),
                        FormSendingDictionaryValue = c.String(maxLength: 4000),
                        KatoDictionaryId = c.String(maxLength: 4000),
                        KatoDictionaryValue = c.String(maxLength: 4000),
                        LanguageDictionaryId = c.String(maxLength: 4000),
                        LanguageDictionaryValue = c.String(maxLength: 4000),
                        NomenclatureDictionaryId = c.String(maxLength: 4000),
                        NomenclatureDictionaryValue = c.String(maxLength: 4000),
                        QuestionDesignDictionaryId = c.String(maxLength: 4000),
                        QuestionDesignDictionaryValue = c.String(maxLength: 4000),
                        SigningFormDictionaryId = c.String(maxLength: 4000),
                        SigningFormDictionaryValue = c.String(maxLength: 4000),
                        CompleteDocumentsId = c.String(maxLength: 4000),
                        EditDocumentsId = c.String(maxLength: 4000),
                        RepealDocumentsId = c.String(maxLength: 4000),
                        RepeaterId = c.Guid(),
                        AttachmentId = c.Guid(),
                        ResolutionId = c.Guid(),
                        AgreementsId = c.String(maxLength: 4000),
                        AgreementsValue = c.String(maxLength: 4000),
                        ExecutorsId = c.String(maxLength: 4000),
                        ExecutorsValue = c.String(maxLength: 4000),
                        ReadersId = c.String(maxLength: 4000),
                        ReadersValue = c.String(maxLength: 4000),
                        RecipientsId = c.String(maxLength: 4000),
                        RecipientsValue = c.String(maxLength: 4000),
                        RegistratorId = c.String(maxLength: 4000),
                        RegistratorValue = c.String(maxLength: 4000),
                        ResponsibleId = c.String(maxLength: 510),
                        ResponsibleValue = c.String(maxLength: 4000),
                        SignerId = c.String(maxLength: 4000),
                        SignerValue = c.String(maxLength: 4000),
                        CorrespondentsId = c.String(maxLength: 4000),
                        CorrespondentsValue = c.String(maxLength: 4000),
                        MonitoringAuthorId = c.String(maxLength: 4000),
                        MonitoringAuthorValue = c.String(maxLength: 4000),
                        MonitoringNote = c.String(maxLength: 4000),
                        AnswersId = c.String(maxLength: 4000),
                        AnswersValue = c.String(maxLength: 4000),
                        CompleteDocumentsValue = c.String(maxLength: 4000),
                        EditDocumentsValue = c.String(maxLength: 4000),
                        RepealDocumentsValue = c.String(maxLength: 4000),
                        DisplayName = c.String(maxLength: 4000),
                        AutoAnswersId = c.String(maxLength: 4000),
                        AutoAnswersValue = c.String(maxLength: 4000),
                        AutoAnswersTempId = c.String(maxLength: 4000),
                        AutoAnswersTempValue = c.String(maxLength: 4000),
                        AutoCompleteDocumentsValue = c.String(maxLength: 4000),
                        AutoEditDocumentsValue = c.String(maxLength: 4000),
                        AutoRepealDocumentsValue = c.String(maxLength: 4000),
                        SortNumber = c.Int(nullable: false),
                        AutoCompleteDocumentsId = c.String(maxLength: 4000),
                        AutoEditDocumentsId = c.String(maxLength: 4000),
                        AutoRepealDocumentsId = c.String(maxLength: 4000),
                        FactExecutionDate = c.DateTime(),
                        FirstExecutionDate = c.DateTime(),
                        ExecutionDate = c.DateTime(),
                        TemplateId = c.Guid(),
                        Counters = c.String(maxLength: 4000),
                        DocumentDictionaryTypeId = c.String(maxLength: 4000),
                        DocumentDictionaryTypeValue = c.String(maxLength: 4000),
                        ResolutionValue = c.String(maxLength: 4000),
                        OutgoingType = c.Int(nullable: false),
                        SourceId = c.String(maxLength: 4000),
                        SourceValue = c.String(maxLength: 4000),
                        DestinationId = c.String(maxLength: 4000),
                        DestinationValue = c.String(maxLength: 4000),
                        MainTaskId = c.Guid(),
                        MainDocumentId = c.Guid(),
                        OwnerId = c.String(maxLength: 4000),
                        OwnerValue = c.String(maxLength: 4000),
                        Country = c.String(maxLength: 4000),
                        Area = c.String(maxLength: 4000),
                        Postcode = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 4000),
                        Department = c.String(maxLength: 4000),
                        City = c.String(maxLength: 4000),
                        Address = c.String(maxLength: 4000),
                        NumberBill = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        SuperMainDocumentId = c.Guid(),
                        ModifiedUser = c.String(maxLength: 4000),
                        IsNotification = c.Boolean(nullable: false),
                        NotificationCount = c.Int(nullable: false),
                        DateDispatch = c.DateTime(),
                        DispatchNote = c.String(maxLength: 4000),
                        Digest = c.String(maxLength: 4000),
                        IsAttachments = c.Boolean(nullable: false),
                        Text = c.String(maxLength: 4000),
                        Recipient = c.String(maxLength: 4000),
                        QrCode = c.Binary(storeType: "image"),
                        IsArchive = c.Boolean(nullable: false),
                        InventoryId = c.Int(),
                        FulfilledDate = c.DateTime(),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        AutoAwaitingResponseDate = c.DateTime(storeType: "date"),
                        AutoDocumentDate = c.DateTime(storeType: "date"),
                        AutoOutgoingDate = c.DateTime(storeType: "date"),
                        AutoProtocolDate = c.DateTime(storeType: "date"),
                        AutoMonitoringDate = c.DateTime(storeType: "date"),
                        AutoFactExecutionDate = c.DateTime(storeType: "date"),
                        AutoFirstExecutionDate = c.DateTime(storeType: "date"),
                        AutoExecutionDate = c.DateTime(storeType: "date"),
                        CreatedUserId = c.String(maxLength: 900),
                        CreatedUserValue = c.String(maxLength: 900),
                        Book = c.String(maxLength: 900),
                        Deed = c.String(maxLength: 900),
                        Ip = c.String(maxLength: 100),
                        Akt = c.String(maxLength: 900),
                        ProjectType = c.Int(nullable: false),
                        OrganizationId = c.Guid(nullable: false),
                        AttachPath = c.String(maxLength: 300),
                        RegionValue = c.String(maxLength: 4000),
                        RegionId = c.String(maxLength: 4000),
                        PriceSum = c.Decimal(precision: 18, scale: 2),
                        RemarkId = c.Int(),
                        ParleyStartDate = c.DateTime(),
                        ParleyEndDate = c.DateTime(),
                        RemarkText1 = c.String(maxLength: 4000),
                        RemarkText2 = c.String(maxLength: 4000),
                        RemarkText3 = c.String(maxLength: 4000),
                        CountDay = c.Int(),
                        ReturnDate = c.DateTime(),
                        CompareConterDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RemarkTypes", t => t.RemarkId)
                .ForeignKey("dbo.Templates", t => t.TemplateId)
                .Index(t => t.TemplateId)
                .Index(t => t.RemarkId);
            
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(maxLength: 4000),
                        Type = c.Int(),
                        AuthorId = c.String(maxLength: 4000),
                        AuthorValue = c.String(maxLength: 4000),
                        CreatedDate = c.DateTime(),
                        ExecutionDate = c.DateTime(),
                        ExecutorsId = c.String(maxLength: 4000),
                        ExecutorsValue = c.String(maxLength: 4000),
                        DocumentId = c.Guid(),
                        DocumentValue = c.String(maxLength: 4000),
                        IsParrent = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                        ParentTask = c.Guid(),
                        ResponsibleId = c.String(maxLength: 4000),
                        ResponsibleValue = c.String(maxLength: 4000),
                        IsNotActive = c.Boolean(nullable: false),
                        IsCurrent = c.Boolean(nullable: false),
                        ModifiedUser = c.String(maxLength: 4000),
                        IsMainLine = c.Boolean(nullable: false),
                        Ip = c.String(maxLength: 100),
                        ModifiedDate = c.DateTime(),
                        TypeEx = c.Int(nullable: false),
                        Branch = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.DocumentId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(maxLength: 4000),
                        ExecutionDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        AuthorId = c.String(maxLength: 4000),
                        AuthorValue = c.String(maxLength: 4000),
                        ExecutorId = c.String(maxLength: 4000),
                        ExecutorValue = c.String(maxLength: 4000),
                        State = c.Int(nullable: false),
                        DocumentValue = c.String(maxLength: 4000),
                        Type = c.Int(),
                        DocumentId = c.Guid(),
                        ActivityId = c.Guid(),
                        AutoExecutionDate = c.DateTime(storeType: "date"),
                        Number = c.String(maxLength: 4000),
                        FunctionType = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        NotificationCount = c.Int(nullable: false),
                        IsNotification = c.Boolean(nullable: false),
                        SortingNumber = c.Int(nullable: false),
                        DateOfOperation = c.DateTime(),
                        ModifiedUser = c.String(maxLength: 4000),
                        IsMainLine = c.Boolean(nullable: false),
                        Ip = c.String(maxLength: 100),
                        ModifiedDate = c.DateTime(),
                        TypeEx = c.Int(nullable: false),
                        Stage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.DocumentId)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .Index(t => t.DocumentId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.AccessTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ObjectId = c.Guid(nullable: false),
                        PropertyName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tasks", t => t.ObjectId, cascadeDelete: true)
                .Index(t => t.ObjectId);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(),
                        Text = c.String(maxLength: 4000),
                        AnswersId = c.String(maxLength: 4000),
                        AnswersValue = c.String(maxLength: 4000),
                        ExecutionDate = c.DateTime(),
                        DocumentId = c.Guid(),
                        TaskId = c.Guid(),
                        ModifiedUser = c.String(maxLength: 4000),
                        Ip = c.String(maxLength: 100),
                        ModifiedDate = c.DateTime(),
                        PageCount = c.Int(),
                        SymbolCount = c.Int(),
                        TitleDicId = c.String(maxLength: 4000),
                        TitleDicValue = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.DocumentId)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.DocumentId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.RemarkTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        Code = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                        Type = c.Int(),
                        Data = c.Binary(),
                        TemplateType = c.Int(),
                        Report = c.Binary(),
                        ConvertDictionaryTypeId = c.Guid(),
                        ConvertType = c.Int(),
                        DictionaryTypeValue = c.String(maxLength: 4000),
                        DictionaryTypeId = c.String(maxLength: 4000),
                        Value = c.String(maxLength: 4000),
                        Validation = c.String(maxLength: 4000),
                        PrintForm = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActionLogPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        PlaceId = c.Int(),
                        Text = c.String(maxLength: 1000),
                        AdditionalText = c.String(),
                        EmployeeId = c.Guid(),
                        Type = c.Int(nullable: false),
                        IpAddress = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActionLogsView",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        PlaceId = c.Int(),
                        PlaceName = c.String(maxLength: 200),
                        Text = c.String(maxLength: 1000),
                        AdditionalText = c.String(),
                        IpAddress = c.String(maxLength: 1000),
                        EmployeeId = c.Guid(),
                        EmployeeLastName = c.String(maxLength: 4000),
                        EmployeeFirstName = c.String(maxLength: 4000),
                        EmployeeMiddleName = c.String(maxLength: 4000),
                        EmployeeLogin = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => new { t.Id, t.Date, t.Type });
            
            CreateTable(
                "dbo.AdministrativeDocument",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsAdministrativeUse = c.Boolean(nullable: false),
                        IsAwaitingResponse = c.Boolean(nullable: false),
                        IsTradeSecret = c.Boolean(nullable: false),
                        ApplicantType = c.Int(nullable: false),
                        DocumentType = c.Int(nullable: false),
                        MonitoringType = c.Int(nullable: false),
                        PriorityType = c.Int(nullable: false),
                        StateType = c.Int(nullable: false),
                        AppendixCount = c.Int(nullable: false),
                        CopiesCount = c.Int(nullable: false),
                        PageCount = c.Int(nullable: false),
                        RepeatCount = c.Int(nullable: false),
                        SortNumber = c.Int(nullable: false),
                        OutgoingType = c.Int(nullable: false),
                        IsNotification = c.Boolean(nullable: false),
                        NotificationCount = c.Int(nullable: false),
                        IsAttachments = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        AwaitingResponseDate = c.DateTime(),
                        DocumentDate = c.DateTime(),
                        OutgoingDate = c.DateTime(),
                        ProtocolDate = c.DateTime(),
                        MonitoringDate = c.DateTime(),
                        ApplicantAddress = c.String(maxLength: 512),
                        ApplicantEmail = c.String(maxLength: 4000),
                        ApplicantName = c.String(maxLength: 512),
                        ApplicantPhone = c.String(maxLength: 512),
                        BlankNumber = c.String(maxLength: 512),
                        CorrespondentsInfo = c.String(maxLength: 512),
                        Number = c.String(maxLength: 512),
                        OutgoingNumber = c.String(maxLength: 512),
                        SortingNumber = c.String(maxLength: 512),
                        SortingOutgoingNumber = c.String(maxLength: 512),
                        Note = c.String(maxLength: 4000),
                        Summary = c.String(maxLength: 4000),
                        AdministrativeTypeDictionaryValue = c.String(maxLength: 4000),
                        ApplicantCategoryDictionaryValue = c.String(maxLength: 4000),
                        CauseCitizenDictionaryValue = c.String(maxLength: 4000),
                        CitizenCategoryDictionaryValue = c.String(maxLength: 4000),
                        CitizenResultDictionaryValue = c.String(maxLength: 4000),
                        CitizenTypeDictionaryValue = c.String(maxLength: 4000),
                        DocumentKindDictionaryValue = c.String(maxLength: 4000),
                        FormDeliveryDictionaryValue = c.String(maxLength: 4000),
                        FormSendingDictionaryValue = c.String(maxLength: 4000),
                        KatoDictionaryValue = c.String(maxLength: 4000),
                        LanguageDictionaryValue = c.String(maxLength: 4000),
                        NomenclatureDictionaryValue = c.String(maxLength: 4000),
                        QuestionDesignDictionaryValue = c.String(maxLength: 4000),
                        SigningFormDictionaryValue = c.String(maxLength: 4000),
                        AgreementsValue = c.String(maxLength: 4000),
                        ExecutorsValue = c.String(maxLength: 4000),
                        ReadersValue = c.String(maxLength: 4000),
                        RecipientsValue = c.String(maxLength: 4000),
                        RegistratorValue = c.String(maxLength: 4000),
                        ResponsibleValue = c.String(maxLength: 4000),
                        SignerValue = c.String(maxLength: 4000),
                        CorrespondentsValue = c.String(maxLength: 4000),
                        MonitoringAuthorValue = c.String(maxLength: 4000),
                        MonitoringNote = c.String(maxLength: 4000),
                        AnswersValue = c.String(maxLength: 4000),
                        CompleteDocumentsValue = c.String(maxLength: 4000),
                        EditDocumentsValue = c.String(maxLength: 4000),
                        RepealDocumentsValue = c.String(maxLength: 4000),
                        DisplayName = c.String(maxLength: 4000),
                        AutoAnswersValue = c.String(maxLength: 4000),
                        AutoAnswersTempValue = c.String(maxLength: 4000),
                        AutoCompleteDocumentsValue = c.String(maxLength: 4000),
                        AutoEditDocumentsValue = c.String(maxLength: 4000),
                        AutoRepealDocumentsValue = c.String(maxLength: 4000),
                        AutoAwaitingResponseDate = c.DateTime(storeType: "date"),
                        AutoDocumentDate = c.DateTime(storeType: "date"),
                        AutoOutgoingDate = c.DateTime(storeType: "date"),
                        AutoProtocolDate = c.DateTime(storeType: "date"),
                        AutoMonitoringDate = c.DateTime(storeType: "date"),
                        FactExecutionDate = c.DateTime(),
                        FirstExecutionDate = c.DateTime(),
                        ExecutionDate = c.DateTime(),
                        AutoFactExecutionDate = c.DateTime(storeType: "date"),
                        AutoFirstExecutionDate = c.DateTime(storeType: "date"),
                        AutoExecutionDate = c.DateTime(storeType: "date"),
                        Counters = c.String(maxLength: 4000),
                        DocumentDictionaryTypeValue = c.String(maxLength: 4000),
                        ResolutionValue = c.String(maxLength: 4000),
                        SourceValue = c.String(maxLength: 4000),
                        DestinationValue = c.String(maxLength: 4000),
                        OwnerValue = c.String(maxLength: 4000),
                        Country = c.String(maxLength: 4000),
                        Area = c.String(maxLength: 4000),
                        Postcode = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 4000),
                        Department = c.String(maxLength: 4000),
                        City = c.String(maxLength: 4000),
                        Address = c.String(maxLength: 4000),
                        NumberBill = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        SuperMainDocumentId = c.Guid(),
                        ModifiedUser = c.String(maxLength: 4000),
                        DateDispatch = c.DateTime(),
                        DispatchNote = c.String(maxLength: 4000),
                        Digest = c.String(maxLength: 4000),
                        Text = c.String(maxLength: 4000),
                        Recipient = c.String(maxLength: 4000),
                        QrCode = c.Binary(storeType: "image"),
                        FulfilledDate = c.DateTime(),
                        State = c.String(maxLength: 510),
                        Monitoring = c.String(maxLength: 510),
                        Priority = c.String(maxLength: 510),
                    })
                .PrimaryKey(t => new { t.Id, t.IsDeleted, t.IsAdministrativeUse, t.IsAwaitingResponse, t.IsTradeSecret, t.ApplicantType, t.DocumentType, t.MonitoringType, t.PriorityType, t.StateType, t.AppendixCount, t.CopiesCount, t.PageCount, t.RepeatCount, t.SortNumber, t.OutgoingType, t.IsNotification, t.NotificationCount, t.IsAttachments, t.IsArchive });
            
            CreateTable(
                "dbo.Archives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                        ConnectionString = c.String(maxLength: 4000),
                        DbName = c.String(maxLength: 4000),
                        CreatedDate = c.DateTime(nullable: false),
                        IsCurrent = c.Boolean(nullable: false),
                        Owner = c.String(maxLength: 4000),
                        Path = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArchivView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DocumentType = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 1, unicode: false),
                        DepartmentsValue = c.String(nullable: false, maxLength: 1, unicode: false),
                        OrganizationId = c.Guid(nullable: false),
                        DocumentDate = c.DateTime(),
                        DisplayName = c.String(maxLength: 4000),
                        Number = c.String(maxLength: 512),
                        NomenclatureDictionaryValue = c.String(maxLength: 4000),
                        Summary = c.String(maxLength: 4000),
                        Deed = c.String(maxLength: 900),
                        Book = c.String(maxLength: 900),
                        Akt = c.String(maxLength: 900),
                        Note = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.DocumentType, t.Code, t.DepartmentsValue, t.OrganizationId });
            
            CreateTable(
                "dbo.aspnet_Applications",
                c => new
                    {
                        ApplicationId = c.Guid(nullable: false),
                        ApplicationName = c.String(nullable: false, maxLength: 256),
                        LoweredApplicationName = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.ApplicationId);
            
            CreateTable(
                "dbo.aspnet_Membership",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                        Password = c.String(maxLength: 256),
                        PasswordFormat = c.Int(nullable: false),
                        PasswordSalt = c.String(maxLength: 256),
                        MobilePIN = c.String(maxLength: 32),
                        Email = c.String(maxLength: 512),
                        LoweredEmail = c.String(maxLength: 512),
                        PasswordQuestion = c.String(maxLength: 512),
                        PasswordAnswer = c.String(maxLength: 256),
                        IsApproved = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastLoginDate = c.DateTime(nullable: false),
                        LastPasswordChangedDate = c.DateTime(nullable: false),
                        LastLockoutDate = c.DateTime(nullable: false),
                        FailedPasswordAttemptCount = c.Int(nullable: false),
                        FailedPasswordAttemptWindowStart = c.DateTime(nullable: false),
                        FailedPasswordAnswerAttemptCount = c.Int(nullable: false),
                        FailedPasswordAnswerAttemptWindowStart = c.DateTime(nullable: false),
                        Comment = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.aspnet_Users", t => t.UserId)
                .ForeignKey("dbo.aspnet_Applications", t => t.ApplicationId)
                .Index(t => t.UserId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.aspnet_Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                        UserName = c.String(maxLength: 512),
                        LoweredUserName = c.String(maxLength: 512),
                        MobileAlias = c.String(maxLength: 32),
                        IsAnonymous = c.Boolean(nullable: false),
                        LastActivityDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.aspnet_Applications", t => t.ApplicationId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.aspnet_PersonalizationPerUser",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PathId = c.Guid(),
                        UserId = c.Guid(),
                        PageSettings = c.Binary(nullable: false, storeType: "image"),
                        LastUpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.aspnet_Paths", t => t.PathId)
                .ForeignKey("dbo.aspnet_Users", t => t.UserId)
                .Index(t => t.PathId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.aspnet_Paths",
                c => new
                    {
                        PathId = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                        Path = c.String(maxLength: 512),
                        LoweredPath = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.PathId)
                .ForeignKey("dbo.aspnet_Applications", t => t.ApplicationId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.aspnet_PersonalizationAllUsers",
                c => new
                    {
                        PathId = c.Guid(nullable: false),
                        PageSettings = c.Binary(nullable: false, storeType: "image"),
                        LastUpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PathId)
                .ForeignKey("dbo.aspnet_Paths", t => t.PathId)
                .Index(t => t.PathId);
            
            CreateTable(
                "dbo.aspnet_Profile",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        PropertyNames = c.String(nullable: false, storeType: "ntext"),
                        PropertyValuesString = c.String(nullable: false, storeType: "ntext"),
                        PropertyValuesBinary = c.Binary(nullable: false, storeType: "image"),
                        LastUpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.aspnet_Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.aspnet_Roles",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                        RoleName = c.String(maxLength: 512),
                        LoweredRoleName = c.String(maxLength: 512),
                        Description = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.aspnet_Applications", t => t.ApplicationId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.aspnet_SchemaVersions",
                c => new
                    {
                        Feature = c.String(nullable: false, maxLength: 128),
                        CompatibleSchemaVersion = c.String(nullable: false, maxLength: 128),
                        IsCurrentVersion = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Feature, t.CompatibleSchemaVersion });
            
            CreateTable(
                "dbo.aspnet_WebEvent_Events",
                c => new
                    {
                        EventId = c.String(nullable: false, maxLength: 32, fixedLength: true, unicode: false),
                        EventTimeUtc = c.DateTime(nullable: false),
                        EventTime = c.DateTime(nullable: false),
                        EventType = c.String(maxLength: 512),
                        EventSequence = c.Decimal(nullable: false, precision: 19, scale: 0),
                        EventOccurrence = c.Decimal(nullable: false, precision: 19, scale: 0),
                        EventCode = c.Int(nullable: false),
                        EventDetailCode = c.Int(nullable: false),
                        Message = c.String(maxLength: 2048),
                        ApplicationPath = c.String(maxLength: 512),
                        ApplicationVirtualPath = c.String(maxLength: 512),
                        MachineName = c.String(maxLength: 512),
                        RequestUrl = c.String(maxLength: 2048),
                        ExceptionType = c.String(maxLength: 512),
                        Details = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.AvgExchangeRatesView",
                c => new
                    {
                        currency_id = c.Int(nullable: false),
                        currency_code = c.String(maxLength: 10, unicode: false),
                        year = c.Int(),
                        month = c.Int(),
                        rate = c.Decimal(precision: 38, scale: 6),
                    })
                .PrimaryKey(t => t.currency_id);
            
            CreateTable(
                "dbo.CategoriesView",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Changes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RegisterProjectId = c.Guid(),
                        Name = c.String(maxLength: 500),
                        Type = c.String(maxLength: 500),
                        Conditions = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CitizenDocument",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsAdministrativeUse = c.Boolean(nullable: false),
                        IsAwaitingResponse = c.Boolean(nullable: false),
                        IsTradeSecret = c.Boolean(nullable: false),
                        ApplicantType = c.Int(nullable: false),
                        DocumentType = c.Int(nullable: false),
                        MonitoringType = c.Int(nullable: false),
                        PriorityType = c.Int(nullable: false),
                        StateType = c.Int(nullable: false),
                        AppendixCount = c.Int(nullable: false),
                        CopiesCount = c.Int(nullable: false),
                        PageCount = c.Int(nullable: false),
                        RepeatCount = c.Int(nullable: false),
                        SortNumber = c.Int(nullable: false),
                        OutgoingType = c.Int(nullable: false),
                        IsNotification = c.Boolean(nullable: false),
                        NotificationCount = c.Int(nullable: false),
                        IsAttachments = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        AwaitingResponseDate = c.DateTime(),
                        DocumentDate = c.DateTime(),
                        OutgoingDate = c.DateTime(),
                        ProtocolDate = c.DateTime(),
                        MonitoringDate = c.DateTime(),
                        ApplicantAddress = c.String(maxLength: 512),
                        ApplicantEmail = c.String(maxLength: 4000),
                        ApplicantName = c.String(maxLength: 512),
                        ApplicantPhone = c.String(maxLength: 512),
                        BlankNumber = c.String(maxLength: 512),
                        CorrespondentsInfo = c.String(maxLength: 512),
                        Number = c.String(maxLength: 512),
                        OutgoingNumber = c.String(maxLength: 512),
                        SortingNumber = c.String(maxLength: 512),
                        SortingOutgoingNumber = c.String(maxLength: 512),
                        Note = c.String(maxLength: 4000),
                        Summary = c.String(maxLength: 4000),
                        AdministrativeTypeDictionaryValue = c.String(maxLength: 4000),
                        ApplicantCategoryDictionaryValue = c.String(maxLength: 4000),
                        CauseCitizenDictionaryValue = c.String(maxLength: 4000),
                        CitizenCategoryDictionaryValue = c.String(maxLength: 4000),
                        CitizenResultDictionaryValue = c.String(maxLength: 4000),
                        CitizenTypeDictionaryValue = c.String(maxLength: 4000),
                        DocumentKindDictionaryValue = c.String(maxLength: 4000),
                        FormDeliveryDictionaryValue = c.String(maxLength: 4000),
                        FormSendingDictionaryValue = c.String(maxLength: 4000),
                        KatoDictionaryValue = c.String(maxLength: 4000),
                        LanguageDictionaryValue = c.String(maxLength: 4000),
                        NomenclatureDictionaryValue = c.String(maxLength: 4000),
                        QuestionDesignDictionaryValue = c.String(maxLength: 4000),
                        SigningFormDictionaryValue = c.String(maxLength: 4000),
                        AgreementsValue = c.String(maxLength: 4000),
                        ExecutorsValue = c.String(maxLength: 4000),
                        ReadersValue = c.String(maxLength: 4000),
                        RecipientsValue = c.String(maxLength: 4000),
                        RegistratorValue = c.String(maxLength: 4000),
                        ResponsibleValue = c.String(maxLength: 4000),
                        SignerValue = c.String(maxLength: 4000),
                        CorrespondentsValue = c.String(maxLength: 4000),
                        MonitoringAuthorValue = c.String(maxLength: 4000),
                        MonitoringNote = c.String(maxLength: 4000),
                        AnswersValue = c.String(maxLength: 4000),
                        CompleteDocumentsValue = c.String(maxLength: 4000),
                        EditDocumentsValue = c.String(maxLength: 4000),
                        RepealDocumentsValue = c.String(maxLength: 4000),
                        DisplayName = c.String(maxLength: 4000),
                        AutoAnswersValue = c.String(maxLength: 4000),
                        AutoAnswersTempValue = c.String(maxLength: 4000),
                        AutoCompleteDocumentsValue = c.String(maxLength: 4000),
                        AutoEditDocumentsValue = c.String(maxLength: 4000),
                        AutoRepealDocumentsValue = c.String(maxLength: 4000),
                        AutoAwaitingResponseDate = c.DateTime(storeType: "date"),
                        AutoDocumentDate = c.DateTime(storeType: "date"),
                        AutoOutgoingDate = c.DateTime(storeType: "date"),
                        AutoProtocolDate = c.DateTime(storeType: "date"),
                        AutoMonitoringDate = c.DateTime(storeType: "date"),
                        FactExecutionDate = c.DateTime(),
                        FirstExecutionDate = c.DateTime(),
                        ExecutionDate = c.DateTime(),
                        AutoFactExecutionDate = c.DateTime(storeType: "date"),
                        AutoFirstExecutionDate = c.DateTime(storeType: "date"),
                        AutoExecutionDate = c.DateTime(storeType: "date"),
                        Counters = c.String(maxLength: 4000),
                        DocumentDictionaryTypeValue = c.String(maxLength: 4000),
                        ResolutionValue = c.String(maxLength: 4000),
                        SourceValue = c.String(maxLength: 4000),
                        DestinationValue = c.String(maxLength: 4000),
                        OwnerValue = c.String(maxLength: 4000),
                        Country = c.String(maxLength: 4000),
                        Area = c.String(maxLength: 4000),
                        Postcode = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 4000),
                        Department = c.String(maxLength: 4000),
                        City = c.String(maxLength: 4000),
                        Address = c.String(maxLength: 4000),
                        NumberBill = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        SuperMainDocumentId = c.Guid(),
                        ModifiedUser = c.String(maxLength: 4000),
                        DateDispatch = c.DateTime(),
                        DispatchNote = c.String(maxLength: 4000),
                        Digest = c.String(maxLength: 4000),
                        Text = c.String(maxLength: 4000),
                        Recipient = c.String(maxLength: 4000),
                        QrCode = c.Binary(storeType: "image"),
                        FulfilledDate = c.DateTime(),
                        State = c.String(maxLength: 510),
                        Monitoring = c.String(maxLength: 510),
                        Priority = c.String(maxLength: 510),
                    })
                .PrimaryKey(t => new { t.Id, t.IsDeleted, t.IsAdministrativeUse, t.IsAwaitingResponse, t.IsTradeSecret, t.ApplicantType, t.DocumentType, t.MonitoringType, t.PriorityType, t.StateType, t.AppendixCount, t.CopiesCount, t.PageCount, t.RepeatCount, t.SortNumber, t.OutgoingType, t.IsNotification, t.NotificationCount, t.IsAttachments, t.IsArchive });
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.String(),
                        AuthorId = c.Guid(nullable: false),
                        AuthorValue = c.String(nullable: false, maxLength: 4000),
                        refObjectId = c.Guid(nullable: false),
                        refParentComment = c.Guid(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommissionConclusionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        CommissionType = c.Int(),
                        Code = c.Int(),
                        Name2 = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommissionDrugDosage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommissionId = c.Int(nullable: false),
                        ConclusionTypeId = c.Int(),
                        ConclusionComment = c.String(),
                        DrugDosageId = c.Long(nullable: false),
                        StageId = c.Guid(nullable: false),
                        ExpertResultId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Commissions", t => t.CommissionId)
                .ForeignKey("dbo.EXP_DrugDosage", t => t.DrugDosageId)
                .ForeignKey("dbo.EXP_ExpertiseStageDosageResult", t => t.ExpertResultId)
                .ForeignKey("dbo.EXP_ExpertiseStage", t => t.StageId)
                .ForeignKey("dbo.CommissionConclusionTypes", t => t.ConclusionTypeId)
                .Index(t => t.CommissionId)
                .Index(t => t.ConclusionTypeId)
                .Index(t => t.DrugDosageId)
                .Index(t => t.StageId)
                .Index(t => t.ExpertResultId);
            
            CreateTable(
                "dbo.Commissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        FullNumber = c.String(nullable: false, maxLength: 100),
                        KindId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsComplete = c.Boolean(nullable: false),
                        Comment = c.String(maxLength: 4000),
                        IsNeedSendTimeOverNotifications = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommissionKinds", t => t.KindId)
                .ForeignKey("dbo.CommissionTypes", t => t.TypeId)
                .Index(t => t.KindId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.CommissionKinds",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        ShortName = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommissionQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommissionId = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommissionQuestionTypes", t => t.TypeId)
                .ForeignKey("dbo.Commissions", t => t.CommissionId)
                .Index(t => t.CommissionId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.CommissionQuestionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommissionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommissionUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommissionId = c.Int(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                        UnitTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Commissions", t => t.CommissionId, cascadeDelete: true)
                .ForeignKey("dbo.CommissionUnitTypes", t => t.UnitTypeId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.CommissionId)
                .Index(t => t.EmployeeId)
                .Index(t => t.UnitTypeId);
            
            CreateTable(
                "dbo.CommissionUnitTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 300),
                        MaxCount = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        LastName = c.String(maxLength: 4000),
                        FirstName = c.String(maxLength: 4000),
                        MiddleName = c.String(maxLength: 4000),
                        FullName = c.String(maxLength: 4000),
                        ShortName = c.String(maxLength: 4000),
                        PositionId = c.Guid(),
                        Sex = c.Int(nullable: false),
                        Email = c.String(maxLength: 510),
                        Phone = c.String(maxLength: 510),
                        Login = c.String(maxLength: 255),
                        DisplayName = c.String(maxLength: 4000),
                        DeputyId = c.String(maxLength: 4000),
                        DeputyValue = c.String(maxLength: 4000),
                        AssistantsId = c.String(maxLength: 4000),
                        AssistantsValue = c.String(maxLength: 4000),
                        Cabinet = c.String(maxLength: 4000),
                        Photo = c.Binary(storeType: "image"),
                        TimeAgreement = c.Int(nullable: false),
                        Number = c.String(maxLength: 4000),
                        Birthday = c.DateTime(storeType: "date"),
                        Birthplace = c.String(maxLength: 4000),
                        UlNumber = c.String(maxLength: 4000),
                        UlDate = c.DateTime(storeType: "date"),
                        UlOwner = c.String(maxLength: 4000),
                        Iin = c.String(maxLength: 4000),
                        PlaceLive = c.String(maxLength: 4000),
                        PlaceRegistration = c.String(maxLength: 4000),
                        PhoneHome = c.String(maxLength: 4000),
                        PhoneMobile = c.String(maxLength: 4000),
                        FamilyStatus = c.Int(nullable: false),
                        Families = c.String(maxLength: 4000),
                        Education = c.String(maxLength: 4000),
                        ExperienceTotal = c.Int(nullable: false),
                        ExperienceSpec = c.Int(nullable: false),
                        IsDegree = c.Boolean(nullable: false),
                        DegreeDate = c.DateTime(storeType: "date"),
                        DegreeSpec = c.String(maxLength: 4000),
                        MilitaryType = c.Int(nullable: false),
                        MilitaryCategory = c.String(maxLength: 4000),
                        MilitarySostav = c.String(maxLength: 4000),
                        MilitaryRank = c.String(maxLength: 4000),
                        MilitaryVus = c.String(maxLength: 4000),
                        MilitaryLocation = c.String(maxLength: 4000),
                        MilitaryLastDate = c.DateTime(storeType: "date"),
                        DateFile = c.DateTime(storeType: "date"),
                        EducationNumber = c.String(maxLength: 4000),
                        EducationDate = c.DateTime(storeType: "date"),
                        EducationSpec = c.String(maxLength: 4000),
                        EducationQual = c.String(maxLength: 4000),
                        OrganizationId = c.Guid(nullable: false),
                        NationalityId = c.String(maxLength: 4000),
                        NationalityValue = c.String(maxLength: 4000),
                        DegreeName = c.String(maxLength: 4000),
                        IsAcademic = c.Boolean(nullable: false),
                        AcademicDate = c.DateTime(storeType: "date"),
                        AcademicSpec = c.String(maxLength: 4000),
                        AcademicName = c.String(maxLength: 4000),
                        Education2 = c.String(maxLength: 4000),
                        EducationNumber2 = c.String(maxLength: 4000),
                        EducationDate2 = c.DateTime(storeType: "date"),
                        EducationSpec2 = c.String(maxLength: 4000),
                        EducationQual2 = c.String(maxLength: 4000),
                        Education3 = c.String(maxLength: 4000),
                        EducationNumber3 = c.String(maxLength: 4000),
                        EducationDate3 = c.DateTime(storeType: "date"),
                        EducationSpec3 = c.String(maxLength: 4000),
                        EducationQual3 = c.String(maxLength: 4000),
                        TerminationDate = c.DateTime(storeType: "date"),
                        MilitaryRankId = c.String(maxLength: 4000),
                        MilitarySostavId = c.String(maxLength: 4000),
                        AcademicNameId = c.String(maxLength: 4000),
                        DegreeNameId = c.String(maxLength: 4000),
                        CauseLayoffsId = c.String(maxLength: 4000),
                        CauseLayoffsValue = c.String(maxLength: 4000),
                        QualificationCategoryId = c.String(maxLength: 4000),
                        QualificationCategoryValue = c.String(maxLength: 4000),
                        IsGuide = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(),
                        LastLoginDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.OrganizationId)
                .ForeignKey("dbo.Units", t => t.PositionId)
                .Index(t => t.PositionId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.ContractComments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 4000),
                        AuthorId = c.Guid(nullable: false),
                        ContractId = c.Guid(nullable: false),
                        Sended = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.ContractId)
                .ForeignKey("dbo.Employees", t => t.AuthorId)
                .Index(t => t.AuthorId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ManufacturerOrganizationId = c.Guid(),
                        ApplicantOrganizationId = c.Guid(),
                        DoverennostTypeDicId = c.Guid(),
                        DoverennostNumber = c.String(maxLength: 500),
                        DoverennostCreatedDate = c.DateTime(storeType: "date"),
                        DoverennostExpiryDate = c.DateTime(storeType: "date"),
                        HolderOrganizationId = c.Guid(),
                        PayerOrganizationId = c.Guid(),
                        Number = c.String(maxLength: 500),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(storeType: "date"),
                        ContractId = c.Guid(),
                        Type = c.Int(),
                        ContractDate = c.DateTime(storeType: "date"),
                        StartDate = c.DateTime(storeType: "date"),
                        EndDate = c.DateTime(storeType: "date"),
                        IsExpired = c.Boolean(),
                        IsSite = c.Boolean(),
                        IsHasDoverennostNumber = c.Boolean(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        PayerTranslationOrganizationId = c.Guid(),
                        StatusId = c.Guid(),
                        SignerId = c.Guid(),
                        ContractAdditionTypeId = c.Guid(),
                        HolderTypeId = c.Guid(),
                        IsHasDigCert = c.Boolean(),
                        IsHasAgentDocNumber = c.Boolean(),
                        AgentDocTypeDicId = c.Guid(),
                        AgentDocNumber = c.String(maxLength: 500),
                        AgentDocCreateDate = c.DateTime(storeType: "date"),
                        AgentDocExpiryDate = c.DateTime(storeType: "date"),
                        AgentOrganizationId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.ApplicantOrganizationId)
                .ForeignKey("dbo.Organizations", t => t.HolderOrganizationId)
                .ForeignKey("dbo.Organizations", t => t.PayerOrganizationId)
                .ForeignKey("dbo.Organizations", t => t.PayerTranslationOrganizationId)
                .ForeignKey("dbo.Organizations", t => t.AgentOrganizationId)
                .ForeignKey("dbo.Organizations", t => t.ManufacturerOrganizationId)
                .ForeignKey("dbo.Dictionaries", t => t.ContractAdditionTypeId)
                .ForeignKey("dbo.Dictionaries", t => t.DoverennostTypeDicId)
                .ForeignKey("dbo.Dictionaries", t => t.AgentDocTypeDicId)
                .ForeignKey("dbo.Dictionaries", t => t.StatusId)
                .ForeignKey("dbo.Dictionaries", t => t.HolderTypeId)
                .ForeignKey("dbo.Contracts", t => t.ContractId)
                .ForeignKey("dbo.Employees", t => t.SignerId)
                .Index(t => t.ManufacturerOrganizationId)
                .Index(t => t.ApplicantOrganizationId)
                .Index(t => t.DoverennostTypeDicId)
                .Index(t => t.HolderOrganizationId)
                .Index(t => t.PayerOrganizationId)
                .Index(t => t.ContractId)
                .Index(t => t.PayerTranslationOrganizationId)
                .Index(t => t.StatusId)
                .Index(t => t.SignerId)
                .Index(t => t.ContractAdditionTypeId)
                .Index(t => t.HolderTypeId)
                .Index(t => t.AgentDocTypeDicId)
                .Index(t => t.AgentOrganizationId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        NameKz = c.String(maxLength: 500),
                        NameRu = c.String(maxLength: 500),
                        NameEn = c.String(maxLength: 500),
                        CountryDicId = c.Guid(),
                        AddressLegal = c.String(maxLength: 500),
                        AddressFact = c.String(maxLength: 500),
                        Phone = c.String(maxLength: 500),
                        Fax = c.String(maxLength: 500),
                        Email = c.String(maxLength: 500),
                        BossFio = c.String(maxLength: 500),
                        BossPosition = c.String(maxLength: 500),
                        ContactFio = c.String(maxLength: 500),
                        ContactPosition = c.String(maxLength: 500),
                        ContactPhone = c.String(maxLength: 500),
                        ContactFax = c.String(maxLength: 500),
                        ContactEmail = c.String(maxLength: 500),
                        OrgManufactureTypeDicId = c.Guid(),
                        DocNumber = c.String(maxLength: 500),
                        DocDate = c.DateTime(storeType: "date"),
                        DocExpiryDate = c.DateTime(storeType: "date"),
                        ObjectId = c.Guid(),
                        OpfTypeDicId = c.Guid(),
                        BankName = c.String(maxLength: 500),
                        BankIik = c.String(maxLength: 500),
                        BankCurencyDicId = c.Guid(),
                        BankSwift = c.String(maxLength: 500),
                        Bin = c.String(maxLength: 500),
                        IsResident = c.Boolean(nullable: false),
                        PayerTypeDicId = c.Guid(),
                        BossLastName = c.String(maxLength: 100),
                        BossFirstName = c.String(maxLength: 100),
                        BossMiddleName = c.String(maxLength: 100),
                        PaymentBill = c.String(maxLength: 500),
                        Iin = c.String(maxLength: 500),
                        BankBik = c.String(maxLength: 500),
                        OriginalOrgId = c.Guid(),
                        Filial = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OriginalOrgId)
                .Index(t => t.OriginalOrgId);
            
            CreateTable(
                "dbo.EXP_DirectionToPays",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(nullable: false, maxLength: 512),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        DirectionDate = c.DateTime(nullable: false),
                        PayerId = c.Guid(),
                        PayerValue = c.String(maxLength: 4000),
                        CreateEmployeeId = c.Guid(nullable: false),
                        CreateEmployeeValue = c.String(nullable: false, maxLength: 1024),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        InvoiceCode1C = c.String(maxLength: 512),
                        InvoiceNumber1C = c.String(maxLength: 512),
                        InvoiceDatetime1C = c.DateTime(),
                        PaymentDatetime1C = c.DateTime(),
                        PaymentValue1C = c.Decimal(precision: 18, scale: 2),
                        PaymentComment1C = c.String(maxLength: 4000),
                        StatusId = c.Guid(nullable: false),
                        StatusValue = c.String(maxLength: 4000),
                        Type = c.Int(nullable: false),
                        TypeValue = c.String(maxLength: 4000),
                        PageCount = c.Int(),
                        PriceForPage = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.StatusId)
                .ForeignKey("dbo.Organizations", t => t.PayerId)
                .ForeignKey("dbo.Employees", t => t.CreateEmployeeId)
                .Index(t => t.PayerId)
                .Index(t => t.CreateEmployeeId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.EXP_DirectionToPays_PriceList",
                c => new
                    {
                        DirectionToPayId = c.Guid(nullable: false),
                        PriceListId = c.Guid(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Count = c.Int(),
                        Total = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.DirectionToPayId, t.PriceListId })
                .ForeignKey("dbo.EXP_PriceList", t => t.PriceListId)
                .ForeignKey("dbo.EXP_DirectionToPays", t => t.DirectionToPayId)
                .Index(t => t.DirectionToPayId)
                .Index(t => t.PriceListId);
            
            CreateTable(
                "dbo.EXP_PriceList",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 450),
                        NameKz = c.String(maxLength: 450),
                        NameEn = c.String(maxLength: 450),
                        PriceRegisterForeign = c.Decimal(precision: 18, scale: 2),
                        PriceRegisterForeignNds = c.Decimal(precision: 18, scale: 2),
                        PriceReRegisterForeign = c.Decimal(precision: 18, scale: 2),
                        PriceReRegisterForeignNds = c.Decimal(precision: 18, scale: 2),
                        PriceRegisterKz = c.Decimal(precision: 18, scale: 2),
                        PriceRegisterKzNds = c.Decimal(precision: 18, scale: 2),
                        PriceReRegisterKz = c.Decimal(precision: 18, scale: 2),
                        PriceReRegisterKzNds = c.Decimal(precision: 18, scale: 2),
                        Category = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DrugDeclaration",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeId = c.Int(nullable: false),
                        Number = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        ContractId = c.Guid(),
                        AccelerationTypeId = c.Int(),
                        AccelerationNumber = c.String(maxLength: 500),
                        AccelerationDate = c.DateTime(storeType: "date"),
                        AccelerationNote = c.String(maxLength: 500),
                        NameRu = c.String(maxLength: 500),
                        NameKz = c.String(maxLength: 500),
                        NameEn = c.String(maxLength: 500),
                        MnnId = c.Int(),
                        MnnComment = c.String(maxLength: 500),
                        DrugFormId = c.Int(),
                        DrugFormRu = c.String(maxLength: 500),
                        DrugFormKz = c.String(maxLength: 500),
                        AtxId = c.Int(),
                        OriginalName = c.String(maxLength: 500),
                        SaleTypeId = c.Int(),
                        Dosage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DosageMeasureTypeId = c.Long(),
                        DosageNoteKz = c.String(maxLength: 500),
                        DosageNoteRu = c.String(maxLength: 500),
                        ConcentrationRu = c.String(maxLength: 500),
                        ConcentrationKz = c.String(maxLength: 500),
                        IsGrls = c.Boolean(nullable: false),
                        Transportation = c.String(maxLength: 500),
                        ManufactureTypeId = c.Int(),
                        IsGmp = c.Boolean(nullable: false),
                        GmpExpiryDate = c.DateTime(storeType: "date"),
                        BestBefore = c.String(maxLength: 500),
                        ProposedShelfLife = c.Double(),
                        ProposedShelfLifeMeasureId = c.Int(),
                        BestBeforeMeasureTypeDicId = c.Long(),
                        AppPeriodOpen = c.String(maxLength: 500),
                        AppPeriodOpenMeasureDicId = c.Long(),
                        AppPeriodMix = c.String(maxLength: 500),
                        AppPeriodMixMeasureDicId = c.Long(),
                        StorageConditions1 = c.String(maxLength: 500),
                        StorageConditions2 = c.String(maxLength: 500),
                        IsConvention = c.Boolean(nullable: false),
                        DesignDate = c.DateTime(),
                        DesignNote = c.String(maxLength: 2000),
                        IsSigned = c.Boolean(nullable: false),
                        FirstSendDate = c.DateTime(),
                        SendDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        ExecuterId = c.Guid(),
                        ExpeditedType = c.String(maxLength: 2000),
                        RegisterId = c.Int(),
                        GrlsNote = c.String(maxLength: 2000),
                        OtdIds = c.String(maxLength: 4000),
                        AtxComment = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_SaleType", t => t.SaleTypeId)
                .ForeignKey("dbo.sr_measures", t => t.DosageMeasureTypeId)
                .ForeignKey("dbo.sr_measures", t => t.BestBeforeMeasureTypeDicId)
                .ForeignKey("dbo.sr_measures", t => t.AppPeriodMixMeasureDicId)
                .ForeignKey("dbo.sr_measures", t => t.AppPeriodOpenMeasureDicId)
                .ForeignKey("dbo.sr_register", t => t.RegisterId)
                .ForeignKey("dbo.sr_atc_codes", t => t.AtxId)
                .ForeignKey("dbo.sr_dosage_forms", t => t.DrugFormId)
                .ForeignKey("dbo.sr_international_names", t => t.MnnId)
                .ForeignKey("dbo.EXP_DIC_Type", t => t.TypeId)
                .ForeignKey("dbo.EXP_DIC_AccelerationType", t => t.AccelerationTypeId)
                .ForeignKey("dbo.EXP_DIC_ManufactureType", t => t.ManufactureTypeId)
                .ForeignKey("dbo.EXP_DIC_PeriodMeasure", t => t.ProposedShelfLifeMeasureId)
                .ForeignKey("dbo.EXP_DIC_Status", t => t.StatusId)
                .Index(t => t.TypeId)
                .Index(t => t.StatusId)
                .Index(t => t.AccelerationTypeId)
                .Index(t => t.MnnId)
                .Index(t => t.DrugFormId)
                .Index(t => t.AtxId)
                .Index(t => t.SaleTypeId)
                .Index(t => t.DosageMeasureTypeId)
                .Index(t => t.ManufactureTypeId)
                .Index(t => t.ProposedShelfLifeMeasureId)
                .Index(t => t.BestBeforeMeasureTypeDicId)
                .Index(t => t.AppPeriodOpenMeasureDicId)
                .Index(t => t.AppPeriodMixMeasureDicId)
                .Index(t => t.RegisterId);
            
            CreateTable(
                "dbo.EXP_CertificateOfCompletion",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(maxLength: 500),
                        DicStageId = c.Int(),
                        DrugDeclarationId = c.Guid(),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        StatusId = c.Guid(),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                        SendDate = c.DateTime(),
                        CreateEmployeeId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_Stage", t => t.DicStageId)
                .ForeignKey("dbo.Dictionaries", t => t.StatusId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .ForeignKey("dbo.Employees", t => t.CreateEmployeeId)
                .Index(t => t.DicStageId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.StatusId)
                .Index(t => t.CreateEmployeeId);
            
            CreateTable(
                "dbo.EXP_DIC_Stage",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DIC_StageResult",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 50),
                        NameRu = c.String(nullable: false, maxLength: 2000),
                        NameKz = c.String(nullable: false, maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_ExpertiseStage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeclarationId = c.Guid(nullable: false),
                        StageId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        FactEndDate = c.DateTime(),
                        ResultId = c.Int(),
                        ParentStageId = c.Guid(),
                        IsHistory = c.Boolean(nullable: false),
                        OtdIds = c.String(maxLength: 4000),
                        OtdRemarks = c.String(),
                        IsSuspended = c.Boolean(nullable: false),
                        SuspendedStartDate = c.DateTime(),
                        IsNew = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_StageStatus", t => t.StatusId)
                .ForeignKey("dbo.EXP_ExpertiseStage", t => t.ParentStageId)
                .ForeignKey("dbo.EXP_DIC_StageResult", t => t.ResultId)
                .ForeignKey("dbo.EXP_DIC_Stage", t => t.StageId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DeclarationId)
                .Index(t => t.DeclarationId)
                .Index(t => t.StageId)
                .Index(t => t.StatusId)
                .Index(t => t.ResultId)
                .Index(t => t.ParentStageId);
            
            CreateTable(
                "dbo.CommissionDrugDosageNeedCommission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DrugDosageId = c.Long(nullable: false),
                        StageId = c.Guid(nullable: false),
                        IsNeedEs = c.Boolean(nullable: false),
                        IsNeedFmk = c.Boolean(nullable: false),
                        IsNeedFmc = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DrugDosage", t => t.DrugDosageId)
                .ForeignKey("dbo.EXP_ExpertiseStage", t => t.StageId)
                .Index(t => t.DrugDosageId)
                .Index(t => t.StageId);
            
            CreateTable(
                "dbo.EXP_DrugDosage",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RegNumber = c.String(maxLength: 50),
                        DrugDeclarationId = c.Guid(nullable: false),
                        Dosage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DosageMeasureTypeId = c.Long(),
                        DosageNoteKz = c.String(maxLength: 500),
                        DosageNoteRu = c.String(maxLength: 500),
                        ConcentrationRu = c.String(maxLength: 500),
                        ConcentrationKz = c.String(maxLength: 500),
                        SaleTypeId = c.Int(),
                        BestBefore = c.String(maxLength: 500),
                        BestBeforeMeasureTypeDicId = c.Long(),
                        AppPeriodOpen = c.String(maxLength: 500),
                        AppPeriodOpenMeasureDicId = c.Long(),
                        AppPeriodMix = c.String(maxLength: 500),
                        AppPeriodMixMeasureDicId = c.Long(),
                        RegisterId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_SaleType", t => t.SaleTypeId)
                .ForeignKey("dbo.sr_measures", t => t.AppPeriodMixMeasureDicId)
                .ForeignKey("dbo.sr_measures", t => t.AppPeriodOpenMeasureDicId)
                .ForeignKey("dbo.sr_measures", t => t.BestBeforeMeasureTypeDicId)
                .ForeignKey("dbo.sr_measures", t => t.DosageMeasureTypeId)
                .ForeignKey("dbo.sr_register", t => t.RegisterId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.DosageMeasureTypeId)
                .Index(t => t.SaleTypeId)
                .Index(t => t.BestBeforeMeasureTypeDicId)
                .Index(t => t.AppPeriodOpenMeasureDicId)
                .Index(t => t.AppPeriodMixMeasureDicId)
                .Index(t => t.RegisterId);
            
            CreateTable(
                "dbo.EXP_DIC_SaleType",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DrugAppDosageRemark",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NameResult = c.String(maxLength: 2000),
                        IsMark = c.Boolean(nullable: false),
                        RemarkTypeId = c.Int(),
                        Note = c.String(maxLength: 2000),
                        RemarkDate = c.DateTime(),
                        FixedDate = c.DateTime(),
                        DrugDosageId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_RemarkType", t => t.RemarkTypeId)
                .ForeignKey("dbo.EXP_DrugDosage", t => t.DrugDosageId)
                .Index(t => t.RemarkTypeId)
                .Index(t => t.DrugDosageId);
            
            CreateTable(
                "dbo.EXP_DIC_RemarkType",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DrugAppDosageResult",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NameResult = c.String(maxLength: 2000),
                        IsMark = c.Boolean(nullable: false),
                        RemarkTypeId = c.Int(),
                        Note = c.String(maxLength: 2000),
                        RemarkDate = c.DateTime(),
                        FixedDate = c.DateTime(),
                        DrugDosageId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_RemarkType", t => t.RemarkTypeId)
                .ForeignKey("dbo.EXP_DrugDosage", t => t.DrugDosageId)
                .Index(t => t.RemarkTypeId)
                .Index(t => t.DrugDosageId);
            
            CreateTable(
                "dbo.EXP_DrugCorespondenceRemark",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugCorespondenceId = c.Guid(nullable: false),
                        RemarkTypeId = c.Int(),
                        NameRemark = c.String(),
                        ExecuterId = c.Guid(),
                        RemarkDate = c.DateTime(),
                        AnswerRemark = c.String(),
                        IsFixed = c.Boolean(nullable: false),
                        IsAccepted = c.Boolean(nullable: false),
                        FixedDate = c.DateTime(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DrugCorespondence", t => t.DrugCorespondenceId)
                .ForeignKey("dbo.EXP_DIC_RemarkType", t => t.RemarkTypeId)
                .ForeignKey("dbo.Employees", t => t.ExecuterId)
                .Index(t => t.DrugCorespondenceId)
                .Index(t => t.RemarkTypeId)
                .Index(t => t.ExecuterId);
            
            CreateTable(
                "dbo.EXP_DrugCorespondence",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DrugDeclarationId = c.Guid(nullable: false),
                        AuthorId = c.Guid(),
                        SignatoryId = c.Guid(),
                        MatchingId = c.Guid(),
                        DateCreate = c.DateTime(nullable: false),
                        DateSend = c.DateTime(),
                        DateRead = c.DateTime(),
                        NumberLetter = c.String(maxLength: 50),
                        Subject = c.String(maxLength: 200),
                        Note = c.String(),
                        StageId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        KindId = c.Int(nullable: false),
                        StatusId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsReadOnly = c.Boolean(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.StatusId)
                .ForeignKey("dbo.EXP_DIC_CorespondenceKind", t => t.KindId)
                .ForeignKey("dbo.EXP_DIC_CorespondenceSubject", t => t.SubjectId)
                .ForeignKey("dbo.EXP_DIC_CorespondenceType", t => t.TypeId)
                .ForeignKey("dbo.EXP_DIC_Stage", t => t.StageId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .ForeignKey("dbo.Employees", t => t.AuthorId)
                .ForeignKey("dbo.Employees", t => t.MatchingId)
                .ForeignKey("dbo.Employees", t => t.SignatoryId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.AuthorId)
                .Index(t => t.SignatoryId)
                .Index(t => t.MatchingId)
                .Index(t => t.StageId)
                .Index(t => t.TypeId)
                .Index(t => t.KindId)
                .Index(t => t.StatusId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Dictionaries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(maxLength: 4000),
                        Name = c.String(maxLength: 4000),
                        Type = c.String(maxLength: 4000),
                        ExpireDate = c.String(maxLength: 4000),
                        Year = c.String(maxLength: 4000),
                        Note = c.String(maxLength: 4000),
                        DepartmentsId = c.String(maxLength: 4000),
                        DepartmentsValue = c.String(maxLength: 4000),
                        ParentId = c.Guid(),
                        DisplayName = c.String(maxLength: 4000),
                        EmployeesValue = c.String(maxLength: 4000),
                        EmployeesId = c.String(maxLength: 4000),
                        NameKz = c.String(maxLength: 4000),
                        IsGuide = c.Boolean(nullable: false),
                        OrganizationId = c.Guid(),
                        AdditionalInfo = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.EXP_Activities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeId = c.Guid(nullable: false),
                        TypeValue = c.String(maxLength: 4000),
                        Text = c.String(nullable: false, maxLength: 4000),
                        StatusId = c.Guid(nullable: false),
                        StatusValue = c.String(maxLength: 4000),
                        AuthorId = c.Guid(nullable: false),
                        AuthorValue = c.String(maxLength: 4000),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ExecutedDate = c.DateTime(),
                        ClosedDate = c.DateTime(),
                        DocumentId = c.Guid(nullable: false),
                        DocumentValue = c.String(maxLength: 4000),
                        DocumentTypeId = c.Guid(nullable: false),
                        DocumentTypeValue = c.String(maxLength: 4000),
                        DocNumber = c.String(maxLength: 255),
                        DocDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.StatusId)
                .ForeignKey("dbo.Dictionaries", t => t.TypeId)
                .ForeignKey("dbo.Dictionaries", t => t.DocumentTypeId)
                .ForeignKey("dbo.Employees", t => t.AuthorId)
                .Index(t => t.TypeId)
                .Index(t => t.StatusId)
                .Index(t => t.AuthorId)
                .Index(t => t.DocumentTypeId);
            
            CreateTable(
                "dbo.EXP_Tasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeId = c.Guid(nullable: false),
                        TypeValue = c.String(maxLength: 4000),
                        StatusId = c.Guid(nullable: false),
                        StatusValue = c.String(maxLength: 4000),
                        OrderNumber = c.Int(nullable: false),
                        Number = c.String(maxLength: 256),
                        Text = c.String(maxLength: 4000),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ExecutedDate = c.DateTime(),
                        ClosedDate = c.DateTime(),
                        AuthorId = c.Guid(nullable: false),
                        AuthorValue = c.String(maxLength: 4000),
                        ExecutorId = c.Guid(nullable: false),
                        ExecutorValue = c.String(maxLength: 4000),
                        ActivityId = c.Guid(nullable: false),
                        ParentTaskId = c.Guid(),
                        Comment = c.String(maxLength: 4000),
                        DigSign = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_Tasks", t => t.ParentTaskId)
                .ForeignKey("dbo.EXP_Activities", t => t.ActivityId)
                .ForeignKey("dbo.Dictionaries", t => t.TypeId)
                .ForeignKey("dbo.Dictionaries", t => t.StatusId)
                .ForeignKey("dbo.Employees", t => t.AuthorId)
                .ForeignKey("dbo.Employees", t => t.ExecutorId)
                .Index(t => t.TypeId)
                .Index(t => t.StatusId)
                .Index(t => t.AuthorId)
                .Index(t => t.ExecutorId)
                .Index(t => t.ActivityId)
                .Index(t => t.ParentTaskId);
            
            CreateTable(
                "dbo.EXP_AgreementProcSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AgreedDocTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.AgreedDocTypeId)
                .Index(t => t.AgreedDocTypeId);
            
            CreateTable(
                "dbo.EXP_AgreementProcSettingsActivities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SettingId = c.Guid(nullable: false),
                        ActivityTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_AgreementProcSettings", t => t.SettingId)
                .ForeignKey("dbo.Dictionaries", t => t.ActivityTypeId)
                .Index(t => t.SettingId)
                .Index(t => t.ActivityTypeId);
            
            CreateTable(
                "dbo.EXP_AgreementProcSettingsTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ActivityId = c.Guid(nullable: false),
                        TaskTypeId = c.Guid(nullable: false),
                        ParentTaskId = c.Guid(),
                        ExecutorId = c.Guid(),
                        OrderNum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_AgreementProcSettingsTasks", t => t.ParentTaskId)
                .ForeignKey("dbo.EXP_AgreementProcSettingsActivities", t => t.ActivityId)
                .ForeignKey("dbo.Dictionaries", t => t.TaskTypeId)
                .ForeignKey("dbo.Employees", t => t.ExecutorId)
                .Index(t => t.ActivityId)
                .Index(t => t.TaskTypeId)
                .Index(t => t.ParentTaskId)
                .Index(t => t.ExecutorId);
            
            CreateTable(
                "dbo.EXP_DrugPrimaryFinalDocument",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsRKProduct = c.Boolean(),
                        IsDossierSection = c.Boolean(),
                        IsSetDocument = c.Boolean(),
                        IsColorModel = c.Boolean(),
                        IsForbiddenDyes = c.Boolean(),
                        IsFromBlood = c.Boolean(),
                        IsNarcoticDrug = c.Boolean(),
                        IsPhoneticSimilar = c.Boolean(),
                        IsAbilityMislead = c.Boolean(),
                        IsAdvertising = c.Boolean(),
                        IsMNNSimilar = c.Boolean(),
                        ExpertiseNormDoc = c.String(maxLength: 2000),
                        SampleDrug = c.String(maxLength: 2000),
                        ComplianceSeries = c.String(maxLength: 2000),
                        ResidualShelfLife = c.String(maxLength: 2000),
                        SampleSubstance = c.String(maxLength: 2000),
                        SampleStandart = c.String(maxLength: 2000),
                        TestLabRecommend = c.String(maxLength: 2000),
                        MedicalInstruction = c.String(maxLength: 2000),
                        ExpertOpinion = c.String(maxLength: 2000),
                        DrugDosageId = c.Long(nullable: false),
                        StatusId = c.Guid(),
                        ResultId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_PrimaryFinalyDocResult", t => t.ResultId)
                .ForeignKey("dbo.Dictionaries", t => t.StatusId)
                .ForeignKey("dbo.EXP_DrugDosage", t => t.DrugDosageId)
                .Index(t => t.DrugDosageId)
                .Index(t => t.StatusId)
                .Index(t => t.ResultId);
            
            CreateTable(
                "dbo.EXP_DIC_PrimaryFinalyDocResult",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_ExpertiseStageDosage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StageId = c.Guid(nullable: false),
                        DosageId = c.Long(nullable: false),
                        ResultId = c.Int(),
                        FinalDocStatusId = c.Guid(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.FinalDocStatusId)
                .ForeignKey("dbo.EXP_DrugDosage", t => t.DosageId)
                .ForeignKey("dbo.EXP_ExpertiseStage", t => t.StageId)
                .ForeignKey("dbo.EXP_DIC_StageResult", t => t.ResultId)
                .Index(t => t.StageId)
                .Index(t => t.DosageId)
                .Index(t => t.ResultId)
                .Index(t => t.FinalDocStatusId);
            
            CreateTable(
                "dbo.EXP_DrugAnaliseIndicator",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DosageStageId = c.Guid(nullable: false),
                        AnalyseIndicator = c.Int(),
                        Temperature = c.Double(),
                        Humidity = c.Double(),
                        Designation = c.String(maxLength: 500),
                        Demand = c.String(maxLength: 2000),
                        ActualResult = c.String(maxLength: 2000),
                        IsMatches = c.Boolean(),
                        InProtocol = c.Boolean(nullable: false),
                        PositionNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_AnalyseIndicator", t => t.AnalyseIndicator)
                .ForeignKey("dbo.EXP_ExpertiseStageDosage", t => t.DosageStageId)
                .Index(t => t.DosageStageId)
                .Index(t => t.AnalyseIndicator);
            
            CreateTable(
                "dbo.EXP_DIC_AnalyseIndicator",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_ExpertisePharmaceuticalFinalDoc",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Indicator1_1 = c.String(storeType: "ntext"),
                        Indicator2_1 = c.String(storeType: "ntext"),
                        Indicator2_2 = c.String(storeType: "ntext"),
                        Indicator2_3 = c.String(storeType: "ntext"),
                        Indicator2_4 = c.String(storeType: "ntext"),
                        Indicator3_1 = c.String(storeType: "ntext"),
                        Indicator3_2 = c.String(storeType: "ntext"),
                        Indicator3_3 = c.String(storeType: "ntext"),
                        Indicator4 = c.String(storeType: "ntext"),
                        Indicator5 = c.String(storeType: "ntext"),
                        Indicator6 = c.String(storeType: "ntext"),
                        Indicator7 = c.String(storeType: "ntext"),
                        Indicator12 = c.String(storeType: "ntext"),
                        Indicator13_1 = c.String(storeType: "ntext"),
                        Indicator13_2 = c.String(storeType: "ntext"),
                        Indicator13_3 = c.String(storeType: "ntext"),
                        Indicator14 = c.String(storeType: "ntext"),
                        Indicator15 = c.String(storeType: "ntext"),
                        Indicator16 = c.String(storeType: "ntext"),
                        Indicator17 = c.String(storeType: "ntext"),
                        Indicator18 = c.String(storeType: "ntext"),
                        Indicator19 = c.String(storeType: "ntext"),
                        Indicator20 = c.String(storeType: "ntext"),
                        DosageStageId = c.Guid(nullable: false),
                        ExpertConslusion = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_ExpertiseStageDosage", t => t.DosageStageId)
                .Index(t => t.DosageStageId);
            
            CreateTable(
                "dbo.EXP_ExpertisePharmacologicalFinalDoc",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Indicator1 = c.String(storeType: "ntext"),
                        Indicator2 = c.String(storeType: "ntext"),
                        Indicator3 = c.String(storeType: "ntext"),
                        Indicator4 = c.String(storeType: "ntext"),
                        Indicator5 = c.String(storeType: "ntext"),
                        Indicator6 = c.String(storeType: "ntext"),
                        Indicator7 = c.String(storeType: "ntext"),
                        Indicator8 = c.String(storeType: "ntext"),
                        Indicator9 = c.String(storeType: "ntext"),
                        Indicator10 = c.String(storeType: "ntext"),
                        Indicator11 = c.String(storeType: "ntext"),
                        Indicator12 = c.String(storeType: "ntext"),
                        Indicator13 = c.String(storeType: "ntext"),
                        Indicator14 = c.String(storeType: "ntext"),
                        Indicator15 = c.String(storeType: "ntext"),
                        Indicator16 = c.String(storeType: "ntext"),
                        Indicator17 = c.String(storeType: "ntext"),
                        Indicator18 = c.String(storeType: "ntext"),
                        Indicator19 = c.String(storeType: "ntext"),
                        Indicator20 = c.String(storeType: "ntext"),
                        Indicator21 = c.String(storeType: "ntext"),
                        Indicator22 = c.String(storeType: "ntext"),
                        Indicator23 = c.String(storeType: "ntext"),
                        DosageStageId = c.Guid(nullable: false),
                        ExpertConslusion = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_ExpertiseStageDosage", t => t.DosageStageId)
                .Index(t => t.DosageStageId);
            
            CreateTable(
                "dbo.EXP_ExpertiseSafetyreportFinalDoc",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PrimaryConclusion = c.String(storeType: "ntext"),
                        PrimaryConclusionKz = c.String(storeType: "ntext"),
                        PharmaceuticalConclusion = c.String(storeType: "ntext"),
                        PharmaceuticalConclusionKz = c.String(storeType: "ntext"),
                        PharmacologicalConclusion = c.String(storeType: "ntext"),
                        PharmacologicalConclusionKz = c.String(storeType: "ntext"),
                        AnalyticalConclusion = c.String(storeType: "ntext"),
                        AnalyticalConclusionKz = c.String(storeType: "ntext"),
                        Conclusion = c.String(storeType: "ntext"),
                        ConclusionKz = c.String(storeType: "ntext"),
                        DosageStageId = c.Guid(nullable: false),
                        IsAccepted = c.Boolean(),
                        Remark = c.String(maxLength: 500),
                        IsAcceptedKz = c.Boolean(),
                        RemarkKz = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_ExpertiseStageDosage", t => t.DosageStageId)
                .Index(t => t.DosageStageId);
            
            CreateTable(
                "dbo.EXP_ExpertiseStageDosageResult",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StageDosageId = c.Guid(nullable: false),
                        ResultId = c.Int(),
                        ResultDate = c.DateTime(),
                        ResultCreatorId = c.Guid(),
                        CommissionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Commissions", t => t.CommissionId)
                .ForeignKey("dbo.EXP_ExpertiseStageDosage", t => t.StageDosageId, cascadeDelete: true)
                .ForeignKey("dbo.EXP_DIC_StageResult", t => t.ResultId)
                .Index(t => t.StageDosageId)
                .Index(t => t.ResultId)
                .Index(t => t.CommissionId);
            
            CreateTable(
                "dbo.EXP_ExpertisePrimaryFinalDoc",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsRKProduct = c.Boolean(),
                        IsDossierSection = c.Boolean(),
                        IsSetDocument = c.Boolean(),
                        IsColorModel = c.Boolean(),
                        IsForbiddenDyes = c.Boolean(),
                        IsFromBlood = c.Boolean(),
                        IsNarcoticDrug = c.Boolean(),
                        IsPhoneticSimilar = c.Boolean(),
                        IsAbilityMislead = c.Boolean(),
                        IsAdvertising = c.Boolean(),
                        IsMNNSimilar = c.Boolean(),
                        ExpertiseNormDoc = c.String(maxLength: 2000),
                        SampleDrug = c.String(maxLength: 2000),
                        ComplianceSeries = c.String(maxLength: 2000),
                        ResidualShelfLife = c.String(maxLength: 2000),
                        SampleSubstance = c.String(maxLength: 2000),
                        SampleStandart = c.String(maxLength: 2000),
                        TestLabRecommend = c.String(maxLength: 2000),
                        MedicalInstruction = c.String(maxLength: 2000),
                        ExpertOpinion = c.String(maxLength: 2000),
                        DosageStageId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_ExpertiseStageDosage", t => t.DosageStageId)
                .Index(t => t.DosageStageId);
            
            CreateTable(
                "dbo.EXP_MaterialDirections",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        Number = c.String(maxLength: 128),
                        DrugDeclarationId = c.Guid(nullable: false),
                        StatusId = c.Guid(nullable: false),
                        SendEmployeeId = c.Guid(),
                        SendDate = c.DateTime(),
                        ExecutorEmployeeId = c.Guid(),
                        ReceiveDate = c.DateTime(),
                        RejectDate = c.DateTime(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.StatusId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .ForeignKey("dbo.Employees", t => t.ExecutorEmployeeId)
                .ForeignKey("dbo.Employees", t => t.SendEmployeeId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.StatusId)
                .Index(t => t.SendEmployeeId)
                .Index(t => t.ExecutorEmployeeId);
            
            CreateTable(
                "dbo.EXP_Materials",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RegistrationDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        Name = c.String(nullable: false),
                        TypeId = c.Guid(nullable: false),
                        DrugFormId = c.Int(),
                        Dosage = c.Decimal(precision: 18, scale: 2),
                        DosageUnitId = c.Guid(),
                        DosageQuantity = c.Int(),
                        Concentration = c.String(maxLength: 512),
                        Volume = c.Decimal(precision: 18, scale: 2),
                        VolumeUnitId = c.Guid(),
                        IsContainNPP = c.Boolean(nullable: false),
                        ProducerId = c.Guid(),
                        CountryId = c.Guid(),
                        Quantity = c.Int(nullable: false),
                        UnitId = c.Guid(nullable: false),
                        Batch = c.String(nullable: false),
                        DateOfManufacture = c.DateTime(),
                        ExpirationDate = c.DateTime(),
                        RetestDate = c.DateTime(),
                        IsCertificatePassport = c.Boolean(nullable: false),
                        StorageId = c.Guid(),
                        StorageTemperatureFrom = c.Decimal(precision: 18, scale: 2),
                        StorageTemperatureTo = c.Decimal(precision: 18, scale: 2),
                        ActiveSubstancePercent = c.Decimal(precision: 18, scale: 2),
                        WaterContentPercent = c.Decimal(precision: 18, scale: 2),
                        DrugDeclarationId = c.Guid(nullable: false),
                        IsAdditional = c.Boolean(nullable: false),
                        StorageConditionId = c.Guid(),
                        StatusId = c.Guid(),
                        ExternalStateId = c.Guid(),
                        ConcordanceStatementId = c.Guid(),
                        OpeningDate = c.DateTime(),
                        ExpirationAfterOpeningDate = c.DateTime(),
                        ConcentrationUnitId = c.Guid(),
                        IsUnLimited = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DIC_Storages", t => t.StorageId)
                .ForeignKey("dbo.sr_dosage_forms", t => t.DrugFormId)
                .ForeignKey("dbo.Dictionaries", t => t.ConcentrationUnitId)
                .ForeignKey("dbo.Dictionaries", t => t.ConcordanceStatementId)
                .ForeignKey("dbo.Dictionaries", t => t.CountryId)
                .ForeignKey("dbo.Dictionaries", t => t.DosageUnitId)
                .ForeignKey("dbo.Dictionaries", t => t.ExternalStateId)
                .ForeignKey("dbo.Dictionaries", t => t.StatusId)
                .ForeignKey("dbo.Dictionaries", t => t.StorageConditionId)
                .ForeignKey("dbo.Dictionaries", t => t.TypeId)
                .ForeignKey("dbo.Dictionaries", t => t.UnitId)
                .ForeignKey("dbo.Dictionaries", t => t.VolumeUnitId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .ForeignKey("dbo.Organizations", t => t.ProducerId)
                .Index(t => t.TypeId)
                .Index(t => t.DrugFormId)
                .Index(t => t.DosageUnitId)
                .Index(t => t.VolumeUnitId)
                .Index(t => t.ProducerId)
                .Index(t => t.CountryId)
                .Index(t => t.UnitId)
                .Index(t => t.StorageId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.StorageConditionId)
                .Index(t => t.StatusId)
                .Index(t => t.ExternalStateId)
                .Index(t => t.ConcordanceStatementId)
                .Index(t => t.ConcentrationUnitId);
            
            CreateTable(
                "dbo.DIC_Storages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(nullable: false, maxLength: 512),
                        NameKz = c.String(maxLength: 512),
                        NameEn = c.String(maxLength: 512),
                        Note = c.String(maxLength: 1024),
                        CreatedDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        ParentId = c.Guid(),
                        OrganizationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DIC_Storages", t => t.ParentId)
                .ForeignKey("dbo.Units", t => t.OrganizationId)
                .Index(t => t.ParentId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Code = c.String(maxLength: 510),
                        Name = c.String(maxLength: 4000),
                        ShortName = c.String(maxLength: 4000),
                        Path = c.String(maxLength: 510),
                        ParentId = c.Guid(),
                        EmployeeId = c.Guid(nullable: false),
                        PositionState = c.Int(nullable: false),
                        ManagerId = c.String(maxLength: 4000),
                        SecretaryId = c.String(maxLength: 4000),
                        BossId = c.String(maxLength: 4000),
                        ChancelleryId = c.String(maxLength: 4000),
                        UnitTypeDictionaryId = c.String(maxLength: 4000),
                        UnitTypeDictionaryValue = c.String(maxLength: 4000),
                        Type = c.Int(nullable: false),
                        ManagerValue = c.String(maxLength: 4000),
                        SecretaryValue = c.String(maxLength: 4000),
                        BossValue = c.String(maxLength: 4000),
                        ChancelleryValue = c.String(maxLength: 4000),
                        DisplayName = c.String(maxLength: 4000),
                        CuratorId = c.String(maxLength: 4000),
                        CuratorValue = c.String(maxLength: 4000),
                        Rank = c.Int(nullable: false),
                        Email = c.String(maxLength: 900),
                        PositionType = c.Int(nullable: false),
                        PositionStaff = c.Int(nullable: false),
                        NameKz = c.String(maxLength: 4000),
                        LegalAddress = c.String(maxLength: 200),
                        ActualAddress = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 100),
                        CountryId = c.Guid(),
                        Iin = c.String(maxLength: 100),
                        Bin = c.String(maxLength: 100),
                        ApplicationNumber = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.ParentId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.ParentId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.OBK_Contract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(nullable: false, maxLength: 30),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        EmployeeId = c.Guid(),
                        DeclarantId = c.Guid(),
                        DeclarantContactId = c.Guid(),
                        ParentId = c.Guid(),
                        ContractType = c.Guid(),
                        ExpertOrganization = c.Guid(),
                        Signer = c.Guid(),
                        SendDate = c.DateTime(),
                        ContractAdditionType = c.Guid(),
                        OBK_Contract2_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_Type", t => t.Type)
                .ForeignKey("dbo.OBK_Ref_Status", t => t.Status)
                .ForeignKey("dbo.OBK_Contract", t => t.OBK_Contract2_Id)
                .ForeignKey("dbo.OBK_DeclarantContact", t => t.DeclarantContactId)
                .ForeignKey("dbo.OBK_Declarant", t => t.DeclarantId)
                .ForeignKey("dbo.Units", t => t.ExpertOrganization)
                .ForeignKey("dbo.Dictionaries", t => t.ContractAdditionType)
                .ForeignKey("dbo.Employees", t => t.Signer)
                .Index(t => t.Type)
                .Index(t => t.Status)
                .Index(t => t.DeclarantId)
                .Index(t => t.DeclarantContactId)
                .Index(t => t.ExpertOrganization)
                .Index(t => t.Signer)
                .Index(t => t.ContractAdditionType)
                .Index(t => t.OBK_Contract2_Id);
            
            CreateTable(
                "dbo.OBK_AssessmentDeclaration",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CertificateGMP = c.String(),
                        CertificateNumber = c.String(),
                        AssuranceCheck = c.Boolean(nullable: false),
                        OrderCheck = c.Boolean(nullable: false),
                        StabilityCheck = c.Boolean(nullable: false),
                        PaymentCheck = c.Boolean(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                        ContractId = c.Guid(),
                        CertificateDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        CertificateGMPCheck = c.Boolean(),
                        StatusId = c.Int(nullable: false),
                        InvoiceRu = c.String(),
                        InvoiceKz = c.String(),
                        InvoiceDate = c.DateTime(),
                        InvoiceContractRu = c.String(),
                        InvoiceContractKz = c.String(),
                        InvoiceAgentLastName = c.String(),
                        InvoiceAgentFirstName = c.String(),
                        InvoiceAgentMiddelName = c.String(),
                        InvoiceAgentPositionName = c.String(),
                        Number = c.String(maxLength: 500),
                        SendDate = c.DateTime(),
                        ExecuterId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DesignDate = c.DateTime(),
                        FirstSendDate = c.DateTime(),
                        IsSigned = c.Boolean(nullable: false),
                        DesignNote = c.String(),
                        CertificateCountryId = c.Guid(),
                        CertificateOrganization = c.String(maxLength: 500),
                        CertificateStartDate = c.DateTime(),
                        CertificateTypeId = c.Int(),
                        InvoiceContractDate = c.DateTime(),
                        CertificateManufacturName = c.String(maxLength: 512),
                        TypeId = c.Int(nullable: false),
                        DomesticProducer = c.Boolean(),
                        KfSelection = c.Boolean(),
                        GDPItself = c.Boolean(),
                        EndDate = c.DateTime(),
                        ExpertRequest = c.Boolean(),
                        ApplicantAgreement = c.Boolean(),
                        OBKApplicantParty = c.Boolean(),
                        OBK_AssessmentDeclaration2_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_Type", t => t.TypeId)
                .ForeignKey("dbo.OBK_AssessmentDeclaration", t => t.OBK_AssessmentDeclaration2_Id)
                .ForeignKey("dbo.OBK_Ref_CertificateType", t => t.CertificateTypeId)
                .ForeignKey("dbo.OBK_Contract", t => t.ContractId)
                .Index(t => t.ContractId)
                .Index(t => t.CertificateTypeId)
                .Index(t => t.TypeId)
                .Index(t => t.OBK_AssessmentDeclaration2_Id);
            
            CreateTable(
                "dbo.OBK_ActReception",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(maxLength: 50),
                        ActDate = c.DateTime(),
                        Address = c.String(),
                        Worker = c.String(),
                        Producer = c.String(),
                        Provider = c.String(),
                        sr_measuresId = c.Long(),
                        ProductSamplesId = c.Guid(),
                        InspectionInstalledId = c.Guid(),
                        PackageConditionId = c.Guid(),
                        MarkingId = c.Guid(),
                        StorageConditionsId = c.Guid(),
                        Accept = c.Boolean(),
                        ApplicantId = c.Guid(),
                        AttachPath = c.String(maxLength: 300),
                        Declarer = c.String(maxLength: 1000),
                        OBK_AssessmentDeclarationId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_AssessmentDeclaration", t => t.OBK_AssessmentDeclarationId)
                .ForeignKey("dbo.OBK_Dictionaries", t => t.InspectionInstalledId)
                .ForeignKey("dbo.OBK_Dictionaries", t => t.MarkingId)
                .ForeignKey("dbo.OBK_Dictionaries", t => t.PackageConditionId)
                .ForeignKey("dbo.OBK_Dictionaries", t => t.StorageConditionsId)
                .ForeignKey("dbo.sr_measures", t => t.sr_measuresId)
                .ForeignKey("dbo.Dictionaries", t => t.ProductSamplesId)
                .Index(t => t.sr_measuresId)
                .Index(t => t.ProductSamplesId)
                .Index(t => t.InspectionInstalledId)
                .Index(t => t.PackageConditionId)
                .Index(t => t.MarkingId)
                .Index(t => t.StorageConditionsId)
                .Index(t => t.OBK_AssessmentDeclarationId);
            
            CreateTable(
                "dbo.OBK_Dictionaries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(maxLength: 4000),
                        Name = c.String(maxLength: 4000),
                        NameKz = c.String(maxLength: 4000),
                        CreateDate = c.DateTime(),
                        ExpireDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_Tasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TaskNumber = c.String(maxLength: 50),
                        RegisterDate = c.String(maxLength: 50),
                        ExecutorId = c.Guid(),
                        UnitId = c.Guid(nullable: false),
                        ActReceptionId = c.Guid(nullable: false),
                        AssessmentDeclarationId = c.Guid(nullable: false),
                        TaskEndDate = c.DateTime(),
                        IsSigned = c.Boolean(nullable: false),
                        SendToCoz = c.Boolean(nullable: false),
                        AcceptToCoz = c.Boolean(nullable: false),
                        SendToIC = c.DateTime(),
                        CozExecutorId = c.Guid(),
                        TaskStatusId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_StageStatus", t => t.TaskStatusId)
                .ForeignKey("dbo.OBK_ActReception", t => t.ActReceptionId)
                .ForeignKey("dbo.OBK_AssessmentDeclaration", t => t.AssessmentDeclarationId)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .ForeignKey("dbo.Employees", t => t.ExecutorId)
                .Index(t => t.ExecutorId)
                .Index(t => t.UnitId)
                .Index(t => t.ActReceptionId)
                .Index(t => t.AssessmentDeclarationId)
                .Index(t => t.TaskStatusId);
            
            CreateTable(
                "dbo.OBK_Ref_StageStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 50),
                        NameRu = c.String(nullable: false, maxLength: 2000),
                        NameKz = c.String(nullable: false, maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_AssessmentStage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeclarationId = c.Guid(nullable: false),
                        StageId = c.Int(nullable: false),
                        StageStatusId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        FactEndDate = c.DateTime(),
                        ResultId = c.Int(),
                        ParentStageId = c.Guid(),
                        IsHistory = c.Boolean(nullable: false),
                        OtdIds = c.String(maxLength: 4000),
                        OtdRemarks = c.String(),
                        IsSuspended = c.Boolean(nullable: false),
                        SuspendedStartDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_Stage", t => t.StageId)
                .ForeignKey("dbo.OBK_Ref_StageStatus", t => t.StageStatusId)
                .ForeignKey("dbo.OBK_AssessmentDeclaration", t => t.DeclarationId)
                .Index(t => t.DeclarationId)
                .Index(t => t.StageId)
                .Index(t => t.StageStatusId);
            
            CreateTable(
                "dbo.OBK_AssessmentStageExecutors",
                c => new
                    {
                        AssessmentStageId = c.Guid(nullable: false),
                        ExecutorId = c.Guid(nullable: false),
                        ExecutorType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AssessmentStageId, t.ExecutorId })
                .ForeignKey("dbo.OBK_AssessmentStage", t => t.AssessmentStageId)
                .ForeignKey("dbo.Employees", t => t.ExecutorId)
                .Index(t => t.AssessmentStageId)
                .Index(t => t.ExecutorId);
            
            CreateTable(
                "dbo.OBK_AssessmentStageSignData",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AssessmentStageId = c.Guid(nullable: false),
                        SignerId = c.Guid(nullable: false),
                        SignXmlData = c.String(nullable: false, storeType: "ntext"),
                        SignDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.AssessmentStageId, t.SignerId })
                .ForeignKey("dbo.OBK_AssessmentStage", t => t.AssessmentStageId)
                .ForeignKey("dbo.Employees", t => t.SignerId)
                .Index(t => t.AssessmentStageId)
                .Index(t => t.SignerId);
            
            CreateTable(
                "dbo.OBK_Ref_Stage",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_ContractStage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContractId = c.Guid(nullable: false),
                        StageId = c.Int(nullable: false),
                        StageStatusId = c.Int(nullable: false),
                        ParentStageId = c.Guid(),
                        ResultId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_ContractStage", t => t.ParentStageId)
                .ForeignKey("dbo.OBK_Ref_Stage", t => t.StageId)
                .ForeignKey("dbo.OBK_Ref_StageStatus", t => t.StageStatusId)
                .ForeignKey("dbo.OBK_Contract", t => t.ContractId)
                .Index(t => t.ContractId)
                .Index(t => t.StageId)
                .Index(t => t.StageStatusId)
                .Index(t => t.ParentStageId);
            
            CreateTable(
                "dbo.OBK_ContractStageExecutors",
                c => new
                    {
                        ContractStageId = c.Guid(nullable: false),
                        ExecutorId = c.Guid(nullable: false),
                        ExecutorType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContractStageId, t.ExecutorId })
                .ForeignKey("dbo.OBK_ContractStage", t => t.ContractStageId)
                .ForeignKey("dbo.Employees", t => t.ExecutorId)
                .Index(t => t.ContractStageId)
                .Index(t => t.ExecutorId);
            
            CreateTable(
                "dbo.OBK_ZBKCopyStage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OBK_ZBKCopyId = c.Guid(),
                        StageId = c.Int(nullable: false),
                        StageStatusId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        ResultId = c.Int(),
                        SendToAccountant = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_Stage", t => t.StageId)
                .ForeignKey("dbo.OBK_Ref_StageStatus", t => t.StageStatusId)
                .Index(t => t.StageId)
                .Index(t => t.StageStatusId);
            
            CreateTable(
                "dbo.OBK_ZBKCopyStageExecutors",
                c => new
                    {
                        ZBKCopyStageId = c.Guid(nullable: false),
                        ExecutorId = c.Guid(nullable: false),
                        ExecutorType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ZBKCopyStageId)
                .ForeignKey("dbo.OBK_ZBKCopyStage", t => t.ZBKCopyStageId)
                .ForeignKey("dbo.Employees", t => t.ExecutorId)
                .Index(t => t.ZBKCopyStageId)
                .Index(t => t.ExecutorId);
            
            CreateTable(
                "dbo.OBK_ZBKCopyStageSignData",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SignerId = c.Guid(nullable: false),
                        SignXmlData = c.String(nullable: false, storeType: "ntext"),
                        SignDateTime = c.DateTime(nullable: false),
                        StageId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_ZBKCopyStage", t => t.StageId)
                .ForeignKey("dbo.Employees", t => t.SignerId)
                .Index(t => t.SignerId)
                .Index(t => t.StageId);
            
            CreateTable(
                "dbo.OBK_AssessmentStageOP",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeclarationId = c.Guid(),
                        StageStatus = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_StageStatus", t => t.StageStatus)
                .ForeignKey("dbo.OBK_AssessmentDeclaration", t => t.DeclarationId)
                .Index(t => t.DeclarationId)
                .Index(t => t.StageStatus);
            
            CreateTable(
                "dbo.OBK_TaskMaterial",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TaskId = c.Guid(nullable: false),
                        LaboratoryTypeId = c.Guid(nullable: false),
                        ProductSeriesId = c.Int(nullable: false),
                        Quantity = c.Int(),
                        UnitLaboratoryId = c.Guid(),
                        IdNumber = c.String(maxLength: 50),
                        StorageConditionId = c.Guid(),
                        ExternalConditionId = c.Guid(),
                        DimensionIMN = c.String(maxLength: 50),
                        ExpertiseResult = c.Boolean(),
                        Regulation = c.String(maxLength: 255),
                        SubTaskNumber = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(),
                        StatusId = c.Int(),
                        LaboratoryAssistantId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Procunts_Series", t => t.ProductSeriesId)
                .ForeignKey("dbo.OBK_Ref_LaboratoryType", t => t.LaboratoryTypeId)
                .ForeignKey("dbo.OBK_Ref_MaterialCondition", t => t.StorageConditionId)
                .ForeignKey("dbo.OBK_Ref_MaterialCondition", t => t.ExternalConditionId)
                .ForeignKey("dbo.OBK_Ref_StageStatus", t => t.StatusId)
                .ForeignKey("dbo.OBK_Tasks", t => t.TaskId)
                .ForeignKey("dbo.Units", t => t.UnitLaboratoryId)
                .ForeignKey("dbo.Employees", t => t.LaboratoryAssistantId)
                .Index(t => t.TaskId)
                .Index(t => t.LaboratoryTypeId)
                .Index(t => t.ProductSeriesId)
                .Index(t => t.UnitLaboratoryId)
                .Index(t => t.StorageConditionId)
                .Index(t => t.ExternalConditionId)
                .Index(t => t.StatusId)
                .Index(t => t.LaboratoryAssistantId);
            
            CreateTable(
                "dbo.OBK_Procunts_Series",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Series = c.String(),
                        SeriesStartdate = c.String(),
                        SeriesEndDate = c.String(),
                        SeriesParty = c.String(),
                        OBK_RS_ProductsId = c.Int(nullable: false),
                        SeriesMeasureId = c.Long(),
                        Quantity = c.Int(),
                        Available = c.Boolean(),
                        Comment = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_RS_Products", t => t.OBK_RS_ProductsId)
                .ForeignKey("dbo.sr_measures", t => t.SeriesMeasureId)
                .Index(t => t.OBK_RS_ProductsId)
                .Index(t => t.SeriesMeasureId);
            
            CreateTable(
                "dbo.OBK_Products_SeriesCom",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductSerieId = c.Int(nullable: false),
                        IsError = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Procunts_Series", t => t.ProductSerieId)
                .Index(t => t.ProductSerieId);
            
            CreateTable(
                "dbo.OBK_Products_SeriesComRecord",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CommentId = c.Guid(nullable: false),
                        UserId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ValueField = c.String(maxLength: 500),
                        Note = c.String(maxLength: 2000),
                        DisplayField = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Products_SeriesCom", t => t.CommentId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OBK_RS_Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameRu = c.String(),
                        NameKz = c.String(),
                        ProducerNameRu = c.String(),
                        ProducerNameKz = c.String(),
                        CountryNameRu = c.String(),
                        CountryNameKZ = c.String(),
                        TnvedCode = c.String(maxLength: 20),
                        KpvedCode = c.String(maxLength: 20),
                        Price = c.String(maxLength: 20),
                        ContractId = c.Guid(),
                        RegTypeId = c.Int(nullable: false),
                        DegreeRiskId = c.Int(),
                        DrugFormBoxCount = c.String(maxLength: 255),
                        DrugFormFullName = c.String(maxLength: 4000),
                        DrugFormFullNameKz = c.String(maxLength: 4000),
                        CurrencyId = c.Guid(),
                        RegisterId = c.Int(nullable: false),
                        RegNumber = c.String(maxLength: 50),
                        RegNumberKz = c.String(maxLength: 50),
                        RegDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(),
                        NdName = c.String(maxLength: 50),
                        NdNumber = c.String(maxLength: 50),
                        ExpertisePlace = c.Int(nullable: false),
                        Dimension = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Contract", t => t.ContractId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.OBK_ContractPrice",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceRefId = c.Guid(nullable: false),
                        Count = c.Int(nullable: false),
                        PriceWithoutTax = c.Double(nullable: false),
                        PriceWithTax = c.Double(nullable: false),
                        ProductId = c.Int(),
                        ContractId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_PriceList", t => t.PriceRefId)
                .ForeignKey("dbo.OBK_RS_Products", t => t.ProductId)
                .ForeignKey("dbo.OBK_Contract", t => t.ContractId)
                .Index(t => t.PriceRefId)
                .Index(t => t.ProductId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.OBK_ContractPriceCom",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContractPriceId = c.Guid(nullable: false),
                        IsError = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_ContractPrice", t => t.ContractPriceId)
                .Index(t => t.ContractPriceId);
            
            CreateTable(
                "dbo.OBK_ContractPriceComRecord",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CommentId = c.Guid(nullable: false),
                        UserId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ValueField = c.String(maxLength: 500),
                        Note = c.String(maxLength: 2000),
                        DisplayField = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_ContractPriceCom", t => t.CommentId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OBK_Ref_PriceList",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeId = c.Int(nullable: false),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        UnitId = c.Guid(nullable: false),
                        Price = c.Double(nullable: false),
                        ServiceTypeId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DegreeRiskId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_DegreeRisk", t => t.DegreeRiskId)
                .ForeignKey("dbo.OBK_Ref_ServiceType", t => t.ServiceTypeId)
                .ForeignKey("dbo.OBK_Ref_Type", t => t.TypeId)
                .ForeignKey("dbo.Dictionaries", t => t.UnitId)
                .Index(t => t.TypeId)
                .Index(t => t.UnitId)
                .Index(t => t.ServiceTypeId)
                .Index(t => t.DegreeRiskId);
            
            CreateTable(
                "dbo.OBK_Ref_DegreeRisk",
                c => new
                    {
                        iD = c.Guid(nullable: false),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.iD);
            
            CreateTable(
                "dbo.OBK_Ref_ServiceType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_Ref_Type",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        NameRu = c.String(nullable: false),
                        NameKz = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ViewOption = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_MtPart",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Int(nullable: false),
                        PartNumber = c.Int(),
                        Model = c.String(maxLength: 500, unicode: false),
                        Specification = c.String(maxLength: 500, unicode: false),
                        SpecificationKz = c.String(maxLength: 500),
                        ProducerName = c.String(maxLength: 2000, unicode: false),
                        CountryName = c.String(maxLength: 500, unicode: false),
                        ProducerNameKz = c.String(maxLength: 2000),
                        CountryNameKz = c.String(maxLength: 500),
                        Name = c.String(nullable: false),
                        NameKz = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_RS_Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.OBK_RS_ProductsCom",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Int(nullable: false),
                        IsError = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_RS_Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.OBK_RS_ProductsComRecord",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CommentId = c.Guid(nullable: false),
                        UserId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ValueField = c.String(maxLength: 500),
                        Note = c.String(maxLength: 2000),
                        DisplayField = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_RS_ProductsCom", t => t.CommentId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OBK_StageExpDocument",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Int(),
                        ProductSeriesId = c.Int(),
                        ExpResult = c.Boolean(nullable: false),
                        ExpStartDate = c.DateTime(),
                        ExpEndDate = c.DateTime(),
                        ExpReasonNameRu = c.String(),
                        ExpReasonNameKz = c.String(),
                        ExpProductNameRu = c.String(),
                        ExpProductNameKz = c.String(),
                        ExpNomenclatureRu = c.String(),
                        ExpNomenclatureKz = c.String(),
                        ExpAddInfoRu = c.String(),
                        ExpAddInfoKz = c.String(),
                        ExpConclusionNumber = c.String(maxLength: 50),
                        ExpBlankNumber = c.String(maxLength: 50),
                        ExpApplication = c.Boolean(nullable: false),
                        ExpApplicationNumber = c.String(maxLength: 50),
                        ExpExecutorSign = c.String(),
                        ExecutorId = c.Guid(),
                        AssessmentDeclarationId = c.Guid(),
                        RefReasonId = c.Int(),
                        RefuseDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_Reason", t => t.RefReasonId)
                .ForeignKey("dbo.OBK_RS_Products", t => t.ProductId)
                .ForeignKey("dbo.OBK_Procunts_Series", t => t.ProductSeriesId)
                .ForeignKey("dbo.OBK_AssessmentDeclaration", t => t.AssessmentDeclarationId)
                .Index(t => t.ProductId)
                .Index(t => t.ProductSeriesId)
                .Index(t => t.AssessmentDeclarationId)
                .Index(t => t.RefReasonId);
            
            CreateTable(
                "dbo.OBK_Ref_Reason",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        NameRu = c.String(nullable: false),
                        NameKz = c.String(nullable: false),
                        ExpertiseResult = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_ZBKCopy",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OBK_StageExpDocumentId = c.Guid(),
                        CopyQuantity = c.Int(),
                        AttachPath = c.String(maxLength: 400),
                        ExpApplication = c.Boolean(),
                        SendDate = c.DateTime(),
                        StatusId = c.Int(),
                        Notes = c.String(storeType: "ntext"),
                        EmployeeId = c.Guid(),
                        ReceiverFIO = c.String(maxLength: 500),
                        ExtraditeDate = c.DateTime(),
                        OriginalsGiven = c.Boolean(),
                        zbkCopiesReady = c.Boolean(),
                        LetterNumber = c.String(maxLength: 100),
                        LetterDate = c.DateTime(),
                        StartNumber = c.Int(),
                        EndPrimeNumber = c.Int(),
                        StartApplicationNumber = c.Int(),
                        EndApplicationNumber = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.OBK_StageExpDocument", t => t.OBK_StageExpDocumentId)
                .Index(t => t.OBK_StageExpDocumentId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.OBK_ZBKCopyBlank",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ZBKCopyId = c.Guid(),
                        CreateDate = c.DateTime(),
                        StartNumber = c.Int(),
                        EndPrimeNumber = c.Int(),
                        StartApplicationNumber = c.Int(),
                        EndApplicationNumber = c.Int(),
                        EmployeeId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_ZBKCopy", t => t.ZBKCopyId)
                .Index(t => t.ZBKCopyId);
            
            CreateTable(
                "dbo.OBK_ZBKCopySignData",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OBK_ZBKCopyId = c.Guid(nullable: false),
                        SignerId = c.Guid(nullable: false),
                        SignXmlData = c.String(nullable: false, storeType: "ntext"),
                        SignDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_ZBKCopy", t => t.OBK_ZBKCopyId)
                .ForeignKey("dbo.Employees", t => t.SignerId)
                .Index(t => t.OBK_ZBKCopyId)
                .Index(t => t.SignerId);
            
            CreateTable(
                "dbo.sr_measures",
                c => new
                    {
                        id = c.Long(nullable: false),
                        name = c.String(maxLength: 255),
                        name_kz = c.String(maxLength: 510),
                        short_name = c.String(maxLength: 250),
                        short_name_kz = c.String(maxLength: 500),
                        block_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.EXP_DrugSubstance",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SubstanceTypeId = c.Int(),
                        SubstanceId = c.Int(),
                        SubstanceName = c.String(maxLength: 500),
                        SubstanceCount = c.String(maxLength: 500),
                        MeasureId = c.Long(),
                        ProducerName = c.String(maxLength: 500),
                        ProducerAddress = c.String(maxLength: 500),
                        CountryId = c.Long(),
                        NormativeDocument = c.String(maxLength: 500),
                        IsControl = c.Boolean(nullable: false),
                        IsPoison = c.Boolean(nullable: false),
                        PlantKindId = c.Int(),
                        Locus = c.String(maxLength: 500),
                        OriginId = c.Int(),
                        DrugDosageId = c.Long(nullable: false),
                        IsNotFound = c.Boolean(nullable: false),
                        NewName = c.String(maxLength: 500),
                        NormDocFarmId = c.Int(),
                        MathFormula = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_NormDocFarm", t => t.NormDocFarmId)
                .ForeignKey("dbo.EXP_DIC_Origin", t => t.OriginId)
                .ForeignKey("dbo.EXP_DIC_PlantKind", t => t.PlantKindId)
                .ForeignKey("dbo.sr_countries", t => t.CountryId)
                .ForeignKey("dbo.sr_substance_types", t => t.SubstanceTypeId)
                .ForeignKey("dbo.sr_substances", t => t.SubstanceId)
                .ForeignKey("dbo.sr_measures", t => t.MeasureId)
                .ForeignKey("dbo.EXP_DrugDosage", t => t.DrugDosageId)
                .Index(t => t.SubstanceTypeId)
                .Index(t => t.SubstanceId)
                .Index(t => t.MeasureId)
                .Index(t => t.CountryId)
                .Index(t => t.PlantKindId)
                .Index(t => t.OriginId)
                .Index(t => t.DrugDosageId)
                .Index(t => t.NormDocFarmId);
            
            CreateTable(
                "dbo.EXP_DIC_NormDocFarm",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DIC_Origin",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DIC_PlantKind",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DrugSubstanceManufacture",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugSubstanceId = c.Long(nullable: false),
                        ProducerName = c.String(maxLength: 500),
                        ProducerAddress = c.String(maxLength: 500),
                        CountryId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.sr_countries", t => t.CountryId)
                .ForeignKey("dbo.EXP_DrugSubstance", t => t.DrugSubstanceId)
                .Index(t => t.DrugSubstanceId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.sr_countries",
                c => new
                    {
                        id = c.Long(nullable: false),
                        name = c.String(maxLength: 255),
                        full_name = c.String(maxLength: 254),
                        name_kz = c.String(maxLength: 510),
                        full_name_kz = c.String(maxLength: 508),
                        name_en = c.String(maxLength: 100, unicode: false),
                        short_name = c.String(maxLength: 3, fixedLength: true, unicode: false),
                        cis_sign = c.Boolean(nullable: false),
                        baltic_sign = c.Boolean(nullable: false),
                        foreign_sign = c.Boolean(nullable: false),
                        europe_sign = c.Boolean(nullable: false),
                        america_sign = c.Boolean(nullable: false),
                        kz_sign = c.Boolean(nullable: false),
                        block_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.EXP_DrugExportTrade",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugDeclarationId = c.Guid(nullable: false),
                        NameRu = c.String(maxLength: 500),
                        NameKz = c.String(maxLength: 500),
                        NameEn = c.String(maxLength: 500),
                        CountryId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.sr_countries", t => t.CountryId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.EXP_DrugOtherCountry",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugDeclarationId = c.Guid(nullable: false),
                        RegNumber = c.String(maxLength: 500),
                        IssueDate = c.DateTime(storeType: "date"),
                        ExpireDate = c.DateTime(storeType: "date"),
                        CountryId = c.Long(),
                        IsUnLimited = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.sr_countries", t => t.CountryId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.sr_register_names",
                c => new
                    {
                        id = c.Long(nullable: false),
                        register_id = c.Int(nullable: false),
                        name = c.String(maxLength: 3000, unicode: false),
                        name_kz = c.String(maxLength: 4000),
                        name_eng = c.String(maxLength: 3000, unicode: false),
                        country_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_register", t => t.register_id)
                .ForeignKey("dbo.sr_countries", t => t.country_id)
                .Index(t => t.register_id)
                .Index(t => t.country_id);
            
            CreateTable(
                "dbo.sr_register",
                c => new
                    {
                        id = c.Int(nullable: false),
                        reg_type_id = c.Int(nullable: false),
                        reg_action_id = c.Int(nullable: false),
                        reg_number = c.String(maxLength: 50),
                        reg_number_kz = c.String(maxLength: 100),
                        reg_date = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        reg_term = c.Short(nullable: false),
                        expiration_date = c.DateTime(storeType: "smalldatetime"),
                        name = c.String(maxLength: 3000),
                        name_kz = c.String(maxLength: 4000),
                        _producer_name = c.String(maxLength: 500),
                        _producer_name_kz = c.String(maxLength: 1000),
                        _country_name = c.String(maxLength: 50),
                        _country_name_kz = c.String(maxLength: 100),
                        gmp_sign = c.Boolean(nullable: false),
                        trademark_sign = c.Boolean(nullable: false),
                        patent_sign = c.Boolean(nullable: false),
                        storage_term = c.Decimal(precision: 6, scale: 3),
                        storage_measure_id = c.Long(),
                        comment = c.String(maxLength: 500, unicode: false),
                        comment_kz = c.String(maxLength: 1000),
                        unlimited_sign = c.Boolean(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_reg_actions", t => t.reg_action_id)
                .ForeignKey("dbo.sr_reg_types", t => t.reg_type_id)
                .ForeignKey("dbo.sr_measures", t => t.storage_measure_id)
                .Index(t => t.reg_type_id)
                .Index(t => t.reg_action_id)
                .Index(t => t.storage_measure_id);
            
            CreateTable(
                "dbo.sr_drug_forms",
                c => new
                    {
                        id = c.Int(nullable: false),
                        register_id = c.Int(nullable: false),
                        pr_box_id = c.Int(),
                        sec_box_id = c.Int(),
                        box_count = c.String(maxLength: 100),
                        full_name = c.String(maxLength: 4000),
                        specification = c.String(maxLength: 1000),
                        full_name_kz = c.String(maxLength: 4000),
                        complete_sign = c.Boolean(),
                        user_id = c.Int(),
                        pr_box_count = c.String(maxLength: 50, unicode: false),
                        sec_box_count = c.String(maxLength: 50, unicode: false),
                        inter_box_id = c.Int(),
                        inter_box_count = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_register_boxes", t => t.pr_box_id)
                .ForeignKey("dbo.sr_register_boxes", t => t.sec_box_id)
                .ForeignKey("dbo.sr_register", t => t.register_id)
                .Index(t => t.register_id)
                .Index(t => t.pr_box_id)
                .Index(t => t.sec_box_id);
            
            CreateTable(
                "dbo.sr_register_boxes",
                c => new
                    {
                        id = c.Int(nullable: false),
                        register_id = c.Int(nullable: false),
                        box_id = c.Int(nullable: false),
                        inner_sign = c.Boolean(),
                        volume = c.Decimal(precision: 9, scale: 3),
                        volume_measure_id = c.Long(),
                        unit_count = c.Int(nullable: false),
                        box_size = c.String(maxLength: 50, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_boxes", t => t.box_id)
                .ForeignKey("dbo.sr_register", t => t.register_id)
                .Index(t => t.register_id)
                .Index(t => t.box_id);
            
            CreateTable(
                "dbo.sr_boxes",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 250),
                        name_kz = c.String(maxLength: 500),
                        parent_id = c.Int(),
                        block_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_boxes", t => t.parent_id)
                .Index(t => t.parent_id);
            
            CreateTable(
                "dbo.EXP_DrugWrapping",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WrappingTypeId = c.Int(),
                        WrappingKindId = c.Int(),
                        WrappingSize = c.Double(),
                        SizeMeasureId = c.Long(),
                        WrappingVolume = c.Double(),
                        VolumeMeasureId = c.Long(),
                        CountUnit = c.Int(),
                        Note = c.String(maxLength: 500),
                        DrugDosageId = c.Long(nullable: false),
                        WrappingSizeStr = c.String(maxLength: 500),
                        WrappingVolumeStr = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_WrappingType", t => t.WrappingTypeId)
                .ForeignKey("dbo.sr_boxes", t => t.WrappingKindId)
                .ForeignKey("dbo.sr_measures", t => t.SizeMeasureId)
                .ForeignKey("dbo.sr_measures", t => t.VolumeMeasureId)
                .ForeignKey("dbo.EXP_DrugDosage", t => t.DrugDosageId)
                .Index(t => t.WrappingTypeId)
                .Index(t => t.WrappingKindId)
                .Index(t => t.SizeMeasureId)
                .Index(t => t.VolumeMeasureId)
                .Index(t => t.DrugDosageId);
            
            CreateTable(
                "dbo.EXP_DIC_WrappingType",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sr_register_boxes_rk_ls",
                c => new
                    {
                        id = c.Int(nullable: false),
                        state_sign = c.Boolean(),
                        state_date = c.DateTime(storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_register_boxes", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.sr_reg_actions",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 50),
                        name_kz = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sr_reg_types",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 50),
                        name_kz = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sr_register_instructions",
                c => new
                    {
                        id = c.Int(nullable: false),
                        register_id = c.Int(nullable: false),
                        instruction_type_id = c.Int(nullable: false),
                        comment = c.String(maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_instruction_types", t => t.instruction_type_id)
                .ForeignKey("dbo.sr_register", t => t.register_id)
                .Index(t => t.register_id)
                .Index(t => t.instruction_type_id);
            
            CreateTable(
                "dbo.sr_instruction_types",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 50),
                        name_kz = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sr_register_instructions_files",
                c => new
                    {
                        id = c.Int(nullable: false),
                        file_kaz = c.Binary(storeType: "image"),
                        file_rus = c.Binary(storeType: "image"),
                        file_ext_kz = c.String(maxLength: 10),
                        file_ext = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_register_instructions", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.sr_register_mt",
                c => new
                    {
                        id = c.Int(nullable: false),
                        description = c.String(maxLength: 4000),
                        purpose = c.String(maxLength: 2500),
                        use_area = c.String(maxLength: 2500),
                        mt_category_id = c.Int(nullable: false),
                        degree_risk_id = c.Int(nullable: false),
                        risk_detail_id = c.Int(nullable: false),
                        mt_sign = c.Boolean(nullable: false),
                        sterility_sign = c.Boolean(nullable: false),
                        measurement_sign = c.Boolean(nullable: false),
                        balk_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_degree_risks", t => t.degree_risk_id)
                .ForeignKey("dbo.sr_degree_risk_details", t => t.risk_detail_id)
                .ForeignKey("dbo.sr_mt_categories", t => t.mt_category_id)
                .ForeignKey("dbo.sr_register", t => t.id)
                .Index(t => t.id)
                .Index(t => t.mt_category_id)
                .Index(t => t.degree_risk_id)
                .Index(t => t.risk_detail_id);
            
            CreateTable(
                "dbo.sr_degree_risk_details",
                c => new
                    {
                        id = c.Int(nullable: false),
                        degree_risk_id = c.Int(nullable: false),
                        name = c.String(maxLength: 1500),
                        name_kz = c.String(maxLength: 3000),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_degree_risks", t => t.degree_risk_id)
                .Index(t => t.degree_risk_id);
            
            CreateTable(
                "dbo.sr_degree_risks",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 50),
                        name_kz = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sr_mt_categories",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 500),
                        name_kz = c.String(maxLength: 500),
                        parent_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_mt_categories", t => t.parent_id)
                .Index(t => t.parent_id);
            
            CreateTable(
                "dbo.sr_register_mt_kz",
                c => new
                    {
                        id = c.Int(nullable: false),
                        purpose_kz = c.String(maxLength: 4000),
                        use_area_kz = c.String(maxLength: 4000),
                        description_kz = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_register_mt", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.sr_register_mt_parts",
                c => new
                    {
                        id = c.Int(nullable: false),
                        register_id = c.Int(nullable: false),
                        name = c.String(nullable: false),
                        name_kz = c.String(nullable: false),
                        part_number = c.Int(),
                        model = c.String(maxLength: 500),
                        specification = c.String(maxLength: 500),
                        specification_kz = c.String(maxLength: 500),
                        producer_id = c.Int(),
                        country_id = c.Int(),
                        producer_name = c.String(maxLength: 2000),
                        country_name = c.String(maxLength: 500),
                        producer_name_kz = c.String(maxLength: 2000),
                        country_name_kz = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_producers", t => t.producer_id)
                .ForeignKey("dbo.sr_register_mt", t => t.register_id, cascadeDelete: true)
                .Index(t => t.register_id)
                .Index(t => t.producer_id);
            
            CreateTable(
                "dbo.sr_producers",
                c => new
                    {
                        id = c.Int(nullable: false),
                        form_type_id = c.Long(),
                        name = c.String(maxLength: 1000),
                        name_kz = c.String(maxLength: 1000),
                        name_eng = c.String(maxLength: 500, unicode: false),
                        rnn = c.String(maxLength: 12, unicode: false),
                        bin = c.String(maxLength: 250, unicode: false),
                        iin = c.String(maxLength: 250, unicode: false),
                        type_id = c.Int(nullable: false),
                        block_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_form_types", t => t.form_type_id)
                .Index(t => t.form_type_id);
            
            CreateTable(
                "dbo.sr_form_types",
                c => new
                    {
                        id = c.Long(nullable: false),
                        name = c.String(maxLength: 255),
                        name_kz = c.String(maxLength: 510),
                        full_name = c.String(maxLength: 255),
                        full_name_kz = c.String(maxLength: 510),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sr_register_producers",
                c => new
                    {
                        id = c.Int(nullable: false),
                        register_id = c.Int(nullable: false),
                        producer_id = c.Int(nullable: false),
                        producer_type_id = c.Int(nullable: false),
                        country_id = c.Long(nullable: false),
                        language_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_producer_types", t => t.producer_type_id)
                .ForeignKey("dbo.sr_producers", t => t.producer_id)
                .ForeignKey("dbo.sr_register", t => t.register_id)
                .ForeignKey("dbo.sr_countries", t => t.country_id)
                .Index(t => t.register_id)
                .Index(t => t.producer_id)
                .Index(t => t.producer_type_id)
                .Index(t => t.country_id);
            
            CreateTable(
                "dbo.sr_producer_types",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 50),
                        name_kz = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sr_register_substances",
                c => new
                    {
                        id = c.Int(nullable: false),
                        register_id = c.Int(nullable: false),
                        substance_type_id = c.Int(nullable: false),
                        substance_id = c.Int(nullable: false),
                        substance_count = c.Decimal(precision: 18, scale: 8),
                        measure_id = c.Long(),
                        producer_id = c.Int(),
                        country_id = c.Long(),
                        nd_type_id = c.Int(),
                        comment = c.String(maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_nd_types", t => t.nd_type_id)
                .ForeignKey("dbo.sr_substance_types", t => t.substance_type_id)
                .ForeignKey("dbo.sr_substances", t => t.substance_id)
                .ForeignKey("dbo.sr_register", t => t.register_id)
                .Index(t => t.register_id)
                .Index(t => t.substance_type_id)
                .Index(t => t.substance_id)
                .Index(t => t.nd_type_id);
            
            CreateTable(
                "dbo.sr_nd_types",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 250),
                        name_kz = c.String(nullable: false, maxLength: 250),
                        short_name = c.String(maxLength: 50),
                        short_name_kz = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sr_substance_types",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 50),
                        name_kz = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sr_substances",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 250),
                        name_kz = c.String(maxLength: 500),
                        name_eng = c.String(maxLength: 250),
                        animal_sign = c.Boolean(nullable: false),
                        category_id = c.Int(),
                        category_pos = c.String(maxLength: 50),
                        block_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_categories", t => t.category_id)
                .Index(t => t.category_id);
            
            CreateTable(
                "dbo.sr_categories",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 50),
                        name_kz = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sr_register_drugs",
                c => new
                    {
                        id = c.Int(nullable: false),
                        drug_type_id = c.Int(nullable: false),
                        int_name_id = c.Int(nullable: false),
                        _int_name = c.String(maxLength: 255),
                        atc_id = c.Int(nullable: false),
                        _atc_code = c.String(maxLength: 50, unicode: false),
                        _atc_name = c.String(maxLength: 255),
                        dosage_form_id = c.Int(nullable: false),
                        dosage_comment = c.String(maxLength: 500),
                        dosage_comment_kz = c.String(maxLength: 1000),
                        _dosage_form_name = c.String(maxLength: 500),
                        dosage_value = c.Decimal(precision: 20, scale: 6),
                        dosage_measure_id = c.Long(),
                        _dosage = c.String(maxLength: 250),
                        concentration = c.String(maxLength: 500, unicode: false),
                        concentration_kz = c.String(maxLength: 1000),
                        _unit_count = c.String(maxLength: 50, unicode: false),
                        recipe_sign = c.Boolean(nullable: false),
                        generic_sign = c.Boolean(nullable: false),
                        life_type_id = c.Int(nullable: false),
                        category_id = c.Int(),
                        category_pos = c.String(maxLength: 250),
                        nd_name_id = c.Int(),
                        nd_name = c.String(maxLength: 50),
                        nd_number = c.String(maxLength: 50),
                        nd_comment = c.String(maxLength: 250),
                        substance = c.String(maxLength: 1000),
                        biosimilar_sign = c.Boolean(nullable: false),
                        auto_generic_sign = c.Boolean(nullable: false),
                        orphan_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_atc_codes", t => t.atc_id)
                .ForeignKey("dbo.sr_dosage_forms", t => t.dosage_form_id)
                .ForeignKey("dbo.sr_drug_types", t => t.drug_type_id)
                .ForeignKey("dbo.sr_international_names", t => t.int_name_id)
                .ForeignKey("dbo.sr_life_types", t => t.life_type_id)
                .ForeignKey("dbo.sr_nd_names", t => t.nd_name_id)
                .ForeignKey("dbo.sr_categories", t => t.category_id)
                .ForeignKey("dbo.sr_measures", t => t.dosage_measure_id)
                .Index(t => t.drug_type_id)
                .Index(t => t.int_name_id)
                .Index(t => t.atc_id)
                .Index(t => t.dosage_form_id)
                .Index(t => t.dosage_measure_id)
                .Index(t => t.life_type_id)
                .Index(t => t.category_id)
                .Index(t => t.nd_name_id);
            
            CreateTable(
                "dbo.sr_atc_codes",
                c => new
                    {
                        id = c.Int(nullable: false),
                        code = c.String(maxLength: 10, unicode: false),
                        name = c.String(maxLength: 500),
                        name_kz = c.String(maxLength: 1000),
                        parent_id = c.Int(),
                        block_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_atc_codes", t => t.parent_id)
                .Index(t => t.parent_id);
            
            CreateTable(
                "dbo.sr_dosage_forms",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 500),
                        name_kz = c.String(maxLength: 1000),
                        short_name = c.String(maxLength: 500),
                        short_name_kz = c.String(maxLength: 1000),
                        parent_id = c.Int(),
                        concentration = c.Boolean(),
                        volume = c.Boolean(),
                        block_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_dosage_forms", t => t.parent_id)
                .Index(t => t.parent_id);
            
            CreateTable(
                "dbo.sr_drug_types",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 100),
                        name_kz = c.String(maxLength: 200),
                        parent_id = c.Int(),
                        block_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sr_international_names",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name_rus = c.String(maxLength: 255),
                        name_eng = c.String(maxLength: 255, unicode: false),
                        name_lat = c.String(maxLength: 255, unicode: false),
                        name_kz = c.String(maxLength: 510),
                        block_sign = c.Boolean(nullable: false),
                        new_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sr_life_types",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sr_nd_names",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(maxLength: 50),
                        name_kz = c.String(maxLength: 100),
                        nd_number = c.Int(nullable: false),
                        nd_term = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.sr_register_pharmacological_actions",
                c => new
                    {
                        id = c.Int(nullable: false),
                        register_id = c.Int(nullable: false),
                        pharmacological_action_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_pharmacological_actions", t => t.pharmacological_action_id, cascadeDelete: true)
                .ForeignKey("dbo.sr_register_drugs", t => t.register_id)
                .Index(t => t.register_id)
                .Index(t => t.pharmacological_action_id);
            
            CreateTable(
                "dbo.sr_pharmacological_actions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 250),
                        name_kz = c.String(nullable: false, maxLength: 250),
                        parent_id = c.Int(),
                        block_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_pharmacological_actions", t => t.parent_id)
                .Index(t => t.parent_id);
            
            CreateTable(
                "dbo.sr_register_use_methods",
                c => new
                    {
                        id = c.Int(nullable: false),
                        register_id = c.Int(nullable: false),
                        use_method_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.sr_use_methods", t => t.use_method_id)
                .ForeignKey("dbo.sr_register_drugs", t => t.register_id)
                .Index(t => t.register_id)
                .Index(t => t.use_method_id);
            
            CreateTable(
                "dbo.sr_use_methods",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 255),
                        name_kz = c.String(nullable: false, maxLength: 255),
                        block_sign = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.EXP_DrugUseMethod",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugDeclarationId = c.Guid(nullable: false),
                        UseMethodsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.sr_use_methods", t => t.UseMethodsId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.UseMethodsId);
            
            CreateTable(
                "dbo.OBK_Ref_LaboratoryType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_Ref_MaterialCondition",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(nullable: false, maxLength: 20),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_ResearchCenterResult",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TaskMaterialId = c.Guid(nullable: false),
                        LaboratoryMarkId = c.Guid(nullable: false),
                        Claim = c.String(nullable: false, maxLength: 255),
                        Humidity = c.String(nullable: false, maxLength: 255),
                        FactResult = c.String(nullable: false, maxLength: 255),
                        LaboratoryRegulationId = c.Guid(),
                        ExecutorId = c.Guid(),
                        ExpertiseResult = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_LaboratoryRegulation", t => t.LaboratoryRegulationId)
                .ForeignKey("dbo.OBK_Ref_LaboratoryMark", t => t.LaboratoryMarkId)
                .ForeignKey("dbo.OBK_TaskMaterial", t => t.TaskMaterialId)
                .ForeignKey("dbo.Employees", t => t.ExecutorId)
                .Index(t => t.TaskMaterialId)
                .Index(t => t.LaboratoryMarkId)
                .Index(t => t.LaboratoryRegulationId)
                .Index(t => t.ExecutorId);
            
            CreateTable(
                "dbo.OBK_Ref_LaboratoryMark",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_Ref_LaboratoryRegulation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LaboratoryMarkId = c.Guid(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_LaboratoryMark", t => t.LaboratoryMarkId)
                .Index(t => t.LaboratoryMarkId);
            
            CreateTable(
                "dbo.OBK_TaskExecutor",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TaskId = c.Guid(nullable: false),
                        ExecutorId = c.Guid(nullable: false),
                        StageId = c.Int(nullable: false),
                        ExecutorType = c.Int(),
                        SignedData = c.String(maxLength: 1024),
                        TaskMaterialId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_TaskMaterial", t => t.TaskMaterialId)
                .ForeignKey("dbo.OBK_Tasks", t => t.TaskId)
                .ForeignKey("dbo.Employees", t => t.ExecutorId)
                .Index(t => t.TaskId)
                .Index(t => t.ExecutorId)
                .Index(t => t.TaskMaterialId);
            
            CreateTable(
                "dbo.OBK_TaskStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TaskId = c.Guid(nullable: false),
                        StatusId = c.Int(nullable: false),
                        UnitLaboratoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_StageStatus", t => t.StatusId)
                .ForeignKey("dbo.OBK_Tasks", t => t.TaskId)
                .ForeignKey("dbo.Units", t => t.UnitLaboratoryId)
                .Index(t => t.TaskId)
                .Index(t => t.StatusId)
                .Index(t => t.UnitLaboratoryId);
            
            CreateTable(
                "dbo.OBK_AssessmentDeclaration__OBK_ExpertCouncil",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeclarationId = c.Guid(),
                        ExpertCouncilId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_ExpertCouncil", t => t.ExpertCouncilId)
                .ForeignKey("dbo.OBK_AssessmentDeclaration", t => t.DeclarationId)
                .Index(t => t.DeclarationId)
                .Index(t => t.ExpertCouncilId);
            
            CreateTable(
                "dbo.OBK_ExpertCouncil",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        ActualDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_AssessmentDeclarationCom",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AssessmentDeclarationId = c.Guid(nullable: false),
                        IsError = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        ControlId = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_AssessmentDeclaration", t => t.AssessmentDeclarationId)
                .Index(t => t.AssessmentDeclarationId);
            
            CreateTable(
                "dbo.OBK_AssessmentDeclarationComRecord",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CommentId = c.Long(nullable: false),
                        UserId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ValueField = c.String(maxLength: 500),
                        Note = c.String(maxLength: 2000),
                        DisplayField = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_AssessmentDeclarationCom", t => t.CommentId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OBK_AssessmentDeclarationFieldHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ControlId = c.String(),
                        UserId = c.Guid(),
                        ValueField = c.String(),
                        DisplayField = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        AssessmentDeclarationId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_AssessmentDeclaration", t => t.AssessmentDeclarationId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.AssessmentDeclarationId);
            
            CreateTable(
                "dbo.OBK_AssessmentDeclarationHistory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Guid(),
                        DateCreate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Note = c.String(),
                        AssessmentDeclarationId = c.Guid(nullable: false),
                        XmlSign = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_Status", t => t.StatusId)
                .ForeignKey("dbo.OBK_AssessmentDeclaration", t => t.AssessmentDeclarationId)
                .Index(t => t.StatusId)
                .Index(t => t.AssessmentDeclarationId);
            
            CreateTable(
                "dbo.OBK_Ref_Status",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(),
                        IsDeleted = c.Boolean(),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_CertificateOfCompletion",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(maxLength: 500),
                        ContractId = c.Guid(),
                        AssessmentDeclarationId = c.Guid(),
                        InvoiceNumber1C = c.String(maxLength: 512),
                        InvoiceDatetime1C = c.DateTime(),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        CreateDate = c.DateTime(),
                        SendDate = c.DateTime(),
                        ActNumber1C = c.String(maxLength: 500),
                        ActDate1C = c.DateTime(),
                        ActReturnedBack = c.Boolean(),
                        SendNotification = c.Boolean(),
                        ZBKCopyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_AssessmentDeclaration", t => t.AssessmentDeclarationId)
                .ForeignKey("dbo.OBK_Contract", t => t.ContractId)
                .Index(t => t.ContractId)
                .Index(t => t.AssessmentDeclarationId);
            
            CreateTable(
                "dbo.OBK_OP_Commission",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateOfCreation = c.DateTime(nullable: false, storeType: "date"),
                        DeclarationId = c.Guid(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_OP_CommissionRoles", t => t.RoleId)
                .ForeignKey("dbo.OBK_AssessmentDeclaration", t => t.DeclarationId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.DeclarationId)
                .Index(t => t.EmployeeId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.OBK_OP_CommissionRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameRu = c.String(nullable: false, maxLength: 50),
                        NameKk = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_Ref_CertificateType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(nullable: false, maxLength: 2000),
                        NameKz = c.String(nullable: false, maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_StageExpDocumentResult",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ExpResult = c.Boolean(nullable: false),
                        AssessmetDeclarationId = c.Guid(nullable: false),
                        SelectionDate = c.DateTime(),
                        SelectionTime = c.DateTime(),
                        SelectionPlace = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_AssessmentDeclaration", t => t.AssessmetDeclarationId)
                .Index(t => t.AssessmetDeclarationId);
            
            CreateTable(
                "dbo.OBK_ContractCom",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContractId = c.Guid(nullable: false),
                        IsError = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        ControlId = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Contract", t => t.ContractId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.OBK_ContractComRecord",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CommentId = c.Guid(nullable: false),
                        UserId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ValueField = c.String(maxLength: 500),
                        Note = c.String(maxLength: 2000),
                        DisplayField = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_ContractCom", t => t.CommentId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OBK_ContractExtHistory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        StatusId = c.Guid(nullable: false),
                        ContractId = c.Guid(nullable: false),
                        EmployeeId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.OBK_Ref_ContractExtHistoryStatus", t => t.StatusId)
                .ForeignKey("dbo.OBK_Contract", t => t.ContractId)
                .Index(t => t.StatusId)
                .Index(t => t.ContractId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.OBK_Ref_ContractExtHistoryStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DescriptionRu = c.String(maxLength: 2000),
                        DescriptionKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_ContractFactory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContractId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        LegalLocation = c.String(nullable: false, maxLength: 255),
                        ActualLocation = c.String(nullable: false, maxLength: 255),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Contract", t => t.ContractId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.OBK_ContractFactoryCom",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContractFactoryId = c.Guid(nullable: false),
                        IsError = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_ContractFactory", t => t.ContractFactoryId)
                .Index(t => t.ContractFactoryId);
            
            CreateTable(
                "dbo.OBK_ContractFactoryComRecord",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CommentId = c.Guid(nullable: false),
                        UserId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ValueField = c.String(maxLength: 500),
                        Note = c.String(maxLength: 2000),
                        DisplayField = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_ContractFactoryCom", t => t.CommentId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OBK_ContractSignedDatas",
                c => new
                    {
                        ContractId = c.Guid(nullable: false),
                        ApplicantSign = c.String(storeType: "ntext"),
                        ApplicantSignDate = c.DateTime(),
                        CeoSign = c.String(storeType: "ntext"),
                        CeoSignDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ContractId)
                .ForeignKey("dbo.OBK_Contract", t => t.ContractId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.OBK_Declarant",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameKz = c.String(maxLength: 255),
                        NameRu = c.String(maxLength: 255),
                        NameEn = c.String(maxLength: 255),
                        CountryId = c.Guid(),
                        Iin = c.String(maxLength: 255),
                        Bin = c.String(maxLength: 255),
                        OrganizationFormId = c.Guid(),
                        IsConfirmed = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsResident = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_Contract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(nullable: false, maxLength: 30),
                        HolderType = c.Guid(),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        MedicalDeviceName = c.String(maxLength: 255),
                        EmployeeId = c.Guid(),
                        DeclarantId = c.Guid(),
                        ManufacturId = c.Guid(),
                        PayerId = c.Guid(),
                        DeclarantContactId = c.Guid(),
                        ManufacturContactId = c.Guid(),
                        PayerContactId = c.Guid(),
                        ParentId = c.Guid(),
                        ExpertOrganization = c.Guid(),
                        Signer = c.Guid(),
                        SendDate = c.DateTime(),
                        DeclarantIsManufactur = c.Boolean(),
                        ChoosePayer = c.String(maxLength: 20),
                        ContractType = c.Guid(),
                        ContractStatusId = c.Guid(),
                        ContractScopeId = c.Guid(),
                        MedicalDeviceNameKz = c.String(maxLength: 255),
                        HasProxy = c.Boolean(),
                        DocumentType = c.Int(),
                        StatemantNumber = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EMP_Ref_ContractScope", t => t.ContractScopeId)
                .ForeignKey("dbo.EMP_Ref_ContractType", t => t.ContractType)
                .ForeignKey("dbo.EMP_Ref_Status", t => t.ContractStatusId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.OBK_DeclarantContact", t => t.DeclarantContactId)
                .ForeignKey("dbo.OBK_DeclarantContact", t => t.ManufacturContactId)
                .ForeignKey("dbo.OBK_DeclarantContact", t => t.PayerContactId)
                .ForeignKey("dbo.OBK_Declarant", t => t.DeclarantId)
                .ForeignKey("dbo.OBK_Declarant", t => t.ManufacturId)
                .ForeignKey("dbo.OBK_Declarant", t => t.PayerId)
                .Index(t => t.EmployeeId)
                .Index(t => t.DeclarantId)
                .Index(t => t.ManufacturId)
                .Index(t => t.PayerId)
                .Index(t => t.DeclarantContactId)
                .Index(t => t.ManufacturContactId)
                .Index(t => t.PayerContactId)
                .Index(t => t.ContractType)
                .Index(t => t.ContractStatusId)
                .Index(t => t.ContractScopeId);
            
            CreateTable(
                "dbo.EMP_ContractExtHistory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        StatusId = c.Guid(nullable: false),
                        ContractId = c.Guid(nullable: false),
                        EmployeeId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.EMP_Contract", t => t.ContractId)
                .Index(t => t.ContractId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EMP_ContractHistory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                        UnitName = c.String(maxLength: 4000),
                        StatusId = c.Guid(nullable: false),
                        RefuseReason = c.String(maxLength: 4000),
                        ContractId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_ContractHistoryStatus", t => t.StatusId)
                .ForeignKey("dbo.EMP_Contract", t => t.ContractId)
                .Index(t => t.StatusId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.OBK_Ref_ContractHistoryStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_ContractHistory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                        UnitName = c.String(maxLength: 4000),
                        StatusId = c.Guid(nullable: false),
                        RefuseReason = c.String(maxLength: 4000),
                        ContractId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_ContractHistoryStatus", t => t.StatusId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.EMP_ContractSignData",
                c => new
                    {
                        ContractId = c.Guid(nullable: false),
                        ApplicationSign = c.String(nullable: false, storeType: "ntext"),
                        ApplicationSignDate = c.DateTime(nullable: false),
                        CeoSign = c.String(storeType: "ntext"),
                        CeoSignDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ContractId)
                .ForeignKey("dbo.EMP_Contract", t => t.ContractId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.EMP_ContractStage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContractId = c.Guid(nullable: false),
                        StageId = c.Guid(nullable: false),
                        StageStatusId = c.Guid(nullable: false),
                        ParentStageId = c.Guid(),
                        Result = c.Int(),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EMP_ContractStage", t => t.ParentStageId)
                .ForeignKey("dbo.EMP_Ref_Stage", t => t.StageId)
                .ForeignKey("dbo.EMP_Ref_StageStatus", t => t.StageStatusId)
                .ForeignKey("dbo.EMP_Contract", t => t.ContractId)
                .Index(t => t.ContractId)
                .Index(t => t.StageId)
                .Index(t => t.StageStatusId)
                .Index(t => t.ParentStageId);
            
            CreateTable(
                "dbo.EMP_ContractStageExecutors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContractStageId = c.Guid(nullable: false),
                        ExecutorId = c.Guid(nullable: false),
                        ExecutorType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EMP_ContractStage", t => t.ContractStageId)
                .ForeignKey("dbo.Employees", t => t.ExecutorId)
                .Index(t => t.ContractStageId)
                .Index(t => t.ExecutorId);
            
            CreateTable(
                "dbo.EMP_Ref_Stage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(nullable: false, maxLength: 50),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_Ref_StageStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(nullable: false, maxLength: 50),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_CostWorks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceListId = c.Guid(nullable: false),
                        Count = c.Int(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        ContractId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EMP_Ref_PriceList", t => t.PriceListId)
                .ForeignKey("dbo.EMP_Contract", t => t.ContractId)
                .Index(t => t.PriceListId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.EMP_Ref_PriceList",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ServiceTypeId = c.Guid(nullable: false),
                        PriceTypeId = c.Guid(nullable: false),
                        Import = c.Boolean(),
                        Price = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EMP_Ref_PriceType", t => t.PriceTypeId)
                .ForeignKey("dbo.EMP_Ref_ServiceType", t => t.ServiceTypeId)
                .Index(t => t.ServiceTypeId)
                .Index(t => t.PriceTypeId);
            
            CreateTable(
                "dbo.EMP_Ref_PriceType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_Ref_ServiceType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        ParentId = c.Guid(),
                        ChangeType = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DegreeRiskId = c.Guid(),
                        Code = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EMP_Ref_DegreeRisk", t => t.DegreeRiskId)
                .ForeignKey("dbo.EMP_Ref_ServiceType", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.DegreeRiskId);
            
            CreateTable(
                "dbo.EMP_Ref_DegreeRisk",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        Code = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_DirectionToPayments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(nullable: false, maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                        DirectionDate = c.DateTime(nullable: false),
                        ContractId = c.Guid(nullable: false),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        PayerId = c.Guid(),
                        PayerValue = c.String(maxLength: 4000),
                        CreateEmployeeId = c.Guid(nullable: false),
                        CreateEmployeeValues = c.String(nullable: false, maxLength: 1024),
                        DeleteDate = c.DateTime(),
                        IsDeleted = c.Boolean(),
                        StatusId = c.Guid(nullable: false),
                        InvoiceNumber = c.String(maxLength: 512),
                        InvoiceNumber1C = c.String(maxLength: 512),
                        InvoiceDate1C = c.DateTime(),
                        IsPaid = c.Boolean(nullable: false),
                        IsNotFullPaid = c.Boolean(nullable: false),
                        PaymentDate = c.DateTime(),
                        PaymentValue = c.Decimal(precision: 18, scale: 2),
                        PaymentBill = c.Decimal(precision: 18, scale: 2),
                        SendNotification = c.String(maxLength: 20),
                        ModifyDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Ref_PaymentStatus", t => t.StatusId)
                .ForeignKey("dbo.EMP_Contract", t => t.ContractId)
                .ForeignKey("dbo.OBK_Declarant", t => t.PayerId)
                .ForeignKey("dbo.Employees", t => t.CreateEmployeeId)
                .Index(t => t.ContractId)
                .Index(t => t.PayerId)
                .Index(t => t.CreateEmployeeId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.OBK_Ref_PaymentStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        NameRu = c.String(nullable: false),
                        NameKz = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_Ref_ContractScope",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(nullable: false, maxLength: 50),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        CreateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_Ref_ContractType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        IsDeleted = c.Boolean(nullable: false),
                        Code = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_Ref_Status",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(nullable: false, maxLength: 50),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_DeclarantContact",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeclarantId = c.Guid(),
                        AddressLegalRu = c.String(maxLength: 255),
                        AddressLegalKz = c.String(maxLength: 255),
                        AddressFact = c.String(maxLength: 255),
                        Phone = c.String(maxLength: 255),
                        Email = c.String(maxLength: 255),
                        BankNameRu = c.String(maxLength: 255),
                        BankNameKz = c.String(maxLength: 255),
                        BankIik = c.String(maxLength: 255),
                        BankBik = c.String(maxLength: 255),
                        CurrencyId = c.Guid(),
                        BossFio = c.String(maxLength: 255),
                        BossPosition = c.String(maxLength: 255),
                        BossLastName = c.String(maxLength: 255),
                        BossFirstName = c.String(maxLength: 255),
                        BossMiddleName = c.String(maxLength: 255),
                        BossDocNumber = c.String(maxLength: 255),
                        BossDocType = c.Guid(),
                        IsHasBossDocNumber = c.Boolean(nullable: false),
                        BossDocCreatedDate = c.DateTime(),
                        SignType = c.Boolean(nullable: false),
                        SignLastName = c.String(maxLength: 255),
                        SignFirstName = c.String(maxLength: 255),
                        SignMiddleName = c.String(maxLength: 255),
                        SignPosition = c.String(maxLength: 255),
                        SignDocType = c.Guid(),
                        IsHasSignDocNumber = c.Boolean(nullable: false),
                        SignDocNumber = c.String(maxLength: 255),
                        SignDocCreatedDate = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                        SignDocEndDate = c.DateTime(),
                        SignDocUnlimited = c.Boolean(nullable: false),
                        BossDocEndDate = c.DateTime(),
                        BossDocUnlimited = c.Boolean(nullable: false),
                        SignerIsBoss = c.Boolean(nullable: false),
                        SignPositionKz = c.String(maxLength: 255),
                        BossPositionKz = c.String(maxLength: 255),
                        BankAccount = c.String(maxLength: 50),
                        BankId = c.Int(),
                        Phone2 = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EMP_Ref_Bank", t => t.BankId)
                .ForeignKey("dbo.OBK_Declarant", t => t.DeclarantId)
                .Index(t => t.DeclarantId)
                .Index(t => t.BankId);
            
            CreateTable(
                "dbo.EMP_Ref_Bank",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(nullable: false),
                        NameKz = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_DirectionToPayments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(nullable: false, maxLength: 512),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(),
                        DirectionDate = c.DateTime(nullable: false),
                        ContractId = c.Guid(nullable: false),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        PayerId = c.Guid(),
                        PayerValue = c.String(maxLength: 4000),
                        CreateEmployeeId = c.Guid(nullable: false),
                        CreateEmployeeValue = c.String(nullable: false, maxLength: 1024),
                        DeleteDate = c.DateTime(),
                        IsDeleted = c.Boolean(),
                        StatusId = c.Guid(nullable: false),
                        InvoiceNumber = c.String(maxLength: 512),
                        InvoiceNumber1C = c.String(maxLength: 512),
                        InvoiceDatetime1C = c.DateTime(),
                        IsPaid = c.Boolean(nullable: false),
                        IsNotFullPaid = c.Boolean(nullable: false),
                        PaymentDatetime = c.DateTime(),
                        PaymentValue = c.Decimal(precision: 18, scale: 2),
                        PaymentBill = c.Decimal(precision: 18, scale: 2),
                        SendNotification = c.String(maxLength: 20),
                        ZBKCopy_id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_Declarant", t => t.PayerId)
                .ForeignKey("dbo.OBK_Contract", t => t.ContractId)
                .ForeignKey("dbo.Employees", t => t.CreateEmployeeId)
                .Index(t => t.ContractId)
                .Index(t => t.PayerId)
                .Index(t => t.CreateEmployeeId);
            
            CreateTable(
                "dbo.OBK_DirectionSignData",
                c => new
                    {
                        DirectionToPaymentId = c.Guid(nullable: false),
                        ExecutorId = c.Guid(),
                        ExecutorSign = c.String(storeType: "ntext"),
                        ExecutorSignDate = c.DateTime(),
                        ChiefAccountantId = c.Guid(),
                        ChiefAccountantSign = c.String(storeType: "ntext"),
                        ChiefAccountantSignDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.DirectionToPaymentId)
                .ForeignKey("dbo.OBK_DirectionToPayments", t => t.DirectionToPaymentId)
                .Index(t => t.DirectionToPaymentId);
            
            CreateTable(
                "dbo.OBK_LetterPortalEdo",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        LetterSentEdoDate = c.DateTime(),
                        AuthorID = c.Guid(),
                        LetterRegDate = c.DateTime(),
                        LetterContent = c.String(maxLength: 1000),
                        LetterStatusId = c.Int(),
                        ContractId = c.Guid(),
                        EdoRegNomer = c.String(maxLength: 50),
                        EdoRegDate = c.DateTime(),
                        OBKLetterRegID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.OBK_LetterRegistration", t => t.OBKLetterRegID)
                .ForeignKey("dbo.OBK_Contract", t => t.ContractId)
                .Index(t => t.ContractId)
                .Index(t => t.OBKLetterRegID);
            
            CreateTable(
                "dbo.OBK_LetterFromEdo",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        IdEdo = c.String(maxLength: 50),
                        LetterPortalEdoID = c.Long(),
                        LetterRegNomer = c.String(maxLength: 50),
                        LetterRegDate = c.DateTime(),
                        UserEdo = c.String(maxLength: 100),
                        LetterText = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.OBK_LetterPortalEdo", t => t.LetterPortalEdoID)
                .Index(t => t.LetterPortalEdoID);
            
            CreateTable(
                "dbo.OBK_LetterRegistration",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        LetterRegName = c.String(maxLength: 100),
                        LetterRegDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FileLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 4000),
                        Version = c.Int(),
                        DocumentId = c.Guid(),
                        CategoryId = c.Guid(),
                        ParentId = c.Guid(),
                        OwnerId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        Comment = c.String(unicode: false),
                        Language = c.String(maxLength: 20, unicode: false),
                        PageNumbers = c.Int(),
                        StageId = c.Int(),
                        StatusId = c.Int(),
                        IsSigned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DIC_FileLinkStatus", t => t.StatusId)
                .ForeignKey("dbo.FileLinks", t => t.ParentId)
                .ForeignKey("dbo.Dictionaries", t => t.CategoryId)
                .ForeignKey("dbo.EXP_DIC_Stage", t => t.StageId)
                .ForeignKey("dbo.Employees", t => t.OwnerId)
                .Index(t => t.CategoryId)
                .Index(t => t.ParentId)
                .Index(t => t.OwnerId)
                .Index(t => t.StageId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.DIC_FileLinkStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LimsEquipmentAct",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EquipmentId = c.Guid(nullable: false),
                        ActTypeId = c.Guid(nullable: false),
                        Reason = c.String(),
                        State = c.String(),
                        HeadOfLaboratoryId = c.Guid(),
                        HeadOfLaboratoryName = c.String(maxLength: 1000),
                        DirectorRCId = c.Guid(),
                        DirectorRCName = c.String(maxLength: 1000),
                        EngineerId = c.Guid(),
                        EngineerName = c.String(maxLength: 1000),
                        Quantity = c.Int(),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LimsEquipment", t => t.EquipmentId)
                .ForeignKey("dbo.Dictionaries", t => t.ActTypeId)
                .ForeignKey("dbo.Employees", t => t.DirectorRCId)
                .ForeignKey("dbo.Employees", t => t.EngineerId)
                .ForeignKey("dbo.Employees", t => t.HeadOfLaboratoryId)
                .Index(t => t.EquipmentId)
                .Index(t => t.ActTypeId)
                .Index(t => t.HeadOfLaboratoryId)
                .Index(t => t.DirectorRCId)
                .Index(t => t.EngineerId);
            
            CreateTable(
                "dbo.LimsEquipment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        InventoryNumber = c.String(maxLength: 500),
                        SerialNumber = c.String(maxLength: 500),
                        YearInstallation = c.Int(),
                        ModelId = c.Guid(),
                        ProducerId = c.Guid(),
                        CountryProductionId = c.Guid(),
                        EquipmentTypeId = c.Guid(),
                        LocationId = c.Guid(),
                        StatusId = c.Guid(),
                        LaboratoryId = c.Guid(),
                        ResponsiblePersonId = c.Guid(),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.CountryProductionId)
                .ForeignKey("dbo.Dictionaries", t => t.EquipmentTypeId)
                .ForeignKey("dbo.Dictionaries", t => t.LaboratoryId)
                .ForeignKey("dbo.Dictionaries", t => t.LocationId)
                .ForeignKey("dbo.Dictionaries", t => t.ModelId)
                .ForeignKey("dbo.Dictionaries", t => t.ProducerId)
                .ForeignKey("dbo.Dictionaries", t => t.StatusId)
                .ForeignKey("dbo.Employees", t => t.ResponsiblePersonId)
                .Index(t => t.ModelId)
                .Index(t => t.ProducerId)
                .Index(t => t.CountryProductionId)
                .Index(t => t.EquipmentTypeId)
                .Index(t => t.LocationId)
                .Index(t => t.StatusId)
                .Index(t => t.LaboratoryId)
                .Index(t => t.ResponsiblePersonId);
            
            CreateTable(
                "dbo.LimsApplicationJournal",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EquipmentId = c.Guid(),
                        ApplicationDate = c.DateTime(),
                        TypeOfMalfunction = c.String(),
                        ApplicantId = c.Guid(),
                        ApplicationSignDate = c.DateTime(),
                        EngineerId = c.Guid(),
                        EngineerSignDate = c.DateTime(),
                        AccepterId = c.Guid(),
                        AccepterSignDate = c.DateTime(),
                        Result = c.String(),
                        Note = c.String(),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LimsEquipment", t => t.EquipmentId)
                .ForeignKey("dbo.Employees", t => t.AccepterId)
                .ForeignKey("dbo.Employees", t => t.ApplicantId)
                .ForeignKey("dbo.Employees", t => t.EngineerId)
                .Index(t => t.EquipmentId)
                .Index(t => t.ApplicantId)
                .Index(t => t.EngineerId)
                .Index(t => t.AccepterId);
            
            CreateTable(
                "dbo.LimsEquipmentJournalRecord",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EquipmentId = c.Guid(nullable: false),
                        JournalId = c.Guid(),
                        ActionDate = c.DateTime(),
                        NextActionDate = c.DateTime(),
                        ActionInfo = c.String(),
                        ActionResult = c.String(),
                        Note = c.String(),
                        ExecutorId = c.Guid(),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LimsEquipmentJournal", t => t.JournalId)
                .ForeignKey("dbo.LimsEquipment", t => t.EquipmentId)
                .ForeignKey("dbo.Employees", t => t.ExecutorId)
                .Index(t => t.EquipmentId)
                .Index(t => t.JournalId)
                .Index(t => t.ExecutorId);
            
            CreateTable(
                "dbo.LimsEquipmentJournal",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        JournalTypeId = c.Guid(nullable: false),
                        Year = c.Int(),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.JournalTypeId)
                .Index(t => t.JournalTypeId);
            
            CreateTable(
                "dbo.LimsPlanEquipmentLink",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(maxLength: 500),
                        EquipmentId = c.Guid(nullable: false),
                        EquipmentPlanId = c.Guid(nullable: false),
                        IsSign = c.Boolean(),
                        SignDate = c.DateTime(),
                        TermDate = c.DateTime(),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LimsEquipmentPlan", t => t.EquipmentPlanId)
                .ForeignKey("dbo.LimsEquipment", t => t.EquipmentId)
                .Index(t => t.EquipmentId)
                .Index(t => t.EquipmentPlanId);
            
            CreateTable(
                "dbo.LimsEquipmentPlan",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlanTypeId = c.Guid(nullable: false),
                        Year = c.Int(),
                        DirectorRcId = c.Guid(),
                        HeadOfOpoloId = c.Guid(),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.PlanTypeId)
                .ForeignKey("dbo.Employees", t => t.DirectorRcId)
                .ForeignKey("dbo.Employees", t => t.HeadOfOpoloId)
                .Index(t => t.PlanTypeId)
                .Index(t => t.DirectorRcId)
                .Index(t => t.HeadOfOpoloId);
            
            CreateTable(
                "dbo.LimsEquipmentActSpareParts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Quantity = c.Int(),
                        InventoryNumber = c.String(maxLength: 500),
                        LocationId = c.Guid(),
                        EquipmentActId = c.Guid(),
                        CreatedDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LimsEquipmentAct", t => t.EquipmentActId)
                .ForeignKey("dbo.Dictionaries", t => t.LocationId)
                .Index(t => t.LocationId)
                .Index(t => t.EquipmentActId);
            
            CreateTable(
                "dbo.PP_AttachRemarks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceProjectId = c.Guid(nullable: false),
                        AttachPriceDicId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceProjects", t => t.PriceProjectId)
                .ForeignKey("dbo.Dictionaries", t => t.AttachPriceDicId)
                .Index(t => t.PriceProjectId)
                .Index(t => t.AttachPriceDicId);
            
            CreateTable(
                "dbo.PriceProjects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        Number = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        ManufacturerOrganizationId = c.Guid(nullable: false),
                        HolderOrganizationId = c.Guid(nullable: false),
                        ProxyOrganizationId = c.Guid(nullable: false),
                        DoverennostNumber = c.String(maxLength: 500),
                        DoverennostCreatedDate = c.DateTime(storeType: "date"),
                        DoverennostExpiryDate = c.DateTime(storeType: "date"),
                        Filial = c.String(maxLength: 500),
                        NameKz = c.String(maxLength: 1000),
                        NameRu = c.String(maxLength: 1000),
                        RegNumber = c.String(maxLength: 500),
                        RegDate = c.DateTime(storeType: "date"),
                        LsTypeDicId = c.Guid(),
                        NameOriginal = c.String(maxLength: 500),
                        MnnRu = c.String(maxLength: 500),
                        MnnEn = c.String(maxLength: 500),
                        FormNameKz = c.String(maxLength: 500),
                        FormNameRu = c.String(maxLength: 500),
                        Dosage = c.String(maxLength: 500),
                        CountPackage = c.String(maxLength: 500),
                        Concentration = c.String(maxLength: 500),
                        CodeAtx = c.String(maxLength: 500),
                        IntroducingMethodDicId = c.Guid(),
                        IsConvention = c.Boolean(nullable: false),
                        ImnSecuryTypeDicId = c.Guid(),
                        RePriceDicId = c.Guid(),
                        ListTypeDicId = c.Guid(),
                        MnnOrderNumber = c.Int(),
                        ResultTypeDicId = c.Guid(),
                        IsPayed = c.Boolean(),
                        PayDate = c.DateTime(storeType: "date"),
                        StartDate = c.DateTime(storeType: "date"),
                        ContrDate = c.DateTime(storeType: "date"),
                        ConclusionDate = c.DateTime(storeType: "date"),
                        IsStageExpired = c.Boolean(),
                        ExpiredDayCount = c.Int(),
                        ExpertAz = c.String(maxLength: 200),
                        OutgoingDoc = c.String(maxLength: 200),
                        DayCount = c.Int(),
                        IsNewManufacrurer = c.Boolean(),
                        IsOrfan = c.Boolean(nullable: false),
                        ReasonDicId = c.Guid(),
                        PriceProjectId = c.Guid(),
                        Volume = c.String(maxLength: 500, unicode: false),
                        RegisterId = c.Int(),
                        RegisterDfId = c.Int(),
                        IsArchive = c.Boolean(nullable: false),
                        IsSended = c.Boolean(nullable: false),
                        IsSigned = c.Boolean(nullable: false),
                        RequestOrderYear = c.Int(),
                        RequestOrderType = c.Int(),
                        NameChangedRu = c.String(maxLength: 1000),
                        ModifiedDate = c.DateTime(),
                        RejectReasonId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceProjectCom",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PriceProjectId = c.Guid(nullable: false),
                        IsError = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        ControlId = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceProjects", t => t.PriceProjectId)
                .Index(t => t.PriceProjectId);
            
            CreateTable(
                "dbo.PriceProjectComRecord",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CommentId = c.Long(nullable: false),
                        UserId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ValueField = c.String(maxLength: 500),
                        Note = c.String(maxLength: 2000),
                        DisplayField = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceProjectCom", t => t.CommentId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PriceProjectFieldHistory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PriceProjectId = c.Guid(nullable: false),
                        ControlId = c.String(nullable: false, maxLength: 500),
                        UserId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ValueField = c.String(maxLength: 500),
                        DisplayField = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceProjects", t => t.PriceProjectId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.PriceProjectId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PriceProjectsHistory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Guid(),
                        DateCreate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Note = c.String(),
                        PriceProjectId = c.Guid(nullable: false),
                        XmlSign = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PriceProjects", t => t.PriceProjectId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PriceProjectId);
            
            CreateTable(
                "dbo.PP_DIC_AvgExchangeRate",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CurrencyId = c.Guid(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChangeDate = c.DateTime(),
                        ChangeEmployeeId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.CurrencyId)
                .ForeignKey("dbo.Employees", t => t.ChangeEmployeeId)
                .Index(t => t.CurrencyId)
                .Index(t => t.ChangeEmployeeId);
            
            CreateTable(
                "dbo.PP_PharmaList",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                        RegionId = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        Phone = c.String(maxLength: 200),
                        RegisterId = c.Int(),
                        RegisterDfId = c.Int(),
                        MnnName = c.String(maxLength: 4000),
                        TradeName = c.String(maxLength: 4000),
                        RegNumber = c.String(maxLength: 4000),
                        DrugForm = c.String(maxLength: 4000),
                        Dosage = c.String(maxLength: 4000),
                        Concentration = c.String(maxLength: 4000),
                        Box_count = c.String(maxLength: 100),
                        Volume = c.String(maxLength: 4000),
                        Country = c.String(maxLength: 4000),
                        Manufacturer = c.String(maxLength: 4000),
                        PricePackage = c.Decimal(precision: 18, scale: 2),
                        PriceUnit = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.RegionId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.EXP_DIC_CorespondenceKind",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DIC_CorespondenceSubject",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DIC_CorespondenceType",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DrugPrimaryRemark",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugDeclarationId = c.Guid(nullable: false),
                        RemarkTypeId = c.Int(),
                        OtdId = c.Int(),
                        NameRemark = c.String(maxLength: 2000),
                        ExecuterId = c.Guid(),
                        RemarkDate = c.DateTime(),
                        AnswerRemark = c.String(maxLength: 2000),
                        IsFixed = c.Boolean(nullable: false),
                        IsAccepted = c.Boolean(nullable: false),
                        FixedDate = c.DateTime(),
                        Note = c.String(maxLength: 2000),
                        IsReadOnly = c.Boolean(nullable: false),
                        CorespondenceId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_RemarkType", t => t.RemarkTypeId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .ForeignKey("dbo.Employees", t => t.ExecuterId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.RemarkTypeId)
                .Index(t => t.ExecuterId);
            
            CreateTable(
                "dbo.EXP_ExpertiseStageRemark",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StageId = c.Guid(nullable: false),
                        RemarkTypeId = c.Int(),
                        OtdId = c.Int(),
                        NameRemark = c.String(),
                        ExecuterId = c.Guid(),
                        RemarkDate = c.DateTime(),
                        AnswerRemark = c.String(),
                        IsFixed = c.Boolean(nullable: false),
                        IsAccepted = c.Boolean(nullable: false),
                        FixedDate = c.DateTime(),
                        Note = c.String(),
                        IsReadOnly = c.Boolean(nullable: false),
                        CorespondenceId = c.String(maxLength: 50),
                        AtthachId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_RemarkType", t => t.RemarkTypeId)
                .ForeignKey("dbo.EXP_ExpertiseStage", t => t.StageId)
                .ForeignKey("dbo.Employees", t => t.ExecuterId)
                .Index(t => t.StageId)
                .Index(t => t.RemarkTypeId)
                .Index(t => t.ExecuterId);
            
            CreateTable(
                "dbo.EXP_DrugPrice",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PrimaryValue = c.String(maxLength: 500),
                        SecondaryValue = c.String(maxLength: 500),
                        IntermediateValue = c.String(maxLength: 500),
                        CountUnit = c.Double(),
                        Barcode = c.String(maxLength: 50),
                        ManufacturePrice = c.Double(),
                        RefPrice = c.Double(),
                        RegPrice = c.Double(),
                        DrugDosageId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DrugDosage", t => t.DrugDosageId)
                .Index(t => t.DrugDosageId);
            
            CreateTable(
                "dbo.EXP_DIC_StageStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 50),
                        NameRu = c.String(nullable: false, maxLength: 2000),
                        NameKz = c.String(nullable: false, maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEnd = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_ExpertiseStageExecutors",
                c => new
                    {
                        ExpertiseStageId = c.Guid(nullable: false),
                        ExecutorId = c.Guid(nullable: false),
                        IsHead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExpertiseStageId, t.ExecutorId })
                .ForeignKey("dbo.EXP_ExpertiseStage", t => t.ExpertiseStageId)
                .ForeignKey("dbo.Employees", t => t.ExecutorId)
                .Index(t => t.ExpertiseStageId)
                .Index(t => t.ExecutorId);
            
            CreateTable(
                "dbo.EXP_RegistrationExpSteps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Duration = c.Int(nullable: false),
                        RegistrationId = c.Guid(nullable: false),
                        Priority = c.Int(nullable: false),
                        RefId = c.Int(),
                        SupervisingEmployeeId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_RegistrationTypes", t => t.RegistrationId, cascadeDelete: true)
                .ForeignKey("dbo.EXP_DIC_Stage", t => t.RefId)
                .ForeignKey("dbo.Employees", t => t.SupervisingEmployeeId)
                .Index(t => t.RegistrationId)
                .Index(t => t.RefId)
                .Index(t => t.SupervisingEmployeeId);
            
            CreateTable(
                "dbo.EXP_RegistrationTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ParentId = c.Guid(),
                        Code = c.String(maxLength: 50),
                        RefId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_Type", t => t.RefId)
                .ForeignKey("dbo.EXP_RegistrationTypes", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.RefId);
            
            CreateTable(
                "dbo.EXP_DIC_Type",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DIC_AccelerationType",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DIC_ManufactureType",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DIC_PeriodMeasure",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DIC_Status",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DrugDeclarationHistory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Guid(),
                        DateCreate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Note = c.String(),
                        DrugDeclarationId = c.Guid(nullable: false),
                        XmlSign = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_Status", t => t.StatusId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.StatusId)
                .Index(t => t.DrugDeclarationId);
            
            CreateTable(
                "dbo.EXP_DrugChangeType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugDeclarationId = c.Guid(nullable: false),
                        ChangeTypeId = c.Int(nullable: false),
                        Condition = c.String(maxLength: 2000),
                        ConditionOld = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_ChangeType", t => t.ChangeTypeId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.ChangeTypeId);
            
            CreateTable(
                "dbo.EXP_DIC_ChangeType",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        ChangeName = c.String(maxLength: 2000),
                        ChangeType = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DrugDeclarationCom",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugDeclarationId = c.Guid(nullable: false),
                        IsError = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        ControlId = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .Index(t => t.DrugDeclarationId);
            
            CreateTable(
                "dbo.EXP_DrugDeclarationComRecord",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CommentId = c.Long(nullable: false),
                        UserId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ValueField = c.String(maxLength: 500),
                        Note = c.String(maxLength: 2000),
                        DisplayField = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DrugDeclarationCom", t => t.CommentId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EXP_DrugDeclarationFieldHistory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugDeclarationId = c.Guid(nullable: false),
                        ControlId = c.String(nullable: false, maxLength: 500),
                        UserId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        ValueField = c.String(maxLength: 500),
                        DisplayField = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EXP_DrugOrganizations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugDeclarationId = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        NameKz = c.String(maxLength: 500),
                        NameRu = c.String(maxLength: 500),
                        NameEn = c.String(maxLength: 500),
                        CountryDicId = c.Guid(),
                        AddressLegal = c.String(maxLength: 500),
                        AddressFact = c.String(maxLength: 500),
                        Phone = c.String(maxLength: 500),
                        Fax = c.String(maxLength: 500),
                        Email = c.String(maxLength: 500),
                        BossFio = c.String(maxLength: 500),
                        BossPosition = c.String(maxLength: 500),
                        ContactFio = c.String(maxLength: 500),
                        ContactPosition = c.String(maxLength: 500),
                        ContactPhone = c.String(maxLength: 500),
                        ContactFax = c.String(maxLength: 500),
                        ContactEmail = c.String(maxLength: 500),
                        OrgManufactureTypeDicId = c.Guid(),
                        DocNumber = c.String(maxLength: 500),
                        DocDate = c.DateTime(storeType: "date"),
                        DocExpiryDate = c.DateTime(storeType: "date"),
                        ObjectId = c.Guid(),
                        OpfTypeDicId = c.Guid(),
                        BankName = c.String(maxLength: 500),
                        BankIik = c.String(maxLength: 500),
                        BankCurencyDicId = c.Guid(),
                        BankSwift = c.String(maxLength: 500),
                        Bin = c.String(maxLength: 500),
                        IsResident = c.Boolean(nullable: false),
                        PayerTypeDicId = c.Guid(),
                        BossLastName = c.String(maxLength: 100),
                        BossFirstName = c.String(maxLength: 100),
                        BossMiddleName = c.String(maxLength: 100),
                        PaymentBill = c.String(maxLength: 500),
                        Iin = c.String(maxLength: 500),
                        BankBik = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .Index(t => t.DrugDeclarationId);
            
            CreateTable(
                "dbo.EXP_DrugPatent",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugDeclarationId = c.Guid(nullable: false),
                        PatentNumber = c.String(maxLength: 500),
                        PatentDate = c.DateTime(),
                        PatentExpiryDate = c.DateTime(),
                        NameOwner = c.String(maxLength: 500),
                        NameDocument = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .Index(t => t.DrugDeclarationId);
            
            CreateTable(
                "dbo.EXP_DrugPrimaryKind",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugDeclarationId = c.Guid(nullable: false),
                        PrimaryKindId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_PrimaryMark", t => t.PrimaryKindId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.PrimaryKindId);
            
            CreateTable(
                "dbo.EXP_DIC_PrimaryMark",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DrugPrimaryNTD",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugDeclarationId = c.Guid(nullable: false),
                        DateReg = c.DateTime(),
                        DateConfirm = c.DateTime(),
                        TypeNDId = c.Int(),
                        TypeFileNDId = c.Int(),
                        isNdConfirm = c.Boolean(nullable: false),
                        NumberNd = c.String(maxLength: 50),
                        Note = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_TypeFileND", t => t.TypeFileNDId)
                .ForeignKey("dbo.EXP_DIC_TypeND", t => t.TypeNDId)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.TypeNDId)
                .Index(t => t.TypeFileNDId);
            
            CreateTable(
                "dbo.EXP_DIC_TypeFileND",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DIC_TypeND",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DrugProtectionDoc",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugDeclarationId = c.Guid(nullable: false),
                        NameDocument = c.String(maxLength: 500),
                        NumberDocument = c.String(maxLength: 500),
                        IssueDate = c.DateTime(),
                        ExpireDate = c.DateTime(),
                        NameOwner = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .Index(t => t.DrugDeclarationId);
            
            CreateTable(
                "dbo.EXP_DrugType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DrugDeclarationId = c.Guid(nullable: false),
                        DrugTypeId = c.Int(),
                        DrugName = c.String(maxLength: 500),
                        DrugTypeKind = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_DrugType", t => t.DrugTypeId)
                .ForeignKey("dbo.EXP_DIC_DrugTypeKind", t => t.DrugTypeKind)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId)
                .Index(t => t.DrugDeclarationId)
                .Index(t => t.DrugTypeId)
                .Index(t => t.DrugTypeKind);
            
            CreateTable(
                "dbo.EXP_DIC_DrugType",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsNeedName = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DIC_DrugTypeKind",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        DateCreate = c.DateTime(nullable: false),
                        IsNeedName = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContractSignedDatas",
                c => new
                    {
                        ContractId = c.Guid(nullable: false),
                        ApplicantSig = c.String(storeType: "ntext"),
                        CeoSign = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.ContractId)
                .ForeignKey("dbo.Contracts", t => t.ContractId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.ContractProcSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProcCenterHeadId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.ProcCenterHeadId)
                .Index(t => t.ProcCenterHeadId);
            
            CreateTable(
                "dbo.FileLinksCategoryComRecord",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CommentId = c.Guid(nullable: false),
                        UserId = c.Guid(),
                        CreateDate = c.DateTime(nullable: false),
                        Note = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileLinksCategoryCom", t => t.CommentId)
                .ForeignKey("dbo.Employees", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FileLinksCategoryCom",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DocumentId = c.Guid(),
                        CategoryId = c.Guid(),
                        IsError = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        IsNotApplicable = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GridSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                        Model = c.String(nullable: false, storeType: "ntext"),
                        GridName = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.PP_ProtocolComissionMembers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ProtocolId = c.Guid(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PP_Protocols", t => t.ProtocolId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.ProtocolId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.PP_Protocols",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Number = c.String(maxLength: 500),
                        ProtocolDate = c.DateTime(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        ChiefId = c.Guid(),
                        RequesterId = c.Guid(),
                        RequesterName = c.String(maxLength: 500),
                        AdditionalPersonName = c.String(maxLength: 500),
                        IsImn = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PP_ProtocolProductPrices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ProtocolId = c.Guid(nullable: false),
                        ProductNameRu = c.String(maxLength: 500),
                        ProductNameKz = c.String(maxLength: 500),
                        PriceFirst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceNew = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceProjectId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PP_Protocols", t => t.ProtocolId)
                .Index(t => t.ProtocolId);
            
            CreateTable(
                "dbo.VisitEmployeeWorkingTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        TimeBegin = c.Int(nullable: false),
                        TimeEnd = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitTypeId = c.Int(nullable: false),
                        VisitorId = c.Guid(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        TimeBegin = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        Comment = c.String(maxLength: 4000),
                        VisitStatusId = c.Int(nullable: false),
                        RatingValue = c.Int(),
                        RatingComment = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VisitStatuses", t => t.VisitStatusId)
                .ForeignKey("dbo.VisitTypes", t => t.VisitTypeId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Employees", t => t.VisitorId)
                .Index(t => t.VisitTypeId)
                .Index(t => t.VisitorId)
                .Index(t => t.EmployeeId)
                .Index(t => t.VisitStatusId);
            
            CreateTable(
                "dbo.VisitStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 4000),
                        Group = c.String(maxLength: 4000),
                        Time = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitEmployeeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Guid(nullable: false),
                        VisitTypeId = c.Int(nullable: false),
                        IsEnable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VisitTypes", t => t.VisitTypeId)
                .Index(t => t.VisitTypeId);
            
            CreateTable(
                "dbo.CommissionDrugDosageCount2View",
                c => new
                    {
                        CommissionId = c.Int(nullable: false),
                        IsRepeat = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        ConclusionTypeId = c.Int(),
                        Count = c.Int(),
                    })
                .PrimaryKey(t => new { t.CommissionId, t.IsRepeat, t.TypeId });
            
            CreateTable(
                "dbo.CommissionDrugDosageCount3View",
                c => new
                    {
                        CommissionId = c.Int(nullable: false),
                        IsRepeat = c.Int(nullable: false),
                        ConclusionTypeId = c.Int(),
                        TypeList = c.String(),
                    })
                .PrimaryKey(t => new { t.CommissionId, t.IsRepeat });
            
            CreateTable(
                "dbo.CommissionDrugDosageCount3WithoutRepeatView",
                c => new
                    {
                        CommissionId = c.Int(nullable: false),
                        ConclusionTypeId = c.Int(),
                        TypeList = c.String(),
                    })
                .PrimaryKey(t => t.CommissionId);
            
            CreateTable(
                "dbo.CommissionDrugDosageCount4View",
                c => new
                    {
                        CommissionId = c.Int(nullable: false),
                        IsRepeat = c.Int(nullable: false),
                        TypeName = c.String(nullable: false, maxLength: 200),
                        TypeId = c.Int(),
                        Count = c.Int(),
                        TypeName2 = c.String(maxLength: 300),
                        TypeList = c.String(),
                    })
                .PrimaryKey(t => new { t.CommissionId, t.IsRepeat, t.TypeName });
            
            CreateTable(
                "dbo.CommissionDrugDosageCount4WithoutRepeatView",
                c => new
                    {
                        CommissionId = c.Int(nullable: false),
                        TypeName = c.String(nullable: false, maxLength: 200),
                        TypeId = c.Int(),
                        Count = c.Int(),
                        TypeName2 = c.String(maxLength: 300),
                        TypeList = c.String(),
                    })
                .PrimaryKey(t => new { t.CommissionId, t.TypeName });
            
            CreateTable(
                "dbo.CommissionDrugDosageCountView",
                c => new
                    {
                        CommissionId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        ConclusionTypeId = c.Int(),
                        Count = c.Int(),
                    })
                .PrimaryKey(t => new { t.CommissionId, t.TypeId });
            
            CreateTable(
                "dbo.CommissionsView",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        FullNumber = c.String(nullable: false, maxLength: 100),
                        Date = c.DateTime(nullable: false),
                        IsComplete = c.Boolean(nullable: false),
                        TypeId = c.Int(nullable: false),
                        TypeName = c.String(nullable: false, maxLength: 300),
                        KindId = c.Int(nullable: false),
                        KindName = c.String(nullable: false, maxLength: 200),
                        KindShortName = c.String(nullable: false, maxLength: 10),
                        Comment = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.Number, t.FullNumber, t.Date, t.IsComplete, t.TypeId, t.TypeName, t.KindId, t.KindName, t.KindShortName });
            
            CreateTable(
                "dbo.Compositions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MaterialTypeDicId = c.Guid(),
                        MaterialNameDicId = c.Guid(),
                        MaterialName = c.String(maxLength: 500),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NormativeDocument = c.String(maxLength: 500),
                        CountryDicId = c.Guid(),
                        City = c.String(maxLength: 500),
                        Street = c.String(maxLength: 500),
                        House = c.String(maxLength: 500),
                        IsControl = c.Boolean(nullable: false),
                        IsPoison = c.Boolean(nullable: false),
                        MaterialGainDicId = c.Guid(),
                        MaterialGain = c.String(maxLength: 500),
                        MaterialOriginDicId = c.Guid(),
                        ObjectId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CompositionsView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsControl = c.Boolean(nullable: false),
                        IsPoison = c.Boolean(nullable: false),
                        MaterialTypeDicId = c.Guid(),
                        MaterialNameDicId = c.Guid(),
                        MaterialName = c.String(maxLength: 500),
                        NormativeDocument = c.String(maxLength: 500),
                        CountryDicId = c.Guid(),
                        City = c.String(maxLength: 500),
                        Street = c.String(maxLength: 500),
                        House = c.String(maxLength: 500),
                        MaterialGainDicId = c.Guid(),
                        MaterialGain = c.String(maxLength: 500),
                        MaterialOriginDicId = c.Guid(),
                        MaterialTypeName = c.String(maxLength: 4000),
                        Name = c.String(maxLength: 4000),
                        CountryName = c.String(maxLength: 4000),
                        MaterialGainName = c.String(maxLength: 4000),
                        MaterialOriginName = c.String(maxLength: 4000),
                        ObjectId = c.Guid(),
                    })
                .PrimaryKey(t => new { t.Id, t.Count, t.IsControl, t.IsPoison });
            
            CreateTable(
                "dbo.ContractAdditionJournal",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ManufacturerOrganizationId = c.Guid(),
                        ApplicantOrganizationId = c.Guid(),
                        DoverennostTypeDicId = c.Guid(),
                        DoverennostNumber = c.String(maxLength: 500),
                        DoverennostCreatedDate = c.DateTime(storeType: "date"),
                        DoverennostExpiryDate = c.DateTime(storeType: "date"),
                        HolderOrganizationId = c.Guid(),
                        PayerOrganizationId = c.Guid(),
                        Number = c.String(maxLength: 500),
                        ContractNumber = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(storeType: "date"),
                        ContractId = c.Guid(),
                        ContractDate = c.DateTime(storeType: "date"),
                        StartDate = c.DateTime(storeType: "date"),
                        EndDate = c.DateTime(storeType: "date"),
                        IsExpired = c.Boolean(),
                        IsSite = c.Boolean(),
                        ManufacturerName = c.String(maxLength: 500),
                        ManufacturerCountry = c.String(maxLength: 4000),
                        ApplicantCountry = c.String(maxLength: 4000),
                        ApplicantCurrency = c.String(maxLength: 4000),
                        Login = c.String(maxLength: 255),
                        DocumentTypeName = c.String(maxLength: 4000),
                        StatusName = c.String(maxLength: 4000),
                        StatusCode = c.String(maxLength: 4000),
                        HolderName = c.String(maxLength: 500),
                        ApplicantName = c.String(maxLength: 500),
                        PayerName = c.String(maxLength: 500),
                        CorrespondentsId = c.String(maxLength: 4000),
                        CorrespondentsValue = c.String(maxLength: 4000),
                        ExecutorId = c.String(maxLength: 4000),
                        CountApplications = c.Int(),
                        ContractAdditionType = c.String(maxLength: 4000),
                        SignerId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContractJournal",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ManufacturerOrganizationId = c.Guid(),
                        ApplicantOrganizationId = c.Guid(),
                        DoverennostTypeDicId = c.Guid(),
                        DoverennostNumber = c.String(maxLength: 500),
                        DoverennostCreatedDate = c.DateTime(storeType: "date"),
                        DoverennostExpiryDate = c.DateTime(storeType: "date"),
                        HolderOrganizationId = c.Guid(),
                        PayerOrganizationId = c.Guid(),
                        Number = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(storeType: "date"),
                        ContractId = c.Guid(),
                        ContractDate = c.DateTime(storeType: "date"),
                        StartDate = c.DateTime(storeType: "date"),
                        EndDate = c.DateTime(storeType: "date"),
                        IsExpired = c.Boolean(),
                        IsSite = c.Boolean(),
                        ManufacturerName = c.String(maxLength: 500),
                        ManufacturerCountry = c.String(maxLength: 4000),
                        ApplicantCountry = c.String(maxLength: 4000),
                        ApplicantCurrency = c.String(maxLength: 4000),
                        Login = c.String(maxLength: 255),
                        DocumentTypeName = c.String(maxLength: 4000),
                        StatusName = c.String(maxLength: 4000),
                        StatusCode = c.String(maxLength: 4000),
                        HolderName = c.String(maxLength: 500),
                        ApplicantName = c.String(maxLength: 500),
                        PayerName = c.String(maxLength: 500),
                        CorrespondentsId = c.String(maxLength: 4000),
                        CorrespondentsValue = c.String(maxLength: 4000),
                        ExecutorId = c.String(maxLength: 4000),
                        CountApplications = c.Int(),
                        SignerId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContractsView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        IsHasDoverennostNumber = c.Boolean(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        ManufacturerOrganizationId = c.Guid(),
                        ApplicantOrganizationId = c.Guid(),
                        DoverennostTypeDicId = c.Guid(),
                        DoverennostNumber = c.String(maxLength: 500),
                        DoverennostCreatedDate = c.DateTime(storeType: "date"),
                        DoverennostExpiryDate = c.DateTime(storeType: "date"),
                        HolderOrganizationId = c.Guid(),
                        PayerOrganizationId = c.Guid(),
                        PayerTranslationOrganizationId = c.Guid(),
                        Number = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(storeType: "date"),
                        ContractId = c.Guid(),
                        Type = c.Int(),
                        ContractDate = c.DateTime(storeType: "date"),
                        StartDate = c.DateTime(storeType: "date"),
                        EndDate = c.DateTime(storeType: "date"),
                        IsExpired = c.Boolean(),
                        IsSite = c.Boolean(),
                        ManufactureOrgName = c.String(maxLength: 500),
                        ApplicantOrgName = c.String(maxLength: 500),
                        HolderOrgName = c.String(maxLength: 500),
                        PayerOrgName = c.String(maxLength: 500),
                        PayerTranslationOrgName = c.String(maxLength: 500),
                        DoverenostName = c.String(maxLength: 4000),
                        StatusName = c.String(maxLength: 4000),
                        ContractAdditionTypeId = c.Guid(),
                        ContractAdditionTypeName = c.String(maxLength: 4000),
                        AgentOrganizationId = c.Guid(),
                        AgentDocTypeDicId = c.Guid(),
                        AgentDocNumber = c.String(maxLength: 500),
                        AgentDocCreateDate = c.DateTime(storeType: "date"),
                        AgentDocExpiryDate = c.DateTime(storeType: "date"),
                        IsHasAgentDocNumber = c.Boolean(),
                        AgentDocName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.Status, t.IsHasDoverennostNumber, t.OwnerId });
            
            CreateTable(
                "dbo.CorrespondenceDocument",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsAdministrativeUse = c.Boolean(nullable: false),
                        IsAwaitingResponse = c.Boolean(nullable: false),
                        IsTradeSecret = c.Boolean(nullable: false),
                        ApplicantType = c.Int(nullable: false),
                        DocumentType = c.Int(nullable: false),
                        MonitoringType = c.Int(nullable: false),
                        PriorityType = c.Int(nullable: false),
                        StateType = c.Int(nullable: false),
                        AppendixCount = c.Int(nullable: false),
                        CopiesCount = c.Int(nullable: false),
                        PageCount = c.Int(nullable: false),
                        RepeatCount = c.Int(nullable: false),
                        SortNumber = c.Int(nullable: false),
                        OutgoingType = c.Int(nullable: false),
                        IsNotification = c.Boolean(nullable: false),
                        NotificationCount = c.Int(nullable: false),
                        IsAttachments = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        AwaitingResponseDate = c.DateTime(),
                        DocumentDate = c.DateTime(),
                        OutgoingDate = c.DateTime(),
                        ProtocolDate = c.DateTime(),
                        MonitoringDate = c.DateTime(),
                        ApplicantAddress = c.String(maxLength: 512),
                        ApplicantEmail = c.String(maxLength: 4000),
                        ApplicantName = c.String(maxLength: 512),
                        ApplicantPhone = c.String(maxLength: 512),
                        BlankNumber = c.String(maxLength: 512),
                        CorrespondentsInfo = c.String(maxLength: 512),
                        Number = c.String(maxLength: 512),
                        OutgoingNumber = c.String(maxLength: 512),
                        SortingNumber = c.String(maxLength: 512),
                        SortingOutgoingNumber = c.String(maxLength: 512),
                        Note = c.String(maxLength: 4000),
                        Summary = c.String(maxLength: 4000),
                        AdministrativeTypeDictionaryValue = c.String(maxLength: 4000),
                        ApplicantCategoryDictionaryValue = c.String(maxLength: 4000),
                        CauseCitizenDictionaryValue = c.String(maxLength: 4000),
                        CitizenCategoryDictionaryValue = c.String(maxLength: 4000),
                        CitizenResultDictionaryValue = c.String(maxLength: 4000),
                        CitizenTypeDictionaryValue = c.String(maxLength: 4000),
                        DocumentKindDictionaryValue = c.String(maxLength: 4000),
                        FormDeliveryDictionaryValue = c.String(maxLength: 4000),
                        FormSendingDictionaryValue = c.String(maxLength: 4000),
                        KatoDictionaryValue = c.String(maxLength: 4000),
                        LanguageDictionaryValue = c.String(maxLength: 4000),
                        NomenclatureDictionaryValue = c.String(maxLength: 4000),
                        QuestionDesignDictionaryValue = c.String(maxLength: 4000),
                        SigningFormDictionaryValue = c.String(maxLength: 4000),
                        AgreementsValue = c.String(maxLength: 4000),
                        ExecutorsValue = c.String(maxLength: 4000),
                        ReadersValue = c.String(maxLength: 4000),
                        RecipientsValue = c.String(maxLength: 4000),
                        RegistratorValue = c.String(maxLength: 4000),
                        ResponsibleValue = c.String(maxLength: 4000),
                        SignerValue = c.String(maxLength: 4000),
                        CorrespondentsValue = c.String(maxLength: 4000),
                        MonitoringAuthorValue = c.String(maxLength: 4000),
                        MonitoringNote = c.String(maxLength: 4000),
                        AnswersValue = c.String(maxLength: 4000),
                        CompleteDocumentsValue = c.String(maxLength: 4000),
                        EditDocumentsValue = c.String(maxLength: 4000),
                        RepealDocumentsValue = c.String(maxLength: 4000),
                        DisplayName = c.String(maxLength: 4000),
                        AutoAnswersValue = c.String(maxLength: 4000),
                        AutoAnswersTempValue = c.String(maxLength: 4000),
                        AutoCompleteDocumentsValue = c.String(maxLength: 4000),
                        AutoEditDocumentsValue = c.String(maxLength: 4000),
                        AutoRepealDocumentsValue = c.String(maxLength: 4000),
                        AutoAwaitingResponseDate = c.DateTime(storeType: "date"),
                        AutoDocumentDate = c.DateTime(storeType: "date"),
                        AutoOutgoingDate = c.DateTime(storeType: "date"),
                        AutoProtocolDate = c.DateTime(storeType: "date"),
                        AutoMonitoringDate = c.DateTime(storeType: "date"),
                        FactExecutionDate = c.DateTime(),
                        FirstExecutionDate = c.DateTime(),
                        ExecutionDate = c.DateTime(),
                        AutoFactExecutionDate = c.DateTime(storeType: "date"),
                        AutoFirstExecutionDate = c.DateTime(storeType: "date"),
                        AutoExecutionDate = c.DateTime(storeType: "date"),
                        Counters = c.String(maxLength: 4000),
                        DocumentDictionaryTypeValue = c.String(maxLength: 4000),
                        ResolutionValue = c.String(maxLength: 4000),
                        SourceValue = c.String(maxLength: 4000),
                        DestinationValue = c.String(maxLength: 4000),
                        OwnerValue = c.String(maxLength: 4000),
                        Country = c.String(maxLength: 4000),
                        Area = c.String(maxLength: 4000),
                        Postcode = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 4000),
                        Department = c.String(maxLength: 4000),
                        City = c.String(maxLength: 4000),
                        Address = c.String(maxLength: 4000),
                        NumberBill = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        SuperMainDocumentId = c.Guid(),
                        ModifiedUser = c.String(maxLength: 4000),
                        DateDispatch = c.DateTime(),
                        DispatchNote = c.String(maxLength: 4000),
                        Digest = c.String(maxLength: 4000),
                        Text = c.String(maxLength: 4000),
                        Recipient = c.String(maxLength: 4000),
                        QrCode = c.Binary(storeType: "image"),
                        FulfilledDate = c.DateTime(),
                        State = c.String(maxLength: 510),
                        Monitoring = c.String(maxLength: 510),
                        Priority = c.String(maxLength: 510),
                    })
                .PrimaryKey(t => new { t.Id, t.IsDeleted, t.IsAdministrativeUse, t.IsAwaitingResponse, t.IsTradeSecret, t.ApplicantType, t.DocumentType, t.MonitoringType, t.PriorityType, t.StateType, t.AppendixCount, t.CopiesCount, t.PageCount, t.RepeatCount, t.SortNumber, t.OutgoingType, t.IsNotification, t.NotificationCount, t.IsAttachments, t.IsArchive });
            
            CreateTable(
                "dbo.DIC_IcdDeseases",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CodeICD = c.String(maxLength: 512),
                        DiseaseOfICD = c.String(),
                        SysnonimAndRareDesease = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DIC_OrphanDrugs",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        CreateDatetime = c.DateTime(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.CreateDatetime });
            
            CreateTable(
                "dbo.EMP_ContractHistoryView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                        StatusId = c.Guid(nullable: false),
                        ContractId = c.Guid(nullable: false),
                        UnitName = c.String(maxLength: 4000),
                        RefuseReason = c.String(maxLength: 4000),
                        EmployeeFullName = c.String(maxLength: 4000),
                        EmployeeShortName = c.String(maxLength: 4000),
                        StatusCode = c.String(maxLength: 50),
                        StatusNameRu = c.String(maxLength: 2000),
                        StatusNameKz = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => new { t.Id, t.Created, t.EmployeeId, t.StatusId, t.ContractId });
            
            CreateTable(
                "dbo.EMP_EAESStatement",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RegistrationKindValue = c.String(maxLength: 50),
                        RegistrationCertificateNumber = c.String(maxLength: 50),
                        NormativeDocumentNumber = c.String(maxLength: 50),
                        RegistrationDate = c.DateTime(),
                        ExpirationDate = c.DateTime(),
                        LetterNumber = c.String(maxLength: 50),
                        LetterDate = c.DateTime(),
                        IsMt = c.Boolean(),
                        MedicalDeviceNameKz = c.String(maxLength: 255),
                        MedicalDeviceNameRu = c.String(maxLength: 255),
                        NomenclatureCode = c.String(maxLength: 50),
                        NomenclatureNameKz = c.String(maxLength: 255),
                        NomenclatureNameRu = c.String(maxLength: 255),
                        NomenclatureDescriptionKz = c.String(maxLength: 2000),
                        NomenclatureDescriptionRu = c.String(maxLength: 2000),
                        ApplicationAreaKz = c.String(maxLength: 2000),
                        ApplicationAreaRu = c.String(maxLength: 2000),
                        PurposeKz = c.String(maxLength: 255),
                        PurposeRu = c.String(maxLength: 255),
                        IsClosedSystem = c.Boolean(),
                        RegistrationDossierPageNumber = c.String(maxLength: 50),
                        ShortTechnicalCharacteristicKz = c.String(maxLength: 255),
                        ShortTechnicalCharacteristicRu = c.String(maxLength: 255),
                        ClassOfPotentialRisk = c.String(maxLength: 50),
                        IsBalk = c.Boolean(),
                        IsMeasurementDevice = c.Boolean(),
                        IsForInvitroDiagnostics = c.Boolean(),
                        IsSterile = c.Boolean(),
                        IsMedicalProductPresence = c.Boolean(),
                        WithouAe = c.Boolean(),
                        TransportConditions = c.String(maxLength: 255),
                        StorageConditions = c.String(maxLength: 255),
                        Production = c.String(maxLength: 255),
                        IsComplectation = c.Boolean(),
                        ManufacturerType = c.String(maxLength: 255),
                        ManufacturerNameRu = c.String(maxLength: 255),
                        AllowedDocumentNumber = c.String(maxLength: 255),
                        BossLastName = c.String(maxLength: 50),
                        BossPosition = c.String(maxLength: 255),
                        OrganizationForm = c.String(maxLength: 255),
                        ManufacturerNameKz = c.String(maxLength: 255),
                        DateOfIssue = c.DateTime(),
                        BossFirstName = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 50),
                        Country = c.String(maxLength: 255),
                        ManufacturerNameEn = c.String(maxLength: 255),
                        ManufacturerExpirationDate = c.DateTime(),
                        BossMiddleName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 255),
                        ContactPersonInitials = c.String(maxLength: 255),
                        ContactPersonLegalAddress = c.String(maxLength: 255),
                        ContactPersonPosition = c.String(maxLength: 255),
                        ContactPersonFactAddress = c.String(maxLength: 255),
                        Agreement = c.String(maxLength: 3999),
                        IsAgreed = c.Boolean(),
                        ContractId = c.Guid(),
                        RegistrationTypeValue = c.String(maxLength: 50),
                        NmirkId = c.Int(),
                        RefCountry = c.String(maxLength: 50),
                        ConCountry = c.String(maxLength: 50),
                        GarantExpDate = c.Int(),
                        GarantUnit = c.String(maxLength: 50),
                        GarantNoExp = c.Boolean(),
                        PlaceType = c.String(maxLength: 255),
                        PlaceNameRu = c.String(maxLength: 255),
                        PlaceAllowedDocumentNumber = c.String(maxLength: 255),
                        PlaceBossLastName = c.String(maxLength: 255),
                        PlaceBossPosition = c.String(maxLength: 255),
                        PlaceOrganizationForm = c.String(maxLength: 255),
                        PlaceNameKz = c.String(maxLength: 255),
                        PlaceDateOfIssue = c.DateTime(storeType: "date"),
                        PlaceBossFirstName = c.String(maxLength: 255),
                        PlacePhone = c.String(maxLength: 255),
                        PlaceCountry = c.String(maxLength: 255),
                        PlaceNameEn = c.String(maxLength: 255),
                        PlaceExpirationDate = c.DateTime(storeType: "date"),
                        PlaceBossMiddleName = c.String(maxLength: 255),
                        PlaceEmail = c.String(maxLength: 255),
                        PlaceContactPersonInitials = c.String(maxLength: 255),
                        PlaceContactPersonPosition = c.String(maxLength: 255),
                        PlaceContactPersonFactAddress = c.String(maxLength: 255),
                        ShowerType = c.String(maxLength: 255),
                        ShowerNameRu = c.String(maxLength: 255),
                        ShowerPAllowedDocumentNumber = c.String(maxLength: 255),
                        ShowerBossLastName = c.String(maxLength: 255),
                        ShowerBossPosition = c.String(maxLength: 255),
                        ShowerOrganizationForm = c.String(maxLength: 255),
                        ShowerNameKz = c.String(maxLength: 255),
                        ShowerDateOfIssue = c.DateTime(storeType: "date"),
                        ShowerBossFirstName = c.String(maxLength: 255),
                        ShowerPhone = c.String(maxLength: 255),
                        ShowerCountry = c.String(maxLength: 255),
                        ShowerNameEn = c.String(maxLength: 255),
                        ShowerExpirationDate = c.DateTime(storeType: "date"),
                        ShowerBossMiddleName = c.String(maxLength: 255),
                        ShowerEmail = c.String(maxLength: 255),
                        ShowerContactPersonInitials = c.String(maxLength: 255),
                        ShowerContactPersonPosition = c.String(maxLength: 255),
                        ShowerContactPersonFactAddress = c.String(maxLength: 255),
                        ShowerContactPersonActualAddress = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_NMIRK", t => t.NmirkId)
                .Index(t => t.NmirkId);
            
            CreateTable(
                "dbo.EXP_DIC_NMIRK",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        NameRu = c.String(maxLength: 50),
                        NameKk = c.String(maxLength: 50),
                        DescriptionRu = c.String(maxLength: 1000),
                        Descriptionkk = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_Statement",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RegistrationKindValue = c.String(maxLength: 50),
                        RegistrationCertificateNumber = c.String(maxLength: 50),
                        NormativeDocumentNumber = c.String(maxLength: 50),
                        RegistrationDate = c.DateTime(),
                        ExpirationDate = c.DateTime(),
                        LetterNumber = c.String(maxLength: 50),
                        LetterDate = c.DateTime(),
                        IsMt = c.Boolean(),
                        MedicalDeviceNameKz = c.String(maxLength: 255),
                        MedicalDeviceNameRu = c.String(maxLength: 255),
                        NomenclatureCode = c.String(maxLength: 50),
                        NomenclatureNameKz = c.String(maxLength: 255),
                        NomenclatureNameRu = c.String(maxLength: 255),
                        NomenclatureDescriptionKz = c.String(maxLength: 2000),
                        NomenclatureDescriptionRu = c.String(maxLength: 2000),
                        ApplicationAreaKz = c.String(maxLength: 2000),
                        ApplicationAreaRu = c.String(maxLength: 2000),
                        PurposeKz = c.String(maxLength: 255),
                        PurposeRu = c.String(maxLength: 255),
                        IsClosedSystem = c.Boolean(),
                        RegistrationDossierPageNumber = c.String(maxLength: 50),
                        ShortTechnicalCharacteristicKz = c.String(maxLength: 255),
                        ShortTechnicalCharacteristicRu = c.String(maxLength: 255),
                        ClassOfPotentialRisk = c.String(maxLength: 50),
                        IsBalk = c.Boolean(),
                        IsMeasurementDevice = c.Boolean(),
                        IsForInvitroDiagnostics = c.Boolean(),
                        IsSterile = c.Boolean(),
                        IsMedicalProductPresence = c.Boolean(),
                        WithouAe = c.Boolean(),
                        TransportConditions = c.String(maxLength: 255),
                        StorageConditions = c.String(maxLength: 255),
                        Production = c.String(maxLength: 255),
                        IsComplectation = c.Boolean(),
                        ManufacturerType = c.String(maxLength: 255),
                        ManufacturerNameRu = c.String(maxLength: 255),
                        AllowedDocumentNumber = c.String(maxLength: 255),
                        BossLastName = c.String(maxLength: 50),
                        BossPosition = c.String(maxLength: 255),
                        OrganizationForm = c.String(maxLength: 255),
                        ManufacturerNameKz = c.String(maxLength: 255),
                        DateOfIssue = c.DateTime(),
                        BossFirstName = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 50),
                        Country = c.String(maxLength: 255),
                        ManufacturerNameEn = c.String(maxLength: 255),
                        ManufacturerExpirationDate = c.DateTime(),
                        BossMiddleName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 255),
                        ContactPersonInitials = c.String(maxLength: 255),
                        ContactPersonLegalAddress = c.String(maxLength: 255),
                        ContactPersonPosition = c.String(maxLength: 255),
                        ContactPersonFactAddress = c.String(maxLength: 255),
                        Agreement = c.String(maxLength: 3999),
                        IsAgreed = c.Boolean(),
                        ContractId = c.Guid(),
                        RegistrationTypeValue = c.String(maxLength: 50),
                        NmirkId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_NMIRK", t => t.NmirkId)
                .Index(t => t.NmirkId);
            
            CreateTable(
                "dbo.EMP_StatementSamples",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatementId = c.Guid(nullable: false),
                        SampleType = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Count = c.Int(nullable: false),
                        Unit = c.String(nullable: false, maxLength: 50),
                        SeriesPart = c.String(nullable: false, maxLength: 50),
                        Storage = c.String(maxLength: 50),
                        Conditions = c.String(maxLength: 50),
                        CreateDate = c.DateTime(storeType: "date"),
                        ExpirationDate = c.DateTime(storeType: "date"),
                        Addition = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EMP_Statement", t => t.StatementId)
                .Index(t => t.StatementId);
            
            CreateTable(
                "dbo.EMP_EAESStatementRegCountry",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(nullable: false, maxLength: 50),
                        RegNumber = c.String(maxLength: 50),
                        DateOfIssue = c.DateTime(storeType: "date"),
                        ExpDate = c.DateTime(storeType: "date"),
                        IsIndefinitely = c.Boolean(),
                        StatementId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_Ref_ChangeType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameRu = c.String(nullable: false, maxLength: 500),
                        NameKz = c.String(nullable: false, maxLength: 500),
                        Code = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_Ref_ContractDocumentType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        NameGenitiveRu = c.String(nullable: false, maxLength: 255),
                        NameGenitiveKz = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_Ref_HolderType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        IsDeleted = c.Boolean(nullable: false),
                        Code = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_StatementChange",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 2000),
                        Type = c.String(maxLength: 2000),
                        BeforeChange = c.String(maxLength: 2000),
                        AfterChange = c.String(maxLength: 2000),
                        StatementId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_StatementCountryRegistration",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Country = c.String(maxLength: 255),
                        RegistrationNumber = c.String(maxLength: 50),
                        StartDate = c.DateTime(),
                        ExpirationDate = c.DateTime(),
                        IsIndefinitely = c.Boolean(),
                        StatementId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_StatementMedicalDeviceComplectation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(maxLength: 50),
                        Name = c.String(maxLength: 255),
                        Identifier = c.String(maxLength: 50),
                        Model = c.String(maxLength: 255),
                        Manufacturer = c.String(maxLength: 255),
                        Country = c.String(maxLength: 255),
                        StatementId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_StatementMedicalDevicePackage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Kind = c.String(maxLength: 50),
                        Name = c.String(maxLength: 255),
                        VolumeValue = c.String(maxLength: 50),
                        VolumeUnit = c.String(maxLength: 50),
                        Count = c.Int(),
                        Description = c.String(maxLength: 255),
                        StatementId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EMP_StatementStorageLife",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Kind = c.String(maxLength: 50),
                        ExpirationDate = c.DateTime(),
                        Measure = c.String(maxLength: 50),
                        IsIndefinitely = c.Boolean(),
                        StatementId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeePermissionRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Guid(nullable: false),
                        PermissionRoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeePermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Guid(nullable: false),
                        PermissionKey = c.String(maxLength: 4000),
                        PermissionValue = c.String(maxLength: 4000),
                        GroupName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_ActivityTaskActualView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeId = c.Guid(nullable: false),
                        Text = c.String(nullable: false, maxLength: 4000),
                        StatusId = c.Guid(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                        DocumentTypeId = c.Guid(nullable: false),
                        TypeValue = c.String(maxLength: 4000),
                        StatusValue = c.String(maxLength: 4000),
                        AuthorValue = c.String(maxLength: 4000),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        ExecutedDate = c.DateTime(),
                        ClosedDate = c.DateTime(),
                        DocumentValue = c.String(maxLength: 4000),
                        DocumentTypeValue = c.String(maxLength: 4000),
                        TaskId = c.Guid(),
                        TaskTypeId = c.Guid(),
                        TaskTypeValue = c.String(maxLength: 4000),
                        TaskStatusId = c.Guid(),
                        TaskStatusValue = c.String(maxLength: 4000),
                        TaskOrderNumber = c.Int(),
                        TaskNumber = c.String(maxLength: 256),
                        TaskText = c.String(maxLength: 4000),
                        TaskCreatedDate = c.DateTime(),
                        TaskModifiedDate = c.DateTime(),
                        TaskExecutedDate = c.DateTime(),
                        TaskClosedDate = c.DateTime(),
                        TaskAuthorId = c.Guid(),
                        TaskAuthorValue = c.String(maxLength: 4000),
                        TaskExecutorId = c.Guid(),
                        TaskExecutorValue = c.String(maxLength: 4000),
                        TaskActivityId = c.Guid(),
                        TaskParentTaskId = c.Guid(),
                        TaskComment = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.TypeId, t.Text, t.StatusId, t.AuthorId, t.DocumentId, t.DocumentTypeId });
            
            CreateTable(
                "dbo.EXP_CertificateOfCompletionView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(maxLength: 500),
                        DicStageId = c.Int(),
                        DrugDeclarationId = c.Guid(),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        StatusId = c.Guid(),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                        SendDate = c.DateTime(),
                        CreateEmployeeId = c.Guid(),
                        TradeNameRu = c.String(maxLength: 500),
                        TradeNameKz = c.String(maxLength: 500),
                        TradeNameEn = c.String(maxLength: 500),
                        ManufacturerNameRu = c.String(maxLength: 500),
                        ManufacturerNameKz = c.String(maxLength: 500),
                        ManufacturerNameEn = c.String(maxLength: 500),
                        CountryName = c.String(maxLength: 4000),
                        CountryNameKz = c.String(maxLength: 4000),
                        StatusStr = c.String(maxLength: 4000),
                        StatusStrKz = c.String(maxLength: 4000),
                        StatusCode = c.String(maxLength: 4000),
                        CreateEmployeeStr = c.String(maxLength: 4000),
                        Stage = c.String(maxLength: 2000),
                        StageKz = c.String(maxLength: 2000),
                        TaskExecutorValue = c.String(maxLength: 4000),
                        TaskExecutorId = c.Guid(),
                        TaskExecutedDate = c.DateTime(),
                        TaskComment = c.String(maxLength: 4000),
                        ActNumber1C = c.String(maxLength: 500),
                        ActDate1C = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_DIC_PrimaryOTD",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Code = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 2000),
                        NameKz = c.String(maxLength: 2000),
                        ParentId = c.Int(),
                        DateCreate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DateEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EXP_DIC_PrimaryOTD", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.EXP_DirectionToPaysView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(nullable: false, maxLength: 512),
                        CreateDate = c.DateTime(nullable: false),
                        DirectionDate = c.DateTime(nullable: false),
                        DrugFormEn = c.String(nullable: false, maxLength: 1, unicode: false),
                        ManufactureKz = c.String(nullable: false, maxLength: 1, unicode: false),
                        ManufactureRu = c.String(nullable: false, maxLength: 1, unicode: false),
                        ManufactureEn = c.String(nullable: false, maxLength: 1, unicode: false),
                        ExpertDisplayName = c.String(nullable: false, maxLength: 1, unicode: false),
                        Currency = c.String(nullable: false, maxLength: 1, unicode: false),
                        PriceListValue = c.String(nullable: false, maxLength: 1, unicode: false),
                        StatusId = c.Guid(nullable: false),
                        CreateEmployeeId = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        ApplicationNumbers = c.String(),
                        ApplicationDates = c.String(),
                        RegistrationForm = c.String(),
                        TradeNameKz = c.String(),
                        TradeNameRu = c.String(),
                        TradeNameEn = c.String(),
                        DrugFormKz = c.String(),
                        DrugFormRu = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        PriceListNameKz = c.String(),
                        PriceListNameRu = c.String(),
                        PriceListNameEn = c.String(),
                        InvoiceNumber1C = c.String(maxLength: 500),
                        InvoiceDatetime1C = c.DateTime(),
                        PaymentDatetime1C = c.DateTime(),
                        PaymentValue1C = c.Decimal(precision: 18, scale: 2),
                        PaymentComment1C = c.String(),
                        StatusCode = c.String(maxLength: 4000),
                        StatusName = c.String(maxLength: 4000),
                        StatusNameKz = c.String(maxLength: 4000),
                        ExecutorId = c.Guid(),
                        ExecutorName = c.String(maxLength: 4000),
                        Comment = c.String(maxLength: 4000),
                        PageCount = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.Number, t.CreateDate, t.DirectionDate, t.DrugFormEn, t.ManufactureKz, t.ManufactureRu, t.ManufactureEn, t.ExpertDisplayName, t.Currency, t.PriceListValue, t.StatusId, t.CreateEmployeeId, t.Type });
            
            CreateTable(
                "dbo.Exp_DrugDeclarationChangeView",
                c => new
                    {
                        DrugDeclarationId = c.Guid(nullable: false),
                        ChangeString = c.String(),
                    })
                .PrimaryKey(t => t.DrugDeclarationId);
            
            CreateTable(
                "dbo.EXP_DrugDeclarationDirectionToPayView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        TypeId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        Number = c.String(maxLength: 500),
                        TypeNameRu = c.String(maxLength: 2000),
                        TypeNameKz = c.String(maxLength: 2000),
                        NameRu = c.String(maxLength: 500),
                        NameKz = c.String(maxLength: 500),
                        NameEn = c.String(maxLength: 500),
                        DrugFormRu = c.String(maxLength: 4000),
                        DrugFormKz = c.String(maxLength: 4000),
                        StausName = c.String(maxLength: 2000),
                        ContractId = c.Guid(),
                        ExecuterId = c.Guid(),
                        DirectionToPayId = c.Guid(),
                    })
                .PrimaryKey(t => new { t.Id, t.CreatedDate, t.TypeId, t.StatusId, t.OwnerId });
            
            CreateTable(
                "dbo.EXP_DrugDeclarationPaysInfoView",
                c => new
                    {
                        DrugDeclarationId = c.Guid(nullable: false),
                        PaysDates = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.DrugDeclarationId);
            
            CreateTable(
                "dbo.EXP_DrugDeclarationRegisterView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        StageId = c.Guid(nullable: false),
                        StatusCode = c.String(nullable: false, maxLength: 50),
                        ExpertId = c.Guid(nullable: false),
                        DicStageId = c.Int(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        Number = c.String(maxLength: 500),
                        TypeNameRu = c.String(maxLength: 2000),
                        TypeNameKz = c.String(maxLength: 2000),
                        NameRu = c.String(maxLength: 500),
                        NameKz = c.String(maxLength: 500),
                        NameEn = c.String(maxLength: 500),
                        FirstSendDate = c.DateTime(),
                        ProducerRu = c.String(maxLength: 500),
                        ProducerKz = c.String(maxLength: 500),
                        ProducerEn = c.String(maxLength: 500),
                        PackerRu = c.String(maxLength: 500),
                        PackerKz = c.String(maxLength: 500),
                        PackerEn = c.String(maxLength: 500),
                        ReleaseControlRu = c.String(maxLength: 500),
                        ReleaseControlKz = c.String(maxLength: 500),
                        ReleaseControlEn = c.String(maxLength: 500),
                        CountryId = c.Guid(),
                        CountryRu = c.String(maxLength: 4000),
                        CountryKz = c.String(maxLength: 4000),
                        StatusRu = c.String(maxLength: 2000),
                        StatusKz = c.String(maxLength: 2000),
                        DrugTypeRu = c.String(),
                        DrugTypeKz = c.String(),
                        MnnId = c.Int(),
                        MnnRu = c.String(maxLength: 255, unicode: false),
                        MnnKz = c.String(maxLength: 510),
                        MnnEn = c.String(maxLength: 255, unicode: false),
                        DrugFormRu = c.String(maxLength: 500, unicode: false),
                        DrugFormKz = c.String(maxLength: 1000),
                        DosageRu = c.String(),
                        DosageKz = c.String(),
                        StageRu = c.String(maxLength: 2000),
                        StageKz = c.String(maxLength: 2000),
                        ExpertInitials = c.String(maxLength: 4000),
                        Suspended = c.Boolean(),
                        OnBoard = c.Boolean(),
                        ProductionEvaluation = c.Boolean(),
                        StageStartDate = c.DateTime(),
                        StageControlDate = c.DateTime(),
                        SuspensionPeriod = c.Int(),
                        ConclusionDate = c.DateTime(),
                        StageCompleted = c.Boolean(),
                        StageEndDate = c.DateTime(),
                        StageOverdue = c.Boolean(),
                        OverdueDays = c.Int(),
                        LetterId = c.Guid(),
                        LetterNumber = c.String(maxLength: 250),
                        StageDays = c.Int(),
                        IsNewProducer = c.Boolean(),
                        ActiveSubstanceRu = c.String(),
                        ActiveSubstanceKz = c.String(),
                        SecondarySubstanceRu = c.String(),
                        SecondarySubstanceKz = c.String(),
                        CountDosageIsControl = c.Int(),
                        ApplicantRu = c.String(maxLength: 500),
                        ApplicantKz = c.String(maxLength: 500),
                        ApplicantEn = c.String(maxLength: 500),
                        ApplicantCountryRu = c.String(maxLength: 4000),
                        ApplicantCountryKz = c.String(maxLength: 4000),
                        HolderRu = c.String(maxLength: 500),
                        HolderKz = c.String(maxLength: 500),
                        HolderEn = c.String(maxLength: 500),
                        HolderCountryRu = c.String(maxLength: 4000),
                        HolderCountryKz = c.String(maxLength: 4000),
                        StageCode = c.String(maxLength: 50),
                        StageResultRu = c.String(maxLength: 2000),
                        StageResultKz = c.String(maxLength: 2000),
                        SuspendedStartDate = c.DateTime(),
                        Paid = c.Boolean(),
                        PaymentDate = c.DateTime(),
                        PaymentOverdue = c.Boolean(),
                        PaymentNumber = c.String(maxLength: 512),
                        InvoiceNumber1C = c.String(maxLength: 512),
                        Unlimited = c.Boolean(),
                        Experts = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.TypeId, t.StatusId, t.StageId, t.StatusCode, t.ExpertId, t.DicStageId, t.IsNew });
            
            CreateTable(
                "dbo.EXP_DrugDeclarationView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        TypeName = c.String(maxLength: 2000),
                        Number = c.String(maxLength: 500),
                        StausName = c.String(maxLength: 2000),
                        NameRu = c.String(maxLength: 2000),
                        SendDate = c.DateTime(),
                        ExecuterId = c.Guid(),
                        ExecuterName = c.String(maxLength: 4000),
                        CountDosageIsControl = c.Int(),
                        SortDate = c.DateTime(),
                        DrugNameRu = c.String(maxLength: 500),
                        DrugNameKz = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => new { t.Id, t.TypeId, t.CreatedDate, t.StatusId, t.OwnerId });
            
            CreateTable(
                "dbo.Exp_DrugDosageStageForAddView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DosageId = c.Long(nullable: false),
                        DosageStageId = c.Guid(nullable: false),
                        DeclarationId = c.Guid(nullable: false),
                        DeclarationCreatedDate = c.DateTime(nullable: false),
                        StageId = c.Int(nullable: false),
                        DosageRegNumber = c.String(maxLength: 50),
                        DeclarationNumber = c.String(maxLength: 500),
                        DeclarationNameRu = c.String(maxLength: 500),
                        StageNameRu = c.String(maxLength: 2000),
                        ResultId = c.Int(),
                        ResultNameRu = c.String(maxLength: 2000),
                        FinalDocStatusId = c.Guid(),
                        FinalDocStatusCode = c.String(maxLength: 4000),
                        FinalDocStatusDisplayName = c.String(maxLength: 4000),
                        ProducerNameRu = c.String(maxLength: 500),
                        ProducerCountryId = c.Guid(),
                        ProducerDocDate = c.DateTime(storeType: "date"),
                        ProducerDocExpiryDate = c.DateTime(storeType: "date"),
                        ProducerCountryName = c.String(maxLength: 4000),
                        DosageFormId = c.Int(),
                        DosageFormName = c.String(maxLength: 500, unicode: false),
                        DeclarationPaysDates = c.String(unicode: false),
                        DeclarationAtxId = c.Int(),
                        DeclarationAtxName = c.String(maxLength: 500, unicode: false),
                        DeclarationAtxCode = c.String(maxLength: 10, unicode: false),
                        DeclarationMnnId = c.Int(),
                        DeclarationMnnNameRu = c.String(maxLength: 255, unicode: false),
                        DeclarationSaleTypeId = c.Int(),
                        DeclarationSaleTypeName = c.String(maxLength: 2000),
                        NtdId = c.Int(),
                        NtdNameRu = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => new { t.Id, t.DosageId, t.DosageStageId, t.DeclarationId, t.DeclarationCreatedDate, t.StageId });
            
            CreateTable(
                "dbo.Exp_DrugDosageStagePrevCommissionConcatinateView",
                c => new
                    {
                        DosageStageId = c.Guid(nullable: false),
                        PrevCommissions = c.String(),
                    })
                .PrimaryKey(t => t.DosageStageId);
            
            CreateTable(
                "dbo.Exp_DrugDosageStagePrevCommissionView",
                c => new
                    {
                        DrugDosageId = c.Long(nullable: false),
                        StageId = c.Guid(nullable: false),
                        CommissionId = c.Int(nullable: false),
                        DrevComId = c.Int(nullable: false),
                        DrevComFullNumber = c.String(nullable: false, maxLength: 100),
                        PrevComDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.DrugDosageId, t.StageId, t.CommissionId, t.DrevComId, t.DrevComFullNumber, t.PrevComDate });
            
            CreateTable(
                "dbo.Exp_DrugDosageStageView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DosageId = c.Long(nullable: false),
                        DosageStageId = c.Guid(nullable: false),
                        DosageDosageValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeclarationId = c.Guid(nullable: false),
                        DeclarationCreatedDate = c.DateTime(nullable: false),
                        DeclarationTypeId = c.Int(nullable: false),
                        StageId = c.Int(nullable: false),
                        CommissionId = c.Int(nullable: false),
                        CommissionTypeId = c.Int(nullable: false),
                        PrevCommissionCount = c.Int(nullable: false),
                        DosageStageStartDate = c.DateTime(),
                        DosageStageEndDate = c.DateTime(),
                        DosageRegNumber = c.String(maxLength: 50),
                        DosageBestBefore = c.String(maxLength: 500),
                        DosageSaleTypeId = c.Int(),
                        DosageSaleTypeName = c.String(maxLength: 2000),
                        DosageDosageName = c.String(maxLength: 255, unicode: false),
                        DosageDosageShortName = c.String(maxLength: 250, unicode: false),
                        DosageBestBeforeName = c.String(maxLength: 255, unicode: false),
                        DosageBestBeforeShortName = c.String(maxLength: 250, unicode: false),
                        DeclarationNumber = c.String(maxLength: 500),
                        DeclarationNameRu = c.String(maxLength: 500),
                        DeclarationTypeNameRu = c.String(maxLength: 2000),
                        StageNameRu = c.String(maxLength: 2000),
                        ResultId = c.Int(),
                        ResultNameRu = c.String(maxLength: 2000),
                        ResultDate = c.DateTime(),
                        ResultCreatorId = c.Guid(),
                        ResultCreatorLastName = c.String(maxLength: 4000),
                        ResultCreatorFirstName = c.String(maxLength: 4000),
                        ResultCreatorMiddleName = c.String(maxLength: 4000),
                        ResultCreatorShortName = c.String(maxLength: 4000),
                        FinalDocStatusId = c.Guid(),
                        FinalDocStatusCode = c.String(maxLength: 4000),
                        FinalDocStatusDisplayName = c.String(maxLength: 4000),
                        CommissionConclusionTypeId = c.Int(),
                        CommissionConclusionComment = c.String(),
                        CommissionConclusionTypeCode = c.Int(),
                        ProducerNameRu = c.String(maxLength: 500),
                        ProducerCountryId = c.Guid(),
                        ProducerDocDate = c.DateTime(storeType: "date"),
                        ProducerDocExpiryDate = c.DateTime(storeType: "date"),
                        ProducerCountryName = c.String(maxLength: 4000),
                        DosageFormId = c.Int(),
                        DosageFormName = c.String(maxLength: 500, unicode: false),
                        DeclarationPaysDates = c.String(unicode: false),
                        DeclarationAtxId = c.Int(),
                        DeclarationAtxName = c.String(maxLength: 500, unicode: false),
                        DeclarationAtxCode = c.String(maxLength: 10, unicode: false),
                        DeclarationMnnId = c.Int(),
                        DeclarationMnnNameRu = c.String(maxLength: 255, unicode: false),
                        NtdId = c.Int(),
                        NtdNameRu = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => new { t.Id, t.DosageId, t.DosageStageId, t.DosageDosageValue, t.DeclarationId, t.DeclarationCreatedDate, t.DeclarationTypeId, t.StageId, t.CommissionId, t.CommissionTypeId, t.PrevCommissionCount });
            
            CreateTable(
                "dbo.EXP_DrugDosageView",
                c => new
                    {
                        DrugDeclarationId = c.Guid(nullable: false),
                        DosageRu = c.String(),
                        DosageKz = c.String(),
                    })
                .PrimaryKey(t => t.DrugDeclarationId);
            
            CreateTable(
                "dbo.EXP_DrugSubstanceView",
                c => new
                    {
                        DrugDeclarationId = c.Guid(nullable: false),
                        ActiveSubstanceRu = c.String(),
                        ActiveSubstanceKz = c.String(),
                        SecondarySubstanceRu = c.String(),
                        SecondarySubstanceKz = c.String(),
                    })
                .PrimaryKey(t => t.DrugDeclarationId);
            
            CreateTable(
                "dbo.EXP_DrugTypeView",
                c => new
                    {
                        DrugDeclarationId = c.Guid(nullable: false),
                        DrugTypeRu = c.String(),
                        DrugTypeKz = c.String(),
                    })
                .PrimaryKey(t => t.DrugDeclarationId);
            
            CreateTable(
                "dbo.EXP_ExpertiseStageDosageCommissionView",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FullNumber = c.String(nullable: false, maxLength: 100),
                        Date = c.DateTime(nullable: false),
                        IsComplete = c.Boolean(nullable: false),
                        CommisionType = c.String(nullable: false, maxLength: 300),
                        StageId = c.Guid(nullable: false),
                        DrugDosageId = c.Long(nullable: false),
                        ConclusionName = c.String(maxLength: 200),
                        ConclusionComment = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.FullNumber, t.Date, t.IsComplete, t.CommisionType, t.StageId, t.DrugDosageId });
            
            CreateTable(
                "dbo.EXP_MaterialDirectionsView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DrugDeclarationId = c.Guid(nullable: false),
                        StatusId = c.Int(nullable: false),
                        RegisteredDate = c.DateTime(nullable: false),
                        Dosage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        Number = c.String(maxLength: 128),
                        StatusCode = c.String(maxLength: 4000),
                        StatusStr = c.String(maxLength: 4000),
                        StatusKzStr = c.String(maxLength: 4000),
                        SendEmployeeId = c.Guid(),
                        SendDate = c.DateTime(),
                        ExecutorEmployeeId = c.Guid(),
                        ReceiveDate = c.DateTime(),
                        RejectDate = c.DateTime(),
                        Comment = c.String(),
                        DdNumber = c.String(maxLength: 500),
                        RegistrationTypeRu = c.String(maxLength: 2000),
                        RegistrationTypeKz = c.String(maxLength: 2000),
                        TradeNameRu = c.String(maxLength: 500),
                        TradeNameKz = c.String(maxLength: 500),
                        TradeNameEn = c.String(maxLength: 500),
                        DosageMeasureTypeId = c.Long(),
                        DosageMeasureTypeName = c.String(maxLength: 255, unicode: false),
                        ConcentrationRu = c.String(maxLength: 500),
                        ConcentrationKz = c.String(maxLength: 500),
                        DrugFormName = c.String(maxLength: 500, unicode: false),
                        DrugFormNameKz = c.String(maxLength: 1000),
                        DrugTypeNameRu = c.String(maxLength: 2000),
                        DrugTypeNameKz = c.String(maxLength: 2000),
                        ProducerNameRu = c.String(maxLength: 500),
                        ProducerNameKz = c.String(maxLength: 500),
                        ProducerNameEn = c.String(maxLength: 500),
                        CountryName = c.String(maxLength: 4000),
                        CountryNameKz = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.DrugDeclarationId, t.StatusId, t.RegisteredDate, t.Dosage });
            
            CreateTable(
                "dbo.EXP_PriceListDirectionToPayView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(maxLength: 50),
                        NameRu = c.String(maxLength: 450),
                        NameKz = c.String(maxLength: 450),
                        NameEn = c.String(maxLength: 450),
                        PriceRegisterForeign = c.Decimal(precision: 18, scale: 2),
                        PriceRegisterForeignNds = c.Decimal(precision: 18, scale: 2),
                        PriceReRegisterForeign = c.Decimal(precision: 18, scale: 2),
                        PriceReRegisterForeignNds = c.Decimal(precision: 18, scale: 2),
                        PriceRegisterKz = c.Decimal(precision: 18, scale: 2),
                        PriceRegisterKzNds = c.Decimal(precision: 18, scale: 2),
                        PriceReRegisterKz = c.Decimal(precision: 18, scale: 2),
                        PriceReRegisterKzNds = c.Decimal(precision: 18, scale: 2),
                        Category = c.String(maxLength: 100),
                        DirectionToPayId = c.Guid(),
                        Count = c.Int(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Total = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_PriceListDrugTypeMapping",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DrugTypeCode = c.String(maxLength: 50),
                        PriceListCode = c.String(maxLength: 50),
                        PriceListMulticomponentCode = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXP_TaskRegistryView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ActivityId = c.Guid(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                        StatusId = c.Guid(nullable: false),
                        TypeId = c.Guid(nullable: false),
                        DocumentTypeId = c.Guid(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                        ExecutorId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(),
                        DocNumber = c.String(maxLength: 255),
                        DocDate = c.DateTime(),
                        StatusRu = c.String(maxLength: 4000),
                        StatusKz = c.String(maxLength: 4000),
                        StatusCode = c.String(maxLength: 4000),
                        TaskTypeCode = c.String(maxLength: 4000),
                        DocumentTypeRu = c.String(maxLength: 4000),
                        DocumentTypeKz = c.String(maxLength: 4000),
                        DocumentTypeCode = c.String(maxLength: 4000),
                        AuthorName = c.String(maxLength: 4000),
                        ActivityTypeCode = c.String(maxLength: 4000),
                        ApproverNames = c.String(),
                        SignerNames = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.ActivityId, t.DocumentId, t.StatusId, t.TypeId, t.DocumentTypeId, t.AuthorId, t.ExecutorId });
            
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EmployeeId = c.String(maxLength: 900),
                        EmployeeValue = c.String(maxLength: 900),
                        DateStart = c.DateTime(storeType: "date"),
                        DateEnd = c.DateTime(storeType: "date"),
                        Organization = c.String(maxLength: 4000),
                        Country = c.String(maxLength: 4000),
                        Position = c.String(maxLength: 4000),
                        Note = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExtensionExecutions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ExecutionDate = c.DateTime(nullable: false, storeType: "date"),
                        Text = c.String(maxLength: 2048),
                        DocumentId = c.Guid(nullable: false),
                        AutorId = c.String(maxLength: 900),
                        AutorValue = c.String(maxLength: 900),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileLinkView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 4000),
                        Version = c.Int(),
                        DocumentId = c.Guid(),
                        CategoryId = c.Guid(),
                        ParentId = c.Guid(),
                        Category = c.String(maxLength: 4000),
                        ParentFileName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.CreateDate, t.FileName });
            
            CreateTable(
                "dbo.History",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Guid(nullable: false),
                        OperationId = c.String(maxLength: 4000),
                        TableName = c.String(maxLength: 4000),
                        ColumnName = c.String(maxLength: 4000),
                        ObjectId = c.Guid(nullable: false),
                        Record = c.String(maxLength: 4000),
                        OldValue = c.String(maxLength: 4000),
                        NewValue = c.String(maxLength: 4000),
                        CreatedTime = c.DateTime(nullable: false),
                        UserName = c.String(maxLength: 4000),
                        Ip = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Holidays",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeId = c.String(maxLength: 900),
                        EmployeeValue = c.String(maxLength: 900),
                        HolidayTypeDictionaryId = c.String(maxLength: 900),
                        HolidayTypeDictionaryValue = c.String(maxLength: 900),
                        PeriodStart = c.DateTime(storeType: "date"),
                        PeriodEnd = c.DateTime(storeType: "date"),
                        DateStart = c.DateTime(storeType: "date"),
                        DateEnd = c.DateTime(storeType: "date"),
                        DocumentId = c.String(maxLength: 900),
                        DocumentValue = c.String(maxLength: 900),
                        Note = c.String(maxLength: 900),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_act_Application",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(maxLength: 500),
                        CreateDatetime = c.DateTime(),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        ActNumber1C = c.String(maxLength: 500),
                        ActDate1C = c.DateTime(),
                        ExportDatetime = c.DateTime(nullable: false),
                        ImportDatetime = c.DateTime(),
                        StageId = c.Int(),
                        StageCode = c.String(maxLength: 500),
                        StageValue = c.String(maxLength: 500),
                        refPrimaryApplication = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_act_ObkApplication",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(maxLength: 500),
                        CreateDatetime = c.DateTime(),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        InvoiceNumber1C = c.String(maxLength: 512),
                        InvoiceDatetime1C = c.DateTime(),
                        ExportDatetime = c.DateTime(nullable: false),
                        StageId = c.Int(),
                        StageCode = c.String(maxLength: 500),
                        StageValue = c.String(maxLength: 500),
                        ActNumber1C = c.String(maxLength: 500),
                        ActDate1C = c.DateTime(),
                        ActReturnedBack = c.Boolean(),
                        ZBKCopyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_exp_Contracts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContractNumber = c.String(nullable: false, maxLength: 500),
                        ContractDate = c.DateTime(nullable: false),
                        ContractUrl = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_lims_ApplicationEAdmissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TmcCode = c.Guid(nullable: false),
                        TmcNumber = c.String(maxLength: 450),
                        QuntityVolume = c.Decimal(precision: 18, scale: 6),
                        QuntityVolumeStr = c.String(maxLength: 450),
                        Unit = c.String(maxLength: 450),
                        UnitCode = c.Guid(),
                        Producer = c.String(maxLength: 450),
                        ProducerExpirationDate = c.DateTime(storeType: "date"),
                        PartyNumber = c.String(maxLength: 450),
                        ShelfNumber = c.String(maxLength: 450),
                        CupboardNumber = c.String(maxLength: 450),
                        WarehouseNumber = c.Guid(),
                        DateOfReceiving = c.DateTime(storeType: "date"),
                        Note = c.String(),
                        ExportDatetime = c.DateTime(),
                        ImportDatetime = c.DateTime(),
                        PowerOfAttorneyNumber_1C = c.String(maxLength: 450),
                        PowerOfAttorneyDatetime_1C = c.DateTime(),
                        QuntityVolumeConvert = c.Decimal(precision: 18, scale: 6),
                        UnitConvert = c.String(maxLength: 450),
                        UnitCodeConvert = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_lims_ApplicationEExploitation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TmcCode = c.Guid(nullable: false),
                        TmcNumber = c.String(maxLength: 450),
                        QuntityVolume = c.Decimal(precision: 18, scale: 6),
                        QuntityVolumeStr = c.String(maxLength: 450),
                        Unit = c.String(maxLength: 450),
                        UnitCode = c.Guid(),
                        PowerOfAttorneyId_1C = c.Guid(),
                        PowerOfAttorneyNumber_1C = c.String(maxLength: 450),
                        OrganizationId = c.Guid(),
                        OrganizationName = c.String(maxLength: 4000),
                        DepartmentId = c.Guid(),
                        DepartmentName = c.String(maxLength: 4000),
                        OwnerEmployeeId = c.Guid(),
                        OwnerEmployeeFio = c.String(maxLength: 450),
                        ExploitatioinDatetime = c.DateTime(),
                        Description = c.String(),
                        Reason = c.String(),
                        ExportDatetime = c.DateTime(),
                        ImportDatetime = c.DateTime(),
                        ExpertiseApplicationId = c.Guid(),
                        ExpertiseApplicationNumber = c.String(maxLength: 450),
                        QuntityVolumeConvert = c.Decimal(precision: 18, scale: 6),
                        UnitConvert = c.String(maxLength: 450),
                        OrganizationCode = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_lims_ApplicationElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TmcCode = c.Guid(nullable: false),
                        TmcNumber = c.String(maxLength: 450),
                        Name = c.String(),
                        QuntityVolume = c.Decimal(precision: 18, scale: 6),
                        QuntityVolumeStr = c.String(maxLength: 450),
                        Unit = c.String(maxLength: 450),
                        UnitCode = c.Guid(),
                        Kind = c.String(maxLength: 450),
                        KindCode = c.Guid(),
                        Paking = c.String(maxLength: 450),
                        PakingCode = c.Guid(),
                        ExportDatetime = c.DateTime(),
                        ImportDatetime = c.DateTime(),
                        refApplication = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_lims_Applications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.Guid(nullable: false),
                        ContractId = c.Guid(),
                        ContractNumber = c.String(maxLength: 450),
                        ContractDate = c.DateTime(storeType: "date"),
                        LastDeliveryDate = c.DateTime(storeType: "date"),
                        ProviderId = c.Guid(),
                        Provider = c.String(maxLength: 450),
                        ProviderBin = c.String(maxLength: 450),
                        FrpersonId = c.Guid(),
                        FrpersonFio = c.String(maxLength: 450),
                        OrganizationId = c.Guid(),
                        FullDelivery = c.Boolean(),
                        PowerOfAttorneyId_1C = c.Guid(),
                        PowerOfAttorneyNumber_1C = c.String(maxLength: 450),
                        PowerOfAttorneyDatetime_1C = c.DateTime(),
                        FilePath = c.String(),
                        Note = c.String(),
                        CreateDatetime = c.DateTime(),
                        ExportDatetime = c.DateTime(),
                        ImportDatetime = c.DateTime(),
                        OrganizationCode = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_lims_ContractProducts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.String(maxLength: 50),
                        Name = c.String(),
                        Unit = c.String(maxLength: 500),
                        QuantityVolume = c.Decimal(precision: 18, scale: 6),
                        ContractNumber = c.String(maxLength: 500),
                        ExportDatetime = c.DateTime(),
                        ImportDatetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_lims_Contracts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BIN = c.String(maxLength: 50),
                        Name = c.String(),
                        ContractNumber = c.String(maxLength: 500),
                        ContractDate = c.DateTime(),
                        ContractDeliveryLastDate = c.DateTime(),
                        ExportDatetime = c.DateTime(),
                        ImportDatetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_primary_ApplicationElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Dosage = c.Decimal(precision: 18, scale: 2),
                        MeasureName = c.String(maxLength: 500),
                        RegNumber = c.String(maxLength: 50),
                        ConcentrationRu = c.String(maxLength: 500),
                        ConcentrationKz = c.String(maxLength: 500),
                        refApplication = c.Guid(nullable: false),
                        DosageId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_primary_ApplicationPartPaymentState",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationNumber = c.String(maxLength: 500),
                        ApplicationDatetime = c.DateTime(),
                        PaymentDatetime = c.DateTime(),
                        PaymentComment = c.String(),
                        PaymentValue = c.Decimal(precision: 18, scale: 2),
                        PaymentSurcharge = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_primary_ApplicationPaymentState",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationNumber = c.String(maxLength: 500),
                        ApplicationDatetime = c.DateTime(),
                        IsPaid = c.Boolean(),
                        PaymentDatetime = c.DateTime(),
                        PaymentComment = c.String(),
                        PaymentValue = c.Decimal(precision: 18, scale: 2),
                        PaymentBill = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_primary_ApplicationRefuseState",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationNumber = c.String(maxLength: 500),
                        ApplicationDatetime = c.DateTime(),
                        IsRefuse = c.Boolean(),
                        RefuseDatetime = c.DateTime(),
                        refApplication = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_primary_Applications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ExportDatetime = c.DateTime(nullable: false),
                        ImportDatetime = c.DateTime(),
                        ApplicationId = c.Guid(),
                        ApplicationNumber = c.String(maxLength: 450),
                        ApplicationDatetime = c.DateTime(),
                        ApplicationType = c.String(maxLength: 500),
                        Producer = c.String(maxLength: 4000),
                        ProducerId = c.Guid(),
                        Applicant = c.String(maxLength: 4000),
                        ApplicantId = c.Guid(),
                        ContractNumber = c.String(maxLength: 450),
                        ContractStartDate = c.DateTime(storeType: "date"),
                        ContractEndDate = c.DateTime(storeType: "date"),
                        ContractId = c.Guid(),
                        DoverennostNumber = c.String(maxLength: 500),
                        DoverennostCreatedDate = c.DateTime(storeType: "date"),
                        DoverennostExpiryDate = c.DateTime(storeType: "date"),
                        Address = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 500),
                        Payer = c.String(maxLength: 4000),
                        PayerId = c.Guid(),
                        PayerAddress = c.String(maxLength: 4000),
                        PayerBank = c.String(maxLength: 4000),
                        PayerAccount = c.String(maxLength: 500),
                        PayerBIK = c.String(maxLength: 500),
                        PayerSWIFT = c.String(maxLength: 500),
                        PayerBIN = c.String(maxLength: 500),
                        PayerIIN = c.String(maxLength: 500),
                        PayerCurrency = c.String(maxLength: 500),
                        PayerCurrencyCode = c.String(maxLength: 500),
                        PayerCurrencyId = c.Guid(),
                        Country = c.String(maxLength: 500),
                        CountryId = c.Guid(),
                        IsResident = c.Boolean(),
                        IsLegal = c.Boolean(),
                        InvoiceNumber1C = c.String(maxLength: 500),
                        InvoiceDatetime1C = c.DateTime(),
                        StatementNumber = c.String(maxLength: 500),
                        DrugTradeName = c.String(maxLength: 500),
                        DrugTradeNameKz = c.String(maxLength: 500),
                        DrugPackage = c.String(maxLength: 500),
                        DrugPackageKz = c.String(maxLength: 500),
                        TypeId = c.Int(),
                        Type = c.String(maxLength: 500),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        ManufactureIsResident = c.Boolean(),
                        DrugFormName = c.String(),
                        DrugFormNameKz = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_primary_DrugPackage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        WrappingTypeId = c.Int(),
                        WrappingTypeNameRu = c.String(),
                        WrappingTypeNameKz = c.String(),
                        WrappingKindId = c.Int(),
                        WrappingKindNameRu = c.String(),
                        WrappingKindNameKz = c.String(),
                        WrappingSize = c.Double(),
                        SizeMeasureId = c.Long(),
                        SizeMeasureNameRu = c.String(),
                        SizeMeasureNameKz = c.String(),
                        WrappingVolume = c.Double(),
                        VolumeMeasureId = c.Long(),
                        VolumeMeasureNameRu = c.String(),
                        VolumeMeasureNameKz = c.String(),
                        CountUnit = c.Int(),
                        Note = c.String(maxLength: 500),
                        WrappingSizeStr = c.String(maxLength: 500),
                        WrappingVolumeStr = c.String(maxLength: 500),
                        DosageId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_primary_ObkApplicationNotFullPaymentState",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsPaid = c.Boolean(),
                        PaymentDatetime = c.DateTime(),
                        PaymentValue = c.Decimal(precision: 18, scale: 2),
                        PaymentBill = c.Decimal(precision: 18, scale: 2),
                        refContractId = c.Guid(),
                        ZBKCopyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_primary_ObkApplicationPaymentState",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsPaid = c.Boolean(),
                        PaymentDatetime = c.DateTime(),
                        refContractId = c.Guid(),
                        ZBKCopyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_primary_ObkApplications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Organization = c.String(maxLength: 450),
                        OrganizationCode = c.String(maxLength: 450),
                        ExportDatetime = c.DateTime(nullable: false),
                        ContractNumber = c.String(maxLength: 450),
                        ContractStartDate = c.DateTime(storeType: "date"),
                        ContractEndDate = c.DateTime(storeType: "date"),
                        ContractId = c.Guid(),
                        DoverennostNumber = c.String(maxLength: 500),
                        DoverennostCreatedDate = c.DateTime(storeType: "date"),
                        DoverennostExpiryDate = c.DateTime(storeType: "date"),
                        Payer = c.String(maxLength: 4000),
                        PayerId = c.Guid(),
                        PayerAddress = c.String(maxLength: 4000),
                        PayerPhone = c.String(maxLength: 500),
                        PayerBank = c.String(maxLength: 4000),
                        PayerAccount = c.String(maxLength: 500),
                        PayerBIK = c.String(maxLength: 500),
                        PayerSWIFT = c.String(maxLength: 500),
                        PayerBIN = c.String(maxLength: 500),
                        PayerIIN = c.String(maxLength: 500),
                        PayerCurrency = c.String(maxLength: 500),
                        PayerCurrencyId = c.String(maxLength: 500),
                        Country = c.String(maxLength: 500),
                        CountryId = c.Guid(),
                        IsResident = c.Boolean(),
                        IsLegal = c.Boolean(),
                        Type = c.String(maxLength: 500),
                        TypeId = c.Int(),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        InvoiceNumber1C = c.String(maxLength: 512),
                        InvoiceDatetime1C = c.DateTime(),
                        ZBKCopyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_primary_ObkPriceListElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceListId = c.Guid(),
                        PriceListName = c.String(maxLength: 512),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        DrugTradeName = c.String(maxLength: 512),
                        DrugTradeNameKz = c.String(maxLength: 512),
                        DrugFormName = c.String(maxLength: 512),
                        DrugFormNameKz = c.String(maxLength: 512),
                        refContractId = c.Guid(),
                        ZBKCopyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_primary_PriceListElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PriceListId = c.Guid(),
                        PriceListName = c.String(maxLength: 500),
                        PriceListNameKz = c.String(maxLength: 500),
                        Quantity = c.Int(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        refApplication = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_trl_Application",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                        ApplicationNumber = c.String(maxLength: 4000),
                        ApplicationDatetime = c.DateTime(),
                        TranslatorFIO = c.String(maxLength: 4000),
                        TranslatorId = c.Guid(),
                        PageQuantity = c.Int(),
                        PagePrice = c.Decimal(precision: 18, scale: 2),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        InvoiceCode_1C = c.String(maxLength: 512),
                        InvoiceNumber_1C = c.String(maxLength: 512),
                        InvoiceDatetime_1C = c.DateTime(),
                        ExportDatetime = c.DateTime(nullable: false),
                        ImportDatetime = c.DateTime(),
                        refPrimaryApplication = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_trl_ApplicationPaymentState",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationNumber = c.String(maxLength: 500),
                        ApplicationDatetime = c.DateTime(),
                        IsPaid = c.Boolean(),
                        PaymentDatetime = c.DateTime(),
                        PaymentComment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.I1c_trl_ApplicationRefuseState",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationNumber = c.String(maxLength: 500),
                        ApplicationDatetime = c.DateTime(),
                        IsRefuse = c.Boolean(),
                        RefuseDatetime = c.DateTime(),
                        refApplication = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IncomingDocument",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsAdministrativeUse = c.Boolean(nullable: false),
                        IsAwaitingResponse = c.Boolean(nullable: false),
                        IsTradeSecret = c.Boolean(nullable: false),
                        ApplicantType = c.Int(nullable: false),
                        DocumentType = c.Int(nullable: false),
                        MonitoringType = c.Int(nullable: false),
                        PriorityType = c.Int(nullable: false),
                        StateType = c.Int(nullable: false),
                        AppendixCount = c.Int(nullable: false),
                        CopiesCount = c.Int(nullable: false),
                        PageCount = c.Int(nullable: false),
                        RepeatCount = c.Int(nullable: false),
                        SortNumber = c.Int(nullable: false),
                        OutgoingType = c.Int(nullable: false),
                        IsNotification = c.Boolean(nullable: false),
                        NotificationCount = c.Int(nullable: false),
                        IsAttachments = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        AwaitingResponseDate = c.DateTime(),
                        DocumentDate = c.DateTime(),
                        OutgoingDate = c.DateTime(),
                        ProtocolDate = c.DateTime(),
                        MonitoringDate = c.DateTime(),
                        ApplicantAddress = c.String(maxLength: 512),
                        ApplicantEmail = c.String(maxLength: 4000),
                        ApplicantName = c.String(maxLength: 512),
                        ApplicantPhone = c.String(maxLength: 512),
                        BlankNumber = c.String(maxLength: 512),
                        CorrespondentsInfo = c.String(maxLength: 512),
                        Number = c.String(maxLength: 512),
                        OutgoingNumber = c.String(maxLength: 512),
                        SortingNumber = c.String(maxLength: 512),
                        SortingOutgoingNumber = c.String(maxLength: 512),
                        Note = c.String(maxLength: 4000),
                        Summary = c.String(maxLength: 4000),
                        AdministrativeTypeDictionaryValue = c.String(maxLength: 4000),
                        ApplicantCategoryDictionaryValue = c.String(maxLength: 4000),
                        CauseCitizenDictionaryValue = c.String(maxLength: 4000),
                        CitizenCategoryDictionaryValue = c.String(maxLength: 4000),
                        CitizenResultDictionaryValue = c.String(maxLength: 4000),
                        CitizenTypeDictionaryValue = c.String(maxLength: 4000),
                        DocumentKindDictionaryValue = c.String(maxLength: 4000),
                        FormDeliveryDictionaryValue = c.String(maxLength: 4000),
                        FormSendingDictionaryValue = c.String(maxLength: 4000),
                        KatoDictionaryValue = c.String(maxLength: 4000),
                        LanguageDictionaryValue = c.String(maxLength: 4000),
                        NomenclatureDictionaryValue = c.String(maxLength: 4000),
                        QuestionDesignDictionaryValue = c.String(maxLength: 4000),
                        SigningFormDictionaryValue = c.String(maxLength: 4000),
                        AgreementsValue = c.String(maxLength: 4000),
                        ExecutorsValue = c.String(maxLength: 4000),
                        ReadersValue = c.String(maxLength: 4000),
                        RecipientsValue = c.String(maxLength: 4000),
                        RegistratorValue = c.String(maxLength: 4000),
                        ResponsibleValue = c.String(maxLength: 4000),
                        SignerValue = c.String(maxLength: 4000),
                        CorrespondentsValue = c.String(maxLength: 4000),
                        MonitoringAuthorValue = c.String(maxLength: 4000),
                        MonitoringNote = c.String(maxLength: 4000),
                        AnswersValue = c.String(maxLength: 4000),
                        CompleteDocumentsValue = c.String(maxLength: 4000),
                        EditDocumentsValue = c.String(maxLength: 4000),
                        RepealDocumentsValue = c.String(maxLength: 4000),
                        DisplayName = c.String(maxLength: 4000),
                        AutoAnswersValue = c.String(maxLength: 4000),
                        AutoAnswersTempValue = c.String(maxLength: 4000),
                        AutoCompleteDocumentsValue = c.String(maxLength: 4000),
                        AutoEditDocumentsValue = c.String(maxLength: 4000),
                        AutoRepealDocumentsValue = c.String(maxLength: 4000),
                        AutoAwaitingResponseDate = c.DateTime(storeType: "date"),
                        AutoDocumentDate = c.DateTime(storeType: "date"),
                        AutoOutgoingDate = c.DateTime(storeType: "date"),
                        AutoProtocolDate = c.DateTime(storeType: "date"),
                        AutoMonitoringDate = c.DateTime(storeType: "date"),
                        FactExecutionDate = c.DateTime(),
                        FirstExecutionDate = c.DateTime(),
                        ExecutionDate = c.DateTime(),
                        AutoFactExecutionDate = c.DateTime(storeType: "date"),
                        AutoFirstExecutionDate = c.DateTime(storeType: "date"),
                        AutoExecutionDate = c.DateTime(storeType: "date"),
                        Counters = c.String(maxLength: 4000),
                        DocumentDictionaryTypeValue = c.String(maxLength: 4000),
                        ResolutionValue = c.String(maxLength: 4000),
                        SourceValue = c.String(maxLength: 4000),
                        DestinationValue = c.String(maxLength: 4000),
                        OwnerValue = c.String(maxLength: 4000),
                        Country = c.String(maxLength: 4000),
                        Area = c.String(maxLength: 4000),
                        Postcode = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 4000),
                        Department = c.String(maxLength: 4000),
                        City = c.String(maxLength: 4000),
                        Address = c.String(maxLength: 4000),
                        NumberBill = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        SuperMainDocumentId = c.Guid(),
                        ModifiedUser = c.String(maxLength: 4000),
                        DateDispatch = c.DateTime(),
                        DispatchNote = c.String(maxLength: 4000),
                        Digest = c.String(maxLength: 4000),
                        Text = c.String(maxLength: 4000),
                        Recipient = c.String(maxLength: 4000),
                        QrCode = c.Binary(storeType: "image"),
                        FulfilledDate = c.DateTime(),
                        State = c.String(maxLength: 510),
                        Monitoring = c.String(maxLength: 510),
                        Priority = c.String(maxLength: 510),
                    })
                .PrimaryKey(t => new { t.Id, t.IsDeleted, t.IsAdministrativeUse, t.IsAwaitingResponse, t.IsTradeSecret, t.ApplicantType, t.DocumentType, t.MonitoringType, t.PriorityType, t.StateType, t.AppendixCount, t.CopiesCount, t.PageCount, t.RepeatCount, t.SortNumber, t.OutgoingType, t.IsNotification, t.NotificationCount, t.IsAttachments, t.IsArchive });
            
            CreateTable(
                "dbo.Installation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 450),
                        Model = c.String(maxLength: 450),
                        ManufacturerFactory = c.String(maxLength: 450),
                        Country = c.String(maxLength: 450),
                        FactoryNumber = c.String(maxLength: 450),
                        InvertoryNumber = c.String(maxLength: 450),
                        LaboratoryRoom = c.String(maxLength: 450),
                        InstallationYear = c.String(maxLength: 450),
                        Note = c.String(maxLength: 450),
                        CheckDate = c.String(maxLength: 450),
                        Type = c.String(maxLength: 450),
                        Laboratory = c.String(maxLength: 450),
                        TypeIn = c.String(maxLength: 450),
                        BuhList = c.String(maxLength: 450),
                        State = c.String(maxLength: 450),
                        StateIn = c.String(maxLength: 450),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lims_ActivityView",
                c => new
                    {
                        ReportByEmployee = c.Int(nullable: false),
                        DisplayName = c.String(maxLength: 4000),
                        AppPoaNewCount = c.Int(),
                        AppPoaSendedTo1CCount = c.Int(),
                        AppPoaReceivedFrom1CCount = c.Int(),
                        TmcReceivedFrom1CCount = c.Int(),
                        AppRecCount = c.Int(),
                        AppRecIssuedCount = c.Int(),
                        AppRecUsedCount = c.Int(),
                        ReportByDepartment = c.Int(),
                        ReportByIc = c.Int(),
                    })
                .PrimaryKey(t => t.ReportByEmployee);
            
            CreateTable(
                "dbo.LimsApplicationJournalView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EquipmentId = c.Guid(),
                        ApplicationDate = c.DateTime(),
                        TypeOfMalfunction = c.String(),
                        ApplicantId = c.Guid(),
                        ApplicationSignDate = c.DateTime(),
                        EngineerId = c.Guid(),
                        EngineerSignDate = c.DateTime(),
                        AccepterId = c.Guid(),
                        AccepterSignDate = c.DateTime(),
                        Result = c.String(),
                        Note = c.String(),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        LimsEquipmentName = c.String(),
                        LimsEquipmentInventoryNumber = c.String(maxLength: 500),
                        LimsEquipmentSerialNumber = c.String(maxLength: 500),
                        LimsEquipmentLocationName = c.String(maxLength: 4000),
                        LimsEquipmentLaboratoryName = c.String(maxLength: 4000),
                        ApplicationEmpName = c.String(maxLength: 4000),
                        EngineerEmpName = c.String(maxLength: 4000),
                        AccepterEmpName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LimsEquipmentActSparePartsView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Quantity = c.Int(),
                        InventoryNumber = c.String(maxLength: 500),
                        LocationId = c.Guid(),
                        LocationName = c.String(maxLength: 4000),
                        LocationNameKz = c.String(maxLength: 4000),
                        EquipmentActId = c.Guid(),
                        CreatedDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.Id, t.Name });
            
            CreateTable(
                "dbo.LimsEquipmentActView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EquipmentId = c.Guid(nullable: false),
                        ActTypeId = c.Guid(nullable: false),
                        EquipmentName = c.String(),
                        SerialNumber = c.String(maxLength: 500),
                        InventoryNumber = c.String(maxLength: 500),
                        LaboratoryId = c.Guid(),
                        LaboratoryName = c.String(maxLength: 4000),
                        ActTypeCode = c.String(maxLength: 4000),
                        Reason = c.String(),
                        State = c.String(),
                        HeadOfLaboratoryId = c.Guid(),
                        HeadOfLaboratoryName = c.String(maxLength: 1000),
                        ResponsiblePersonId = c.Guid(),
                        ResponsiblePersonName = c.String(maxLength: 4000),
                        DirectorRCId = c.Guid(),
                        DirectorRCName = c.String(maxLength: 1000),
                        EngineerId = c.Guid(),
                        EngineerName = c.String(maxLength: 1000),
                        Quantity = c.Int(),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        ProducerName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.EquipmentId, t.ActTypeId });
            
            CreateTable(
                "dbo.LimsEquipmentJournalRecordView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EquipmentId = c.Guid(nullable: false),
                        JournalId = c.Guid(),
                        ActionDate = c.DateTime(),
                        NextActionDate = c.DateTime(),
                        ActionInfo = c.String(),
                        ActionResult = c.String(),
                        Note = c.String(),
                        ExecutorId = c.Guid(),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        EquipmentName = c.String(),
                        EquipmentModelName = c.String(maxLength: 4000),
                        EquipmentProducerName = c.String(maxLength: 4000),
                        EquipmentYearInstallation = c.Int(),
                        ExecutorName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.EquipmentId });
            
            CreateTable(
                "dbo.LimsEquipmentPlanView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlanTypeId = c.Guid(nullable: false),
                        PlanTypeCode = c.String(maxLength: 4000),
                        Year = c.Int(),
                        DirectorRcId = c.Guid(),
                        DirectorRcShortName = c.String(maxLength: 4000),
                        DirectorRcFullName = c.String(maxLength: 4000),
                        HeadOfOpoloId = c.Guid(),
                        HeadOfOpoloShortName = c.String(maxLength: 4000),
                        HeadOfOpoloFullName = c.String(maxLength: 4000),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.Id, t.PlanTypeId });
            
            CreateTable(
                "dbo.LimsEquipmentView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EquipmentPlanId = c.Guid(nullable: false),
                        Name = c.String(),
                        InventoryNumber = c.String(maxLength: 500),
                        SerialNumber = c.String(maxLength: 500),
                        YearInstallation = c.Int(),
                        ModelId = c.Guid(),
                        ModelName = c.String(maxLength: 4000),
                        ProducerId = c.Guid(),
                        ProducerName = c.String(maxLength: 4000),
                        CountryProductionId = c.Guid(),
                        CountryProductionName = c.String(maxLength: 4000),
                        EquipmentTypeId = c.Guid(),
                        EquipmentTypeName = c.String(maxLength: 4000),
                        LocationId = c.Guid(),
                        LocationName = c.String(maxLength: 4000),
                        StatusId = c.Guid(),
                        StatusName = c.String(maxLength: 4000),
                        LaboratoryId = c.Guid(),
                        LaboratoryName = c.String(maxLength: 4000),
                        ResponsiblePersonId = c.Guid(),
                        ResponsiblePersonShortName = c.String(maxLength: 4000),
                        ResponsiblePersonFullName = c.String(maxLength: 4000),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        TermDate = c.DateTime(),
                        EquipmentPlanCode = c.String(maxLength: 4000),
                        Number = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => new { t.Id, t.EquipmentPlanId });
            
            CreateTable(
                "dbo.LimsOrganization1CLink",
                c => new
                    {
                        OrganizationId = c.Guid(nullable: false),
                        Code = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.OrganizationId, t.Code });
            
            CreateTable(
                "dbo.LimsTmcActualView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        CountIssuedActual = c.Decimal(nullable: false, precision: 38, scale: 6),
                        CountUseActual = c.Decimal(nullable: false, precision: 38, scale: 6),
                        Code = c.String(maxLength: 450),
                        Number = c.String(maxLength: 450),
                        Name = c.String(),
                        MeasureTypeConvertName = c.String(maxLength: 4000),
                        MeasureTypeConvertNameKz = c.String(maxLength: 4000),
                        TmcCount = c.Decimal(precision: 18, scale: 6),
                        TmcCountFact = c.Decimal(precision: 18, scale: 6),
                        TmcCountConvert = c.Decimal(precision: 18, scale: 6),
                        TmcCountActual = c.Decimal(precision: 18, scale: 6),
                        CountSum = c.Decimal(precision: 38, scale: 6),
                        CountFactSum = c.Decimal(precision: 38, scale: 6),
                        CountActual = c.Decimal(precision: 38, scale: 6),
                        CreatedEmployeeFullName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.CreatedEmployeeId, t.CountIssuedActual, t.CountUseActual });
            
            CreateTable(
                "dbo.LimsTmcInView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, storeType: "date"),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        StateType = c.Int(nullable: false),
                        IsFullDelivery = c.Boolean(nullable: false),
                        IsScan = c.Boolean(nullable: false),
                        OwnerEmployeeId = c.Guid(),
                        OwnerEmployeeValue = c.String(maxLength: 4000),
                        Provider = c.String(maxLength: 450),
                        ProviderBin = c.String(maxLength: 450),
                        ContractNumber = c.String(maxLength: 450),
                        ContractDate = c.DateTime(storeType: "date"),
                        LastDeliveryDate = c.DateTime(storeType: "date"),
                        PowerOfAttorney = c.String(maxLength: 450),
                        ExecutorEmployeeId = c.Guid(),
                        ExecutorEmployeeValue = c.String(maxLength: 4000),
                        AgreementEmployeeId = c.Guid(),
                        AgreementEmployeeValue = c.String(maxLength: 4000),
                        AccountantEmployeeId = c.Guid(),
                        AccountantEmployeeValue = c.String(maxLength: 4000),
                        DayCount = c.Int(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.CreatedDate, t.CreatedEmployeeId, t.StateType, t.IsFullDelivery, t.IsScan });
            
            CreateTable(
                "dbo.LimsTmcOutView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TmcOutId = c.Guid(nullable: false),
                        TmcId = c.Guid(nullable: false),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 6),
                        CountFact = c.Decimal(nullable: false, precision: 18, scale: 6),
                        StateType = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, storeType: "date"),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        Code = c.String(maxLength: 450),
                        Number = c.String(maxLength: 450),
                        Name = c.String(),
                        MeasureTypeConvertDicId = c.Guid(),
                        MeasureTypeConvertName = c.String(maxLength: 4000),
                        MeasureTypeConvertNameKz = c.String(maxLength: 4000),
                        Note = c.String(maxLength: 450),
                        Comment = c.String(),
                        OutTypeDicId = c.Guid(),
                        OutTypeName = c.String(maxLength: 4000),
                        OutTypeNameKz = c.String(maxLength: 4000),
                        CreatedEmployeeFullName = c.String(maxLength: 4000),
                        OwnerEmployeeId = c.Guid(),
                        OwnerEmployeeFullName = c.String(maxLength: 4000),
                        StorageDicId = c.Guid(),
                        Rack = c.String(maxLength: 450),
                        Safe = c.String(maxLength: 450),
                    })
                .PrimaryKey(t => new { t.Id, t.TmcOutId, t.TmcId, t.Count, t.CountFact, t.StateType, t.CreatedDate, t.CreatedEmployeeId });
            
            CreateTable(
                "dbo.LimsTmcTemp",
                c => new
                    {
                        TmcId = c.Guid(nullable: false),
                        TmcInId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(),
                        CountRequest = c.Decimal(precision: 18, scale: 6),
                        CountReceived = c.Decimal(precision: 18, scale: 6),
                        IsSelected = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.TmcId, t.TmcInId })
                .ForeignKey("dbo.Tmcs", t => t.TmcId)
                .ForeignKey("dbo.TmcIns", t => t.TmcInId)
                .Index(t => t.TmcId)
                .Index(t => t.TmcInId);
            
            CreateTable(
                "dbo.Tmcs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, storeType: "date"),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        StateType = c.Int(nullable: false),
                        TmcInId = c.Guid(nullable: false),
                        Number = c.String(maxLength: 450),
                        Name = c.String(),
                        Code = c.String(maxLength: 450),
                        Manufacturer = c.String(maxLength: 450),
                        Serial = c.String(maxLength: 450),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 6),
                        MeasureTypeDicId = c.Guid(),
                        CountFact = c.Decimal(nullable: false, precision: 18, scale: 6),
                        CountConvert = c.Decimal(nullable: false, precision: 18, scale: 6),
                        MeasureTypeConvertDicId = c.Guid(),
                        CountActual = c.Decimal(nullable: false, precision: 18, scale: 6),
                        ManufactureDate = c.DateTime(storeType: "date"),
                        ExpiryDate = c.DateTime(storeType: "date"),
                        PackageDicId = c.Guid(),
                        TmcTypeDicId = c.Guid(),
                        StorageDicId = c.Guid(),
                        Safe = c.String(maxLength: 450),
                        Rack = c.String(maxLength: 450),
                        OwnerEmployeeId = c.Guid(),
                        ReceivingDate = c.DateTime(storeType: "date"),
                        WriteoffDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TmcIns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, storeType: "date"),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        StateType = c.Int(nullable: false),
                        OwnerEmployeeId = c.Guid(),
                        Provider = c.String(maxLength: 450),
                        ContractNumber = c.String(maxLength: 450),
                        ContractDate = c.DateTime(storeType: "date"),
                        LastDeliveryDate = c.DateTime(storeType: "date"),
                        IsFullDelivery = c.Boolean(nullable: false),
                        PowerOfAttorney = c.String(maxLength: 450),
                        ProviderBin = c.String(maxLength: 450),
                        ExecutorEmployeeId = c.Guid(),
                        AgreementEmployeeId = c.Guid(),
                        IsScan = c.Boolean(nullable: false),
                        AccountantEmployeeId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LimsTmcTempView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 6),
                        TmcInId = c.Guid(nullable: false),
                        CountFact = c.Decimal(nullable: false, precision: 18, scale: 6),
                        CountConvert = c.Decimal(nullable: false, precision: 18, scale: 6),
                        StateType = c.Int(nullable: false),
                        Code = c.String(maxLength: 450),
                        TmcName = c.String(),
                        MeasureTypeDicId = c.Guid(),
                        MeasureTypeDicName = c.String(maxLength: 4000),
                        MeasureTypeDicNameKz = c.String(maxLength: 4000),
                        CreatedDate = c.DateTime(),
                        CountRequest = c.Decimal(precision: 18, scale: 6),
                        CountReceived = c.Decimal(precision: 18, scale: 6),
                        IsSelected = c.Boolean(),
                        MeasureTypeConvertDicId = c.Guid(),
                        MeasureTypeConvertDicName = c.String(maxLength: 4000),
                        MeasureTypeConvertDicNameKz = c.String(maxLength: 4000),
                        ReceivingDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => new { t.Id, t.Count, t.TmcInId, t.CountFact, t.CountConvert, t.StateType });
            
            CreateTable(
                "dbo.LinkDicOrphanDrugsIcdDeseases",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        refDicOrphanDrugs = c.Long(nullable: false),
                        refDicIcdDeseases = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LinkDictionaries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DictionaryId = c.Guid(),
                        OwnerId = c.Guid(),
                        PropertyName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LinkDocuments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DocumentId = c.Guid(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        PropertyName = c.String(maxLength: 400),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LinkUnits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EmployeeId = c.Guid(),
                        PositionId = c.Guid(),
                        Department1Id = c.Guid(),
                        Department2Id = c.Guid(),
                        Department3Id = c.Guid(),
                        OrganizationId = c.Guid(),
                        PropertyName = c.String(maxLength: 4000),
                        OwnerId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Message = c.String(maxLength: 4000),
                        InnerException = c.String(maxLength: 4000),
                        StackTrace = c.String(maxLength: 4000),
                        CurrentUser = c.String(maxLength: 4000),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        EmployeesId = c.String(nullable: false, maxLength: 4000),
                        IsRead = c.Boolean(nullable: false),
                        Note = c.String(maxLength: 2000),
                        TableName = c.String(maxLength: 500),
                        ObjectId = c.String(maxLength: 500),
                        StateType = c.Int(),
                        ModifiedUser = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 510),
                        IsSend = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.obk_appendix",
                c => new
                    {
                        id = c.Long(nullable: false),
                        certification_id = c.Long(nullable: false),
                        appendix_number = c.String(nullable: false, maxLength: 50, unicode: false),
                        comment = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.obk_certifications", t => t.certification_id, cascadeDelete: true)
                .Index(t => t.certification_id);
            
            CreateTable(
                "dbo.obk_appendix_series",
                c => new
                    {
                        id = c.Long(nullable: false),
                        appendix_id = c.Long(nullable: false),
                        product_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.obk_products", t => t.product_id)
                .ForeignKey("dbo.obk_appendix", t => t.appendix_id, cascadeDelete: true)
                .Index(t => t.appendix_id)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.obk_products",
                c => new
                    {
                        id = c.Long(nullable: false),
                        application_id = c.Long(),
                        name = c.String(maxLength: 2000, unicode: false),
                        name_kz = c.String(maxLength: 4000),
                        producer_name = c.String(maxLength: 1000, unicode: false),
                        producer_name_kz = c.String(maxLength: 2000),
                        country_name = c.String(maxLength: 150, unicode: false),
                        country_name_kz = c.String(maxLength: 300),
                        party_count = c.Decimal(storeType: "money"),
                        party_measure_id = c.Int(),
                        tnved_code = c.String(maxLength: 20, unicode: false),
                        kpved_code = c.String(maxLength: 20, unicode: false),
                        mandatory_sign = c.Boolean(nullable: false),
                        register_nd = c.String(maxLength: 2000, unicode: false),
                        nd = c.String(maxLength: 2000, unicode: false),
                        nd_kz = c.String(maxLength: 4000),
                        reg_number = c.String(maxLength: 50, unicode: false),
                        register_id = c.Long(),
                        drug_form_id = c.Long(),
                        cert_sign = c.Boolean(),
                        types_no_reg = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.obk_certifications",
                c => new
                    {
                        id = c.Long(nullable: false),
                        product_id = c.Long(nullable: false),
                        reg_date = c.DateTime(storeType: "smalldatetime"),
                        end_date = c.DateTime(storeType: "smalldatetime"),
                        reg_number = c.String(nullable: false, maxLength: 50, unicode: false),
                        reg_number_kz = c.String(nullable: false, maxLength: 50),
                        blank_number = c.String(nullable: false, maxLength: 50, unicode: false),
                        product_name = c.String(nullable: false, unicode: false),
                        product_name_kz = c.String(),
                        add_info = c.String(nullable: false, unicode: false),
                        add_info_kz = c.String(),
                        date_prolongation = c.DateTime(storeType: "smalldatetime"),
                        annulment_sign = c.Boolean(nullable: false),
                        annulment_date = c.DateTime(storeType: "smalldatetime"),
                        annulment_reason = c.String(unicode: false),
                        dublicate_sign = c.Boolean(nullable: false),
                        cert_dublicate_id = c.Long(),
                        refuse_sign = c.Boolean(nullable: false),
                        refuse_date = c.DateTime(storeType: "smalldatetime"),
                        refuse_reason = c.String(unicode: false),
                        argument = c.String(unicode: false),
                        argument_kz = c.String(),
                        user_id = c.Long(),
                        unlimited_sign = c.Boolean(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.obk_products", t => t.product_id, cascadeDelete: true)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.obk_certification_copies",
                c => new
                    {
                        id = c.Long(nullable: false),
                        copies_application_id = c.Long(nullable: false),
                        certification_id = c.Long(nullable: false),
                        blank_begin_number = c.String(nullable: false, maxLength: 50, unicode: false),
                        blank_end_number = c.String(nullable: false, maxLength: 50, unicode: false),
                        certificate_reg_number = c.String(maxLength: 50, unicode: false),
                        certificate_blank_number = c.String(maxLength: 50, unicode: false),
                        comment = c.String(maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.obk_certifications", t => t.certification_id, cascadeDelete: true)
                .Index(t => t.certification_id);
            
            CreateTable(
                "dbo.obk_product_cost",
                c => new
                    {
                        id = c.Long(nullable: false),
                        cost = c.Decimal(nullable: false, storeType: "money"),
                        currency_id = c.Int(nullable: false),
                        date_cost = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.obk_products", t => t.id, cascadeDelete: true)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.OBK_Applicant",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameRu = c.String(maxLength: 1000),
                        NameKz = c.String(maxLength: 1000),
                        CreateDate = c.DateTime(),
                        ExpireDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_AssessmentDeclarationProgram",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeclarationId = c.Guid(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_AssessmentDeclarationProgramExecutors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeclarationId = c.Guid(nullable: false),
                        ProgramId = c.Guid(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.DeclarationId, t.ProgramId, t.EmployeeId });
            
            CreateTable(
                "dbo.OBK_AssessmentDeclarationRegisterView",
                c => new
                    {
                        DeclarationId = c.Guid(nullable: false),
                        StatusId = c.Int(nullable: false),
                        StageStatusId = c.Int(nullable: false),
                        ExecutorId = c.Guid(nullable: false),
                        RegType = c.String(nullable: false, maxLength: 128),
                        StageId = c.Guid(nullable: false),
                        StageStatusCode = c.String(nullable: false, maxLength: 50),
                        ContractNumber = c.String(nullable: false, maxLength: 30),
                        Number = c.String(maxLength: 500),
                        FirstSendDate = c.DateTime(),
                        DeclarantName = c.String(maxLength: 255),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        StatusName = c.String(maxLength: 2000),
                        StageCode = c.String(maxLength: 50),
                        CountryId = c.Guid(),
                        CountryNameRu = c.String(maxLength: 4000),
                        ProductsCount = c.Int(),
                        Series = c.String(),
                    })
                .PrimaryKey(t => new { t.DeclarationId, t.StatusId, t.StageStatusId, t.ExecutorId, t.RegType, t.StageId, t.StageStatusCode, t.ContractNumber });
            
            CreateTable(
                "dbo.OBK_AssessmentDeclarationReportView",
                c => new
                    {
                        Типуслуги = c.String(name: "Тип услуги", nullable: false, maxLength: 255),
                        Наименованиелекарственногоперепаратанаказахском = c.String(name: "Наименование лекарственного перепарата на казахском", nullable: false, maxLength: 255),
                        Типзаявки = c.String(name: "Тип заявки", nullable: false, maxLength: 128),
                        Номерзаявки = c.String(name: "Номер заявки", maxLength: 500),
                        Кодстатуса = c.String(name: "Код статуса", maxLength: 50),
                        Наименованиестатуса = c.String(name: "Наименование статуса", maxLength: 2000),
                        Датаподачизаявки = c.DateTime(name: "Дата подачи заявки"),
                        Датаокончаниязаявки = c.DateTime(name: "Дата окончания заявки"),
                        Наименованиепродукции = c.String(name: "Наименование продукции"),
                        Наименованиепроизводителя = c.String(name: "Наименование производителя"),
                    })
                .PrimaryKey(t => new { t.Типуслуги, t.Наименованиелекарственногоперепаратанаказахском, t.Типзаявки });
            
            CreateTable(
                "dbo.OBK_AssessmentDeclarationView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        TypeId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        TypeName = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        Number = c.String(maxLength: 500),
                        SendDate = c.DateTime(),
                        StausName = c.String(maxLength: 2000),
                        SortDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.Id, t.OwnerId, t.TypeId, t.StatusId, t.TypeName, t.CreatedDate });
            
            CreateTable(
                "dbo.OBK_BlankAccountingView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.Int(),
                        RegisterDate = c.DateTime(),
                        ExpConclusionNumber = c.String(maxLength: 50),
                        Declarant = c.String(maxLength: 255),
                        Executor = c.String(maxLength: 4000),
                        DocumentType = c.String(maxLength: 50),
                        Sign = c.String(maxLength: 11),
                        OrganName = c.String(maxLength: 4000),
                        Decommissioned = c.Boolean(),
                        DecommissionedDate = c.DateTime(),
                        Corrupted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_BlankNumber",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Object_Id = c.Guid(),
                        Number = c.Int(),
                        BlankTypeId = c.Short(),
                        EmployeeId = c.Guid(),
                        CreateDate = c.DateTime(),
                        Decommissioned = c.Boolean(),
                        Corrupted = c.Boolean(),
                        DecommissionedDate = c.DateTime(),
                        ParentId = c.Guid(),
                        CorruptDate = c.DateTime(),
                        CorruptEmployee = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_BlankNumber", t => t.ParentId)
                .ForeignKey("dbo.OBK_BlankType", t => t.BlankTypeId)
                .Index(t => t.BlankTypeId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.OBK_BlankType",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        NameRu = c.String(maxLength: 50),
                        NameKz = c.String(maxLength: 50),
                        Code = c.String(maxLength: 50),
                        CreateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_CertificateReference",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(),
                        CertificateNumber = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        CertificateCountryId = c.Guid(),
                        CertificateOrganization = c.String(maxLength: 500),
                        CertificateTypeId = c.Int(nullable: false),
                        LastInspection = c.DateTime(),
                        CertificateValidityTypeId = c.Short(),
                        AttachPath = c.String(maxLength: 300),
                        CertificateProducer = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_CertificateValidityType", t => t.CertificateValidityTypeId)
                .Index(t => t.CertificateValidityTypeId);
            
            CreateTable(
                "dbo.OBK_CertificateReferenceFieldHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Controlld = c.String(),
                        UserId = c.Guid(),
                        ValueField = c.String(),
                        DisplayField = c.String(),
                        CreateDate = c.DateTime(),
                        OBK_CertificateReferenceId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OBK_CertificateReference", t => t.OBK_CertificateReferenceId)
                .Index(t => t.OBK_CertificateReferenceId);
            
            CreateTable(
                "dbo.OBK_CertificateValidityType",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_ContractHistoryView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                        StatusId = c.Guid(nullable: false),
                        ContractId = c.Guid(nullable: false),
                        UnitName = c.String(maxLength: 4000),
                        RefuseReason = c.String(maxLength: 4000),
                        EmployeeFullName = c.String(maxLength: 4000),
                        EmployeeShortName = c.String(maxLength: 4000),
                        StatusCode = c.String(maxLength: 50),
                        StatusNameRu = c.String(maxLength: 2000),
                        StatusNameKz = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => new { t.Id, t.Created, t.EmployeeId, t.StatusId, t.ContractId });
            
            CreateTable(
                "dbo.OBK_ContractRegisterView",
                c => new
                    {
                        ContractId = c.Guid(nullable: false),
                        ContractTypeRu = c.String(nullable: false, maxLength: 128),
                        ContractTypeKz = c.String(nullable: false, maxLength: 128),
                        ContractNumber = c.String(nullable: false, maxLength: 30),
                        ContractCreatedDate = c.DateTime(nullable: false),
                        ExecutorId = c.Guid(nullable: false),
                        ExecutorType = c.Int(nullable: false),
                        ContractStatusId = c.Int(nullable: false),
                        StageStatusId = c.Int(nullable: false),
                        StageStatusNameRu = c.String(nullable: false, maxLength: 2000),
                        StageStatusCode = c.String(nullable: false, maxLength: 50),
                        ContractStageId = c.Guid(nullable: false),
                        ContractStageStageId = c.Int(nullable: false),
                        ContractParentId = c.Guid(),
                        ContractDisplayNumber = c.String(maxLength: 61),
                        ContractSendDate = c.DateTime(),
                        ContractStartDate = c.DateTime(),
                        ContractEndDate = c.DateTime(),
                        ContractSigner = c.Guid(),
                        DeclarantNameRu = c.String(maxLength: 255),
                        ContractStatusNameRu = c.String(maxLength: 2000),
                        StageCOZResult = c.Int(),
                        StageUOBKResult = c.Int(),
                        StageUOBKExecutor = c.String(maxLength: 4000),
                        StageDEFResult = c.Int(),
                        StageDEFExecutor = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.ContractId, t.ContractTypeRu, t.ContractTypeKz, t.ContractNumber, t.ContractCreatedDate, t.ExecutorId, t.ExecutorType, t.ContractStatusId, t.StageStatusId, t.StageStatusNameRu, t.StageStatusCode, t.ContractStageId, t.ContractStageStageId });
            
            CreateTable(
                "dbo.OBK_ContractReportView",
                c => new
                    {
                        Idдоговора = c.Guid(name: "Id договора", nullable: false),
                        ПОдписаноЭЦП = c.String(name: "ПОдписано ЭЦП", nullable: false, maxLength: 1, unicode: false),
                        Типдоговора = c.String(name: "Тип договора", nullable: false, maxLength: 128),
                        Кодстатуса = c.String(name: "Код статуса", maxLength: 50),
                        Наименованиестатуса = c.String(name: "Наименование статуса", maxLength: 2000),
                        Статусоплаты = c.Boolean(name: "Статус оплаты"),
                        Статуснеполнойоплаты = c.Boolean(name: "Статус не полной оплаты"),
                        Датаподачидоговора = c.DateTime(name: "Дата подачи договора"),
                        заявки = c.String(name: "№ заявки", maxLength: 500),
                        Кодстатусазаявки = c.String(name: "Код статуса заявки", maxLength: 50),
                        Наименованиестатусазаявки = c.String(name: "Наименование статуса заявки", maxLength: 2000),
                        Экспертинаяорганизация = c.String(name: "Экспертиная организация", maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Idдоговора, t.ПОдписаноЭЦП, t.Типдоговора });
            
            CreateTable(
                "dbo.OBK_ContractView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        DocType = c.String(nullable: false, maxLength: 15),
                        Number = c.String(maxLength: 61),
                        StatusNameRu = c.String(maxLength: 2000),
                        DeclarantNameRu = c.String(maxLength: 255),
                        Type = c.String(),
                        TypeKz = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        ParentId = c.Guid(),
                        EmployeeId = c.Guid(),
                    })
                .PrimaryKey(t => new { t.Id, t.CreatedDate, t.DocType });
            
            CreateTable(
                "dbo.OBK_ContractWithoutAttachmentsView",
                c => new
                    {
                        ExecutorId = c.Guid(nullable: false),
                        Number = c.String(maxLength: 61),
                    })
                .PrimaryKey(t => t.ExecutorId);
            
            CreateTable(
                "dbo.OBK_CorruptedBlankNumberView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.Int(),
                        CorruptDate = c.DateTime(),
                        CorruptEmployee = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.obk_currencies",
                c => new
                    {
                        id = c.Int(nullable: false),
                        currency_name = c.String(maxLength: 50, unicode: false),
                        short_name = c.String(maxLength: 10, unicode: false),
                        currency_code = c.String(maxLength: 10, unicode: false),
                        abbreviation = c.String(maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.obk_exchangerate",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        currency_id = c.Int(nullable: false),
                        rate = c.Decimal(nullable: false, precision: 18, scale: 3),
                        rate_date = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.obk_currencies", t => t.currency_id)
                .Index(t => t.currency_id);
            
            CreateTable(
                "dbo.OBK_DeclarationCorrespondingView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BlankNumber = c.String(maxLength: 50, unicode: false),
                        ExtraditeDate = c.DateTime(),
                        ExpConclusionNumber = c.String(maxLength: 50),
                        ZBKType = c.String(maxLength: 20),
                        ProductName = c.String(maxLength: 4000),
                        SeriesNumber = c.String(),
                        SeriesEndDate = c.String(),
                        SeriesParty = c.String(),
                        ExpStartDate = c.DateTime(),
                        ExpEndDate = c.DateTime(),
                        RequestType = c.String(),
                        ZBKReviewDate = c.Int(),
                        OrganName = c.String(maxLength: 4000),
                        OrganId = c.Guid(),
                        ExpResult = c.Boolean(),
                        ExpReason = c.String(),
                        RequestTypeId = c.Int(),
                        ProducerName = c.String(),
                        RefuseDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_DirectionToPaymentsView",
                c => new
                    {
                        CreateDate = c.DateTime(nullable: false),
                        ContractId = c.Guid(nullable: false),
                        CreateEmployeeId = c.Guid(nullable: false),
                        CreateEmployeeValue = c.String(nullable: false, maxLength: 1024),
                        StatusId = c.Guid(nullable: false),
                        ContractNumber = c.String(nullable: false, maxLength: 30),
                        Id = c.Guid(nullable: false),
                        ExecutorSign = c.String(nullable: false, maxLength: 3),
                        ChiefAccountantSign = c.String(nullable: false, maxLength: 3),
                        PayerId = c.Guid(),
                        PayerValue = c.String(maxLength: 4000),
                        IsDeleted = c.Boolean(),
                        DisplayName = c.String(maxLength: 4000),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        StatusCode = c.String(),
                        InvoiceNumber = c.String(maxLength: 512),
                        ContractStartDate = c.DateTime(),
                        InvoiceNumber1C = c.String(maxLength: 512),
                        ExecutorId = c.Guid(),
                        ChiefAccountantId = c.Guid(),
                        ExpertOrganization = c.Guid(),
                        ChiefName = c.String(maxLength: 4000),
                        ExecutorName = c.String(maxLength: 4000),
                        ZBKCopy_id = c.Guid(),
                    })
                .PrimaryKey(t => new { t.CreateDate, t.ContractId, t.CreateEmployeeId, t.CreateEmployeeValue, t.StatusId, t.ContractNumber, t.Id, t.ExecutorSign, t.ChiefAccountantSign });
            
            CreateTable(
                "dbo.OBK_LetterAttachments",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        AttachmentName = c.String(maxLength: 100),
                        ContentFile = c.Binary(),
                        LetterId = c.Long(),
                        SignXmlBigData = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OBK_Ref_ContractDocumentType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NameRu = c.String(nullable: false, maxLength: 255),
                        NameKz = c.String(nullable: false, maxLength: 255),
                        NameGenitiveRu = c.String(nullable: false, maxLength: 255),
                        NameGenitiveKz = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_Ref_Nomenclature",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        NameRu = c.String(nullable: false),
                        NameKz = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_Ref_ValueAddedTax",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_UniqueNumber",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeclarantId = c.Guid(nullable: false),
                        Code = c.String(nullable: false, maxLength: 20),
                        Number = c.Int(nullable: false),
                        ProductSeriesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_ZBKCopyStageView",
                c => new
                    {
                        OBK_ZBKCopyStageId = c.Guid(nullable: false),
                        StageId = c.Int(nullable: false),
                        ExpConclusionNumber = c.String(maxLength: 50),
                        ExpStartDate = c.DateTime(),
                        ExpEndDate = c.DateTime(),
                        ExpApplication = c.Boolean(),
                        DeclarationNumber = c.String(maxLength: 500),
                        ZBKCopySendDate = c.DateTime(),
                        Declarer = c.String(maxLength: 765),
                        DeclarationType = c.String(),
                        OrganizationName = c.String(maxLength: 4000),
                        StageStatusCode = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.OBK_ZBKCopyStageId, t.StageId });
            
            CreateTable(
                "dbo.OBK_ZBKRegisterView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BlankNumber = c.String(maxLength: 50, unicode: false),
                        ExtraditeDate = c.DateTime(),
                        ExpConclusionNumber = c.String(maxLength: 50),
                        ZBKType = c.String(maxLength: 50),
                        Declarant = c.String(maxLength: 255),
                        ProductName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OBK_ZBKTransferRegisterView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RequestType = c.String(nullable: false, maxLength: 5),
                        Declarant = c.String(maxLength: 255),
                        ConclusionNumber = c.String(maxLength: 50),
                        SendDate = c.DateTime(),
                        DrugFormFullName = c.String(),
                        ExtraditeDate = c.DateTime(),
                        ReceiverFIO = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => new { t.Id, t.RequestType });
            
            CreateTable(
                "dbo.OBKContractProductsView",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        OBK_RS_ProductsId = c.Int(nullable: false),
                        ProdSeriesId = c.Int(nullable: false),
                        NameRu = c.String(),
                        ProdSeries = c.String(),
                        ProdSeriesEndDate = c.String(),
                        ProdSeriesParty = c.String(),
                        ProdCountryNameRu = c.String(),
                        ProdProducerNameRu = c.String(),
                        ProdShortName = c.String(maxLength: 250, unicode: false),
                        ContractId = c.Guid(),
                    })
                .PrimaryKey(t => new { t.Id, t.OBK_RS_ProductsId, t.ProdSeriesId });
            
            CreateTable(
                "dbo.OBKTaskListRegisterView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UnitId = c.Guid(nullable: false),
                        TaskNumber = c.String(maxLength: 50),
                        RegisterDate = c.String(maxLength: 50),
                        TaskEndDate = c.DateTime(),
                        ShortName = c.String(maxLength: 4000),
                        StageId = c.Int(),
                        ExecutorId = c.Guid(),
                        StatusNameRu = c.String(maxLength: 2000),
                        StatusNameKz = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => new { t.Id, t.UnitId });
            
            CreateTable(
                "dbo.OrganizationsView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        IsResident = c.Boolean(nullable: false),
                        NameKz = c.String(maxLength: 500),
                        NameRu = c.String(maxLength: 500),
                        NameEn = c.String(maxLength: 500),
                        CountryDicId = c.Guid(),
                        AddressLegal = c.String(maxLength: 500),
                        AddressFact = c.String(maxLength: 500),
                        Phone = c.String(maxLength: 500),
                        Fax = c.String(maxLength: 500),
                        Email = c.String(maxLength: 500),
                        BossFio = c.String(maxLength: 500),
                        BossPosition = c.String(maxLength: 500),
                        ContactFio = c.String(maxLength: 500),
                        ContactPosition = c.String(maxLength: 500),
                        ContactPhone = c.String(maxLength: 500),
                        ContactFax = c.String(maxLength: 500),
                        ContactEmail = c.String(maxLength: 500),
                        OrgManufactureTypeDicId = c.Guid(),
                        DocNumber = c.String(maxLength: 500),
                        DocDate = c.DateTime(storeType: "date"),
                        DocExpiryDate = c.DateTime(storeType: "date"),
                        ObjectId = c.Guid(),
                        OpfTypeDicId = c.Guid(),
                        BankName = c.String(maxLength: 500),
                        BankIik = c.String(maxLength: 500),
                        BankCurencyDicId = c.Guid(),
                        BankSwift = c.String(maxLength: 500),
                        Bin = c.String(maxLength: 500),
                        PayerTypeDicId = c.Guid(),
                        CountryName = c.String(maxLength: 4000),
                        OrgManufactureTypeName = c.String(maxLength: 4000),
                        OpfTypeName = c.String(maxLength: 4000),
                        BankCurencyDicName = c.String(maxLength: 4000),
                        PayerTypeName = c.String(maxLength: 4000),
                        BossLastName = c.String(maxLength: 100),
                        BossFirstName = c.String(maxLength: 100),
                        BossMiddleName = c.String(maxLength: 100),
                        PaymentBill = c.String(maxLength: 500),
                        Iin = c.String(maxLength: 500),
                        BankBik = c.String(maxLength: 500),
                        OriginalOrgId = c.Guid(),
                        Filial = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => new { t.Id, t.Type, t.IsResident });
            
            CreateTable(
                "dbo.OrphanDrugsIcdDeseasesView",
                c => new
                    {
                        OrphanDrugsId = c.Long(nullable: false),
                        CreateDatetime = c.DateTime(nullable: false),
                        Id = c.Guid(),
                        LinkDicId = c.Long(),
                        Name = c.String(),
                        IcdDeseasesId = c.Long(),
                        CodeICD = c.String(maxLength: 512),
                        DiseaseOfICD = c.String(),
                        SysnonimAndRareDesease = c.String(),
                    })
                .PrimaryKey(t => new { t.OrphanDrugsId, t.CreateDatetime });
            
            CreateTable(
                "dbo.OutgoingDocument",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsAdministrativeUse = c.Boolean(nullable: false),
                        IsAwaitingResponse = c.Boolean(nullable: false),
                        IsTradeSecret = c.Boolean(nullable: false),
                        ApplicantType = c.Int(nullable: false),
                        DocumentType = c.Int(nullable: false),
                        MonitoringType = c.Int(nullable: false),
                        PriorityType = c.Int(nullable: false),
                        StateType = c.Int(nullable: false),
                        AppendixCount = c.Int(nullable: false),
                        CopiesCount = c.Int(nullable: false),
                        PageCount = c.Int(nullable: false),
                        RepeatCount = c.Int(nullable: false),
                        SortNumber = c.Int(nullable: false),
                        OutgoingType = c.Int(nullable: false),
                        IsNotification = c.Boolean(nullable: false),
                        NotificationCount = c.Int(nullable: false),
                        IsAttachments = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        AwaitingResponseDate = c.DateTime(),
                        DocumentDate = c.DateTime(),
                        OutgoingDate = c.DateTime(),
                        ProtocolDate = c.DateTime(),
                        MonitoringDate = c.DateTime(),
                        ApplicantAddress = c.String(maxLength: 512),
                        ApplicantEmail = c.String(maxLength: 4000),
                        ApplicantName = c.String(maxLength: 512),
                        ApplicantPhone = c.String(maxLength: 512),
                        BlankNumber = c.String(maxLength: 512),
                        CorrespondentsInfo = c.String(maxLength: 512),
                        Number = c.String(maxLength: 512),
                        OutgoingNumber = c.String(maxLength: 512),
                        SortingNumber = c.String(maxLength: 512),
                        SortingOutgoingNumber = c.String(maxLength: 512),
                        Note = c.String(maxLength: 4000),
                        Summary = c.String(maxLength: 4000),
                        AdministrativeTypeDictionaryValue = c.String(maxLength: 4000),
                        ApplicantCategoryDictionaryValue = c.String(maxLength: 4000),
                        CauseCitizenDictionaryValue = c.String(maxLength: 4000),
                        CitizenCategoryDictionaryValue = c.String(maxLength: 4000),
                        CitizenResultDictionaryValue = c.String(maxLength: 4000),
                        CitizenTypeDictionaryValue = c.String(maxLength: 4000),
                        DocumentKindDictionaryValue = c.String(maxLength: 4000),
                        FormDeliveryDictionaryValue = c.String(maxLength: 4000),
                        FormSendingDictionaryValue = c.String(maxLength: 4000),
                        KatoDictionaryValue = c.String(maxLength: 4000),
                        LanguageDictionaryValue = c.String(maxLength: 4000),
                        NomenclatureDictionaryValue = c.String(maxLength: 4000),
                        QuestionDesignDictionaryValue = c.String(maxLength: 4000),
                        SigningFormDictionaryValue = c.String(maxLength: 4000),
                        AgreementsValue = c.String(maxLength: 4000),
                        ExecutorsValue = c.String(maxLength: 4000),
                        ReadersValue = c.String(maxLength: 4000),
                        RecipientsValue = c.String(maxLength: 4000),
                        RegistratorValue = c.String(maxLength: 4000),
                        ResponsibleValue = c.String(maxLength: 4000),
                        SignerValue = c.String(maxLength: 4000),
                        CorrespondentsValue = c.String(maxLength: 4000),
                        MonitoringAuthorValue = c.String(maxLength: 4000),
                        MonitoringNote = c.String(maxLength: 4000),
                        AnswersValue = c.String(maxLength: 4000),
                        CompleteDocumentsValue = c.String(maxLength: 4000),
                        EditDocumentsValue = c.String(maxLength: 4000),
                        RepealDocumentsValue = c.String(maxLength: 4000),
                        DisplayName = c.String(maxLength: 4000),
                        AutoAnswersValue = c.String(maxLength: 4000),
                        AutoAnswersTempValue = c.String(maxLength: 4000),
                        AutoCompleteDocumentsValue = c.String(maxLength: 4000),
                        AutoEditDocumentsValue = c.String(maxLength: 4000),
                        AutoRepealDocumentsValue = c.String(maxLength: 4000),
                        AutoAwaitingResponseDate = c.DateTime(storeType: "date"),
                        AutoDocumentDate = c.DateTime(storeType: "date"),
                        AutoOutgoingDate = c.DateTime(storeType: "date"),
                        AutoProtocolDate = c.DateTime(storeType: "date"),
                        AutoMonitoringDate = c.DateTime(storeType: "date"),
                        FactExecutionDate = c.DateTime(),
                        FirstExecutionDate = c.DateTime(),
                        ExecutionDate = c.DateTime(),
                        AutoFactExecutionDate = c.DateTime(storeType: "date"),
                        AutoFirstExecutionDate = c.DateTime(storeType: "date"),
                        AutoExecutionDate = c.DateTime(storeType: "date"),
                        Counters = c.String(maxLength: 4000),
                        DocumentDictionaryTypeValue = c.String(maxLength: 4000),
                        ResolutionValue = c.String(maxLength: 4000),
                        SourceValue = c.String(maxLength: 4000),
                        DestinationValue = c.String(maxLength: 4000),
                        OwnerValue = c.String(maxLength: 4000),
                        Country = c.String(maxLength: 4000),
                        Area = c.String(maxLength: 4000),
                        Postcode = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 4000),
                        Department = c.String(maxLength: 4000),
                        City = c.String(maxLength: 4000),
                        Address = c.String(maxLength: 4000),
                        NumberBill = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        SuperMainDocumentId = c.Guid(),
                        ModifiedUser = c.String(maxLength: 4000),
                        DateDispatch = c.DateTime(),
                        DispatchNote = c.String(maxLength: 4000),
                        Digest = c.String(maxLength: 4000),
                        Text = c.String(maxLength: 4000),
                        Recipient = c.String(maxLength: 4000),
                        QrCode = c.Binary(storeType: "image"),
                        FulfilledDate = c.DateTime(),
                        State = c.String(maxLength: 510),
                        Outgoing = c.String(maxLength: 510),
                    })
                .PrimaryKey(t => new { t.Id, t.IsDeleted, t.IsAdministrativeUse, t.IsAwaitingResponse, t.IsTradeSecret, t.ApplicantType, t.DocumentType, t.MonitoringType, t.PriorityType, t.StateType, t.AppendixCount, t.CopiesCount, t.PageCount, t.RepeatCount, t.SortNumber, t.OutgoingType, t.IsNotification, t.NotificationCount, t.IsAttachments, t.IsArchive });
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PackageTypeDicId = c.Guid(),
                        PackageNameDicId = c.Guid(),
                        PackageName = c.String(maxLength: 500),
                        Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SizeMeasureTypeDicId = c.Guid(),
                        Volume = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VolumeMeasureTypeDicId = c.Guid(),
                        Count = c.Int(nullable: false),
                        Note = c.String(maxLength: 500),
                        ObjectId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PackagesView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Volume = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Count = c.Int(nullable: false),
                        PackageTypeDicId = c.Guid(),
                        PackageNameDicId = c.Guid(),
                        PackageName = c.String(maxLength: 500),
                        SizeMeasureTypeDicId = c.Guid(),
                        VolumeMeasureTypeDicId = c.Guid(),
                        Note = c.String(maxLength: 500),
                        ObjectId = c.Guid(),
                        PackageNameDicName = c.String(maxLength: 4000),
                        PackageTypeName = c.String(maxLength: 4000),
                        SizeMeasureTypeName = c.String(maxLength: 4000),
                        VolumeMeasureName = c.String(maxLength: 4000),
                        PackageNameAuto = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.Size, t.Volume, t.Count });
            
            CreateTable(
                "dbo.PermissionKeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(maxLength: 1000),
                        Type = c.Int(nullable: false),
                        KeyName = c.String(maxLength: 4000),
                        KeyDescription = c.String(maxLength: 4000),
                        GroupName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionRoleKeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PermissionRoleId = c.Int(),
                        PermissionKey = c.String(maxLength: 1000),
                        PermissionValue = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PermissionKey = c.String(maxLength: 4000),
                        Value = c.String(maxLength: 4000),
                        Name = c.String(maxLength: 4000),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PP_AttachRemarksView",
                c => new
                    {
                        AttachRemarkId = c.Guid(nullable: false),
                        PriceProjectId = c.Guid(nullable: false),
                        AttachDicId = c.Guid(nullable: false),
                        dicName = c.String(maxLength: 4000),
                        RemarkNote = c.String(),
                    })
                .PrimaryKey(t => new { t.AttachRemarkId, t.PriceProjectId, t.AttachDicId });
            
            CreateTable(
                "dbo.PP_DIC_AvgExchangeRateView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CurrencyId = c.Guid(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(maxLength: 4000),
                        ChangeEmployeeId = c.Guid(),
                    })
                .PrimaryKey(t => new { t.Id, t.CurrencyId, t.Rate });
            
            CreateTable(
                "dbo.PP_ProcessingJournal",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ProxyOrganizationId = c.Guid(nullable: false),
                        ManufacturerOrganizationId = c.Guid(nullable: false),
                        SelectStatus = c.Int(),
                        Number = c.String(maxLength: 512),
                        IncomeNumber = c.String(maxLength: 500),
                        ProxyOrgName = c.String(maxLength: 500),
                        ManufacturerOrgName = c.String(maxLength: 500),
                        RegNumber = c.String(maxLength: 500),
                        MnnRu = c.String(maxLength: 500),
                        MnnEn = c.String(maxLength: 500),
                        TradeName = c.String(maxLength: 1000),
                        HasSign = c.Boolean(),
                        EndExecDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.Id, t.Type, t.Status, t.CreatedDate, t.ProxyOrganizationId, t.ManufacturerOrganizationId });
            
            CreateTable(
                "dbo.PP_ProtocolListView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProtocolDate = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        HasEdit = c.Boolean(),
                        Number = c.String(maxLength: 500),
                        IsFinal = c.Boolean(),
                        TypeName = c.String(maxLength: 4000),
                        UserId = c.Guid(),
                        OwnerName = c.String(maxLength: 4000),
                        RequesterName = c.String(maxLength: 500),
                        StatusName = c.String(maxLength: 4000),
                        ChiefId = c.Guid(),
                    })
                .PrimaryKey(t => new { t.Id, t.ProtocolDate, t.Type, t.Status, t.IsDeleted });
            
            CreateTable(
                "dbo.PriceListExpertise",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(maxLength: 50),
                        Name = c.String(maxLength: 450),
                        PriceRegisterForeign = c.Decimal(precision: 18, scale: 2),
                        PriceRegisterForeignNds = c.Decimal(precision: 18, scale: 2),
                        PriceReRegisterForeign = c.Decimal(precision: 18, scale: 2),
                        PriceReRegisterForeignNds = c.Decimal(precision: 18, scale: 2),
                        PriceRegisterKz = c.Decimal(precision: 18, scale: 2),
                        PriceRegisterKzNds = c.Decimal(precision: 18, scale: 2),
                        PriceReRegisterKz = c.Decimal(precision: 18, scale: 2),
                        PriceReRegisterKzNds = c.Decimal(precision: 18, scale: 2),
                        Category = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceProject_Ext",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MnnCode = c.String(maxLength: 100),
                        DrugDescription = c.String(maxLength: 1000),
                        RegEndDate = c.DateTime(),
                        RequesterContacts = c.String(maxLength: 500),
                        MarginalPriceMnn2016 = c.Decimal(precision: 18, scale: 2),
                        MarginalPriceTn622 = c.Decimal(precision: 18, scale: 2),
                        PriceDropPercent = c.Decimal(precision: 18, scale: 2),
                        IsConformity639 = c.Boolean(),
                        Conformity639Note = c.String(maxLength: 500),
                        FixPrice_a_11_16 = c.Decimal(precision: 18, scale: 2),
                        PriceDifference2016 = c.Decimal(precision: 18, scale: 2),
                        IntRef_AvgInPricePackage2015 = c.Decimal(precision: 18, scale: 2),
                        IntRef_AvgInPricePackage2015CurrencyId = c.Guid(),
                        IntRef_AvgInPricePackage = c.Decimal(precision: 18, scale: 2),
                        IntRef_RetailAktobe = c.Decimal(precision: 18, scale: 2),
                        MinRefPriceCoef = c.Decimal(precision: 18, scale: 2),
                        p16_Country = c.String(maxLength: 100),
                        p16_RegPrice = c.String(maxLength: 100),
                        p16_RegYear = c.Int(),
                        p16_MarginalPrice = c.String(maxLength: 100),
                        p16_MarginalYear = c.Int(),
                        p16_AvgOptPricee = c.String(maxLength: 100),
                        p16_AvgRetailPrice = c.String(maxLength: 100),
                        RegNcelsPrice_11_16 = c.Decimal(precision: 18, scale: 2),
                        RegMzsrPrice_11_16 = c.Decimal(precision: 18, scale: 2),
                        FinalPrice = c.Decimal(precision: 18, scale: 2),
                        FinalPricePercent = c.Decimal(precision: 18, scale: 2),
                        FinalFixPrice = c.Decimal(precision: 18, scale: 2),
                        FinalMarginalPriceTn = c.Decimal(precision: 18, scale: 2),
                        ProjectPrice2017 = c.Decimal(precision: 18, scale: 2),
                        MinRefPrice2016 = c.Decimal(precision: 18, scale: 2),
                        FixPrice_a_11_16_Reason = c.String(maxLength: 100),
                        BritishPriceNote = c.String(maxLength: 500),
                        IntRef_AvgRetPricePackage = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceProjectArchiveDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        MnnCode = c.String(maxLength: 100),
                        MnnRu = c.String(maxLength: 500),
                        DrugDescription = c.String(maxLength: 1000),
                        DrugName = c.String(maxLength: 1000),
                        CountPackage = c.String(maxLength: 500),
                        ProducerName = c.String(maxLength: 500),
                        ProducerCountry = c.String(maxLength: 4000),
                        RegNumber = c.String(maxLength: 500),
                        RegDate = c.DateTime(storeType: "date"),
                        RegEndDate = c.DateTime(),
                        ProxyName = c.String(maxLength: 500),
                        RequesterContacts = c.String(maxLength: 500),
                        LsTypeDicId = c.Guid(),
                        LsTypeName = c.String(maxLength: 4000),
                        MarginalPriceTn622 = c.Decimal(precision: 18, scale: 2),
                        CipPrice = c.Decimal(precision: 18, scale: 2),
                        CipPriceCur = c.String(maxLength: 4000),
                        ManufacturerPrice = c.Decimal(precision: 18, scale: 2),
                        ManufacturerPriceCur = c.String(maxLength: 4000),
                        RefPrice = c.Decimal(precision: 18, scale: 2),
                        RefPriceCur = c.String(maxLength: 4000),
                        OwnerPrice = c.Decimal(precision: 18, scale: 2),
                        OwnerPriceCur = c.String(maxLength: 4000),
                        BasePrice = c.Decimal(precision: 18, scale: 2),
                        BasePriceDate = c.DateTime(storeType: "date"),
                        LimitCost = c.Decimal(precision: 18, scale: 2),
                        AvgObkCost = c.Decimal(precision: 18, scale: 2),
                        AvgOptCost = c.Decimal(precision: 18, scale: 2),
                        AvgRznCost = c.Decimal(precision: 18, scale: 2),
                        ZakupCost = c.Decimal(precision: 18, scale: 2),
                        MinimalCost = c.Decimal(precision: 18, scale: 2),
                        TalkCipPrice = c.Decimal(precision: 18, scale: 2),
                        TalkCipPriceCur = c.String(maxLength: 4000),
                        TalkManufacturerPrice = c.Decimal(precision: 18, scale: 2),
                        TalkManufacturerPriceCur = c.String(maxLength: 4000),
                        TalkefPrice = c.Decimal(precision: 18, scale: 2),
                        TalkefPriceCur = c.String(maxLength: 4000),
                        TalkOwnerPrice = c.Decimal(precision: 18, scale: 2),
                        TalkOwnerPriceCur = c.String(maxLength: 4000),
                        TalkUnitPrice = c.Decimal(precision: 18, scale: 2),
                        TalkPriceDate = c.DateTime(storeType: "date"),
                        MarginalPriceMnn2016 = c.Decimal(precision: 18, scale: 2),
                        PriceDropPercent = c.Decimal(precision: 18, scale: 2),
                        IsConformity639 = c.Boolean(),
                        Conformity639Note = c.String(maxLength: 500),
                        FixPrice_a_11_16 = c.Decimal(precision: 18, scale: 2),
                        PriceDifference2016 = c.Decimal(precision: 18, scale: 2),
                        IntRef_AvgInPricePackage2015 = c.Decimal(precision: 18, scale: 2),
                        IntRef_AvgInPricePackage2015Cur = c.String(maxLength: 4000),
                        IntRef_AvgInPricePackage = c.Decimal(precision: 18, scale: 2),
                        IntRef_RetailAktobe = c.Decimal(precision: 18, scale: 2),
                        MinRefPrice2016 = c.Decimal(precision: 18, scale: 2),
                        MinRefPriceCoef = c.Decimal(precision: 18, scale: 2),
                        RegNcelsPrice_11_16 = c.Decimal(precision: 18, scale: 2),
                        RegMzsrPrice_11_16 = c.Decimal(precision: 18, scale: 2),
                        FinalPrice = c.Decimal(precision: 18, scale: 2),
                        FinalPricePercent = c.Decimal(precision: 18, scale: 2),
                        FinalFixPrice = c.Decimal(precision: 18, scale: 2),
                        FinalMarginalPriceTn = c.Decimal(precision: 18, scale: 2),
                        ProjectPrice2017 = c.Decimal(precision: 18, scale: 2),
                        RequestOrderType = c.Int(),
                        RequestOrderYear = c.Int(),
                        BritishPrice = c.Decimal(precision: 18, scale: 2),
                        BelarusPrice = c.Decimal(precision: 18, scale: 2),
                        CzechPrice = c.Decimal(precision: 18, scale: 2),
                        HungaryPrice = c.Decimal(precision: 18, scale: 2),
                        LatviaPrice = c.Decimal(precision: 18, scale: 2),
                        RfPrice = c.Decimal(precision: 18, scale: 2),
                        AustriaPrice = c.Decimal(precision: 18, scale: 2),
                        UkrainePrice = c.Decimal(precision: 18, scale: 2),
                        TurkeyPrice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.Id, t.CreatedDate });
            
            CreateTable(
                "dbo.PriceProjectArchiveView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        MnnCode = c.String(maxLength: 100),
                        MnnRu = c.String(maxLength: 500),
                        DrugDescription = c.String(maxLength: 1000),
                        DrugName = c.String(maxLength: 1000),
                        CountPackage = c.String(maxLength: 500),
                        ProducerName = c.String(maxLength: 500),
                        ProducerCountry = c.String(maxLength: 4000),
                        RegNumber = c.String(maxLength: 500),
                        RegDate = c.DateTime(storeType: "date"),
                        RegEndDate = c.DateTime(),
                        ProxyName = c.String(maxLength: 500),
                        RequesterContacts = c.String(maxLength: 500),
                        LsTypeDicId = c.Guid(),
                        LsTypeName = c.String(maxLength: 4000),
                        MarginalPriceTn622 = c.Decimal(precision: 18, scale: 2),
                        BasePrice = c.Decimal(precision: 18, scale: 2),
                        BasePriceDate = c.DateTime(storeType: "date"),
                        TalkPrice = c.Decimal(precision: 18, scale: 2),
                        TalkPriceDate = c.DateTime(storeType: "date"),
                        BritishPrice = c.Decimal(precision: 18, scale: 2),
                        LimitCost = c.Decimal(precision: 18, scale: 2),
                        MinRefPrice2016 = c.Decimal(precision: 18, scale: 2),
                        FinalPrice = c.Decimal(precision: 18, scale: 2),
                        FinalFixPrice = c.Decimal(precision: 18, scale: 2),
                        FinalMarginalPriceTn = c.Decimal(precision: 18, scale: 2),
                        RequestOrderType = c.Int(),
                        RequestOrderYear = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.CreatedDate });
            
            CreateTable(
                "dbo.PriceProjectJournal",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        ManufacturerOrganizationId = c.Guid(nullable: false),
                        HolderOrganizationId = c.Guid(nullable: false),
                        ProxyOrganizationId = c.Guid(nullable: false),
                        IsConvention = c.Boolean(nullable: false),
                        IsSigned = c.Boolean(nullable: false),
                        Number = c.String(maxLength: 512),
                        OutgoingNumber = c.String(maxLength: 512),
                        OutgoingDate = c.DateTime(),
                        DoverennostNumber = c.String(maxLength: 500),
                        DoverennostCreatedDate = c.DateTime(storeType: "date"),
                        DoverennostExpiryDate = c.DateTime(storeType: "date"),
                        Filial = c.String(maxLength: 500),
                        NameKz = c.String(maxLength: 1000),
                        NameRu = c.String(maxLength: 1000),
                        RegNumber = c.String(maxLength: 500),
                        RegDate = c.DateTime(storeType: "date"),
                        LsTypeDicId = c.Guid(),
                        NameOriginal = c.String(maxLength: 500),
                        MnnRu = c.String(maxLength: 500),
                        MnnEn = c.String(maxLength: 500),
                        FormNameKz = c.String(maxLength: 500),
                        FormNameRu = c.String(maxLength: 500),
                        Dosage = c.String(maxLength: 500),
                        CountPackage = c.String(maxLength: 500),
                        Concentration = c.String(maxLength: 500),
                        CodeAtx = c.String(maxLength: 500),
                        IntroducingMethodDicId = c.Guid(),
                        ImnSecuryTypeDicId = c.Guid(),
                        RePriceDicId = c.Guid(),
                        ResultTypeDicId = c.Guid(),
                        IsPayed = c.Boolean(),
                        PayDate = c.DateTime(storeType: "date"),
                        StartDate = c.DateTime(storeType: "date"),
                        ContrDate = c.DateTime(storeType: "date"),
                        ConclusionDate = c.DateTime(storeType: "date"),
                        IsStageExpired = c.Boolean(),
                        ExpiredDayCount = c.Int(),
                        ExpertAz = c.String(maxLength: 200),
                        OutgoingDoc = c.String(maxLength: 200),
                        DayCount = c.Int(),
                        IsNewManufacrurer = c.Boolean(),
                        ManufacturerOrgName = c.String(maxLength: 500),
                        ApplicantOrgName = c.String(maxLength: 500),
                        CountryName = c.String(maxLength: 4000),
                        ListTypeDicId = c.Guid(),
                        MnnOrderNumber = c.Int(),
                        RePriceName = c.String(maxLength: 4000),
                        ListTypeName = c.String(maxLength: 6, unicode: false),
                        TypeValue = c.String(maxLength: 4000),
                        StatusValue = c.String(maxLength: 4000),
                        PriceProjectId = c.Guid(),
                        LeftDays = c.Int(),
                        ReturnDate = c.DateTime(),
                        PassedDays = c.Int(),
                        RequestOrderYear = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.Type, t.CreatedDate, t.Status, t.OwnerId, t.ManufacturerOrganizationId, t.HolderOrganizationId, t.ProxyOrganizationId, t.IsConvention, t.IsSigned });
            
            CreateTable(
                "dbo.PriceProjectsView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        ManufacturerOrganizationId = c.Guid(nullable: false),
                        HolderOrganizationId = c.Guid(nullable: false),
                        ProxyOrganizationId = c.Guid(nullable: false),
                        IsConvention = c.Boolean(nullable: false),
                        Number = c.String(maxLength: 500),
                        DoverennostNumber = c.String(maxLength: 500),
                        DoverennostCreatedDate = c.DateTime(storeType: "date"),
                        DoverennostExpiryDate = c.DateTime(storeType: "date"),
                        Filial = c.String(maxLength: 500),
                        NameKz = c.String(maxLength: 1000),
                        NameRu = c.String(maxLength: 1000),
                        NameChangedRu = c.String(maxLength: 1000),
                        RegNumber = c.String(maxLength: 500),
                        RegDate = c.DateTime(storeType: "date"),
                        LsTypeDicId = c.Guid(),
                        NameOriginal = c.String(maxLength: 500),
                        MnnRu = c.String(maxLength: 500),
                        MnnEn = c.String(maxLength: 500),
                        FormNameKz = c.String(maxLength: 500),
                        FormNameRu = c.String(maxLength: 500),
                        Dosage = c.String(maxLength: 500),
                        CountPackage = c.String(maxLength: 500),
                        Concentration = c.String(maxLength: 500),
                        CodeAtx = c.String(maxLength: 500),
                        IntroducingMethodDicId = c.Guid(),
                        ImnSecuryTypeDicId = c.Guid(),
                        RePriceDicId = c.Guid(),
                        OwnerLastName = c.String(maxLength: 4000),
                        OwnerFirstName = c.String(maxLength: 4000),
                        OwnerMiddleName = c.String(maxLength: 4000),
                        ManufacturerOrganizationName = c.String(maxLength: 500),
                        HolderOrganizationName = c.String(maxLength: 500),
                        ProxyOrganizationName = c.String(maxLength: 500),
                        LsTypeName = c.String(maxLength: 4000),
                        IntroducingMethodName = c.String(maxLength: 4000),
                        ImnSecuryTypeName = c.String(maxLength: 4000),
                        RePriceName = c.String(maxLength: 4000),
                        PriceProjectId = c.Guid(),
                        RegisterId = c.Int(),
                        RegisterDfId = c.Int(),
                        Volume = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.Type, t.CreatedDate, t.Status, t.OwnerId, t.ManufacturerOrganizationId, t.HolderOrganizationId, t.ProxyOrganizationId, t.IsConvention });
            
            CreateTable(
                "dbo.PriceRejectProjects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RegNumber = c.String(maxLength: 500),
                        RejectReasonDicId = c.Guid(),
                        DocumentId = c.Guid(),
                        RegisterId = c.Int(),
                        RegisterDfId = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        Name = c.String(maxLength: 500),
                        PriceProjectId = c.Guid(nullable: false),
                        CountryId = c.Guid(),
                        ManufacturerPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ManufacturerPriceCurrencyDicId = c.Guid(),
                        ManufacturerPriceNote = c.String(maxLength: 500),
                        LimitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LimitPriceCurrencyDicId = c.Guid(),
                        LimitPriceNote = c.String(maxLength: 500),
                        AvgOptPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgOptPriceCurrencyDicId = c.Guid(),
                        AvgOptPriceNote = c.String(maxLength: 500),
                        AvgRozPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgRozPriceCurrencyDicId = c.Guid(),
                        AvgRozPriceNote = c.String(maxLength: 500),
                        CipPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CipPriceCurrencyDicId = c.Guid(),
                        RefPriceTypeDicId = c.Guid(),
                        RefPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RefPriceCurrencyDicId = c.Guid(),
                        OwnerPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OwnerPriceCurrencyDicId = c.Guid(),
                        BritishPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitPriceCurrencyDicId = c.Guid(),
                        BritishCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgObkCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgRznCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgOptCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ZakupCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LimitCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinimalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(storeType: "date"),
                        MtPartsId = c.Int(),
                        OriginalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MarkupCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MarkupCostOpt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CalcDateStart = c.DateTime(storeType: "date"),
                        CalcDateEnd = c.DateTime(storeType: "date"),
                        RequestDate = c.DateTime(storeType: "date"),
                        Description = c.String(maxLength: 500),
                        IsIncluded = c.Boolean(nullable: false),
                        IsUnitPrice = c.Boolean(nullable: false),
                        IsManufacturerPrice = c.Boolean(nullable: false),
                        IsLimitPrice = c.Boolean(nullable: false),
                        IsAvgOptPrice = c.Boolean(nullable: false),
                        IsAvgRozPrice = c.Boolean(nullable: false),
                        FixRfkPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgRozPriceLink = c.String(maxLength: 1000),
                        AvgOptPriceLink = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PricesView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        PriceProjectId = c.Guid(nullable: false),
                        ManufacturerPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LimitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgOptPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgRozPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CipPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RefPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OwnerPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BritishPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BritishCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsIncluded = c.Boolean(nullable: false),
                        IsUnitPrice = c.Boolean(nullable: false),
                        IsAvgOptPrice = c.Boolean(nullable: false),
                        IsAvgRozPrice = c.Boolean(nullable: false),
                        IsLimitPrice = c.Boolean(nullable: false),
                        IsManufacturerPrice = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 500),
                        CountryId = c.Guid(),
                        ManufacturerPriceCurrencyDicId = c.Guid(),
                        ManufacturerPriceNote = c.String(maxLength: 500),
                        LimitPriceCurrencyDicId = c.Guid(),
                        LimitPriceNote = c.String(maxLength: 500),
                        AvgOptPriceCurrencyDicId = c.Guid(),
                        AvgOptPriceNote = c.String(maxLength: 500),
                        AvgRozPriceCurrencyDicId = c.Guid(),
                        AvgRozPriceNote = c.String(maxLength: 500),
                        CipPriceCurrencyDicId = c.Guid(),
                        RefPriceTypeDicId = c.Guid(),
                        RefPriceCurrencyDicId = c.Guid(),
                        OwnerPriceCurrencyDicId = c.Guid(),
                        CountryName = c.String(maxLength: 4000),
                        ManufacturerPriceCurrencyName = c.String(maxLength: 4000),
                        LimitPriceCurrencyName = c.String(maxLength: 4000),
                        AvgOptPriceCurrencyName = c.String(maxLength: 4000),
                        AvgRozPriceCurrencyName = c.String(maxLength: 4000),
                        CipPriceCurrencyName = c.String(maxLength: 4000),
                        RefPriceCurrencyName = c.String(maxLength: 4000),
                        RefPriceTypeName = c.String(maxLength: 4000),
                        OwnerPriceCurrencyName = c.String(maxLength: 4000),
                        UnitPriceCurrencyDicId = c.Guid(),
                        UnitPriceCurrencyName = c.String(maxLength: 4000),
                        CreatedDate = c.DateTime(storeType: "date"),
                        MtPartsId = c.Int(),
                        PartsName = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.Id, t.Type, t.PriceProjectId, t.ManufacturerPrice, t.LimitPrice, t.AvgOptPrice, t.AvgRozPrice, t.CipPrice, t.RefPrice, t.OwnerPrice, t.BritishPrice, t.UnitPrice, t.BritishCost, t.IsIncluded, t.IsUnitPrice, t.IsAvgOptPrice, t.IsAvgRozPrice, t.IsLimitPrice, t.IsManufacturerPrice });
            
            CreateTable(
                "dbo.PrismEnums",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Key = c.Int(),
                        Value = c.String(maxLength: 510),
                        Type = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrismReports",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 4000),
                        Category = c.String(maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                        Report = c.Binary(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectDocument",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsAdministrativeUse = c.Boolean(nullable: false),
                        IsAwaitingResponse = c.Boolean(nullable: false),
                        IsTradeSecret = c.Boolean(nullable: false),
                        ApplicantType = c.Int(nullable: false),
                        DocumentType = c.Int(nullable: false),
                        MonitoringType = c.Int(nullable: false),
                        PriorityType = c.Int(nullable: false),
                        StateType = c.Int(nullable: false),
                        AppendixCount = c.Int(nullable: false),
                        CopiesCount = c.Int(nullable: false),
                        PageCount = c.Int(nullable: false),
                        RepeatCount = c.Int(nullable: false),
                        SortNumber = c.Int(nullable: false),
                        OutgoingType = c.Int(nullable: false),
                        IsNotification = c.Boolean(nullable: false),
                        NotificationCount = c.Int(nullable: false),
                        IsAttachments = c.Boolean(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        AwaitingResponseDate = c.DateTime(),
                        DocumentDate = c.DateTime(),
                        OutgoingDate = c.DateTime(),
                        ProtocolDate = c.DateTime(),
                        MonitoringDate = c.DateTime(),
                        ApplicantAddress = c.String(maxLength: 512),
                        ApplicantEmail = c.String(maxLength: 4000),
                        ApplicantName = c.String(maxLength: 512),
                        ApplicantPhone = c.String(maxLength: 512),
                        BlankNumber = c.String(maxLength: 512),
                        CorrespondentsInfo = c.String(maxLength: 512),
                        Number = c.String(maxLength: 512),
                        OutgoingNumber = c.String(maxLength: 512),
                        SortingNumber = c.String(maxLength: 512),
                        SortingOutgoingNumber = c.String(maxLength: 512),
                        Note = c.String(maxLength: 4000),
                        Summary = c.String(maxLength: 4000),
                        AdministrativeTypeDictionaryValue = c.String(maxLength: 4000),
                        ApplicantCategoryDictionaryValue = c.String(maxLength: 4000),
                        CauseCitizenDictionaryValue = c.String(maxLength: 4000),
                        CitizenCategoryDictionaryValue = c.String(maxLength: 4000),
                        CitizenResultDictionaryValue = c.String(maxLength: 4000),
                        CitizenTypeDictionaryValue = c.String(maxLength: 4000),
                        DocumentKindDictionaryValue = c.String(maxLength: 4000),
                        FormDeliveryDictionaryValue = c.String(maxLength: 4000),
                        FormSendingDictionaryValue = c.String(maxLength: 4000),
                        KatoDictionaryValue = c.String(maxLength: 4000),
                        LanguageDictionaryValue = c.String(maxLength: 4000),
                        NomenclatureDictionaryValue = c.String(maxLength: 4000),
                        QuestionDesignDictionaryValue = c.String(maxLength: 4000),
                        SigningFormDictionaryValue = c.String(maxLength: 4000),
                        AgreementsValue = c.String(maxLength: 4000),
                        ExecutorsValue = c.String(maxLength: 4000),
                        ReadersValue = c.String(maxLength: 4000),
                        RecipientsValue = c.String(maxLength: 4000),
                        RegistratorValue = c.String(maxLength: 4000),
                        ResponsibleValue = c.String(maxLength: 4000),
                        SignerValue = c.String(maxLength: 4000),
                        CorrespondentsValue = c.String(maxLength: 4000),
                        MonitoringAuthorValue = c.String(maxLength: 4000),
                        MonitoringNote = c.String(maxLength: 4000),
                        AnswersValue = c.String(maxLength: 4000),
                        CompleteDocumentsValue = c.String(maxLength: 4000),
                        EditDocumentsValue = c.String(maxLength: 4000),
                        RepealDocumentsValue = c.String(maxLength: 4000),
                        DisplayName = c.String(maxLength: 4000),
                        AutoAnswersValue = c.String(maxLength: 4000),
                        AutoAnswersTempValue = c.String(maxLength: 4000),
                        AutoCompleteDocumentsValue = c.String(maxLength: 4000),
                        AutoEditDocumentsValue = c.String(maxLength: 4000),
                        AutoRepealDocumentsValue = c.String(maxLength: 4000),
                        AutoAwaitingResponseDate = c.DateTime(storeType: "date"),
                        AutoDocumentDate = c.DateTime(storeType: "date"),
                        AutoOutgoingDate = c.DateTime(storeType: "date"),
                        AutoProtocolDate = c.DateTime(storeType: "date"),
                        AutoMonitoringDate = c.DateTime(storeType: "date"),
                        FactExecutionDate = c.DateTime(),
                        FirstExecutionDate = c.DateTime(),
                        ExecutionDate = c.DateTime(),
                        AutoFactExecutionDate = c.DateTime(storeType: "date"),
                        AutoFirstExecutionDate = c.DateTime(storeType: "date"),
                        AutoExecutionDate = c.DateTime(storeType: "date"),
                        Counters = c.String(maxLength: 4000),
                        DocumentDictionaryTypeValue = c.String(maxLength: 40, unicode: false),
                        ResolutionValue = c.String(maxLength: 4000),
                        SourceValue = c.String(maxLength: 4000),
                        DestinationValue = c.String(maxLength: 4000),
                        OwnerValue = c.String(maxLength: 4000),
                        Country = c.String(maxLength: 4000),
                        Area = c.String(maxLength: 4000),
                        Postcode = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 4000),
                        Department = c.String(maxLength: 4000),
                        City = c.String(maxLength: 4000),
                        Address = c.String(maxLength: 4000),
                        NumberBill = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        SuperMainDocumentId = c.Guid(),
                        ModifiedUser = c.String(maxLength: 4000),
                        DateDispatch = c.DateTime(),
                        DispatchNote = c.String(maxLength: 4000),
                        Digest = c.String(maxLength: 4000),
                        Text = c.String(maxLength: 4000),
                        Recipient = c.String(maxLength: 4000),
                        QrCode = c.Binary(storeType: "image"),
                        FulfilledDate = c.DateTime(),
                        State = c.String(maxLength: 510),
                        Priority = c.String(maxLength: 510),
                    })
                .PrimaryKey(t => new { t.Id, t.IsDeleted, t.IsAdministrativeUse, t.IsAwaitingResponse, t.IsTradeSecret, t.ApplicantType, t.DocumentType, t.MonitoringType, t.PriorityType, t.StateType, t.AppendixCount, t.CopiesCount, t.PageCount, t.RepeatCount, t.SortNumber, t.OutgoingType, t.IsNotification, t.NotificationCount, t.IsAttachments, t.IsArchive });
            
            CreateTable(
                "dbo.ProjectsView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        TypeValue = c.String(maxLength: 4000),
                        Number = c.String(maxLength: 512),
                        OutgoingDate = c.DateTime(),
                        StausValue = c.String(maxLength: 4000),
                        NameRu = c.String(maxLength: 1000),
                        IsRegisterProject = c.Boolean(),
                        info = c.String(maxLength: 1506),
                    })
                .PrimaryKey(t => new { t.Id, t.Type, t.CreatedDate, t.Status, t.OwnerId });
            
            CreateTable(
                "dbo.PrtPrjs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        ProtocolId = c.Guid(nullable: false),
                        ResultDictionaryId = c.String(maxLength: 450),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrtPrjsView2",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProjectId = c.Guid(),
                        ProjectNumber = c.String(maxLength: 512),
                        ProjectDate = c.DateTime(),
                        ManufaturerName = c.String(maxLength: 500),
                        CountryName = c.String(maxLength: 4000),
                        ApplicantName = c.String(maxLength: 500),
                        ProtocolNumber = c.String(maxLength: 512),
                        ProtocolDate = c.DateTime(),
                        ProtocolSummary = c.String(maxLength: 4000),
                        ResultDictionaryId = c.String(maxLength: 450),
                        ProtocolId = c.Guid(),
                        Name = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrtPrjsView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProjectId = c.Guid(),
                        ProjectNumber = c.String(maxLength: 512),
                        ProjectDate = c.DateTime(),
                        ManufaturerName = c.String(maxLength: 500),
                        CountryName = c.String(maxLength: 4000),
                        ApplicantName = c.String(maxLength: 500),
                        ProtocolNumber = c.String(maxLength: 512),
                        ProtocolDate = c.DateTime(),
                        ProtocolSummary = c.String(maxLength: 4000),
                        ResultDictionaryId = c.String(maxLength: 450),
                        ProtocolId = c.Guid(),
                        Name = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReesrtObk",
                c => new
                    {
                        id = c.Long(nullable: false),
                        name = c.String(maxLength: 2000, unicode: false),
                        producer_name = c.String(maxLength: 1000, unicode: false),
                        country_name = c.String(maxLength: 150, unicode: false),
                        tnved_code = c.String(maxLength: 20, unicode: false),
                        kpved_code = c.String(maxLength: 20, unicode: false),
                        register_nd = c.String(maxLength: 2000, unicode: false),
                        cost = c.Decimal(storeType: "money"),
                        currency_name = c.String(maxLength: 50, unicode: false),
                        costExch = c.Decimal(precision: 38, scale: 7),
                        register_id = c.Long(),
                        drug_form_id = c.Long(),
                        reg_date = c.DateTime(storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ReestrDirectionToPayView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        DirectionToPayNumber = c.String(nullable: false, maxLength: 512),
                        TypeNameRu = c.String(maxLength: 2000),
                        TypeNameKz = c.String(maxLength: 2000),
                        DeclarationNumber = c.String(maxLength: 500),
                        ContractId = c.Guid(),
                        ContractNumber = c.String(maxLength: 500),
                        ContractDate = c.DateTime(storeType: "date"),
                        DeclarationNameRu = c.String(maxLength: 500),
                        DeclarationNameKz = c.String(maxLength: 500),
                        DeclarationNameEn = c.String(maxLength: 500),
                        DrugFormId = c.Int(),
                        DrugFormRu = c.String(maxLength: 500, unicode: false),
                        DrugFormKz = c.String(maxLength: 1000),
                        DosageRu = c.String(),
                        DosageKz = c.String(),
                        PayerOrganizationId = c.Guid(),
                        PayerNameRu = c.String(maxLength: 500),
                        PayerNameKz = c.String(maxLength: 500),
                        PayerNameEn = c.String(maxLength: 500),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        InvoiceNumber1C = c.String(maxLength: 512),
                        PaymentValue1C = c.Decimal(precision: 18, scale: 2),
                        ModifyDate = c.DateTime(),
                        PrimaryNumber = c.String(maxLength: 500),
                        PrimaryDate = c.DateTime(),
                        PrimaryTotalPrice = c.Decimal(precision: 18, scale: 2),
                        ZobNumber = c.String(maxLength: 500),
                        ZobDate = c.DateTime(),
                        ZobTotalPrice = c.Decimal(precision: 18, scale: 2),
                        PrimaryId = c.Guid(),
                        ZobId = c.Guid(),
                    })
                .PrimaryKey(t => new { t.Id, t.TypeId, t.CreatedDate, t.StatusId, t.OwnerId, t.DirectionToPayNumber });
            
            CreateTable(
                "dbo.Ref_Knfs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(maxLength: 4000),
                        GroupFarm = c.String(maxLength: 4000),
                        Name = c.String(maxLength: 4000),
                        Form = c.String(maxLength: 4000),
                        Number = c.String(maxLength: 4000),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Characteristic = c.String(maxLength: 4000),
                        Measure = c.String(maxLength: 4000),
                        Tn = c.String(maxLength: 4000),
                        DateExpiry = c.DateTime(storeType: "date"),
                        Dosage = c.String(maxLength: 4000),
                        CostTn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        State = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ref_Limits",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Code = c.String(maxLength: 4000),
                        GroupFarm = c.String(maxLength: 4000),
                        Name = c.String(maxLength: 4000),
                        Form = c.String(maxLength: 4000),
                        Number = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.Cost });
            
            CreateTable(
                "dbo.Ref_MarketPrices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1024),
                        DosageForm = c.Single(),
                        Producer = c.String(maxLength: 512),
                        Country = c.String(maxLength: 512),
                        Number = c.String(maxLength: 64),
                        RegistrationDatetime = c.DateTime(),
                        ExpireDatetime = c.DateTime(),
                        Price = c.Single(),
                        refSource = c.Long(nullable: false),
                        CreateDatetime = c.DateTime(),
                        refSourceName = c.String(maxLength: 50),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ref_PurchasePrices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SKP = c.String(maxLength: 32),
                        Name = c.String(maxLength: 512),
                        TradeName = c.String(maxLength: 512),
                        DosageForm = c.String(maxLength: 512),
                        Unit = c.String(maxLength: 128),
                        Packing = c.Single(),
                        Producer = c.String(maxLength: 512),
                        Price = c.Single(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ref_Sources",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Source = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Registeries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 4000),
                        Count = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Code = c.String(maxLength: 100),
                        Number = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsTake = c.Boolean(nullable: false),
                        Country = c.Guid(),
                        Type = c.Int(nullable: false),
                        OrganizationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RegisterOrderer2Views",
                c => new
                    {
                        regId = c.Int(nullable: false),
                        IntId = c.Int(),
                        Id = c.Guid(),
                        _int_name = c.String(maxLength: 255, unicode: false),
                        substance = c.String(),
                        volume = c.String(maxLength: 286),
                        dosage_comment = c.String(maxLength: 500, unicode: false),
                        name = c.String(maxLength: 3000, unicode: false),
                        _dosage_form_name = c.String(maxLength: 500, unicode: false),
                        concentration = c.String(maxLength: 500, unicode: false),
                        reg_number = c.String(maxLength: 50, unicode: false),
                        _producer_name = c.String(maxLength: 1000),
                        _country_name = c.String(maxLength: 255, unicode: false),
                        _atc_code = c.String(maxLength: 10, unicode: false),
                        dosage_value = c.String(maxLength: 281),
                        um = c.String(maxLength: 4000),
                        box_name1 = c.String(maxLength: 301, unicode: false),
                        box_name2 = c.String(maxLength: 301, unicode: false),
                        box_count = c.String(maxLength: 100),
                        dfId = c.Int(),
                    })
                .PrimaryKey(t => t.regId);
            
            CreateTable(
                "dbo.RegisterOrdererView",
                c => new
                    {
                        ReestrId = c.Int(nullable: false),
                        MnnName = c.String(nullable: false, maxLength: 1000, unicode: false),
                        DrugForm = c.String(nullable: false, maxLength: 500, unicode: false),
                        RegNumber = c.String(nullable: false, maxLength: 100, unicode: false),
                        RegDate = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        AtxCode = c.String(nullable: false, maxLength: 50, unicode: false),
                        Country = c.String(nullable: false, maxLength: 200, unicode: false),
                        Characteristic = c.String(maxLength: 652, unicode: false),
                        Concentration = c.String(maxLength: 50, unicode: false),
                        Dosage = c.String(maxLength: 100, unicode: false),
                        TradeName = c.String(maxLength: 4000, unicode: false),
                        RegDateExpire = c.DateTime(storeType: "smalldatetime"),
                        Manufacturer = c.String(maxLength: 1000, unicode: false),
                        Measure = c.String(maxLength: 50, unicode: false),
                        substance_count = c.Decimal(precision: 18, scale: 8),
                        unit_count = c.Int(),
                        volume = c.Decimal(precision: 9, scale: 3),
                        dosage_comment = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => new { t.ReestrId, t.MnnName, t.DrugForm, t.RegNumber, t.RegDate, t.AtxCode, t.Country });
            
            CreateTable(
                "dbo.RegisterProjectJournal",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        IsPatent = c.Boolean(nullable: false),
                        Dosage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsGrls = c.Boolean(nullable: false),
                        IsGmp = c.Boolean(nullable: false),
                        ManufacturePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsConvention = c.Boolean(nullable: false),
                        IsStageEnd = c.Boolean(),
                        EndDate = c.DateTime(storeType: "date"),
                        Number = c.String(maxLength: 512),
                        ContractId = c.Guid(),
                        AccelerationTypeDicId = c.Guid(),
                        AccelerationNumber = c.String(maxLength: 500),
                        AccelerationDate = c.DateTime(storeType: "date"),
                        AccelerationNote = c.String(maxLength: 500),
                        TradeName = c.String(maxLength: 500),
                        NameKz = c.String(maxLength: 500),
                        NameRu = c.String(maxLength: 500),
                        NameEn = c.String(maxLength: 500),
                        MnnKz = c.String(maxLength: 500),
                        MnnEn = c.String(maxLength: 500),
                        MnnRu = c.String(maxLength: 500),
                        PatentNumber = c.String(maxLength: 500),
                        PatentDate = c.DateTime(storeType: "date"),
                        PatentExpiryDate = c.DateTime(storeType: "date"),
                        LsFormNameKz = c.String(maxLength: 500),
                        LsFormNameRu = c.String(maxLength: 500),
                        AtxCode = c.String(maxLength: 500),
                        AtxNameKz = c.String(maxLength: 500),
                        AtxNameRu = c.String(maxLength: 500),
                        LsTypeDicId = c.Guid(),
                        LsType2DicId = c.Guid(),
                        OriginalName = c.String(maxLength: 500),
                        SaleTypeDicId = c.Guid(),
                        IntroducingMethodDicId = c.Guid(),
                        DosageMeasureTypeDicId = c.Guid(),
                        DosageNoteKz = c.String(maxLength: 500),
                        DosageNoteRu = c.String(maxLength: 500),
                        ConcentrationRu = c.String(maxLength: 500),
                        ConcentrationKz = c.String(maxLength: 500),
                        CompositionId = c.Guid(),
                        Transportation = c.String(maxLength: 500),
                        ManufactureTypeDicId = c.Guid(),
                        GmpExpiryDate = c.DateTime(storeType: "date"),
                        BestBefore = c.String(maxLength: 500),
                        BestBeforeMeasureTypeDicId = c.Guid(),
                        AppPeriod1BeginDate = c.DateTime(storeType: "date"),
                        AppPeriod1FinishDate = c.DateTime(storeType: "date"),
                        AppPeriod2BeginDate = c.DateTime(storeType: "date"),
                        AppPeriod2FinishDate = c.DateTime(storeType: "date"),
                        StorageConditions1 = c.String(maxLength: 500),
                        StorageConditions2 = c.String(maxLength: 500),
                        Barcode = c.String(maxLength: 500),
                        RefPrice = c.Decimal(precision: 18, scale: 2),
                        RegPrice = c.Decimal(precision: 18, scale: 2),
                        SecureDocument = c.String(maxLength: 500),
                        SecureDocumentNumber = c.String(maxLength: 500),
                        SecureDocumentDate = c.DateTime(storeType: "date"),
                        SecureDocumentExpiryDate = c.DateTime(storeType: "date"),
                        RegDocNumber = c.String(maxLength: 500),
                        RegDocDate = c.DateTime(storeType: "date"),
                        RegDocExpiryDate = c.DateTime(storeType: "date"),
                        RegDocNormativeNumber = c.String(maxLength: 500),
                        ResultTypeDicId = c.Guid(),
                        IsPayed = c.Boolean(),
                        PayDate = c.DateTime(storeType: "date"),
                        StartDate = c.DateTime(storeType: "date"),
                        ContrDate = c.DateTime(storeType: "date"),
                        ConclusionDate = c.DateTime(storeType: "date"),
                        IsStageExpired = c.Boolean(),
                        ExpiredDayCount = c.Int(),
                        ExpertAz = c.String(maxLength: 200),
                        OutgoingDoc = c.String(maxLength: 200),
                        DayCount = c.Int(),
                        IsNewManufacrurer = c.Boolean(),
                        RegisterType = c.String(maxLength: 4000),
                        ManufaturerName = c.String(maxLength: 500),
                        CountryName = c.String(maxLength: 4000),
                        ApplicantName = c.String(maxLength: 500),
                        Classification = c.String(maxLength: 4000),
                        Mnn = c.String(maxLength: 4000),
                        DosageMeasureTypeName = c.String(maxLength: 4000),
                        ExpertiseStage = c.String(maxLength: 4000),
                        ResponsibleId = c.String(maxLength: 510),
                        ResponsibleValue = c.String(maxLength: 4000),
                        ResultTypeName = c.String(maxLength: 4000),
                        StatusValue = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.Type, t.CreatedDate, t.Status, t.OwnerId, t.IsPatent, t.Dosage, t.IsGrls, t.IsGmp, t.ManufacturePrice, t.IsConvention });
            
            CreateTable(
                "dbo.RegisterProjects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        Number = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        ContractId = c.Guid(),
                        AccelerationTypeDicId = c.Guid(),
                        AccelerationNumber = c.String(maxLength: 500),
                        AccelerationDate = c.DateTime(storeType: "date"),
                        AccelerationNote = c.String(maxLength: 500),
                        NameRu = c.String(maxLength: 500),
                        NameKz = c.String(maxLength: 500),
                        NameEn = c.String(maxLength: 500),
                        MnnKz = c.String(maxLength: 500),
                        MnnEn = c.String(maxLength: 500),
                        MnnRu = c.String(maxLength: 500),
                        IsPatent = c.Boolean(nullable: false),
                        PatentNumber = c.String(maxLength: 500),
                        PatentDate = c.DateTime(storeType: "date"),
                        PatentExpiryDate = c.DateTime(storeType: "date"),
                        LsFormNameKz = c.String(maxLength: 500),
                        LsFormNameRu = c.String(maxLength: 500),
                        AtxCode = c.String(maxLength: 500),
                        AtxNameKz = c.String(maxLength: 500),
                        AtxNameRu = c.String(maxLength: 500),
                        LsTypeDicId = c.Guid(),
                        LsType2DicId = c.Guid(),
                        OriginalName = c.String(maxLength: 500),
                        SaleTypeDicId = c.Guid(),
                        IntroducingMethodDicId = c.Guid(),
                        Dosage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DosageMeasureTypeDicId = c.Guid(),
                        DosageNoteKz = c.String(maxLength: 500),
                        DosageNoteRu = c.String(maxLength: 500),
                        ConcentrationRu = c.String(maxLength: 500),
                        ConcentrationKz = c.String(maxLength: 500),
                        CompositionId = c.Guid(),
                        IsGrls = c.Boolean(nullable: false),
                        Transportation = c.String(maxLength: 500),
                        ManufactureTypeDicId = c.Guid(),
                        IsGmp = c.Boolean(nullable: false),
                        GmpExpiryDate = c.DateTime(storeType: "date"),
                        BestBefore = c.String(maxLength: 500),
                        BestBeforeMeasureTypeDicId = c.Guid(),
                        AppPeriod1BeginDate = c.DateTime(storeType: "date"),
                        AppPeriod1FinishDate = c.DateTime(storeType: "date"),
                        AppPeriod2BeginDate = c.DateTime(storeType: "date"),
                        AppPeriod2FinishDate = c.DateTime(storeType: "date"),
                        StorageConditions1 = c.String(maxLength: 500),
                        StorageConditions2 = c.String(maxLength: 500),
                        Barcode = c.String(maxLength: 500),
                        ManufacturePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RefPrice = c.Decimal(precision: 18, scale: 2),
                        RegPrice = c.Decimal(precision: 18, scale: 2),
                        SecureDocument = c.String(maxLength: 500),
                        SecureDocumentNumber = c.String(maxLength: 500),
                        SecureDocumentDate = c.DateTime(storeType: "date"),
                        SecureDocumentExpiryDate = c.DateTime(storeType: "date"),
                        IsConvention = c.Boolean(nullable: false),
                        RegDocNumber = c.String(maxLength: 500),
                        RegDocDate = c.DateTime(storeType: "date"),
                        RegDocExpiryDate = c.DateTime(storeType: "date"),
                        RegDocNormativeNumber = c.String(maxLength: 500),
                        IsStageEnd = c.Boolean(),
                        EndDate = c.DateTime(storeType: "date"),
                        ResultTypeDicId = c.Guid(),
                        IsPayed = c.Boolean(),
                        PayDate = c.DateTime(storeType: "date"),
                        StartDate = c.DateTime(storeType: "date"),
                        ContrDate = c.DateTime(storeType: "date"),
                        ConclusionDate = c.DateTime(storeType: "date"),
                        IsStageExpired = c.Boolean(),
                        ExpiredDayCount = c.Int(),
                        ExpertAz = c.String(maxLength: 200),
                        OutgoingDoc = c.String(maxLength: 200),
                        DayCount = c.Int(),
                        IsNewManufacrurer = c.Boolean(),
                        AppPeriodOpen = c.String(maxLength: 500),
                        AppPeriodOpenMeasureDicId = c.Guid(),
                        AppPeriodMix = c.String(maxLength: 500),
                        AppPeriodMixMeasureDicId = c.Guid(),
                        ReasonDicId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RegisterProjectsView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        IsPatent = c.Boolean(nullable: false),
                        Dosage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsGrls = c.Boolean(nullable: false),
                        IsGmp = c.Boolean(nullable: false),
                        ManufacturePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsConvention = c.Boolean(nullable: false),
                        Number = c.String(maxLength: 512),
                        ContractId = c.Guid(),
                        AccelerationTypeDicId = c.Guid(),
                        AccelerationNumber = c.String(maxLength: 500),
                        AccelerationDate = c.DateTime(storeType: "date"),
                        AccelerationNote = c.String(maxLength: 500),
                        NameRu = c.String(maxLength: 500),
                        NameKz = c.String(maxLength: 500),
                        NameEn = c.String(maxLength: 500),
                        MnnKz = c.String(maxLength: 500),
                        MnnEn = c.String(maxLength: 500),
                        MnnRu = c.String(maxLength: 500),
                        PatentNumber = c.String(maxLength: 500),
                        PatentDate = c.DateTime(storeType: "date"),
                        PatentExpiryDate = c.DateTime(storeType: "date"),
                        LsFormNameKz = c.String(maxLength: 500),
                        LsFormNameRu = c.String(maxLength: 500),
                        AtxCode = c.String(maxLength: 500),
                        AtxNameKz = c.String(maxLength: 500),
                        AtxNameRu = c.String(maxLength: 500),
                        LsTypeDicId = c.Guid(),
                        LsType2DicId = c.Guid(),
                        OriginalName = c.String(maxLength: 500),
                        SaleTypeDicId = c.Guid(),
                        IntroducingMethodDicId = c.Guid(),
                        DosageMeasureTypeDicId = c.Guid(),
                        DosageNoteKz = c.String(maxLength: 500),
                        DosageNoteRu = c.String(maxLength: 500),
                        ConcentrationRu = c.String(maxLength: 500),
                        ConcentrationKz = c.String(maxLength: 500),
                        CompositionId = c.Guid(),
                        Transportation = c.String(maxLength: 500),
                        ManufactureTypeDicId = c.Guid(),
                        GmpExpiryDate = c.DateTime(storeType: "date"),
                        BestBefore = c.String(maxLength: 500),
                        BestBeforeMeasureTypeDicId = c.Guid(),
                        AppPeriod1BeginDate = c.DateTime(storeType: "date"),
                        AppPeriod1FinishDate = c.DateTime(storeType: "date"),
                        AppPeriod2BeginDate = c.DateTime(storeType: "date"),
                        AppPeriod2FinishDate = c.DateTime(storeType: "date"),
                        StorageConditions1 = c.String(maxLength: 500),
                        StorageConditions2 = c.String(maxLength: 500),
                        Barcode = c.String(maxLength: 500),
                        RefPrice = c.Decimal(precision: 18, scale: 2),
                        RegPrice = c.Decimal(precision: 18, scale: 2),
                        SecureDocument = c.String(maxLength: 500),
                        SecureDocumentNumber = c.String(maxLength: 500),
                        SecureDocumentDate = c.DateTime(storeType: "date"),
                        SecureDocumentExpiryDate = c.DateTime(storeType: "date"),
                        RegDocNumber = c.String(maxLength: 500),
                        RegDocDate = c.DateTime(storeType: "date"),
                        RegDocExpiryDate = c.DateTime(storeType: "date"),
                        RegDocNormativeNumber = c.String(maxLength: 500),
                        OwnerLastName = c.String(maxLength: 4000),
                        OwnerFirstName = c.String(maxLength: 4000),
                        OwnerMiddleName = c.String(maxLength: 4000),
                        AccelerationTypeName = c.String(maxLength: 4000),
                        LsTypeDicName = c.String(maxLength: 4000),
                        LsType2Name = c.String(maxLength: 4000),
                        SaleTypeName = c.String(maxLength: 4000),
                        IntroductionMethodName = c.String(maxLength: 4000),
                        DosageMeasureTypeName = c.String(maxLength: 4000),
                        ManufactureTypeName = c.String(maxLength: 4000),
                        BestBeforeMeasureTypeName = c.String(maxLength: 4000),
                        AppPeriodOpen = c.String(maxLength: 500),
                        AppPeriodOpenMeasureDicId = c.Guid(),
                        AppPeriodMix = c.String(maxLength: 500),
                        AppPeriodMixMeasureDicId = c.Guid(),
                        AppPeriodOpenMeasureName = c.String(maxLength: 4000),
                        AppPeriodMixMeasureName = c.String(maxLength: 4000),
                        StatusValue = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.Type, t.CreatedDate, t.Status, t.OwnerId, t.IsPatent, t.Dosage, t.IsGrls, t.IsGmp, t.ManufacturePrice, t.IsConvention });
            
            CreateTable(
                "dbo.RequestList",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ReestrId = c.Int(),
                        MnnName = c.String(maxLength: 4000),
                        Characteristic = c.String(maxLength: 4000),
                        DrugForm = c.String(maxLength: 4000),
                        Concentration = c.String(maxLength: 4000),
                        Dosage = c.String(maxLength: 4000),
                        TradeName = c.String(maxLength: 4000),
                        RegNumber = c.String(maxLength: 4000),
                        RegDate = c.DateTime(storeType: "smalldatetime"),
                        RegDateExpire = c.DateTime(storeType: "smalldatetime"),
                        AtxCode = c.String(maxLength: 4000),
                        Manufacturer = c.String(maxLength: 4000),
                        Measure = c.String(maxLength: 4000),
                        Type = c.Int(),
                        State = c.Int(),
                        LimitPriceTn = c.Decimal(precision: 18, scale: 2),
                        LimitPriceMnn = c.Decimal(precision: 18, scale: 2),
                        Country = c.String(maxLength: 4000),
                        Number = c.Int(nullable: false),
                        Applicant = c.String(maxLength: 4000),
                        substance_count = c.String(maxLength: 4000),
                        unit_count = c.String(maxLength: 4000),
                        volume = c.String(maxLength: 4000),
                        dosage_comment = c.String(maxLength: 4000),
                        Mark = c.String(maxLength: 4000),
                        RegisterDfId = c.Int(),
                        RequestOrderId = c.Guid(),
                        box_count = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RequestOrders", t => t.RequestOrderId)
                .Index(t => t.RequestOrderId);
            
            CreateTable(
                "dbo.RequestOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        OrderType = c.Int(nullable: false),
                        OrderYear = c.Int(nullable: false),
                        OrderNumber = c.String(maxLength: 500),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequestListView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 5),
                        ReestrId = c.Int(),
                        MnnName = c.String(maxLength: 4000),
                        Characteristic = c.String(maxLength: 4000),
                        DrugForm = c.String(maxLength: 4000),
                        Concentration = c.String(maxLength: 4000),
                        Dosage = c.String(maxLength: 4000),
                        TradeName = c.String(maxLength: 4000),
                        RegNumber = c.String(maxLength: 4000),
                        RegDate = c.DateTime(storeType: "smalldatetime"),
                        RegDateExpire = c.DateTime(storeType: "smalldatetime"),
                        AtxCode = c.String(maxLength: 4000),
                        Manufacturer = c.String(maxLength: 4000),
                        Measure = c.String(maxLength: 4000),
                        Type = c.Int(),
                        State = c.Int(),
                        LimitPriceTn = c.Decimal(precision: 18, scale: 2),
                        LimitPriceMnn = c.Decimal(precision: 18, scale: 2),
                        Country = c.String(maxLength: 4000),
                        Applicant = c.String(maxLength: 4000),
                        substance_count = c.String(maxLength: 4000),
                        unit_count = c.String(maxLength: 4000),
                        volume = c.String(maxLength: 4000),
                        dosage_comment = c.String(maxLength: 4000),
                        Mark = c.String(maxLength: 4000),
                        Reason = c.String(maxLength: 4000),
                        RegisterDfId = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.Number, t.Status });
            
            CreateTable(
                "dbo.RequestOrderListView",
                c => new
                    {
                        OrderId = c.Guid(nullable: false),
                        OrderType = c.Int(nullable: false),
                        OrderYear = c.Int(nullable: false),
                        Id = c.Guid(nullable: false),
                        Number = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 5),
                        OrderNumber = c.String(maxLength: 500),
                        ReestrId = c.Int(),
                        MnnName = c.String(maxLength: 4000),
                        Characteristic = c.String(maxLength: 4000),
                        DrugForm = c.String(maxLength: 4000),
                        Concentration = c.String(maxLength: 4000),
                        Dosage = c.String(maxLength: 4000),
                        TradeName = c.String(maxLength: 4000),
                        RegNumber = c.String(maxLength: 4000),
                        RegDate = c.DateTime(storeType: "smalldatetime"),
                        RegDateExpire = c.DateTime(storeType: "smalldatetime"),
                        AtxCode = c.String(maxLength: 4000),
                        Manufacturer = c.String(maxLength: 4000),
                        Measure = c.String(maxLength: 4000),
                        State = c.Int(),
                        LimitPriceTn = c.Decimal(precision: 18, scale: 2),
                        LimitPriceMnn = c.Decimal(precision: 18, scale: 2),
                        Country = c.String(maxLength: 4000),
                        Applicant = c.String(maxLength: 4000),
                        substance_count = c.String(maxLength: 4000),
                        unit_count = c.String(maxLength: 4000),
                        volume = c.String(maxLength: 4000),
                        dosage_comment = c.String(maxLength: 4000),
                        Mark = c.String(maxLength: 4000),
                        Reason = c.String(maxLength: 4000),
                        RegisterDfId = c.Int(),
                        box_count = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => new { t.OrderId, t.OrderType, t.OrderYear, t.Id, t.Number, t.Status });
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(maxLength: 300),
                        DateStart = c.DateTime(storeType: "date"),
                        CategoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UniqueName = c.String(maxLength: 100),
                        Type = c.String(maxLength: 100),
                        Value = c.String(),
                        Name = c.String(maxLength: 100),
                        Discription = c.String(maxLength: 510),
                        Rank = c.Int(nullable: false),
                        UserId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SignDocument",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ClassName = c.String(maxLength: 500),
                        ObjectId = c.String(maxLength: 50, unicode: false),
                        CreatedTime = c.DateTime(nullable: false),
                        EmployeeId = c.Guid(),
                        SignXml = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sr_register_ordered",
                c => new
                    {
                        reestr = c.Int(nullable: false),
                        law_number = c.String(maxLength: 50, unicode: false),
                        law_date = c.DateTime(storeType: "smalldatetime"),
                        law_description = c.String(maxLength: 200, unicode: false),
                        reestr_drug_type = c.Short(nullable: false),
                        registration_number = c.String(nullable: false, maxLength: 100, unicode: false),
                        reg_date = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        val_date = c.DateTime(storeType: "smalldatetime"),
                        ats = c.Int(nullable: false),
                        mnn = c.Int(nullable: false),
                        pharm_name = c.Int(nullable: false),
                        pharm_form = c.Int(nullable: false),
                        firm = c.Int(nullable: false),
                        country = c.Short(nullable: false),
                        package = c.Int(),
                        dose = c.Int(),
                        concentrant = c.Int(),
                        recept = c.Boolean(),
                        content = c.String(maxLength: 8000, unicode: false),
                        in_control = c.Short(),
                        list_imp_life = c.Boolean(),
                        parent = c.Int(),
                        registration_type = c.Short(),
                        expired = c.Decimal(storeType: "money"),
                        structure = c.String(maxLength: 8000, unicode: false),
                        volume_of_fill = c.Int(),
                        npp = c.Int(),
                        val_year = c.Short(),
                        primary_packing = c.Short(),
                        secondary_packing = c.Short(),
                        firm_packing = c.Int(),
                        country_packing = c.Short(),
                        firm_owner = c.Int(),
                        country_owner = c.Short(),
                        reestr_drug_type_code = c.String(nullable: false, maxLength: 50, unicode: false),
                        reestr_drug_type_name = c.String(nullable: false, maxLength: 50, unicode: false),
                        ats_code = c.String(nullable: false, maxLength: 50, unicode: false),
                        ats_name = c.String(nullable: false, maxLength: 500, unicode: false),
                        MNN_NAME = c.String(nullable: false, maxLength: 1000, unicode: false),
                        PHARM_NAMES_NAME = c.String(maxLength: 4000, unicode: false),
                        pharm_name_category = c.Short(nullable: false),
                        PHARM_NAME_CATEGORY_NAME = c.String(nullable: false, maxLength: 200, unicode: false),
                        PHARM_FORM_NAME = c.String(nullable: false, maxLength: 500, unicode: false),
                        PHARM_FORM_NAME_KAZ = c.String(maxLength: 500, unicode: false),
                        FIRM_NAME = c.String(maxLength: 1000, unicode: false),
                        COUNTRY_NAME = c.String(nullable: false, maxLength: 200, unicode: false),
                        PACKAGES_NAME = c.String(maxLength: 50, unicode: false),
                        DOSES_NAME = c.String(maxLength: 50, unicode: false),
                        DOSES_UNIT_NAME = c.String(maxLength: 50, unicode: false),
                        CONC_NAME = c.String(maxLength: 50, unicode: false),
                        IN_CONTROL_NAME = c.String(maxLength: 100, unicode: false),
                        color = c.String(maxLength: 50, unicode: false),
                        REGISTRATION_TYPE_NAME = c.String(maxLength: 50, unicode: false),
                        VOF_NAME = c.String(maxLength: 200, unicode: false),
                        VOF_UNIT_NAME = c.String(maxLength: 50, unicode: false),
                        country_type = c.String(maxLength: 50, unicode: false),
                        ats_parent = c.Int(),
                        reestr_text = c.String(maxLength: 8000, unicode: false),
                        pharm_form_union_name = c.String(maxLength: 500, unicode: false),
                        package_union = c.Decimal(storeType: "smallmoney"),
                        dose_union = c.String(maxLength: 100, unicode: false),
                        conc_union = c.String(maxLength: 50, unicode: false),
                        vof_union = c.String(maxLength: 100, unicode: false),
                        primary_packing_name = c.String(maxLength: 1000, unicode: false),
                        secondary_packing_name = c.String(maxLength: 1000, unicode: false),
                        firm_owner_name = c.String(maxLength: 1000, unicode: false),
                        firm_packing_name = c.String(maxLength: 1000, unicode: false),
                        country_owner_name = c.String(maxLength: 200, unicode: false),
                        country_packing_name = c.String(maxLength: 200, unicode: false),
                        pkg_txt = c.String(maxLength: 51, unicode: false),
                        license_owner_firm_name = c.String(maxLength: 1000, unicode: false),
                        license_owner_country_name = c.String(maxLength: 200, unicode: false),
                        firm_license_owner = c.Int(),
                        country_license_owner = c.Short(),
                        reestr_change_type = c.Short(),
                        change_type_name = c.String(maxLength: 100, unicode: false),
                        expire_date = c.DateTime(storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.reestr);
            
            CreateTable(
                "dbo.SrProducerView",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(maxLength: 1000),
                        NameEng = c.String(maxLength: 500, unicode: false),
                        NameKz = c.String(maxLength: 1000),
                        FormTypeId = c.Long(),
                        FormTypeFullName = c.String(maxLength: 255, unicode: false),
                        FormTypeFullNameKz = c.String(maxLength: 510),
                        FormTypeName = c.String(maxLength: 255, unicode: false),
                        FormTypeNameKz = c.String(maxLength: 510),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SrReestrView",
                c => new
                    {
                        n = c.Int(nullable: false),
                        id = c.Int(nullable: false),
                        reg_date = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        reg_action_id = c.Int(nullable: false),
                        type = c.Int(nullable: false),
                        df_id = c.Int(),
                        _producer_name_kz = c.String(maxLength: 1000),
                        _producer_name = c.String(maxLength: 1000),
                        _producer_name_en = c.String(maxLength: 500, unicode: false),
                        _country_name = c.String(maxLength: 255, unicode: false),
                        _country_Id = c.Guid(),
                        name_kz = c.String(maxLength: 4000),
                        name = c.String(maxLength: 3000, unicode: false),
                        reg_number = c.String(maxLength: 50, unicode: false),
                        reg_action_name = c.String(maxLength: 50, unicode: false),
                        reg_action_name_kz = c.String(maxLength: 100),
                        _int_name = c.String(maxLength: 255, unicode: false),
                        SubstanceName = c.String(),
                        _dosage_form_name = c.String(maxLength: 500, unicode: false),
                        _dosage_form_name_kz = c.String(maxLength: 1000),
                        dosage_value = c.String(maxLength: 281),
                        concentration = c.String(maxLength: 500, unicode: false),
                        _atc_code = c.String(maxLength: 10, unicode: false),
                        description = c.String(maxLength: 15),
                        um = c.String(maxLength: 4000),
                        umId = c.Guid(),
                        expiration_date = c.DateTime(storeType: "smalldatetime"),
                        type_name = c.String(maxLength: 50, unicode: false),
                        type_name_kz = c.String(maxLength: 100),
                        volume = c.String(maxLength: 286),
                        volume_measure = c.String(maxLength: 255, unicode: false),
                        owner_name_kz = c.String(maxLength: 1000),
                        owner_name_ru = c.String(maxLength: 1000),
                        owner_name_en = c.String(maxLength: 500, unicode: false),
                        box_name1 = c.String(maxLength: 301, unicode: false),
                        box_name2 = c.String(maxLength: 301, unicode: false),
                        box_count = c.String(maxLength: 100),
                        owner_country_id = c.Guid(),
                        degree_risk_id = c.Guid(),
                    })
                .PrimaryKey(t => new { t.n, t.id, t.reg_date, t.reg_action_id, t.type });
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_act_Application_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_exp_Contracts_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_lims_ApplicationEAdmissions_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_lims_ApplicationEExploitation_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_lims_ApplicationElements_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_lims_Applications_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_lims_ContractProducts_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_lims_Contracts_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_primary_ApplicationElements_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_primary_ApplicationPartPaymentState_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_primary_ApplicationPaymentState_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_primary_ApplicationRefuseState_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_primary_Applications_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_primary_DrugPackage_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_primary_PriceListElements_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_trl_Application_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_trl_ApplicationPaymentState_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_I1c_trl_ApplicationRefuseState_tracking",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        update_scope_local_id = c.Int(),
                        scope_update_peer_key = c.Int(),
                        scope_update_peer_timestamp = c.Long(),
                        local_update_peer_key = c.Int(nullable: false),
                        local_update_peer_timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"),
                        create_scope_local_id = c.Int(),
                        scope_create_peer_key = c.Int(),
                        scope_create_peer_timestamp = c.Long(),
                        local_create_peer_key = c.Int(nullable: false),
                        local_create_peer_timestamp = c.Long(nullable: false),
                        sync_row_is_tombstone = c.Int(nullable: false),
                        restore_timestamp = c.Long(),
                        last_change_datetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DbSync1C.sync_1c_schema_info",
                c => new
                    {
                        schema_major_version = c.Int(nullable: false),
                        schema_minor_version = c.Int(nullable: false),
                        schema_extended_info = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.schema_major_version, t.schema_minor_version });
            
            CreateTable(
                "DbSync1C.sync_1c_scope_config",
                c => new
                    {
                        config_id = c.Guid(nullable: false),
                        config_data = c.String(nullable: false, storeType: "xml"),
                        scope_status = c.String(maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.config_id);
            
            CreateTable(
                "DbSync1C.sync_1c_scope_info",
                c => new
                    {
                        sync_scope_name = c.String(nullable: false, maxLength: 100),
                        scope_local_id = c.Int(nullable: false, identity: true),
                        scope_id = c.Guid(nullable: false),
                        scope_sync_knowledge = c.Binary(),
                        scope_tombstone_cleanup_knowledge = c.Binary(),
                        scope_timestamp = c.Binary(fixedLength: true, timestamp: true, storeType: "timestamp"),
                        scope_config_id = c.Guid(),
                        scope_restore_count = c.Int(nullable: false),
                        scope_user_comment = c.String(),
                    })
                .PrimaryKey(t => t.sync_scope_name);
            
            CreateTable(
                "dbo.TempTmc",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Number = c.String(),
                        Code = c.String(),
                        Name = c.String(),
                        Count = c.String(),
                        CountFact = c.String(),
                        Unit = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TmcApplicationComments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDate = c.DateTime(storeType: "date"),
                        Comment = c.String(),
                        ApplicationId = c.Guid(nullable: false),
                        CreateEmployeeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TmcCountStateView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CountFact = c.Decimal(nullable: false, precision: 18, scale: 6),
                        CountConvert = c.Decimal(nullable: false, precision: 18, scale: 6),
                        IssuedCount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        CreatedDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Code = c.String(maxLength: 450),
                        UsedCount = c.Decimal(precision: 38, scale: 6),
                    })
                .PrimaryKey(t => new { t.Id, t.CountFact, t.CountConvert, t.IssuedCount, t.CreatedDate });
            
            CreateTable(
                "dbo.TmcInView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, storeType: "date"),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        StateType = c.Int(nullable: false),
                        IsFullDelivery = c.Boolean(nullable: false),
                        IsScan = c.Boolean(nullable: false),
                        StateTypeValue = c.String(maxLength: 24),
                        OwnerEmployeeId = c.Guid(),
                        OwnerEmployeeValue = c.String(maxLength: 4000),
                        Provider = c.String(maxLength: 450),
                        ProviderBin = c.String(maxLength: 450),
                        ContractNumber = c.String(maxLength: 450),
                        ContractDate = c.DateTime(storeType: "date"),
                        LastDeliveryDate = c.DateTime(storeType: "date"),
                        IsFullDeliveryValue = c.String(maxLength: 3),
                        PowerOfAttorney = c.String(maxLength: 450),
                        ExecutorEmployeeValue = c.String(maxLength: 4000),
                        AgreementEmployeeValue = c.String(maxLength: 4000),
                        ExecutorEmployeeId = c.Guid(),
                        AgreementEmployeeId = c.Guid(),
                        IsScanValue = c.String(maxLength: 3),
                        Func1 = c.Int(),
                        AccountantEmployeeId = c.Guid(),
                        AccountantEmployeeValue = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.CreatedDate, t.CreatedEmployeeId, t.StateType, t.IsFullDelivery, t.IsScan });
            
            CreateTable(
                "dbo.TmcOffs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        StateType = c.Int(nullable: false),
                        TmcOutId = c.Guid(),
                        Note = c.String(maxLength: 450),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 6),
                        ExpertiseStatementId = c.Guid(),
                        ExpertiseStatementNumber = c.String(maxLength: 512),
                        ExpertiseStatementTypeStr = c.String(maxLength: 512),
                        TmcId = c.Guid(),
                        DeleteDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TmcOffStateView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        StateType = c.Int(nullable: false),
                        UsedCount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        RequstedCount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        ReceivedCount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        TmcId = c.Guid(nullable: false),
                        TmcCreateDate = c.DateTime(nullable: false, storeType: "date"),
                        TmcCreateEmployeeId = c.Guid(nullable: false),
                        RequestedCount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        CountFact = c.Decimal(nullable: false, precision: 18, scale: 6),
                        ConvertedCount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        ResidueCount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        UseReason = c.String(maxLength: 450),
                        ExpertiseStatementId = c.Guid(),
                        ExpertiseStatementNumber = c.String(maxLength: 512),
                        ExpertiseStatementTypeStr = c.String(maxLength: 512),
                        Number = c.String(maxLength: 450),
                        Name = c.String(),
                        Code = c.String(maxLength: 450),
                        Manufacturer = c.String(maxLength: 450),
                        Serial = c.String(maxLength: 450),
                        MeasureTypeDicId = c.Guid(),
                        MeasureTypeConvertDicId = c.Guid(),
                        ManufactureDate = c.DateTime(storeType: "date"),
                        ExpiryDate = c.DateTime(storeType: "date"),
                        PackageDicId = c.Guid(),
                        TmcTypeDicId = c.Guid(),
                        OwnerEmployeeId = c.Guid(),
                        ReceivingDate = c.DateTime(storeType: "date"),
                        WriteoffDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.Id, t.CreatedDate, t.CreatedEmployeeId, t.StateType, t.UsedCount, t.RequstedCount, t.ReceivedCount, t.TmcId, t.TmcCreateDate, t.TmcCreateEmployeeId, t.RequestedCount, t.CountFact, t.ConvertedCount, t.ResidueCount });
            
            CreateTable(
                "dbo.TmcOffView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        StateType = c.Int(nullable: false),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 6),
                        StateTypeValue = c.String(nullable: false, maxLength: 11, unicode: false),
                        CreatedEmployeeValue = c.String(maxLength: 4000),
                        TmcOutId = c.Guid(),
                        Note = c.String(maxLength: 450),
                        ExpertiseStatementId = c.Guid(),
                        ExpertiseStatementNumber = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => new { t.Id, t.CreatedDate, t.CreatedEmployeeId, t.StateType, t.Count, t.StateTypeValue });
            
            CreateTable(
                "dbo.TmcOutCounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TmcOutId = c.Guid(nullable: false),
                        TmcId = c.Guid(nullable: false),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 6),
                        CountFact = c.Decimal(nullable: false, precision: 18, scale: 6),
                        StateType = c.Int(nullable: false),
                        Note = c.String(maxLength: 450),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TmcOutCountView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TmcOutId = c.Guid(nullable: false),
                        TmcId = c.Guid(nullable: false),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 6),
                        CountFact = c.Decimal(nullable: false, precision: 18, scale: 6),
                        StateType = c.Int(nullable: false),
                        StateTypeValue = c.String(nullable: false, maxLength: 6, unicode: false),
                        CountActual = c.Decimal(precision: 38, scale: 6),
                        Name = c.String(),
                        MeasureTypeConvertDicId = c.Guid(),
                        MeasureTypeConvertDicValue = c.String(maxLength: 4000),
                        Note = c.String(maxLength: 450),
                        OwnerEmployeeValue = c.String(maxLength: 4000),
                        StorageDicValue = c.String(maxLength: 4000),
                        Safe = c.String(maxLength: 450),
                        Rack = c.String(maxLength: 450),
                        CreatedDate = c.DateTime(storeType: "date"),
                        CreatedEmployeeId = c.Guid(),
                        TmcStateType = c.Int(),
                        TmcInId = c.Guid(),
                        Number = c.String(maxLength: 450),
                        Code = c.String(maxLength: 450),
                        Manufacturer = c.String(maxLength: 450),
                        Serial = c.String(maxLength: 450),
                        TmcCount = c.Decimal(precision: 18, scale: 6),
                        MeasureTypeDicId = c.Guid(),
                        TmcCountFact = c.Decimal(precision: 18, scale: 6),
                        CountConvert = c.Decimal(precision: 18, scale: 6),
                        TmcMeasureTypeConvertDicId = c.Guid(),
                        ManufactureDate = c.DateTime(storeType: "date"),
                        ExpiryDate = c.DateTime(storeType: "date"),
                        PackageDicId = c.Guid(),
                        TmcTypeDicId = c.Guid(),
                        StorageDicId = c.Guid(),
                        TmcSafe = c.String(maxLength: 450),
                        TmcRack = c.String(maxLength: 450),
                        OwnerEmployeeId = c.Guid(),
                        MeasureTypeDicValue = c.String(maxLength: 4000),
                        PackageDicValue = c.String(maxLength: 4000),
                        TmcTypeDicValue = c.String(maxLength: 4000),
                        OwnerEmployeeName = c.String(maxLength: 4000),
                        ApplicationStateType = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.TmcOutId, t.TmcId, t.Count, t.CountFact, t.StateType, t.StateTypeValue });
            
            CreateTable(
                "dbo.TmcOuts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, storeType: "date"),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        StateType = c.Int(nullable: false),
                        Note = c.String(maxLength: 450),
                        OutTypeDicId = c.Guid(),
                        StorageDicId = c.Guid(),
                        Safe = c.String(maxLength: 450),
                        Rack = c.String(maxLength: 450),
                        OwnerEmployeeId = c.Guid(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TmcOutView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, storeType: "date"),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        StateType = c.Int(nullable: false),
                        StateTypeValue = c.String(nullable: false, maxLength: 12),
                        CreatedEmployeeValue = c.String(maxLength: 4000),
                        Note = c.String(maxLength: 450),
                        OutTypeDicId = c.Guid(),
                        OutTypeDicValue = c.String(maxLength: 4000),
                        StorageDicId = c.Guid(),
                        StorageDicValue = c.String(maxLength: 4000),
                        Safe = c.String(maxLength: 450),
                        Rack = c.String(maxLength: 450),
                        OwnerEmployeeId = c.Guid(),
                        OwnerEmployeeValue = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.CreatedDate, t.CreatedEmployeeId, t.StateType, t.StateTypeValue });
            
            CreateTable(
                "dbo.TmcReportData",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        CreatedEmployeeName = c.String(nullable: false, maxLength: 4000),
                        TmcId = c.Guid(),
                        TmcName = c.String(maxLength: 4000),
                        TmcCode = c.String(maxLength: 512),
                        GeneralCount = c.Decimal(precision: 18, scale: 6),
                        GeneralCountFact = c.Decimal(precision: 18, scale: 6),
                        GeneralConvertFact = c.Decimal(precision: 18, scale: 6),
                        IssuedCount = c.Decimal(precision: 18, scale: 6),
                        UseCount = c.Decimal(precision: 18, scale: 6),
                        UseFactCount = c.Decimal(precision: 18, scale: 6),
                        ReceivedDate = c.DateTime(),
                        MeasureName = c.String(maxLength: 4000),
                        MeasureConvertName = c.String(maxLength: 4000),
                        UseReasonText = c.String(unicode: false, storeType: "text"),
                        PositionId = c.Guid(),
                        PositionName = c.String(maxLength: 4000),
                        SubDepartmentId = c.Guid(),
                        SubDepartmentName = c.String(maxLength: 4000),
                        DepartmentId = c.Guid(),
                        DepartmentName = c.String(maxLength: 4000),
                        CenterId = c.Guid(),
                        CenterName = c.String(maxLength: 4000),
                        ExpertiseStatementId = c.Guid(),
                        ExpertiseStatementNumber = c.String(unicode: false, storeType: "text"),
                        ExpertiseStatementTypeStr = c.String(unicode: false, storeType: "text"),
                        refTmcReport = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TmcReportDataSourceView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TmcId = c.Guid(nullable: false),
                        GeneralCountFact = c.Decimal(nullable: false, precision: 18, scale: 6),
                        GeneralConvertFact = c.Decimal(nullable: false, precision: 18, scale: 6),
                        IssuedCount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        PositionId = c.Guid(nullable: false),
                        SubDepartmentId = c.Guid(nullable: false),
                        DepartmentId = c.Guid(nullable: false),
                        CenterId = c.Guid(nullable: false),
                        TmcName = c.String(),
                        TmcCode = c.String(maxLength: 450),
                        ReceivingDate = c.DateTime(storeType: "date"),
                        UsedCount = c.Decimal(precision: 38, scale: 6),
                        UseFactCount = c.Decimal(precision: 38, scale: 6),
                        MeasureName = c.String(maxLength: 4000),
                        MeasureConvertName = c.String(maxLength: 4000),
                        UseReason = c.String(maxLength: 450),
                        CreateEmployeeName = c.String(maxLength: 4000),
                        ExpertiseStatementId = c.Guid(),
                        ExpertiseStatementNumber = c.String(maxLength: 512),
                        ExpertiseStatementTypeStr = c.String(maxLength: 512),
                        PositionName = c.String(maxLength: 4000),
                        SubDepartmentName = c.String(maxLength: 4000),
                        DepartmentName = c.String(maxLength: 4000),
                        CenterName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.TmcId, t.GeneralCountFact, t.GeneralConvertFact, t.IssuedCount, t.CreatedDate, t.CreatedEmployeeId, t.PositionId, t.SubDepartmentId, t.DepartmentId, t.CenterId });
            
            CreateTable(
                "dbo.TmcReports",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 4000),
                        PeriodStartDate = c.DateTime(nullable: false),
                        PeriodEndDate = c.DateTime(nullable: false),
                        DepartmentId = c.Guid(nullable: false),
                        DepartmentValue = c.String(maxLength: 4000),
                        CreateEmployeeId = c.Guid(nullable: false),
                        CreateEmployeeValue = c.String(maxLength: 4000),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        ReportType = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TmcReportTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Operation = c.String(nullable: false, maxLength: 512),
                        Stage = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        ExecutorEmployeeId = c.Guid(nullable: false),
                        ExecutorEmployeeValue = c.String(nullable: false, maxLength: 4000),
                        CreateEmployeeId = c.Guid(nullable: false),
                        CreateEmployeeValue = c.String(nullable: false, maxLength: 4000),
                        CreateDate = c.DateTime(nullable: false),
                        DeleteDate = c.DateTime(),
                        Note = c.String(maxLength: 4000),
                        refTmcReport = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TmcReports", t => t.refTmcReport)
                .Index(t => t.refTmcReport);
            
            CreateTable(
                "dbo.TmcReportsView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 4000),
                        PeriodStartDate = c.DateTime(nullable: false),
                        PeriodEndDate = c.DateTime(nullable: false),
                        DepartmentId = c.Guid(nullable: false),
                        CreateEmployeeId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ReportType = c.String(nullable: false, maxLength: 4000),
                        TaskId = c.Guid(nullable: false),
                        Operation = c.String(nullable: false, maxLength: 512),
                        ExecutorEmployeeId = c.Guid(nullable: false),
                        ExecutorEmployeeValue = c.String(nullable: false, maxLength: 4000),
                        Stage = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        TaskAuthorId = c.Guid(nullable: false),
                        TaskAuthorValue = c.String(nullable: false, maxLength: 4000),
                        DepartmentValue = c.String(maxLength: 4000),
                        CreateEmployeeValue = c.String(maxLength: 4000),
                        ModifiedDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        Note = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.Id, t.Name, t.PeriodStartDate, t.PeriodEndDate, t.DepartmentId, t.CreateEmployeeId, t.CreatedDate, t.ReportType, t.TaskId, t.Operation, t.ExecutorEmployeeId, t.ExecutorEmployeeValue, t.Stage, t.State, t.TaskAuthorId, t.TaskAuthorValue });
            
            CreateTable(
                "dbo.TmcUseOffView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        StateType = c.Int(nullable: false),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 6),
                        DeleteDate = c.DateTime(),
                        TmcOutId = c.Guid(),
                        TmcId = c.Guid(),
                        Note = c.String(maxLength: 450),
                        ExpertiseStatementId = c.Guid(),
                        ExpertiseStatementNumber = c.String(maxLength: 512),
                        ExpertiseStatementTypeStr = c.String(maxLength: 512),
                        TmcCode = c.String(maxLength: 450),
                        TmcName = c.String(),
                        TmcCount = c.Decimal(precision: 38, scale: 6),
                    })
                .PrimaryKey(t => new { t.Id, t.CreatedDate, t.CreatedEmployeeId, t.StateType, t.Count });
            
            CreateTable(
                "dbo.TmcView",
                c => new
                    {
                        CountActual = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, storeType: "date"),
                        CreatedEmployeeId = c.Guid(nullable: false),
                        StateType = c.Int(nullable: false),
                        StateTypeValue = c.String(nullable: false, maxLength: 9),
                        TmcInId = c.Guid(nullable: false),
                        Count = c.Decimal(nullable: false, precision: 18, scale: 6),
                        CountFact = c.Decimal(nullable: false, precision: 18, scale: 6),
                        CountConvert = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Number = c.String(maxLength: 450),
                        Name = c.String(),
                        Code = c.String(maxLength: 450),
                        Manufacturer = c.String(maxLength: 450),
                        Serial = c.String(maxLength: 450),
                        MeasureTypeDicId = c.Guid(),
                        MeasureTypeDicValue = c.String(maxLength: 4000),
                        MeasureTypeConvertDicId = c.Guid(),
                        MeasureTypeConvertDicValue = c.String(maxLength: 4000),
                        ManufactureDate = c.DateTime(storeType: "date"),
                        ExpiryDate = c.DateTime(storeType: "date"),
                        PackageDicId = c.Guid(),
                        PackageDicValue = c.String(maxLength: 4000),
                        TmcTypeDicId = c.Guid(),
                        TmcTypeDicValue = c.String(maxLength: 4000),
                        StorageDicId = c.Guid(),
                        StorageDicValue = c.String(maxLength: 4000),
                        Safe = c.String(maxLength: 450),
                        Rack = c.String(maxLength: 450),
                        OwnerEmployeeId = c.Guid(),
                        ReceivingDate = c.DateTime(storeType: "date"),
                        OwnerEmployeeValue = c.String(maxLength: 4000),
                        UsedCount = c.Decimal(precision: 38, scale: 6),
                    })
                .PrimaryKey(t => new { t.CountActual, t.Id, t.CreatedDate, t.CreatedEmployeeId, t.StateType, t.StateTypeValue, t.TmcInId, t.Count, t.CountFact, t.CountConvert });
            
            CreateTable(
                "dbo.UniqueIdentificators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 500),
                        Value = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UnitsAddress",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UnitsId = c.Guid(nullable: false),
                        RegionId = c.Guid(nullable: false),
                        AddressNameRu = c.String(maxLength: 4000),
                        AddressNameKz = c.String(maxLength: 4000),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UnitsBank",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UnitsId = c.Guid(nullable: false),
                        CurrencyId = c.Guid(),
                        BankNameRu = c.String(maxLength: 500),
                        BankNameKz = c.String(maxLength: 500),
                        KBE = c.String(maxLength: 50),
                        Code = c.String(maxLength: 50),
                        SWIFT = c.String(maxLength: 50),
                        IIK = c.String(maxLength: 50),
                        CorrespondentBank = c.String(maxLength: 500),
                        CorrespondentAccount = c.String(maxLength: 500),
                        SWIFT1 = c.String(maxLength: 50),
                        CorrespondentAccount1 = c.String(maxLength: 500),
                        SWIFT2 = c.String(maxLength: 50),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UnitSigner",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UnitsId = c.Guid(nullable: false),
                        SignerId = c.Guid(nullable: false),
                        DocNumber = c.String(maxLength: 50),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(),
                        DocumentType = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UpdateScripts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitsView",
                c => new
                    {
                        VisitId = c.Int(nullable: false),
                        VisitDate = c.DateTime(nullable: false, storeType: "date"),
                        VisitDuration = c.Int(nullable: false),
                        VisitTimeBegin = c.Int(nullable: false),
                        VisitStatusId = c.Int(nullable: false),
                        VisitStatusName = c.String(nullable: false, maxLength: 200),
                        VisitTypeId = c.Int(nullable: false),
                        VisitTypeName = c.String(nullable: false, maxLength: 4000),
                        VisitorId = c.Guid(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                        VisitComment = c.String(maxLength: 4000),
                        VisitRatingComment = c.String(maxLength: 4000),
                        VisitRatingValue = c.Int(),
                        VisitTypeGroup = c.String(maxLength: 4000),
                        VisitorLastName = c.String(maxLength: 4000),
                        VisitorFirstName = c.String(maxLength: 4000),
                        VisitorMiddleName = c.String(maxLength: 4000),
                        EmployeeLastName = c.String(maxLength: 4000),
                        EmployeeFirstName = c.String(maxLength: 4000),
                        EmployeeMiddleName = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.VisitId, t.VisitDate, t.VisitDuration, t.VisitTimeBegin, t.VisitStatusId, t.VisitStatusName, t.VisitTypeId, t.VisitTypeName, t.VisitorId, t.EmployeeId });
            
            CreateTable(
                "dbo.vw_aspnet_Applications",
                c => new
                    {
                        ApplicationName = c.String(nullable: false, maxLength: 256),
                        LoweredApplicationName = c.String(nullable: false, maxLength: 256),
                        ApplicationId = c.Guid(nullable: false),
                        Description = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => new { t.ApplicationName, t.LoweredApplicationName, t.ApplicationId });
            
            CreateTable(
                "dbo.vw_aspnet_MembershipUsers",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        PasswordFormat = c.Int(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastLoginDate = c.DateTime(nullable: false),
                        LastPasswordChangedDate = c.DateTime(nullable: false),
                        LastLockoutDate = c.DateTime(nullable: false),
                        FailedPasswordAttemptCount = c.Int(nullable: false),
                        FailedPasswordAttemptWindowStart = c.DateTime(nullable: false),
                        FailedPasswordAnswerAttemptCount = c.Int(nullable: false),
                        FailedPasswordAnswerAttemptWindowStart = c.DateTime(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                        IsAnonymous = c.Boolean(nullable: false),
                        LastActivityDate = c.DateTime(nullable: false),
                        MobilePIN = c.String(maxLength: 32),
                        Email = c.String(maxLength: 512),
                        LoweredEmail = c.String(maxLength: 512),
                        PasswordQuestion = c.String(maxLength: 512),
                        PasswordAnswer = c.String(maxLength: 256),
                        Comment = c.String(storeType: "ntext"),
                        UserName = c.String(maxLength: 512),
                        MobileAlias = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => new { t.UserId, t.PasswordFormat, t.IsApproved, t.IsLockedOut, t.CreateDate, t.LastLoginDate, t.LastPasswordChangedDate, t.LastLockoutDate, t.FailedPasswordAttemptCount, t.FailedPasswordAttemptWindowStart, t.FailedPasswordAnswerAttemptCount, t.FailedPasswordAnswerAttemptWindowStart, t.ApplicationId, t.IsAnonymous, t.LastActivityDate });
            
            CreateTable(
                "dbo.vw_aspnet_Profiles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        LastUpdatedDate = c.DateTime(nullable: false),
                        DataSize = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserId, t.LastUpdatedDate });
            
            CreateTable(
                "dbo.vw_aspnet_Roles",
                c => new
                    {
                        ApplicationId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        RoleName = c.String(maxLength: 512),
                        LoweredRoleName = c.String(maxLength: 512),
                        Description = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => new { t.ApplicationId, t.RoleId });
            
            CreateTable(
                "dbo.vw_aspnet_Users",
                c => new
                    {
                        ApplicationId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        IsAnonymous = c.Boolean(nullable: false),
                        LastActivityDate = c.DateTime(nullable: false),
                        UserName = c.String(maxLength: 512),
                        LoweredUserName = c.String(maxLength: 512),
                        MobileAlias = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => new { t.ApplicationId, t.UserId, t.IsAnonymous, t.LastActivityDate });
            
            CreateTable(
                "dbo.vw_aspnet_UsersInRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.vw_aspnet_WebPartState_Paths",
                c => new
                    {
                        ApplicationId = c.Guid(nullable: false),
                        PathId = c.Guid(nullable: false),
                        Path = c.String(maxLength: 512),
                        LoweredPath = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => new { t.ApplicationId, t.PathId });
            
            CreateTable(
                "dbo.vw_aspnet_WebPartState_Shared",
                c => new
                    {
                        PathId = c.Guid(nullable: false),
                        LastUpdatedDate = c.DateTime(nullable: false),
                        DataSize = c.Int(),
                    })
                .PrimaryKey(t => new { t.PathId, t.LastUpdatedDate });
            
            CreateTable(
                "dbo.vw_aspnet_WebPartState_User",
                c => new
                    {
                        LastUpdatedDate = c.DateTime(nullable: false),
                        PathId = c.Guid(),
                        UserId = c.Guid(),
                        DataSize = c.Int(),
                    })
                .PrimaryKey(t => t.LastUpdatedDate);
            
            CreateTable(
                "dbo.aspnet_UsersInRoles",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.aspnet_Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.aspnet_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EXP_DIC_ResultByStage",
                c => new
                    {
                        StageId = c.Int(nullable: false),
                        StageResultId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StageId, t.StageResultId })
                .ForeignKey("dbo.EXP_DIC_Stage", t => t.StageId, cascadeDelete: true)
                .ForeignKey("dbo.EXP_DIC_StageResult", t => t.StageResultId, cascadeDelete: true)
                .Index(t => t.StageId)
                .Index(t => t.StageResultId);
            
            CreateTable(
                "dbo.EXP_DirectionToPays_DrugDeclaration",
                c => new
                    {
                        DirectionToPayId = c.Guid(nullable: false),
                        DrugDeclarationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DirectionToPayId, t.DrugDeclarationId })
                .ForeignKey("dbo.EXP_DirectionToPays", t => t.DirectionToPayId, cascadeDelete: true)
                .ForeignKey("dbo.EXP_DrugDeclaration", t => t.DrugDeclarationId, cascadeDelete: true)
                .Index(t => t.DirectionToPayId)
                .Index(t => t.DrugDeclarationId);
            
            CreateTable(
                "dbo.EXP_RegistrationExpStepsExecutors",
                c => new
                    {
                        ExecutorId = c.Guid(nullable: false),
                        StepId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExecutorId, t.StepId })
                .ForeignKey("dbo.Employees", t => t.ExecutorId, cascadeDelete: true)
                .ForeignKey("dbo.EXP_RegistrationExpSteps", t => t.StepId, cascadeDelete: true)
                .Index(t => t.ExecutorId)
                .Index(t => t.StepId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TmcReportTasks", "refTmcReport", "dbo.TmcReports");
            DropForeignKey("dbo.RequestList", "RequestOrderId", "dbo.RequestOrders");
            DropForeignKey("dbo.obk_exchangerate", "currency_id", "dbo.obk_currencies");
            DropForeignKey("dbo.OBK_CertificateReference", "CertificateValidityTypeId", "dbo.OBK_CertificateValidityType");
            DropForeignKey("dbo.OBK_CertificateReferenceFieldHistory", "OBK_CertificateReferenceId", "dbo.OBK_CertificateReference");
            DropForeignKey("dbo.OBK_BlankNumber", "BlankTypeId", "dbo.OBK_BlankType");
            DropForeignKey("dbo.OBK_BlankNumber", "ParentId", "dbo.OBK_BlankNumber");
            DropForeignKey("dbo.obk_appendix_series", "appendix_id", "dbo.obk_appendix");
            DropForeignKey("dbo.obk_product_cost", "id", "dbo.obk_products");
            DropForeignKey("dbo.obk_certifications", "product_id", "dbo.obk_products");
            DropForeignKey("dbo.obk_certification_copies", "certification_id", "dbo.obk_certifications");
            DropForeignKey("dbo.obk_appendix", "certification_id", "dbo.obk_certifications");
            DropForeignKey("dbo.obk_appendix_series", "product_id", "dbo.obk_products");
            DropForeignKey("dbo.LimsTmcTemp", "TmcInId", "dbo.TmcIns");
            DropForeignKey("dbo.LimsTmcTemp", "TmcId", "dbo.Tmcs");
            DropForeignKey("dbo.EXP_DIC_PrimaryOTD", "ParentId", "dbo.EXP_DIC_PrimaryOTD");
            DropForeignKey("dbo.EMP_Statement", "NmirkId", "dbo.EXP_DIC_NMIRK");
            DropForeignKey("dbo.EMP_StatementSamples", "StatementId", "dbo.EMP_Statement");
            DropForeignKey("dbo.EMP_EAESStatement", "NmirkId", "dbo.EXP_DIC_NMIRK");
            DropForeignKey("dbo.CommissionDrugDosage", "ConclusionTypeId", "dbo.CommissionConclusionTypes");
            DropForeignKey("dbo.Visits", "VisitorId", "dbo.Employees");
            DropForeignKey("dbo.Visits", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Visits", "VisitTypeId", "dbo.VisitTypes");
            DropForeignKey("dbo.VisitEmployeeTypes", "VisitTypeId", "dbo.VisitTypes");
            DropForeignKey("dbo.Visits", "VisitStatusId", "dbo.VisitStatuses");
            DropForeignKey("dbo.VisitEmployeeWorkingTimes", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Units", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.PriceProjectsHistory", "UserId", "dbo.Employees");
            DropForeignKey("dbo.PriceProjectFieldHistory", "UserId", "dbo.Employees");
            DropForeignKey("dbo.PriceProjectComRecord", "UserId", "dbo.Employees");
            DropForeignKey("dbo.PP_ProtocolComissionMembers", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.PP_ProtocolProductPrices", "ProtocolId", "dbo.PP_Protocols");
            DropForeignKey("dbo.PP_ProtocolComissionMembers", "ProtocolId", "dbo.PP_Protocols");
            DropForeignKey("dbo.PP_PharmaList", "UserId", "dbo.Employees");
            DropForeignKey("dbo.PP_DIC_AvgExchangeRate", "ChangeEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.OBK_ZBKCopyStageSignData", "SignerId", "dbo.Employees");
            DropForeignKey("dbo.OBK_ZBKCopyStageExecutors", "ExecutorId", "dbo.Employees");
            DropForeignKey("dbo.OBK_ZBKCopySignData", "SignerId", "dbo.Employees");
            DropForeignKey("dbo.OBK_Tasks", "ExecutorId", "dbo.Employees");
            DropForeignKey("dbo.OBK_TaskMaterial", "LaboratoryAssistantId", "dbo.Employees");
            DropForeignKey("dbo.OBK_TaskExecutor", "ExecutorId", "dbo.Employees");
            DropForeignKey("dbo.OBK_RS_ProductsComRecord", "UserId", "dbo.Employees");
            DropForeignKey("dbo.OBK_ResearchCenterResult", "ExecutorId", "dbo.Employees");
            DropForeignKey("dbo.OBK_Products_SeriesComRecord", "UserId", "dbo.Employees");
            DropForeignKey("dbo.OBK_OP_Commission", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.OBK_DirectionToPayments", "CreateEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.OBK_ContractStageExecutors", "ExecutorId", "dbo.Employees");
            DropForeignKey("dbo.OBK_ContractPriceComRecord", "UserId", "dbo.Employees");
            DropForeignKey("dbo.OBK_ContractFactoryComRecord", "UserId", "dbo.Employees");
            DropForeignKey("dbo.OBK_ContractComRecord", "UserId", "dbo.Employees");
            DropForeignKey("dbo.OBK_Contract", "Signer", "dbo.Employees");
            DropForeignKey("dbo.OBK_AssessmentStageSignData", "SignerId", "dbo.Employees");
            DropForeignKey("dbo.OBK_AssessmentStageExecutors", "ExecutorId", "dbo.Employees");
            DropForeignKey("dbo.OBK_AssessmentDeclarationFieldHistory", "UserId", "dbo.Employees");
            DropForeignKey("dbo.OBK_AssessmentDeclarationComRecord", "UserId", "dbo.Employees");
            DropForeignKey("dbo.LimsEquipment", "ResponsiblePersonId", "dbo.Employees");
            DropForeignKey("dbo.LimsEquipmentPlan", "HeadOfOpoloId", "dbo.Employees");
            DropForeignKey("dbo.LimsEquipmentPlan", "DirectorRcId", "dbo.Employees");
            DropForeignKey("dbo.LimsEquipmentJournalRecord", "ExecutorId", "dbo.Employees");
            DropForeignKey("dbo.LimsEquipmentAct", "HeadOfLaboratoryId", "dbo.Employees");
            DropForeignKey("dbo.LimsEquipmentAct", "EngineerId", "dbo.Employees");
            DropForeignKey("dbo.LimsEquipmentAct", "DirectorRCId", "dbo.Employees");
            DropForeignKey("dbo.LimsApplicationJournal", "EngineerId", "dbo.Employees");
            DropForeignKey("dbo.LimsApplicationJournal", "ApplicantId", "dbo.Employees");
            DropForeignKey("dbo.LimsApplicationJournal", "AccepterId", "dbo.Employees");
            DropForeignKey("dbo.GridSettings", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.FileLinksCategoryComRecord", "UserId", "dbo.Employees");
            DropForeignKey("dbo.FileLinksCategoryComRecord", "CommentId", "dbo.FileLinksCategoryCom");
            DropForeignKey("dbo.FileLinks", "OwnerId", "dbo.Employees");
            DropForeignKey("dbo.EXP_Tasks", "ExecutorId", "dbo.Employees");
            DropForeignKey("dbo.EXP_Tasks", "AuthorId", "dbo.Employees");
            DropForeignKey("dbo.EXP_RegistrationExpStepsExecutors", "StepId", "dbo.EXP_RegistrationExpSteps");
            DropForeignKey("dbo.EXP_RegistrationExpStepsExecutors", "ExecutorId", "dbo.Employees");
            DropForeignKey("dbo.EXP_RegistrationExpSteps", "SupervisingEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EXP_MaterialDirections", "SendEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EXP_MaterialDirections", "ExecutorEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EXP_ExpertiseStageRemark", "ExecuterId", "dbo.Employees");
            DropForeignKey("dbo.EXP_ExpertiseStageExecutors", "ExecutorId", "dbo.Employees");
            DropForeignKey("dbo.EXP_DrugPrimaryRemark", "ExecuterId", "dbo.Employees");
            DropForeignKey("dbo.EXP_DrugDeclarationHistory", "UserId", "dbo.Employees");
            DropForeignKey("dbo.EXP_DrugDeclarationFieldHistory", "UserId", "dbo.Employees");
            DropForeignKey("dbo.EXP_DrugDeclarationComRecord", "UserId", "dbo.Employees");
            DropForeignKey("dbo.EXP_DrugCorespondenceRemark", "ExecuterId", "dbo.Employees");
            DropForeignKey("dbo.EXP_DrugCorespondence", "SignatoryId", "dbo.Employees");
            DropForeignKey("dbo.EXP_DrugCorespondence", "MatchingId", "dbo.Employees");
            DropForeignKey("dbo.EXP_DrugCorespondence", "AuthorId", "dbo.Employees");
            DropForeignKey("dbo.EXP_DirectionToPays", "CreateEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EXP_CertificateOfCompletion", "CreateEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EXP_AgreementProcSettingsTasks", "ExecutorId", "dbo.Employees");
            DropForeignKey("dbo.EXP_Activities", "AuthorId", "dbo.Employees");
            DropForeignKey("dbo.EMP_DirectionToPayments", "CreateEmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EMP_ContractStageExecutors", "ExecutorId", "dbo.Employees");
            DropForeignKey("dbo.Contracts", "SignerId", "dbo.Employees");
            DropForeignKey("dbo.ContractProcSettings", "ProcCenterHeadId", "dbo.Employees");
            DropForeignKey("dbo.ContractComments", "AuthorId", "dbo.Employees");
            DropForeignKey("dbo.ContractSignedDatas", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.Contracts", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.ContractComments", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.Organizations", "OriginalOrgId", "dbo.Organizations");
            DropForeignKey("dbo.EXP_Materials", "ProducerId", "dbo.Organizations");
            DropForeignKey("dbo.EXP_DirectionToPays", "PayerId", "dbo.Organizations");
            DropForeignKey("dbo.EXP_DirectionToPays_DrugDeclaration", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DirectionToPays_DrugDeclaration", "DirectionToPayId", "dbo.EXP_DirectionToPays");
            DropForeignKey("dbo.EXP_Materials", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_MaterialDirections", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_ExpertiseStage", "DeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugUseMethod", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugType", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugType", "DrugTypeKind", "dbo.EXP_DIC_DrugTypeKind");
            DropForeignKey("dbo.EXP_DrugType", "DrugTypeId", "dbo.EXP_DIC_DrugType");
            DropForeignKey("dbo.EXP_DrugProtectionDoc", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugPrimaryRemark", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugPrimaryNTD", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugPrimaryNTD", "TypeNDId", "dbo.EXP_DIC_TypeND");
            DropForeignKey("dbo.EXP_DrugPrimaryNTD", "TypeFileNDId", "dbo.EXP_DIC_TypeFileND");
            DropForeignKey("dbo.EXP_DrugPrimaryKind", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugPrimaryKind", "PrimaryKindId", "dbo.EXP_DIC_PrimaryMark");
            DropForeignKey("dbo.EXP_DrugPatent", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugOtherCountry", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugOrganizations", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugExportTrade", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugDosage", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugDeclarationHistory", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugDeclarationFieldHistory", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugDeclarationCom", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugDeclarationComRecord", "CommentId", "dbo.EXP_DrugDeclarationCom");
            DropForeignKey("dbo.EXP_DrugCorespondence", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugChangeType", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.EXP_DrugChangeType", "ChangeTypeId", "dbo.EXP_DIC_ChangeType");
            DropForeignKey("dbo.EXP_DrugDeclarationHistory", "StatusId", "dbo.EXP_DIC_Status");
            DropForeignKey("dbo.EXP_DrugDeclaration", "StatusId", "dbo.EXP_DIC_Status");
            DropForeignKey("dbo.EXP_DrugDeclaration", "ProposedShelfLifeMeasureId", "dbo.EXP_DIC_PeriodMeasure");
            DropForeignKey("dbo.EXP_DrugDeclaration", "ManufactureTypeId", "dbo.EXP_DIC_ManufactureType");
            DropForeignKey("dbo.EXP_DrugDeclaration", "AccelerationTypeId", "dbo.EXP_DIC_AccelerationType");
            DropForeignKey("dbo.EXP_CertificateOfCompletion", "DrugDeclarationId", "dbo.EXP_DrugDeclaration");
            DropForeignKey("dbo.FileLinks", "StageId", "dbo.EXP_DIC_Stage");
            DropForeignKey("dbo.EXP_RegistrationExpSteps", "RefId", "dbo.EXP_DIC_Stage");
            DropForeignKey("dbo.EXP_RegistrationTypes", "ParentId", "dbo.EXP_RegistrationTypes");
            DropForeignKey("dbo.EXP_RegistrationExpSteps", "RegistrationId", "dbo.EXP_RegistrationTypes");
            DropForeignKey("dbo.EXP_RegistrationTypes", "RefId", "dbo.EXP_DIC_Type");
            DropForeignKey("dbo.EXP_DrugDeclaration", "TypeId", "dbo.EXP_DIC_Type");
            DropForeignKey("dbo.EXP_ExpertiseStage", "StageId", "dbo.EXP_DIC_Stage");
            DropForeignKey("dbo.EXP_DrugCorespondence", "StageId", "dbo.EXP_DIC_Stage");
            DropForeignKey("dbo.EXP_DIC_ResultByStage", "StageResultId", "dbo.EXP_DIC_StageResult");
            DropForeignKey("dbo.EXP_DIC_ResultByStage", "StageId", "dbo.EXP_DIC_Stage");
            DropForeignKey("dbo.EXP_ExpertiseStageDosageResult", "ResultId", "dbo.EXP_DIC_StageResult");
            DropForeignKey("dbo.EXP_ExpertiseStageDosage", "ResultId", "dbo.EXP_DIC_StageResult");
            DropForeignKey("dbo.EXP_ExpertiseStage", "ResultId", "dbo.EXP_DIC_StageResult");
            DropForeignKey("dbo.EXP_ExpertiseStageRemark", "StageId", "dbo.EXP_ExpertiseStage");
            DropForeignKey("dbo.EXP_ExpertiseStageExecutors", "ExpertiseStageId", "dbo.EXP_ExpertiseStage");
            DropForeignKey("dbo.EXP_ExpertiseStageDosage", "StageId", "dbo.EXP_ExpertiseStage");
            DropForeignKey("dbo.EXP_ExpertiseStage", "ParentStageId", "dbo.EXP_ExpertiseStage");
            DropForeignKey("dbo.EXP_ExpertiseStage", "StatusId", "dbo.EXP_DIC_StageStatus");
            DropForeignKey("dbo.CommissionDrugDosage", "StageId", "dbo.EXP_ExpertiseStage");
            DropForeignKey("dbo.CommissionDrugDosageNeedCommission", "StageId", "dbo.EXP_ExpertiseStage");
            DropForeignKey("dbo.EXP_ExpertiseStageDosage", "DosageId", "dbo.EXP_DrugDosage");
            DropForeignKey("dbo.EXP_DrugWrapping", "DrugDosageId", "dbo.EXP_DrugDosage");
            DropForeignKey("dbo.EXP_DrugSubstance", "DrugDosageId", "dbo.EXP_DrugDosage");
            DropForeignKey("dbo.EXP_DrugPrimaryFinalDocument", "DrugDosageId", "dbo.EXP_DrugDosage");
            DropForeignKey("dbo.EXP_DrugPrice", "DrugDosageId", "dbo.EXP_DrugDosage");
            DropForeignKey("dbo.EXP_DrugAppDosageResult", "DrugDosageId", "dbo.EXP_DrugDosage");
            DropForeignKey("dbo.EXP_DrugAppDosageRemark", "DrugDosageId", "dbo.EXP_DrugDosage");
            DropForeignKey("dbo.EXP_ExpertiseStageRemark", "RemarkTypeId", "dbo.EXP_DIC_RemarkType");
            DropForeignKey("dbo.EXP_DrugPrimaryRemark", "RemarkTypeId", "dbo.EXP_DIC_RemarkType");
            DropForeignKey("dbo.EXP_DrugCorespondenceRemark", "RemarkTypeId", "dbo.EXP_DIC_RemarkType");
            DropForeignKey("dbo.EXP_DrugCorespondenceRemark", "DrugCorespondenceId", "dbo.EXP_DrugCorespondence");
            DropForeignKey("dbo.EXP_DrugCorespondence", "TypeId", "dbo.EXP_DIC_CorespondenceType");
            DropForeignKey("dbo.EXP_DrugCorespondence", "SubjectId", "dbo.EXP_DIC_CorespondenceSubject");
            DropForeignKey("dbo.EXP_DrugCorespondence", "KindId", "dbo.EXP_DIC_CorespondenceKind");
            DropForeignKey("dbo.PP_PharmaList", "RegionId", "dbo.Dictionaries");
            DropForeignKey("dbo.PP_DIC_AvgExchangeRate", "CurrencyId", "dbo.Dictionaries");
            DropForeignKey("dbo.PP_AttachRemarks", "AttachPriceDicId", "dbo.Dictionaries");
            DropForeignKey("dbo.PriceProjectsHistory", "PriceProjectId", "dbo.PriceProjects");
            DropForeignKey("dbo.PriceProjectFieldHistory", "PriceProjectId", "dbo.PriceProjects");
            DropForeignKey("dbo.PriceProjectCom", "PriceProjectId", "dbo.PriceProjects");
            DropForeignKey("dbo.PriceProjectComRecord", "CommentId", "dbo.PriceProjectCom");
            DropForeignKey("dbo.PP_AttachRemarks", "PriceProjectId", "dbo.PriceProjects");
            DropForeignKey("dbo.OBK_Ref_PriceList", "UnitId", "dbo.Dictionaries");
            DropForeignKey("dbo.OBK_Contract", "ContractAdditionType", "dbo.Dictionaries");
            DropForeignKey("dbo.OBK_ActReception", "ProductSamplesId", "dbo.Dictionaries");
            DropForeignKey("dbo.LimsEquipment", "StatusId", "dbo.Dictionaries");
            DropForeignKey("dbo.LimsEquipment", "ProducerId", "dbo.Dictionaries");
            DropForeignKey("dbo.LimsEquipment", "ModelId", "dbo.Dictionaries");
            DropForeignKey("dbo.LimsEquipment", "LocationId", "dbo.Dictionaries");
            DropForeignKey("dbo.LimsEquipment", "LaboratoryId", "dbo.Dictionaries");
            DropForeignKey("dbo.LimsEquipment", "EquipmentTypeId", "dbo.Dictionaries");
            DropForeignKey("dbo.LimsEquipment", "CountryProductionId", "dbo.Dictionaries");
            DropForeignKey("dbo.LimsEquipmentPlan", "PlanTypeId", "dbo.Dictionaries");
            DropForeignKey("dbo.LimsEquipmentJournal", "JournalTypeId", "dbo.Dictionaries");
            DropForeignKey("dbo.LimsEquipmentActSpareParts", "LocationId", "dbo.Dictionaries");
            DropForeignKey("dbo.LimsEquipmentAct", "ActTypeId", "dbo.Dictionaries");
            DropForeignKey("dbo.LimsEquipmentActSpareParts", "EquipmentActId", "dbo.LimsEquipmentAct");
            DropForeignKey("dbo.LimsPlanEquipmentLink", "EquipmentId", "dbo.LimsEquipment");
            DropForeignKey("dbo.LimsPlanEquipmentLink", "EquipmentPlanId", "dbo.LimsEquipmentPlan");
            DropForeignKey("dbo.LimsEquipmentJournalRecord", "EquipmentId", "dbo.LimsEquipment");
            DropForeignKey("dbo.LimsEquipmentJournalRecord", "JournalId", "dbo.LimsEquipmentJournal");
            DropForeignKey("dbo.LimsEquipmentAct", "EquipmentId", "dbo.LimsEquipment");
            DropForeignKey("dbo.LimsApplicationJournal", "EquipmentId", "dbo.LimsEquipment");
            DropForeignKey("dbo.FileLinks", "CategoryId", "dbo.Dictionaries");
            DropForeignKey("dbo.FileLinks", "ParentId", "dbo.FileLinks");
            DropForeignKey("dbo.FileLinks", "StatusId", "dbo.DIC_FileLinkStatus");
            DropForeignKey("dbo.EXP_Tasks", "StatusId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Tasks", "TypeId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Materials", "VolumeUnitId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Materials", "UnitId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Materials", "TypeId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Materials", "StorageConditionId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Materials", "StatusId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Materials", "ExternalStateId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Materials", "DosageUnitId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Materials", "CountryId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Materials", "ConcordanceStatementId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Materials", "ConcentrationUnitId", "dbo.Dictionaries");
            DropForeignKey("dbo.Units", "ParentId", "dbo.Units");
            DropForeignKey("dbo.OBK_TaskStatus", "UnitLaboratoryId", "dbo.Units");
            DropForeignKey("dbo.OBK_Tasks", "UnitId", "dbo.Units");
            DropForeignKey("dbo.OBK_TaskMaterial", "UnitLaboratoryId", "dbo.Units");
            DropForeignKey("dbo.OBK_Contract", "ExpertOrganization", "dbo.Units");
            DropForeignKey("dbo.OBK_RS_Products", "ContractId", "dbo.OBK_Contract");
            DropForeignKey("dbo.OBK_LetterPortalEdo", "ContractId", "dbo.OBK_Contract");
            DropForeignKey("dbo.OBK_LetterPortalEdo", "OBKLetterRegID", "dbo.OBK_LetterRegistration");
            DropForeignKey("dbo.OBK_LetterFromEdo", "LetterPortalEdoID", "dbo.OBK_LetterPortalEdo");
            DropForeignKey("dbo.OBK_DirectionToPayments", "ContractId", "dbo.OBK_Contract");
            DropForeignKey("dbo.OBK_DirectionToPayments", "PayerId", "dbo.OBK_Declarant");
            DropForeignKey("dbo.OBK_DirectionSignData", "DirectionToPaymentId", "dbo.OBK_DirectionToPayments");
            DropForeignKey("dbo.OBK_DeclarantContact", "DeclarantId", "dbo.OBK_Declarant");
            DropForeignKey("dbo.OBK_Contract", "DeclarantId", "dbo.OBK_Declarant");
            DropForeignKey("dbo.EMP_DirectionToPayments", "PayerId", "dbo.OBK_Declarant");
            DropForeignKey("dbo.EMP_Contract", "PayerId", "dbo.OBK_Declarant");
            DropForeignKey("dbo.EMP_Contract", "ManufacturId", "dbo.OBK_Declarant");
            DropForeignKey("dbo.EMP_Contract", "DeclarantId", "dbo.OBK_Declarant");
            DropForeignKey("dbo.OBK_Contract", "DeclarantContactId", "dbo.OBK_DeclarantContact");
            DropForeignKey("dbo.OBK_DeclarantContact", "BankId", "dbo.EMP_Ref_Bank");
            DropForeignKey("dbo.EMP_Contract", "PayerContactId", "dbo.OBK_DeclarantContact");
            DropForeignKey("dbo.EMP_Contract", "ManufacturContactId", "dbo.OBK_DeclarantContact");
            DropForeignKey("dbo.EMP_Contract", "DeclarantContactId", "dbo.OBK_DeclarantContact");
            DropForeignKey("dbo.EMP_Contract", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.EMP_Contract", "ContractStatusId", "dbo.EMP_Ref_Status");
            DropForeignKey("dbo.EMP_Contract", "ContractType", "dbo.EMP_Ref_ContractType");
            DropForeignKey("dbo.EMP_Contract", "ContractScopeId", "dbo.EMP_Ref_ContractScope");
            DropForeignKey("dbo.EMP_DirectionToPayments", "ContractId", "dbo.EMP_Contract");
            DropForeignKey("dbo.EMP_DirectionToPayments", "StatusId", "dbo.OBK_Ref_PaymentStatus");
            DropForeignKey("dbo.EMP_CostWorks", "ContractId", "dbo.EMP_Contract");
            DropForeignKey("dbo.EMP_Ref_ServiceType", "ParentId", "dbo.EMP_Ref_ServiceType");
            DropForeignKey("dbo.EMP_Ref_PriceList", "ServiceTypeId", "dbo.EMP_Ref_ServiceType");
            DropForeignKey("dbo.EMP_Ref_ServiceType", "DegreeRiskId", "dbo.EMP_Ref_DegreeRisk");
            DropForeignKey("dbo.EMP_Ref_PriceList", "PriceTypeId", "dbo.EMP_Ref_PriceType");
            DropForeignKey("dbo.EMP_CostWorks", "PriceListId", "dbo.EMP_Ref_PriceList");
            DropForeignKey("dbo.EMP_ContractStage", "ContractId", "dbo.EMP_Contract");
            DropForeignKey("dbo.EMP_ContractStage", "StageStatusId", "dbo.EMP_Ref_StageStatus");
            DropForeignKey("dbo.EMP_ContractStage", "StageId", "dbo.EMP_Ref_Stage");
            DropForeignKey("dbo.EMP_ContractStageExecutors", "ContractStageId", "dbo.EMP_ContractStage");
            DropForeignKey("dbo.EMP_ContractStage", "ParentStageId", "dbo.EMP_ContractStage");
            DropForeignKey("dbo.EMP_ContractSignData", "ContractId", "dbo.EMP_Contract");
            DropForeignKey("dbo.EMP_ContractHistory", "ContractId", "dbo.EMP_Contract");
            DropForeignKey("dbo.OBK_ContractHistory", "StatusId", "dbo.OBK_Ref_ContractHistoryStatus");
            DropForeignKey("dbo.EMP_ContractHistory", "StatusId", "dbo.OBK_Ref_ContractHistoryStatus");
            DropForeignKey("dbo.EMP_ContractExtHistory", "ContractId", "dbo.EMP_Contract");
            DropForeignKey("dbo.EMP_ContractExtHistory", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.OBK_ContractStage", "ContractId", "dbo.OBK_Contract");
            DropForeignKey("dbo.OBK_ContractSignedDatas", "ContractId", "dbo.OBK_Contract");
            DropForeignKey("dbo.OBK_ContractPrice", "ContractId", "dbo.OBK_Contract");
            DropForeignKey("dbo.OBK_ContractFactory", "ContractId", "dbo.OBK_Contract");
            DropForeignKey("dbo.OBK_ContractFactoryCom", "ContractFactoryId", "dbo.OBK_ContractFactory");
            DropForeignKey("dbo.OBK_ContractFactoryComRecord", "CommentId", "dbo.OBK_ContractFactoryCom");
            DropForeignKey("dbo.OBK_ContractExtHistory", "ContractId", "dbo.OBK_Contract");
            DropForeignKey("dbo.OBK_ContractExtHistory", "StatusId", "dbo.OBK_Ref_ContractExtHistoryStatus");
            DropForeignKey("dbo.OBK_ContractExtHistory", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.OBK_ContractCom", "ContractId", "dbo.OBK_Contract");
            DropForeignKey("dbo.OBK_ContractComRecord", "CommentId", "dbo.OBK_ContractCom");
            DropForeignKey("dbo.OBK_Contract", "OBK_Contract2_Id", "dbo.OBK_Contract");
            DropForeignKey("dbo.OBK_CertificateOfCompletion", "ContractId", "dbo.OBK_Contract");
            DropForeignKey("dbo.OBK_AssessmentDeclaration", "ContractId", "dbo.OBK_Contract");
            DropForeignKey("dbo.OBK_Tasks", "AssessmentDeclarationId", "dbo.OBK_AssessmentDeclaration");
            DropForeignKey("dbo.OBK_StageExpDocumentResult", "AssessmetDeclarationId", "dbo.OBK_AssessmentDeclaration");
            DropForeignKey("dbo.OBK_StageExpDocument", "AssessmentDeclarationId", "dbo.OBK_AssessmentDeclaration");
            DropForeignKey("dbo.OBK_AssessmentDeclaration", "CertificateTypeId", "dbo.OBK_Ref_CertificateType");
            DropForeignKey("dbo.OBK_OP_Commission", "DeclarationId", "dbo.OBK_AssessmentDeclaration");
            DropForeignKey("dbo.OBK_OP_Commission", "RoleId", "dbo.OBK_OP_CommissionRoles");
            DropForeignKey("dbo.OBK_CertificateOfCompletion", "AssessmentDeclarationId", "dbo.OBK_AssessmentDeclaration");
            DropForeignKey("dbo.OBK_AssessmentStageOP", "DeclarationId", "dbo.OBK_AssessmentDeclaration");
            DropForeignKey("dbo.OBK_AssessmentStage", "DeclarationId", "dbo.OBK_AssessmentDeclaration");
            DropForeignKey("dbo.OBK_AssessmentDeclarationHistory", "AssessmentDeclarationId", "dbo.OBK_AssessmentDeclaration");
            DropForeignKey("dbo.OBK_Contract", "Status", "dbo.OBK_Ref_Status");
            DropForeignKey("dbo.OBK_AssessmentDeclarationHistory", "StatusId", "dbo.OBK_Ref_Status");
            DropForeignKey("dbo.OBK_AssessmentDeclarationFieldHistory", "AssessmentDeclarationId", "dbo.OBK_AssessmentDeclaration");
            DropForeignKey("dbo.OBK_AssessmentDeclarationCom", "AssessmentDeclarationId", "dbo.OBK_AssessmentDeclaration");
            DropForeignKey("dbo.OBK_AssessmentDeclarationComRecord", "CommentId", "dbo.OBK_AssessmentDeclarationCom");
            DropForeignKey("dbo.OBK_AssessmentDeclaration", "OBK_AssessmentDeclaration2_Id", "dbo.OBK_AssessmentDeclaration");
            DropForeignKey("dbo.OBK_AssessmentDeclaration__OBK_ExpertCouncil", "DeclarationId", "dbo.OBK_AssessmentDeclaration");
            DropForeignKey("dbo.OBK_AssessmentDeclaration__OBK_ExpertCouncil", "ExpertCouncilId", "dbo.OBK_ExpertCouncil");
            DropForeignKey("dbo.OBK_Tasks", "ActReceptionId", "dbo.OBK_ActReception");
            DropForeignKey("dbo.OBK_TaskStatus", "TaskId", "dbo.OBK_Tasks");
            DropForeignKey("dbo.OBK_TaskMaterial", "TaskId", "dbo.OBK_Tasks");
            DropForeignKey("dbo.OBK_TaskExecutor", "TaskId", "dbo.OBK_Tasks");
            DropForeignKey("dbo.OBK_ZBKCopyStage", "StageStatusId", "dbo.OBK_Ref_StageStatus");
            DropForeignKey("dbo.OBK_TaskStatus", "StatusId", "dbo.OBK_Ref_StageStatus");
            DropForeignKey("dbo.OBK_Tasks", "TaskStatusId", "dbo.OBK_Ref_StageStatus");
            DropForeignKey("dbo.OBK_TaskMaterial", "StatusId", "dbo.OBK_Ref_StageStatus");
            DropForeignKey("dbo.OBK_TaskExecutor", "TaskMaterialId", "dbo.OBK_TaskMaterial");
            DropForeignKey("dbo.OBK_ResearchCenterResult", "TaskMaterialId", "dbo.OBK_TaskMaterial");
            DropForeignKey("dbo.OBK_ResearchCenterResult", "LaboratoryMarkId", "dbo.OBK_Ref_LaboratoryMark");
            DropForeignKey("dbo.OBK_Ref_LaboratoryRegulation", "LaboratoryMarkId", "dbo.OBK_Ref_LaboratoryMark");
            DropForeignKey("dbo.OBK_ResearchCenterResult", "LaboratoryRegulationId", "dbo.OBK_Ref_LaboratoryRegulation");
            DropForeignKey("dbo.OBK_TaskMaterial", "ExternalConditionId", "dbo.OBK_Ref_MaterialCondition");
            DropForeignKey("dbo.OBK_TaskMaterial", "StorageConditionId", "dbo.OBK_Ref_MaterialCondition");
            DropForeignKey("dbo.OBK_TaskMaterial", "LaboratoryTypeId", "dbo.OBK_Ref_LaboratoryType");
            DropForeignKey("dbo.sr_register_drugs", "dosage_measure_id", "dbo.sr_measures");
            DropForeignKey("dbo.sr_register", "storage_measure_id", "dbo.sr_measures");
            DropForeignKey("dbo.OBK_Procunts_Series", "SeriesMeasureId", "dbo.sr_measures");
            DropForeignKey("dbo.OBK_ActReception", "sr_measuresId", "dbo.sr_measures");
            DropForeignKey("dbo.EXP_DrugWrapping", "VolumeMeasureId", "dbo.sr_measures");
            DropForeignKey("dbo.EXP_DrugWrapping", "SizeMeasureId", "dbo.sr_measures");
            DropForeignKey("dbo.EXP_DrugSubstance", "MeasureId", "dbo.sr_measures");
            DropForeignKey("dbo.EXP_DrugSubstanceManufacture", "DrugSubstanceId", "dbo.EXP_DrugSubstance");
            DropForeignKey("dbo.sr_register_producers", "country_id", "dbo.sr_countries");
            DropForeignKey("dbo.sr_register_names", "country_id", "dbo.sr_countries");
            DropForeignKey("dbo.sr_register_substances", "register_id", "dbo.sr_register");
            DropForeignKey("dbo.sr_register_substances", "substance_id", "dbo.sr_substances");
            DropForeignKey("dbo.sr_substances", "category_id", "dbo.sr_categories");
            DropForeignKey("dbo.sr_register_drugs", "category_id", "dbo.sr_categories");
            DropForeignKey("dbo.sr_register_use_methods", "register_id", "dbo.sr_register_drugs");
            DropForeignKey("dbo.sr_register_use_methods", "use_method_id", "dbo.sr_use_methods");
            DropForeignKey("dbo.EXP_DrugUseMethod", "UseMethodsId", "dbo.sr_use_methods");
            DropForeignKey("dbo.sr_register_pharmacological_actions", "register_id", "dbo.sr_register_drugs");
            DropForeignKey("dbo.sr_register_pharmacological_actions", "pharmacological_action_id", "dbo.sr_pharmacological_actions");
            DropForeignKey("dbo.sr_pharmacological_actions", "parent_id", "dbo.sr_pharmacological_actions");
            DropForeignKey("dbo.sr_register_drugs", "nd_name_id", "dbo.sr_nd_names");
            DropForeignKey("dbo.sr_register_drugs", "life_type_id", "dbo.sr_life_types");
            DropForeignKey("dbo.sr_register_drugs", "int_name_id", "dbo.sr_international_names");
            DropForeignKey("dbo.EXP_DrugDeclaration", "MnnId", "dbo.sr_international_names");
            DropForeignKey("dbo.sr_register_drugs", "drug_type_id", "dbo.sr_drug_types");
            DropForeignKey("dbo.sr_register_drugs", "dosage_form_id", "dbo.sr_dosage_forms");
            DropForeignKey("dbo.sr_dosage_forms", "parent_id", "dbo.sr_dosage_forms");
            DropForeignKey("dbo.EXP_Materials", "DrugFormId", "dbo.sr_dosage_forms");
            DropForeignKey("dbo.EXP_DrugDeclaration", "DrugFormId", "dbo.sr_dosage_forms");
            DropForeignKey("dbo.sr_register_drugs", "atc_id", "dbo.sr_atc_codes");
            DropForeignKey("dbo.sr_atc_codes", "parent_id", "dbo.sr_atc_codes");
            DropForeignKey("dbo.EXP_DrugDeclaration", "AtxId", "dbo.sr_atc_codes");
            DropForeignKey("dbo.EXP_DrugSubstance", "SubstanceId", "dbo.sr_substances");
            DropForeignKey("dbo.sr_register_substances", "substance_type_id", "dbo.sr_substance_types");
            DropForeignKey("dbo.EXP_DrugSubstance", "SubstanceTypeId", "dbo.sr_substance_types");
            DropForeignKey("dbo.sr_register_substances", "nd_type_id", "dbo.sr_nd_types");
            DropForeignKey("dbo.sr_register_producers", "register_id", "dbo.sr_register");
            DropForeignKey("dbo.sr_register_names", "register_id", "dbo.sr_register");
            DropForeignKey("dbo.sr_register_mt", "id", "dbo.sr_register");
            DropForeignKey("dbo.sr_register_mt_parts", "register_id", "dbo.sr_register_mt");
            DropForeignKey("dbo.sr_register_producers", "producer_id", "dbo.sr_producers");
            DropForeignKey("dbo.sr_register_producers", "producer_type_id", "dbo.sr_producer_types");
            DropForeignKey("dbo.sr_register_mt_parts", "producer_id", "dbo.sr_producers");
            DropForeignKey("dbo.sr_producers", "form_type_id", "dbo.sr_form_types");
            DropForeignKey("dbo.sr_register_mt_kz", "id", "dbo.sr_register_mt");
            DropForeignKey("dbo.sr_register_mt", "mt_category_id", "dbo.sr_mt_categories");
            DropForeignKey("dbo.sr_mt_categories", "parent_id", "dbo.sr_mt_categories");
            DropForeignKey("dbo.sr_register_mt", "risk_detail_id", "dbo.sr_degree_risk_details");
            DropForeignKey("dbo.sr_register_mt", "degree_risk_id", "dbo.sr_degree_risks");
            DropForeignKey("dbo.sr_degree_risk_details", "degree_risk_id", "dbo.sr_degree_risks");
            DropForeignKey("dbo.sr_register_instructions", "register_id", "dbo.sr_register");
            DropForeignKey("dbo.sr_register_instructions_files", "id", "dbo.sr_register_instructions");
            DropForeignKey("dbo.sr_register_instructions", "instruction_type_id", "dbo.sr_instruction_types");
            DropForeignKey("dbo.sr_register_boxes", "register_id", "dbo.sr_register");
            DropForeignKey("dbo.sr_register", "reg_type_id", "dbo.sr_reg_types");
            DropForeignKey("dbo.sr_register", "reg_action_id", "dbo.sr_reg_actions");
            DropForeignKey("dbo.sr_drug_forms", "register_id", "dbo.sr_register");
            DropForeignKey("dbo.sr_register_boxes_rk_ls", "id", "dbo.sr_register_boxes");
            DropForeignKey("dbo.sr_drug_forms", "sec_box_id", "dbo.sr_register_boxes");
            DropForeignKey("dbo.sr_drug_forms", "pr_box_id", "dbo.sr_register_boxes");
            DropForeignKey("dbo.sr_register_boxes", "box_id", "dbo.sr_boxes");
            DropForeignKey("dbo.sr_boxes", "parent_id", "dbo.sr_boxes");
            DropForeignKey("dbo.EXP_DrugWrapping", "WrappingKindId", "dbo.sr_boxes");
            DropForeignKey("dbo.EXP_DrugWrapping", "WrappingTypeId", "dbo.EXP_DIC_WrappingType");
            DropForeignKey("dbo.EXP_DrugDosage", "RegisterId", "dbo.sr_register");
            DropForeignKey("dbo.EXP_DrugDeclaration", "RegisterId", "dbo.sr_register");
            DropForeignKey("dbo.EXP_DrugSubstanceManufacture", "CountryId", "dbo.sr_countries");
            DropForeignKey("dbo.EXP_DrugSubstance", "CountryId", "dbo.sr_countries");
            DropForeignKey("dbo.EXP_DrugOtherCountry", "CountryId", "dbo.sr_countries");
            DropForeignKey("dbo.EXP_DrugExportTrade", "CountryId", "dbo.sr_countries");
            DropForeignKey("dbo.EXP_DrugSubstance", "PlantKindId", "dbo.EXP_DIC_PlantKind");
            DropForeignKey("dbo.EXP_DrugSubstance", "OriginId", "dbo.EXP_DIC_Origin");
            DropForeignKey("dbo.EXP_DrugSubstance", "NormDocFarmId", "dbo.EXP_DIC_NormDocFarm");
            DropForeignKey("dbo.EXP_DrugDosage", "DosageMeasureTypeId", "dbo.sr_measures");
            DropForeignKey("dbo.EXP_DrugDosage", "BestBeforeMeasureTypeDicId", "dbo.sr_measures");
            DropForeignKey("dbo.EXP_DrugDosage", "AppPeriodOpenMeasureDicId", "dbo.sr_measures");
            DropForeignKey("dbo.EXP_DrugDosage", "AppPeriodMixMeasureDicId", "dbo.sr_measures");
            DropForeignKey("dbo.EXP_DrugDeclaration", "AppPeriodOpenMeasureDicId", "dbo.sr_measures");
            DropForeignKey("dbo.EXP_DrugDeclaration", "AppPeriodMixMeasureDicId", "dbo.sr_measures");
            DropForeignKey("dbo.EXP_DrugDeclaration", "BestBeforeMeasureTypeDicId", "dbo.sr_measures");
            DropForeignKey("dbo.EXP_DrugDeclaration", "DosageMeasureTypeId", "dbo.sr_measures");
            DropForeignKey("dbo.OBK_TaskMaterial", "ProductSeriesId", "dbo.OBK_Procunts_Series");
            DropForeignKey("dbo.OBK_StageExpDocument", "ProductSeriesId", "dbo.OBK_Procunts_Series");
            DropForeignKey("dbo.OBK_StageExpDocument", "ProductId", "dbo.OBK_RS_Products");
            DropForeignKey("dbo.OBK_ZBKCopySignData", "OBK_ZBKCopyId", "dbo.OBK_ZBKCopy");
            DropForeignKey("dbo.OBK_ZBKCopyBlank", "ZBKCopyId", "dbo.OBK_ZBKCopy");
            DropForeignKey("dbo.OBK_ZBKCopy", "OBK_StageExpDocumentId", "dbo.OBK_StageExpDocument");
            DropForeignKey("dbo.OBK_ZBKCopy", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.OBK_StageExpDocument", "RefReasonId", "dbo.OBK_Ref_Reason");
            DropForeignKey("dbo.OBK_RS_ProductsCom", "ProductId", "dbo.OBK_RS_Products");
            DropForeignKey("dbo.OBK_RS_ProductsComRecord", "CommentId", "dbo.OBK_RS_ProductsCom");
            DropForeignKey("dbo.OBK_Procunts_Series", "OBK_RS_ProductsId", "dbo.OBK_RS_Products");
            DropForeignKey("dbo.OBK_MtPart", "ProductId", "dbo.OBK_RS_Products");
            DropForeignKey("dbo.OBK_ContractPrice", "ProductId", "dbo.OBK_RS_Products");
            DropForeignKey("dbo.OBK_Ref_PriceList", "TypeId", "dbo.OBK_Ref_Type");
            DropForeignKey("dbo.OBK_Contract", "Type", "dbo.OBK_Ref_Type");
            DropForeignKey("dbo.OBK_AssessmentDeclaration", "TypeId", "dbo.OBK_Ref_Type");
            DropForeignKey("dbo.OBK_Ref_PriceList", "ServiceTypeId", "dbo.OBK_Ref_ServiceType");
            DropForeignKey("dbo.OBK_Ref_PriceList", "DegreeRiskId", "dbo.OBK_Ref_DegreeRisk");
            DropForeignKey("dbo.OBK_ContractPrice", "PriceRefId", "dbo.OBK_Ref_PriceList");
            DropForeignKey("dbo.OBK_ContractPriceCom", "ContractPriceId", "dbo.OBK_ContractPrice");
            DropForeignKey("dbo.OBK_ContractPriceComRecord", "CommentId", "dbo.OBK_ContractPriceCom");
            DropForeignKey("dbo.OBK_Products_SeriesCom", "ProductSerieId", "dbo.OBK_Procunts_Series");
            DropForeignKey("dbo.OBK_Products_SeriesComRecord", "CommentId", "dbo.OBK_Products_SeriesCom");
            DropForeignKey("dbo.OBK_ContractStage", "StageStatusId", "dbo.OBK_Ref_StageStatus");
            DropForeignKey("dbo.OBK_AssessmentStageOP", "StageStatus", "dbo.OBK_Ref_StageStatus");
            DropForeignKey("dbo.OBK_AssessmentStage", "StageStatusId", "dbo.OBK_Ref_StageStatus");
            DropForeignKey("dbo.OBK_ZBKCopyStage", "StageId", "dbo.OBK_Ref_Stage");
            DropForeignKey("dbo.OBK_ZBKCopyStageSignData", "StageId", "dbo.OBK_ZBKCopyStage");
            DropForeignKey("dbo.OBK_ZBKCopyStageExecutors", "ZBKCopyStageId", "dbo.OBK_ZBKCopyStage");
            DropForeignKey("dbo.OBK_ContractStage", "StageId", "dbo.OBK_Ref_Stage");
            DropForeignKey("dbo.OBK_ContractStageExecutors", "ContractStageId", "dbo.OBK_ContractStage");
            DropForeignKey("dbo.OBK_ContractStage", "ParentStageId", "dbo.OBK_ContractStage");
            DropForeignKey("dbo.OBK_AssessmentStage", "StageId", "dbo.OBK_Ref_Stage");
            DropForeignKey("dbo.OBK_AssessmentStageSignData", "AssessmentStageId", "dbo.OBK_AssessmentStage");
            DropForeignKey("dbo.OBK_AssessmentStageExecutors", "AssessmentStageId", "dbo.OBK_AssessmentStage");
            DropForeignKey("dbo.OBK_ActReception", "StorageConditionsId", "dbo.OBK_Dictionaries");
            DropForeignKey("dbo.OBK_ActReception", "PackageConditionId", "dbo.OBK_Dictionaries");
            DropForeignKey("dbo.OBK_ActReception", "MarkingId", "dbo.OBK_Dictionaries");
            DropForeignKey("dbo.OBK_ActReception", "InspectionInstalledId", "dbo.OBK_Dictionaries");
            DropForeignKey("dbo.OBK_ActReception", "OBK_AssessmentDeclarationId", "dbo.OBK_AssessmentDeclaration");
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Units");
            DropForeignKey("dbo.Employees", "OrganizationId", "dbo.Units");
            DropForeignKey("dbo.DIC_Storages", "OrganizationId", "dbo.Units");
            DropForeignKey("dbo.EXP_Materials", "StorageId", "dbo.DIC_Storages");
            DropForeignKey("dbo.DIC_Storages", "ParentId", "dbo.DIC_Storages");
            DropForeignKey("dbo.EXP_MaterialDirections", "StatusId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_ExpertiseStageDosage", "FinalDocStatusId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_ExpertisePrimaryFinalDoc", "DosageStageId", "dbo.EXP_ExpertiseStageDosage");
            DropForeignKey("dbo.EXP_ExpertiseStageDosageResult", "StageDosageId", "dbo.EXP_ExpertiseStageDosage");
            DropForeignKey("dbo.CommissionDrugDosage", "ExpertResultId", "dbo.EXP_ExpertiseStageDosageResult");
            DropForeignKey("dbo.EXP_ExpertiseStageDosageResult", "CommissionId", "dbo.Commissions");
            DropForeignKey("dbo.EXP_ExpertiseSafetyreportFinalDoc", "DosageStageId", "dbo.EXP_ExpertiseStageDosage");
            DropForeignKey("dbo.EXP_ExpertisePharmacologicalFinalDoc", "DosageStageId", "dbo.EXP_ExpertiseStageDosage");
            DropForeignKey("dbo.EXP_ExpertisePharmaceuticalFinalDoc", "DosageStageId", "dbo.EXP_ExpertiseStageDosage");
            DropForeignKey("dbo.EXP_DrugAnaliseIndicator", "DosageStageId", "dbo.EXP_ExpertiseStageDosage");
            DropForeignKey("dbo.EXP_DrugAnaliseIndicator", "AnalyseIndicator", "dbo.EXP_DIC_AnalyseIndicator");
            DropForeignKey("dbo.EXP_DrugPrimaryFinalDocument", "StatusId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_DrugPrimaryFinalDocument", "ResultId", "dbo.EXP_DIC_PrimaryFinalyDocResult");
            DropForeignKey("dbo.EXP_DrugCorespondence", "StatusId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_DirectionToPays", "StatusId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_CertificateOfCompletion", "StatusId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_AgreementProcSettingsTasks", "TaskTypeId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_AgreementProcSettingsActivities", "ActivityTypeId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_AgreementProcSettings", "AgreedDocTypeId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_AgreementProcSettingsActivities", "SettingId", "dbo.EXP_AgreementProcSettings");
            DropForeignKey("dbo.EXP_AgreementProcSettingsTasks", "ActivityId", "dbo.EXP_AgreementProcSettingsActivities");
            DropForeignKey("dbo.EXP_AgreementProcSettingsTasks", "ParentTaskId", "dbo.EXP_AgreementProcSettingsTasks");
            DropForeignKey("dbo.EXP_Activities", "DocumentTypeId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Activities", "TypeId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Activities", "StatusId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_Tasks", "ActivityId", "dbo.EXP_Activities");
            DropForeignKey("dbo.EXP_Tasks", "ParentTaskId", "dbo.EXP_Tasks");
            DropForeignKey("dbo.Dictionaries", "ParentId", "dbo.Dictionaries");
            DropForeignKey("dbo.Contracts", "HolderTypeId", "dbo.Dictionaries");
            DropForeignKey("dbo.Contracts", "StatusId", "dbo.Dictionaries");
            DropForeignKey("dbo.Contracts", "AgentDocTypeDicId", "dbo.Dictionaries");
            DropForeignKey("dbo.Contracts", "DoverennostTypeDicId", "dbo.Dictionaries");
            DropForeignKey("dbo.Contracts", "ContractAdditionTypeId", "dbo.Dictionaries");
            DropForeignKey("dbo.EXP_DrugAppDosageResult", "RemarkTypeId", "dbo.EXP_DIC_RemarkType");
            DropForeignKey("dbo.EXP_DrugAppDosageRemark", "RemarkTypeId", "dbo.EXP_DIC_RemarkType");
            DropForeignKey("dbo.EXP_DrugDosage", "SaleTypeId", "dbo.EXP_DIC_SaleType");
            DropForeignKey("dbo.EXP_DrugDeclaration", "SaleTypeId", "dbo.EXP_DIC_SaleType");
            DropForeignKey("dbo.CommissionDrugDosage", "DrugDosageId", "dbo.EXP_DrugDosage");
            DropForeignKey("dbo.CommissionDrugDosageNeedCommission", "DrugDosageId", "dbo.EXP_DrugDosage");
            DropForeignKey("dbo.EXP_CertificateOfCompletion", "DicStageId", "dbo.EXP_DIC_Stage");
            DropForeignKey("dbo.EXP_DirectionToPays_PriceList", "DirectionToPayId", "dbo.EXP_DirectionToPays");
            DropForeignKey("dbo.EXP_DirectionToPays_PriceList", "PriceListId", "dbo.EXP_PriceList");
            DropForeignKey("dbo.Contracts", "ManufacturerOrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Contracts", "AgentOrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Contracts", "PayerTranslationOrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Contracts", "PayerOrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Contracts", "HolderOrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Contracts", "ApplicantOrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.CommissionUnits", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.CommissionUnits", "UnitTypeId", "dbo.CommissionUnitTypes");
            DropForeignKey("dbo.CommissionUnits", "CommissionId", "dbo.Commissions");
            DropForeignKey("dbo.Commissions", "TypeId", "dbo.CommissionTypes");
            DropForeignKey("dbo.CommissionQuestions", "CommissionId", "dbo.Commissions");
            DropForeignKey("dbo.CommissionQuestions", "TypeId", "dbo.CommissionQuestionTypes");
            DropForeignKey("dbo.Commissions", "KindId", "dbo.CommissionKinds");
            DropForeignKey("dbo.CommissionDrugDosage", "CommissionId", "dbo.Commissions");
            DropForeignKey("dbo.aspnet_Users", "ApplicationId", "dbo.aspnet_Applications");
            DropForeignKey("dbo.aspnet_Roles", "ApplicationId", "dbo.aspnet_Applications");
            DropForeignKey("dbo.aspnet_Paths", "ApplicationId", "dbo.aspnet_Applications");
            DropForeignKey("dbo.aspnet_Membership", "ApplicationId", "dbo.aspnet_Applications");
            DropForeignKey("dbo.aspnet_UsersInRoles", "UserId", "dbo.aspnet_Users");
            DropForeignKey("dbo.aspnet_UsersInRoles", "RoleId", "dbo.aspnet_Roles");
            DropForeignKey("dbo.aspnet_Profile", "UserId", "dbo.aspnet_Users");
            DropForeignKey("dbo.aspnet_PersonalizationPerUser", "UserId", "dbo.aspnet_Users");
            DropForeignKey("dbo.aspnet_PersonalizationPerUser", "PathId", "dbo.aspnet_Paths");
            DropForeignKey("dbo.aspnet_PersonalizationAllUsers", "PathId", "dbo.aspnet_Paths");
            DropForeignKey("dbo.aspnet_Membership", "UserId", "dbo.aspnet_Users");
            DropForeignKey("dbo.Documents", "TemplateId", "dbo.Templates");
            DropForeignKey("dbo.Documents", "RemarkId", "dbo.RemarkTypes");
            DropForeignKey("dbo.Activities", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.Tasks", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Reports", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Reports", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.Tasks", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.AccessTasks", "ObjectId", "dbo.Tasks");
            DropForeignKey("dbo.AccessDocuments", "ObjectId", "dbo.Documents");
            DropIndex("dbo.EXP_RegistrationExpStepsExecutors", new[] { "StepId" });
            DropIndex("dbo.EXP_RegistrationExpStepsExecutors", new[] { "ExecutorId" });
            DropIndex("dbo.EXP_DirectionToPays_DrugDeclaration", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DirectionToPays_DrugDeclaration", new[] { "DirectionToPayId" });
            DropIndex("dbo.EXP_DIC_ResultByStage", new[] { "StageResultId" });
            DropIndex("dbo.EXP_DIC_ResultByStage", new[] { "StageId" });
            DropIndex("dbo.aspnet_UsersInRoles", new[] { "UserId" });
            DropIndex("dbo.aspnet_UsersInRoles", new[] { "RoleId" });
            DropIndex("dbo.TmcReportTasks", new[] { "refTmcReport" });
            DropIndex("dbo.RequestList", new[] { "RequestOrderId" });
            DropIndex("dbo.obk_exchangerate", new[] { "currency_id" });
            DropIndex("dbo.OBK_CertificateReferenceFieldHistory", new[] { "OBK_CertificateReferenceId" });
            DropIndex("dbo.OBK_CertificateReference", new[] { "CertificateValidityTypeId" });
            DropIndex("dbo.OBK_BlankNumber", new[] { "ParentId" });
            DropIndex("dbo.OBK_BlankNumber", new[] { "BlankTypeId" });
            DropIndex("dbo.obk_product_cost", new[] { "id" });
            DropIndex("dbo.obk_certification_copies", new[] { "certification_id" });
            DropIndex("dbo.obk_certifications", new[] { "product_id" });
            DropIndex("dbo.obk_appendix_series", new[] { "product_id" });
            DropIndex("dbo.obk_appendix_series", new[] { "appendix_id" });
            DropIndex("dbo.obk_appendix", new[] { "certification_id" });
            DropIndex("dbo.LimsTmcTemp", new[] { "TmcInId" });
            DropIndex("dbo.LimsTmcTemp", new[] { "TmcId" });
            DropIndex("dbo.EXP_DIC_PrimaryOTD", new[] { "ParentId" });
            DropIndex("dbo.EMP_StatementSamples", new[] { "StatementId" });
            DropIndex("dbo.EMP_Statement", new[] { "NmirkId" });
            DropIndex("dbo.EMP_EAESStatement", new[] { "NmirkId" });
            DropIndex("dbo.VisitEmployeeTypes", new[] { "VisitTypeId" });
            DropIndex("dbo.Visits", new[] { "VisitStatusId" });
            DropIndex("dbo.Visits", new[] { "EmployeeId" });
            DropIndex("dbo.Visits", new[] { "VisitorId" });
            DropIndex("dbo.Visits", new[] { "VisitTypeId" });
            DropIndex("dbo.VisitEmployeeWorkingTimes", new[] { "EmployeeId" });
            DropIndex("dbo.PP_ProtocolProductPrices", new[] { "ProtocolId" });
            DropIndex("dbo.PP_ProtocolComissionMembers", new[] { "EmployeeId" });
            DropIndex("dbo.PP_ProtocolComissionMembers", new[] { "ProtocolId" });
            DropIndex("dbo.GridSettings", new[] { "EmployeeId" });
            DropIndex("dbo.FileLinksCategoryComRecord", new[] { "UserId" });
            DropIndex("dbo.FileLinksCategoryComRecord", new[] { "CommentId" });
            DropIndex("dbo.ContractProcSettings", new[] { "ProcCenterHeadId" });
            DropIndex("dbo.ContractSignedDatas", new[] { "ContractId" });
            DropIndex("dbo.EXP_DrugType", new[] { "DrugTypeKind" });
            DropIndex("dbo.EXP_DrugType", new[] { "DrugTypeId" });
            DropIndex("dbo.EXP_DrugType", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DrugProtectionDoc", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DrugPrimaryNTD", new[] { "TypeFileNDId" });
            DropIndex("dbo.EXP_DrugPrimaryNTD", new[] { "TypeNDId" });
            DropIndex("dbo.EXP_DrugPrimaryNTD", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DrugPrimaryKind", new[] { "PrimaryKindId" });
            DropIndex("dbo.EXP_DrugPrimaryKind", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DrugPatent", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DrugOrganizations", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DrugDeclarationFieldHistory", new[] { "UserId" });
            DropIndex("dbo.EXP_DrugDeclarationFieldHistory", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DrugDeclarationComRecord", new[] { "UserId" });
            DropIndex("dbo.EXP_DrugDeclarationComRecord", new[] { "CommentId" });
            DropIndex("dbo.EXP_DrugDeclarationCom", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DrugChangeType", new[] { "ChangeTypeId" });
            DropIndex("dbo.EXP_DrugChangeType", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DrugDeclarationHistory", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DrugDeclarationHistory", new[] { "StatusId" });
            DropIndex("dbo.EXP_DrugDeclarationHistory", new[] { "UserId" });
            DropIndex("dbo.EXP_RegistrationTypes", new[] { "RefId" });
            DropIndex("dbo.EXP_RegistrationTypes", new[] { "ParentId" });
            DropIndex("dbo.EXP_RegistrationExpSteps", new[] { "SupervisingEmployeeId" });
            DropIndex("dbo.EXP_RegistrationExpSteps", new[] { "RefId" });
            DropIndex("dbo.EXP_RegistrationExpSteps", new[] { "RegistrationId" });
            DropIndex("dbo.EXP_ExpertiseStageExecutors", new[] { "ExecutorId" });
            DropIndex("dbo.EXP_ExpertiseStageExecutors", new[] { "ExpertiseStageId" });
            DropIndex("dbo.EXP_DrugPrice", new[] { "DrugDosageId" });
            DropIndex("dbo.EXP_ExpertiseStageRemark", new[] { "ExecuterId" });
            DropIndex("dbo.EXP_ExpertiseStageRemark", new[] { "RemarkTypeId" });
            DropIndex("dbo.EXP_ExpertiseStageRemark", new[] { "StageId" });
            DropIndex("dbo.EXP_DrugPrimaryRemark", new[] { "ExecuterId" });
            DropIndex("dbo.EXP_DrugPrimaryRemark", new[] { "RemarkTypeId" });
            DropIndex("dbo.EXP_DrugPrimaryRemark", new[] { "DrugDeclarationId" });
            DropIndex("dbo.PP_PharmaList", new[] { "RegionId" });
            DropIndex("dbo.PP_PharmaList", new[] { "UserId" });
            DropIndex("dbo.PP_DIC_AvgExchangeRate", new[] { "ChangeEmployeeId" });
            DropIndex("dbo.PP_DIC_AvgExchangeRate", new[] { "CurrencyId" });
            DropIndex("dbo.PriceProjectsHistory", new[] { "PriceProjectId" });
            DropIndex("dbo.PriceProjectsHistory", new[] { "UserId" });
            DropIndex("dbo.PriceProjectFieldHistory", new[] { "UserId" });
            DropIndex("dbo.PriceProjectFieldHistory", new[] { "PriceProjectId" });
            DropIndex("dbo.PriceProjectComRecord", new[] { "UserId" });
            DropIndex("dbo.PriceProjectComRecord", new[] { "CommentId" });
            DropIndex("dbo.PriceProjectCom", new[] { "PriceProjectId" });
            DropIndex("dbo.PP_AttachRemarks", new[] { "AttachPriceDicId" });
            DropIndex("dbo.PP_AttachRemarks", new[] { "PriceProjectId" });
            DropIndex("dbo.LimsEquipmentActSpareParts", new[] { "EquipmentActId" });
            DropIndex("dbo.LimsEquipmentActSpareParts", new[] { "LocationId" });
            DropIndex("dbo.LimsEquipmentPlan", new[] { "HeadOfOpoloId" });
            DropIndex("dbo.LimsEquipmentPlan", new[] { "DirectorRcId" });
            DropIndex("dbo.LimsEquipmentPlan", new[] { "PlanTypeId" });
            DropIndex("dbo.LimsPlanEquipmentLink", new[] { "EquipmentPlanId" });
            DropIndex("dbo.LimsPlanEquipmentLink", new[] { "EquipmentId" });
            DropIndex("dbo.LimsEquipmentJournal", new[] { "JournalTypeId" });
            DropIndex("dbo.LimsEquipmentJournalRecord", new[] { "ExecutorId" });
            DropIndex("dbo.LimsEquipmentJournalRecord", new[] { "JournalId" });
            DropIndex("dbo.LimsEquipmentJournalRecord", new[] { "EquipmentId" });
            DropIndex("dbo.LimsApplicationJournal", new[] { "AccepterId" });
            DropIndex("dbo.LimsApplicationJournal", new[] { "EngineerId" });
            DropIndex("dbo.LimsApplicationJournal", new[] { "ApplicantId" });
            DropIndex("dbo.LimsApplicationJournal", new[] { "EquipmentId" });
            DropIndex("dbo.LimsEquipment", new[] { "ResponsiblePersonId" });
            DropIndex("dbo.LimsEquipment", new[] { "LaboratoryId" });
            DropIndex("dbo.LimsEquipment", new[] { "StatusId" });
            DropIndex("dbo.LimsEquipment", new[] { "LocationId" });
            DropIndex("dbo.LimsEquipment", new[] { "EquipmentTypeId" });
            DropIndex("dbo.LimsEquipment", new[] { "CountryProductionId" });
            DropIndex("dbo.LimsEquipment", new[] { "ProducerId" });
            DropIndex("dbo.LimsEquipment", new[] { "ModelId" });
            DropIndex("dbo.LimsEquipmentAct", new[] { "EngineerId" });
            DropIndex("dbo.LimsEquipmentAct", new[] { "DirectorRCId" });
            DropIndex("dbo.LimsEquipmentAct", new[] { "HeadOfLaboratoryId" });
            DropIndex("dbo.LimsEquipmentAct", new[] { "ActTypeId" });
            DropIndex("dbo.LimsEquipmentAct", new[] { "EquipmentId" });
            DropIndex("dbo.FileLinks", new[] { "StatusId" });
            DropIndex("dbo.FileLinks", new[] { "StageId" });
            DropIndex("dbo.FileLinks", new[] { "OwnerId" });
            DropIndex("dbo.FileLinks", new[] { "ParentId" });
            DropIndex("dbo.FileLinks", new[] { "CategoryId" });
            DropIndex("dbo.OBK_LetterFromEdo", new[] { "LetterPortalEdoID" });
            DropIndex("dbo.OBK_LetterPortalEdo", new[] { "OBKLetterRegID" });
            DropIndex("dbo.OBK_LetterPortalEdo", new[] { "ContractId" });
            DropIndex("dbo.OBK_DirectionSignData", new[] { "DirectionToPaymentId" });
            DropIndex("dbo.OBK_DirectionToPayments", new[] { "CreateEmployeeId" });
            DropIndex("dbo.OBK_DirectionToPayments", new[] { "PayerId" });
            DropIndex("dbo.OBK_DirectionToPayments", new[] { "ContractId" });
            DropIndex("dbo.OBK_DeclarantContact", new[] { "BankId" });
            DropIndex("dbo.OBK_DeclarantContact", new[] { "DeclarantId" });
            DropIndex("dbo.EMP_DirectionToPayments", new[] { "StatusId" });
            DropIndex("dbo.EMP_DirectionToPayments", new[] { "CreateEmployeeId" });
            DropIndex("dbo.EMP_DirectionToPayments", new[] { "PayerId" });
            DropIndex("dbo.EMP_DirectionToPayments", new[] { "ContractId" });
            DropIndex("dbo.EMP_Ref_ServiceType", new[] { "DegreeRiskId" });
            DropIndex("dbo.EMP_Ref_ServiceType", new[] { "ParentId" });
            DropIndex("dbo.EMP_Ref_PriceList", new[] { "PriceTypeId" });
            DropIndex("dbo.EMP_Ref_PriceList", new[] { "ServiceTypeId" });
            DropIndex("dbo.EMP_CostWorks", new[] { "ContractId" });
            DropIndex("dbo.EMP_CostWorks", new[] { "PriceListId" });
            DropIndex("dbo.EMP_ContractStageExecutors", new[] { "ExecutorId" });
            DropIndex("dbo.EMP_ContractStageExecutors", new[] { "ContractStageId" });
            DropIndex("dbo.EMP_ContractStage", new[] { "ParentStageId" });
            DropIndex("dbo.EMP_ContractStage", new[] { "StageStatusId" });
            DropIndex("dbo.EMP_ContractStage", new[] { "StageId" });
            DropIndex("dbo.EMP_ContractStage", new[] { "ContractId" });
            DropIndex("dbo.EMP_ContractSignData", new[] { "ContractId" });
            DropIndex("dbo.OBK_ContractHistory", new[] { "StatusId" });
            DropIndex("dbo.EMP_ContractHistory", new[] { "ContractId" });
            DropIndex("dbo.EMP_ContractHistory", new[] { "StatusId" });
            DropIndex("dbo.EMP_ContractExtHistory", new[] { "EmployeeId" });
            DropIndex("dbo.EMP_ContractExtHistory", new[] { "ContractId" });
            DropIndex("dbo.EMP_Contract", new[] { "ContractScopeId" });
            DropIndex("dbo.EMP_Contract", new[] { "ContractStatusId" });
            DropIndex("dbo.EMP_Contract", new[] { "ContractType" });
            DropIndex("dbo.EMP_Contract", new[] { "PayerContactId" });
            DropIndex("dbo.EMP_Contract", new[] { "ManufacturContactId" });
            DropIndex("dbo.EMP_Contract", new[] { "DeclarantContactId" });
            DropIndex("dbo.EMP_Contract", new[] { "PayerId" });
            DropIndex("dbo.EMP_Contract", new[] { "ManufacturId" });
            DropIndex("dbo.EMP_Contract", new[] { "DeclarantId" });
            DropIndex("dbo.EMP_Contract", new[] { "EmployeeId" });
            DropIndex("dbo.OBK_ContractSignedDatas", new[] { "ContractId" });
            DropIndex("dbo.OBK_ContractFactoryComRecord", new[] { "UserId" });
            DropIndex("dbo.OBK_ContractFactoryComRecord", new[] { "CommentId" });
            DropIndex("dbo.OBK_ContractFactoryCom", new[] { "ContractFactoryId" });
            DropIndex("dbo.OBK_ContractFactory", new[] { "ContractId" });
            DropIndex("dbo.OBK_ContractExtHistory", new[] { "EmployeeId" });
            DropIndex("dbo.OBK_ContractExtHistory", new[] { "ContractId" });
            DropIndex("dbo.OBK_ContractExtHistory", new[] { "StatusId" });
            DropIndex("dbo.OBK_ContractComRecord", new[] { "UserId" });
            DropIndex("dbo.OBK_ContractComRecord", new[] { "CommentId" });
            DropIndex("dbo.OBK_ContractCom", new[] { "ContractId" });
            DropIndex("dbo.OBK_StageExpDocumentResult", new[] { "AssessmetDeclarationId" });
            DropIndex("dbo.OBK_OP_Commission", new[] { "RoleId" });
            DropIndex("dbo.OBK_OP_Commission", new[] { "EmployeeId" });
            DropIndex("dbo.OBK_OP_Commission", new[] { "DeclarationId" });
            DropIndex("dbo.OBK_CertificateOfCompletion", new[] { "AssessmentDeclarationId" });
            DropIndex("dbo.OBK_CertificateOfCompletion", new[] { "ContractId" });
            DropIndex("dbo.OBK_AssessmentDeclarationHistory", new[] { "AssessmentDeclarationId" });
            DropIndex("dbo.OBK_AssessmentDeclarationHistory", new[] { "StatusId" });
            DropIndex("dbo.OBK_AssessmentDeclarationFieldHistory", new[] { "AssessmentDeclarationId" });
            DropIndex("dbo.OBK_AssessmentDeclarationFieldHistory", new[] { "UserId" });
            DropIndex("dbo.OBK_AssessmentDeclarationComRecord", new[] { "UserId" });
            DropIndex("dbo.OBK_AssessmentDeclarationComRecord", new[] { "CommentId" });
            DropIndex("dbo.OBK_AssessmentDeclarationCom", new[] { "AssessmentDeclarationId" });
            DropIndex("dbo.OBK_AssessmentDeclaration__OBK_ExpertCouncil", new[] { "ExpertCouncilId" });
            DropIndex("dbo.OBK_AssessmentDeclaration__OBK_ExpertCouncil", new[] { "DeclarationId" });
            DropIndex("dbo.OBK_TaskStatus", new[] { "UnitLaboratoryId" });
            DropIndex("dbo.OBK_TaskStatus", new[] { "StatusId" });
            DropIndex("dbo.OBK_TaskStatus", new[] { "TaskId" });
            DropIndex("dbo.OBK_TaskExecutor", new[] { "TaskMaterialId" });
            DropIndex("dbo.OBK_TaskExecutor", new[] { "ExecutorId" });
            DropIndex("dbo.OBK_TaskExecutor", new[] { "TaskId" });
            DropIndex("dbo.OBK_Ref_LaboratoryRegulation", new[] { "LaboratoryMarkId" });
            DropIndex("dbo.OBK_ResearchCenterResult", new[] { "ExecutorId" });
            DropIndex("dbo.OBK_ResearchCenterResult", new[] { "LaboratoryRegulationId" });
            DropIndex("dbo.OBK_ResearchCenterResult", new[] { "LaboratoryMarkId" });
            DropIndex("dbo.OBK_ResearchCenterResult", new[] { "TaskMaterialId" });
            DropIndex("dbo.EXP_DrugUseMethod", new[] { "UseMethodsId" });
            DropIndex("dbo.EXP_DrugUseMethod", new[] { "DrugDeclarationId" });
            DropIndex("dbo.sr_register_use_methods", new[] { "use_method_id" });
            DropIndex("dbo.sr_register_use_methods", new[] { "register_id" });
            DropIndex("dbo.sr_pharmacological_actions", new[] { "parent_id" });
            DropIndex("dbo.sr_register_pharmacological_actions", new[] { "pharmacological_action_id" });
            DropIndex("dbo.sr_register_pharmacological_actions", new[] { "register_id" });
            DropIndex("dbo.sr_dosage_forms", new[] { "parent_id" });
            DropIndex("dbo.sr_atc_codes", new[] { "parent_id" });
            DropIndex("dbo.sr_register_drugs", new[] { "nd_name_id" });
            DropIndex("dbo.sr_register_drugs", new[] { "category_id" });
            DropIndex("dbo.sr_register_drugs", new[] { "life_type_id" });
            DropIndex("dbo.sr_register_drugs", new[] { "dosage_measure_id" });
            DropIndex("dbo.sr_register_drugs", new[] { "dosage_form_id" });
            DropIndex("dbo.sr_register_drugs", new[] { "atc_id" });
            DropIndex("dbo.sr_register_drugs", new[] { "int_name_id" });
            DropIndex("dbo.sr_register_drugs", new[] { "drug_type_id" });
            DropIndex("dbo.sr_substances", new[] { "category_id" });
            DropIndex("dbo.sr_register_substances", new[] { "nd_type_id" });
            DropIndex("dbo.sr_register_substances", new[] { "substance_id" });
            DropIndex("dbo.sr_register_substances", new[] { "substance_type_id" });
            DropIndex("dbo.sr_register_substances", new[] { "register_id" });
            DropIndex("dbo.sr_register_producers", new[] { "country_id" });
            DropIndex("dbo.sr_register_producers", new[] { "producer_type_id" });
            DropIndex("dbo.sr_register_producers", new[] { "producer_id" });
            DropIndex("dbo.sr_register_producers", new[] { "register_id" });
            DropIndex("dbo.sr_producers", new[] { "form_type_id" });
            DropIndex("dbo.sr_register_mt_parts", new[] { "producer_id" });
            DropIndex("dbo.sr_register_mt_parts", new[] { "register_id" });
            DropIndex("dbo.sr_register_mt_kz", new[] { "id" });
            DropIndex("dbo.sr_mt_categories", new[] { "parent_id" });
            DropIndex("dbo.sr_degree_risk_details", new[] { "degree_risk_id" });
            DropIndex("dbo.sr_register_mt", new[] { "risk_detail_id" });
            DropIndex("dbo.sr_register_mt", new[] { "degree_risk_id" });
            DropIndex("dbo.sr_register_mt", new[] { "mt_category_id" });
            DropIndex("dbo.sr_register_mt", new[] { "id" });
            DropIndex("dbo.sr_register_instructions_files", new[] { "id" });
            DropIndex("dbo.sr_register_instructions", new[] { "instruction_type_id" });
            DropIndex("dbo.sr_register_instructions", new[] { "register_id" });
            DropIndex("dbo.sr_register_boxes_rk_ls", new[] { "id" });
            DropIndex("dbo.EXP_DrugWrapping", new[] { "DrugDosageId" });
            DropIndex("dbo.EXP_DrugWrapping", new[] { "VolumeMeasureId" });
            DropIndex("dbo.EXP_DrugWrapping", new[] { "SizeMeasureId" });
            DropIndex("dbo.EXP_DrugWrapping", new[] { "WrappingKindId" });
            DropIndex("dbo.EXP_DrugWrapping", new[] { "WrappingTypeId" });
            DropIndex("dbo.sr_boxes", new[] { "parent_id" });
            DropIndex("dbo.sr_register_boxes", new[] { "box_id" });
            DropIndex("dbo.sr_register_boxes", new[] { "register_id" });
            DropIndex("dbo.sr_drug_forms", new[] { "sec_box_id" });
            DropIndex("dbo.sr_drug_forms", new[] { "pr_box_id" });
            DropIndex("dbo.sr_drug_forms", new[] { "register_id" });
            DropIndex("dbo.sr_register", new[] { "storage_measure_id" });
            DropIndex("dbo.sr_register", new[] { "reg_action_id" });
            DropIndex("dbo.sr_register", new[] { "reg_type_id" });
            DropIndex("dbo.sr_register_names", new[] { "country_id" });
            DropIndex("dbo.sr_register_names", new[] { "register_id" });
            DropIndex("dbo.EXP_DrugOtherCountry", new[] { "CountryId" });
            DropIndex("dbo.EXP_DrugOtherCountry", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DrugExportTrade", new[] { "CountryId" });
            DropIndex("dbo.EXP_DrugExportTrade", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DrugSubstanceManufacture", new[] { "CountryId" });
            DropIndex("dbo.EXP_DrugSubstanceManufacture", new[] { "DrugSubstanceId" });
            DropIndex("dbo.EXP_DrugSubstance", new[] { "NormDocFarmId" });
            DropIndex("dbo.EXP_DrugSubstance", new[] { "DrugDosageId" });
            DropIndex("dbo.EXP_DrugSubstance", new[] { "OriginId" });
            DropIndex("dbo.EXP_DrugSubstance", new[] { "PlantKindId" });
            DropIndex("dbo.EXP_DrugSubstance", new[] { "CountryId" });
            DropIndex("dbo.EXP_DrugSubstance", new[] { "MeasureId" });
            DropIndex("dbo.EXP_DrugSubstance", new[] { "SubstanceId" });
            DropIndex("dbo.EXP_DrugSubstance", new[] { "SubstanceTypeId" });
            DropIndex("dbo.OBK_ZBKCopySignData", new[] { "SignerId" });
            DropIndex("dbo.OBK_ZBKCopySignData", new[] { "OBK_ZBKCopyId" });
            DropIndex("dbo.OBK_ZBKCopyBlank", new[] { "ZBKCopyId" });
            DropIndex("dbo.OBK_ZBKCopy", new[] { "EmployeeId" });
            DropIndex("dbo.OBK_ZBKCopy", new[] { "OBK_StageExpDocumentId" });
            DropIndex("dbo.OBK_StageExpDocument", new[] { "RefReasonId" });
            DropIndex("dbo.OBK_StageExpDocument", new[] { "AssessmentDeclarationId" });
            DropIndex("dbo.OBK_StageExpDocument", new[] { "ProductSeriesId" });
            DropIndex("dbo.OBK_StageExpDocument", new[] { "ProductId" });
            DropIndex("dbo.OBK_RS_ProductsComRecord", new[] { "UserId" });
            DropIndex("dbo.OBK_RS_ProductsComRecord", new[] { "CommentId" });
            DropIndex("dbo.OBK_RS_ProductsCom", new[] { "ProductId" });
            DropIndex("dbo.OBK_MtPart", new[] { "ProductId" });
            DropIndex("dbo.OBK_Ref_PriceList", new[] { "DegreeRiskId" });
            DropIndex("dbo.OBK_Ref_PriceList", new[] { "ServiceTypeId" });
            DropIndex("dbo.OBK_Ref_PriceList", new[] { "UnitId" });
            DropIndex("dbo.OBK_Ref_PriceList", new[] { "TypeId" });
            DropIndex("dbo.OBK_ContractPriceComRecord", new[] { "UserId" });
            DropIndex("dbo.OBK_ContractPriceComRecord", new[] { "CommentId" });
            DropIndex("dbo.OBK_ContractPriceCom", new[] { "ContractPriceId" });
            DropIndex("dbo.OBK_ContractPrice", new[] { "ContractId" });
            DropIndex("dbo.OBK_ContractPrice", new[] { "ProductId" });
            DropIndex("dbo.OBK_ContractPrice", new[] { "PriceRefId" });
            DropIndex("dbo.OBK_RS_Products", new[] { "ContractId" });
            DropIndex("dbo.OBK_Products_SeriesComRecord", new[] { "UserId" });
            DropIndex("dbo.OBK_Products_SeriesComRecord", new[] { "CommentId" });
            DropIndex("dbo.OBK_Products_SeriesCom", new[] { "ProductSerieId" });
            DropIndex("dbo.OBK_Procunts_Series", new[] { "SeriesMeasureId" });
            DropIndex("dbo.OBK_Procunts_Series", new[] { "OBK_RS_ProductsId" });
            DropIndex("dbo.OBK_TaskMaterial", new[] { "LaboratoryAssistantId" });
            DropIndex("dbo.OBK_TaskMaterial", new[] { "StatusId" });
            DropIndex("dbo.OBK_TaskMaterial", new[] { "ExternalConditionId" });
            DropIndex("dbo.OBK_TaskMaterial", new[] { "StorageConditionId" });
            DropIndex("dbo.OBK_TaskMaterial", new[] { "UnitLaboratoryId" });
            DropIndex("dbo.OBK_TaskMaterial", new[] { "ProductSeriesId" });
            DropIndex("dbo.OBK_TaskMaterial", new[] { "LaboratoryTypeId" });
            DropIndex("dbo.OBK_TaskMaterial", new[] { "TaskId" });
            DropIndex("dbo.OBK_AssessmentStageOP", new[] { "StageStatus" });
            DropIndex("dbo.OBK_AssessmentStageOP", new[] { "DeclarationId" });
            DropIndex("dbo.OBK_ZBKCopyStageSignData", new[] { "StageId" });
            DropIndex("dbo.OBK_ZBKCopyStageSignData", new[] { "SignerId" });
            DropIndex("dbo.OBK_ZBKCopyStageExecutors", new[] { "ExecutorId" });
            DropIndex("dbo.OBK_ZBKCopyStageExecutors", new[] { "ZBKCopyStageId" });
            DropIndex("dbo.OBK_ZBKCopyStage", new[] { "StageStatusId" });
            DropIndex("dbo.OBK_ZBKCopyStage", new[] { "StageId" });
            DropIndex("dbo.OBK_ContractStageExecutors", new[] { "ExecutorId" });
            DropIndex("dbo.OBK_ContractStageExecutors", new[] { "ContractStageId" });
            DropIndex("dbo.OBK_ContractStage", new[] { "ParentStageId" });
            DropIndex("dbo.OBK_ContractStage", new[] { "StageStatusId" });
            DropIndex("dbo.OBK_ContractStage", new[] { "StageId" });
            DropIndex("dbo.OBK_ContractStage", new[] { "ContractId" });
            DropIndex("dbo.OBK_AssessmentStageSignData", new[] { "SignerId" });
            DropIndex("dbo.OBK_AssessmentStageSignData", new[] { "AssessmentStageId" });
            DropIndex("dbo.OBK_AssessmentStageExecutors", new[] { "ExecutorId" });
            DropIndex("dbo.OBK_AssessmentStageExecutors", new[] { "AssessmentStageId" });
            DropIndex("dbo.OBK_AssessmentStage", new[] { "StageStatusId" });
            DropIndex("dbo.OBK_AssessmentStage", new[] { "StageId" });
            DropIndex("dbo.OBK_AssessmentStage", new[] { "DeclarationId" });
            DropIndex("dbo.OBK_Tasks", new[] { "TaskStatusId" });
            DropIndex("dbo.OBK_Tasks", new[] { "AssessmentDeclarationId" });
            DropIndex("dbo.OBK_Tasks", new[] { "ActReceptionId" });
            DropIndex("dbo.OBK_Tasks", new[] { "UnitId" });
            DropIndex("dbo.OBK_Tasks", new[] { "ExecutorId" });
            DropIndex("dbo.OBK_ActReception", new[] { "OBK_AssessmentDeclarationId" });
            DropIndex("dbo.OBK_ActReception", new[] { "StorageConditionsId" });
            DropIndex("dbo.OBK_ActReception", new[] { "MarkingId" });
            DropIndex("dbo.OBK_ActReception", new[] { "PackageConditionId" });
            DropIndex("dbo.OBK_ActReception", new[] { "InspectionInstalledId" });
            DropIndex("dbo.OBK_ActReception", new[] { "ProductSamplesId" });
            DropIndex("dbo.OBK_ActReception", new[] { "sr_measuresId" });
            DropIndex("dbo.OBK_AssessmentDeclaration", new[] { "OBK_AssessmentDeclaration2_Id" });
            DropIndex("dbo.OBK_AssessmentDeclaration", new[] { "TypeId" });
            DropIndex("dbo.OBK_AssessmentDeclaration", new[] { "CertificateTypeId" });
            DropIndex("dbo.OBK_AssessmentDeclaration", new[] { "ContractId" });
            DropIndex("dbo.OBK_Contract", new[] { "OBK_Contract2_Id" });
            DropIndex("dbo.OBK_Contract", new[] { "ContractAdditionType" });
            DropIndex("dbo.OBK_Contract", new[] { "Signer" });
            DropIndex("dbo.OBK_Contract", new[] { "ExpertOrganization" });
            DropIndex("dbo.OBK_Contract", new[] { "DeclarantContactId" });
            DropIndex("dbo.OBK_Contract", new[] { "DeclarantId" });
            DropIndex("dbo.OBK_Contract", new[] { "Status" });
            DropIndex("dbo.OBK_Contract", new[] { "Type" });
            DropIndex("dbo.Units", new[] { "EmployeeId" });
            DropIndex("dbo.Units", new[] { "ParentId" });
            DropIndex("dbo.DIC_Storages", new[] { "OrganizationId" });
            DropIndex("dbo.DIC_Storages", new[] { "ParentId" });
            DropIndex("dbo.EXP_Materials", new[] { "ConcentrationUnitId" });
            DropIndex("dbo.EXP_Materials", new[] { "ConcordanceStatementId" });
            DropIndex("dbo.EXP_Materials", new[] { "ExternalStateId" });
            DropIndex("dbo.EXP_Materials", new[] { "StatusId" });
            DropIndex("dbo.EXP_Materials", new[] { "StorageConditionId" });
            DropIndex("dbo.EXP_Materials", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_Materials", new[] { "StorageId" });
            DropIndex("dbo.EXP_Materials", new[] { "UnitId" });
            DropIndex("dbo.EXP_Materials", new[] { "CountryId" });
            DropIndex("dbo.EXP_Materials", new[] { "ProducerId" });
            DropIndex("dbo.EXP_Materials", new[] { "VolumeUnitId" });
            DropIndex("dbo.EXP_Materials", new[] { "DosageUnitId" });
            DropIndex("dbo.EXP_Materials", new[] { "DrugFormId" });
            DropIndex("dbo.EXP_Materials", new[] { "TypeId" });
            DropIndex("dbo.EXP_MaterialDirections", new[] { "ExecutorEmployeeId" });
            DropIndex("dbo.EXP_MaterialDirections", new[] { "SendEmployeeId" });
            DropIndex("dbo.EXP_MaterialDirections", new[] { "StatusId" });
            DropIndex("dbo.EXP_MaterialDirections", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_ExpertisePrimaryFinalDoc", new[] { "DosageStageId" });
            DropIndex("dbo.EXP_ExpertiseStageDosageResult", new[] { "CommissionId" });
            DropIndex("dbo.EXP_ExpertiseStageDosageResult", new[] { "ResultId" });
            DropIndex("dbo.EXP_ExpertiseStageDosageResult", new[] { "StageDosageId" });
            DropIndex("dbo.EXP_ExpertiseSafetyreportFinalDoc", new[] { "DosageStageId" });
            DropIndex("dbo.EXP_ExpertisePharmacologicalFinalDoc", new[] { "DosageStageId" });
            DropIndex("dbo.EXP_ExpertisePharmaceuticalFinalDoc", new[] { "DosageStageId" });
            DropIndex("dbo.EXP_DrugAnaliseIndicator", new[] { "AnalyseIndicator" });
            DropIndex("dbo.EXP_DrugAnaliseIndicator", new[] { "DosageStageId" });
            DropIndex("dbo.EXP_ExpertiseStageDosage", new[] { "FinalDocStatusId" });
            DropIndex("dbo.EXP_ExpertiseStageDosage", new[] { "ResultId" });
            DropIndex("dbo.EXP_ExpertiseStageDosage", new[] { "DosageId" });
            DropIndex("dbo.EXP_ExpertiseStageDosage", new[] { "StageId" });
            DropIndex("dbo.EXP_DrugPrimaryFinalDocument", new[] { "ResultId" });
            DropIndex("dbo.EXP_DrugPrimaryFinalDocument", new[] { "StatusId" });
            DropIndex("dbo.EXP_DrugPrimaryFinalDocument", new[] { "DrugDosageId" });
            DropIndex("dbo.EXP_AgreementProcSettingsTasks", new[] { "ExecutorId" });
            DropIndex("dbo.EXP_AgreementProcSettingsTasks", new[] { "ParentTaskId" });
            DropIndex("dbo.EXP_AgreementProcSettingsTasks", new[] { "TaskTypeId" });
            DropIndex("dbo.EXP_AgreementProcSettingsTasks", new[] { "ActivityId" });
            DropIndex("dbo.EXP_AgreementProcSettingsActivities", new[] { "ActivityTypeId" });
            DropIndex("dbo.EXP_AgreementProcSettingsActivities", new[] { "SettingId" });
            DropIndex("dbo.EXP_AgreementProcSettings", new[] { "AgreedDocTypeId" });
            DropIndex("dbo.EXP_Tasks", new[] { "ParentTaskId" });
            DropIndex("dbo.EXP_Tasks", new[] { "ActivityId" });
            DropIndex("dbo.EXP_Tasks", new[] { "ExecutorId" });
            DropIndex("dbo.EXP_Tasks", new[] { "AuthorId" });
            DropIndex("dbo.EXP_Tasks", new[] { "StatusId" });
            DropIndex("dbo.EXP_Tasks", new[] { "TypeId" });
            DropIndex("dbo.EXP_Activities", new[] { "DocumentTypeId" });
            DropIndex("dbo.EXP_Activities", new[] { "AuthorId" });
            DropIndex("dbo.EXP_Activities", new[] { "StatusId" });
            DropIndex("dbo.EXP_Activities", new[] { "TypeId" });
            DropIndex("dbo.Dictionaries", new[] { "ParentId" });
            DropIndex("dbo.EXP_DrugCorespondence", new[] { "SubjectId" });
            DropIndex("dbo.EXP_DrugCorespondence", new[] { "StatusId" });
            DropIndex("dbo.EXP_DrugCorespondence", new[] { "KindId" });
            DropIndex("dbo.EXP_DrugCorespondence", new[] { "TypeId" });
            DropIndex("dbo.EXP_DrugCorespondence", new[] { "StageId" });
            DropIndex("dbo.EXP_DrugCorespondence", new[] { "MatchingId" });
            DropIndex("dbo.EXP_DrugCorespondence", new[] { "SignatoryId" });
            DropIndex("dbo.EXP_DrugCorespondence", new[] { "AuthorId" });
            DropIndex("dbo.EXP_DrugCorespondence", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_DrugCorespondenceRemark", new[] { "ExecuterId" });
            DropIndex("dbo.EXP_DrugCorespondenceRemark", new[] { "RemarkTypeId" });
            DropIndex("dbo.EXP_DrugCorespondenceRemark", new[] { "DrugCorespondenceId" });
            DropIndex("dbo.EXP_DrugAppDosageResult", new[] { "DrugDosageId" });
            DropIndex("dbo.EXP_DrugAppDosageResult", new[] { "RemarkTypeId" });
            DropIndex("dbo.EXP_DrugAppDosageRemark", new[] { "DrugDosageId" });
            DropIndex("dbo.EXP_DrugAppDosageRemark", new[] { "RemarkTypeId" });
            DropIndex("dbo.EXP_DrugDosage", new[] { "RegisterId" });
            DropIndex("dbo.EXP_DrugDosage", new[] { "AppPeriodMixMeasureDicId" });
            DropIndex("dbo.EXP_DrugDosage", new[] { "AppPeriodOpenMeasureDicId" });
            DropIndex("dbo.EXP_DrugDosage", new[] { "BestBeforeMeasureTypeDicId" });
            DropIndex("dbo.EXP_DrugDosage", new[] { "SaleTypeId" });
            DropIndex("dbo.EXP_DrugDosage", new[] { "DosageMeasureTypeId" });
            DropIndex("dbo.EXP_DrugDosage", new[] { "DrugDeclarationId" });
            DropIndex("dbo.CommissionDrugDosageNeedCommission", new[] { "StageId" });
            DropIndex("dbo.CommissionDrugDosageNeedCommission", new[] { "DrugDosageId" });
            DropIndex("dbo.EXP_ExpertiseStage", new[] { "ParentStageId" });
            DropIndex("dbo.EXP_ExpertiseStage", new[] { "ResultId" });
            DropIndex("dbo.EXP_ExpertiseStage", new[] { "StatusId" });
            DropIndex("dbo.EXP_ExpertiseStage", new[] { "StageId" });
            DropIndex("dbo.EXP_ExpertiseStage", new[] { "DeclarationId" });
            DropIndex("dbo.EXP_CertificateOfCompletion", new[] { "CreateEmployeeId" });
            DropIndex("dbo.EXP_CertificateOfCompletion", new[] { "StatusId" });
            DropIndex("dbo.EXP_CertificateOfCompletion", new[] { "DrugDeclarationId" });
            DropIndex("dbo.EXP_CertificateOfCompletion", new[] { "DicStageId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "RegisterId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "AppPeriodMixMeasureDicId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "AppPeriodOpenMeasureDicId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "BestBeforeMeasureTypeDicId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "ProposedShelfLifeMeasureId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "ManufactureTypeId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "DosageMeasureTypeId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "SaleTypeId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "AtxId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "DrugFormId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "MnnId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "AccelerationTypeId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "StatusId" });
            DropIndex("dbo.EXP_DrugDeclaration", new[] { "TypeId" });
            DropIndex("dbo.EXP_DirectionToPays_PriceList", new[] { "PriceListId" });
            DropIndex("dbo.EXP_DirectionToPays_PriceList", new[] { "DirectionToPayId" });
            DropIndex("dbo.EXP_DirectionToPays", new[] { "StatusId" });
            DropIndex("dbo.EXP_DirectionToPays", new[] { "CreateEmployeeId" });
            DropIndex("dbo.EXP_DirectionToPays", new[] { "PayerId" });
            DropIndex("dbo.Organizations", new[] { "OriginalOrgId" });
            DropIndex("dbo.Contracts", new[] { "AgentOrganizationId" });
            DropIndex("dbo.Contracts", new[] { "AgentDocTypeDicId" });
            DropIndex("dbo.Contracts", new[] { "HolderTypeId" });
            DropIndex("dbo.Contracts", new[] { "ContractAdditionTypeId" });
            DropIndex("dbo.Contracts", new[] { "SignerId" });
            DropIndex("dbo.Contracts", new[] { "StatusId" });
            DropIndex("dbo.Contracts", new[] { "PayerTranslationOrganizationId" });
            DropIndex("dbo.Contracts", new[] { "ContractId" });
            DropIndex("dbo.Contracts", new[] { "PayerOrganizationId" });
            DropIndex("dbo.Contracts", new[] { "HolderOrganizationId" });
            DropIndex("dbo.Contracts", new[] { "DoverennostTypeDicId" });
            DropIndex("dbo.Contracts", new[] { "ApplicantOrganizationId" });
            DropIndex("dbo.Contracts", new[] { "ManufacturerOrganizationId" });
            DropIndex("dbo.ContractComments", new[] { "ContractId" });
            DropIndex("dbo.ContractComments", new[] { "AuthorId" });
            DropIndex("dbo.Employees", new[] { "OrganizationId" });
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropIndex("dbo.CommissionUnits", new[] { "UnitTypeId" });
            DropIndex("dbo.CommissionUnits", new[] { "EmployeeId" });
            DropIndex("dbo.CommissionUnits", new[] { "CommissionId" });
            DropIndex("dbo.CommissionQuestions", new[] { "TypeId" });
            DropIndex("dbo.CommissionQuestions", new[] { "CommissionId" });
            DropIndex("dbo.Commissions", new[] { "TypeId" });
            DropIndex("dbo.Commissions", new[] { "KindId" });
            DropIndex("dbo.CommissionDrugDosage", new[] { "ExpertResultId" });
            DropIndex("dbo.CommissionDrugDosage", new[] { "StageId" });
            DropIndex("dbo.CommissionDrugDosage", new[] { "DrugDosageId" });
            DropIndex("dbo.CommissionDrugDosage", new[] { "ConclusionTypeId" });
            DropIndex("dbo.CommissionDrugDosage", new[] { "CommissionId" });
            DropIndex("dbo.aspnet_Roles", new[] { "ApplicationId" });
            DropIndex("dbo.aspnet_Profile", new[] { "UserId" });
            DropIndex("dbo.aspnet_PersonalizationAllUsers", new[] { "PathId" });
            DropIndex("dbo.aspnet_Paths", new[] { "ApplicationId" });
            DropIndex("dbo.aspnet_PersonalizationPerUser", new[] { "UserId" });
            DropIndex("dbo.aspnet_PersonalizationPerUser", new[] { "PathId" });
            DropIndex("dbo.aspnet_Users", new[] { "ApplicationId" });
            DropIndex("dbo.aspnet_Membership", new[] { "ApplicationId" });
            DropIndex("dbo.aspnet_Membership", new[] { "UserId" });
            DropIndex("dbo.Reports", new[] { "TaskId" });
            DropIndex("dbo.Reports", new[] { "DocumentId" });
            DropIndex("dbo.AccessTasks", new[] { "ObjectId" });
            DropIndex("dbo.Tasks", new[] { "ActivityId" });
            DropIndex("dbo.Tasks", new[] { "DocumentId" });
            DropIndex("dbo.Activities", new[] { "DocumentId" });
            DropIndex("dbo.Documents", new[] { "RemarkId" });
            DropIndex("dbo.Documents", new[] { "TemplateId" });
            DropIndex("dbo.AccessDocuments", new[] { "ObjectId" });
            DropTable("dbo.EXP_RegistrationExpStepsExecutors");
            DropTable("dbo.EXP_DirectionToPays_DrugDeclaration");
            DropTable("dbo.EXP_DIC_ResultByStage");
            DropTable("dbo.aspnet_UsersInRoles");
            DropTable("dbo.vw_aspnet_WebPartState_User");
            DropTable("dbo.vw_aspnet_WebPartState_Shared");
            DropTable("dbo.vw_aspnet_WebPartState_Paths");
            DropTable("dbo.vw_aspnet_UsersInRoles");
            DropTable("dbo.vw_aspnet_Users");
            DropTable("dbo.vw_aspnet_Roles");
            DropTable("dbo.vw_aspnet_Profiles");
            DropTable("dbo.vw_aspnet_MembershipUsers");
            DropTable("dbo.vw_aspnet_Applications");
            DropTable("dbo.VisitsView");
            DropTable("dbo.UpdateScripts");
            DropTable("dbo.UnitSigner");
            DropTable("dbo.UnitsBank");
            DropTable("dbo.UnitsAddress");
            DropTable("dbo.UniqueIdentificators");
            DropTable("dbo.TmcView");
            DropTable("dbo.TmcUseOffView");
            DropTable("dbo.TmcReportsView");
            DropTable("dbo.TmcReportTasks");
            DropTable("dbo.TmcReports");
            DropTable("dbo.TmcReportDataSourceView");
            DropTable("dbo.TmcReportData");
            DropTable("dbo.TmcOutView");
            DropTable("dbo.TmcOuts");
            DropTable("dbo.TmcOutCountView");
            DropTable("dbo.TmcOutCounts");
            DropTable("dbo.TmcOffView");
            DropTable("dbo.TmcOffStateView");
            DropTable("dbo.TmcOffs");
            DropTable("dbo.TmcInView");
            DropTable("dbo.TmcCountStateView");
            DropTable("dbo.TmcApplicationComments");
            DropTable("dbo.TempTmc");
            DropTable("DbSync1C.sync_1c_scope_info");
            DropTable("DbSync1C.sync_1c_scope_config");
            DropTable("DbSync1C.sync_1c_schema_info");
            DropTable("DbSync1C.sync_1c_I1c_trl_ApplicationRefuseState_tracking");
            DropTable("DbSync1C.sync_1c_I1c_trl_ApplicationPaymentState_tracking");
            DropTable("DbSync1C.sync_1c_I1c_trl_Application_tracking");
            DropTable("DbSync1C.sync_1c_I1c_primary_PriceListElements_tracking");
            DropTable("DbSync1C.sync_1c_I1c_primary_DrugPackage_tracking");
            DropTable("DbSync1C.sync_1c_I1c_primary_Applications_tracking");
            DropTable("DbSync1C.sync_1c_I1c_primary_ApplicationRefuseState_tracking");
            DropTable("DbSync1C.sync_1c_I1c_primary_ApplicationPaymentState_tracking");
            DropTable("DbSync1C.sync_1c_I1c_primary_ApplicationPartPaymentState_tracking");
            DropTable("DbSync1C.sync_1c_I1c_primary_ApplicationElements_tracking");
            DropTable("DbSync1C.sync_1c_I1c_lims_Contracts_tracking");
            DropTable("DbSync1C.sync_1c_I1c_lims_ContractProducts_tracking");
            DropTable("DbSync1C.sync_1c_I1c_lims_Applications_tracking");
            DropTable("DbSync1C.sync_1c_I1c_lims_ApplicationElements_tracking");
            DropTable("DbSync1C.sync_1c_I1c_lims_ApplicationEExploitation_tracking");
            DropTable("DbSync1C.sync_1c_I1c_lims_ApplicationEAdmissions_tracking");
            DropTable("DbSync1C.sync_1c_I1c_exp_Contracts_tracking");
            DropTable("DbSync1C.sync_1c_I1c_act_Application_tracking");
            DropTable("dbo.SrReestrView");
            DropTable("dbo.SrProducerView");
            DropTable("dbo.sr_register_ordered");
            DropTable("dbo.SignDocument");
            DropTable("dbo.Settings");
            DropTable("dbo.Requests");
            DropTable("dbo.RequestOrderListView");
            DropTable("dbo.RequestListView");
            DropTable("dbo.RequestOrders");
            DropTable("dbo.RequestList");
            DropTable("dbo.RegisterProjectsView");
            DropTable("dbo.RegisterProjects");
            DropTable("dbo.RegisterProjectJournal");
            DropTable("dbo.RegisterOrdererView");
            DropTable("dbo.RegisterOrderer2Views");
            DropTable("dbo.Registeries");
            DropTable("dbo.Ref_Sources");
            DropTable("dbo.Ref_PurchasePrices");
            DropTable("dbo.Ref_MarketPrices");
            DropTable("dbo.Ref_Limits");
            DropTable("dbo.Ref_Knfs");
            DropTable("dbo.ReestrDirectionToPayView");
            DropTable("dbo.ReesrtObk");
            DropTable("dbo.PrtPrjsView");
            DropTable("dbo.PrtPrjsView2");
            DropTable("dbo.PrtPrjs");
            DropTable("dbo.ProjectsView");
            DropTable("dbo.ProjectDocument");
            DropTable("dbo.PrismReports");
            DropTable("dbo.PrismEnums");
            DropTable("dbo.PricesView");
            DropTable("dbo.Prices");
            DropTable("dbo.PriceRejectProjects");
            DropTable("dbo.PriceProjectsView");
            DropTable("dbo.PriceProjectJournal");
            DropTable("dbo.PriceProjectArchiveView");
            DropTable("dbo.PriceProjectArchiveDetails");
            DropTable("dbo.PriceProject_Ext");
            DropTable("dbo.PriceListExpertise");
            DropTable("dbo.PP_ProtocolListView");
            DropTable("dbo.PP_ProcessingJournal");
            DropTable("dbo.PP_DIC_AvgExchangeRateView");
            DropTable("dbo.PP_AttachRemarksView");
            DropTable("dbo.PermissionValues");
            DropTable("dbo.PermissionRole");
            DropTable("dbo.PermissionRoleKeys");
            DropTable("dbo.PermissionKeys");
            DropTable("dbo.PackagesView");
            DropTable("dbo.Packages");
            DropTable("dbo.OutgoingDocument");
            DropTable("dbo.OrphanDrugsIcdDeseasesView");
            DropTable("dbo.OrganizationsView");
            DropTable("dbo.OBKTaskListRegisterView");
            DropTable("dbo.OBKContractProductsView");
            DropTable("dbo.OBK_ZBKTransferRegisterView");
            DropTable("dbo.OBK_ZBKRegisterView");
            DropTable("dbo.OBK_ZBKCopyStageView");
            DropTable("dbo.OBK_UniqueNumber");
            DropTable("dbo.OBK_Ref_ValueAddedTax");
            DropTable("dbo.OBK_Ref_Nomenclature");
            DropTable("dbo.OBK_Ref_ContractDocumentType");
            DropTable("dbo.OBK_LetterAttachments");
            DropTable("dbo.OBK_DirectionToPaymentsView");
            DropTable("dbo.OBK_DeclarationCorrespondingView");
            DropTable("dbo.obk_exchangerate");
            DropTable("dbo.obk_currencies");
            DropTable("dbo.OBK_CorruptedBlankNumberView");
            DropTable("dbo.OBK_ContractWithoutAttachmentsView");
            DropTable("dbo.OBK_ContractView");
            DropTable("dbo.OBK_ContractReportView");
            DropTable("dbo.OBK_ContractRegisterView");
            DropTable("dbo.OBK_ContractHistoryView");
            DropTable("dbo.OBK_CertificateValidityType");
            DropTable("dbo.OBK_CertificateReferenceFieldHistory");
            DropTable("dbo.OBK_CertificateReference");
            DropTable("dbo.OBK_BlankType");
            DropTable("dbo.OBK_BlankNumber");
            DropTable("dbo.OBK_BlankAccountingView");
            DropTable("dbo.OBK_AssessmentDeclarationView");
            DropTable("dbo.OBK_AssessmentDeclarationReportView");
            DropTable("dbo.OBK_AssessmentDeclarationRegisterView");
            DropTable("dbo.OBK_AssessmentDeclarationProgramExecutors");
            DropTable("dbo.OBK_AssessmentDeclarationProgram");
            DropTable("dbo.OBK_Applicant");
            DropTable("dbo.obk_product_cost");
            DropTable("dbo.obk_certification_copies");
            DropTable("dbo.obk_certifications");
            DropTable("dbo.obk_products");
            DropTable("dbo.obk_appendix_series");
            DropTable("dbo.obk_appendix");
            DropTable("dbo.Notification");
            DropTable("dbo.Logs");
            DropTable("dbo.LinkUnits");
            DropTable("dbo.LinkDocuments");
            DropTable("dbo.LinkDictionaries");
            DropTable("dbo.LinkDicOrphanDrugsIcdDeseases");
            DropTable("dbo.LimsTmcTempView");
            DropTable("dbo.TmcIns");
            DropTable("dbo.Tmcs");
            DropTable("dbo.LimsTmcTemp");
            DropTable("dbo.LimsTmcOutView");
            DropTable("dbo.LimsTmcInView");
            DropTable("dbo.LimsTmcActualView");
            DropTable("dbo.LimsOrganization1CLink");
            DropTable("dbo.LimsEquipmentView");
            DropTable("dbo.LimsEquipmentPlanView");
            DropTable("dbo.LimsEquipmentJournalRecordView");
            DropTable("dbo.LimsEquipmentActView");
            DropTable("dbo.LimsEquipmentActSparePartsView");
            DropTable("dbo.LimsApplicationJournalView");
            DropTable("dbo.Lims_ActivityView");
            DropTable("dbo.Installation");
            DropTable("dbo.IncomingDocument");
            DropTable("dbo.I1c_trl_ApplicationRefuseState");
            DropTable("dbo.I1c_trl_ApplicationPaymentState");
            DropTable("dbo.I1c_trl_Application");
            DropTable("dbo.I1c_primary_PriceListElements");
            DropTable("dbo.I1c_primary_ObkPriceListElements");
            DropTable("dbo.I1c_primary_ObkApplications");
            DropTable("dbo.I1c_primary_ObkApplicationPaymentState");
            DropTable("dbo.I1c_primary_ObkApplicationNotFullPaymentState");
            DropTable("dbo.I1c_primary_DrugPackage");
            DropTable("dbo.I1c_primary_Applications");
            DropTable("dbo.I1c_primary_ApplicationRefuseState");
            DropTable("dbo.I1c_primary_ApplicationPaymentState");
            DropTable("dbo.I1c_primary_ApplicationPartPaymentState");
            DropTable("dbo.I1c_primary_ApplicationElements");
            DropTable("dbo.I1c_lims_Contracts");
            DropTable("dbo.I1c_lims_ContractProducts");
            DropTable("dbo.I1c_lims_Applications");
            DropTable("dbo.I1c_lims_ApplicationElements");
            DropTable("dbo.I1c_lims_ApplicationEExploitation");
            DropTable("dbo.I1c_lims_ApplicationEAdmissions");
            DropTable("dbo.I1c_exp_Contracts");
            DropTable("dbo.I1c_act_ObkApplication");
            DropTable("dbo.I1c_act_Application");
            DropTable("dbo.Holidays");
            DropTable("dbo.History");
            DropTable("dbo.FileLinkView");
            DropTable("dbo.ExtensionExecutions");
            DropTable("dbo.Experiences");
            DropTable("dbo.EXP_TaskRegistryView");
            DropTable("dbo.EXP_PriceListDrugTypeMapping");
            DropTable("dbo.EXP_PriceListDirectionToPayView");
            DropTable("dbo.EXP_MaterialDirectionsView");
            DropTable("dbo.EXP_ExpertiseStageDosageCommissionView");
            DropTable("dbo.EXP_DrugTypeView");
            DropTable("dbo.EXP_DrugSubstanceView");
            DropTable("dbo.EXP_DrugDosageView");
            DropTable("dbo.Exp_DrugDosageStageView");
            DropTable("dbo.Exp_DrugDosageStagePrevCommissionView");
            DropTable("dbo.Exp_DrugDosageStagePrevCommissionConcatinateView");
            DropTable("dbo.Exp_DrugDosageStageForAddView");
            DropTable("dbo.EXP_DrugDeclarationView");
            DropTable("dbo.EXP_DrugDeclarationRegisterView");
            DropTable("dbo.EXP_DrugDeclarationPaysInfoView");
            DropTable("dbo.EXP_DrugDeclarationDirectionToPayView");
            DropTable("dbo.Exp_DrugDeclarationChangeView");
            DropTable("dbo.EXP_DirectionToPaysView");
            DropTable("dbo.EXP_DIC_PrimaryOTD");
            DropTable("dbo.EXP_CertificateOfCompletionView");
            DropTable("dbo.EXP_ActivityTaskActualView");
            DropTable("dbo.EmployeePermissions");
            DropTable("dbo.EmployeePermissionRoles");
            DropTable("dbo.EMP_StatementStorageLife");
            DropTable("dbo.EMP_StatementMedicalDevicePackage");
            DropTable("dbo.EMP_StatementMedicalDeviceComplectation");
            DropTable("dbo.EMP_StatementCountryRegistration");
            DropTable("dbo.EMP_StatementChange");
            DropTable("dbo.EMP_Ref_HolderType");
            DropTable("dbo.EMP_Ref_ContractDocumentType");
            DropTable("dbo.EMP_Ref_ChangeType");
            DropTable("dbo.EMP_EAESStatementRegCountry");
            DropTable("dbo.EMP_StatementSamples");
            DropTable("dbo.EMP_Statement");
            DropTable("dbo.EXP_DIC_NMIRK");
            DropTable("dbo.EMP_EAESStatement");
            DropTable("dbo.EMP_ContractHistoryView");
            DropTable("dbo.DIC_OrphanDrugs");
            DropTable("dbo.DIC_IcdDeseases");
            DropTable("dbo.CorrespondenceDocument");
            DropTable("dbo.ContractsView");
            DropTable("dbo.ContractJournal");
            DropTable("dbo.ContractAdditionJournal");
            DropTable("dbo.CompositionsView");
            DropTable("dbo.Compositions");
            DropTable("dbo.CommissionsView");
            DropTable("dbo.CommissionDrugDosageCountView");
            DropTable("dbo.CommissionDrugDosageCount4WithoutRepeatView");
            DropTable("dbo.CommissionDrugDosageCount4View");
            DropTable("dbo.CommissionDrugDosageCount3WithoutRepeatView");
            DropTable("dbo.CommissionDrugDosageCount3View");
            DropTable("dbo.CommissionDrugDosageCount2View");
            DropTable("dbo.VisitEmployeeTypes");
            DropTable("dbo.VisitTypes");
            DropTable("dbo.VisitStatuses");
            DropTable("dbo.Visits");
            DropTable("dbo.VisitEmployeeWorkingTimes");
            DropTable("dbo.PP_ProtocolProductPrices");
            DropTable("dbo.PP_Protocols");
            DropTable("dbo.PP_ProtocolComissionMembers");
            DropTable("dbo.GridSettings");
            DropTable("dbo.FileLinksCategoryCom");
            DropTable("dbo.FileLinksCategoryComRecord");
            DropTable("dbo.ContractProcSettings");
            DropTable("dbo.ContractSignedDatas");
            DropTable("dbo.EXP_DIC_DrugTypeKind");
            DropTable("dbo.EXP_DIC_DrugType");
            DropTable("dbo.EXP_DrugType");
            DropTable("dbo.EXP_DrugProtectionDoc");
            DropTable("dbo.EXP_DIC_TypeND");
            DropTable("dbo.EXP_DIC_TypeFileND");
            DropTable("dbo.EXP_DrugPrimaryNTD");
            DropTable("dbo.EXP_DIC_PrimaryMark");
            DropTable("dbo.EXP_DrugPrimaryKind");
            DropTable("dbo.EXP_DrugPatent");
            DropTable("dbo.EXP_DrugOrganizations");
            DropTable("dbo.EXP_DrugDeclarationFieldHistory");
            DropTable("dbo.EXP_DrugDeclarationComRecord");
            DropTable("dbo.EXP_DrugDeclarationCom");
            DropTable("dbo.EXP_DIC_ChangeType");
            DropTable("dbo.EXP_DrugChangeType");
            DropTable("dbo.EXP_DrugDeclarationHistory");
            DropTable("dbo.EXP_DIC_Status");
            DropTable("dbo.EXP_DIC_PeriodMeasure");
            DropTable("dbo.EXP_DIC_ManufactureType");
            DropTable("dbo.EXP_DIC_AccelerationType");
            DropTable("dbo.EXP_DIC_Type");
            DropTable("dbo.EXP_RegistrationTypes");
            DropTable("dbo.EXP_RegistrationExpSteps");
            DropTable("dbo.EXP_ExpertiseStageExecutors");
            DropTable("dbo.EXP_DIC_StageStatus");
            DropTable("dbo.EXP_DrugPrice");
            DropTable("dbo.EXP_ExpertiseStageRemark");
            DropTable("dbo.EXP_DrugPrimaryRemark");
            DropTable("dbo.EXP_DIC_CorespondenceType");
            DropTable("dbo.EXP_DIC_CorespondenceSubject");
            DropTable("dbo.EXP_DIC_CorespondenceKind");
            DropTable("dbo.PP_PharmaList");
            DropTable("dbo.PP_DIC_AvgExchangeRate");
            DropTable("dbo.PriceProjectsHistory");
            DropTable("dbo.PriceProjectFieldHistory");
            DropTable("dbo.PriceProjectComRecord");
            DropTable("dbo.PriceProjectCom");
            DropTable("dbo.PriceProjects");
            DropTable("dbo.PP_AttachRemarks");
            DropTable("dbo.LimsEquipmentActSpareParts");
            DropTable("dbo.LimsEquipmentPlan");
            DropTable("dbo.LimsPlanEquipmentLink");
            DropTable("dbo.LimsEquipmentJournal");
            DropTable("dbo.LimsEquipmentJournalRecord");
            DropTable("dbo.LimsApplicationJournal");
            DropTable("dbo.LimsEquipment");
            DropTable("dbo.LimsEquipmentAct");
            DropTable("dbo.DIC_FileLinkStatus");
            DropTable("dbo.FileLinks");
            DropTable("dbo.OBK_LetterRegistration");
            DropTable("dbo.OBK_LetterFromEdo");
            DropTable("dbo.OBK_LetterPortalEdo");
            DropTable("dbo.OBK_DirectionSignData");
            DropTable("dbo.OBK_DirectionToPayments");
            DropTable("dbo.EMP_Ref_Bank");
            DropTable("dbo.OBK_DeclarantContact");
            DropTable("dbo.EMP_Ref_Status");
            DropTable("dbo.EMP_Ref_ContractType");
            DropTable("dbo.EMP_Ref_ContractScope");
            DropTable("dbo.OBK_Ref_PaymentStatus");
            DropTable("dbo.EMP_DirectionToPayments");
            DropTable("dbo.EMP_Ref_DegreeRisk");
            DropTable("dbo.EMP_Ref_ServiceType");
            DropTable("dbo.EMP_Ref_PriceType");
            DropTable("dbo.EMP_Ref_PriceList");
            DropTable("dbo.EMP_CostWorks");
            DropTable("dbo.EMP_Ref_StageStatus");
            DropTable("dbo.EMP_Ref_Stage");
            DropTable("dbo.EMP_ContractStageExecutors");
            DropTable("dbo.EMP_ContractStage");
            DropTable("dbo.EMP_ContractSignData");
            DropTable("dbo.OBK_ContractHistory");
            DropTable("dbo.OBK_Ref_ContractHistoryStatus");
            DropTable("dbo.EMP_ContractHistory");
            DropTable("dbo.EMP_ContractExtHistory");
            DropTable("dbo.EMP_Contract");
            DropTable("dbo.OBK_Declarant");
            DropTable("dbo.OBK_ContractSignedDatas");
            DropTable("dbo.OBK_ContractFactoryComRecord");
            DropTable("dbo.OBK_ContractFactoryCom");
            DropTable("dbo.OBK_ContractFactory");
            DropTable("dbo.OBK_Ref_ContractExtHistoryStatus");
            DropTable("dbo.OBK_ContractExtHistory");
            DropTable("dbo.OBK_ContractComRecord");
            DropTable("dbo.OBK_ContractCom");
            DropTable("dbo.OBK_StageExpDocumentResult");
            DropTable("dbo.OBK_Ref_CertificateType");
            DropTable("dbo.OBK_OP_CommissionRoles");
            DropTable("dbo.OBK_OP_Commission");
            DropTable("dbo.OBK_CertificateOfCompletion");
            DropTable("dbo.OBK_Ref_Status");
            DropTable("dbo.OBK_AssessmentDeclarationHistory");
            DropTable("dbo.OBK_AssessmentDeclarationFieldHistory");
            DropTable("dbo.OBK_AssessmentDeclarationComRecord");
            DropTable("dbo.OBK_AssessmentDeclarationCom");
            DropTable("dbo.OBK_ExpertCouncil");
            DropTable("dbo.OBK_AssessmentDeclaration__OBK_ExpertCouncil");
            DropTable("dbo.OBK_TaskStatus");
            DropTable("dbo.OBK_TaskExecutor");
            DropTable("dbo.OBK_Ref_LaboratoryRegulation");
            DropTable("dbo.OBK_Ref_LaboratoryMark");
            DropTable("dbo.OBK_ResearchCenterResult");
            DropTable("dbo.OBK_Ref_MaterialCondition");
            DropTable("dbo.OBK_Ref_LaboratoryType");
            DropTable("dbo.EXP_DrugUseMethod");
            DropTable("dbo.sr_use_methods");
            DropTable("dbo.sr_register_use_methods");
            DropTable("dbo.sr_pharmacological_actions");
            DropTable("dbo.sr_register_pharmacological_actions");
            DropTable("dbo.sr_nd_names");
            DropTable("dbo.sr_life_types");
            DropTable("dbo.sr_international_names");
            DropTable("dbo.sr_drug_types");
            DropTable("dbo.sr_dosage_forms");
            DropTable("dbo.sr_atc_codes");
            DropTable("dbo.sr_register_drugs");
            DropTable("dbo.sr_categories");
            DropTable("dbo.sr_substances");
            DropTable("dbo.sr_substance_types");
            DropTable("dbo.sr_nd_types");
            DropTable("dbo.sr_register_substances");
            DropTable("dbo.sr_producer_types");
            DropTable("dbo.sr_register_producers");
            DropTable("dbo.sr_form_types");
            DropTable("dbo.sr_producers");
            DropTable("dbo.sr_register_mt_parts");
            DropTable("dbo.sr_register_mt_kz");
            DropTable("dbo.sr_mt_categories");
            DropTable("dbo.sr_degree_risks");
            DropTable("dbo.sr_degree_risk_details");
            DropTable("dbo.sr_register_mt");
            DropTable("dbo.sr_register_instructions_files");
            DropTable("dbo.sr_instruction_types");
            DropTable("dbo.sr_register_instructions");
            DropTable("dbo.sr_reg_types");
            DropTable("dbo.sr_reg_actions");
            DropTable("dbo.sr_register_boxes_rk_ls");
            DropTable("dbo.EXP_DIC_WrappingType");
            DropTable("dbo.EXP_DrugWrapping");
            DropTable("dbo.sr_boxes");
            DropTable("dbo.sr_register_boxes");
            DropTable("dbo.sr_drug_forms");
            DropTable("dbo.sr_register");
            DropTable("dbo.sr_register_names");
            DropTable("dbo.EXP_DrugOtherCountry");
            DropTable("dbo.EXP_DrugExportTrade");
            DropTable("dbo.sr_countries");
            DropTable("dbo.EXP_DrugSubstanceManufacture");
            DropTable("dbo.EXP_DIC_PlantKind");
            DropTable("dbo.EXP_DIC_Origin");
            DropTable("dbo.EXP_DIC_NormDocFarm");
            DropTable("dbo.EXP_DrugSubstance");
            DropTable("dbo.sr_measures");
            DropTable("dbo.OBK_ZBKCopySignData");
            DropTable("dbo.OBK_ZBKCopyBlank");
            DropTable("dbo.OBK_ZBKCopy");
            DropTable("dbo.OBK_Ref_Reason");
            DropTable("dbo.OBK_StageExpDocument");
            DropTable("dbo.OBK_RS_ProductsComRecord");
            DropTable("dbo.OBK_RS_ProductsCom");
            DropTable("dbo.OBK_MtPart");
            DropTable("dbo.OBK_Ref_Type");
            DropTable("dbo.OBK_Ref_ServiceType");
            DropTable("dbo.OBK_Ref_DegreeRisk");
            DropTable("dbo.OBK_Ref_PriceList");
            DropTable("dbo.OBK_ContractPriceComRecord");
            DropTable("dbo.OBK_ContractPriceCom");
            DropTable("dbo.OBK_ContractPrice");
            DropTable("dbo.OBK_RS_Products");
            DropTable("dbo.OBK_Products_SeriesComRecord");
            DropTable("dbo.OBK_Products_SeriesCom");
            DropTable("dbo.OBK_Procunts_Series");
            DropTable("dbo.OBK_TaskMaterial");
            DropTable("dbo.OBK_AssessmentStageOP");
            DropTable("dbo.OBK_ZBKCopyStageSignData");
            DropTable("dbo.OBK_ZBKCopyStageExecutors");
            DropTable("dbo.OBK_ZBKCopyStage");
            DropTable("dbo.OBK_ContractStageExecutors");
            DropTable("dbo.OBK_ContractStage");
            DropTable("dbo.OBK_Ref_Stage");
            DropTable("dbo.OBK_AssessmentStageSignData");
            DropTable("dbo.OBK_AssessmentStageExecutors");
            DropTable("dbo.OBK_AssessmentStage");
            DropTable("dbo.OBK_Ref_StageStatus");
            DropTable("dbo.OBK_Tasks");
            DropTable("dbo.OBK_Dictionaries");
            DropTable("dbo.OBK_ActReception");
            DropTable("dbo.OBK_AssessmentDeclaration");
            DropTable("dbo.OBK_Contract");
            DropTable("dbo.Units");
            DropTable("dbo.DIC_Storages");
            DropTable("dbo.EXP_Materials");
            DropTable("dbo.EXP_MaterialDirections");
            DropTable("dbo.EXP_ExpertisePrimaryFinalDoc");
            DropTable("dbo.EXP_ExpertiseStageDosageResult");
            DropTable("dbo.EXP_ExpertiseSafetyreportFinalDoc");
            DropTable("dbo.EXP_ExpertisePharmacologicalFinalDoc");
            DropTable("dbo.EXP_ExpertisePharmaceuticalFinalDoc");
            DropTable("dbo.EXP_DIC_AnalyseIndicator");
            DropTable("dbo.EXP_DrugAnaliseIndicator");
            DropTable("dbo.EXP_ExpertiseStageDosage");
            DropTable("dbo.EXP_DIC_PrimaryFinalyDocResult");
            DropTable("dbo.EXP_DrugPrimaryFinalDocument");
            DropTable("dbo.EXP_AgreementProcSettingsTasks");
            DropTable("dbo.EXP_AgreementProcSettingsActivities");
            DropTable("dbo.EXP_AgreementProcSettings");
            DropTable("dbo.EXP_Tasks");
            DropTable("dbo.EXP_Activities");
            DropTable("dbo.Dictionaries");
            DropTable("dbo.EXP_DrugCorespondence");
            DropTable("dbo.EXP_DrugCorespondenceRemark");
            DropTable("dbo.EXP_DrugAppDosageResult");
            DropTable("dbo.EXP_DIC_RemarkType");
            DropTable("dbo.EXP_DrugAppDosageRemark");
            DropTable("dbo.EXP_DIC_SaleType");
            DropTable("dbo.EXP_DrugDosage");
            DropTable("dbo.CommissionDrugDosageNeedCommission");
            DropTable("dbo.EXP_ExpertiseStage");
            DropTable("dbo.EXP_DIC_StageResult");
            DropTable("dbo.EXP_DIC_Stage");
            DropTable("dbo.EXP_CertificateOfCompletion");
            DropTable("dbo.EXP_DrugDeclaration");
            DropTable("dbo.EXP_PriceList");
            DropTable("dbo.EXP_DirectionToPays_PriceList");
            DropTable("dbo.EXP_DirectionToPays");
            DropTable("dbo.Organizations");
            DropTable("dbo.Contracts");
            DropTable("dbo.ContractComments");
            DropTable("dbo.Employees");
            DropTable("dbo.CommissionUnitTypes");
            DropTable("dbo.CommissionUnits");
            DropTable("dbo.CommissionTypes");
            DropTable("dbo.CommissionQuestionTypes");
            DropTable("dbo.CommissionQuestions");
            DropTable("dbo.CommissionKinds");
            DropTable("dbo.Commissions");
            DropTable("dbo.CommissionDrugDosage");
            DropTable("dbo.CommissionConclusionTypes");
            DropTable("dbo.Comments");
            DropTable("dbo.CitizenDocument");
            DropTable("dbo.Changes");
            DropTable("dbo.CategoriesView");
            DropTable("dbo.AvgExchangeRatesView");
            DropTable("dbo.aspnet_WebEvent_Events");
            DropTable("dbo.aspnet_SchemaVersions");
            DropTable("dbo.aspnet_Roles");
            DropTable("dbo.aspnet_Profile");
            DropTable("dbo.aspnet_PersonalizationAllUsers");
            DropTable("dbo.aspnet_Paths");
            DropTable("dbo.aspnet_PersonalizationPerUser");
            DropTable("dbo.aspnet_Users");
            DropTable("dbo.aspnet_Membership");
            DropTable("dbo.aspnet_Applications");
            DropTable("dbo.ArchivView");
            DropTable("dbo.Archives");
            DropTable("dbo.AdministrativeDocument");
            DropTable("dbo.ActionLogsView");
            DropTable("dbo.ActionLogs");
            DropTable("dbo.ActionLogPlaces");
            DropTable("dbo.Templates");
            DropTable("dbo.RemarkTypes");
            DropTable("dbo.Reports");
            DropTable("dbo.AccessTasks");
            DropTable("dbo.Tasks");
            DropTable("dbo.Activities");
            DropTable("dbo.Documents");
            DropTable("dbo.AccessDocuments");
        }
    }
}
