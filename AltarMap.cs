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

    internal class AltarMapEvents : MapEvents
    {
        public AltarMapEvents()
        {
            MapString = "#######################################################\r\n" +
                        "#                                                     #\r\n" +
                        "#                       ###                           #\r\n" +
                        "#                        #                            #\r\n" +
                        "#                        #                            #\r\n" +
                        "########################                              #\r\n" +
                        "|                                                     #\r\n" +
                        "|                                                     #\r\n" +
                        "|                                     ##              #\r\n" +
                        "########################                #             #\r\n" +
                        "#                        #        ###    #            #\r\n" +
                        "#                        #           #    ##          #\r\n" +
                        "#                       ###           #     #         #\r\n" +
                        "#                                      #     ##       #\r\n" +
                        "#                                              #      #\r\n" +
                        "#                                       #       #     #\r\n" +
                        "#                                       #       #     #\r\n" +
                        "#                                       #       #     #\r\n" +
                        "######                                  #########     #\r\n" +
                        "#                                       #   #   #     #\r\n" +
                        "#     #                                 #  ###  #     #\r\n" +
                        "#     #                       Алтарь    #   #   #     #\r\n" +
                        "#######################################################\r\n";
            PlayerPosX = 1;
            PlayerPosY = 7;
            SpawnOnStartPosition = false;
            Triggers = new char[] { '|' };

            int[] enemy1StartCoord = { 23, 7 };
            int[] enemy1EndCoord = { 2, 7 };
            MapEnemy mapEnemy1 = AddEnemy("Культист-мечник", 80, 12, 0, enemy1StartCoord, enemy1EndCoord, (int)Coordinate.X);
            Enemies.Add(mapEnemy1);

            int[] enemy2StartCoord = { 5, 19 };
            int[] enemy2EndCoord = { 5, 19 };
            MapEnemy mapEnemy2 = AddEnemy("Культист-лучник", 30, 16, 2, enemy2StartCoord, enemy2EndCoord, (int)Coordinate.X);
            Enemies.Add(mapEnemy2);

            int[] enemy3StartCoord = { 44, 16 };
            int[] enemy3EndCoord = { 44, 16 };
            MapEnemy mapEnemy3 = AddEnemy("Культист-маг", 50, 14, 1, enemy3StartCoord, enemy3EndCoord, (int)Coordinate.Y);
            Enemies.Add(mapEnemy3);


            CollectableItem grass1 = new CollectableItem('$', 2, 21, "Трын-трава", "Трын-трава для травницы", textColor: ConsoleColor.Magenta);
            Collectables.Add(grass1);
            CollectableItem grass2 = new CollectableItem('$', 4, 21, "Трын-трава", "Трын-трава для травницы", textColor: ConsoleColor.Magenta);
            Collectables.Add(grass2);

            int[][] toOutside = new[]
            {
                new int[]{ 0, 6 },
                new int[]{ 0, 7 },
                new int[]{ 0, 8 }
            };
            EventsDictionary.Add(toOutside, EventName.OutsideAltarMap);

        }

        internal override bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            bool goOut = false;
            switch (way)
            {
                case (int)EventName.OutsideAltarMap:
                    goOut = true;
                    HubEvents.ToOutside(ref player, ref story);
                    break;

                default: break;

            }
            return goOut;
        }

        internal static MapEnemy AddEnemy(string enemyName, int enemyHP, int enemyDamage, int enemyAtcRange, int[] enemyStartCoord, int[] enemyEndCoord, int coordinate)
        {
            Fight.BaseEnemy enemy = new Fight.BaseEnemy(enemyName, enemyHP, enemyDamage, enemyAtcRange);
            EnemyMovement enemy1Movement = new EnemyMovement(enemyStartCoord, enemyEndCoord, coordinate);
            MapEnemy mapEnemy = new MapEnemy(enemy, enemy1Movement);
            return mapEnemy;
        }
    }

}