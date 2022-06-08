using GTANetworkAPI;

namespace ServerSide
{
    public class PrivateVehicle : Script
    {
        private static string _privateVehicleKey = nameof(_privateVehicleKey);
        
        [Command]
        private void MyVeh(Player player)
        {
            var veh =NAPI.Vehicle.CreateVehicle(VehicleHash.Adder, player.Position, player.Rotation, 131, 131);
            veh.SetData(_privateVehicleKey, player.SocialClubId);
        }
        
        [ServerEvent(Event.PlayerEnterVehicle)]
        private void OnPlayerEnterVehicle(Player player, Vehicle vehicle, sbyte seatid)
        {
            if (vehicle.HasData(_privateVehicleKey) &&
                vehicle.GetData<ulong>(_privateVehicleKey) != player.SocialClubId)
            {
                player.SendChatMessage("Its not your car!");
                player.WarpOutOfVehicle();
            }
        }
    }
}