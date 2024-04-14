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
using ConsoleFight;
using static System.Collections.Specialized.BitVector32;
using static ConsoleFight.Fight;
using ConsoleHub;
using ConsoleShop;
using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;
using static consoleTextRPG.Program;



namespace consoleTextRPG
{
    public class Program
    {
        internal class MapList
        {
            HubEvents HubEvents = new HubEvents();
            public static Map Hub;



            BridgeThirdEvents BridgeThirdEvents = new BridgeThirdEvents();
            public static Map BridgeThird;

            BridgeSecondEvents BridgeSecondEvents = new BridgeSecondEvents();
            public static Map BridgeSecond;

            BridgeFirstEvents BridgeFirstEvents = new BridgeFirstEvents();
            public static Map BridgeFirst;

            BridgeZeroEvents BridgeZeroEvents = new BridgeZeroEvents();
            public static Map BridgeZero;



            private static List<MapEnemy> EnemyList = new List<MapEnemy>();

            MainCampFirstEvents MainCampFirstEvents = new MainCampFirstEvents();
            public static Map MainCampFirst;

            MainCampSecondEvents MainCampSecondEvents = new MainCampSecondEvents(EnemyList);
            public static Map MainCampSecond;

            MainCampThirdEvents MainCampThirdEvents = new MainCampThirdEvents(EnemyList);
            public static Map MainCampThird;

            MainCampFourthEvents MainCampFourthEvents = new MainCampFourthEvents(EnemyList);
            public static Map MainCampFourth;

            MainCampFifthEvents MainCampFifthEvents = new MainCampFifthEvents(EnemyList);
            public static Map MainCampFifth;


            HerbalistEvents HerbalistEvents = new HerbalistEvents();
            public static Map HerbalistMap;

            AltarMapEvents AltarEvents = new AltarMapEvents();
            public static Map AltarMap;

            public MapList()
            {
                Hub = new Map(HubEvents);


                HerbalistMap = new Map(HerbalistEvents);


                AltarMap = new Map(AltarEvents);


                BridgeThird = new Map(BridgeThirdEvents);
                BridgeSecond = new Map(BridgeSecondEvents);
                BridgeFirst = new Map(BridgeFirstEvents);
                BridgeZero = new Map(BridgeZeroEvents);


                MainCampFirst = new Map(MainCampFirstEvents);
                MainCampSecond = new Map(MainCampSecondEvents);
                MainCampThird = new Map(MainCampThirdEvents);
                MainCampFourth = new Map(MainCampFourthEvents);
                MainCampFifth = new Map(MainCampFifthEvents);

                int[] enemy1StartCoord = { 100, 9 };
                int[] enemy1EndCoord = { 93, 9 };
                MapEnemy mapEnemy1 = AddEnemy("Культист-мечник", 80, 12, 0, enemy1StartCoord, enemy1EndCoord, (int)Coordinate.X);
                EnemyList.Add(mapEnemy1);

                int[] enemy2StartCoord = { 100, 10 };
                int[] enemy2EndCoord = { 93, 10 };
                MapEnemy mapEnemy2 = AddEnemy("Культист-мечник", 80, 12, 0, enemy2StartCoord, enemy2EndCoord, (int)Coordinate.X);
                EnemyList.Add(mapEnemy2);

                int[] enemy3StartCoord = { 112, 8 };
                int[] enemy3EndCoord = { 113, 8 };
                MapEnemy mapEnemy3 = AddEnemy("Культист-мечник", 80, 12, 0, enemy3StartCoord, enemy3EndCoord, (int)Coordinate.X);
                mapEnemy3.QuestItem = new QuestItem("Ключ от клетки", "Ключ от клетки, в которой держат жителей деревни.");
                EnemyList.Add(mapEnemy3);

                int[] enemy4StartCoord = { 87, 17 };
                int[] enemy4EndCoord = { 87, 13 };
                MapEnemy mapEnemy4 = AddEnemy("Культист-мечник", 80, 12, 0, enemy4StartCoord, enemy4EndCoord, (int)Coordinate.Y);
                EnemyList.Add(mapEnemy4);

                int[] enemy5StartCoord = { 91, 18 };
                int[] enemy5EndCoord = { 98, 18 };
                MapEnemy mapEnemy5 = AddEnemy("Культист-мечник", 80, 12, 0, enemy5StartCoord, enemy5EndCoord, (int)Coordinate.X);
                EnemyList.Add(mapEnemy5);

                int[] enemy6StartCoord = { 83, 18 };
                int[] enemy6EndCoord = { 76, 18 };
                MapEnemy mapEnemy6 = AddEnemy("Культист-мечник", 80, 12, 0, enemy6StartCoord, enemy6EndCoord, (int)Coordinate.X);
                EnemyList.Add(mapEnemy6);

            }

            internal static MapEnemy AddEnemy(string enemyName, int enemyHP, int enemyDamage, int enemyAtcRange, int[] enemyStartCoord, int[] enemyEndCoord, int coordinate)
            {
                Fight.BaseEnemy enemy = new Fight.BaseEnemy(enemyName, enemyHP, enemyDamage, enemyAtcRange);
                EnemyMovement enemy1Movement = new EnemyMovement(enemyStartCoord, enemyEndCoord, coordinate);
                MapEnemy mapEnemy = new MapEnemy(enemy, enemy1Movement);
                return mapEnemy;
            }
        }

        class writeThread
        {
            public Thread thread;

            public writeThread(object[] parameters) //Конструктор
            {

                thread = new Thread(func);
                thread.Start(parameters); //передача параметра в поток
                thread.IsBackground = true;
            }


            void func(object parameters)
            {
                object[] parameter = (object[])parameters;
                string str = parameter[0] as string;
                ConsoleColor textColor = (ConsoleColor)parameter[1];
                bool needClear = (bool)parameter[2];
                int speed = (int)parameter[3];
                string teller = parameter[4] as string;

                if (needClear)
                {
                    Console.Clear();

                }

                if (teller != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\n   ");
                    Console.Write(teller);
                    Console.Write("\n");
                }

                Console.ForegroundColor = textColor;

                if (str.Length < 106)
                {
                    Console.Write("\n   ");
                    foreach (char letter in str)
                    {
                        Console.Write(letter);
                        Thread.Sleep(speed);
                    }
                }
                else
                {
                    int lines = (int)Math.Ceiling((double)str.Length / 106);
                    Console.Write("\n   ");
                    for (int i = 0; i < lines; i++)
                    {
                        
                        for (int j = 0 + (i * 106); j < (i + 1) * 106 && j < str.Length; j++)
                        {
                            Console.Write(str[j]);
                            Thread.Sleep(speed);
                        }
                        Console.Write("\n   ");
                        
                    }
                }

                if (needClear)
                {
                    Console.ReadKey(true);

                }
            }


        }


        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 31);

            Task t = Task.Run(() => {
                while (true)
                {
                    if (Console.WindowWidth < 150 || Console.WindowHeight < 31)
                    {
                        Console.SetWindowSize(150, 31);
/*                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(125, 31);
                        Console.WriteLine("НЕ ТРОГАЙ ОКНО БЛЯТЬ!!1!");
                        Console.CursorVisible = false;*/
                    }
                }
            });


            MapList Data = new MapList();


            BridgeFirstEvents bridgeFirstEvents = new BridgeFirstEvents();
            Map bridgeFirst = new Map(bridgeFirstEvents);

            BridgeSecondEvents bridgeSecondEvents = new BridgeSecondEvents();
            Map bridgeSecond = new Map(bridgeSecondEvents);

            HealingPotion healingPotion = new HealingPotion();
            ManaPotion manaPotion = new ManaPotion();

            Story story = new Story();

            Console.CursorVisible = false;
            SlowWrite("ConsoleTextRPG");
            Console.Clear();
            SlowWrite("Введите имя персонажа: ", needClear: false, ableToSkip: false, tech: true);
            string nickName;
            nickName = Console.ReadLine();
            while (nickName == "")
            {
                SlowWrite("Имя не может быть пустым", speed: 0);
                Console.Clear();
                SlowWrite("Введите имя персонажа: ", needClear: false, ableToSkip: false, tech: true);
                nickName = Console.ReadLine();
            }

            Hub.Welcome(nickName, ref story);

            int chosenClass = 1;
            PlayerClass player = PlayerClassFactory.CreateInstance(chosenClass, nickName);
            player.Inventory.AppendItem(healingPotion);
            player.Inventory.AppendItem(manaPotion);

            player.Inventory.playerItems.Find(item => item.Name == "Зелье лечения").AddItem();
            player.Inventory.playerItems.Find(item => item.Name == "Зелье маны").AddItem();
            player.Inventory.playerItems.Find(item => item.Name == "Зелье лечения").AddItem();
            player.Inventory.playerItems.Find(item => item.Name == "Зелье маны").AddItem();

            Maps.GoToMap(ref player, ref story, ref MapList.Hub, MapList.Hub.PlayerPosX, MapList.Hub.PlayerPosY);
            //Maps.GoToMap(ref player, ref story, ref MapList.MainCampFirst, MapList.MainCampFirst.PlayerPosX, MapList.MainCampFirst.PlayerPosY);

            //Maps.GoToMap(ref player, ref story, ref MapList.BridgeFirst, MapList.BridgeFirst.PlayerPosX, MapList.BridgeFirst.PlayerPosY);


            

            

            SlowWrite("Продолжение следует...");

            t.Wait();

            
        }

        internal static void GameOver()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("GameOver");
                Console.ReadKey(true);
            }
        }

        internal static int PlayerPick(PlayerClass warrior, PlayerClass sorcerer, PlayerClass slayer, PlayerClass archer)
        {
            ShowClassList(warrior, sorcerer, slayer, archer);

            bool inChoice = true;
            int chosenClass = 0;
            ConsoleKeyInfo PressedKey;
            while (inChoice)
            {
                PressedKey = Console.ReadKey(true);
                inChoice = ChoisingClass(warrior, sorcerer, slayer, archer, PressedKey, ref chosenClass);
            }


            switch (chosenClass)
            {
                case 1:
                    SlowWrite("Выбран Воин!");
                    break;
                case 2:
                    SlowWrite("Выбран Маг!");
                    break;
                case 3:
                    SlowWrite("Выбран Убийца!");
                    break;
                case 4:
                    SlowWrite("Выбран Лучник!");
                    break;
                default:
                    Console.WriteLine("Шота сломалось :(");
                    break;
            }
            return chosenClass;
        }


        public static void SlowWrite(string str, ConsoleColor textColor = ConsoleColor.Yellow, bool needClear = true, int speed = 1, string teller = null, bool ableToSkip = true, bool tech = false)
        {
            object[] parameters = { str, textColor, needClear, speed, teller };
            writeThread t1 = new writeThread(parameters);
            bool inWhile = true;
            ConsoleKey pressedKey;
            if (ableToSkip)
            {
                do
                {
                    pressedKey = Console.ReadKey(true).Key;
                    if (pressedKey == ConsoleKey.Enter || pressedKey == ConsoleKey.Spacebar)
                        inWhile = false;
                } while (inWhile); //   || !t1.thread.IsAlive


                t1.thread.Abort();


            }
            else
            {
                while (t1.thread.IsAlive)
                {
                    Thread.Sleep(0);

                }
                if (!tech)
                    Console.ReadKey(true);


            }

        }


        static void ShowClassList(PlayerClass warrior, PlayerClass sorcerer, PlayerClass slayer, PlayerClass archer)
        {
            Console.Clear();
            SlowWrite($"1. {warrior.Name}", ConsoleColor.Red, false, speed: 1, ableToSkip: false, tech: true);
            SlowWrite($"2. {sorcerer.Name}", ConsoleColor.Blue, false, speed: 1, ableToSkip: false, tech: true);
            SlowWrite($"3. {slayer.Name}", ConsoleColor.DarkRed, false, speed: 1, ableToSkip: false, tech: true);
            SlowWrite($"4. {archer.Name}", ConsoleColor.Green, false, speed: 1, ableToSkip: false, tech: true);
            Console.WriteLine();
            SlowWrite("Нажми соответствующую цифру для получения подробной информации о классе.", needClear: false, speed: 0, ableToSkip: false, tech: true);
        }

        static bool ChoisingClass(PlayerClass warrior, PlayerClass sorcerer, PlayerClass slayer, PlayerClass archer, ConsoleKeyInfo pressedKey, ref int chosenClass)
        {

            string text = "\n   Нажмите Enter для выбора класса или любую другую клавишу чтобы вернуться к списку классов.";
            bool inChoice = true;
            switch (pressedKey.Key)
            {
                case ConsoleKey.D1:
                    warrior.ShowStats(false);
                    SlowWrite(text, needClear: false, speed: 0, ableToSkip: false, tech: true);
                    break;
                case ConsoleKey.D2:
                    sorcerer.ShowStats(false);
                    SlowWrite(text, needClear: false, speed: 0, ableToSkip: false, tech: true);
                    break;
                case ConsoleKey.D3:
                    slayer.ShowStats(false);
                    SlowWrite(text, needClear: false, speed: 0, ableToSkip: false, tech: true);
                    break;
                case ConsoleKey.D4:
                    archer.ShowStats(false);
                    SlowWrite(text, needClear: false, speed: 0, ableToSkip: false, tech: true);
                    break;
                default:
                    Console.Clear();
                    ShowClassList(warrior, sorcerer, slayer, archer);
                    return inChoice;
            }
            ConsoleKey secondKey = Console.ReadKey(true).Key;
            switch (secondKey)
            {
                case ConsoleKey.Enter:
                    if (pressedKey.Key == ConsoleKey.D1)
                    {
                        chosenClass = 1;
                        inChoice = false;
                        return inChoice;
                    }
                    else if (pressedKey.Key == ConsoleKey.D2)
                    {
                        chosenClass = 2;
                        inChoice = false;
                        return inChoice;
                    }
                    else if (pressedKey.Key == ConsoleKey.D3)
                    {
                        chosenClass = 3;
                        inChoice = false;
                        return inChoice;
                    }
                    else if (pressedKey.Key == ConsoleKey.D4)
                    {
                        chosenClass = 4;
                        inChoice = false;
                        return inChoice;
                    }
                    else
                    {
                        Console.Clear();
                        ShowClassList(warrior, sorcerer, slayer, archer);
                        break;
                    }
                default:
                    Console.Clear();
                    ShowClassList(warrior, sorcerer, slayer, archer);
                    break;
            }

            return inChoice;

        }


        internal class PlayerClass
        {

            public string Name { get; private set; }

            public string NickName { get; private set; }
            public int HP { get; private set; }
            public int MP { get; private set; }
            public int AtcRange { get; private set; }
            public int Level { get; private set; }
            public int EXP { get; private set; }
            public int MaxHP { get; private set; }
            public int MaxMP { get; private set; }

            public int Gold {  get; private set; }
            public Weapon Weapon { get; private set; }
            public PlayerActiveAbility ActiveAbility { get; private set; }
            public PlayerPassiveAbility PassiveAbility { get; private set; }

            public Inventory Inventory { get; private set; }
            public PlayerClass(string name, string nickName, int hp, int mp, int atcRange, int level, int exp, Weapon weapon, PlayerActiveAbility activeAbility, PlayerPassiveAbility passiveAbility)
            {
                Name = name;
                NickName = nickName;
                HP = hp;
                MP = mp;
                AtcRange = atcRange;
                Level = level;
                EXP = exp;
                Weapon = weapon;
                MaxHP = HP;
                MaxMP = MP;
                ActiveAbility = activeAbility; 
                PassiveAbility = passiveAbility;
                Gold = 10;
                Inventory = new Inventory();

            }

            public virtual void ShowStats(bool showPotions = true)
            {
                Console.Clear();
                SlowWrite($"{Name}\n", ConsoleColor.Yellow, false, speed: 0, ableToSkip: false, tech: true);
                SlowWrite($"Здоровье: {HP}", ConsoleColor.Yellow, false, speed: 0, ableToSkip: false, tech: true);
                SlowWrite($"Мана: {MP}", ConsoleColor.Yellow, false, speed: 0, ableToSkip: false, tech: true);
                if (AtcRange < 1)
                    SlowWrite("Ближний бой", ConsoleColor.Yellow, false, speed: 0, ableToSkip: false, tech: true);
                else
                    SlowWrite("Дальний бой", ConsoleColor.Yellow, false, speed: 0, ableToSkip: false, tech: true);

                Console.WriteLine();
                SlowWrite($"Снаряжение: {Weapon.Name}  Урон: {Weapon.Damage}", ConsoleColor.Yellow, false, speed: 0, ableToSkip: false, tech: true);

                Console.WriteLine();

                SlowWrite(ActiveAbility.Name, needClear: false, speed: 0, ableToSkip: false, tech: true);
                SlowWrite(ActiveAbility.Description, needClear: false, speed: 0, ableToSkip: false, tech: true);
                Console.WriteLine();
                SlowWrite(PassiveAbility.Name, needClear: false, speed: 0, ableToSkip: false, tech: true);
                SlowWrite(PassiveAbility.Description, needClear: false, speed: 0, ableToSkip: false, tech: true);
                Console.WriteLine();

                if (showPotions)
                {
                    DrawBar(3, 20, HP, MaxHP, ConsoleColor.Green);
                    DrawBar(3, 22, MP, MaxMP, ConsoleColor.Blue);
                }



            }

            public void GainExp(int amountEXP)
            {
                EXP += amountEXP;
                while (EXP >= 10)
                {
                    EXP -= 10;
                    LevelUp();
                }
            }
            private void LevelUp()
            {
                Level += 1;
                HP += 5;
            }

            public void GetDamage(int dealtDamage)
            {
                HP -= dealtDamage;
            }

            public void RestoreHP(int RestoredHP)
            {
                HP += RestoredHP;
                if (HP > MaxHP)
                    HP = MaxHP;
            }

            public void SpendMana()
            {
                MP -= ActiveAbility.ManaCost;
            }

            public void RestoreMP(int restoreValue)
            {
                MP += restoreValue;
                if (MP > MaxMP)
                    MP = MaxMP;
            }

            public bool SpendGold(int goldToSpend)
            {
                if (Gold >= goldToSpend)
                {
                    Gold -= goldToSpend;
                    return true;
                }
                else
                    return false;
            }

            public void getGold(int goldToGet)
            {
                Gold += goldToGet;
            }


        }

        internal class PlayerClassFactory
        {
            public static PlayerClass CreateInstance(int value, string nickName)
            {
                PlayerActiveAbility ActiveAbility;
                PlayerPassiveAbility PassiveAbility;
                Weapon weapon;
                if (value == 1)
                {
                    ActiveAbility = new PlayerActiveAbility("Изнуряющий удар", $"Воин наносит 15 урона противнику, уменьшая наносимый им урон на 30% на 2 хода. Нанесение урона изнуренному противнику оглушает его.", 15, 6);
                    PassiveAbility = new PlayerPassiveAbility("Нарастающая ярость", "Течение битвы ожесточает воина, увеличивая наносимый им урон на 1 за ход.");
                    weapon = new Weapon("Стандартный меч", 10, "Улучшеный меч", 12);
                    return new PlayerClass("Воин", nickName, 120, 20, 0, 1, 0, weapon, ActiveAbility, PassiveAbility);
                }
                else if (value == 2)
                {
                    ActiveAbility = new PlayerActiveAbility("Ледяное копье", "Маг поражает противника ледяным копьем, которое наносит 18 урона и замораживает цель на 1 ход.", 18, 15);
                    PassiveAbility = new PlayerPassiveAbility("Благословение богов", "Боги направляют руку мага, что может значительно усилить его заклинания.");
                    weapon = new Weapon("Стандартный посох", 15, "Улучшеный посох", 17);
                    return new PlayerClass("Маг", nickName, 65, 75, 1, 1, 1, weapon, ActiveAbility, PassiveAbility);
                }
                else if (value == 3)
                {
                    ActiveAbility = new PlayerActiveAbility("Казнь", "Убийца наносит выверенный удар клинком (23 урона). Умение может мгновенно убить противника, если его здоровье ниже 30%.", 23, 11);
                    PassiveAbility = new PlayerPassiveAbility("Ловкость", "Ловкость убийцы позволяет ему уклоняться от ударов противника с вероятностью 15%.");
                    weapon = new Weapon("Стандартный кинжал", 17, "Улучшеный кинжал", 19);
                    return new PlayerClass("Убийца", nickName, 90, 30, 0, 1, 0, weapon, ActiveAbility, PassiveAbility);
                }
                else if (value == 4)
                {
                    ActiveAbility = new PlayerActiveAbility("Отступление", "Лучник разрывает дистанцию с противником на 1, нанося 8 урона и обездвиживая его на 1 ход", 8, 9);
                    PassiveAbility = new PlayerPassiveAbility("Меткий глаз", "Меткость лучника позволяет ему наносить дополнительные 3 урона удаленным целям, а также почти никогда не промахиваться.");
                    weapon = new Weapon("Стандартный лук", 12, "Улучшеный лук", 14);
                    return new PlayerClass("Лучник", nickName, 80, 20, 2, 1, 0, weapon, ActiveAbility, PassiveAbility);
                }
                else { throw new ArgumentException("Неверный класс"); }

            }

        }

        internal class Weapon
        {
            public string Name { get; private set; }
            public int Damage { get; private set; }
            public string UpgradedName { get; private set; }
            public int UpgradedDamage { get; private set; }
            public Weapon(string weaponName, int weaponDamage, string upgradedName, int upgradedDamage)
            {
                Name = weaponName;
                Damage = weaponDamage;
                UpgradedName = upgradedName;
                UpgradedDamage = upgradedDamage;
            }

            public void UpgradeWeapon()
            {
                Name = UpgradedName;
                Damage = UpgradedDamage;
                SlowWrite($"Новое оружие - {Name}", needClear: true, ableToSkip: true);
            }
        }


        internal class PlayerBaseAbility
        {
            public string Name { get; private set; }
            public string Description { get; private set; }
            public PlayerBaseAbility(string name, string description)
            {
                Name = name;
                Description = description;

            }
        }

        internal class PlayerActiveAbility : PlayerBaseAbility
        {
            public int Damage { get; private set; }

            public int ManaCost { get; private set; }

            public PlayerActiveAbility(string name, string description, int damage, int manaCost) : base(name, description)
            {
                Damage = damage;
                ManaCost = manaCost;
            }
        }

        internal class PlayerPassiveAbility : PlayerBaseAbility
        {
            public PlayerPassiveAbility(string name, string description) : base(name, description)
            {

            }
        }


        internal class Inventory
        {

            public List<Item> playerItems = new List<Item>() { };

            public void CountPageItems(ref int itemsPerPage, ref int integer, ref int pageCounter)
            {

                Console.Clear();

                    for (int i = (pageCounter - 1) * itemsPerPage; i < playerItems.Count && i < (pageCounter - 1) * itemsPerPage + 9; i++)
                    {
/*                        if (integer / pageCounter >= itemsPerPage)
                        {
                            break;
                        }*/

                        string itemName = playerItems[i].Name;
                        if (playerItems[i].AbleToUse)
                            itemName += $": {playerItems[i].AmountOfItems}";
                        integer++;
                        SlowWrite($"{i + 1 - (pageCounter - 1) * itemsPerPage}. {itemName}", needClear: false, speed: 0, ableToSkip: false, tech: true, textColor: playerItems[i].TextColor);

                    }
            }
            public void Open(ref PlayerClass player)
            {
                int itemsPerPage = 9;
                int integer = 0;
                int pageCounter = 1;
                bool inInventory = true;
                while (inInventory)
                {
                    CountPageItems(ref itemsPerPage, ref integer, ref pageCounter);

                    List<ConsoleKey> actions = NumberOfActions(integer);

                    if (playerItems.Count > itemsPerPage && (pageCounter * itemsPerPage) < playerItems.Count)
                    {
                        SlowWrite($"\n   →. Вперед", needClear: false, speed: 0, ableToSkip: false, tech: true);
                        actions.Add(ConsoleKey.RightArrow);
                    }

                    if (playerItems.Count > itemsPerPage && pageCounter > 1)
                    {
                        SlowWrite($"\n   ←. Назад", needClear: false, speed: 0, ableToSkip: false, tech: true);
                        actions.Add(ConsoleKey.LeftArrow);
                    }
                        

                    ConsoleKey playerAction = GetPlayerAction(actions, false, false, true);
                    Console.Clear();
                    int chosenItem;
                    switch (playerAction)
                    {
                        case ConsoleKey.D1:
                            chosenItem = 1 + (pageCounter - 1) * itemsPerPage;
                            break;
                        case ConsoleKey.D2:
                            chosenItem = 2 + (pageCounter - 1) * itemsPerPage;
                            break;
                        case ConsoleKey.D3:
                            chosenItem = 3 + (pageCounter - 1) * itemsPerPage;
                            break;
                        case ConsoleKey.D4:
                            chosenItem = 4 + (pageCounter - 1) * itemsPerPage;
                            break;
                        case ConsoleKey.D5:
                            chosenItem = 5 + (pageCounter - 1) * itemsPerPage;
                            break;
                        case ConsoleKey.D6:
                            chosenItem = 6 + (pageCounter - 1) * itemsPerPage;
                            break;
                        case ConsoleKey.D7:
                            chosenItem = 7 + (pageCounter - 1) * itemsPerPage;
                            break;
                        case ConsoleKey.D8:
                            chosenItem = 8 + (pageCounter - 1) * itemsPerPage;
                            break;
                        case ConsoleKey.D9:
                            chosenItem = 9 + (pageCounter - 1) * itemsPerPage;
                            break;

                        case ConsoleKey.RightArrow:
                            if (playerItems.Count > itemsPerPage && (pageCounter * itemsPerPage) < playerItems.Count)
                                pageCounter += 1; 
                            else
                                inInventory = false;
                            chosenItem = 0;
                            break;

                        case ConsoleKey.LeftArrow:
                            if (playerItems.Count > itemsPerPage && pageCounter > 1)
                                pageCounter -= 1;
                            else
                                inInventory = false;
                            chosenItem = 0;
                            break;

                        default:
                            chosenItem = 0;
                            inInventory = false;
                            break;
                    }
                    if (chosenItem != 0)
                    {
                        try
                        {
                            string itemName = playerItems[chosenItem - 1].Name;
                            string itemDescription = playerItems[chosenItem - 1].Description;

                            SlowWrite($"{itemName}", speed: 0, needClear: false, ableToSkip: false, tech: true);
                            Console.WriteLine();
                            SlowWrite($"{itemDescription}", speed: 0, needClear: false, ableToSkip: false, tech: true);
                            if (playerItems[chosenItem - 1].AbleToUse && playerItems[chosenItem - 1].AmountOfItems > 0)
                            {
                                Console.WriteLine();
                                SlowWrite($"Enter - Использовать {itemName}", speed: 0, needClear: false, ableToSkip: false, tech: true);

                                List<ConsoleKey> actions2 = new List<ConsoleKey> { ConsoleKey.Enter };
                                ConsoleKey playerAction2 = GetPlayerAction(actions2, false, false, true);
                                switch (playerAction2)
                                {
                                    case ConsoleKey.Enter:
                                        if (playerItems[chosenItem - 1].GetType() == typeof(HealingPotion))
                                        {
                                            if (player.HP >= player.MaxHP)
                                                SlowWrite("У вас максимум здоровья.");
                                            else
                                            {
                                                HealingPotion healingPotion = (HealingPotion)player.Inventory.playerItems.Find(item => item.Name == "Зелье лечения").UseItem();
                                                player.RestoreHP(healingPotion.RestoreValue);
                                                SlowWrite("Использовано зелье лечения.", needClear: true, speed: 0, ableToSkip: false, tech: true);
                                            }
                                        }
                                        else
                                        {
                                            if (player.MP >= player.MaxMP)
                                                SlowWrite("У вас максимум маны.");
                                            else
                                            {
                                                ManaPotion manaPotion = (ManaPotion)player.Inventory.playerItems.Find(item => item.Name == "Зелье маны").UseItem();
                                                player.RestoreMP(manaPotion.RestoreValue);
                                                SlowWrite("Использовано зелье маны.", needClear: true, speed: 0, ableToSkip: false, tech: true);

                                            }

                                        }

                                        break;
                                    default: break;
                                }
                                Console.Clear();
                            }
                            else
                                Console.ReadKey(true);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    

                }
            }

            public void ShowPotions(bool inFight = false)
            {
                int potionCounter = 2;

                int yPos;
                if (inFight)
                    yPos = 2;
                else
                    yPos = 25;
                foreach (Item item in playerItems)
                {
                    if (potionCounter <= 0)
                        break;
                    potionCounter--;
                    Console.SetCursorPosition(3, yPos);
                    if (item.GetType() == typeof(HealingPotion))
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (item.GetType() == typeof(ManaPotion))
                        Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{item.Name}: {item.AmountOfItems}\n");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    yPos++;
                }
                
            }

            internal void AppendItem(Item item)
            {
                playerItems.Add(item);
            }

            internal void RemoveItem(Item item)
            {
                playerItems.Remove(item);
            }


        }

        internal abstract class Item
        {
            public string Name { get; protected set; }

            public string Description { get; protected set; }

            public int AmountOfItems {  get; private set; }

            public bool AbleToUse {  get; protected set; }

            public ConsoleColor TextColor { get; protected set; }
            public Item(string name, string description, bool ableToUse, ConsoleColor textColor)
            {
                Name = name;
                AmountOfItems = 0;
                Description = description;
                AbleToUse = ableToUse;
                TextColor = textColor;
            }

            public void AddItem()
            {
                AmountOfItems++;
            }
            
            public Item UseItem()
            {
                if ( AmountOfItems <= 0 )
                    return this;
                AmountOfItems--;
                return this;
            }

            public ConsoleColor GetItemColor(Item item)
            {
                ConsoleColor color = ConsoleColor.Yellow;
                if (item.GetType() == typeof(HealingPotion))
                    color = ConsoleColor.Green;
                else if (item.GetType() == typeof(ManaPotion))
                    color = ConsoleColor.Blue;
                return color;
            }
        }

        internal class CollectableItem: Item
        {
            public char ItemChar { get; private set; }

            public int ItemCoordinateX { get; private set; }

            public int ItemCoordinateY { get; private set; }

            public ConsoleColor ItemColor { get; private set; }

            public CollectableItem(char itemChar, int itemCoordinateX, int itemCoordinateY, string name, string description, ConsoleColor itemColor = ConsoleColor.Magenta, bool ableToUse = false, ConsoleColor textColor = ConsoleColor.Yellow) : base(name, description, ableToUse, textColor)
            {
                ItemChar = itemChar;
                ItemCoordinateX = itemCoordinateX;
                ItemCoordinateY = itemCoordinateY;
                ItemColor = itemColor;
            }
        }

        internal class QuestItem: Item
        {
            public QuestItem(string name, string description, bool ableToUse = false, ConsoleColor textColor = ConsoleColor.Yellow) : base(name, description, ableToUse, textColor)
            {

            }
        }

        internal abstract class Potion : Item
        {
            public int RestoreValue { get; protected set; }
            public Potion(string name, string description, bool ableToUse, ConsoleColor textColor) : base(name, description, ableToUse, textColor)
            {

            }

        }

        internal class HealingPotion : Potion
        {
            public HealingPotion(string name = "", string description = "", bool ableToUse = true, ConsoleColor textColor = ConsoleColor.Green) : base(name, description, ableToUse, textColor)
            {
                Name = "Зелье лечения";
                Description = "Восстанавливает 20 ед. здоровья.";
                RestoreValue = 20;
            }
        }

        internal class ManaPotion : Potion
        {
            public ManaPotion(string name = "", string description = "", bool ableToUse = true, ConsoleColor textColor = ConsoleColor.Blue) : base(name, description, ableToUse, textColor)
            {
                Name = "Зелье маны";
                Description = "Восстанавливает 10 ед. маны.";
                RestoreValue = 10;
            }
        }


        internal class Story
        {
            public List<Quest> Quests = new List<Quest>();

            public bool HelpYourHome = false;

            public bool FirstShopVisit = false;

            public bool FirstVillageVisit = false;

            public bool ArtefactCollected = false;

            public bool FirstVisitHeadman = false;

            public bool SecondHeadmanVisit = false;

            public bool FoundedSteps = false;

            public bool FoundedConvoy = false;

            public bool FreeVillagers = false;

            public int BoughtPotions = 0;

            public bool WeaponUpgraded = false;






            public Quest FirstVisitHomeQuest;


            public Quest SealMainQuest;


            public Quest HeadmanPersonalQuest;


            public Quest TraderQuest;
   

            public Quest FriendQuest;


            public Quest BossfightQuest;
   

            public Quest HeadmanMainQuest;
        

            public Quest TempleQuest;
           

            public Quest HerbalistMainQuest;

            public bool SpawnNearHome = false;


            public Story()
            {

                string[] descriptions;

                descriptions = new string[] {"FirstVisitHomeQuest description" };
                FirstVisitHomeQuest = new Quest("FirstVisitHomeQuest", descriptions);

                descriptions = new string[] { "FirstVisitHomeQuest description" };
                FirstVisitHomeQuest = new Quest("FirstVisitHomeQuest", descriptions);
                Quests.Add(FirstVisitHomeQuest);

                descriptions = new string[] { "предмет был спрятан, и его нужно найти. Предмет охраняется стражем, стражу нужно дать по голове." };
                SealMainQuest = new Quest("Прогрессия ослабления печати посредством нахождения предмета", descriptions);
                Quests.Add(SealMainQuest);

                descriptions = new string[] { "Помнишь, я тебе рассказывал о пропаже вещей, в которых были замешаны эти культисты? В один из таких налетов у меня пропала фамильная реликвия, которая осталась у меня от покойной жены. Прошу, найди эту реликвию, она очень важна для меня. " };
                HeadmanPersonalQuest = new Quest("Личная просьба старосты", descriptions);
                Quests.Add(HeadmanPersonalQuest);

                descriptions = new string[] { "Изучить обломки обозов около моста, недалеко от деревни.", "Проследовать по следам ведущим в чащу.", "Проникнуть в лагерь и вернуться припасы.\n   Убить главу лагеря (необяз.)", "Вернуться к торговцу за наградой." };
                TraderQuest = new Quest("Помощь торговцу", descriptions);
                Quests.Add(TraderQuest);

                descriptions = new string[] { "@Описание@" };
                FriendQuest = new Quest("Помощь старому другу", descriptions);
                Quests.Add(FriendQuest);

                descriptions = new string[] { "Какое-то время назад у нас начали пропадать сначала скот, и мелкое имущество, но однажды утром, мы не досчитались нескольких наших жителей. Я подозреваю, что в этом замешаны культисты, которые недавно объявились в наших краях. Прошу тебя, проникни в лагерь этих культистов и спаси наших людей!" };
                HeadmanMainQuest = new Quest("Спасение жителей деревни", descriptions);
                Quests.Add(HeadmanMainQuest);

                descriptions = new string[] { "@Описание@" };
                TempleQuest = new Quest("Помощь местной церкви", descriptions);
                Quests.Add(TempleQuest);

                descriptions = new string[] { "@Описание@" };
                HerbalistMainQuest = new Quest("herbalistMainQuest", descriptions);
                Quests.Add(HerbalistMainQuest);

                descriptions = new string[] { "@Описание@" };
                BossfightQuest = new Quest("Главная угроза", descriptions);
                Quests.Add(BossfightQuest);

            }

            public void ShowJournal()
            {
                bool inJournal = true;
                while (inJournal)
                {
                    List<Quest> activeQuests = new List<Quest>();
                    Console.Clear();

                    int integer = 0;
                    foreach (Quest quest in Quests)
                    {

                            if (quest.QuestStarted && !quest.QuestPassed)  //if queest started and not passed
                            {
                                integer++;
                                SlowWrite($"{integer}. {quest.Name}", needClear: false, speed: 0, ableToSkip: false, tech: true);
                                activeQuests.Add(quest);
                            }
                    }
                    List<ConsoleKey> actions = NumberOfActions(integer);
                    ConsoleKey playerAction = GetPlayerAction(actions, false, false, true);
                    Console.Clear();
                    int chosenQuest;
                    switch (playerAction)
                    {
                        case ConsoleKey.D1:
                            chosenQuest = 1;
                            break;
                        case ConsoleKey.D2:
                            chosenQuest = 2;
                            break;
                        case ConsoleKey.D3:
                            chosenQuest = 3;
                            break;
                        case ConsoleKey.D4:
                            chosenQuest = 4;
                            break;
                        case ConsoleKey.D5:
                            chosenQuest = 5;
                            break;
                        case ConsoleKey.D6:
                            chosenQuest = 6;
                            break;
                        case ConsoleKey.D7:
                            chosenQuest = 7;
                            break;
                        case ConsoleKey.D8:
                            chosenQuest = 8;
                            break;
                        case ConsoleKey.D9:
                            chosenQuest = 9;
                            break;
                        default: 
                            chosenQuest = 0;
                            inJournal = false;
                            break;
                    }
                    try
                    {
                        string questName = activeQuests[chosenQuest - 1].Name;
                        string questDescription = activeQuests[chosenQuest - 1].Descriptions[activeQuests[chosenQuest - 1].DescriptionCounter];
                        
                        SlowWrite($"{questName}", speed: 0, needClear: false, ableToSkip: false, tech: true);
                        Console.WriteLine();
                        SlowWrite($"{questDescription}", speed: 0, needClear: false, ableToSkip: false, tech: true);
                        Console.ReadKey(true);
                    }
                    catch 
                    {
                        continue;
                    }
                    
                }
            }
                

        }

        public class Quest
        {
            public string[] Descriptions { get; private set; }
            public string Name { get; private set; }
            public bool QuestStarted { get; private set; }
            public bool QuestCompleted { get; private set; }
            public bool QuestPassed { get; private set; }
            public int DescriptionCounter { get; private set; }

            public Quest(string name, string[] descriptions)
            {
                Name = name;
                QuestStarted = false;
                QuestCompleted = false;
                QuestPassed = false;
                Descriptions = descriptions;
                DescriptionCounter = 0;
            }

            public void NextDescription()
            {
                if (DescriptionCounter < Descriptions.Length - 1)
                {
                    SlowWrite($"Задание \"{Name}\" обновлено.");
                    DescriptionCounter++;
                }
                
            }

            public void StartQuest()
            {
                SlowWrite($"Задание \"{Name}\" принято.");
                QuestStarted = true;
            }

            public void CompleteQuest()
            {
                SlowWrite($"Подзадача \"{Name}\" выполнена.");
                QuestCompleted = true;
            }

            public void PassQuest()
            {
                SlowWrite($"Задание \"{Name}\" выполнено.");
                QuestPassed = true;
            }
        }
        enum intActions
        {
            one = 1, two, three, four, five, six, seven, eight, nine
        }
        static internal List<ConsoleKey> NumberOfActions (int numberOfActions)
        {
            List<ConsoleKey> actions = new List<ConsoleKey>();
            ConsoleKey action;

            for (int i = 1; i < numberOfActions + 1; i++)
            {
                switch(i)
                {
                    case (int)intActions.one: action = ConsoleKey.D1; break;
                    case (int)intActions.two: action = ConsoleKey.D2; break;
                    case (int)intActions.three: action = ConsoleKey.D3; break;
                    case (int)intActions.four: action = ConsoleKey.D4; break;
                    case (int)intActions.five: action = ConsoleKey.D5; break;
                    case (int)intActions.six: action = ConsoleKey.D6; break;
                    case (int)intActions.seven: action = ConsoleKey.D7; break;
                    case (int)intActions.eight: action = ConsoleKey.D8; break;
                    case (int)intActions.nine: action = ConsoleKey.D9; break;
                    default: action = ConsoleKey.End; break;

                }
                actions.Add(action);
            }

            return actions;
        }

        
    }
}






/*⠀⡐⢠⠿⣝⠠⡝⡆⣻⢿⡓⢮⢓⣯⡗⣻⢿⣿⣻⣟⡿⣜⠡⡞⠥⣾⡻⣽⠣⣏⣿⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⡶⡜⢿⣿⣿⣿⣿⣿⣿⣿⠿⠿⣛⣋⣭⡽⠯⡝⣧⡒⢶⡬⣭⣍⡭⣉⢛⠻⠿⣿⡿⣞⡽⣫⠽⣿⣿⣿⣿⣿⣻⠌⣿⢿⡆⢱⡃⣿⣻⢻⣭⣛⢷⣣⠐⡀⢂⠿⣝⡯⣟⢿⡖⠰⡀⢂⡐⢠⠂
⠐⡈⣜⣻⠬⢐⢧⢃⡷⣯⢃⡯⣼⢷⡍⣿⣻⣾⡽⣞⡳⣌⠳⡜⢱⡷⣟⡶⠁⣻⡖⠀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣯⢷⡽⣎⢻⣿⣿⠿⣛⣭⣴⣾⣿⣿⣿⣽⣽⣫⣷⣚⣛⣛⣛⣶⡭⠞⣭⢮⣕⠣⢆⠌⡉⠶⣧⢿⣿⣿⣿⣿⣿⣧⢋⡾⣽⡖⢨⠖⣽⣏⢿⡆⣟⡮⣗⠢⠌⡐⢸⡧⢿⡼⢧⣿⡁⢽⡀⢐⠠⠉⡔⠠
⠐⣠⢗⣿⡘⣸⢢⢃⣟⡞⡸⢆⡿⢾⡥⣟⡷⣯⢿⣝⡳⢂⠷⡘⢸⣽⣺⡅⢰⣟⡇⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢸⣗⣻⣼⠦⠛⣑⣻⣿⣿⣯⢯⣛⣛⠛⣭⢶⣿⣿⣿⣿⢿⣿⡿⣿⡿⣶⣯⡜⠯⢦⣁⠪⡐⢈⠙⢿⣿⣿⣿⣿⢺⡅⣳⢯⣟⠠⣛⢼⡞⣿⡎⢷⡳⢯⢘⡰⠀⡅⣟⢧⣻⢳⣎⢧⠘⣼⡀⢂⠡⠀⡅
⠐⡴⣻⣧⡓⡼⢸⠰⣯⠣⡝⣸⣻⢯⢴⡿⣽⣻⣞⡾⡱⣉⠶⠁⣿⢮⡷⠁⣼⣹⠆⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢡⣟⡾⠇⢁⡸⢻⣿⣿⣫⡥⣲⣿⠚⣡⣾⡵⣦⠻⣿⣿⣯⣳⣬⡛⢧⡟⡵⢦⡹⣙⠆⡑⢂⠈⢧⡈⠤⠙⣿⣿⣿⢼⠢⣽⣳⢿⡄⢭⢺⡽⣶⢏⡳⣝⡻⡌⡇⠥⠐⡸⡧⣏⡷⣹⣎⢇⢰⡯⣄⠂⠡⠐
⢠⢻⣽⢶⢡⡛⡴⢸⣝⢲⡁⣿⡽⣯⢺⣽⣻⢷⣻⢎⡵⢨⠆⢸⡿⡽⡏⢀⡿⡽⠀⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢣⣾⠏⣠⣾⢟⣥⣾⣿⣿⠏⣴⣿⣯⣾⣿⡷⣿⡽⣧⡙⢿⣮⠛⠯⣟⣶⣍⡹⢖⡣⣍⠚⠤⠀⣠⣤⣅⠈⠒⡈⢿⣿⢸⢣⢖⣯⣟⡆⢸⡸⣗⢯⣳⣙⠮⣷⢱⡙⣆⡁⠆⡿⣜⢷⣣⡟⢮⣂⢷⡞⣌⠡⢈
⡰⣟⡾⣏⢲⡱⢊⡽⣊⠖⣸⢷⣻⢧⢻⣷⣻⢯⣷⠫⡔⢣⢀⣿⢿⣽⠁⣼⣟⡇⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣡⡿⢡⣾⣿⢫⣾⣿⡟⣹⢏⣾⣿⣿⣿⣿⣾⣿⣿⣿⣏⠻⡄⠨⣟⠮⠳⣤⣍⣙⢧⠲⣌⠱⣈⠀⣿⣿⣎⢣⠑⡀⠌⣿⢸⡇⣞⣿⣾⣟⠰⣃⣿⣛⡦⢭⢻⣜⡣⡜⢦⢐⠂⢹⡞⣧⢳⡻⣝⢦⡙⣿⣬⣒⠠
⢰⡿⣽⠺⣔⢣⠣⣝⡰⠃⣾⣯⣟⡇⣿⢷⣯⢿⣞⠳⣌⠃⣸⡟⣾⡇⢀⡿⣎⡇⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⡋⣰⡟⣰⣿⣿⣿⣸⣿⣿⢱⢏⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⢻⡄⠘⣿⣟⣿⡜⣞⢎⡳⢌⡱⢠⢘⣿⣿⣿⡆⢧⠱⡀⠘⠆⣟⡰⣿⣾⢿⡐⢧⣻⣽⡎⡷⣩⢖⡧⣏⠼⣆⠱⠀⢻⣜⣯⢳⢯⣞⡵⢚⣧⢿⣰
⣫⣷⣛⡳⣜⢣⢃⡇⢞⢡⣿⡾⣽⠎⣿⢯⡾⣟⣮⠓⡬⢐⣿⢽⡿⠀⣼⢳⡏⠆⣼⣿⠿⠛⠛⠉⠉⠉⠉⠀⠉⠉⠉⠉⠛⠛⠻⠿⢿⣿⣻⣵⣿⣵⣯⢁⣿⣣⣿⣿⢿⣿⣷⢧⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢹⣼⣿⡀⠈⢿⣾⡹⢞⣬⡑⢎⡰⠁⢸⡿⣿⣿⢿⡀⡄⢣⠀⠀⣿⠰⣿⡿⣿⠆⢣⢿⣳⡏⡵⢣⢯⣷⢜⠲⣍⡆⢡⠘⡾⣼⢫⣟⣼⣹⢚⡼⣻⣽
⢽⣟⣯⠕⣎⢇⡚⢬⠃⣾⣿⣿⣿⣹⣿⣿⣿⣿⡎⡕⠂⣼⣏⣾⠇⢀⣟⡿⢸⠀⠁⠀⢀⠀⠄⡀⠄⠠⠐⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⠿⣿⡟⣼⣯⣿⡿⢹⣯⣿⡿⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢹⡟⣷⢠⠘⣧⡹⣞⢤⡛⠤⢃⠀⠘⣧⡻⣿⡜⣇⠠⠱⠀⢂⣯⠓⣿⡿⣿⣏⢸⡹⣿⢜⡳⣍⢶⣻⡜⣇⡳⣞⡠⠂⢹⣞⡳⣮⢳⡏⣿⢰⡟⣾
⢯⣟⣧⠻⡜⢦⠙⣆⢣⣿⣿⣯⣿⢼⣿⣿⣿⣟⠧⡸⢰⣿⢹⡿⠀⣼⠟⣡⠇⢠⠂⡅⠢⢌⠰⠠⠌⠤⡁⠂⠀⠀⠀⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢁⣿⣿⣿⢣⣿⣿⣿⡇⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣿⣇⠀⠸⣳⢚⠦⢙⣠⠆⠄⠀⣿⣿⣿⣇⢻⢰⣈⠇⠀⢫⡝⣾⢿⣿⣿⡀⡟⣿⠎⣵⢊⢧⢻⡧⢧⢱⢫⢧⡡⠘⣮⢷⣹⡳⣟⢾⣣⢟⣽
⢯⣟⣎⠳⡝⢦⡙⠆⣾⣿⣻⣿⣏⣿⣿⣿⣿⢏⠖⡁⣼⢧⣿⠇⠐⣃⠞⡥⡇⡰⡩⡜⣱⠎⣢⠙⣈⣴⠢⢁⢀⠂⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡈⡿⣿⡿⢸⣿⣿⣿⢡⣿⡹⣿⣿⣿⣧⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⠀⣁⣩⢴⡫⢂⡜⠂⠀⢸⣿⣿⣿⡈⡇⡎⢖⢸⠈⢖⣹⣿⣿⣷⡃⢭⣻⡝⢦⣋⢮⡹⣿⡸⡆⢋⡷⣱⠂⢸⣯⣷⣟⣾⣻⠼⣏⣾
⢯⡗⣯⢹⡙⡦⣙⢸⣿⣿⣿⣿⡧⣿⣿⣿⣟⢯⡘⢰⡏⣼⡟⢀⠰⣍⡟⡼⠁⡖⣱⡽⢃⡜⢡⠞⡼⢂⠀⡆⠄⡌⠀⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⡇⣿⡇⢿⣿⣿⣿⢸⣿⣿⣮⢻⣿⣿⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⣧⠀⠸⡵⡎⢡⠎⡸⠁⠠⠘⣿⣿⣿⡇⢱⢩⢎⠈⢐⠸⢼⣟⣿⣿⣳⠨⣳⡍⢶⢩⢖⡹⣞⣧⢳⠘⡘⡷⡩⠄⢳⣻⢾⣽⣻⢿⣿⣼
⢯⡟⣼⢣⡝⡔⢣⣾⣿⣿⣽⣿⡗⣿⣿⣿⣟⢆⡱⣿⢱⣿⠁⡌⡸⣜⡞⡱⠘⣡⡟⠇⡞⢠⡟⠼⢁⠂⠸⣜⠰⠀⠌⠀⠀⠀⠀⠀⠀⠀⠀⣆⠀⠀⠀⡇⣿⡇⣻⣿⣿⣿⢸⣿⢸⣿⣏⣿⣿⡀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⣿⣿⠀⢈⠳⡐⠋⡰⠉⠀⡱⠀⣿⣿⣿⣷⠈⡖⠬⢰⣌⣸⢚⣯⣿⣿⡿⡄⣳⢏⣎⠳⡎⡴⣻⢾⣎⡇⢡⢹⣧⡙⡌⣟⣯⣿⣽⣟⣯⣿
⢯⡟⣦⢋⡖⡙⢦⣿⢿⣿⣟⣿⣹⣿⣿⣿⣏⠒⣴⠇⣾⡏⢐⢂⡿⣸⠭⡇⢶⡝⠞⡜⢠⠟⢀⠃⠈⠀⡳⢌⠂⠈⠀⠀⠀⠀⠀⠀⠀⠀⣸⣯⣗⡀⠀⠃⣿⡇⢹⣿⣿⣿⠀⣿⣼⣿⣿⣿⣿⡇⢸⣿⢹⣿⣿⣿⣿⣿⣿⣟⣿⢾⡃⢀⠣⠁⣰⢁⡆⠀⡕⠂⢹⣿⣿⣿⡄⢎⡓⢨⢰⣏⠞⣽⣿⣿⣿⣃⠼⣣⢎⠵⣹⢰⣹⢯⣿⣞⡈⢇⢶⢳⡘⢬⣳⣟⣾⣽⣾⣿
⣯⡗⣥⢫⡜⣡⣿⣻⣿⣿⣾⣿⣺⣿⣿⣿⠜⣸⡟⢸⣿⢀⠯⢰⡏⢷⣹⠀⣟⠎⡚⠀⠃⢀⠂⠀⠀⠸⣑⢊⠀⠀⠀⠀⠀⠀⡄⠀⠀⠀⠿⢿⣿⡇⠀⠀⢸⡇⢸⣿⣿⣿⠀⢻⡇⣿⣿⣿⣿⣧⠀⢿⣇⢿⣿⣯⢿⣿⣿⣿⢾⣻⣇⠠⠁⡐⠁⢨⠀⠀⡙⠀⠀⣿⣿⣿⣇⠸⣘⢸⢸⣿⡘⣿⣻⣿⣿⣽⠐⣿⠸⣌⡳⣘⢲⣟⣷⣯⣧⠘⡬⢛⣬⢣⢚⣾⣻⡽⣞⣿
⣾⡱⢎⡵⢚⡴⣿⣻⣽⣿⣾⡷⣻⣿⢿⣏⢲⣽⢠⣿⡇⣜⠃⡾⣍⠧⡇⢰⠏⡰⠁⠠⠈⠀⠀⠀⠂⡘⢄⠃⠀⠀⠀⠀⢀⢸⡇⠀⠀⠀⣿⣦⣌⡙⠀⠀⠀⢷⠀⢿⣿⣿⠘⡸⣿⣸⣿⣿⣿⣿⡀⡜⣿⡌⣿⣿⡘⣿⣿⣿⣫⣗⡯⠀⢈⠄⢀⣥⣄⠀⠀⠁⠀⠸⣿⣿⣿⠀⠧⢸⢸⣷⡃⣿⣿⢿⣿⣞⡧⢸⡇⢧⡓⡍⠶⣯⡷⣿⡾⡆⢱⠩⢞⣧⢩⠖⣿⣻⣯⣿
⣷⢍⡳⢌⢳⣾⣟⣿⣽⣷⣻⢷⣿⣻⢿⡃⣾⠃⣼⣿⠰⣍⢺⡕⢮⡹⠆⣹⠂⡅⣠⠚⣛⡛⢦⠀⢀⠁⠄⠀⠀⠀⠀⠀⠘⠋⠉⠀⠀⠂⢿⣿⣿⡿⠀⠀⠀⠀⠀⠀⠹⣿⡎⠅⡹⣇⢙⣛⣛⣛⣃⠂⠜⣧⠈⢿⣷⣿⢻⣯⣗⣯⡳⠀⢂⣼⠫⠔⣂⢳⡀⢠⠂⠀⢻⡿⣿⡆⢩⠸⡜⣷⡇⢿⣻⣿⣿⣧⢿⡌⣿⢰⠹⡜⢣⣽⣻⣽⣟⣿⡄⠙⣆⢻⣆⠻⣜⢯⣿⣽
⡿⣌⠳⢌⣾⣽⣾⣿⣽⡷⣿⣹⣿⣯⢟⣸⡟⢰⣿⠏⡜⡌⣶⣘⢧⡹⠀⠇⡘⢰⠃⡾⢻⠙⣸⣦⠈⠀⡀⠀⠀⠀⠀⠠⢠⣤⠄⠀⠀⠀⠒⡍⢿⠇⠀⠀⠀⠀⡀⠀⠀⠘⠳⠠⢰⡝⡆⠈⢿⣿⣿⡄⢿⣮⢧⠠⡙⢿⠈⣿⣞⣧⡏⢠⢸⡇⢺⡘⢿⠀⣇⠰⠀⢐⠈⣿⠸⣇⠰⡁⠋⣿⣧⢻⣿⣽⣿⣿⢺⣧⢸⡎⢧⡙⡆⢧⣿⣳⣿⣞⣷⡘⠤⡃⢯⡓⣬⢛⡷⣿
⡗⡎⡝⢠⣿⣿⣿⣿⣻⡟⣿⣽⡿⡟⢆⣿⢀⣾⡟⡰⢩⢒⡵⣘⢦⠇⡀⡓⡀⢹⡘⡅⣳⢈⣴⣿⠀⠀⠀⠀⠀⠀⠀⢀⣾⡻⢀⣄⡀⣰⣬⣷⣮⡾⠠⠁⢀⡀⠀⠀⠀⠀⡄⠐⣶⣿⣮⡀⠀⠝⠻⢷⠈⠻⠷⣕⠀⢸⠀⣿⣿⣷⠃⢸⢸⣏⠳⡎⠸⢀⡏⠀⠀⠌⠀⡘⡇⢯⠐⡅⡃⣿⣳⠜⣿⢾⣽⣿⢯⣽⡄⣇⢳⢸⡱⢊⣼⣷⢯⣟⡾⣷⢨⠱⡌⢻⢤⢫⡽⣿
⡝⢢⡑⣺⣿⡷⣯⡿⣭⣿⣏⣿⡿⡝⢸⠇⣸⡿⢠⡝⣌⠺⡴⣩⢮⠁⠄⡁⠐⠈⢧⡱⣄⡂⢻⣿⠀⠀⠀⠀⠀⠀⠀⢸⣿⡙⣶⡌⣹⣿⣿⣿⣿⣷⣷⣾⣯⠇⠀⠀⠀⣸⣧⠀⣼⣿⣿⣿⣧⡄⡀⠀⠀⣀⣤⣶⣶⢺⠀⣿⣿⣿⠁⣞⢸⣿⢃⡔⣡⠞⠀⠀⠘⢰⠀⣷⠹⠸⠀⢒⠀⣼⣿⡩⣿⢯⣿⣿⣏⣯⣧⠹⡄⣇⢳⠩⣜⣯⣿⡽⣯⣟⣧⠣⢜⡈⢷⢣⠞⣽
⡜⢃⠴⣻⣯⣟⣷⣿⢿⣻⢳⣿⡟⢠⡟⢠⡟⣰⢣⠞⢤⡛⡴⢣⡎⠀⠂⠠⠁⠂⠀⠑⢶⣿⣾⣿⠀⠀⠀⠀⠀⢀⡄⢸⣿⣧⡈⠳⣎⠙⢿⣟⣿⣿⡟⠢⠉⠀⠀⠀⢀⠯⢍⣼⣽⣿⣿⣿⣿⣿⣿⣶⣿⣿⣿⣿⣿⠏⠀⣿⣿⣿⠀⣯⢸⣯⣤⠜⢀⡀⠀⠀⠆⢀⠈⠋⣴⣇⢀⢘⠘⢸⣿⡥⣻⢿⣾⣿⣷⣹⣾⡅⢷⢨⢇⡳⢌⣿⣽⣻⡷⣽⣻⣧⢢⠙⠬⣳⠹⣜
⡘⠄⣻⣽⣷⣿⣻⣯⣿⡿⣹⣿⠃⡼⠁⡾⢠⢇⡏⡜⡢⢵⣉⢗⡂⠄⠁⠀⠐⠀⠀⠀⠀⠈⢻⣿⢀⠀⠀⠀⠀⠀⢿⣶⣿⣿⣧⡢⣝⡿⣌⠿⣽⡿⠇⠀⠀⠀⡀⠐⢨⢏⣌⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⡌⡔⣿⣿⣿⢠⡓⡼⠃⣠⠖⣭⢚⠀⠀⠀⠸⣲⠄⣿⣿⢸⡈⠀⢸⡿⣧⣹⣿⢾⣷⣿⡼⣷⣻⠸⣇⡳⢜⢢⢺⣯⣿⣽⣳⢯⡿⣆⢫⠰⡙⣿⣜
⢀⢣⣿⣽⣾⣿⣽⣿⣽⢳⡟⡵⢰⠃⣸⢃⡿⣘⢲⢡⠝⡆⡞⡼⠀⢀⠀⠀⠀⠀⠀⠀⠀⠰⡌⣿⢸⡀⠀⠀⠀⠠⣌⠿⣿⣿⣿⣿⣿⣿⣯⢿⡚⡷⠐⠀⠀⢰⡅⣢⡘⢮⢼⡰⢦⠹⣿⣿⣿⣿⣿⣿⣿⣿⡿⣫⡾⣱⡏⣿⣹⡟⢰⠙⣠⢚⡥⣛⠔⠋⠀⠀⢧⠀⡏⢰⣿⣿⢸⡅⡄⠸⣟⣷⣸⣿⣯⣿⣿⣗⣯⣟⣧⢻⡘⢮⡑⡎⣷⣯⡿⣽⢯⣟⡿⣇⢣⠔⡹⣾
⠀⣾⣿⣽⣿⣿⣻⣾⢏⡶⣻⢁⡏⢠⣟⣾⡇⣝⡂⢧⡚⣥⢣⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣧⠸⠈⣷⡀⠀⠀⠀⢻⣷⣮⣻⣿⣿⣿⣿⣿⡿⠇⢴⠃⠀⢠⠾⣿⣿⣷⠘⣎⠽⣩⢳⣌⠛⣟⠻⠿⣿⣿⣿⣿⣿⣼⣿⡇⡇⣿⠇⡠⡞⣥⢋⠖⠁⠈⠀⠀⠀⠘⡆⠁⣻⣿⡟⠸⣇⠃⠀⡿⣿⡔⣿⣿⢾⣿⣿⣰⣯⣟⣎⠿⣥⠓⣬⢳⣯⡿⣯⢿⣾⣽⣿⣇⢎⠡⢻
⢐⣯⣿⣾⣿⡿⢛⡼⢳⡜⡧⠸⠀⡾⣭⣿⣘⠧⣘⠦⡹⢴⢩⠀⠁⠀⠀⠀⠀⠀⠀⠀⢰⡀⠉⢧⠀⢻⣿⣄⠀⠀⠈⢿⣿⣿⣿⡟⠉⠁⢤⣀⡻⢶⣄⡠⢈⠛⢷⠏⣿⣧⠘⣧⣙⢦⣏⠷⣬⡛⢿⣿⣿⣿⣿⣿⣿⠿⢡⢱⡟⠀⠉⠘⠀⠁⠀⠀⠀⠀⢀⡤⢶⡀⣸⣿⣿⡇⡅⢿⠸⠁⢻⡽⣇⣿⢿⣻⣷⣿⡶⣽⢾⡽⡞⣦⡛⡔⡎⢷⣟⣿⣻⣶⣟⣿⢾⣏⡲⢁
⢸⣿⣿⣾⢻⡵⣏⢟⡲⡽⢁⠃⣸⢏⣷⡇⣾⠑⢊⢱⡝⠂⡜⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣦⣈⠣⠠⠻⣿⣷⣄⠀⠈⠻⣿⣿⣇⢀⠘⢶⣮⣝⡻⠶⣯⣕⡺⣦⣬⣟⣛⣃⡒⠹⠮⣜⡛⡴⢋⢠⡙⢿⣿⡿⠛⣡⠂⢸⢸⡏⠀⠀⠀⠀⠀⠀⢀⡠⡼⡩⠞⠉⢁⣿⣋⣿⡇⢺⠸⡇⢹⢸⢽⣇⢾⡿⣟⣿⣟⣷⢭⡿⣞⣯⢵⣣⢳⠸⣹⢯⡿⣷⣻⣾⣿⣻⡽⣿⢾
⢿⣷⡿⣸⢏⡾⡜⢧⡝⠆⡌⢰⣫⢟⣾⢼⡣⢥⡟⡲⠜⣸⠁⠀⠀⠀⠀⠀⠀⠀⢀⠀⢠⢸⣿⣿⣷⣦⣌⠙⢿⣿⣿⣶⣤⣹⣿⣿⣦⣤⡤⠉⠻⣿⣿⣶⣭⣛⢿⣿⣿⣿⣿⣿⣿⣷⣄⡙⠼⣭⠸⡄⡆⠆⠀⣰⣿⠀⣾⢸⢁⡟⣲⠖⡖⠎⠜⠃⠉⠀⠀⠀⠀⢸⢟⣴⣞⡇⠼⡆⣿⢸⢸⣹⢾⣩⣟⣯⢿⣾⢿⡞⣽⢯⣟⣮⢳⡭⣃⢧⢻⣽⣻⢷⣻⣽⣻⡽⣿⣻
⢻⡷⣙⡳⢮⣳⡙⡮⡝⠰⢀⡯⡞⣽⠆⣾⡱⣯⠳⢉⣼⡏⢀⠆⠀⠀⡀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠛⢿⢏⡳⣌⡙⢿⣿⣿⣿⣿⣿⠿⢏⣁⣀⣀⡘⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣄⠸⡅⠘⡀⣰⣿⡏⠀⡇⡻⠈⠚⠁⠁⠀⢀⣀⣤⣴⠎⠁⠀⣠⡌⢿⣿⣿⣧⠘⢵⢸⡞⡈⡗⣿⡜⣻⣞⡿⣞⡿⣿⢜⣻⣮⢿⡜⣳⡍⡆⢯⡽⣯⢿⣭⣷⣻⢷⡿⣿
⢛⡜⣧⣹⢳⢧⡹⣜⢡⠃⣾⣱⢻⡼⢰⢇⢿⠍⣰⣟⡿⠀⣪⣴⣿⣿⣿⣿⣿⣶⣤⣤⣤⣄⣀⠀⠀⠀⠈⠱⠫⠘⠐⠈⠛⠿⡿⣡⡾⠟⠟⠻⠿⠿⢿⣮⡙⠿⣿⣿⣿⣿⣿⠏⢿⣧⡙⢿⣿⢿⠆⡠⠀⢀⣿⣿⣇⠀⡇⡝⠀⢀⠤⢠⣾⣿⣿⡟⠁⠀⣠⣿⣿⣿⣤⡙⠻⢿⡀⠄⠂⣧⡅⢧⣻⡗⣽⢯⣿⣹⢷⣻⢞⡵⣯⣟⣿⡱⢿⡱⡌⢿⣽⣻⡽⣷⢿⡿⣽⣿
⢪⡕⣳⡜⣯⠖⣹⢂⠧⣸⡱⢧⣏⢇⡏⢮⣝⣾⣛⣾⡹⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣶⣶⣤⣅⡀⠀⠀⢠⠀⡠⢅⣢⣤⣶⣶⣶⣶⣶⣆⣭⡴⠈⢉⣉⣡⣄⣀⡈⠙⣿⣶⣽⡿⠀⡇⠀⣸⠿⠿⠁⠀⡧⠁⡎⠼⢠⣿⣿⣿⠏⠀⢀⣼⣿⣿⣿⣿⣿⣿⣷⣶⣅⠈⢱⠘⣧⢸⡱⣿⢸⣿⢾⡽⣞⡿⣎⣳⣻⢞⡾⣷⢋⣿⡖⠹⣎⣿⡽⣯⢿⣻⣟⣾
⢢⢏⡷⣸⢧⢻⢡⠚⢤⣳⠹⣇⠾⡸⡜⣱⢾⡳⢿⠾⡁⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⢹⣿⣿⣿⣿⠿⠛⠋⠉⡀⠀⠀⢠⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⣵⠐⣿⣿⣿⣿⣿⣿⣦⣀⠉⠛⡃⠰⠀⠀⣀⣀⣠⡀⠀⡇⠀⢰⠋⣾⣿⣿⡟⠀⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⡿⣏⣶⠈⠖⢸⢰⡹⣏⡗⣿⢯⡿⣽⣳⢟⡶⣭⢻⡽⣞⣯⢜⣿⡅⢻⡾⣽⢿⣯⡷⣿⣽
⢬⢣⡟⡴⢫⢃⢇⡋⡼⣥⢻⢬⠳⣱⠣⣝⣺⠭⣏⣟⢀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢷⠈⣿⡿⠋⢁⠠⠀⢠⠀⠀⢀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢤⠘⣿⣿⣿⣿⣿⣿⡿⠠⣛⣁⡀⠀⠀⢻⣿⣿⡇⢰⡇⡀⢘⢸⣿⣿⣿⠁⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⡹⢞⡵⣃⢘⠂⡆⡝⣯⡗⣻⣯⣟⡷⣯⡻⣧⢫⣏⡷⣛⡾⡞⣜⡿⣄⢻⣯⣿⢾⡷⣯⢿
⢌⡗⣮⣹⢣⢏⡒⣸⠳⣥⢏⡞⡱⢣⠝⣲⣟⣿⣹⠎⢸⣿⣿⣿⣿⣿⡿⠟⠛⠛⠛⠏⠞⠀⠟⠁⠀⣀⠂⠁⢀⠀⢀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡝⣮⠅⠿⠽⠻⢿⣿⣿⢣⣿⣿⣿⣷⠀⠀⠈⣿⣿⠂⢸⡇⡇⣈⢸⣿⣿⡏⠀⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⢧⣏⢳⣚⣭⠀⡃⠆⣙⢾⣝⡳⣿⢾⣽⢳⣟⡽⣓⡮⢷⣫⢷⣹⢎⡽⣎⢆⢿⣞⣯⢿⣽⣻
⠸⣜⣣⠱⡎⢦⢡⢏⣳⢣⢞⡸⣑⢣⢫⠵⣞⣏⡾⢁⡎⣿⣿⣿⠟⠁⡀⠄⠁⢈⠀⠀⠀⠀⠠⠀⠰⢀⠠⢂⠀⢀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣟⢣⢟⡨⠇⠐⣯⢟⡶⣬⠙⠈⢻⣿⣿⣿⠀⠀⠀⠹⣿⡁⢸⡇⠃⠈⣾⣿⣿⡇⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣏⢮⠳⡜⣼⠀⡱⢠⢘⠾⣽⣣⢿⣟⣾⣛⣮⡟⣧⢻⣽⢣⡿⣼⣳⢺⡹⣮⡘⣿⢾⣯⣷⣿
⢱⠣⣇⠽⣘⠡⣎⠷⣬⢣⡎⢵⠣⡝⣎⢯⣻⢾⡹⢸⣿⣹⣿⠃⠀⡈⡀⠔⠢⠈⢀⠀⠀⠀⠀⡀⢆⠀⣠⡏⠀⣼⣿⣿⣿⣿⣿⣿⣿⣿⢿⣛⠻⣌⢳⣉⢏⢮⠱⠀⠜⣮⠏⣟⡷⡆⢠⠀⠹⠻⡟⠀⠀⠀⠀⠙⠇⢈⡇⠀⡁⣿⣿⣿⡇⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣝⣎⢳⡙⣲⠀⡱⠸⢨⡝⣷⣫⢞⡿⣾⣽⣳⢟⣭⡗⣺⣏⡷⣹⣎⢷⣓⢳⣳⠘⣿⠾⣽⣾
⡘⣇⠞⡬⠱⣘⢧⡛⢤⢳⡜⢣⡝⣜⢮⡳⣏⡿⢁⣿⣿⣷⡍⠀⡐⢄⣥⣶⣿⣿⣿⠟⠃⠀⢂⠰⠏⢰⡟⠀⢸⣿⠿⠛⠛⠋⠉⠉⠉⠚⠢⠍⠳⢌⠣⠚⠌⠁⠀⢀⢳⡘⢣⡘⢿⡹⣄⠀⠁⠋⠄⠀⠀⠀⠀⠀⠀⠀⢃⠀⢀⣽⣿⣿⣷⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⡳⢎⡳⡸⠥⠀⣑⠂⠴⢸⣳⣟⡮⣿⣳⣏⡿⣞⢧⢿⡱⣾⡹⢧⣯⢻⣌⠷⣭⣓⠜⡻⣯⣿
⢘⡆⣛⠰⣃⢏⡶⣉⡞⣱⠪⡵⣚⡭⢞⡱⣯⠟⢸⣿⣿⣿⣿⡄⣰⣿⣿⣿⠿⠋⠀⠠⠔⠋⠂⠀⠈⠚⢁⠀⠋⠁⠀⠀⠀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⡖⡹⣏⢷⡐⣫⢧⡁⢠⣒⡀⠀⢀⡀⠀⠀⢀⣴⡟⠀⣾⣿⣿⣿⣿⡄⠸⣿⣿⣿⣿⣿⣿⣿⣿⣏⡳⣍⠶⣱⠃⠀⡔⠃⠈⡇⣱⡾⣷⣹⡷⣯⣻⣝⡾⣋⡷⢣⣟⡻⣜⣯⢞⡝⣖⣻⢬⠱⣋⣿
⢢⡕⢣⠓⣼⣉⠖⣱⢎⡱⡹⣜⡥⣏⢽⡹⣞⢁⣿⣿⣿⣿⣿⢧⣿⣿⠟⠁⠀⠀⠁⠀⠀⠀⠀⠤⢐⠆⠀⠀⢀⠐⠈⠈⠀⢀⠀⠈⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠌⠱⡙⢦⢛⠬⠃⢀⡷⣹⠆⣴⣿⣿⠆⣴⣿⣿⠅⠀⠀⠈⠛⠻⠿⠷⡀⢹⣿⣿⣿⣿⣿⣿⢏⢮⡱⢎⡑⠃⣀⠀⠞⢠⠀⣑⢂⢿⣽⡝⣿⣵⡻⣾⠽⣭⣛⡽⣎⡿⣱⢞⣯⢻⡼⡜⣯⢇⡙⢾
⢒⡌⢇⢺⡱⢬⡙⢧⢎⡱⣇⢧⡽⣘⢮⢳⡍⣸⣿⣿⣿⣿⣿⡼⣿⠇⠠⠂⡈⠁⠄⠒⠠⠀⠀⠀⠀⠀⠀⡀⠄⢂⣀⣤⣤⡤⠔⠂⠂⠀⠀⣀⣠⣤⡤⣤⠦⣤⣀⠀⠀⠁⠘⠠⠉⠌⣠⢞⣱⠇⣼⣿⣿⡟⠈⢀⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠻⠿⡿⢟⡱⢎⠲⠑⠈⠀⠐⢠⠀⡍⠠⠐⣨⠜⣸⢷⣻⢻⣾⡹⣯⣟⣳⡭⢶⣹⡞⣵⢻⢾⣭⢷⡹⢼⡫⣞⠬
⠌⡜⣊⠶⣙⠦⣏⠳⢎⡵⡺⣜⢼⣡⢏⡷⢠⣿⣿⣿⣿⣿⣿⣧⡛⠠⠁⢠⠐⣁⣬⣤⣤⣤⣄⠀⠀⢀⣤⣶⣿⣿⡿⠛⡁⠐⣈⣤⣴⣾⣿⣿⣿⣿⡷⣥⣛⠴⣩⠖⣄⠀⠀⠀⡴⢺⡵⣚⢧⢰⣿⣿⣿⣿⡆⣿⣿⣿⣷⡀⢠⣀⣀⠀⠀⠀⠀⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⠈⢄⠀⡜⢀⠃⠐⡣⠜⡞⣿⣻⣷⢿⣱⠿⣼⣝⢣⢷⡹⣧⢯⡻⣼⣻⣝⡲⢽⣜⣯
⠘⡴⣩⢒⣭⠺⣥⠛⣜⢶⡹⡜⢦⡝⢮⢁⢺⣿⣿⣿⣿⣿⣿⣿⣷⡄⢐⣴⣾⡿⠟⠛⠛⠛⠉⠚⠄⣾⣿⡿⠟⠉⠐⣀⠴⠞⠋⠉⠁⡀⠀⠀⠀⠀⠀⠈⠉⠙⠣⢻⡜⣆⠀⠀⣟⢳⢮⡝⣦⠈⣿⣿⣿⣿⡿⣸⣿⣿⣿⡇⠀⢿⣿⣿⣶⣆⢰⣿⣿⣷⣦⣤⣀⣀⢀⠀⣠⢒⡅⣈⠠⠂⢨⠀⡱⢠⠱⢩⠼⣝⣷⢻⡯⣟⡽⢾⣜⣏⢮⡳⡽⣎⠷⣝⡾⣽⡜⡧⢏⣾
⠘⡴⢡⢚⡼⢳⣌⠻⣜⢦⡳⣙⢮⠜⡏⢀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⣿⠏⠁⢀⠈⡄⠌⠀⠀⠀⠀⠛⠋⠀⠀⢀⠈⠄⠐⢊⡠⠤⠔⠐⠂⡐⠀⠀⠀⠀⠀⠀⠀⠀⠈⠘⣣⠄⢸⢫⡞⣼⢲⠇⡣⠙⣿⣿⣷⣿⣿⣿⠻⡜⠀⣿⣿⣿⣿⡃⣸⣿⣿⣿⣿⣿⣿⣿⣟⣣⠈⢧⠆⠈⠳⢤⠐⢀⡁⠘⡸⢄⡋⡷⣺⡿⣿⣭⢿⣏⡾⣥⢳⡹⢧⣏⠿⣼⣹⢯⣿⣸⣋⢿
⠘⡔⣣⢻⡜⣣⢜⡹⣎⠶⡱⣍⠞⡼⠐⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⡜⠃⠀⢐⣨⣴⣶⡾⠷⠖⠀⠀⠀⠀⠀⠐⠈⠀⠄⠉⠀⠀⠀⠀⠀⡀⠀⠈⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠋⠀⣏⠞⣥⡛⣆⠀⣀⠸⢿⠿⡟⢯⡜⠃⠁⡀⠿⠿⡛⠧⠃⣿⣿⣿⣿⣿⣿⣿⣿⣿⢆⡛⢬⠃⢐⠀⠣⠈⠘⡽⡂⠅⡆⡱⣹⢱⢿⣹⡞⣿⢾⣹⢧⡇⣏⠿⣜⠯⣖⢯⢿⣾⣇⢮⣹
⠘⡔⣏⢞⡜⢦⢫⡜⣎⢳⡱⢎⡽⠁⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⣠⣿⡿⠋⠁⠀⠄⠀⡀⠀⢀⠰⠆⡴⠁⠠⠈⠀⠀⠀⠀⠂⠀⠀⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢮⣝⠲⣝⣺⢳⣿⣷⣤⣂⡈⠀⠀⡄⣀⠀⠀⠀⠀⠀⢰⣿⣿⣿⣿⣿⣿⣿⣿⡿⢬⡙⡼⠀⢸⡀⠀⠀⠂⠱⣩⢰⢸⠰⡡⢟⡮⣝⣿⢺⣻⣟⡮⢷⢬⢻⡭⡷⢎⡯⣟⣾⡽⣶⢩
⢸⡱⣏⠼⣸⢣⢏⡞⣬⠳⣘⠧⡞⢈⣿⣳⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢱⣿⠟⠀⠄⠁⠄⠠⠤⠶⠌⢢⠀⢰⠇⠀⠀⠀⠀⣀⣥⣶⣶⣿⣿⣿⣿⣿⣿⣷⣶⡤⢤⣀⠀⠀⠀⠀⢀⠳⣎⠻⣬⣓⢯⣿⣿⣿⣿⣿⣷⣤⡈⠇⢿⣿⣶⣶⡶⢸⣿⣿⣿⣿⣿⣿⣿⣿⡟⢦⡙⡄⠀⡴⢃⠀⠀⠘⠀⠐⡀⠜⡢⢙⠼⣓⣯⢼⡻⣜⣯⣟⣯⠞⡥⣟⡽⣎⢷⣹⢾⡷⣯⢯
⢠⢡⠋⣸⢡⢋⡜⡘⡄⢣⡉⡜⠁⣸⢣⡟⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢸⡏⠀⠀⠀⢈⠀⠀⠀⠀⠀⠀⠁⠘⠀⠀⠀⣠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡙⣤⢉⠛⣤⢀⠀⠀⢃⡘⠃⣄⠛⣌⣹⣿⣿⣿⣿⣿⣿⣿⣌⠁⣈⣿⣿⠛⢸⣿⣿⣿⣿⣿⣿⣿⣿⡙⣄⢹⠀⢸⠘⠃⠀⠀⠀⠃⢀⡀⠀⣇⡈⡇⣿⡘⡇⢻⡘⣧⣼⣹⢻⣸⢡⣿⣸⣌⢹⣼⣹⣏⣿
⢌⡧⡛⢴⣫⠞⡴⢣⡝⢦⢱⠇⢠⡏⣯⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣏⠇⠀⠄⢁⡀⠄⠀⠀⠀⠀⠀⠀⠀⠀⢠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣗⡣⢏⡞⡴⣋⢷⣀⠠⠓⡜⣬⢳⢎⣯⣿⣿⣿⣿⣿⣿⣿⣿⣷⣄⠈⠘⠃⣾⣿⣿⣿⣿⣿⣿⣿⣿⠱⣎⠜⢠⢋⠞⡡⠀⠀⠀⡘⠨⣝⢠⢱⠸⣍⠲⣍⡻⠌⡷⣭⢻⣽⣯⢞⡵⣾⡵⣎⡗⣺⣵⣻⢾
⠜⣆⢻⣘⢮⡝⡼⢡⡚⢆⡏⠀⣾⡱⢯⣻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠀⠈⣠⣴⣶⣶⣷⣶⡦⣀⠀⠀⢀⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠛⠛⠛⠛⠛⠛⠘⠱⠫⣔⢣⡝⢦⡛⣆⠑⣎⢧⢏⡞⢶⣻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣏⠳⢌⠂⡜⣌⠚⠄⠀⠀⠀⠐⠀⢯⡈⡰⡇⢎⡳⢥⡙⣇⠸⣇⡟⢿⡾⣟⢶⣹⣽⣳⢽⡱⢾⣽⣻
⠸⣌⠷⣸⢳⡜⣡⢣⠝⡎⡐⣸⢧⡻⣽⣫⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢀⣾⣿⣿⣿⣿⣿⣿⡳⢄⠣⠀⢸⣿⣿⣿⣿⠿⠋⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠘⢦⡹⢜⡂⢸⠺⣜⡹⢶⡹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⣿⣿⣿⣿⣿⣿⣿⣿⢎⡝⡌⢀⡱⢂⡉⠀⠀⠀⡄⠀⠀⢣⢃⠀⡟⢬⡱⢣⡝⡜⡆⢹⡜⡧⢿⣻⣽⡒⣯⣟⣮⢝⡳⢾⣽
⠱⣎⠷⣙⠶⣡⢣⠎⡞⠡⢰⣏⡾⢯⣷⣻⣞⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠸⣿⣿⠿⠛⠋⠉⠉⠓⠌⢢⠁⢠⣿⠿⠋⠁⠀⠀⢀⣠⣤⣶⣶⣾⣿⣿⣿⣿⣷⣧⡲⢤⣀⠀⠀⠘⢱⠂⠈⠳⡘⣥⢫⡵⣻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣿⣿⣿⣿⣿⣿⣿⡿⡜⡰⠁⠰⡀⠃⠀⠀⠀⢠⢒⡀⠠⠀⠩⠀⢹⢢⡙⢧⡜⣱⢹⡀⢿⡜⣭⣛⣶⢻⡵⣟⣾⣎⠳⣏⣿
⠱⣎⠷⣩⠞⣡⠎⡼⢁⢡⣿⢾⣹⢿⡼⣧⢿⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠟⠁⠀⢀⠁⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣤⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢧⣏⡝⡲⣄⠀⠁⢠⢣⡙⢦⢧⢳⡭⣟⣿⣿⣿⣿⣿⣿⣿⣿⣿⣗⣿⣿⣿⣿⣿⣿⣿⡷⣉⠆⠀⠡⠀⠁⠀⠀⢀⠮⡑⡖⡀⠘⠤⠀⠈⣧⡙⢶⡸⡱⢎⡥⠈⡾⣰⡙⣮⢷⡹⢿⣳⣯⢳⢎⢾
⠱⣎⢛⠴⣉⠦⣙⠆⢠⡟⡼⢳⣏⢾⡱⣏⣟⣞⣿⣿⣿⡿⠿⣿⣿⣿⣿⣿⠀⠀⠠⠈⠂⠈⠀⠀⠀⠀⠀⢀⠏⠀⢀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠛⠉⠉⠀⣀⣁⣈⠀⠀⢂⠦⣙⠷⣪⢗⣳⡽⣿⣿⣿⣿⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿⣿⣿⡟⣡⢊⠀⠀⠀⠀⠀⠀⢠⢎⡱⡩⢜⡡⠀⠘⣱⠀⢧⣙⢦⢳⡱⢎⡵⣃⠸⡱⡣⠝⣶⡹⣏⣷⢻⣧⠞⣮
⡙⣆⢋⠖⡱⢪⠅⢠⣓⢮⡝⣳⢞⡯⣽⡞⣽⢾⣷⣋⣵⣾⣿⣿⣿⣿⣿⡿⠀⠀⣊⣥⣤⣶⣤⣤⣄⠀⠀⠀⠀⠀⠈⠉⠙⠻⢿⣿⣿⣿⣿⣿⣿⣿⠟⠋⠀⣀⣤⣴⣾⣿⣿⣿⣷⢫⠇⠈⠘⠉⡙⣁⠛⠶⢽⣿⣿⣿⣿⣿⣿⣿⣿⢟⣼⣿⣿⣿⣿⣿⡟⡴⢡⠂⠀⠀⠀⠀⢠⠘⣂⠎⡴⢃⡇⢳⠀⢠⠳⡀⠸⡜⡜⢦⡹⢜⡲⢡⠆⢱⢱⡋⠶⣏⠼⣯⢿⢾⡙⡶
⡘⠦⣉⠮⣑⠇⢀⡷⣸⢎⡷⣹⢾⣹⣳⢯⣟⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢠⣾⣿⣿⣿⣿⣿⣿⣿⡰⠀⣠⣶⣶⣶⣦⣤⣁⠀⠈⠻⢿⣿⣿⠛⠁⢀⣤⣾⣿⣿⣿⣿⣿⣿⣿⣿⠃⠀⠀⣴⣿⣿⣿⣿⣿⡶⣤⡛⢿⣿⣿⣿⢟⣵⣿⣿⣿⣿⣿⣿⡟⢼⡈⣇⠃⢀⠀⠠⠘⢠⠃⡜⡸⢔⠣⡜⢣⠀⢰⢩⡅⠀⣯⣙⠦⡝⢦⡹⢜⠲⡀⢆⢹⢢⢹⡚⣽⣻⢏⣷⡹
⢈⡓⣌⠳⣌⢂⠾⣔⢯⠾⣝⣯⣟⣳⢯⣷⣻⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣿⣿⣿⣿⠿⠿⠿⣿⢿⢁⣾⣿⣿⣿⣿⣿⣿⣿⣿⣦⡀⠂⠉⠀⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣣⠟⠒⠙⠻⣼⣿⣿⣿⣿⣿⣿⣿⢫⠜⡦⡑⠂⠠⣉⠞⣦⠈⢀⠣⠜⡱⢊⠵⣘⡃⢀⠧⣒⢭⠀⠰⣍⠞⣍⠶⡑⢎⡱⡁⡼⡌⢳⡌⢳⠼⣽⡻⣾⡽
⠠⡓⣌⣳⣏⢮⡱⣎⡯⣟⡽⣞⡾⣽⣻⠶⣯⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣿⠟⠉⠀⢀⠀⡀⠀⠀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⠀⠀⣿⣿⣿⣿⣿⣿⣿⠓⠁⠀⠀⠀⠀⠀⠙⠛⠿⢿⣿⢟⠢⢏⣚⡑⠀⠀⢦⡑⢯⣒⠯⣆⠘⠌⠲⣉⠖⡡⢔⡊⡖⡱⢪⠅⠀⣏⠞⣬⠲⣉⠖⡱⠀⡁⠘⡜⣬⢃⠮⣱⣟⣷⣻
⡔⢣⠼⡽⣘⢦⡱⣭⢳⡭⣟⡽⣛⠷⣯⡟⣷⢯⣿⣛⠿⢿⣿⣿⣿⣿⣿⡏⠁⢀⠠⠄⠂⠀⠀⠀⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⢼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⢰⣿⣿⣿⣿⣿⡟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠰⣶⣶⣤⣤⣄⣀⡑⠢⣝⣣⢎⠷⣩⣗⡈⠱⣀⠫⠔⠣⠜⣐⢣⢃⠞⢀⠹⢺⡰⣃⠇⣎⠁⢄⡙⠤⠘⣞⡥⢎⡱⣟⡾⣽
⠌⣧⡻⡵⣩⢖⣳⣭⣷⢻⡞⣷⢯⡟⣾⡹⢯⣿⣿⣿⣿⣷⣮⣝⣿⣿⣿⡇⠄⣠⣶⣾⣿⣿⣿⣿⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⢺⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⢸⣿⣿⣿⣿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣷⣶⣬⣙⢿⣷⣶⣯⣦⠀⢃⠌⡡⠃⠆⢢⠡⠚⠄⡌⡇⡗⣌⠚⠀⡐⠢⢌⠱⣂⠸⣝⢦⡱⢺⣽⣻
⡜⡖⡱⢫⢵⣛⣞⡳⣏⡿⣽⣫⡟⣽⢾⡽⣯⣿⣯⣻⣿⣿⣿⣿⣿⣿⣿⡇⢰⣿⣿⣿⣿⣿⣿⣿⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⠀⢸⣿⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀⠠⠀⠐⠀⠀⡀⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣮⣝⡻⣿⣷⡀⠂⠄⠡⠈⠄⠂⡉⠄⢰⠱⠙⣄⠠⡑⢌⠱⣈⠱⠌⠦⡹⣎⢼⡡⢞⣿
⣻⡤⠱⣉⠶⣙⢮⡝⣧⢛⡶⣫⠽⣾⢹⠾⡽⢿⣿⣿⣶⣝⣿⣿⣿⣿⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⢸⣿⣿⡟⠀⠀⠀⠀⠁⠀⠀⠀⠠⠁⠠⠀⠀⢰⡇⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣝⠷⡄⠀⠂⠁⠀⡁⠀⠀⠈⡆⡄⢣⠆⡱⢈⠒⠤⡉⢎⡱⠢⣽⢢⡝⣎⠾
⡽⡇⠱⣈⢧⡙⣮⡝⣮⢏⣷⣹⣛⣶⢫⣝⢾⣭⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⡆⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⣿⣿⠀⠀⠀⠀⠁⠀⠀⠀⠀⠄⠠⠀⠀⢀⣿⡇⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣷⣌⠀⠀⠀⠀⠀⠀⠀⠀⢱⠠⠈⡖⢡⠊⠜⡠⢑⢊⡔⠳⣌⢳⡜⣱⢻
⠷⠁⠱⢐⠪⡜⡧⣝⣮⢻⣜⡷⣻⡼⣛⣮⢟⣮⣿⣿⣿⣿⣿⣿⣿⣿⣿⡧⣇⣿⣿⣿⣿⣿⣿⣿⣷⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⠀⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠀⠀⢿⡏⠀⠀⠀⡈⠀⠁⠀⠀⡈⠀⠀⠀⠀⣼⣿⣷⠀⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣻⣗⣻⡄⠀⠀⠀⠀⠀⠀⠀⠈⣆⠠⠘⡅⢊⠔⡁⢎⠰⢌⡳⣌⢳⠸⡥⢛
⣧⠈⡐⢨⠱⣩⢗⣹⣎⣟⣮⡽⢧⣻⣝⡮⣟⡾⣽⣿⣿⣿⣿⣿⣿⣿⣿⡇⣷⣿⡿⠟⠛⠉⠉⠉⠛⠐⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠀⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠀⠸⡇⠀⠀⠀⠀⠀⠀⠀⠀⢀⠈⠀⠀⢰⣿⣿⣿⡄⠀⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⣞⡷⠆⠀⠀⠀⠀⠀⠀⠀⠀⢹⠀⠀⢹⡀⠎⡐⢌⠢⣡⠳⡜⣬⢣⡝⣩
⣽⡄⠐⡠⢣⡱⣏⢶⡝⣮⣳⡽⣏⣷⢺⡷⢯⣟⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣿⠉⠀⡀⠄⠀⠀⠀⠀⠀⠀⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⣧⠀⠀⠈⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡃⠀⠀⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⣿⣿⣿⡇⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣟⡾⣭⠃⠀⠐⠀⠀⠀⠀⠀⠀⠀⡆⣦⣄⠣⠂⡑⡌⠶⣡⢏⡜⢦⣃⢞⡱
⠼⣇⠠⢡⢧⡹⣞⡽⣺⠵⣧⣻⡵⣞⢯⡟⣿⣺⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⢿⡿⣿⢿⡿⣏⠷⡹⢎⡽⣀⠀⠀⠙⠻⣿⣿⣿⢿⣿⢻⢣⡝⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⣿⣿⣿⣿⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣯⡟⣵⠂⠀⠈⠀⠄⠀⠀⠀⠀⠀⣷⢹⣿⣇⢳⣌⡙⠶⣡⢚⡜⣣⠜⢮⡱
⠸⣷⢨⢇⡯⣵⢫⢾⡵⣻⢧⣳⣏⣯⣗⡯⣷⣻⣽⣿⣿⣿⣿⣿⣿⣿⣿⣗⠻⠀⠀⠐⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠘⠣⠞⠬⠓⠍⠒⡀⠁⠀⠀⣀⡀⠑⠪⢜⠣⢎⢣⠓⡬⣑⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⣿⣿⣿⣿⣿⡇⠀⠈⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣳⢻⡜⠀⠀⠀⠄⠀⠀⠀⠀⠀⢀⣿⡆⢿⣯⠎⢿⣿⣶⣌⡳⢬⣑⢫⢖⡹
⠱⣻⡎⡜⠲⣍⢟⣮⣳⡭⡷⣝⡮⣷⢺⡝⣷⣻⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⢠⣶⣾⣿⣿⣿⢶⣦⠀⠀⠀⠀⠁⢂⠀⠀⠑⢦⣄⡀⠀⠘⣑⣠⣠⣴⣶⡯⠉⢀⠀⠀⠉⠀⠃⠍⠰⠠⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡚⠿⢿⡿⣿⣿⣿⣧⠀⠀⠘⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡗⣯⠳⠀⠀⠀⠀⠂⠀⠀⠀⠀⠀⣼⣿⣷⢸⡿⡎⠌⢷⡿⣿⡽⣢⡙⢦⡋⡼
⡐⣯⣷⢈⠳⡌⢞⢶⣣⢟⡵⣫⠷⣭⣏⡿⣽⢳⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⣿⣿⣿⣿⣿⣿⠎⠹⣧⢀⢀⠀⠀⠂⠄⠀⠀⠈⠛⣿⣿⣶⡙⠿⠛⠉⠀⠀⠀⠀⠀⠀⠀⢀⠠⠀⠄⠠⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠘⠢⠱⠌⢆⠣⡜⠀⠀⢈⠆⡙⠿⣻⢟⡿⡻⢟⡏⣗⢣⡝⠎⠁⠀⠀⠀⢀⠀⠀⠀⠀⠀⣰⣿⣿⣿⡆⢻⣿⡜⡘⢿⣳⣿⡥⢛⡴⢩⡜
⡗⣲⢻⡌⡒⢍⠺⣬⢳⣏⢾⣱⢯⣗⡮⣗⣻⡽⣾⣿⣿⣿⣿⣿⣿⣿⣿⢣⡔⣿⣿⣿⢯⢿⣧⣳⡄⠻⣿⣶⡐⢤⡀⠀⠐⠄⠀⠀⠌⡙⢿⣿⡶⠀⠀⠀⠀⠀⠀⠀⠠⢀⠂⠆⡉⠄⢃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠀⠃⠘⠀⠀⠀⠈⡁⠂⢁⣊⣐⣙⣊⣘⣀⡃⠈⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡰⢿⠿⣿⣿⣿⡘⣿⣷⡰⠘⣟⣾⣽⢣⠞⡥⣚
⡟⣬⢻⣧⢑⢪⡑⢮⡳⣎⡷⣹⢞⣼⢳⡝⣾⣱⢿⣿⣿⣿⣿⣿⣿⣿⢣⣿⣧⢿⣿⠃⠀⠘⢿⣯⡄⠀⣿⣿⠀⠄⠋⠶⣀⠀⠀⠀⠀⠤⢀⠈⡀⠄⠀⠀⠀⠀⠀⠀⠡⢀⠘⠠⠐⡈⠠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣄⠀⠀⠀⠀⠀⠀⠀⠲⢶⣶⣶⣤⣻⣿⣇⠸⣿⣧⠱⠸⣻⣞⢧⡙⢦⡹
⣿⡐⣏⢿⢠⢃⠼⣡⢷⡹⣜⢧⡻⣼⣣⢟⣼⢣⣿⣿⣿⣿⣿⣿⣿⢇⣿⣿⣿⢸⣿⠀⠆⣠⢸⣿⡇⢠⡿⠟⠀⢠⣶⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠀⠐⠀⠀⠀⠀⣀⣀⣀⣀⠀⠀⠀⠀⠀⠀⣶⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡄⠀⠀⠀⠀⠀⠀⠀⠙⣿⣿⣿⣿⣿⣦⢹⣻⣧⠡⠹⡾⣹⢌⡳⣜
⣿⡔⢪⢟⡆⡍⢲⡱⢮⢵⡫⢞⡵⣣⠟⣞⡼⣳⠾⣽⣿⣿⣿⣿⡿⣸⣿⣿⡇⢸⣿⠀⣴⣿⢸⢏⠇⢸⡐⠂⣠⣿⣿⣿⣿⣷⣤⣀⠀⠀⠀⠀⠀⠠⠀⠀⠀⠀⠀⠄⣀⣀⣤⣤⣶⣶⣶⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠘⣿⣿⣿⣿⣏⠀⠀⠀⠀⠀⠀⠀⠀⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⠀⠀⠀⠀⠀⠀⠀⠈⣿⣿⣿⣿⣿⣆⢹⣯⢷⠠⠹⡱⢎⠴⣩
⣿⡜⡱⢎⣧⠘⡡⢝⡪⢶⣹⢫⡞⡵⡻⣜⡳⢥⠻⣥⢿⣿⣶⣿⣇⣿⠟⣋⡉⠸⢁⣼⣿⣿⠈⢊⠀⢄⣤⣾⣿⣿⣿⣿⣿⣿⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⢹⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠁⠸⣿⣿⣿⣿⣿⡄⢻⡞⡥⠐⡟⣌⠧⡳
⡿⡗⠤⢫⠴⡆⠑⣎⠱⣣⠧⣓⢮⡱⢳⡼⣜⣣⢟⡼⣻⣿⣿⣿⢸⡇⢸⣦⠀⣤⣿⣿⣿⣿⣆⣠⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣵⠃⠀⠀⠀⠀⠀⠄⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡗⠀⠀⠀⢸⠀⠈⣿⣿⣿⣿⠀⠀⠁⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣧⠈⢶⢡⢃⠘⢧⣚⡱
⡝⣿⠠⡙⢮⡥⠘⢠⢋⡔⢣⠭⣖⡹⢧⡞⡵⣎⠿⣜⣿⣿⣿⣿⠸⠃⣋⣵⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢧⠀⠀⠀⠀⢀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⠀⠀⠀⣾⠀⠀⢸⣿⣿⣿⡀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⢿⢻⠛⣏⠹⡐⣂⠈⢳⡌⢆⡈⢶⡱
⠘⣽⠂⡅⢣⢚⢃⠰⠨⡜⣡⢛⢦⡛⢧⠞⣵⢫⡽⣚⢾⣿⢟⣣⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⡟⢏⢯⡙⢖⡩⢍⠯⡙⠀⠀⠀⠀⠀⠀⠀⠀⠈⠀⠀⢿⢻⠻⡟⢿⠻⡟⢿⡛⠿⣛⠟⡯⡝⢯⠋⠀⠀⠀⠀⢿⠁⠀⠈⣟⢻⣛⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⠿⣿⢻⢿⢻⢻⡝⣻⢝⡫⡝⣭⠀⠀⠀⠀⠀⠀⠀⠀⠀⡎⢦⡙⠤⢃⡱⢠⢂⠈⢎⢣⠒⡌⢳
⠞⡹⣇⣈⠒⡆⢻⡀⠱⣈⠴⡉⢖⡩⢏⡝⣮⢳⡹⠝⣣⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⢿⡛⠿⣩⢣⠙⡘⡌⢆⡙⢢⠑⠪⠜⣁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠠⢃⢎⡱⢩⢎⠳⣉⠖⣩⠣⡍⠞⡴⡙⠦⠁⠀⠀⠀⡐⢪⠄⠀⠀⡜⢢⡱⠀⠀⠀⠀⠀⠀⠀⠀⢠⠣⡝⢢⣋⢎⢣⢃⠞⡡⢎⡕⠺⡔⠀⠀⠀⠀⠀⠀⠀⠀⡌⡜⢢⠜⣡⠣⠔⠣⠌⠀⠈⢧⡙⢦⣉
⢎⡱⣹⢌⡳⢲⣌⢣⠀⠀⠒⣉⢎⡱⣋⢈⣤⣅⣩⣼⣿⣿⣿⡿⢿⠟⡛⢋⡍⢃⠖⡈⢆⡉⠒⡄⢢⡉⠖⡘⠤⢃⠣⠌⡑⠈⠄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠐⣉⠆⣑⠣⢌⠓⡌⡚⢤⠣⡙⡜⡰⣉⠃⠀⠀⠀⠠⠜⡱⠂⠀⠀⡌⢣⠜⠀⠀⠀⠀⠀⠀⠀⠀⣌⠳⣌⠣⡜⡌⢦⡉⢎⡱⠣⠜⡓⠀⠀⠀⠀⠀⠀⠀⠀⠰⠌⠱⠃⠙⠀⠁⠠⠀⠀⢀⠌⢂⢏⡞⣽
⢢⡑⢧⣚⢥⡓⣎⢇⢧⠓⣦⡀⠊⠴⡱⢊⡻⠿⠛⠛⠋⢉⠧⣙⠢⡘⢄⢣⡘⠤⢚⡐⠆⢬⠑⡬⢡⠘⢢⠉⠂⡁⠂⠀⢀⠀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠀⠁⠈⠘⠀⠑⠂⠁⠑⠈⠐⠁⠀⠀⠀⠀⠑⠘⠀⠃⠀⠀⠰⠁⠎⠀⠀⠀⠀⠀⠀⠀⠐⠀⠃⠌⠑⠈⠘⠀⠘⠀⠁⠃⠈⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠀⠄⠂⠌⠀⠁⡀⠶⣬⣙⣮⣄⣿⣿
⢢⡑⢧⡙⢦⡹⣘⠎⣞⢧⠰⡛⣆⠀⡀⠻⡟⢓⠒⡒⠀⡌⠲⢄⢣⡙⣌⠲⣌⢣⠣⡜⣘⠢⡉⠔⠁⢈⠀⡰⠠⢄⠡⠈⠄⠂⠄⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠂⢀⡈⣀⣀⣤⣂⢶⡱⣏⢶⣿⣿⣿⣷⣻
⠰⡍⢦⡙⡶⣑⠣⣛⡸⢆⢣⠑⡆⡱⠀⢶⠆⡠⢄⡰⠠⠌⠑⢎⢢⠱⣌⠳⣌⠆⠓⠈⠀⢀⠠⡐⢌⠢⡑⢢⠑⡌⠠⠁⠈⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⠀⡄⡠⠄⡄⣄⢠⡀⢄⡀⣀⠀⡀⢀⡀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⠀⠀⡀⠄⢤⡠⢄⣀⣀⣀⢀⡀⢄⠢⢄⠉⠙⢮⡵⣳⢶⡹⣎⣷⣻⣾⣿⣿⣿⣿⣧
⠰⣙⠦⣙⠶⣉⢳⡌⣇⢯⢣⠳⡘⢥⠋⠄⣬⡄⢠⢀⠆⠘⠈⣈⢀⠗⣈⠁⠀⡀⠠⢀⠌⢢⠑⣌⠢⡑⢌⠂⠡⠀⠀⠀⠀⠀⠀⢀⠠⣀⠠⢀⡐⢰⠒⣆⠦⣄⡡⠀⡘⢀⠣⢛⠼⠴⣣⢝⡮⢶⣡⠯⣜⢣⡜⣣⠝⡜⣆⢖⡠⢄⡠⢀⠀⠠⢠⠐⡤⢤⡐⢤⠒⣌⡐⢠⠐⢠⠩⣝⡲⢤⣌⠠⠙⠮⣵⢪⣍⠷⣚⢦⣏⢦⣭⡹⣖⡳⣍⢧⣛⣏⣿⣿⣿⣿⣿⣿⣿⣿
⠱⡌⢷⢨⣛⢬⠳⣜⡸⢆⢏⡳⣔⠀⠭⠄⣩⣉⠄⠠⠄⡣⠣⠘⣠⣚⢉⠀⡱⢀⠣⢌⢊⡑⠎⡄⢣⠈⠄⠈⠀⠀⡀⢄⠢⡄⢤⢈⠒⣥⢛⡶⡜⣧⢻⢬⡳⣭⢓⡻⢴⡡⢆⣃⠚⡴⡑⠮⡱⣓⢮⢳⡭⢶⣙⢦⠻⣜⠲⣎⠶⣩⠖⡭⢎⡕⢦⣑⢦⡣⣝⣎⢳⣬⣙⢧⢻⣤⡋⠴⣉⡓⠮⣝⡳⡜⣤⠩⡜⢫⣭⡻⣜⢏⡶⢽⣩⣷⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⠰⣉⠶⢣⢞⣡⠟⣰⢃⠯⡜⣱⠹⣎⢀⢻⡵⢺⡤⣤⣤⣶⣾⣷⠘⠻⢸⠀⡱⣈⠱⢊⠆⠉⠐⠀⠀⢀⠠⢤⣄⡐⠈⠮⣵⡹⢦⣏⡜⣤⣉⠚⡽⢼⣙⡶⣽⣒⢯⣓⠯⣞⢳⡜⣫⠴⡭⣖⡱⣌⢎⡳⣭⣻⢼⣫⣟⣮⢷⣎⣳⣣⢯⡝⣮⢭⣧⣏⡶⢽⣲⢮⠷⣮⣙⣮⣳⣬⣝⣻⣦⣝⣶⣜⣷⣽⢶⣯⣿⣷⣶⣿⣾⣾⣿⣿⣷⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⠰⣡⢛⢬⢫⡜⣹⢆⡫⣜⢣⣍⢻⡜⣦⢢⠹⣧⠞⣵⣺⣿⣿⠃⢛⣵⡿⢀⠱⡄⠋⠄⠀⣠⠰⣀⠐⣌⢣⡏⢶⢩⡗⣦⢤⡙⠶⣭⡞⣵⣺⡽⠦⢧⣮⣙⣶⣟⣿⣺⣻⣯⣿⣞⣿⣳⣟⣾⣿⣿⣿⣿⣶⣭⡛⠿⣿⣿⣷⣾⣿⣝⡿⢛⣵⣷⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⠰⡡⢎⢎⡳⣜⡱⢎⡵⢊⡗⣎⢧⡹⢲⢧⡱⡜⢻⠶⣹⣾⡇⢰⣿⡿⠁⠎⠒⠁⢀⢦⡛⣴⢫⡒⣍⢮⣇⠾⣉⠞⣬⢵⡣⢟⡶⡤⣉⣩⣥⡶⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣬⣛⠿⣿⡿⣫⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⢡⠓⡌⢮⡱⢬⡕⢫⡜⣝⡺⣬⢳⡍⢯⣒⢳⡜⢮⡹⢧⣿⣧⠸⠇⠀⣁⡤⢖⢬⡛⣆⢿⣘⢧⡙⢦⢳⣬⢳⡥⢋⡖⣯⡽⠋⣡⡺⣿⣿⣷⣭⣷⣝⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⢟⣬⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⢌⡳⣍⢦⡽⢲⣭⠳⡽⢬⡳⢭⣇⠿⣱⣍⠾⣙⢦⡱⣌⠻⣿⣷⣶⣾⣧⡙⣿⢦⣻⠜⡾⣹⣞⣝⡮⣵⢮⡷⡜⢧⠾⣥⣾⣿⣿⣿⣶⣽⣿⣿⣿⣿⣷⡻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⢌⡓⡮⢜⣹⢣⣎⢷⣩⢳⣍⠷⣬⢻⡥⢮⢽⡘⣧⠝⣮⡱⢝⢿⣿⣿⣿⣿⣶⣶⣭⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣾⣝⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⢌⡳⣙⢮⢳⢣⡞⢦⢇⡳⣎⢽⡲⢭⡞⣹⠮⣵⣩⢞⡲⡹⢦⡑⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣮⣭⣭⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣈⢇⡏⡼⣹⢦⣛⡬⣏⠶⣍⡶⣙⣮⡜⣧⢻⡴⣳⢮⡵⣫⣷⣽⢲⡝⠻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿*/