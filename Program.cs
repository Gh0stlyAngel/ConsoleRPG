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



namespace consoleTextRPG
{
    public class Program
    {
        internal class MapList
        {
            BridgeThirdEvents BridgeThirdEvents = new BridgeThirdEvents();
            public static Map BridgeThird;

            BridgeSecondEvents BridgeSecondEvents = new BridgeSecondEvents();
            public static Map BridgeSecond;

            BridgeFirstEvents BridgeFirstEvents = new BridgeFirstEvents();
            public static Map BridgeFirst;

            HubEvents HubEvents = new HubEvents();
            public static Map Hub;

            public MapList()
            {
                BridgeThird = new Map(BridgeThirdEvents);
                BridgeSecond = new Map(BridgeSecondEvents);
                BridgeFirst = new Map(BridgeFirstEvents);
                Hub = new Map(HubEvents);
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
                    Console.Write("\n   ");
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
                        /*                        if ((i + 1) * 106 < str.Length)
                                                {
                                                    if (Char.IsLetter(str[(i + 1) * 106]) && Char.IsLetter(str[((i + 1) * 106) - 1]))
                                                        Console.Write("-\n   ");
                                                    else
                                                        Console.Write("\n   ");
                                                }*/
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
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            string[] separator = { "\r\n" };
            string MapString = "#############################################################################################################################\r\n#                          ##                                                                                               #\r\n#                         ##                             ###############################################################    #\r\n#                        ##                              ############################################# |    ########## #    #\r\n#                       ##                               ################################################   ########## #    #\r\n#                     ##                                 #######    #########   #########               #   ########## #    #\r\n|                      ##                                #######    #       #   #       #               #   ########## #    #\r\n|                       ######                           #######    #       #   #       #               #       %      #    #\r\n|               ##           #########                   #######    #########   #########   #############              #    #\r\n#               # ##               ##########            #######                                    %                  #    #\r\n#               #  ##                       ##         #######                                      %                  #    #\r\n#               #   ####                         %                                          #############              #    #\r\n#               #      ###########                     #######                                          #              #    #\r\n#               #               ############             #######                                        ################    #\r\n#              ##                                        #######                                                       #    #\r\n#             ##                                         #######                                                       #    #\r\n#            ##                                          #######                                                       #    #\r\n#           ##                                           ####### #########             %              #########        #    #\r\n#          ##                                            ####### #       #                            #       #        #    #\r\n#         ##                                             ####### #       #           #####            #       #        #    #\r\n#        ##                                              ####### #########       %   #####   %        #########        #    #\r\n#       ##                                               #######                     #####                             #    #\r\n#     ###                                                ##########################################################    #    #\r\n#    ###                                                 ###################################################### |      #    #\r\n#  ###                                                   ###############################################################    #\r\n####                                                                                                                        #\r\n#                                                                                                                           #\r\n#############################################################################################################################";
            string[] mapArray = MapString.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (string mapItem in mapArray)
            {
                Console.WriteLine(mapItem);
            }

            Console.ReadKey();
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
            SlowWrite("Введите имя персонажа: ", needClear: false);
            string nickName = Console.ReadLine();

            Hub.Welcome(nickName, ref story);

            int chosenClass = 1;
            PlayerClass player = PlayerClassFactory.CreateInstance(chosenClass, nickName);
            player.Inventory.AppendItem(healingPotion);
            player.Inventory.AppendItem(manaPotion);

            

            Maps.GoToMap(ref player, ref story, ref MapList.BridgeFirst, MapList.BridgeFirst.PlayerPosX, MapList.BridgeFirst.PlayerPosY);


            

            Maps.GoToMap(ref player, ref story, ref MapList.Hub, MapList.Hub.PlayerPosX, MapList.Hub.PlayerPosY);
            

            SlowWrite("Продолжение следует...");



            
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


        public static void SlowWrite(string str, ConsoleColor textColor = ConsoleColor.Yellow, bool needClear = true, int speed = 14, string teller = null)
        {
            object[] parameters = { str, textColor, needClear, speed, teller };
            writeThread t1 = new writeThread(parameters);

            if (needClear)
            {
                Console.ReadKey(true);

                t1.thread.Abort();


            }
            else
            {
                while (t1.thread.IsAlive)
                {
                    Thread.Sleep(0);
                } 
                
            }

        }


        static void ShowClassList(PlayerClass warrior, PlayerClass sorcerer, PlayerClass slayer, PlayerClass archer)
        {
            Console.Clear();
            SlowWrite($"1. {warrior.Name}", ConsoleColor.Red, false, speed: 1);
            SlowWrite($"2. {sorcerer.Name}", ConsoleColor.Blue, false, speed: 1);
            SlowWrite($"3. {slayer.Name}", ConsoleColor.DarkRed, false, speed: 1);
            SlowWrite($"4. {archer.Name}", ConsoleColor.Green, false, speed: 1);
            Console.WriteLine();
            SlowWrite("Нажми соответствующую цифру для получения подробной информации о классе.", needClear: false, speed: 0);
        }

        static bool ChoisingClass(PlayerClass warrior, PlayerClass sorcerer, PlayerClass slayer, PlayerClass archer, ConsoleKeyInfo pressedKey, ref int chosenClass)
        {

            string text = "\n   Нажмите Enter для выбора класса или любую другую клавишу чтобы вернуться к списку классов.";
            bool inChoice = true;
            switch (pressedKey.Key)
            {
                case ConsoleKey.D1:
                    warrior.ShowStats(false);
                    SlowWrite(text, needClear: false, speed: 0);
                    break;
                case ConsoleKey.D2:
                    sorcerer.ShowStats(false);
                    SlowWrite(text, needClear: false, speed: 0);
                    break;
                case ConsoleKey.D3:
                    slayer.ShowStats(false);
                    SlowWrite(text, needClear: false, speed: 0);
                    break;
                case ConsoleKey.D4:
                    archer.ShowStats(false);
                    SlowWrite(text, needClear: false, speed: 0);
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
                SlowWrite($"{Name}\n", ConsoleColor.Yellow, false, speed: 0);
                SlowWrite($"Здоровье: {HP}", ConsoleColor.Yellow, false, speed: 0);
                SlowWrite($"Мана: {MP}", ConsoleColor.Yellow, false, speed: 0);
                if (AtcRange < 1)
                    SlowWrite("Ближний бой", ConsoleColor.Yellow, false, speed: 0);
                else
                    SlowWrite("Дальний бой", ConsoleColor.Yellow, false, speed: 0);

                Console.WriteLine();
                SlowWrite($"Снаряжение: {Weapon.Name}  Урон: {Weapon.Damage}", ConsoleColor.Yellow, false, speed: 0);

                Console.WriteLine();

                SlowWrite(ActiveAbility.Name, needClear: false, speed: 0);
                SlowWrite(ActiveAbility.Description, needClear: false, speed: 0);
                Console.WriteLine();
                SlowWrite(PassiveAbility.Name, needClear: false, speed: 0);
                SlowWrite(PassiveAbility.Description, needClear: false, speed: 0);
                Console.WriteLine();

                if (showPotions)
                {
                    Inventory.ShowItems();
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
                    weapon = new Weapon("Стандартный меч", 10);
                    return new PlayerClass("Воин", nickName, 120, 20, 0, 1, 0, weapon, ActiveAbility, PassiveAbility);
                }
                else if (value == 2)
                {
                    ActiveAbility = new PlayerActiveAbility("Ледяное копье", "Маг поражает противника ледяным копьем, которое наносит 18 урона и замораживает цель на 1 ход.", 18, 15);
                    PassiveAbility = new PlayerPassiveAbility("Благословение богов", "Боги направляют руку мага, что может значительно усилить его заклинания.");
                    weapon = new Weapon("Стандартный посох", 15);
                    return new PlayerClass("Маг", nickName, 65, 75, 1, 1, 1, weapon, ActiveAbility, PassiveAbility);
                }
                else if (value == 3)
                {
                    ActiveAbility = new PlayerActiveAbility("Казнь", "Убийца наносит выверенный удар клинком (23 урона). Умение может мгновенно убить противника, если его здоровье ниже 30%.", 23, 11);
                    PassiveAbility = new PlayerPassiveAbility("Ловкость", "Ловкость убийцы позволяет ему уклоняться от ударов противника с вероятностью 15%.");
                    weapon = new Weapon("Стандартный кинжал", 17);
                    return new PlayerClass("Убийца", nickName, 90, 30, 0, 1, 0, weapon, ActiveAbility, PassiveAbility);
                }
                else if (value == 4)
                {
                    ActiveAbility = new PlayerActiveAbility("Отступление", "Лучник разрывает дистанцию с противником на 1, нанося 8 урона и обездвиживая его на 1 ход", 8, 9);
                    PassiveAbility = new PlayerPassiveAbility("Меткий глаз", "Меткость лучника позволяет ему наносить дополнительные 3 урона удаленным целям, а также почти никогда не промахиваться.");
                    weapon = new Weapon("Стандартный лук", 12);
                    return new PlayerClass("Лучник", nickName, 80, 20, 2, 1, 0, weapon, ActiveAbility, PassiveAbility);
                }
                else { throw new ArgumentException("Неверный класс"); }

            }

        }

        internal class Weapon
        {
            public string Name { get; private set; }
            public int Damage { get; private set; }
            public Weapon(string weaponName, int weaponDamage) 
            {
                Name = weaponName;
                Damage = weaponDamage;
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

            public void ShowItems(bool inFight = false)
            {

                int yPos;
                if (inFight)
                    yPos = 2;
                else
                    yPos = 25;
                foreach (Item item in playerItems)
                {
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


        }

        internal abstract class Item
        {
            public string Name { get; protected set; }

            public int AmountOfItems {  get; private set; }
            public Item(string name)
            {
                Name = name;
                AmountOfItems = 0;
            }

            public void AddItem()
            {
                AmountOfItems++;
            }
            
            public Item RemoveItem()
            {
                if ( AmountOfItems <= 0 )
                    return this;
                AmountOfItems--;
                return this;
            }
        }

        internal abstract class Potion : Item
        {
            public int RestoreValue { get; protected set; }
            public Potion(string name) : base(name)
            {

            }

        }

        internal class HealingPotion : Potion
        {
            public HealingPotion(string name = "") : base(name)
            {
                Name = "Зелье лечения";
                RestoreValue = 20;
            }
        }

        internal class ManaPotion : Potion
        {
            public ManaPotion(string name = "") : base(name)
            {
                Name = "Зелье маны";
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


            public Quest FirstVisitHomeQuest;


            public Quest SealMainQuest;


            public Quest HeadmanPersonalQuest;


            public Quest TraderQuest;
   

            public Quest FriendQuest;
   

            public Quest HeadmanMainQuest;
        

            public Quest TempleQuest;
   

            public Quest BlacksmithMainQuest;
           

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

                descriptions = new string[] { "Изучить обломки обозов около моста, недалеко от деревни." };
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
                BlacksmithMainQuest = new Quest("blacksmithMainQuest", descriptions);
                Quests.Add(BlacksmithMainQuest);

                descriptions = new string[] { "@Описание@" };
                HerbalistMainQuest = new Quest("herbalistMainQuest", descriptions);
                Quests.Add(HerbalistMainQuest);

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
                                SlowWrite($"{integer}. {quest.Name}", needClear: false, speed: 0);
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
                        
                        SlowWrite($"{questName}", speed: 0, needClear: false);
                        Console.WriteLine();
                        SlowWrite($"{questDescription}", speed: 0, needClear: false);
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