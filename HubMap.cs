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
    internal class HubMap
    {
        internal static void GoToHub(ref PlayerClass player, ref Story story, string nickName = null, int playerPosX = 43, int playerPosY = 18)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            string[] separator = { "\r\n" };
            string mapString = "####################################################################################\r\n#                                                                                  #\r\n#     Лавка          Дом травницы      Дом друга        Мастерская                 #\r\n#     ######           ########        #########        ##########                 #\r\n#     #    \\           #      #        #       #        #        #                 #\r\n#     #    /           ####\\/##        ####\\/###        ####\\/####                 #\r\n#     ######                                                                       #\r\n#                                                                                  #\r\n#                                                                                  |\r\n#                                                                                  |\r\n#                                                                                  |\r\n#                                                                                  #\r\n#                                                                                  #\r\n#       I                                                                          #\r\n#    ---I---                                                                       #\r\n#       I                                                                          #\r\n#       I                                                                          #\r\n#       I                                                                          #\r\n#     ########/\\##                                                                 #\r\n#     #          #                                                                 #\r\n#     #          #                 #######/\\#           ########/\\##               #\r\n#     #   Храм   #                 #        #           #          #               #\r\n#     #          #                 #        #           #          #               #\r\n#     #          #                 ##########           ############               #\r\n#     ############                 Родной дом           Дом старосты               #\r\n#                                                                                  #\r\n####################################################################################";

            string[] map = mapString.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            //Console.WriteLine(mapString);

            foreach (string mapItem in map)
            {
                Console.WriteLine(mapItem);
            }

            int[] previousPosition = { playerPosX, playerPosY };
            Console.SetCursorPosition(previousPosition[0], previousPosition[1]);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("@");

            Console.SetCursorPosition(88, 3);
            Console.Write("→ ← ↑ ↓ - Управление");
            if (story.FirstVillageVisit)
            {
                Console.SetCursorPosition(88, 4);
                Console.Write("C - Персонаж");
            }
            Console.SetCursorPosition(88, 5);
            Console.Write("J - Журнал");


            int[] newPos;
            bool gotEvent = false;
            while (!gotEvent)
            {
                ConsoleKey playerMove = Console.ReadKey(true).Key;
                Console.ForegroundColor = ConsoleColor.Yellow;

                switch (playerMove)
                {
                    case ConsoleKey.LeftArrow:

                        newPos = DrawNewPlayerPos(map, playerPosX, playerPosY, playerPosX - 1, playerPosY);
                        previousPosition[0] = playerPosX;
                        previousPosition[1] = playerPosY;
                        playerPosX = newPos[0];
                        playerPosY = newPos[1];

                        break;
                    case ConsoleKey.UpArrow:
                        newPos = DrawNewPlayerPos(map, playerPosX, playerPosY, playerPosX, playerPosY - 1);
                        previousPosition[0] = playerPosX;
                        previousPosition[1] = playerPosY;
                        playerPosX = newPos[0];
                        playerPosY = newPos[1];

                        break;
                    case ConsoleKey.RightArrow:
                        newPos = DrawNewPlayerPos(map, playerPosX, playerPosY, playerPosX + 1, playerPosY);
                        previousPosition[0] = playerPosX;
                        previousPosition[1] = playerPosY;
                        playerPosX = newPos[0];
                        playerPosY = newPos[1];

                        break;
                    case ConsoleKey.DownArrow:
                        newPos = DrawNewPlayerPos(map, playerPosX, playerPosY, playerPosX, playerPosY + 1);
                        previousPosition[0] = playerPosX;
                        previousPosition[1] = playerPosY;
                        playerPosX = newPos[0];
                        playerPosY = newPos[1];
                        break;
                    case ConsoleKey.J:
                        story.ShowJournal();
                        previousPosition[0] = playerPosX;
                        previousPosition[1] = playerPosY;
                        gotEvent = true;
                        break;
                    case ConsoleKey.C:
                        if (story.FirstVillageVisit)
                        {
                            player.ShowStats();
                            Console.ReadKey(true);
                            previousPosition[0] = playerPosX;
                            previousPosition[1] = playerPosY;
                            gotEvent = true;
                        }
                        break;
                    default:

                        break;
                }
                if (map[playerPosY][playerPosX] == '/' || map[playerPosY][playerPosX] == '\\' || map[playerPosY][playerPosX] == '|')
                {
                    gotEvent = true;
                }
            }
            int[] coordinates = { playerPosX, playerPosY };
            bool goOut = MoveTo(ref player, ref story, coordinates, nickName);

            if (!goOut && story.SpawnNearHome)
            {
                story.SpawnNearHome = false;
                GoToHub(ref player, ref story, nickName: player.NickName, playerPosX: 43, playerPosY: 18);
            }
            else if (!goOut)
                GoToHub(ref player, ref story, nickName: player.NickName, playerPosX: previousPosition[0], playerPosY: previousPosition[1]);

        }

        internal static int[] DrawNewPlayerPos(string[] map, int oldX, int oldY, int newX, int newY)
        {
            int[] nextXY;
            if (map[newY][newX] == '#')
            {
                nextXY = new int[] { oldX, oldY };
                return nextXY;
            }
            else if (map[newY][newX] == ' ')
            {
                nextXY = new int[] { newX, newY };
                Console.SetCursorPosition(oldX, oldY);
                Console.Write(map[oldY][oldX]);
                Console.SetCursorPosition(newX, newY);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("@");
                return nextXY;
            }
            else if (map[newY][newX] == '/' || map[newY][newX] == '\\' || map[newY][newX] == '|')
            {
                nextXY = new int[] { newX, newY };
                return nextXY;
            }
            else
            {
                nextXY = new int[] { newX, newY };
                Console.SetCursorPosition(oldX, oldY);
                Console.Write(map[oldY][oldX]);
                Console.SetCursorPosition(newX, newY);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("@");
                return nextXY;
            }
        }

        internal static bool MoveTo(ref PlayerClass player, ref Story story, int[] coordinates, string nickName = null)
        {
            bool goOut = false;
            HubEvenst hubEvenst = new HubEvenst();
            int way = 0;

            foreach(var pair in hubEvenst.EventsDictionary)
            {
                foreach (int[] coord in pair.Key)
                {
                    if (coord[0] == coordinates[0] && coord[1] == coordinates[1])
                    {
                        way = (int)pair.Value;
                        break;
                    }
                    if (way != 0)
                        break;
                }
            }

            switch (way)
            {
                case (int)Events.Trader:
                    HubEvenst.ToTraderHouse(ref player, ref story);
                    break;
                case (int)Events.Herbalist:
                    HubEvenst.ToHerbalistHouse(ref player, ref story);
                    break;
                case (int)Events.Friend:
                    HubEvenst.ToFriendHouse(ref player, ref story);
                    break;
                case (int)Events.Manufactory:
                    HubEvenst.ToManufactory(ref player, ref story);
                    break;
                case (int)Events.Temple:
                    HubEvenst.ToTemple(ref player, ref story);
                    break;
                case (int)Events.Home:
                    HubEvenst.ToHome(ref player, ref story, nickName);
                    break;
                case (int)Events.Headman:
                    HubEvenst.ToHeadmanHouse(ref player, ref story);
                    break;
                case (int)Events.Outside:
                    goOut = true;
                    HubEvenst.ToOutside(ref player, ref story);
                    if (!story.FirstVillageVisit || story.ArtefactCollected)
                        goOut = false;
                    break;
                default: break;
            }
            return goOut;
        }

    }
    enum Events
    {
        Trader = 1,
        Herbalist,
        Friend,
        Manufactory,
        Temple,
        Home,
        Headman,
        Outside
    }

    abstract class MapEvents
    {

    }
    internal class HubEvenst: MapEvents
    {
        public Dictionary<int[][], Events> EventsDictionary = new Dictionary<int[][], Events>();

        public static string EmptyHome = "Вы постучали в дверь, но никто не ответил. Наверное, дома никого нет.";

        public static string VisitHome = "Это подождет, нужно посетить родных.";

        public static string CollectArtefact = "Нет времени, в первую очередь - артефакт!";

        public HubEvenst()
        {
            int[][] trader = new []
            {
                new int[]{ 11, 4 },
                new int[]{ 11, 5 }
            };
            EventsDictionary.Add(trader, Events.Trader);

            int[][] herbalist = new[]
            {
                new int[]{ 27, 5 },
                new int[]{ 28, 5 }
            };
            EventsDictionary.Add(herbalist, Events.Herbalist);

            int[][] friend = new[]
{
                new int[]{ 43, 5 },
                new int[]{ 44, 5 }
            };
            EventsDictionary.Add(friend, Events.Friend);

            int[][] manufactory = new[]
{
                new int[]{ 60, 5 },
                new int[]{ 61, 5 }
            };
            EventsDictionary.Add(manufactory, Events.Manufactory);

            int[][] temple = new[]
{
                new int[]{ 14, 18 },
                new int[]{ 15, 18 }
            };
            EventsDictionary.Add(temple, Events.Temple);

            int[][] home = new[]
{
                new int[]{ 42, 20 },
                new int[]{ 43, 20 }
            };
            EventsDictionary.Add(home, Events.Home);

            int[][] headman = new[]
{
                new int[]{ 64, 20 },
                new int[]{ 65, 20 }
            };
            EventsDictionary.Add(headman, Events.Headman);

            int[][] outside = new[]
{
                new int[]{ 83, 8 },
                new int[]{ 83, 9 },
                new int[]{ 83, 10 }
            };
            EventsDictionary.Add(outside, Events.Outside);
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
