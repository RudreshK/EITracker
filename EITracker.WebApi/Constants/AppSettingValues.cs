// -----------------------------------------------------------------------------
// <copyright file="AppSettingValues.cs" company="ecoInsight, Inc.">
//  Copyright © ecoInsight, Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

namespace EITracker.Constants
{
    /// <summary>
    /// Class for constants used for AppSettings.
    /// </summary>
    public class AppSettingValues
    {
        /// <summary>
        /// Name of default token provider.
        /// </summary>
        internal const string DefaultTokenProvider = "Default";

        /// <summary>
        /// Name of the refresh token provider service.
        /// </summary>
        internal const string RefreshTokenProvider = "RefreshToken";

        /// <summary>
        /// Name of two factor authentication token.
        /// </summary>
        internal const string TwoFactorAuthenticationToken = "TwoFactor";

        /// <summary>
        /// Name of refresh token.
        /// </summary>
        internal const string RefreshToken = "RefreshToken";

        /// <summary>
        /// Name of basic authorization header's prefix.
        /// </summary>
        internal const string BasicAuthorization = "basic";

        /// <summary>
        /// Name of bearer authorization header's prefix.
        /// </summary>
        internal const string BearerAuthorization = "bearer";

        /// <summary>
        /// Represents Authorization request header.
        /// </summary>
        internal const string RequestHeaderAuthorization = "Authorization";

        /// <summary>
        /// Represents company Id claim.
        /// </summary>
        internal const string ClaimCompanyId = "cId";
        internal const string ClaimServiceId = "sId";

        /// <summary>
        /// Represents application user Id claim.
        /// </summary>
        internal const string ClaimApplicationUserId = "appUserId";

        /// <summary>
        /// Represents application user first name claim.
        /// </summary>
        internal const string ClaimApplicationUserFirstName = "fname";

        /// <summary>
        /// Represents application user last name claim.
        /// </summary>
        internal const string ClaimApplicationUserLastName = "lname";

        /// <summary>
        /// Represents application user email claim.
        /// </summary>
        internal const string ClaimApplicationUserEmail = "email";

        /// <summary>
        /// Represents purpose of claim.
        /// </summary>
        internal const string ClaimTokenPurpose = "purpose";

        /// <summary>
        /// Represents purpose of claim.
        /// </summary>
        internal const string BlacklistedToken = "blacklisted";
        
        /// <summary>
        /// Represents purpose of Password reset.
        /// </summary>
        internal const string PasswordResetToken = "passwordreset";

        /// <summary>
        /// Represents purpose of claim.
        /// </summary>
        internal const string HttpContextItemRefreshedToken = "refreshedtoken";

        /// <summary>
        /// Represents HTTP context item named token.
        /// </summary>
        internal const string HttpContextItemUserToken = "usertoken";
        internal const string HttpContextItemClientToken = "clienttoken";

        /// <summary>
        /// Represents HTTP context item named token.
        /// </summary>
        internal const string HttpContextItemUser = "user";

        /// <summary>
        /// Represents Lumigent Api token name
        /// </summary>
        internal const string LumigentApiTokenName = "LumigentToken";

        /// <summary>
        /// Represents application user Id claim.
        /// </summary>
        internal const string QueueName = "productupdatequeue";

        /// <summary>
        /// Represents PO JSON queue
        /// </summary>
        internal const string QueuePOJson = "uploadpodataqueue";

        /// <summary>
        /// Represents missing role based authorization error.
        /// </summary>
        internal const string InsufficientAccessRights = "insufficientAccessRights";

        internal const string RoleMissingResponse = "roleMissing";
        internal const string InvalidClientTokenResponse = "invalidClientToken";

        public const string DefaultConnection = "DefaultConnection";

        public const string JwtSecret = "JwtSecret";

        /// <summary>
        /// SendSupplierPackingListEmailQueue
        /// </summary>
        internal const string SendSupplierPackingListEmailQueue = "sendsupplierpackinglistemailqueue";
    }
}
