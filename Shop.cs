using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using consoleTextRPG;
using static consoleTextRPG.Program;
using static ConsoleFight.Fight;
using ConsoleHub;

namespace ConsoleShop
{
    internal class Shop
    {

        internal static void ToShop(ref PlayerClass player, ref Story story)
        {
            SlowWrite("Вы прибыли в лавку торговца.");
            Console.Clear();
            if (!story.FirstShopVisit && story.FirstVisitHeadman)
            {
                Hub.ShopGetPotions(ref player, ref story);
            }
            else
            {
                List<ConsoleKey> actions;
                if ((story.TraderQuest.First().Value[0] && !story.TraderQuest.First().Value[1]) || story.TraderQuest.First().Value[2])
                {
                    SlowWrite("1. Посмотреть ассортимент", needClear: false);
                    SlowWrite("2. Покинуть магазин", needClear: false);
                    actions = new List<ConsoleKey> { ConsoleKey.D1, ConsoleKey.D2 };
                    ConsoleKey playerAction = GetPlayerAction(actions);



                    switch (playerAction)
                    {

                        case ConsoleKey.D1:
                            OpenShop(player);

                            break;

                        case ConsoleKey.D2:
                            SlowWrite("Вы уходите.");

                            break;

                        default: break;
                    }
                }
                else
                {
                    SlowWrite("1. Посмотреть ассортимент", needClear: false);
                    SlowWrite("2. Спросить про запасы", needClear: false);
                    SlowWrite("3. Покинуть магазин", needClear: false);
                    actions = NumberOfActions(3);
                    ConsoleKey playerAction = GetPlayerAction(actions);



                    switch (playerAction)
                    {

                        case ConsoleKey.D1:
                            OpenShop(player);

                            break;
                        case ConsoleKey.D2:
                            Hub.ShopQuest(player, ref story);
                            break;

                        case ConsoleKey.D3:
                            SlowWrite("Вы уходите.");
                            break;

                        default: break;
                    }
                }

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

                List<ConsoleKey> actions = new List<ConsoleKey> { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3 };
                ConsoleKey playerAction = GetPlayerAction(actions);

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
                SlowWrite($"Куплено {itemName}.");
            }
            else
                SlowWrite("Недостаточно монет для покупки.");
        }
    }

}
