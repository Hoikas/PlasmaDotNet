using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public partial class pnVaultSession {

        private void ICreatePlayer() {
            pnCli2Vault_PlayerCreateRequest req = new pnCli2Vault_PlayerCreateRequest();
            req.Read(fStream);

            // Go ahead and init the response
            pnVault2Cli_PlayerCreateReply reply = new pnVault2Cli_PlayerCreateReply();
            reply.fTransID = req.fTransID;
            reply.fResult = ENetError.kNetSuccess;

            // Search for a matching PlayerInfo
            pnVaultPlayerInfoNode pi = new pnVaultPlayerInfoNode();
            pi.PlayerName = req.fPlayerName;
            try {
                if (IFindNode(pi.BaseNode).Length != 0)
                    reply.fResult = ENetError.kNetErrPlayerAlreadyExists;
            } catch (pnDbException e) {
                fLog.Error(e, "Failed to create player");
                reply.fResult = ENetError.kNetErrInternalError;
            }

            if (reply.fResult != ENetError.kNetSuccess) {
                pnVaultPlayerNode player = new pnVaultPlayerNode();
                player.AccountUUID = req.fAcctGuid;
                player.AvatarShape = req.fShape;
                player.CreatorUUID = req.fAcctGuid;
                player.PlayerName = req.fPlayerName;

                // TODO: ...
            }

            // Send the response :)
            reply.Send(fStream);
        }
    }
}
