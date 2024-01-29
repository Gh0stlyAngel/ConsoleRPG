using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using consoleTextRPG;
using System.Linq.Expressions;
using static consoleTextRPG.Program;





namespace ConsoleFight
{

    public class Fight
    {
        internal static int StartFight(ref Program.Warrior player)
        {
            int eventCounter = 0;
            int logPositionY = 23;
            int logPositionX = 21;
            Queue<string> logQueue = new Queue<string>();

            Random rnd = new Random();
            BaseEnemy baseEnemy = new BaseEnemy("Базовый противник", 50, 10, 1);

            DrawGUI(6, 13, 6, 15, player, baseEnemy);

            int distance = 0;
            int playerSkipNext = 0;
            int enemySkipNext = 0;
            if (baseEnemy.AtcRange > player.AtcRange)
            {
                distance = baseEnemy.AtcRange;
                playerSkipNext = distance - player.AtcRange;
            }
            else
            {
                distance = player.AtcRange;
                enemySkipNext = distance - baseEnemy.AtcRange;
            }
            while (baseEnemy.HP > 0 && player.HP > 0)
            {
                eventCounter++;
                Console.SetCursorPosition(logPositionX, logPositionY);
                if (playerSkipNext > 0)
                {
                    playerSkipNext -= 1;
                    WriteLogs(logQueue, $"{player.Name} пропускает ход.");
                }

                else
                {
                    baseEnemy.GetDamage(player.Weapon.Damage);
                    WriteLogs(logQueue, $"{baseEnemy.Name} получает {player.Weapon.Damage} урона.");
                }
                DrawGUI(6, 13, 6, 15, player, baseEnemy);
                Console.ReadKey(true);
                Console.SetCursorPosition(logPositionX, logPositionY);
                eventCounter++;
                if (baseEnemy.HP > 0)
                {
                    if (enemySkipNext > 0)
                    {
                        enemySkipNext -= 1;
                        WriteLogs(logQueue, "Противник пропускает ход.");
                    }
                    else
                    {
                        player.GetDamage(baseEnemy.Damage);
                        WriteLogs(logQueue, $"{player.Name} получает {baseEnemy.Damage} урона.");
                    }
                }

                DrawGUI(6, 13, 6, 15, player, baseEnemy);
                Console.ReadKey(true);

            }
            return player.HP;
        }

        internal static void DrawGUI(int hpPosX, int hpPosY, int manaPosX, int manaPosY, Program.BaseClass player, BaseEnemy enemy = null)
        {
            int startX = 21;
            int startY = 8;
            DrawBorder(startX, startY);
            DrawBar(hpPosX, hpPosY, player.HP, player.MaxHP, ConsoleColor.Green);
            DrawBar(manaPosX, manaPosY, player.MP, player.MaxMP, ConsoleColor.Blue);
            if (enemy != null)
                DrawBar(95, hpPosY + 1, enemy.HP, enemy.MaxHP, ConsoleColor.Red, enemy.Name);

            
        }

        internal static void DrawBar(int posX, int posY, int current, int max, ConsoleColor barColor, string name = "")
        {
            int barValues = $"{current}/{max}".Length;
            int indents = (10 - barValues) / 2;
            string bar = "";
            for (int i = 0; i < indents; i++)
            {
                bar += ' ';
            }
            bar += $"{current}/{max}";
            int barLength = bar.Length;
            for (int i = 0; i < (10 - barLength); i++)
            {
                bar += ' ';
            }
            ConsoleColor defaultColor = Console.BackgroundColor;
            int currentPercent = current * 10 / max;
            if (currentPercent < 1 && current > 0)
            {
                currentPercent = 1;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(posX, posY);
            Console.Write('[');
            Console.BackgroundColor = barColor;
            for (int i = 0; i < currentPercent; i++)
            {
                Console.Write(bar[i]);
            }
            Console.BackgroundColor = defaultColor;
            for (int i = currentPercent; i < 10; i++)
            {
                Console.Write(bar[i]);
            }
            Console.Write(']');
            Console.SetCursorPosition(posX, posY-1);
            Console.ForegroundColor = barColor;
            Console.Write(name);

        }

        internal static void DrawBorder(int startX, int startY)
        {
            Console.SetCursorPosition(startX, startY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < 70; i++)
            {
                if (i % 2 == 0)
                    Console.Write('#');
                else
                    Console.Write(' ');
            }
            int currentX = Console.CursorLeft;
            int currentY = Console.CursorTop;

            int maxTop = 14;

            for (int i = 0; i < maxTop; i++)
            {
                Console.SetCursorPosition(currentX, currentY + i);
                Console.Write('#');
                Console.SetCursorPosition(startX, currentY + i);
                Console.Write('#');
            }

            Console.SetCursorPosition(startX, maxTop + 7);
            for (int i = 0; i < 70; i++)
            {
                if (i % 2 == 0)
                    Console.Write('#');
                else
                    Console.Write(' ');
            }


        }

        internal static void WriteLogs(Queue<string> logQueue, string newlog)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            int logPositionY = 24;
            int logPositionX = 21;
            Console.SetCursorPosition(logPositionX, logPositionY - 1);
            Console.Write("   История:");
            Console.SetCursorPosition(logPositionX, logPositionY);
            for (int i = 0; i < logQueue.Count; i++)
            {
                Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
            }

            if (logQueue.Count > 4)
                logQueue.Dequeue();

            logQueue.Enqueue(newlog);
            foreach (string log in logQueue)
            {
                Console.SetCursorPosition(logPositionX, logPositionY);
                logPositionY++;
                Console.Write(logPositionY - 24 + ". " + log);

            }

        }
        


        internal class BaseEnemy
        {
            public string Name { get; private set; }
            public int HP { get; private set; }
            public int Damage { get; private set; }
            public int AtcRange { get; private set; }
            
            public int MaxHP { get; private set; }

            public BaseEnemy(string name, int hp, int damage, int atcRange)
            {
                Name = name;
                HP = hp;
                Damage = damage;
                AtcRange = atcRange;
                MaxHP = HP;
            }

            public void GetDamage(int dealtDamage)
            {
                HP -= dealtDamage;
            }
        }

    }
}