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


        [Command("marker")]
        public void Marker(Player player, uint markerType)
        {
            NAPI.Marker.CreateMarker(markerType, player.Position, new Vector3(), new Vector3(), 1f, new Color(255, 0, 0, 100), false, player.Dimension);
        }

        private Checkpoint _prevCheckpoint;
        [Command("checkpoint")]
        public void Checkpoint(Player player, uint checkpointType)
        {
            var direction = _prevCheckpoint?.Position ?? player.Position;

            _prevCheckpoint = NAPI.Checkpoint.CreateCheckpoint(checkpointType, player.Position + new Vector3(0f, 0f, -1f), direction, 1f, new Color(255, 0, 0, 100), player.Dimension);
        }

        [Command("blip")]
        public void Blip(Player player, uint sprite, byte color, string name, bool shortRange)
        {
            NAPI.Blip.CreateBlip(sprite, player.Position, 1f, color, name, 255, 0f, shortRange, 0, player.Dimension);
        }


        [Command("colshape")]
        public void Colshape(Player player, float scale)
        {
            var position = player.Position + new Vector3(0f, 0f, -1f);

            var colShape = NAPI.ColShape.CreateCylinderColShape(position, scale, 2f, player.Dimension);
            colShape.SetData(nameof(GTANetworkAPI.Marker), NAPI.Marker.CreateMarker(1, position, new Vector3(), new Vector3(), scale * 2, new Color(255, 0, 0, 100), false, player.Dimension));

            colShape.OnEntityEnterColShape += OnEntityEnterColShape;
            colShape.OnEntityExitColShape += OnEntityExitColShape;
        }

        private void OnEntityEnterColShape(ColShape colShape, Player player)
        {
            player.Armor = 100;
        }
        private void OnEntityExitColShape(ColShape colShape, Player player)
        {
            player.Armor = 0;
        }
        [Command("randomizeme")]
        private void RandomizeMe(Player player)
        {
            player.TriggerEvent("SERVER:CLIENT:RandomizePlayer");
        }
    }
}