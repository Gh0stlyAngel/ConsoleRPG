﻿using ConsoleFight;
using ConsoleHub;
using ConsoleShop;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static consoleTextRPG.Program;

namespace consoleTextRPG
{
    internal class Maps
    {


        internal static void GoToMap(ref PlayerClass player, ref Story story, ref Map map, int playerPosX, int playerPosY, string nickName = null)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            string[] separator = { "\r\n" };
            
            string[] mapArray = map.MapString.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (string mapItem in mapArray)
            {
                Console.WriteLine(mapItem);
            }

            int[] previousPosition = { playerPosX, playerPosY };
            Console.SetCursorPosition(previousPosition[0], previousPosition[1]);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("@");

            map.Events.DrawEnemies(mapArray);


            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(mapArray[0].Length + 2, 3);
            Console.Write("→ ← ↑ ↓ - Управление");
            if (story.FirstVillageVisit)
            {
                Console.SetCursorPosition(mapArray[0].Length + 2, 4);
                Console.Write("C - Персонаж");
            }
            Console.SetCursorPosition(mapArray[0].Length + 2, 5);
            Console.Write("J - Журнал");


            int[] newPos;
            bool gotEvent = false;

            while (!gotEvent)
            {
                bool moveEnemy = true;
                ConsoleKey playerMove = Console.ReadKey(true).Key;
                Console.ForegroundColor = ConsoleColor.Yellow;

                switch (playerMove)
                {
                    case ConsoleKey.LeftArrow:

                        newPos = DrawNewPlayerPos(mapArray, playerPosX, playerPosY, playerPosX - 1, playerPosY, map);
                        previousPosition[0] = playerPosX;
                        previousPosition[1] = playerPosY;
                        playerPosX = newPos[0];
                        playerPosY = newPos[1];

                        break;
                    case ConsoleKey.UpArrow:
                        newPos = DrawNewPlayerPos(mapArray, playerPosX, playerPosY, playerPosX, playerPosY - 1, map);
                        previousPosition[0] = playerPosX;
                        previousPosition[1] = playerPosY;
                        playerPosX = newPos[0];
                        playerPosY = newPos[1];

                        break;
                    case ConsoleKey.RightArrow:
                        newPos = DrawNewPlayerPos(mapArray, playerPosX, playerPosY, playerPosX + 1, playerPosY, map);
                        previousPosition[0] = playerPosX;
                        previousPosition[1] = playerPosY;
                        playerPosX = newPos[0];
                        playerPosY = newPos[1];

                        break;
                    case ConsoleKey.DownArrow:
                        newPos = DrawNewPlayerPos(mapArray, playerPosX, playerPosY, playerPosX, playerPosY + 1, map);
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
                        moveEnemy = false;
                        break;
                    case ConsoleKey.C:
                        if (story.FirstVillageVisit)
                        {
                            player.ShowStats();
                            Console.ReadKey(true);
                            previousPosition[0] = playerPosX;
                            previousPosition[1] = playerPosY;
                            gotEvent = true;
                            moveEnemy = false;
                        }
                        break;

                    default:
                        moveEnemy = false;
                        break;
                }

                bool gotTrigger = map.Events.Triggered(mapArray[playerPosY][playerPosX], playerPosX, playerPosY);
                if (gotTrigger)
                {
                    gotEvent = true;
                    moveEnemy = false;
                }
                /*foreach (MapEnemy enemy in map.Enemies)
                {
                    int enemyX = enemy.EnemyMovement.CurrentCoordinates[0];
                    int enemyY = enemy.EnemyMovement.CurrentCoordinates[1];
                    if (enemyX == playerPosX && enemyY == playerPosY)
                    {
                        Fight.StartFight(ref player, enemy.BaseEnemy);
                        if (player.HP > 0)
                        {   
                            map.Enemies.Remove(enemy);
                        }
                        else
                        {
                            GameOver();
                        }
                        GoToMap(ref player, ref story, ref map, nickName: player.NickName, playerPosX: playerPosX, playerPosY: playerPosY);
                    }
                }*/
                CheckOnEnemy(ref player, ref story, playerPosX, playerPosY, ref map);
                if (playerPosX != previousPosition[0] || playerPosY != previousPosition[1])
                {
                    if (moveEnemy)
                        map.Events.DrawEnemies(mapArray);
                    CheckOnEnemy(ref player, ref story, playerPosX, playerPosY, ref map);
                }

            }
            int[] coordinates = { playerPosX, playerPosY };
            bool goOut = MoveTo(ref player, ref story, map, coordinates, nickName);

            if (!goOut && map.SpawnOnStartPosition)
            {
                map.SpawnOnStartPosition = false;
                GoToMap(ref player, ref story, ref map, nickName: player.NickName, playerPosX: map.PlayerPosX, playerPosY: map.PlayerPosY);
            }
            else if (!goOut)
                GoToMap(ref player, ref story, ref map, nickName: player.NickName, playerPosX: previousPosition[0], playerPosY: previousPosition[1]);

        }

        internal static void CheckOnEnemy(ref PlayerClass player, ref Story story, int playerPosX, int playerPosY,ref Map map)
        {
            foreach (MapEnemy enemy in map.Enemies)
            {
                int[][] enemyTriggerPositions = new int[][]
                {
                    new int[] { enemy.EnemyMovement.CurrentCoordinates[0]-1, enemy.EnemyMovement.CurrentCoordinates[1]-1 },
                    new int[] { enemy.EnemyMovement.CurrentCoordinates[0], enemy.EnemyMovement.CurrentCoordinates[1]-1 },
                    new int[] { enemy.EnemyMovement.CurrentCoordinates[0]+1, enemy.EnemyMovement.CurrentCoordinates[1]-1 },

                    new int[] { enemy.EnemyMovement.CurrentCoordinates[0]-1, enemy.EnemyMovement.CurrentCoordinates[1] },
                    new int[] { enemy.EnemyMovement.CurrentCoordinates[0], enemy.EnemyMovement.CurrentCoordinates[1] },
                    new int[] { enemy.EnemyMovement.CurrentCoordinates[0]+1, enemy.EnemyMovement.CurrentCoordinates[1] },

                    new int[] { enemy.EnemyMovement.CurrentCoordinates[0]-1, enemy.EnemyMovement.CurrentCoordinates[1]+1 },
                    new int[] { enemy.EnemyMovement.CurrentCoordinates[0], enemy.EnemyMovement.CurrentCoordinates[1]+1 },
                    new int[] { enemy.EnemyMovement.CurrentCoordinates[0]+1, enemy.EnemyMovement.CurrentCoordinates[1]+1 },
                };

                foreach (var triggerPosition in enemyTriggerPositions)
                {
                    if (triggerPosition[0] == playerPosX && triggerPosition[1] == playerPosY)
                    {
                        Fight.StartFight(ref player, enemy.BaseEnemy);
                        if (player.HP > 0)
                        {
                            if (enemy.QuestItem != null)
                            {
                                SlowWrite($"Получен {enemy.QuestItem}", speed: 1, needClear: true, ableToSkip: true);
                                player.Inventory.AppendItem(enemy.QuestItem);
                            }
                            map.Enemies.Remove(enemy);
                        }
                        else
                        {
                            GameOver();
                        }
                        GoToMap(ref player, ref story, ref map, nickName: player.NickName, playerPosX: playerPosX, playerPosY: playerPosY);

                    }
                }

            }
        }

        internal static int[] DrawNewPlayerPos(string[] mapString, int oldX, int oldY, int newX, int newY, Map map)
        {
            bool gotTrigger = map.Events.Triggered(mapString[newY][newX], newX, newY);
            int[] nextXY;
            if (mapString[newY][newX] == '#')
            {
                nextXY = new int[] { oldX, oldY };
                return nextXY;
            }
            else if (mapString[newY][newX] == ' ')
            {
                nextXY = new int[] { newX, newY };
                Console.SetCursorPosition(oldX, oldY);
                Console.Write(mapString[oldY][oldX]);
                Console.SetCursorPosition(newX, newY);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("@");
                return nextXY;
            }
            
            else if (gotTrigger)
            {
                nextXY = new int[] { newX, newY };
                return nextXY;
            }
            else
            {
                nextXY = new int[] { newX, newY };
                Console.SetCursorPosition(oldX, oldY);
                Console.Write(mapString[oldY][oldX]);
                Console.SetCursorPosition(newX, newY);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("@");
                return nextXY;
            }
        }

        internal static bool MoveTo(ref PlayerClass player, ref Story story, Map map, int[] playerCoordinates, string nickName = null)
        {
            bool goOut = false;
            int way = 0;

            if (map.Events.EventsDictionary != null)
            {
                foreach (var pair in map.Events.EventsDictionary)
                {
                    foreach (int[] eventCoordinates in pair.Key)
                    {
                        if (eventCoordinates[0] == playerCoordinates[0] && eventCoordinates[1] == playerCoordinates[1])
                        {
                            way = (int)pair.Value;
                            goto Way;
                        }
                    }
                }
            }
            if (map.Events.InvisibleEventsDictionary != null)
            {
                foreach (var pair in map.Events.InvisibleEventsDictionary)
                {
                    foreach (int[] eventCoordinates in pair.Key)
                    {
                        if (eventCoordinates[0] == playerCoordinates[0] && eventCoordinates[1] == playerCoordinates[1])
                        {
                            way = (int)pair.Value;
                            goto Way;
                        }
                    }
                }
            }

        Way:
            map.Events.StartEvent(ref player, ref story, player.NickName, way);
            return goOut;
        }
    }

    class Map
    {
        public string MapString { get; private set; }

        public int PlayerPosX { get; private set; }

        public int PlayerPosY { get; private set; }

        public MapEvents Events { get; private set; }

        public bool SpawnOnStartPosition { get; set; }

        public char[] Triggers { get; protected set; }

        public List<MapEnemy> Enemies = new List<MapEnemy>();

        public Map(MapEvents events)
        {
            Events = events;
            PlayerPosX = events.PlayerPosX;
            PlayerPosY = events.PlayerPosY;
            MapString = events.MapString;
            SpawnOnStartPosition = events.SpawnOnStartPosition;
            Triggers = events.Triggers;
            Enemies = events.Enemies;
        }
    }


    abstract class MapEvents
    {
        public Dictionary<int[][], EventName> EventsDictionary = new Dictionary<int[][], EventName>();

        public Dictionary<int[][], EventName> InvisibleEventsDictionary = new Dictionary<int[][], EventName>();

        public List<MapEnemy> Enemies = new List<MapEnemy>();

        public string MapString { get; protected set; }

        public int PlayerPosX { get; protected set; }

        public int PlayerPosY { get; protected set; }

        public bool SpawnOnStartPosition { get; set; }

        public char[] Triggers { get; protected set; }



        internal virtual bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            return false;
        }

        public bool Triggered(char symbol, int playerPositionX, int playerPositionY)
        {
            bool isTriggered = false;

            if (Triggers != null)
            {
                foreach (char trigger in Triggers)
                {
                    if (trigger == symbol)
                    {
                        isTriggered = true;
                        return isTriggered;
                    }
                }  
            }
            if (InvisibleEventsDictionary != null)
            {
                foreach (int[][] trigger in InvisibleEventsDictionary.Keys)
                {
                    foreach (int[] triggerCoordinates in trigger)
                    {
                        if (playerPositionX == triggerCoordinates[0] && playerPositionY == triggerCoordinates[1])
                        {
                            isTriggered = true;
                            return isTriggered;
                        }
                    }
                }
            }

            return isTriggered;
        }

        public void DrawEnemies(string[] mapArray)
        {
            foreach (MapEnemy enemy in Enemies)
            {
                int oldX = enemy.EnemyMovement.CurrentCoordinates[0];
                int oldY = enemy.EnemyMovement.CurrentCoordinates[1];
                Console.SetCursorPosition(oldX, oldY);
                Console.Write(mapArray[oldY][oldX]);
                enemy.EnemyMovement.Update();

                Console.SetCursorPosition(enemy.EnemyMovement.CurrentCoordinates[0], enemy.EnemyMovement.CurrentCoordinates[1]);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("%");
            }
        }
    }

    enum EventName
    {
        Trader = 1,
        Herbalist,
        Friend,
        Manufactory,
        Temple,
        Home,
        Headman,
        Outside,

        Combat,

        OutsideBridge,
        Convoy,
        BridgeCamp,
        OutsideCamp,

        MainCampFirst,
        MainCampSecond,
        MainCampThird,
        MainCampFourth,
        MainCampfifth,
        OutsideMainCamp,
        FreeVillagers,
        ToSouthLadder,
        ToNorthLadder,
        FindFriend,
        FriendDecision,

    }

    class MapEnemy
    {
        public Fight.BaseEnemy BaseEnemy { get; protected set; }

        public EnemyMovement EnemyMovement { get; protected set; }

        public Item QuestItem { get; set; }

        public MapEnemy(Fight.BaseEnemy baseEnemy, EnemyMovement enemyMovement)
        {
            BaseEnemy = baseEnemy;
            EnemyMovement = enemyMovement;

        }
    }

    enum Coordinate
    {
        X, Y
    }

    class EnemyMovement
    {
        private int[] StartCoordinates;

        public int[] CurrentCoordinates { get; protected set; }

        public int[] EndCoordinates;

        public int MoveCoordinate { get; private set; }

        private bool ToStart = true;

        public EnemyMovement(int[] startCoordinates, int[] endCoordinates, int moveCoordinate)
        {
            StartCoordinates = startCoordinates;
            CurrentCoordinates = new int[] { startCoordinates[0], startCoordinates[1] };
            EndCoordinates = endCoordinates;
            MoveCoordinate = moveCoordinate;
        }

        public void Update()
        {
            if (CurrentCoordinates[MoveCoordinate] == StartCoordinates[MoveCoordinate] && CurrentCoordinates[MoveCoordinate] == StartCoordinates[MoveCoordinate])
                ToStart = false;

            else if (CurrentCoordinates[MoveCoordinate] == EndCoordinates[MoveCoordinate] && CurrentCoordinates[MoveCoordinate] == EndCoordinates[MoveCoordinate])
                ToStart = true;

            UpdateByCoordinate();


        }

        private void UpdateByCoordinate()
        {
            if (StartCoordinates[MoveCoordinate] < EndCoordinates[MoveCoordinate] && !ToStart)
            {
                CurrentCoordinates[MoveCoordinate] = CurrentCoordinates[MoveCoordinate] + 1;
            }

            else if (StartCoordinates[MoveCoordinate] > EndCoordinates[MoveCoordinate] && !ToStart)
            {
                CurrentCoordinates[MoveCoordinate] = CurrentCoordinates[MoveCoordinate] - 1;
            }

            else if (StartCoordinates[MoveCoordinate] < EndCoordinates[MoveCoordinate] && ToStart)
            {
                CurrentCoordinates[MoveCoordinate] = CurrentCoordinates[MoveCoordinate] - 1;
            }

            else if (StartCoordinates[MoveCoordinate] > EndCoordinates[MoveCoordinate] && ToStart)
            {
                CurrentCoordinates[MoveCoordinate] = CurrentCoordinates[MoveCoordinate] + 1;
            }
        }
    }

    
}

