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


namespace consoleTextRPG
{
    internal class Program
    {

        internal static void Block()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key != null)
                {
                    // Ignore letters
                    continue;
                }
            } while (key.Key != ConsoleKey.Enter);
        }
        static void Main(string[] args)
        {

            Weapon baseWarriorWeapon = new Weapon("Стандартный меч", 10);
            Weapon baseSorcererWeapon = new Weapon("Стандартный посох", 17);
            Weapon baseSlayerWeapon = new Weapon("Стандартный кинжал", 15);
            Weapon baseArcherWeapon = new Weapon("Стандартный лук", 12);

            Warrior warrior = new Warrior("Воин", 120, 20, 0, 1, 0, baseWarriorWeapon);
            Sorcerer sorcerer = new Sorcerer("Маг", 70, 60, 1, 1, 0, baseSorcererWeapon);
            Slayer slayer = new Slayer("Убийца", 90, 30, 0, 1, 0, baseSlayerWeapon);
            Archer archer = new Archer("Лучник", 80, 20, 1, 1, 0, baseArcherWeapon);




/*            Welcome();


            int chosenClass = PlayerPick(warrior, sorcerer, slayer, archer);*/

            int chosenClass = 1;
            PlayerClass player = PlayerClassFactory.CreateInstance(chosenClass);

            Fight.StartFight(ref player);

            if (warrior.HP <= 0)
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

            while (true)
            {
                Console.ReadKey(true);
            }


        }

        static void Welcome()
        {
            Console.CursorVisible = false;
            SlowWrite("Нажми любую клавишу чтобы начать...");
            SlowWrite("Привет!");
            SlowWrite("Это текстовая РПГ для моего обучения.");
            SlowWrite("Для начала выбери класс персонажа:");
        }

        static int PlayerPick(Warrior warrior, Sorcerer sorcerer, Slayer slayer, Archer archer)
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

        static void SlowWrite(string str, ConsoleColor textColor = ConsoleColor.Yellow, bool needClear = true)
        {

            Console.ForegroundColor = textColor;

            if (needClear)
            {
                Console.Clear();

            }
            if (str.Length < 106)
            {
                Console.Write("\n   ");
                foreach (char letter in str)
                {
                    Console.Write(letter);
                    Thread.Sleep(14);
                }
            }
            else
            {
                Console.Write("\n   ");
                for (int i = 0; i < 106; i++)
                {
                    Console.Write(str[i]);
                    Thread.Sleep(14);
                }
                if (str[106] == ' ')
                    Console.Write("\n   ");
                else
                    Console.Write("-\n   ");
                for (int i = 106; i < str.Length; i++)
                {
                    Console.Write(str[i]);
                    Thread.Sleep(14);
                }
            }

            if (needClear)
            {
                Console.ReadKey(true);

            }

        }


        static void ShowClassList(Warrior warrior, Sorcerer sorcerer, Slayer slayer, Archer archer)
        {
            Console.Clear();
            SlowWrite($"1. {warrior.Name}", ConsoleColor.Red, false);
            SlowWrite($"2. {sorcerer.Name}", ConsoleColor.Blue, false);
            SlowWrite($"3. {slayer.Name}", ConsoleColor.DarkRed, false);
            SlowWrite($"4. {archer.Name}", ConsoleColor.Green, false);
            Console.WriteLine();
            SlowWrite("Нажми соответствующую цифру для получения подробной информации о классе.", needClear: false);
        }

        static bool ChoisingClass(Warrior warrior, Sorcerer sorcerer, Slayer slayer, Archer archer, ConsoleKeyInfo pressedKey, ref int chosenClass)
        {

            string text = "\n   Нажмите Enter для выбора класса или любую другую клавишу чтобы вернуться к списку классов.";
            bool inChoice = true;
            switch (pressedKey.Key)
            {
                case ConsoleKey.D1:
                    warrior.ShowStats();
                    SlowWrite(text, needClear: false);
                    break;
                case ConsoleKey.D2:
                    sorcerer.ShowStats();
                    SlowWrite(text, needClear: false);
                    break;
                case ConsoleKey.D3:
                    slayer.ShowStats();
                    SlowWrite(text, needClear: false);
                    break;
                case ConsoleKey.D4:
                    archer.ShowStats();
                    SlowWrite(text, needClear: false);
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

        internal abstract class BaseClass
        {

            public string Name { get; private set; }
            public int HP { get; private set; }
            public int MP { get; private set; }
            public int AtcRange { get; private set; }
            public int Level { get; private set; }
            public int EXP { get; private set; }

            public int MaxHP { get; private set; }

            public int MaxMP { get; private set; }

            public Weapon Weapon { get; private set; }

            public BaseClass(string name, int hp, int mp, int atcRange, int level, int exp, Weapon weapon)
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
            }

            public virtual void ShowStats()
            {
                Console.Clear();
                SlowWrite($"{Name}\n", ConsoleColor.Yellow, false);
                SlowWrite($"Здоровье: {HP}", ConsoleColor.Yellow, false);
                SlowWrite($"Мана: {MP}", ConsoleColor.Yellow, false);
                if (AtcRange < 1)
                    SlowWrite("Ближний бой", ConsoleColor.Yellow, false);
                else
                    SlowWrite("Дальний бой", ConsoleColor.Yellow, false);

                Console.WriteLine();
                SlowWrite($"Снаряжение: {Weapon.Name}  Урон: {Weapon.Damage}", ConsoleColor.Yellow, false);

                Console.WriteLine();



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
            public Weapon Weapon { get; private set; }
            public PlayerActiveAbility ActiveAbility { get; private set; }
            public PlayerPassiveAbility PassiveAbility { get; private set; }
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
            }

            public virtual void ShowStats()
            {
                Console.Clear();
                SlowWrite($"{Name}\n", ConsoleColor.Yellow, false);
                SlowWrite($"Здоровье: {HP}", ConsoleColor.Yellow, false);
                SlowWrite($"Мана: {MP}", ConsoleColor.Yellow, false);
                if (AtcRange < 1)
                    SlowWrite("Ближний бой", ConsoleColor.Yellow, false);
                else
                    SlowWrite("Дальний бой", ConsoleColor.Yellow, false);

                Console.WriteLine();
                SlowWrite($"Снаряжение: {Weapon.Name}  Урон: {Weapon.Damage}", ConsoleColor.Yellow, false);

                Console.WriteLine();

                SlowWrite(ActiveAbility.Name, needClear: false);
                SlowWrite(ActiveAbility.Description, needClear: false);
                Console.WriteLine();
                SlowWrite(PassiveAbility.Name, needClear: false);
                SlowWrite(PassiveAbility.Description, needClear: false);
                Console.WriteLine();



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

            public void restoreHP(int RestoredHP)
            {
                HP += RestoredHP;
            }


        }

        internal class Warrior : BaseClass
        {
            public WarriorActiveAbility ActiveAbility { get; private set; }

            public WarriorPassiveAbility PassiveAbility { get; private set; }
            public Warrior(string name, int hp, int mp, int atcRange, int level, int exp, Weapon weapon) : base(name, hp, mp, atcRange, level, exp, weapon)
            {
                ActiveAbility = new WarriorActiveAbility("Калечащий удар", "Воин наносит сильный удар противнику, уменьшая наносимый им урон на 2 хода. Нанесение урона покалеченному противнику оглушает его.");
                PassiveAbility = new WarriorPassiveAbility("Нарастающая ярость", "Течение битвы ожесточает воина, увеличивая наносимый им урон.");
            }

            public override void ShowStats()
            {
                base.ShowStats();
                SlowWrite(ActiveAbility.Name, needClear: false);
                SlowWrite(ActiveAbility.Description, needClear: false);
                Console.WriteLine();
                SlowWrite(PassiveAbility.Name, needClear: false);
                SlowWrite(PassiveAbility.Description, needClear: false);
                Console.WriteLine();
            }

        }

        internal class Sorcerer : BaseClass
        {
            public SorcererActiveAbility ActiveAbility { get; private set; }

            public SorcererPassiveAbility PassiveAbility { get; private set; }

            public Sorcerer(string name, int hp, int mp, int atcRange, int level, int exp, Weapon weapon) : base(name, hp, mp, atcRange, level, exp, weapon)
            {

                ActiveAbility = new SorcererActiveAbility("Ледяное копье", "Маг поражает противника ледяным копьем, которое наносит урон замораживает цель на 1 ход.");
                PassiveAbility = new SorcererPassiveAbility("Благословение богов", "Боги направляют руку мага, что может значительно усилить его заклинания.");

            }
            public override void ShowStats()
            {
                base.ShowStats();
                SlowWrite(ActiveAbility.Name, needClear: false);
                SlowWrite(ActiveAbility.Description, needClear: false);
                Console.WriteLine();
                SlowWrite(PassiveAbility.Name, needClear: false);
                SlowWrite(PassiveAbility.Description, needClear: false);
                Console.WriteLine();
            }
        }

        internal class Slayer : BaseClass
        {
            public SlayerActiveAbility ActiveAbility { get; private set; }

            public SlayerPassiveAbility PassiveAbility { get; private set; }
            public Slayer(string name, int hp, int mp, int atcRange, int level, int exp, Weapon weapon) : base(name, hp, mp, atcRange, level, exp, weapon)
            {

                ActiveAbility = new SlayerActiveAbility("Казнь", "Убийца наносит выверенный удар клинком. Чем серьезнее противник ранен, тем выше вероятность, что умение может мгновенно убить его.");
                PassiveAbility = new SlayerPassiveAbility("Ловкость", "Ловкость убийцы позволяет ему уклоняться от ударов противника.");

            }
            public override void ShowStats()
            {
                base.ShowStats();
                SlowWrite(ActiveAbility.Name, needClear: false);
                SlowWrite(ActiveAbility.Description, needClear: false);
                Console.WriteLine();
                SlowWrite(PassiveAbility.Name, needClear: false);
                SlowWrite(PassiveAbility.Description, needClear: false);
                Console.WriteLine();
            }
        }

        internal class Archer : BaseClass
        {
            public ArcherActiveAbility ActiveAbility { get; private set; }

            public ArcherPassiveAbility PassiveAbility { get; private set; }
            public Archer(string name, int hp, int mp, int atcRange, int level, int exp, Weapon weapon) : base(name, hp, mp, atcRange, level, exp, weapon)
            {

                ActiveAbility = new ArcherActiveAbility("Отступление", "Лучник разрывает дистанцию с противником, нанося урон.");
                PassiveAbility = new ArcherPassiveAbility("Меткий глаз", "Меткость лучника позволяет ему наносить дополнительный урон удаленным целям, а также почти никогда не промахиваться.");

            }
            public override void ShowStats()
            {
                base.ShowStats();
                SlowWrite(ActiveAbility.Name, needClear: false);
                SlowWrite(ActiveAbility.Description, needClear: false);
                Console.WriteLine();
                SlowWrite(PassiveAbility.Name, needClear: false);
                SlowWrite(PassiveAbility.Description, needClear: false);
                Console.WriteLine();
            }
        }

        internal class ClassFactory
        {
            public static BaseClass CreateInstance(int value)
            {
                if (value == 1)
                {
                    Weapon weapon1 = new Weapon("Стандартный меч", 10);
                    return new Warrior("Воин", 120, 20, 0, 1, 0, weapon1);
                }
                else if (value == 2)
                {
                    Weapon weapon2 = new Weapon("Стандартный посох", 17);
                    return new Sorcerer("Маг", 70, 60, 1, 1, 0, weapon2);
                }
                else if (value == 3)
                {
                    Weapon weapon3 = new Weapon("Стандартный кинжал", 15);
                    return new Slayer("Убийца", 90, 30, 0, 1, 0, weapon3);
                }
                else if (value == 4)
                {
                    Weapon weapon4 = new Weapon("Стандартный лук", 12);
                    return new Archer("Лучник", 80, 20, 1, 1, 0, weapon4);
                }
                else { throw new ArgumentException("Неверный класс"); }

            }

        }
        internal class PlayerClassFactory
        {
            public static PlayerClass CreateInstance(int value)
            {
                PlayerActiveAbility ActiveAbility;
                PlayerPassiveAbility PassiveAbility;
                if (value == 1)
                {
                    ActiveAbility = new PlayerActiveAbility("Калечащий удар", "Воин наносит сильный удар противнику, уменьшая наносимый им урон на 2 хода. Нанесение урона покалеченному противнику оглушает его.");
                    PassiveAbility = new PlayerPassiveAbility("Нарастающая ярость", "Течение битвы ожесточает воина, увеличивая наносимый им урон.");
                    Weapon weapon1 = new Weapon("Стандартный меч", 10);
                    return new PlayerClass("Воин", 120, 20, 0, 1, 0, weapon1, ActiveAbility, PassiveAbility);
                }
                else if (value == 2)
                {
                    ActiveAbility = new PlayerActiveAbility("Ледяное копье", "Маг поражает противника ледяным копьем, которое наносит урон замораживает цель на 1 ход.");
                    PassiveAbility = new PlayerPassiveAbility("Благословение богов", "Боги направляют руку мага, что может значительно усилить его заклинания.");
                    Weapon weapon2 = new Weapon("Стандартный посох", 17);
                    return new PlayerClass("Маг", 70, 60, 1, 1, 0, weapon2, ActiveAbility, PassiveAbility);
                }
                else if (value == 3)
                {
                    ActiveAbility = new PlayerActiveAbility("Казнь", "Убийца наносит выверенный удар клинком. Чем серьезнее противник ранен, тем выше вероятность, что умение может мгновенно убить его.");
                    PassiveAbility = new PlayerPassiveAbility("Ловкость", "Ловкость убийцы позволяет ему уклоняться от ударов противника.");
                    Weapon weapon3 = new Weapon("Стандартный кинжал", 15);
                    return new PlayerClass("Убийца", 90, 30, 0, 1, 0, weapon3, ActiveAbility, PassiveAbility);
                }
                else if (value == 4)
                {
                    ActiveAbility = new PlayerActiveAbility("Отступление", "Лучник разрывает дистанцию с противником, нанося урон.");
                    PassiveAbility = new PlayerPassiveAbility("Меткий глаз", "Меткость лучника позволяет ему наносить дополнительный урон удаленным целям, а также почти никогда не промахиваться.");
                    Weapon weapon4 = new Weapon("Стандартный лук", 12);
                    return new PlayerClass("Лучник", 80, 20, 1, 1, 0, weapon4, ActiveAbility, PassiveAbility);
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
            public string Name { get; set; }
            public string Description { get; set; }
            public PlayerBaseAbility(string name, string description)
            {
                Name = name;
                Description = description;
            }
        }

        internal class PlayerActiveAbility : PlayerBaseAbility
        {
            public PlayerActiveAbility(string name, string description) : base(name, description)
            {

            }
        }

        internal class PlayerPassiveAbility : PlayerBaseAbility
        {
            public PlayerPassiveAbility(string name, string description) : base(name, description)
            {

            }
        }


        internal class BaseAbility
        {
            public string Name { get; set; }
            public string Description { get; set; }

            public BaseAbility(string name, string description)
            {
                Name = name;
                Description = description;
            }
        }

        internal class BaseActiveAbility : BaseAbility
        {
            public BaseActiveAbility(string name, string description) : base(name, description)
            {

            }
        }

        internal class BasePassiveAbility : BaseAbility
        {
            public BasePassiveAbility(string name, string description) : base(name, description)
            {

            }
        }

        internal class WarriorActiveAbility : BaseActiveAbility
        {
            public WarriorActiveAbility(string name, string description) : base(name, description)
            {

            }
        }

        internal class WarriorPassiveAbility : BasePassiveAbility
        {
            public WarriorPassiveAbility(string name, string description) : base(name, description)
            {

            }
        }

        internal class SorcererActiveAbility : BaseActiveAbility
        {
            public SorcererActiveAbility(string name, string description) : base(name, description)
            {

            }
        }

        internal class SorcererPassiveAbility : BasePassiveAbility
        {
            public SorcererPassiveAbility(string name, string description) : base(name, description)
            {

            }
        }

        internal class SlayerActiveAbility : BaseActiveAbility
        {
            public SlayerActiveAbility(string name, string description) : base(name, description)
            {

            }
        }

        internal class SlayerPassiveAbility : BasePassiveAbility
        {
            public SlayerPassiveAbility(string name, string description) : base(name, description)
            {

            }
        }

        internal class ArcherActiveAbility : BaseActiveAbility
        {
            public ArcherActiveAbility(string name, string description) : base(name, description)
            {

            }
        }

        internal class ArcherPassiveAbility : BasePassiveAbility
        {
            public ArcherPassiveAbility(string name, string description) : base(name, description)
            {

            }
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