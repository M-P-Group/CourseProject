using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GTANetworkAPI;

namespace ServerSide
{
    public class RpcExample : Script
    {
        private const string ShowServerTimeKey = "RPC::CLIENT:SERVER:ShowServerTime";
        private const string GetFPSKey = "RPC::SERVER:CLIENT:GetFPS";

        [RemoteProc(ShowServerTimeKey, true)]
        private async Task<object> ShowServerTime(Player player)
        {
            return NAPI.Util.ToJson(DateTime.Now);
        }

        [Command("getplayerfps")]
        private void GetPlayerFps(Player player, int playerId)
        {
            var target = NAPI.Pools.GetAllPlayers().FirstOrDefault(x => x.Value == playerId);
            if (target == null)
            {
                player.SendChatMessage("Player not found");
                return;
            }
            NAPI.Task.Run(async () =>
            {
                var fps = (float)await target.TriggerProcedure(GetFPSKey);

                await NAPI.Task.WaitForMainThread();
                player.SendChatMessage("Player FPS: " + fps);
            });
        }
    }
}