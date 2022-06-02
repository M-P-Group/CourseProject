using System;
using GTANetworkAPI;

namespace ServerSide
{
    public class Events : Script
    {
        [ServerEvent(Event.PlayerSpawn)]
        private void OnPlayerSpawn(Player player)
        {
            player.Armor = 100;
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        private void OnPlayerEnterVehicle(Player player, Vehicle vehicle, SByte seatID)
        {
            if (vehicle == null) return;
            vehicle.PrimaryColor = 12;
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        private void OnPlayerExitVehicle(Player player, Vehicle vehicle)
        {
            if (vehicle == null) return;
            vehicle.PrimaryColor = 131;
        }

        [RemoteEvent("CLIENT:SERVER:RepairCar")]
        private void RepairCar(Player player)
        {
            if (player.Vehicle == null)
            {
                player.SendChatMessage("You're not in a vehicle! From server!");
                return;
            }

            player.Vehicle.Repair();
        }
    }
}