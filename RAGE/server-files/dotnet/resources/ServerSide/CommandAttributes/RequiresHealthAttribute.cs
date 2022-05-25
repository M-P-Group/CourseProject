using System.Collections;
using GTANetworkAPI;
using GTANetworkMethods;
using Player = GTANetworkAPI.Player;

namespace ServerSide.CommandAttributes
{
    public class RequiresHealthAttribute : CommandConditionAttribute
    {
        public int Amount { get; set; }

        public RequiresHealthAttribute(int amount = 100)
        {
            Amount = amount;
        }
        
        public override bool Check(Player player, string cmdName, string cmdText)
        {
            if (player.Health > Amount)
            {
                player.SendChatMessage("Youre healthy af");
                return false;
            }

            return true;
        }
    }
}