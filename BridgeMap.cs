using ConsoleFight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static consoleTextRPG.Program;

namespace consoleTextRPG
{
    internal class BridgeFirstEvents: MapEvents
    {
        public BridgeFirstEvents()
        {
            MapString = "#######################################################\r\n" +
                        "#                                                     #\r\n" +
                        "#                                                     #\r\n" +
                        "#                                                     #\r\n" +
                        "#      Мост                                           #\r\n" +
                        "###################                                   #\r\n" +
                        "|                 #                                   #\r\n" +
                        "|                                                     #\r\n" +
                        "|                 #                                   #\r\n" +
                        "###################                                   #\r\n" +
                        "#                                                     #\r\n" +
                        "#     Обоз                                            #\r\n" +
                        "#     ___                                             #\r\n" +
                        "#    /   \\                                            #\r\n" +
                        "#   /     \\                                           #\r\n" +
                        "#   \\     /                                           #\r\n" +
                        "#    \\___/                                            #\r\n" +
                        "#                                                     #\r\n" +
                        "#                                                     #\r\n" +
                        "#                                                     #\r\n" +
                        "#                                                     #\r\n" +
                        "#                                                     #\r\n" +
                        "#######################################################\r\n";
            PlayerPosX = 1;
            PlayerPosY = 7;
            SpawnOnStartPosition = false;
            Triggers = new char[] { '/', '\\', '|', '_' };

            int[][] outside = new[]
            {
                new int[]{ 0, 6 },
                new int[]{ 0, 7 },
                new int[]{ 0, 8 }
            };
            EventsDictionary.Add(outside, EventName.OutsideBridge);

            int[][] convoy = new[]
{
                new int[]{ 6, 12 },
                new int[]{ 7, 12 },
                new int[]{ 8, 12 },
                new int[]{ 5, 13 },
                new int[]{ 9, 13 },
                new int[]{ 4, 14 },
                new int[]{ 10, 14 },
                new int[]{ 4, 15 },
                new int[]{ 10, 15 },
                new int[]{ 5, 16 },
                new int[]{ 9, 16 },
                new int[]{ 6, 16 },
                new int[]{ 7, 16 },
                new int[]{ 8, 16 },
            };
            EventsDictionary.Add(convoy, EventName.Convoy);


        }

        internal override bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            bool goOut = false;
            switch (way)
            {
                case (int)EventName.Convoy:
                    goOut = true;
                    ToBridgeSecond(ref player, ref story);
                    break;
                default: break;

            }
            return goOut;
        }

        internal static void ToBridgeSecond(ref PlayerClass player, ref Story story)
        {
            Maps.GoToMap(ref player, ref story, ref MapList.BridgeSecond, MapList.BridgeSecond.PlayerPosX, MapList.BridgeSecond.PlayerPosY);
        }
    }

    internal class BridgeSecondEvents: MapEvents
    {
        public BridgeSecondEvents()
        {
            MapString = "########################################################\r\n" +
                        "#                                                      #\r\n" +
                        "#                                                      #\r\n" +
                        "#                                     # #  # #  # #    #\r\n" +
                        "#      Мост                         * * * *  *      #  #\r\n" +
                        "###################                * #  #  # #  *    # #\r\n" +
                        "|                 #                *          #   *    |\r\n" +
                        "|                                  *            #  * * |\r\n" +
                        "|                 #               *               #    |\r\n" +
                        "###################              *                 # ###\r\n" +
                        "#                               *                      #\r\n" +
                        "#     Обоз                     *                       #\r\n" +
                        "#     ___                    *                         #\r\n" +
                        "#    /   \\                 *                           #\r\n" +
                        "#   /     \\              *                             #\r\n" +
                        "#   \\     /            *                               #\r\n" +
                        "#    \\___/  * * * ** *                                 #\r\n" +
                        "#                                                      #\r\n" +
                        "#                                                      #\r\n" +
                        "#                                                      #\r\n" +
                        "#                                                      #\r\n" +
                        "#                                                      #\r\n" +
                        "########################################################\r\n";
            PlayerPosX = 10;
            PlayerPosY = 16;
            SpawnOnStartPosition = false;
            Triggers = new char[] { '/', '\\', '|', '_' };

            int[][] outside = new[]
            {
                new int[]{ 0, 6 },
                new int[]{ 0, 7 },
                new int[]{ 0, 8 }
            };
            EventsDictionary.Add(outside, EventName.OutsideBridge);

            int[][] bridgeCamp = new int[][]
            {
                new int[] { 55, 6 },
                new int[] { 55, 7 },
                new int[] { 55, 8 }
            };
            EventsDictionary.Add(bridgeCamp, EventName.BridgeCamp);
        }

        internal override bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            bool goOut = false;
            switch (way)
            {
                case (int)EventName.Convoy:
                    goOut = true;
                    //ToBridgeThird(ref player, ref story);
                    break;

                case (int)EventName.BridgeCamp: 
                    goOut = true;
                    ToBridgeThird(ref player, ref story);
                    break;

                default: break;

            }
            return goOut;
        }
        internal static void ToBridgeThird(ref PlayerClass player, ref Story story)
        {
            Maps.GoToMap(ref player, ref story, ref MapList.BridgeThird, MapList.BridgeThird.PlayerPosX, MapList.BridgeThird.PlayerPosY);
        }
    }

    internal class BridgeThirdEvents : MapEvents
    {
        public BridgeThirdEvents()
        {
            MapString = "#######################################################\r\n" +
                        "#                                                     #\r\n" +
                        "#       Склад                       Костер            #\r\n" +
                        "#     ##########                                      #\r\n" +
                        "#     #        #                     #####            #\r\n" +
                        "#     #        #                    #     #           #\r\n" +
                        "#     #        #                    #  #  #           #\r\n" +
                        "#     ####\\/####                    #     #           #\r\n" +
                        "#                                    #####            #\r\n" +
                        "#                   ####/\\####                        #\r\n" +
                        "#                   #        #                        #\r\n" +
                        "#                   #        #                        #\r\n" +
                        "#                   #        #                        #\r\n" +
                        "#                   ##########                        #\r\n" +
                        "#      Палатка    Дом начальника                      #\r\n" +
                        "#        #           лагеря                           #\r\n" +
                        "#       # #                          Полигон          #\r\n" +
                        "#      #   #                                          #\r\n" +
                        "#     #     #                       #         #       #\r\n" +
                        "#    #       #                      #---------#       #\r\n" +
                        "#     ##\\_/##      #  #             #         #       #\r\n" +
                        "#                  #  #             # &  &  & #       #\r\n" +
                        "####################\\/#################################";
            PlayerPosX = 21;
            PlayerPosY = 20;
            SpawnOnStartPosition = false;
            Triggers = new char[] { '/', '\\', '|', '_' };

            int[][] outside = new[]
            {
                new int[]{ 20, 22 },
                new int[]{ 21, 22 }
            };
            EventsDictionary.Add(outside, EventName.OutsideCamp);


            int[] enemy1StartCoord = { 18, 18 };
            int[] enemy1EndCoord = { 23, 18 };
            MapEnemy mapEnemy1 = AddEnemy("Культист-мечник", 80, 12, 0, enemy1StartCoord, enemy1EndCoord, (int)Coordinate.X, ref Enemies);
            Enemies.Add(mapEnemy1);


        }

        internal static MapEnemy AddEnemy(string enemyName, int enemyHP, int enemyDamage, int enemyAtcRange, int[] enemyStartCoord, int[] enemyEndCoord, int coordinate, ref List<MapEnemy> Enemies)
        {
            Fight.BaseEnemy enemy = new Fight.BaseEnemy(enemyName, enemyHP, enemyDamage, enemyAtcRange);
            EnemyMovement enemy1Movement = new EnemyMovement(enemyStartCoord, enemyEndCoord, coordinate);
            MapEnemy mapEnemy = new MapEnemy(enemy, enemy1Movement);
            return mapEnemy;
        }

        internal override bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            bool goOut = false;
            switch (way)
            {
                case (int)EventName.OutsideCamp:
                    goOut = true;
                    ToBridgeSecond(ref player, ref story);
                    break;
                default: break;

            }
            return goOut;
        }

        internal static void ToBridgeSecond(ref PlayerClass player, ref Story story)
        {
            Maps.GoToMap(ref player, ref story, ref MapList.BridgeSecond, 54, 7);
        }

    }
}
