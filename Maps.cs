using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static consoleTextRPG.Program;

namespace consoleTextRPG
{
    internal class Maps
    {
        internal static void GoToMap(ref PlayerClass player, ref Story story, string mapString, string nickName = null, int playerPosX = 43, int playerPosY = 18)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            string[] separator = { "\r\n" };
            mapString = "####################################################################################\r\n#                                                                                  #\r\n#     Лавка          Дом травницы      Дом друга        Мастерская                 #\r\n#     ######           ########        #########        ##########                 #\r\n#     #    \\           #      #        #       #        #        #                 #\r\n#     #    /           ####\\/##        ####\\/###        ####\\/####                 #\r\n#     ######                                                                       #\r\n#                                                                                  #\r\n#                                                                                  |\r\n#                                                                                  |\r\n#                                                                                  |\r\n#                                                                                  #\r\n#                                                                                  #\r\n#       I                                                                          #\r\n#    ---I---                                                                       #\r\n#       I                                                                          #\r\n#       I                                                                          #\r\n#       I                                                                          #\r\n#     ########/\\##                                                                 #\r\n#     #          #                                                                 #\r\n#     #          #                 #######/\\#           ########/\\##               #\r\n#     #   Храм   #                 #        #           #          #               #\r\n#     #          #                 #        #           #          #               #\r\n#     #          #                 ##########           ############               #\r\n#     ############                 Родной дом           Дом старосты               #\r\n#                                                                                  #\r\n####################################################################################";

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
                GoToMap(ref player, ref story, mapString, nickName: player.NickName, playerPosX: 43, playerPosY: 18);
            }
            else if (!goOut)
                GoToMap(ref player, ref story, mapString, nickName: player.NickName, playerPosX: previousPosition[0], playerPosY: previousPosition[1]);

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

            foreach (var pair in hubEvenst.EventsDictionary)
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
}
