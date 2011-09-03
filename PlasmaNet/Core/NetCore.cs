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

    public static partial class pnHelpers {
        public static byte[] HashLogin(string user, string pass) {
            if (user.Contains('@')) {
                // Oh boy... Emails are SHA-0 with an strcpy bug.
                // Yes, eap is really that stupid.
                byte[] theAcct = Encoding.Unicode.GetBytes(user.ToLower());
                theAcct[theAcct.Length - 1] = 0;
                theAcct[theAcct.Length - 2] = 0;

                byte[] thePass = Encoding.Unicode.GetBytes(pass);
                thePass[thePass.Length - 1] = 0;
                thePass[thePass.Length - 2] = 0;

                // Put them both in a single buffer, then SHA-0
                byte[] buf = new byte[theAcct.Length + thePass.Length];
                Buffer.BlockCopy(thePass, 0, buf, 0, thePass.Length);
                Buffer.BlockCopy(theAcct, 0, buf, thePass.Length, theAcct.Length);
                return OpenSSL.Hash.SHA0(buf);
            } else
                // User names are just a simple SHA-1
                return OpenSSL.Hash.SHA1(Encoding.UTF8.GetBytes(pass));
        }

        public static byte[] HashLogin(byte[] userPass, uint cliChg, uint srvChg) {
            // Why eap didn't remove this after they introduced the encryption, I'll never know.
            byte[] buf = new byte[8 + userPass.Length];
            Buffer.BlockCopy(BitConverter.GetBytes(cliChg), 0, buf, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(srvChg), 0, buf, 4, 4);
            Buffer.BlockCopy(userPass, 0, buf, 8, userPass.Length);
            buf = OpenSSL.Hash.SHA0(buf);

            // Now fix eap's stupidity
            // In other words, reverse the uints
            for (int i = 0; i < buf.Length; i += 4)
                Array.Reverse(buf, i, 4);
            return buf;
        }

        /// <summary>
        /// Generates the final password hash to be sent to the server.
        /// </summary>
        /// <param name="user">The username</param>
        /// <param name="pass">The user's password</param>
        /// <param name="cliChg">The challenge generated by the client and sent to the AuthSrv.</param>
        /// <param name="srvChg">The challenge sent to the client on a successful registration.</param>
        /// <returns>The final hash</returns>
        public static byte[] HashLogin(string user, string pass, uint cliChg, uint srvChg) {
            byte[] buf = HashLogin(user, pass);
            if (user.Contains('@'))
                return HashLogin(buf, cliChg, srvChg);
            else
                return buf;
        }
    }
}
