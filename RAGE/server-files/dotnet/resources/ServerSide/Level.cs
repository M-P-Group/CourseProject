using System;
using GTANetworkAPI;

namespace ServerSide
{
    public class Level : Script
    {
        private static string _levelKey = nameof(_levelKey);
        
        [ServerEvent(Event.PlayerConnected)]
        private void OnPlayerConnected(Player player)
        {
            player.SetSharedData(_levelKey, new Random().Next(0,100));
        }
    }
}