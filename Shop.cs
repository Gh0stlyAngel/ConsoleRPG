using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using consoleTextRPG;
using static consoleTextRPG.Program;
using static ConsoleFight.Fight;

namespace ConsoleShop
{
    internal class Shop
    {
        internal static void ToShop()
        {
            SlowWrite("Вы прибыли в магазин.");
            Console.Clear();
            SlowWrite("1. Посмотреть ассортимент.", needClear: false);
            SlowWrite("2. Уйти.", needClear: false);
            ConsoleKey[] actions = { ConsoleKey.D1, ConsoleKey.D2 };
            ConsoleKey playerAction = GetPlayerAction(actions);

            switch (playerAction)
            {
                
                case ConsoleKey.D1:
                    OpenShop();

                    break;

                case ConsoleKey.D2:
                    SlowWrite("Вы уходите.");

                    break;

                default:
                    break;
            }

        }

        internal static void OpenShop()
        {
            SlowWrite("Ассортимент:");
        }
    }
}
