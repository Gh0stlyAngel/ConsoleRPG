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

    internal class HerbalistEvents : MapEvents
    {
        public HerbalistEvents()
        {
            MapString = "########################################################\r\n" +
                        "#                                                      #\r\n" +
                        "#                                                      #\r\n" +
                        "#                                                      #\r\n" +
                        "|                                                      #\r\n" +
                        "|                                                      #\r\n" +
                        "|                                                      #\r\n" +
                        "#                                                      #\r\n" +
                        "#                                                      #\r\n" +
                        "#                                                      #\r\n" +
                        "#                                                      #\r\n" +
                        "#                                                      #\r\n" +
                        "#                           ############   #############\r\n" +
                        "#                           #                          #\r\n" +
                        "#                           #                          #\r\n" +
                        "#                           #                          #\r\n" +
                        "#                           #                          #\r\n" +
                        "#                           #                          #\r\n" +
                        "#                           #                          #\r\n" +
                        "#                           #                          #\r\n" +
                        "########################################################";
            PlayerPosX = 1;
            PlayerPosY = 5;
            SpawnOnStartPosition = false;
            Triggers = new char[] { '|' };

            int[] enemy1StartCoord = { 41, 12 };
            int[] enemy1EndCoord = { 41, 12 };
            MapEnemy mapEnemy1 = AddEnemy("Культист-мечник", 80, 12, 0, enemy1StartCoord, enemy1EndCoord, (int)Coordinate.X);
            Enemies.Add(mapEnemy1);

            int[] enemy2StartCoord = { 3, 14 };
            int[] enemy2EndCoord = { 19, 14 };
            MapEnemy mapEnemy2 = AddEnemy("Культист-лучник", 30, 16, 2, enemy2StartCoord, enemy2EndCoord, (int)Coordinate.X);
            Enemies.Add(mapEnemy2);

            int[] enemy3StartCoord = { 20, 2 };
            int[] enemy3EndCoord = { 20, 9 };
            MapEnemy mapEnemy3 = AddEnemy("Культист-маг", 50, 14, 1, enemy3StartCoord, enemy3EndCoord, (int)Coordinate.Y);
            Enemies.Add(mapEnemy3);


            CollectableItem grass1 = new CollectableItem('$', 5, 1, "Трын-трава", "Трын-трава для травницы", textColor: ConsoleColor.Magenta);
            Collectables.Add(grass1);
            CollectableItem grass2 = new CollectableItem('$', 51, 1, "Трын-трава", "Трын-трава для травницы", textColor: ConsoleColor.Magenta);
            Collectables.Add(grass2);
            CollectableItem grass3 = new CollectableItem('$', 42, 4, "Трын-трава", "Трын-трава для травницы", textColor: ConsoleColor.Magenta);
            Collectables.Add(grass3);
            CollectableItem grass4 = new CollectableItem('$', 23, 7, "Трын-трава", "Трын-трава для травницы", textColor: ConsoleColor.Magenta);
            Collectables.Add(grass4);
            CollectableItem grass5 = new CollectableItem('$', 9, 11, "Трын-трава", "Трын-трава для травницы", textColor: ConsoleColor.Magenta);
            Collectables.Add(grass5);
            CollectableItem grass6 = new CollectableItem('$', 4, 18, "Трын-трава", "Трын-трава для травницы", textColor: ConsoleColor.Magenta);
            Collectables.Add(grass6);
            CollectableItem grass7 = new CollectableItem('$', 34, 14, "Трын-трава", "Трын-трава для травницы", textColor: ConsoleColor.Magenta);
            Collectables.Add(grass7);
            CollectableItem grass8 = new CollectableItem('$', 51, 16, "Трын-трава", "Трын-трава для травницы", textColor: ConsoleColor.Magenta);
            Collectables.Add(grass8);
            CollectableItem grass9 = new CollectableItem('$', 35, 19, "Трын-трава", "Трын-трава для травницы", textColor: ConsoleColor.Magenta);
            Collectables.Add(grass9);

            int[][] toOutside = new[]
            {
                new int[]{ 0, 4 },
                new int[]{ 0, 5 },
                new int[]{ 0, 6 }
            };
            EventsDictionary.Add(toOutside, EventName.OutsideHerbalistMap);

        }

        internal override bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            bool goOut = false;
            switch (way)
            {
                case (int)EventName.OutsideHerbalistMap:
                    goOut = true;

                    if (story.HerbalistMainQuest.QuestStarted)
                    {
                        if (Collectables.Count < 1)
                        {
                            SlowWrite("Этого количества травы должно быть достаточно. Нужно принести ее травнице.");
                        }
                        else
                            SlowWrite("Думаю травнице может пригодится больше травы, чем я собрал. Нужно поискать еще.");
                    }
                    
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