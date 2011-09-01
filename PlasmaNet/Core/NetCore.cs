using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    /// <summary>
    /// Plasma Network Utilities and Helpers
    /// </summary>
    public static class plNetCore {

        internal static readonly byte kNetCliConnect = 0;
        internal static readonly byte kNetCliEncrypt = 1;
        internal static readonly byte kNetCliError   = 2;

        /// <summary>
        /// The most recent protocol version.
        /// </summary>
        public const uint Version = 50;

        public static string GetErrorString(ENetError result) {
            switch (result) {
                case ENetError.kNetErrAccountAlreadyExists:
                    return "Account Already Exists";
                case ENetError.kNetErrAccountBanned:
                    return "Account Banned";
                case ENetError.kNetErrAccountNotActivated:
                    return "Account Not Activated";
                case ENetError.kNetErrAccountNotFound:
                    return "Account Not Found";
                case ENetError.kNetErrActivationCodeNotFound:
                    return "Activation Code Not Found";
                case ENetError.kNetErrAgeNotFound:
                    return "Age not Found";
                case ENetError.kNetErrAuthenticationFailed:
                    return "Authentication Failed";
                case ENetError.kNetErrAuthTokenTooOld:
                    return "Authentication Token Too Old";
                case ENetError.kNetErrBadServerData:
                    return "Bad Server Data";
                case ENetError.kNetErrCircularReference:
                    return "Circular Reference";
                case ENetError.kNetErrConnectFailed:
                    return "Connection Failed";
                case ENetError.kNetErrDisconnected:
                    return "Disconnected";
                case ENetError.kNetErrFileNotFound:
                    return "File Not Found";
                case ENetError.kNetErrGameTapConnectionFailed:
                    return "Could Not Connect To GameTap";
                case ENetError.kNetErrGTMissingParameter:
                    return "Missing GameTap Paramater";
                case ENetError.kNetErrGTServerError:
                    return "GameTap Error";
                case ENetError.kNetErrGTTooManyAuthOptions:
                    return "Too Many GameTap Authentication Options";
                case ENetError.kNetErrInternalError:
                    return "Internal Error";
                case ENetError.kNetErrInvalidParameter:
                    return "Invalid Parameter";
                case ENetError.kNetErrInviteNoMatchingPlayer:
                    return "No Matching Player for Invitation";
                case ENetError.kNetErrInviteTooManyHoods:
                    return "Too Many Neighborhoods for Invitation";
                case ENetError.kNetErrKeyAlreadyUsed:
                    return "Key Already Used";
                case ENetError.kNetErrKeyNotFound:
                    return "Key Not Found";
                case ENetError.kNetErrKickedByCCR:
                    return "Kicked Off";
                case ENetError.kNetErrLoggedInElsewhere:
                    return "Logged in Elsewhere";
                case ENetError.kNetErrLoginDenied:
                    return "Login Denied";
                case ENetError.kNetErrMaxPlayersOnAcct:
                    return "Maxmium Number of Players on Account";
                case ENetError.kNetErrMustUseGameTapClient:
                    return "Must use GameTap Client";
                case ENetError.kNetErrNameLookupFailed:
                    return "Name Lookup Failed";
                case ENetError.kNetErrNeedToPay:
                    return "You Need to Pay";
                case ENetError.kNetErrNotSupported:
                    return "Not Supported";
                case ENetError.kNetErrOldBuildId:
                    return "Old Build";
                case ENetError.kNetErrPlayerAlreadyExists:
                    return "Player Already Exists";
                case ENetError.kNetErrPlayerNameInvalid:
                    return "Player Name Incorrect";
                case ENetError.kNetErrPlayerNotFound:
                    return "Player Not Found";
                case ENetError.kNetErrRemoteShutdown:
                    return "Remote Shutdown";
                case ENetError.kNetErrScoreAlreadyExists:
                    return "Score Already Exists";
                case ENetError.kNetErrScoreNoDataFound:
                    return "No Score Data Found";
                case ENetError.kNetErrScoreNotEnoughPoints:
                    return "Not Enough Points";
                case ENetError.kNetErrScoreWrongType:
                    return "Wrong Score Type";
                case ENetError.kNetErrServerBusy:
                    return "Server Busy";
                case ENetError.kNetErrServiceForbidden:
                    return "Service Forbidden";
                case ENetError.kNetErrStateObjectNotFound:
                    return "State Object Not Found";
                case ENetError.kNetErrTimeout:
                    return "Timeout";
                case ENetError.kNetErrTimeoutOdbc:
                    return "ODBC Timeout";
                case ENetError.kNetErrTooManyFailedLogins:
                    return "Too Many Failed Logins";
                case ENetError.kNetErrVaultNodeAccessViolation:
                    return "Vault Node Access Violation";
                case ENetError.kNetErrVaultNodeNotFound:
                    return "Vault Node Not Found";
                case ENetError.kNetPending:
                    return "Pending...";
                case ENetError.kNetSuccess:
                    return "Success!";
                default:
                    throw new ArgumentException("Unknown Result Code: " + result.ToString());
            }
        }
    }

    public class plNetException : hsException {
        public plNetException() { }
        public plNetException(string message) : base(message) { }
        public plNetException(string message, Exception inner) : base(message, inner) { }
    }
}
