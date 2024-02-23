using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using consoleTextRPG;
using ConsoleFight;
using static ConsoleFight.Fight;
using static consoleTextRPG.Program;
using ConsoleShop;

namespace ConsoleHub
{
    internal class Hub
    {
        internal static void ToHub(ref PlayerClass player, ref Story story)
        {
            HubStart(player);
            bool inHub = true;
            int wins = 0;


            while (inHub)
            {
                List<ConsoleKey> actions = new List<ConsoleKey> { ConsoleKey.D1, ConsoleKey.D2 };
                ConsoleColor actionColor;
                Console.Clear();
                SlowWrite("1. toShop", needClear: false, speed:0);
                SlowWrite("2. toFight", needClear: false, speed: 0);


                if (player.Inventory.playerItems.Find(item => item.Name == "Зелье лечения").AmountOfItems > 0)
                {
                    actions.Add(ConsoleKey.D3);
                    SlowWrite("3. useHealingPotion", needClear: false, speed: 0);
                }
                else
                    SlowWrite("3. useHealingPotion", textColor: ConsoleColor.DarkYellow, needClear: false, speed: 0);


                if (player.Inventory.playerItems.Find(item => item.Name == "Зелье маны").AmountOfItems > 0)
                {
                    actions.Add(ConsoleKey.D4);
                    SlowWrite("4. useManaPotion", needClear: false, speed: 0);
                }
                else
                    SlowWrite("4. useManaPotion", textColor: ConsoleColor.DarkYellow, needClear: false, speed: 0);


                SlowWrite("C. ShowStats", needClear: false, speed: 0);
                SlowWrite("J. ShowJournal", needClear: false, speed: 0);
                ConsoleKey playerAction = GetPlayerAction(actions, true);
                
                switch (playerAction)
                {
                    case ConsoleKey.D1:
                        Shop.ToShop(player);
                        break;
                    case ConsoleKey.D2:
                        StartFight(ref player);
                        if (player.HP <= 0)
                            inHub = false;
                        else
                            wins++;
                        break;
                    case ConsoleKey.D3:
                        HealingPotion healingPotion = (HealingPotion)player.Inventory.playerItems.Find(item => item.Name == "Зелье лечения").RemoveItem();
                        player.RestoreHP(healingPotion.RestoreValue);
                        SlowWrite($"{player.Name} использует зелье лечения. Восстановлено {healingPotion.RestoreValue} здоровья.", speed: 1);

                        break;
                    case ConsoleKey.D4:
                        ManaPotion manaPotion = (ManaPotion)player.Inventory.playerItems.Find(item => item.Name == "Зелье маны").RemoveItem();
                        player.RestoreMP(manaPotion.RestoreValue);
                        SlowWrite( $"{player.Name} использует зелье маны. Восстановлено {manaPotion.RestoreValue} маны.", speed: 1);
                        break;

                    case ConsoleKey.C:
                        player.ShowStats();
                        Console.ReadKey(true);
                        break;

                    case ConsoleKey.J:
                        story.ShowJournal();
                        break;
                    default: break;
                }
                

            }
            while (true)
            {

                Console.Clear();
                Console.WriteLine($"Класс: {player.Name}\nПобед {wins}");
                Console.ReadKey(true);

            }
        }

        internal static void HubStart(PlayerClass player)
        {
            SlowWrite($"Первым делом {player.Name} решает навестить старосту деревни, чтобы узнать подробности.");
            SlowWrite($"Однако староста нашёл его раньше...");
            SlowWrite($"{player.Name}!", teller: "Староста");
            SlowWrite($"Наконец ты прибыл..", teller: "Староста");
            SlowWrite($"Здравствуй, рад тебя видеть. Рассказывай, что случилось.", teller: player.Name);
            SlowWrite($"На нашу деревню уже давно нападают ____. До недавних пор это происходило не так часто, и мы справлялись своими силами, но..", teller: "Староста");
            SlowWrite($"Но в последнее время они будто с цепи сорвались! Мужики поговаривают, что видели ___, думаю его/ее появление как-то с этим связано.", teller: "Староста");
            SlowWrite($"___?! Если это правда, то у нас неприятности, мягко говоря.", teller: player.Name);
            SlowWrite($"Ты сможешь нам с этим помочь?", teller: "Староста");
            // Вариативный ответ
            SlowWrite($"Я сделаю все что в моих силах.", teller: player.Name);
            SlowWrite($"Но для начала мне нужно подготовиться. У вас есть где достать зелья?", teller: player.Name);
            SlowWrite($"Конечно. Загляни к торговцу, скажи что от меня, он даст тебе парочку.", teller: "Староста");
            SlowWrite($"Отлично, отправлюсь прямо сейчас.", teller: player.Name);
            SlowWrite($"Дай знать, когда выяснишь что-нибудь. И.. Береги себя!", teller: "Староста");
        }
    }
}