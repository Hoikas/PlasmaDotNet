using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum EBuildType {
        kDev = 10,
        kQA = 20,
        kTest = 30,
        kBeta = 40,
        kLive = 50,
    }

    public enum ENetError {
        // codes <= 0 are not errors
        kNetPending = -1,
        kNetSuccess = 0,

        // codes > 0 are errors
        kNetErrInternalError = 1,
        kNetErrTimeout = 2,
        kNetErrBadServerData = 3,
        kNetErrAgeNotFound = 4,
        kNetErrConnectFailed = 5,
        kNetErrDisconnected = 6,
        kNetErrFileNotFound = 7,
        kNetErrOldBuildId = 8,
        kNetErrRemoteShutdown = 9,
        kNetErrTimeoutOdbc = 10,
        kNetErrAccountAlreadyExists = 11,
        kNetErrPlayerAlreadyExists = 12,
        kNetErrAccountNotFound = 13,
        kNetErrPlayerNotFound = 14,
        kNetErrInvalidParameter = 15,
        kNetErrNameLookupFailed = 16,
        kNetErrLoggedInElsewhere = 17,
        kNetErrVaultNodeNotFound = 18,
        kNetErrMaxPlayersOnAcct = 19,
        kNetErrAuthenticationFailed = 20,
        kNetErrStateObjectNotFound = 21,
        kNetErrLoginDenied = 22,
        kNetErrCircularReference = 23,
        kNetErrAccountNotActivated = 24,
        kNetErrKeyAlreadyUsed = 25,
        kNetErrKeyNotFound = 26,
        kNetErrActivationCodeNotFound = 27,
        kNetErrPlayerNameInvalid = 28,
        kNetErrNotSupported = 29,
        kNetErrServiceForbidden = 30,
        kNetErrAuthTokenTooOld = 31,
        kNetErrMustUseGameTapClient = 32,
        kNetErrTooManyFailedLogins = 33,
        kNetErrGameTapConnectionFailed = 34,
        kNetErrGTTooManyAuthOptions = 35,
        kNetErrGTMissingParameter = 36,
        kNetErrGTServerError = 37,
        kNetErrAccountBanned = 38,
        kNetErrKickedByCCR = 39,
        kNetErrScoreWrongType = 40,
        kNetErrScoreNotEnoughPoints = 41,
        kNetErrScoreAlreadyExists = 42,
        kNetErrScoreNoDataFound = 43,
        kNetErrInviteNoMatchingPlayer = 44,
        kNetErrInviteTooManyHoods = 45,
        kNetErrNeedToPay = 46,
        kNetErrServerBusy = 47,
        kNetErrVaultNodeAccessViolation = 48,

        // Add new values below here
    }

    public enum ENetProtocol {
        kConnTypeNil, kConnTypeDebug,
        kConnTypeCliToAuth = 10, kConnTypeCliToGame,
        kConnTypeCliToFile = 16, 
        kConnTypeCliToGate = 22,

        // The following are Plasma .NET Srv2Srv protocols
        kConnTypeSrvToVault = 50, kConnTypeSrvToLookup,
    }
}
