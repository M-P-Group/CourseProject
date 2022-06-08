using RAGE;
using System;
using RAGE.Elements;
using RAGE.Ui;

namespace ClientSide
{
    public class Client : Events.Script
    {
        private static string _ownMoneyKey = nameof(_ownMoneyKey);
        private static string _levelKey = nameof(_levelKey);
        public Client()
        {
            RAGE.Chat.Output("Hello World! Client");
            RAGE.Input.Bind(VirtualKeys.F4, true, LevelNotifier);
            RAGE.Events.AddDataHandler(_ownMoneyKey, MoneyHandler);
        }

        private void MoneyHandler(Entity entity, object arg, object oldarg)
        {
            if (entity is RAGE.Elements.Player == false) return;
            var newMoney = (int)arg;
            var oldMoney = (int)oldarg;

            if (newMoney > oldMoney)
            {
                Chat.Output("You earned some money! Amount:" + (newMoney - oldMoney));
            }
            else
            {
                Chat.Output("You spent some money! Amount: " + (oldMoney - newMoney));
            }
        }

        private void LevelNotifier()
        {
            foreach (var player in RAGE.Elements.Entities.Players.All)
            {
                Chat.Output($"{player.Name} -- {player._GetSharedData<int>(_levelKey)}");
            }
        }
    }
}
