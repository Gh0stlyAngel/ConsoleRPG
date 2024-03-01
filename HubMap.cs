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

namespace consoleTextRPG
{
    internal class HubEvents : MapEvents
    {
        public static string EmptyHome = "Вы постучали в дверь, но никто не ответил. Наверное, дома никого нет.";

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
            if (!story.FirstVillageVisit)
                SlowWrite(VisitHome);
            else if (!story.ArtefactCollected)
                SlowWrite(CollectArtefact);
            else
                Shop.ToShop(ref player, ref story);
        }
        internal static void ToHerbalistHouse(ref PlayerClass player, ref Story story)
        {
            if (!story.FirstVillageVisit)
                SlowWrite(VisitHome);
            else if (!story.ArtefactCollected)
                SlowWrite(CollectArtefact);
            else
                SlowWrite(EmptyHome);
        }
        internal static void ToFriendHouse(ref PlayerClass player, ref Story story)
        {
            if (!story.FirstVillageVisit)
                SlowWrite(VisitHome);
            else if (!story.ArtefactCollected)
                SlowWrite(CollectArtefact);
            else
                SlowWrite(EmptyHome);
        }
        internal static void ToManufactory(ref PlayerClass player, ref Story story)
        {
            if (!story.FirstVillageVisit)
                SlowWrite(VisitHome);
            else if (!story.ArtefactCollected)
                SlowWrite(CollectArtefact);
            else
                SlowWrite(EmptyHome);
        }
        internal static void ToTemple(ref PlayerClass player, ref Story story)
        {
            if (!story.FirstVillageVisit)
                SlowWrite(VisitHome);
            else if (!story.ArtefactCollected)
                SlowWrite(CollectArtefact);
            else
                SlowWrite(EmptyHome);
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
                SlowWrite("ToHome");
        }
        internal static void ToHeadmanHouse(ref PlayerClass player, ref Story story)
        {
            if (!story.FirstVillageVisit)
                SlowWrite(VisitHome);
            else if (!story.ArtefactCollected)
                SlowWrite(CollectArtefact);
            else if (!story.FirstVisitHeadman)
            {
                Console.Clear();
                story.FirstVisitHeadman = true;
                Hub.ToHeadman(ref player);
            }
            else if (story.FirstVisitHeadman && !story.FirstShopVisit)
            {
                SlowWrite($"Загляни к торговцу, скажи что от меня, он выдаст тебе несколько зелий, на всякий случай. А после жду тебя тут.", teller: "Староста");
            }
            else if (story.FirstVisitHeadman && story.FirstShopVisit)
            {
                Hub.HeadmanMainQuest(ref player, ref story);
            }
            else
            {
                SlowWrite("toHeadman");
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
                Console.Clear();
                Console.WriteLine("ToOutside");
                Console.ReadKey(true);
            }

        }


    }
}
