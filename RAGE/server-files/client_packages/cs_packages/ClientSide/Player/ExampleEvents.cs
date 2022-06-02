using System;
using RAGE;
using RAGE.Elements;
using RAGE.Ui;

namespace ClientSide.Player
{
    public class ExampleEvents : Events.Script
    {
        public ExampleEvents()
        {
            Events.OnPlayerEnterColshape += OnPlayerEnterColshape;
            Events.OnPlayerExitColshape += OnPlayerExitColshape;
            Events.Add("SERVER:CLIENT:RandomizePlayer", RandomizePlayer);
            RAGE.Input.Bind(VirtualKeys.F5, true, () =>
            {
                if (RAGE.Elements.Player.LocalPlayer.Vehicle == null)
                {
                    RAGE.Chat.Output("You're not in car! From client!");
                    return;
                }
                Events.CallRemote("CLIENT:SERVER:RepairCar");
            });
        }

        private void RandomizePlayer(object[] args)
        {
            Random rand = new Random();
            RAGE.Elements.Player.LocalPlayer.SetHeadBlendData(
                rand.Next(0, 3),
                rand.Next(0, 3),
                rand.Next(0, 3),
                rand.Next(0, 3),
                rand.Next(0, 3),
                rand.Next(0, 3),
                0.5f,
                0.5f,
                0.5f,
                false);
        }

        private void OnPlayerExitColshape(Colshape colshape, Events.CancelEventArgs cancel)
        {
            RAGE.Chat.Output("You exited the colshape!");
        }

        private void OnPlayerEnterColshape(Colshape colshape, Events.CancelEventArgs cancel)
        {
            RAGE.Chat.Output("You entered the colshape!");
        }
    }
}