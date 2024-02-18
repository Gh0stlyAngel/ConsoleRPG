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
using static System.Net.Mime.MediaTypeNames;
using static ConsoleFight.Fight;





namespace ConsoleFight
{

    internal class Fight
    {
        internal static int StartFight(ref PlayerClass player)
        {

            ConsoleKey[] actions = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4 };
            ConsoleKey[] actionsOnCD = { ConsoleKey.D1, ConsoleKey.D3, ConsoleKey.D4 };

            int eventCounter = 0;

            Queue<string> logQueue = new Queue<string>();

            Random rnd = new Random();
            BaseEnemy baseEnemy = new BaseEnemy("������� ���������", 120, 10, 1);

            int distance = 0;

            // Player
            int playerAbilityCooldown = 0;
            int playerStuned = 0;
            int playerSkipDueAtcRange = 0;

            int enemySkipDueAtcRange = 0;
            int enemyEvadeChance = 8;

            // Warrior
            int enemyStuned = 0;
            int enemyHurt = 0;
            int warriorAdditionalDamage = 0;

            // Sorcerer
            int enemyFreezed = 0;

            // Slayer
            int PlayerEvadeChance = 5;
            if (player.Name == "������")
                PlayerEvadeChance = 15;

            // Archer
            int archerAdditionalDamage = 0;
            int enemyRooted = 0;
            if (player.Name == "������")
                enemyEvadeChance = 3;


            if (baseEnemy.AtcRange > player.AtcRange)
            {
                distance = baseEnemy.AtcRange;
                playerSkipDueAtcRange = distance - player.AtcRange;
            }
            else
            {
                distance = player.AtcRange;
                enemySkipDueAtcRange = distance - baseEnemy.AtcRange;
            }
            
            Console.Clear();
            DrawGUI(6, 13, 6, 15, player, distance, baseEnemy);
            WriteActions(player, ConsoleColor.DarkYellow, playerAbilityCooldown);
            WriteLogs(logQueue);
            Console.ReadKey(true);
            if (distance > 0 && baseEnemy.AtcRange < distance)
                archerAdditionalDamage += 5;
            int dealtDamage;
            dealtDamage = player.Weapon.Damage + rnd.Next(-2, 2) + warriorAdditionalDamage;
            if (player.Name == "������")
                dealtDamage += archerAdditionalDamage;

            int startDamage = player.Weapon.Damage + warriorAdditionalDamage;
            if (player.Name == "������")
                startDamage += archerAdditionalDamage;

            while (baseEnemy.HP > 0 && player.HP > 0)
            {
                WriteActions(player, ConsoleColor.DarkYellow, playerAbilityCooldown);
                // Player's turn
                eventCounter++;
                if (playerSkipDueAtcRange > 0)
                {
                    playerSkipDueAtcRange -= 1;
                    distance -= 1;
                    WriteLogs(logQueue, $"{player.Name} ��������� ��������� ��� �����.");
                }
                else if (playerStuned > 0)
                {
                    playerStuned -= 1;
                    WriteLogs(logQueue, $"{player.Name} �������.");
                }

                else
                {
                    startDamage = player.Weapon.Damage + warriorAdditionalDamage;
                    if (baseEnemy.AtcRange < distance && distance > 0)
                        archerAdditionalDamage = 5;
                    else 
                        archerAdditionalDamage = 0;
                    if (player.Name == "������")
                        startDamage += archerAdditionalDamage;
                    dealtDamage = PlayerTurn(ref player, ref playerAbilityCooldown, ref distance, ref archerAdditionalDamage, ref rnd, ref enemyEvadeChance, ref logQueue, ref warriorAdditionalDamage, ref baseEnemy, ref enemyHurt, ref enemyStuned, ref enemyFreezed, ref  enemySkipDueAtcRange, ref dealtDamage, startDamage, ref enemyRooted);

                }
                WriteActions(player, ConsoleColor.DarkYellow, playerAbilityCooldown);
                DrawGUI(6, 13, 6, 15, player, distance, baseEnemy);
                Console.ReadKey(true);
                eventCounter++;
                playerStuned = CheckOnZero(playerStuned);
                playerAbilityCooldown = CheckOnZero(playerAbilityCooldown);
                if (baseEnemy.HP > 0)
                {
                    if (enemyRooted > 0 && baseEnemy.AtcRange < distance)
                    {
                        enemyRooted = CheckOnZero(enemyRooted);
                        WriteLogs(logQueue, $"{baseEnemy.Name} ����������.");
                    }
                    else if (enemyStuned > 0)
                    {
                        enemyStuned = CheckOnZero(enemyStuned);
                        WriteLogs(logQueue, $"{baseEnemy.Name} �������.");
                    }
                    else if (enemyFreezed > 0)
                    {
                        enemyFreezed = CheckOnZero(enemyFreezed);
                        WriteLogs(logQueue, $"{baseEnemy.Name} ���������.");
                    }
                    else if (enemySkipDueAtcRange > 0)
                    {
                        enemySkipDueAtcRange -= 1;
                        distance -= 1;
                        WriteLogs(logQueue, $"{baseEnemy.Name} ��������� ��������� ��� �����.");
                    }

                    else
                    {
                        float enemyDamageChange = 0f;
                        if (enemyHurt > 0)
                            enemyDamageChange += 0.3f;
                        int bash = rnd.Next(0, 101);
                        int bashDamage = 3;

                        int enemyDealtDamage;
                        int EnemyChanceToMiss = rnd.Next(0, 101);
                        if (bash >= 95)
                        {
                            enemyDealtDamage = (int)(baseEnemy.Damage + bashDamage - Math.Round(baseEnemy.Damage * enemyDamageChange));
                            if (EnemyChanceToMiss < PlayerEvadeChance)
                                WriteLogs(logQueue, $"{baseEnemy.Name} ������������� ������.");
                            else
                            {
                                player.GetDamage(enemyDealtDamage);
                                WriteLogs(logQueue, $"{baseEnemy.Name} ������� � �������� ������ �� 1 ���. {player.Name} �������� {enemyDealtDamage} �����.");
                                playerStuned += 1;
                            }
                        }
                        else
                        {
                            if (EnemyChanceToMiss < PlayerEvadeChance)
                                WriteLogs(logQueue, $"{baseEnemy.Name} ������������� ������.");
                            else
                            {
                                enemyDealtDamage = (int)(baseEnemy.Damage - Math.Round(baseEnemy.Damage * enemyDamageChange));
                                player.GetDamage(enemyDealtDamage);
                                WriteLogs(logQueue, $"{baseEnemy.Name} �������. {player.Name} �������� {enemyDealtDamage} �����.");
                            }
                        }

                    }
                }
                
                enemyHurt = CheckOnZero(enemyHurt);
                if (player.Name == "����")
                    warriorAdditionalDamage++;
                DrawGUI(6, 13, 6, 15, player, distance, baseEnemy);
                Console.ReadKey(true);

            }
            if (player.HP > 0)
            {
                player.getGold(15);
                SlowWrite($"�������� 15 ������.", needClear: true);
            }
                
            return player.HP;
        }

        internal static void DrawGUI(int hpPosX, int hpPosY, int manaPosX, int manaPosY, PlayerClass player, int distance, BaseEnemy enemy = null)
        {
            int startX = 21;
            int startY = 8;
            DrawBorder(startX, startY);
            DrawBar(hpPosX, hpPosY, player.HP, player.MaxHP, ConsoleColor.Green);
            DrawBar(manaPosX, manaPosY, player.MP, player.MaxMP, ConsoleColor.Blue);
            if (enemy != null)
                DrawBar(95, hpPosY + 1, enemy.HP, enemy.MaxHP, ConsoleColor.Red, enemy.Name);
            WriteInfo(distance, player.AtcRange, enemy.AtcRange, player);

            
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
                if (i < 0)
                    i = 0;
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

        internal static void WriteInfo(int distance, int playerActRange, int enemyAtcRange, PlayerClass player)
        {
            int distancePosX = 50;
            int distancePosY = 6;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(distancePosX, distancePosY);
            Console.Write($"����������: {distance}");

            int atcRangeX = 3;
            int atcRangeY = distancePosY;
            Console.SetCursorPosition(atcRangeX, atcRangeY);
            Console.Write($"��������� �����: {playerActRange}");
            Console.SetCursorPosition(atcRangeX + 90, atcRangeY);
            Console.Write($"��������� �����: {enemyAtcRange}");

            player.Inventory.ShowItems(true);

        }

        internal static void WriteLogs(Queue<string> logQueue, string newlog)
        {
            int logPositionY = 24;
            int logPositionX = 21;

            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(logPositionX, logPositionY - 1);
            Console.Write("   �������:");
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
                Console.Write( log);

            }

        }

        internal static void WriteLogs(Queue<string> logQueue)
        {
            int logPositionY = 24;
            int logPositionX = 21;

            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(logPositionX, logPositionY - 1);
            Console.Write("   �������:");
            Console.SetCursorPosition(logPositionX, logPositionY);
            for (int i = 0; i < logQueue.Count; i++)
            {
                Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
            }

            foreach (string log in logQueue)
            {
                Console.SetCursorPosition(logPositionX, logPositionY);
                logPositionY++;
                Console.Write(log);

            }

        }

        internal static void WriteActions(PlayerClass player, ConsoleColor actionsColor, int abilityOnCD, int dealtDamage)
        {
            int startX = 38;
            int startY = 13;
            Console.CursorVisible = false;
            Console.ForegroundColor = actionsColor;

            HealingPotion healingPotion = (HealingPotion)player.Inventory.playerItems.Find(item => item.Name == "����� �������");
            int healingValue = healingPotion.RestoreValue;

            ManaPotion manaPotion = (ManaPotion)player.Inventory.playerItems.Find(item => item.Name == "����� ����");
            int manaValue = manaPotion.RestoreValue;

            Console.SetCursorPosition(startX, startY);
            Console.Write($"1. ��������� ({dealtDamage - 2} - {dealtDamage + 2} �����)\n");

            Console.SetCursorPosition(startX, startY + 1);
            if (abilityOnCD > 0 || player.ActiveAbility.ManaCost > player.MP)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"2. ��������� {player.ActiveAbility.Name} (-{player.ActiveAbility.ManaCost} ����)");

            Console.SetCursorPosition(startX, startY + 2);
            Console.ForegroundColor = actionsColor;
            if (player.Inventory.playerItems.Find(item => item.Name == "����� �������").AmountOfItems <= 0)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"3. ������������ ����� ������� (+{healingValue} ��������)");

            Console.SetCursorPosition(startX, startY + 3);
            Console.ForegroundColor = actionsColor;
            if (player.Inventory.playerItems.Find(item => item.Name == "����� ����").AmountOfItems <= 0)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"4. ������������ ����� ���� (+{manaValue} ����)");
            }


        internal static void WriteActions(PlayerClass player, ConsoleColor actionsColor, int abilityOnCD)
        {
            var clear = new string(' ', 50);
            int startX = 38;
            int startY = 13;
            Console.CursorVisible = false;
            Console.ForegroundColor = actionsColor;

            Console.SetCursorPosition(startX, startY);
            Console.Write(clear);
            Console.SetCursorPosition(startX, startY);
            Console.Write($"1. ���������\n");

            Console.SetCursorPosition(startX, startY + 1);
            Console.Write(clear);
            Console.SetCursorPosition(startX, startY + 1);
            if (abilityOnCD > 0 || player.ActiveAbility.ManaCost > player.MP)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"2. ��������� {player.ActiveAbility.Name}");

            Console.SetCursorPosition(startX, startY + 2);
            Console.Write(clear);
            Console.SetCursorPosition(startX, startY + 2);
            Console.ForegroundColor = actionsColor;
            if (player.Inventory.playerItems.Find(item => item.Name == "����� �������").AmountOfItems <= 0)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"3. ������������ ����� �������");

            Console.SetCursorPosition(startX, startY + 3);
            Console.Write(clear);
            Console.SetCursorPosition(startX, startY + 3);
            Console.ForegroundColor = actionsColor;
            if (player.Inventory.playerItems.Find(item => item.Name == "����� ����").AmountOfItems <= 0)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"4. ������������ ����� ����");
        }

        internal static ConsoleKey GetPlayerAction(List<ConsoleKey> actions, bool showStats = false)
        {
            if (showStats)
                actions.Add(ConsoleKey.C);
            bool inArray = false;
            ConsoleKey playerAction;

            do
            {
                playerAction = Console.ReadKey(true).Key;
                foreach (var action in actions)
                {
                    if (action == playerAction)
                        inArray = true;
                }
            }
            while (!inArray);
            
            return playerAction;
        }

        internal static ConsoleKey GetPlayerAction(PlayerClass player, int playerAbilityCooldown)
        {
            List<ConsoleKey> actions = new List<ConsoleKey> { ConsoleKey.D1 };

            if (playerAbilityCooldown <= 0 && player.ActiveAbility.ManaCost <= player.MP)
                actions.Add(ConsoleKey.D2);

            if (player.Inventory.playerItems.Find(item => item.Name == "����� �������").AmountOfItems > 0)
                actions.Add(ConsoleKey.D3);

            if (player.Inventory.playerItems.Find(item => item.Name == "����� ����").AmountOfItems > 0)
                actions.Add(ConsoleKey.D4);

            actions.Add(ConsoleKey.C);


            bool inList = false;
            ConsoleKey playerAction;
            do
            {
                playerAction = Console.ReadKey(true).Key;
                foreach (var action in actions)
                {
                    if (action == playerAction)
                        inList = true;
                }
            }
            while (!inList);

            return playerAction;
        }



        internal static int CheckOnZero(int value)
        {
            if (value <= 0)
                value = 0;
            else
                value -= 1;
            return value;
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

        public static int PlayerTurn(ref PlayerClass player, ref int playerAbilityCooldown, ref int distance, ref int archerAdditionalDamage, ref Random rnd, ref int enemyEvadeChance, ref Queue<string> logQueue, ref int warriorAdditionalDamage, ref BaseEnemy baseEnemy, ref int enemyHurt, ref int enemyStuned, ref int enemyFreezed, ref int enemySkipDueAtcRange, ref int dealtDamage, int startDamage, ref int enemyRooted)
        {
            
            WriteActions(player, ConsoleColor.Yellow, playerAbilityCooldown, startDamage);
            ConsoleKey playerAction = GetPlayerAction(player, playerAbilityCooldown);
            if (distance > 0 && baseEnemy.AtcRange < distance)
                archerAdditionalDamage = 5;
            else
                archerAdditionalDamage = 0;
            switch (playerAction)
            {
                case ConsoleKey.D1:
                    int playerChanceToMiss = rnd.Next(0, 101);
                    if (playerChanceToMiss < enemyEvadeChance)
                        WriteLogs(logQueue, $"{player.Name} ������������� ������.");
                    else
                    {
                        dealtDamage = player.Weapon.Damage + rnd.Next(-2, 2) + warriorAdditionalDamage;
                        if (player.Name == "������")
                            dealtDamage += archerAdditionalDamage;
                        baseEnemy.GetDamage(dealtDamage);
                        WriteLogs(logQueue, $"{player.Name} �������. {baseEnemy.Name} �������� {dealtDamage} �����.");
                        if (enemyHurt > 0)
                        {
                            enemyStuned += 1;
                            enemyHurt = 0;
                        }
                    }
                    break;

                case ConsoleKey.D2:
                    switch (player.Name)
                    {
                        case "����":
                            // ���������� ����
                            enemyHurt += 2;
                            playerAbilityCooldown += 4;
                            dealtDamage = player.ActiveAbility.Damage;
                            baseEnemy.GetDamage(dealtDamage);
                            WriteLogs(logQueue, $"{player.Name} ��������� {player.ActiveAbility.Name}. {baseEnemy.Name} �������� {dealtDamage} �����.");
                            player.SpendMana();
                            break;

                        case "���":
                            // ������� �����
                            int passiveChance = rnd.Next(0, 101);
                            enemyFreezed += 1;
                            playerAbilityCooldown += 3;
                            dealtDamage = player.ActiveAbility.Damage;
                            baseEnemy.GetDamage(dealtDamage);
                            WriteLogs(logQueue, $"{player.Name} ��������� {player.ActiveAbility.Name}. {baseEnemy.Name} �������� {dealtDamage} �����.");
                            if (passiveChance >= 70)
                            {
                                baseEnemy.GetDamage((int)(dealtDamage / 2));
                                WriteLogs(logQueue, $"������������ �������������! {player.Name} �������� ��������� {player.ActiveAbility.Name}, ������ {(int)(dealtDamage / 2)} �����.");
                            }
                            player.SpendMana();
                            break;

                        case "������":
                            // �����
                            playerAbilityCooldown += 3;
                            if (baseEnemy.HP <= Math.Round(baseEnemy.MaxHP * 0.3))
                            {
                                baseEnemy.GetDamage(baseEnemy.HP);
                                WriteLogs(logQueue, $"{player.Name} ��������� {player.ActiveAbility.Name}. {baseEnemy.Name} ������� ����������� ����.");
                            }
                            else
                            {
                                dealtDamage = player.ActiveAbility.Damage;
                                baseEnemy.GetDamage(dealtDamage);
                                WriteLogs(logQueue, $"{player.Name} ��������� {player.ActiveAbility.Name}. {baseEnemy.Name} �������� {dealtDamage} �����.");
                            }
                            player.SpendMana();
                            break;

                        case "������":
                            // �����������
                            playerAbilityCooldown += 3;
                            enemyRooted += 1;
                            dealtDamage = player.ActiveAbility.Damage;
                            baseEnemy.GetDamage(dealtDamage);
                            WriteLogs(logQueue, $"{player.Name} ��������� {player.ActiveAbility.Name}. {baseEnemy.Name} �������� {dealtDamage} �����.");
                            distance += 1;
                            if (distance > baseEnemy.AtcRange)
                                enemySkipDueAtcRange += 1;
                            player.SpendMana();
                            break;

                        default:
                            break;
                    }
                    break;
                case ConsoleKey.D3:
                    HealingPotion healingPotion = (HealingPotion)player.Inventory.playerItems.Find(item => item.Name == "����� �������").RemoveItem();
                    player.RestoreHP(healingPotion.RestoreValue);
                    WriteLogs(logQueue, $"{player.Name} ���������� ����� �������. ������������� {healingPotion.RestoreValue} ��������.");

                    break;
                case ConsoleKey.D4:
                    ManaPotion manaPotion = (ManaPotion)player.Inventory.playerItems.Find(item => item.Name == "����� ����").RemoveItem();
                    player.RestoreMP(manaPotion.RestoreValue);
                    WriteLogs(logQueue, $"{player.Name} ���������� ����� ����. ������������� {manaPotion.RestoreValue} ����.");
                    break;

                case ConsoleKey.C:
                    player.ShowStats();
                    Console.ReadKey(true);
                    Console.Clear();
                    DrawGUI(6, 13, 6, 15, player, distance, baseEnemy);
                    WriteActions(player, ConsoleColor.Yellow, playerAbilityCooldown, startDamage);
                    WriteLogs(logQueue);
                    PlayerTurn(ref player, ref playerAbilityCooldown, ref distance, ref archerAdditionalDamage, ref rnd, ref enemyEvadeChance, ref logQueue, ref warriorAdditionalDamage, ref baseEnemy, ref enemyHurt, ref enemyStuned, ref enemyFreezed, ref enemySkipDueAtcRange, ref dealtDamage, startDamage, ref enemyRooted);



                    break;

                default:
                    break;


            }

            return dealtDamage;
        }

    }
}