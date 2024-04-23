// -----------------------------------------------------------------------------
// <copyright file="AppConfiguration.cs" company="ecoInsight, Inc.">
//  Copyright © ecoInsight, Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

namespace EITracker.WebApi.Constants
{
    public class AppConfiguration
    {
        /// <summary>
        /// Represents Authorization request header.
        /// </summary>
        internal const string RequestHeaderAuthorization = "Authorization";

        /// <summary>
        /// Name of bearer authorization header's prefix.
        /// </summary>
        internal const string BearerAuthorization = "bearer";


        private readonly IConfiguration configuration;
        public AppConfiguration(IConfiguration configuration) =>
            this.configuration = configuration;

        public string JwtSecret => configuration.GetValue<string>("JWT:Key");
        public string JwtIssuert => configuration.GetValue<string>("JWT:Issuer");
        public string JwtAudience => configuration.GetValue<string>("JWT:Audience");
        public string AppsettingsBaseUrl => configuration.GetValue<string>("appSettings:baseurl");
        public string AppsettingsBlobUrl =>  configuration.GetValue<string>("appSettings:bloburl");
        public string AppsettingSearchdocUrl => configuration.GetValue<string>("appSettings:searchdocurl");
        public string CloudStorageConnectionString => configuration.GetValue<string>("ConnectionStrings:StorageConnectionString");

    }
}

