using RAGE;
using System;

namespace ClientSide
{
    public class Client : Events.Script
    {
        public Client()
        {
            RAGE.Chat.Output("Hello World! Client");
        }
    }
}
