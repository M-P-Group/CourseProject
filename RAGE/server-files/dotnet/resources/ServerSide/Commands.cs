using GTANetworkAPI;
using ServerSide.CommandAttributes;

namespace ServerSide
{
    public class Commands : Script
    {
        [Command]
        public void CreateCar(Player player)
        {
            NAPI.Vehicle.CreateVehicle(VehicleHash.Adder, player.Position, player.Heading, 131, 131);
        }

        [Command("hp")]
        public void SetHealth(Player player, int count)
        {
            player.Health = count;
        }

        [Command("teleport", Alias = "tp")]
        public void TeleportPlayer(Player player, float x, float y, float z)
        {
            player.Position = new Vector3(x, y, z);
        }

        [Command("armor")]
        public void SetPlayerArmor(Player player, int armor = 100)
        {
            player.Armor = armor;
        }

        [Command("me", GreedyArg = true)]
        public void TypeMe(Player player, string actions)
        {
            player.SendChatMessage($"{player.Name} did that: " + actions);
        }

        [RequiresHealth(75)]
        [Command("healme")]
        public void HealPlayer(Player player)
        {
            player.Health = 100;
            player.SendChatMessage("Healed!");
        }
    }
}