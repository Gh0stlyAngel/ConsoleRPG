using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Diagnostics.Eventing.Reader;
using ConsoleShop;
using consoleTextRPG;
using ConsoleHub;
using static consoleTextRPG.Program;
using System.Security.Policy;
using static ConsoleFight.Fight;

namespace consoleTextRPG
{
    internal class HubEvents : MapEvents
    {
        public static string EmptyHome = "Вы постучали в дверь, но никто не ответил. Наверное, внутри никого нет.";

        public static string VisitHome = "Это подождет, нужно посетить родных.";

        public static string CollectArtefact = "Нет времени, в первую очередь - артефакт!";



        public HubEvents()
        {
            MapString = "####################################################################################\r\n" +
                        "#                                                                                  #\r\n" +
                        "#     Лавка          Дом травницы      Дом друга        Мастерская                 #\r\n" +
                        "#     ######           ########        #########        ##########                 #\r\n" +
                        "#     #    \\           #      #        #       #        #        #                 #\r\n" +
                        "#     #    /           ####\\/##        ####\\/###        ####\\/####                 #\r\n" +
                        "#     ######                                                                       #\r\n" +
                        "#                                                                                  #\r\n" +
                        "#                                                                                  |\r\n" +
                        "#                                                                                  |\r\n" +
                        "#                                                                                  |\r\n" +
                        "#                                                                                  #\r\n" +
                        "#                                                                                  #\r\n" +
                        "#       I                                                                          #\r\n" +
                        "#    ---I---                                                                       #\r\n" +
                        "#       I                                                                          #\r\n" +
                        "#       I                                                                          #\r\n" +
                        "#       I                                                                          #\r\n" +
                        "#     ########/\\##                                                                 #\r\n" +
                        "#     #          #                                                                 #\r\n" +
                        "#     #          #                 #######/\\#           ########/\\##               #\r\n" +
                        "#     #   Храм   #                 #        #           #          #               #\r\n" +
                        "#     #          #                 #        #           #          #               #\r\n" +
                        "#     #          #                 ##########           ############               #\r\n" +
                        "#     ############                 Родной дом           Дом старосты               #\r\n" +
                        "#                                                                                  #\r\n" +
                        "####################################################################################";
            PlayerPosX = 43;
            PlayerPosY = 18;
            SpawnOnStartPosition = false;
            Triggers = new char [] { '/', '\\', '|' };

            Collectables = new List<CollectableItem>();

            int[][] trader = new[]
            {
                new int[]{ 11, 4 },
                new int[]{ 11, 5 }
            };
            EventsDictionary.Add(trader, EventName.Trader);

            int[][] herbalist = new[]
            {
                new int[]{ 27, 5 },
                new int[]{ 28, 5 }
            };
            EventsDictionary.Add(herbalist, EventName.Herbalist);

            int[][] friend = new[]
{
                new int[]{ 43, 5 },
                new int[]{ 44, 5 }
            };
            EventsDictionary.Add(friend, EventName.Friend);

            int[][] manufactory = new[]
{
                new int[]{ 60, 5 },
                new int[]{ 61, 5 }
            };
            EventsDictionary.Add(manufactory, EventName.Manufactory);

            int[][] temple = new[]
{
                new int[]{ 14, 18 },
                new int[]{ 15, 18 }
            };
            EventsDictionary.Add(temple, EventName.Temple);

            int[][] home = new[]
{
                new int[]{ 42, 20 },
                new int[]{ 43, 20 }
            };
            EventsDictionary.Add(home, EventName.Home);

            int[][] headman = new[]
{
                new int[]{ 64, 20 },
                new int[]{ 65, 20 }
            };
            EventsDictionary.Add(headman, EventName.Headman);

            int[][] outside = new[]
{
                new int[]{ 83, 8 },
                new int[]{ 83, 9 },
                new int[]{ 83, 10 }
            };
            EventsDictionary.Add(outside, EventName.Outside);

            
        }

        internal static bool BasicAnswers(Story story, bool knock)
        {
            bool said = false;
            if (!story.FirstVillageVisit)
            {
                said = true;
                SlowWrite(VisitHome);
            }
            else if (!story.ArtefactCollected)
            {
                said = true;
                SlowWrite(CollectArtefact);
            }
            else if (knock)
                said = Knock(story);
            return said;
        }

        internal static bool Knock(Story story)
        {
            bool said = false;
            if (story.ArtefactCollected && !story.HeadmanMainQuest.QuestPassed)
            {
                said = true;
                SlowWrite(EmptyHome);
            }
            return said;
        }
        internal override bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            bool goOut = false;
            switch (way)
            {
                case (int)EventName.Trader:
                    ToTraderHouse(ref player, ref story);
                    break;
                case (int)EventName.Herbalist:
                    ToHerbalistHouse(ref player, ref story);
                    break;
                case (int)EventName.Friend:
                    ToFriendHouse(ref player, ref story);
                    break;
                case (int)EventName.Manufactory:
                    ToManufactory(ref player, ref story);
                    break;
                case (int)EventName.Temple:
                    ToTemple(ref player, ref story);
                    break;
                case (int)EventName.Home:
                    ToHome(ref player, ref story, nickName);
                    break;
                case (int)EventName.Headman:
                    ToHeadmanHouse(ref player, ref story);
                    break;
                case (int)EventName.Outside:
                    goOut = true;
                    ToOutside(ref player, ref story);
                    if (!story.FirstVillageVisit || story.ArtefactCollected)
                        goOut = false;
                    break;
                default: break;

            }
            return goOut;
        }
        internal static void ToTraderHouse(ref PlayerClass player, ref Story story)
        {
            if (BasicAnswers(story, false))
            {

            }

            else
                Shop.ToShop(ref player, ref story);
        }
        internal static void ToHerbalistHouse(ref PlayerClass player, ref Story story)
        {
            if (BasicAnswers(story, true))
            {

            }
                
            else if (story.HeadmanMainQuest.QuestPassed && !story.HerbalistMainQuest.QuestStarted)
            {
                SlowWrite("'Текст получения квеста травницы'");
                story.HerbalistMainQuest.StartQuest();
            }
            else if (story.HerbalistMainQuest.QuestStarted && !story.HerbalistMainQuest.QuestPassed)
            {
                int grassCounter = 0;
                foreach (var item in player.Inventory.playerItems)
                {
                    if (item.Name == "Трын-трава")
                    { grassCounter++; }
                }
                if (grassCounter >= 9)
                {
                    foreach (var item in player.Inventory.playerItems)
                    {
                        if (item.Name == "Трын-трава")
                            player.Inventory.playerItems.Remove(item);
                    }
                    SlowWrite("'Текст завершения квеста травницы'");
                    story.HerbalistMainQuest.PassQuest();
                }
                else
                    SlowWrite("Слышь де трава?!", teller: "Травница");
            }
            else
            {
                SlowWrite("Сейчас от Травницы мне ничего не нужно.");
            }

        }
        internal static void ToFriendHouse(ref PlayerClass player, ref Story story)
        {
            if (BasicAnswers(story, true))
            {

            }

            else
                SlowWrite("ToFriendHouse");
        }
        internal static void ToManufactory(ref PlayerClass player, ref Story story)
        {
            if (BasicAnswers(story, true))
            {

            }
            else if(story.HeadmanMainQuest.QuestPassed && !story.WeaponUpgraded)
            {
                SlowWrite("Привет, спасибо за спасение, в благодарность я хочу немного улучшить твое оружие.", teller: "Кузнец");
                player.Weapon.UpgradeWeapon();
                story.WeaponUpgraded = true;
            }
            else
                SlowWrite("Не буду тревожить кузнеца, он сейчас занят.");
        }
        internal static void ToTemple(ref PlayerClass player, ref Story story)
        {
            if (BasicAnswers(story, true))
            {

            }

            else if (story.HeadmanMainQuest.QuestPassed && !story.TempleQuest.QuestStarted)
            {
                SlowWrite("Текст получения квеста храмовницы/монашки.");
                story.TempleQuest.StartQuest();
            }
            else if (story.TempleQuest.QuestCompleted && !story.TempleQuest.QuestPassed)
            {
                SlowWrite("Текст сдачи квеста");
                story.TempleQuest.PassQuest();
            }

            else
                Console.WriteLine("Кажется храмовница помогает местным жителям, не буду ей мешать.");
        }
        internal static void ToHome(ref PlayerClass player, ref Story story, string nickName = null)
        {
            if (!story.FirstVillageVisit)
            {
                Hub.ComeToHome(ref player, nickName, ref story);
                story.FirstVillageVisit = true;
            }
            else if (!story.ArtefactCollected)
                SlowWrite(CollectArtefact);
            else if (!story.FirstVisitHeadman)
                SlowWrite("Тебе стоило бы посетить старосту, он тебя ждет.", teller: "Мама");
            else
                SlowWrite("Дом, милый дом..");
        }
        internal static void ToHeadmanHouse(ref PlayerClass player, ref Story story)
        {
            if (BasicAnswers(story, false))
            {

            }
            else if (!story.FirstVisitHeadman)
            {
                Console.Clear();
                story.FirstVisitHeadman = true;
                Hub.ToHeadman(ref player, ref story);
            }
            else if (story.FirstVisitHeadman && !story.FirstShopVisit)
            {
                SlowWrite($"Загляни к торговцу, скажи что от меня, он выдаст тебе несколько зелий, на всякий случай. А после жду тебя тут.", teller: "Староста");
            }
            else if (story.FirstVisitHeadman && story.FirstShopVisit && story.HeadmanMainQuest.QuestStarted && !story.SecondHeadmanVisit)
            {
                Hub.HeadmanMainQuest(ref player, ref story);
                story.SecondHeadmanVisit = true;
            }
            else if (story.HeadmanMainQuest.QuestStarted && !story.HeadmanMainQuest.QuestCompleted)
            {
                SlowWrite("Нужно спасти жителей, староста рассчитывает на меня!");
            }
            else if(story.HeadmanMainQuest.QuestCompleted && !story.HeadmanMainQuest.QuestPassed)
            {
                SlowWrite("Жители спасены, огромное спасибо тебе!");
                story.HeadmanMainQuest.PassQuest();
            }
            else if (story.HeadmanMainQuest.QuestPassed && !story.BossfightQuest.QuestStarted)
            {
                SlowWrite("Спасенные жители рассказали, что видели главу культа, когда были в плену.", teller: "Староста");
                SlowWrite("Он хотел принести жертву, чтобы стать сильнее. Ты нарушил его планы, но не думаю, что он остановится.", teller: "Староста");
                SlowWrite("Нужно разобраться с ним раз и навсегда, иначе, если ему удасться воплотить свои планы в жизнь..", teller: "Староста");
                SlowWrite("Проблемы будут не только у нашей деревни. И только ты можешь с ним справится!", teller: "Староста");

                story.BossfightQuest.StartQuest();
            }
            else
            {
                SlowWrite("Дом старосты. Пока мне от него ничего не нужно.");
            }

        }
        internal static void ToOutside(ref PlayerClass player, ref Story story)
        {
            if (!story.FirstVillageVisit)
                SlowWrite(VisitHome);
            else if (!story.ArtefactCollected)
            {
                Hub.ToCave(ref player, ref story);
                story.ArtefactCollected = true;
                story.SpawnNearHome = true;
            }
            else
            {
                List<ConsoleKey> actions = new List<ConsoleKey>();
                int counter = 1;
                Console.Clear();
                if (story.HeadmanMainQuest.QuestStarted || story.TraderQuest.QuestStarted || true)
                {
                    SlowWrite($"{counter}. В деревню.", needClear: false, ableToSkip: false, tech: true);
                    actions.Add(ConsoleKey.D1);
                    counter++;
                    if (story.SecondHeadmanVisit)
                    {
                        SlowWrite($"{counter}. К лагерю культистов.", needClear: false, ableToSkip: false, tech: true);
                        counter++;
                        actions.Add(ConsoleKey.D2);
                    }
                    else
                    {
                        SlowWrite($"{counter}. ???", needClear: false, ableToSkip: false, tech: true);
                        counter++;
                    }
                    if (story.TraderQuest.QuestStarted)
                    {
                        SlowWrite($"{counter}. К мосту у деревни.", needClear: false, ableToSkip: false, tech: true);
                        counter++;
                        actions.Add(ConsoleKey.D3);
                    }
                    else
                    {
                        SlowWrite($"{counter}. ???", needClear: false, ableToSkip: false, tech: true);
                        counter++;
                    }

                    if (story.HerbalistMainQuest.QuestStarted)
                    {
                        SlowWrite($"{counter}. К опушке у деревни.", needClear: false, ableToSkip: false, tech: true);
                        actions.Add(ConsoleKey.D4);
                        counter++;
                    }
                    else
                    {
                        SlowWrite($"{counter}. ???", needClear: false, ableToSkip: false, tech: true);
                        counter++;
                    }

                    if (story.TempleQuest.QuestStarted)
                    {
                        SlowWrite($"{counter}. К алтарю.", needClear: false, ableToSkip: false, tech: true);
                        actions.Add(ConsoleKey.D5);
                        counter++;
                    }
                    else
                    {
                        SlowWrite($"{counter}. ???", needClear: false, ableToSkip: false, tech: true);
                        counter++;
                    }

                    if (story.BossfightQuest.QuestStarted)
                    {
                        SlowWrite($"{counter}. \"Главная угроза\"", needClear: false, ableToSkip: false, tech: true);
                        actions.Add(ConsoleKey.D6);
                        counter++;
                    }
                    else
                    {
                        SlowWrite($"{counter}. ???", needClear: false, ableToSkip: false, tech: true);
                        counter++;
                    }




                    ConsoleKey playerAction = ConsoleFight.Fight.GetPlayerAction(actions, false, false);

                    switch (playerAction)
                    {
                        case ConsoleKey.D1:
                            Maps.GoToMap(ref player, ref story, ref MapList.Hub, 82, 9);
                            break;
                        case ConsoleKey.D2:
                            Maps.GoToMap(ref player, ref story, ref MapList.MainCampFirst, MapList.MainCampFirst.PlayerPosX, MapList.MainCampFirst.PlayerPosY);
                            break;
                        case ConsoleKey.D3:
                            if (!story.FoundedConvoy && !story.FoundedSteps)
                                Maps.GoToMap(ref player, ref story, ref MapList.BridgeZero, MapList.BridgeZero.PlayerPosX, MapList.BridgeZero.PlayerPosY);


                            else if (!story.FoundedSteps)
                                Maps.GoToMap(ref player, ref story, ref MapList.BridgeFirst, MapList.BridgeZero.PlayerPosX, MapList.BridgeZero.PlayerPosY);

                            else
                                Maps.GoToMap(ref player, ref story, ref MapList.BridgeSecond, MapList.BridgeZero.PlayerPosX, MapList.BridgeZero.PlayerPosY);
                            break;
                        case ConsoleKey.D4:
                            Maps.GoToMap(ref player, ref story, ref MapList.HerbalistMap, MapList.HerbalistMap.PlayerPosX, MapList.HerbalistMap.PlayerPosY);
                            break;
                        case ConsoleKey.D5:
                            Maps.GoToMap(ref player, ref story, ref MapList.AltarMap, MapList.AltarMap.PlayerPosX, MapList.AltarMap.PlayerPosY);
                            break;

                        case ConsoleKey.D6:
                            SlowWrite("Перед боем лучше подготовиться. Вы готовы?");

                            List<ConsoleKey> fightSolution;
                            SlowWrite("1. Да!", needClear: false, ableToSkip: false, tech: true);
                            SlowWrite("2. Нет.", needClear: false, ableToSkip: false, tech: true);
                            actions = new List<ConsoleKey> { ConsoleKey.D1, ConsoleKey.D2 };

                            ConsoleKey fightlayerAction = GetPlayerAction(actions);



                            switch (fightlayerAction)
                            {

                                case ConsoleKey.D1:
                                    BaseEnemy boss = new BaseEnemy("Глава Культа", 150, 10, 1);
                                    StartFight(ref player, boss);

                                    if (player.HP > 0)
                                    {
                                        while (true)
                                            SlowWrite("Конец :)");
                                    }
                                    else
                                        SlowWrite("Game Over");
                                    break;

                                case ConsoleKey.D2:
                                    Maps.GoToMap(ref player, ref story, ref MapList.Hub, 82, 9);

                                    break;

                                default: break;
                            }
                            break;
                    }
                }


                

                Console.ReadKey(true);
            }

        }


    }
}
