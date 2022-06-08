using System.Runtime.CompilerServices;
using GTANetworkAPI;

namespace ServerSide
{
    public class Wallet : Script
    {
        private static string _ownMoneyKey = nameof(_ownMoneyKey);
        
        [Command("getmoney")]
        private void GetMoney(Player player, int amount)
        {
            if (player.GetOwnSharedData<int?>(_ownMoneyKey) == null) player.SetOwnSharedData(_ownMoneyKey, 0);
            player.SetOwnSharedData(_ownMoneyKey, player.GetOwnSharedData<int>(_ownMoneyKey) + amount);
        }

        [Command("spendmoney")]
        private void SpendMoney(Player player, int amount)
        {
            if (player.GetOwnSharedData<int?>(_ownMoneyKey) == null) player.SetOwnSharedData(_ownMoneyKey, 0);
            if (player.GetOwnSharedData<int>(_ownMoneyKey) < amount)
            {
                player.SendChatMessage("You don't have enough money!");
                return;
            }
            player.SetOwnSharedData(_ownMoneyKey, player.GetOwnSharedData<int>(_ownMoneyKey) - amount);
        }
    }
}