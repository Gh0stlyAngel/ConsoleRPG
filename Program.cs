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



namespace consoleTextRPG
{
    public class Program
    {

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
                        if ((i + 1) * 106 < str.Length)
                        {
                            if (Char.IsLetter(str[(i + 1) * 106]) && Char.IsLetter(str[((i + 1) * 106) - 1]))
                                Console.Write("-\n   ");
                            else
                                Console.Write("\n   ");
                        }
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

            HealingPotion healingPotion = new HealingPotion();
            ManaPotion manaPotion = new ManaPotion();


            PlayerClass warrior = PlayerClassFactory.CreateInstance(1);
            PlayerClass sorcerer = PlayerClassFactory.CreateInstance(2);
            PlayerClass slayer = PlayerClassFactory.CreateInstance(3);
            PlayerClass archer = PlayerClassFactory.CreateInstance(4);
            Console.CursorVisible = false;
            SlowWrite("ConsoleTextRPG");
            Console.Clear();
            SlowWrite("Введите имя персонажа: ", needClear: false);
            string nickName = Console.ReadLine();
            
            //Welcome(nickName);


            int chosenClass = PlayerPick(warrior, sorcerer, slayer, archer);
            //int chosenClass = 1;
            PlayerClass player = PlayerClassFactory.CreateInstance(chosenClass);
            player.Inventory.AppendItem(healingPotion);
            player.Inventory.AppendItem(manaPotion);


            Hub.ToHub(ref player);

            //Shop.ToShop(player);



            Fight.StartFight(ref player);



            if (player.HP <= 0)
            {
                Console.WriteLine("GameOVER");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("YouWIN");
                Console.ReadKey();
            }

            SlowWrite("Продолжение следует...");

            Console.ReadKey(true);
        }

        static void Welcome(string nickName)
        {
            
            Console.CursorVisible = false;
            SlowWrite($"{nickName} родился и вырос в дереве под названием «». Жили они не особо богато, но на образование и обучение ремеслу хватало. К моменту завершения обучения {nickName} как раз достиг возраста, который позволял ему в большой город, который был в трех днях пути на юг. И уже там {nickName} мог бы применить свои новые навыки для заработка денег, ведь в семье у него ещё был маленький брат, которому скоро тоже нужно было начинать обучаться ремеслам, а деньги это то немногое, чем {nickName} мог отплатить своей семье за все, что она сделала для него.");
            SlowWrite($"Шли недели и месяцы, {nickName} устроился в приличную мастерскую, все было довольно неплохо, до тех пор, пока в одно утро он не получил весточку из своей родной деревни. Развернув сверток, он увидел почерк матери, в котором говорилось, что его родной деревне угрожает опасность, и возможно, он единственный, кто может им помочь. ");
            SlowWrite($"Не понимая, почему матушка выделила его среди прочих, {nickName} оповестил своего работодателя, что ему нужно спешно покинуть город и вернуться в родную деревню. Мастеровой, конечно, не очень хотел отпускать {nickName}, так как был самый сезон работ, и было очень много заказов. Даже начал грозить увольнением.");
            SlowWrite($"«Выбор» Остаться работать(Но совесть замучает, и {nickName} все равно сбежит), или же Настоять на своем (По итогу мастеровой даст заднюю и сохранит за ним место, так как наш {nickName} рукастый)");
            SlowWrite($"По приезду в деревню, {nickName} заметил, что деревня сильно изменился с его последнего визита сюда. Дома обветшали, заборы поломаны, люди вокруг сильно потеряли в лице. {nickName}  первым делом прибежал домой, дабы убедиться, что с его родными все в порядке. Он застал матушку, которая латала раны его отцу, и брата, который помогал ей. «Выбор» Кинуться обнимать родных или сначала выяснить причину, по которой мама вызвала именно его.");
            SlowWrite($"Достав письмо, {nickName} решил узнать у матушки, чем же таким он отличается от других, что она назвала его последней надеждой деревни. ");
            SlowWrite($"Матушка кивнула родным, попросив их покинуть комнату, дабы объясниться с сыном.");
            SlowWrite($"{nickName}, знал ли ты, что когда-то я относилась к древнему роду «Придумать название»? Наш род отличался силой и интеллектом, которые особо ярко проявлялись в мужчинах. Но ты, {nickName}, особенный. Все потому, что при твоем рождении наши жрецы заметили в тебе силу, которая могла поменять баланс добра и зла.");
            SlowWrite($"В связи с чем, мы с твоим отцом провели особый обряд, который частично запечатал твою силу. Но к великому счастью, со временем, как ты креп, печать слабела. И сейчас, тебе уже вполне по силам начать учиться тому, что знал мой клан. ");


            /*            SlowWrite("Нажми любую клавишу чтобы начать...");
                        SlowWrite("Привет!");
                        SlowWrite("Это текстовая РПГ для моего обучения.");
                        SlowWrite("Для начала выбери класс персонажа:");*/
        }


        static int PlayerPick(PlayerClass warrior, PlayerClass sorcerer, PlayerClass slayer, PlayerClass archer)
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
            SlowWrite("Нажми соответствующую цифру для получения подробной информации о классе.", needClear: false, speed: 1);
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
            public PlayerClass(string name, int hp, int mp, int atcRange, int level, int exp, Weapon weapon, PlayerActiveAbility activeAbility, PlayerPassiveAbility passiveAbility)
            {
                Name = name;
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
            public static PlayerClass CreateInstance(int value)
            {
                PlayerActiveAbility ActiveAbility;
                PlayerPassiveAbility PassiveAbility;
                Weapon weapon;
                if (value == 1)
                {
                    ActiveAbility = new PlayerActiveAbility("Изнуряющий удар", $"Воин наносит 15 урона противнику, уменьшая наносимый им урон на 30% на 2 хода. Нанесение урона изнуренному противнику оглушает его.", 15, 6);
                    PassiveAbility = new PlayerPassiveAbility("Нарастающая ярость", "Течение битвы ожесточает воина, увеличивая наносимый им урон на 1 за ход.");
                    weapon = new Weapon("Стандартный меч", 10);
                    return new PlayerClass("Воин", 120, 20, 0, 1, 0, weapon, ActiveAbility, PassiveAbility);
                }
                else if (value == 2)
                {
                    ActiveAbility = new PlayerActiveAbility("Ледяное копье", "Маг поражает противника ледяным копьем, которое наносит 18 урона и замораживает цель на 1 ход.", 18, 15);
                    PassiveAbility = new PlayerPassiveAbility("Благословение богов", "Боги направляют руку мага, что может значительно усилить его заклинания.");
                    weapon = new Weapon("Стандартный посох", 15);
                    return new PlayerClass("Маг", 65, 75, 1, 1, 0, weapon, ActiveAbility, PassiveAbility);
                }
                else if (value == 3)
                {
                    ActiveAbility = new PlayerActiveAbility("Казнь", "Убийца наносит выверенный удар клинком (18 урона). Умение может мгновенно убить противника, если его здоровье ниже 30%.", 23, 11);
                    PassiveAbility = new PlayerPassiveAbility("Ловкость", "Ловкость убийцы позволяет ему уклоняться от ударов противника с вероятностью 15%.");
                    weapon = new Weapon("Стандартный кинжал", 17);
                    return new PlayerClass("Убийца", 90, 30, 0, 1, 0, weapon, ActiveAbility, PassiveAbility);
                }
                else if (value == 4)
                {
                    ActiveAbility = new PlayerActiveAbility("Отступление", "Лучник разрывает дистанцию с противником на 1, нанося 8 урона и обездвиживая его на 1 ход", 8, 9);
                    PassiveAbility = new PlayerPassiveAbility("Меткий глаз", "Меткость лучника позволяет ему наносить дополнительные 3 урона удаленным целям, а также почти никогда не промахиваться.");
                    weapon = new Weapon("Стандартный лук", 12);
                    return new PlayerClass("Лучник", 80, 20, 2, 1, 0, weapon, ActiveAbility, PassiveAbility);
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
            public bool helpYourHome = false;

            public bool firstShopVisit = false;

            public bool[] sealMainQuest = { false, false };

            public bool[] headmanPersonalQuest = { false, false };

            public bool[] traderQuest = { false, false };

            public bool[] friendQuest = { false, false };

            public bool[] headmanMainQuest = { false, false };

            public bool[] tampleQuest = { false, false };

            public bool[] blacksmithMainQuest = { false, false };

            public bool[] herbalistMainQuest = { false, false };

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