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

namespace consoleTextRPG
{
    internal class HubMap
    {
        internal static void GoToHub(PlayerClass player, Story story, int playerPosX = 43, int playerPosY = 18)
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
            Console.SetCursorPosition(88, 4);
            Console.Write("C - Персонаж");
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
                        player.ShowStats();
                        Console.ReadKey(true);
                        previousPosition[0] = playerPosX;
                        previousPosition[1] = playerPosY;
                        gotEvent = true;
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
            bool goOut = MoveTo(player, coordinates);

            if (!goOut)
                GoToHub(player, story, previousPosition[0], previousPosition[1]);

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

        internal static bool MoveTo(PlayerClass player, int[] coordinates)
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
                    HubEvenst.ToTraderHouse(player);
                    break;
                case (int)Events.Herbalist:
                    HubEvenst.ToHerbalistHouse(player);
                    break;
                case (int)Events.Friend:
                    HubEvenst.ToFriendHouse(player);
                    break;
                case (int)Events.Manufactory:
                    HubEvenst.ToManufactory(player);
                    break;
                case (int)Events.Temple:
                    HubEvenst.ToTemple(player);
                    break;
                case (int)Events.Home:
                    HubEvenst.ToHome(player);
                    break;
                case (int)Events.Headman:
                    HubEvenst.ToHeadmanHouse(player);
                    break;
                case (int)Events.Outside:
                    goOut = true;
                    HubEvenst.ToOutside(player);
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

    internal class HubEvenst
    {
        public Dictionary<int[][], Events> EventsDictionary = new Dictionary<int[][], Events>();

        public static string EmptyHome = "Вы постучали в дверь, но никто не ответил. Наверное, дома никого нет.";

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

        internal static void ToTraderHouse(PlayerClass player)
        {
            Shop.ToShop(player);
        }
        internal static void ToHerbalistHouse(PlayerClass player)
        {
            Console.Clear();
            Console.WriteLine(EmptyHome);
            Console.ReadKey(true);
        }
        internal static void ToFriendHouse(PlayerClass player)
        {
            Console.Clear();
            Console.WriteLine(EmptyHome);
            Console.ReadKey(true);
        }
        internal static void ToManufactory(PlayerClass player)
        {
            Console.Clear();
            Console.WriteLine(EmptyHome);
            Console.ReadKey(true);
        }
        internal static void ToTemple(PlayerClass player)
        {
            Console.Clear();
            Console.WriteLine(EmptyHome);
            Console.ReadKey(true);
        }
        internal static void ToHome(PlayerClass player)
        {
            Console.Clear();
            Console.WriteLine("ToHome");
            Console.ReadKey(true);
        }
        internal static void ToHeadmanHouse(PlayerClass player)
        {
            Console.Clear();
            Hub.HubStart(player);
            Console.ReadKey(true);
        }
        internal static void ToOutside(PlayerClass player)
        {
            Console.Clear();
            Console.WriteLine("ToOutside");
            Console.ReadKey(true);
        }


    }
}
