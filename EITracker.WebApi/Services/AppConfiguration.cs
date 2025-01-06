// -----------------------------------------------------------------------------
// <copyright file="AppConfiguration.cs" company="ecoInsight, Inc.">
//  Copyright © ecoInsight, Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

using EITracker.Constants;

namespace EITracker.Services
{
    public class AppConfiguration 
    {
        private const string PasswordResetTokenTimeOutMinutesName = "PasswordResetTokenTimeOutMinutes";
        private const string SASTokenExpiryMinutesName = "SASTokenExpiryMinutes";
        private const string SASTokenExpiryMinutesSubmittalName = "SASTokenExpiryMinutesSubmittal";
        private const string SessionTimeOutMinutesName = "SessionTimeOutMinutes";
        private const string TwoFactorPasscodeMinutesName = "TwoFactorPasscodeMinutes";
        private const string TwoFactorDisableHoursName = "TwoFactorDisableHours";
        private const string RefreshTokenLifetimeInDaysName = "RefreshTokenLifetimeInDays";
        private const string AzureStorageTableName = "CQConfigurations";
        private const string AzureTableUIBaseUrl = "UIBaseUrl";
        private const string AzureTableColSummaryViewJSON = "SummaryViewJSON";
        private const string JwtSecretName = "JwtSecret";
        private const string SendGridKeyName = "SendGridKey";
        private const string RedisConnectionStringName = "RedisConnectionString";
        private const string StorageConnectionStringName = "StorageConnectionString";
        private const string CognitiveServicesApiKeyName = "CognitiveServicesApiKey";
        private const string CognitiveServicesEndPointName = "CognitiveServicesEndPoint";
        private const string FileStorageName = "FileStorage";
        private const string FileStoragePublicBaseUrlName = "FileStoragePublicBaseUrl";
        private const string SenderMailAddressName = "SenderMailAddress";
        private const string SenderMailDisplayName = "SenderMailDisplay";
        private const string AllowedVersionsName = "AllowedVersions";
        private const string AllowedIpadVersionsName = "IpadConstruction";
        private const string AllowedAndroidWarehouseVersionsName = "AndroidWarehouse";
        private const string AllowedIpadVersionsWarehouseName = "IpadWarehouse";
        private const string LogLevelName = "LogLevel";
        private const string WNMulesoftAuthUrlName = "WNMulesoftAuthUrl";
        private const string WNMulesoftClientIdName = "WNMulesoftClientId";
        private const string WNMulesoftClientSecretName = "WNMulesoftClientSecret";
        private const string WNMulesoftScopeName = "WNMulesoftScope";
        private const string WNJobSeqBaseUrlName = "WNJobSeqBaseUrl";
        private const string WNRPMXmlBaseUrlName = "WNRPMXmlBaseUrl";
        private const string WNBranchTypeName = "WNBranchType";

        private bool valuesInitializedWithAzureTable;
        private string uiBaseUrl;
        private string summaryViewJSON;
       
        private const string SupportErrorToEmailsName = "SupportErrorToEmails";
        private const string SupportErrorCCEmailsName = "SupportErrorCCEmails";

        private readonly IConfiguration configuration;
        public AppConfiguration(IConfiguration configuration) =>
            this.configuration = configuration;

        internal double PasswordResetTokenTimeOutMinutes => double.Parse(this.configuration.GetSection(PasswordResetTokenTimeOutMinutesName).Value);
        internal double SASTokenExpiryMinutes => double.Parse(this.configuration.GetSection(SASTokenExpiryMinutesName).Value);
        internal double SASTokenExpiryMinutesSubmittal => double.Parse(this.configuration.GetSection(SASTokenExpiryMinutesSubmittalName).Value);
        internal double SessionTimeOutMinutes => double.Parse(this.configuration.GetSection(SessionTimeOutMinutesName).Value);
        internal double TwoFactorPasscodeMinutes => double.Parse(this.configuration.GetSection(TwoFactorPasscodeMinutesName).Value);
        internal double TwoFactorDisableHours => double.Parse(this.configuration.GetSection(TwoFactorDisableHoursName).Value);
        internal double RefreshTokenLifetimeInDays => double.Parse(this.configuration.GetSection(RefreshTokenLifetimeInDaysName).Value);
        internal string JwtSecret => this.configuration.GetSection(JwtSecretName).Value;
        internal string SendGridKey => this.configuration.GetSection(SendGridKeyName).Value;
        public string RedisConnectionString => this.configuration.GetConnectionString(RedisConnectionStringName);
        internal string CognitiveServicesEndPoint => this.configuration.GetSection(CognitiveServicesEndPointName).Value;
        internal string CognitiveServicesApiKey => this.configuration.GetSection(CognitiveServicesApiKeyName).Value;
        internal string StorageConnectionString => this.configuration.GetConnectionString(StorageConnectionStringName);
        internal string FileStorage => this.configuration.GetSection(FileStorageName).Value;
        internal string FileStoragePublicBaseUrl => this.configuration.GetSection(FileStoragePublicBaseUrlName).Value;
        internal string SenderMailAddress => this.configuration.GetSection(SenderMailAddressName).Value;
        internal string SenderMailDisplay => this.configuration.GetSection(SenderMailDisplayName).Value;
        internal string IpadConstructionVersions => this.configuration.GetSection(AllowedVersionsName)[AllowedIpadVersionsName];
        internal string AndroidWarehouseVersions => this.configuration.GetSection(AllowedVersionsName)[AllowedAndroidWarehouseVersionsName];
        internal string IpadWarehouseVersions => this.configuration.GetSection(AllowedVersionsName)[AllowedIpadVersionsWarehouseName];


        public string WNMulesoftAuthUrl => this.configuration.GetSection(WNMulesoftAuthUrlName).Value;

        public string WNMulesoftClientId => this.configuration.GetSection(WNMulesoftClientIdName).Value;

        public string WNMulesoftClientSecret => this.configuration.GetSection(WNMulesoftClientSecretName).Value;

        public string WNMulesoftScope => this.configuration.GetSection(WNMulesoftScopeName).Value;

        public string WNJobSeqBaseUrl => this.configuration.GetSection(WNJobSeqBaseUrlName).Value;

        public string WNRPMXmlBaseUrl => this.configuration.GetSection(WNRPMXmlBaseUrlName).Value;
        public string WNBranchType => this.configuration.GetSection(WNBranchTypeName).Value;

        public string ConstructionDbConnectionString => this.configuration.GetConnectionString(AppSettingValues.DefaultConnection);

        internal string SupportErrorToEmails => this.configuration.GetSection(SupportErrorToEmailsName).Value;
        internal string SupportErrorCCEmails => this.configuration.GetSection(SupportErrorCCEmailsName).Value;

    }
}
