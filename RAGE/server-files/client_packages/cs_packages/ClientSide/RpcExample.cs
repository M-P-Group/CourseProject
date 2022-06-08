using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RAGE;
using RAGE.Ui;

namespace ClientSide
{
    public class RpcExample : Events.Script
    {
        private const string ShowServerTimeKey = "RPC::CLIENT:SERVER:ShowServerTime";
        private const string GetFPSKey = "RPC::SERVER:CLIENT:GetFPS";
        
        public RpcExample()
        {
            Input.Bind(VirtualKeys.F6, true, ShowServerTime);
            Events.AddProc(GetFPSKey, GetFPS, async: true);
        }

        private async Task<float> GetFPS(object[] args)
        {
            return 1000 / (RAGE.Game.Misc.GetFrameTime() * 1000);
        }

        private async void ShowServerTime()
        {
            var response = (string) await Events.CallRemoteProc(ShowServerTimeKey);
            var time = JsonConvert.DeserializeObject<DateTime>(response);
            Chat.Output($"Current server time is {time.ToLocalTime()}");
        }
    }
}