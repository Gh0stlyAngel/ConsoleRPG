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

namespace ConsoleFight
{

    public class Fight
    {
        internal static int StartWariorFight(ref Program.Warrior player)
        {

            Random rnd = new Random();
            BaseEnemy baseEnemy = new BaseEnemy("Ѕазовый противник", 50, 10, 1);

            DrawGUI(3, 3, 3, 5, player, baseEnemy);
            while (true)
            {
                Console.ReadKey();
            }
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
                if (playerSkipNext > 0)
                {
                    playerSkipNext -= 1;
                    Console.WriteLine($"{player.Name} пропускает ход.");
                }

                else
                {
                    baseEnemy.GetDamage(player.Weapon.Damage);
                    Console.WriteLine($"{baseEnemy.Name} получает {player.Weapon.Damage} урона.");
                }


                if (enemySkipNext > 0)
                {
                    enemySkipNext -= 1;
                    Console.WriteLine("ѕротивник пропускает ход.");
                }
                else
                {
                    player.GetDamage(baseEnemy.Damage);
                    Console.WriteLine($"{player.Name} получает {baseEnemy.Damage} урона.");
                }
                Console.ReadKey();

            }
            Console.ReadKey();
            return player.HP;
        }

        internal static void DrawGUI(int hpPosX, int hpPosY, int manaPosX, int manaPosY, Program.BaseClass player, BaseEnemy enemy = null)
        {
            DrawBar(hpPosX, hpPosY, player.HP, player.MaxHP, ConsoleColor.Green);
            DrawBar(manaPosX, manaPosY, player.MP, player.MaxMP, ConsoleColor.Blue);
            if (enemy != null)
                DrawBar(45, hpPosY, enemy.HP, enemy.MaxHP, ConsoleColor.Red);
        }

        internal static void DrawBar(int posX, int posY, int current, int max, ConsoleColor barColor)
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

        }


        /*            Console.SetCursorPosition(posX + 3, posY - 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{current}/{max}");*/

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