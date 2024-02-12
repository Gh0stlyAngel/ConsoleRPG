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

        internal static void ToShop(PlayerClass player)
        {
            SlowWrite("Вы прибыли в магазин.");
            Console.Clear();
            SlowWrite("1. Посмотреть ассортимент", needClear: false);
            SlowWrite("2. Покинуть магазин", needClear: false);
            ConsoleKey[] actions = { ConsoleKey.D1, ConsoleKey.D2 };
            ConsoleKey playerAction = GetPlayerAction(actions);

            switch (playerAction)
            {
                
                case ConsoleKey.D1:
                    OpenShop(player);

                    break;

                case ConsoleKey.D2:
                    SlowWrite("Вы уходите.");

                    break;

                default:
                    break;
            }

        }

        internal static void OpenShop(PlayerClass player)
        {
            bool leave = false;
            int HealingPotionPrice = 5;
            int ManaPotionPrice = 5;


            while (!leave)
            {

                Console.Clear();
                SlowWrite("Ассортимент:", needClear: false, speed: 0);
                Console.WriteLine();
                SlowWrite($"1. Зелье лечения - {HealingPotionPrice} монет", needClear: false, speed: 0);
                SlowWrite($"2. Зелье маны - {ManaPotionPrice} монет", needClear: false, speed: 0);
                SlowWrite("3. Покинуть магазин", needClear: false, speed: 0);
                Console.SetCursorPosition(65, 1);
                Console.Write($"Монет: {player.Gold}");

                ConsoleKey[] actions = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3 };
                ConsoleKey playerAction = GetPlayerAction(actions);
                bool ableToPay;

                switch (playerAction)
                {
                    case ConsoleKey.D1:
                        BuyItem(HealingPotionPrice, "Зелье лечения", player);
                        break;

                    case ConsoleKey.D2:
                        BuyItem(ManaPotionPrice, "Зелье маны", player);

                        break;

                    case ConsoleKey.D3:
                        leave = true;
                        SlowWrite("Вы уходите.");
                        Console.ReadKey();

                        break;

                    default:
                        break;
                }
            }

            
        }
        public static void BuyItem(int itemPrice, string itemName, PlayerClass player)
        {
            bool ableToPay = player.SpendGold(itemPrice);
            if (ableToPay)
            {
                player.Inventory.playerItems.Find(item => item.Name == itemName).AddItem();
                SlowWrite($"Куплено {itemName}");
            }
            else
                SlowWrite("Недостаточно монет для покупки");
        }
    }

}
