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

    internal class BridgeZeroEvents: MapEvents
    {
        public BridgeZeroEvents()
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
                        "#                                                     #\r\n" +
                        "#                                                     #\r\n" +
                        "#                                                     #\r\n" +
                        "#                                                     #\r\n" +
                        "#                                                     #\r\n" +
                        "#                                                     #\r\n" +
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

            int[][] drawConvoy = new[]
            {
                new int[]{ 18, 7 }
            };
            InvisibleEventsDictionary.Add(drawConvoy, EventName.DrawConvoy);
        }

        internal override bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            bool goOut = false;
            switch (way)
            {
                case (int)EventName.OutsideBridge:
                    goOut = true;
                    HubEvents.ToOutside(ref player, ref story);
                    break;
                    
                case (int)EventName.DrawConvoy:
                    goOut = true;
                    FindConvoy(ref player, ref story);
                    break;
                default: break;

            }
            return goOut;
        }

        internal static void FindConvoy(ref PlayerClass player, ref Story story)
        {
            SlowWrite("Да где же может быть этот обоз? Нужно осмотреться.", speed: 1, needClear: true, ableToSkip: true);
            SlowWrite("...", speed: 1, needClear: true, ableToSkip: true);
            SlowWrite($"Осмотревшись, {player.NickName} замечает какие-то обломки в стороне от дороги. Возможно, это тот самый обоз, о котором говорил торговец. ", speed: 1, needClear: true, ableToSkip: true);
            story.FoundedConvoy = true;
            Maps.GoToMap(ref player, ref story, ref MapList.BridgeFirst, MapList.BridgeFirst.PlayerPosX, MapList.BridgeFirst.PlayerPosY);
        }
    }

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
            PlayerPosX = 18;
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
                    story.FoundedSteps = true;
                    SlowWrite($"Подойдя и осмотрев обломки {player.NickName} замечает следы, которые ведут куда-то в густую чащу.", speed: 1, needClear: true, ableToSkip: true);
                    story.TraderQuest.NextDescription();
                    ToBridgeSecond(ref player, ref story);
                    break;

                case (int)EventName.OutsideBridge:
                    goOut = true;
                    HubEvents.ToOutside(ref player, ref story);
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
                    SlowWrite($"Здесь пусто. Возможно, следы приведут меня куда нужно.", speed: 1, needClear: true, ableToSkip: true);
                    break;

                case (int)EventName.BridgeCamp: 
                    goOut = true;
                    SlowWrite($"Пройдя по следам, {player.NickName} вышел на большую поляну, на которой располагался лагерь, и следы вели прямо в него.", speed: 1, needClear: true, ableToSkip: true);
                    story.TraderQuest.NextDescription();
                    ToBridgeThird(ref player, ref story);
                    break;

                case (int)EventName.OutsideBridge:
                    goOut = true;
                    HubEvents.ToOutside(ref player, ref story);
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

            int[][] storage = new[]
            {
                new int[]{ 10, 7 },
                new int[]{ 11, 7 }
            };
            EventsDictionary.Add(storage, EventName.Storage);

            int[][] bossHouse = new[]
            {
                new int[]{ 24, 9 },
                new int[]{ 25, 9 }
            };
            EventsDictionary.Add(bossHouse, EventName.BossHouse);

            int[][] tent = new[]
            {
                new int[]{ 24, 9 },
                new int[]{ 25, 9 }
            };
            EventsDictionary.Add(tent, EventName.Tent);


            int[] enemy1StartCoord = { 18, 18 };
            int[] enemy1EndCoord = { 23, 18 };
            MapEnemy mapEnemy1 = AddEnemy("Культист-мечник", 80, 12, 0, enemy1StartCoord, enemy1EndCoord, (int)Coordinate.X);
            Enemies.Add(mapEnemy1);


        }

        internal static MapEnemy AddEnemy(string enemyName, int enemyHP, int enemyDamage, int enemyAtcRange, int[] enemyStartCoord, int[] enemyEndCoord, int coordinate)
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
