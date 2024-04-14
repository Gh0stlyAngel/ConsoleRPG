using ConsoleShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static consoleTextRPG.Program;

namespace consoleTextRPG
{
    internal class MainCampFirstEvents : MapEvents
    {
        public MainCampFirstEvents()
        {
            MapString = "#############################################################################################################################\r\n#                          ##                                                                                               #\r\n#                         ##                             ###############################################################    #\r\n#                        ##                              #############################################  |              #    #\r\n#                       ##                               ################################################   ########## #    #\r\n#                     ##                                 #######    #########   #########               #   ########## #    #\r\n|                      ##                                #######    #       #   #       #               #   ########## #    #\r\n|                       ######                           #######    #       #   #       #               #   ####--#### #    #\r\n|               ##           #########                   #######    #########   #########   #############              #    #\r\n#               # ##               ##########            #######                                                       #    #\r\n#               #  ##                       ##         #######                                                         #    #\r\n#               #   ####                                                                    #############              #    #\r\n#               #      ###########                     #######                                          #              #    #\r\n#               #               ############             #######                                        ################    #\r\n#              ##                                        #######                                                       #    #\r\n#             ##                                         #######                                                       #    #\r\n#            ##                                          #######                                                       #    #\r\n#           ##                                           ####### #########                            #########        #    #\r\n#          ##                                            ####### #       #                            #       #        #    #\r\n#         ##                                             ####### #       #           #####            #       #        #    #\r\n#        ##                                              ####### #########           #####            #########        #    #\r\n#       ##                                               #######                     #####                             #    #\r\n#     ###                                                #########################################################     #    #\r\n#    ###                                                 ####################################################### |     #    #\r\n#  ###                                                   ###############################################################    #\r\n####                                                                                                                        #\r\n#                                                                                                                           #\r\n#############################################################################################################################";
            PlayerPosX = 1;
            PlayerPosY = 7;
            SpawnOnStartPosition = false;
            Triggers = new char[] {'|'};

            int[][] toOutside = new[]
            {
                new int[]{ 0, 6 },
                new int[]{ 0, 7 },
                new int[]{ 0, 8 }
            };
            EventsDictionary.Add(toOutside, EventName.OutsideMainCamp);

            int[][] mainCampSecond = new[]
            {
                new int[]{ 12, 1 },
                new int[]{ 12, 2 },
                new int[]{ 12, 3 },
                new int[]{ 12, 4 },
                new int[]{ 12, 5 },
                new int[]{ 12, 6 },
                new int[]{ 12, 7 },
                new int[]{ 12, 8 },
                new int[]{ 12, 9 },
                new int[]{ 12, 10 },
                new int[]{ 12, 11 },
                new int[]{ 12, 12 },
                new int[]{ 12, 13 },
                new int[]{ 12, 14 },
                new int[]{ 12, 15 },
                new int[]{ 12, 16 }

            };
            InvisibleEventsDictionary.Add(mainCampSecond, EventName.MainCampSecond);
        }

        internal override bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            bool goOut = false;
            switch (way)
            {
                case (int)EventName.MainCampSecond:
                    goOut = true;
                    ToMainCampSecond(ref player, ref story);
                    break;
                case (int)EventName.OutsideMainCamp:
                    goOut = true;
                    ToOutside(ref player, ref story);
                    break;
                default: break;

            }
            return goOut;
        }

        internal static void ToMainCampSecond(ref PlayerClass player, ref Story story)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("123");
            SlowWrite("Осматриваем лагерь, видим патрули", speed: 1, needClear: true, ableToSkip: true);

            Maps.GoToMap(ref player, ref story, ref MapList.MainCampSecond, MapList.MainCampSecond.PlayerPosX, MapList.MainCampSecond.PlayerPosY);
        }

        internal static void ToOutside(ref PlayerClass player, ref Story story)
        {  
            HubEvents.ToOutside(ref player, ref story);
        }

    }


    class MainCampSecondEvents: MapEvents
    {
        public MainCampSecondEvents(List<MapEnemy> enemies)
        {
            Enemies = enemies;

            MapString = "#############################################################################################################################\r\n#                          ##                                                                                               #\r\n#                         ##                             ###############################################################    #\r\n#                        ##                              #############################################  |              #    #\r\n#                       ##                               ################################################   ########## #    #\r\n#                     ##                                 #######    #########   #########               #   ########## #    #\r\n|                      ##                                #######    #       #   #       #               #   ########## #    #\r\n|                       ######                           #######    #       #   #       #               #   ####--#### #    #\r\n|               ##           #########                   #######    #########   #########   #############              #    #\r\n#               # ##               ##########            #######                                                       #    #\r\n#               #  ##                       ##         #######                                                         #    #\r\n#               #   ####                                                                    #############              #    #\r\n#               #      ###########                     #######                                          #              #    #\r\n#               #               ############             #######                                        ################    #\r\n#              ##                                        #######                                                       #    #\r\n#             ##                                         #######                                                       #    #\r\n#            ##                                          #######                                                       #    #\r\n#           ##                                           ####### #########                            #########        #    #\r\n#          ##                                            ####### #       #                            #       #        #    #\r\n#         ##                                             ####### #       #           #####            #       #        #    #\r\n#        ##                                              ####### #########           #####            #########        #    #\r\n#       ##                                               #######                     #####                             #    #\r\n#     ###                                                #########################################################     #    #\r\n#    ###                                                 ####################################################### |     #    #\r\n#  ###                                                   ###############################################################    #\r\n####                                                                                                                        #\r\n#                                                                                                                           #\r\n#############################################################################################################################";
            PlayerPosX = 12;
            PlayerPosY = 7;
            SpawnOnStartPosition = false;
            Triggers = new char[] { '|', '-' };

            int[][] toOutside = new[]
            {
                new int[]{ 0, 6 },
                new int[]{ 0, 7 },
                new int[]{ 0, 8 }
            };
            EventsDictionary.Add(toOutside, EventName.OutsideMainCamp);

            int[][] toSouthLadder = new[]
            {
                new int[]{ 113, 23 }
            };
            EventsDictionary.Add(toSouthLadder, EventName.ToSouthLadder);

            int[][] toNorthLadder = new[]
            {
                new int[]{ 104, 3 }
            };
            EventsDictionary.Add(toNorthLadder, EventName.ToNorthLadder);

            int[][] freeVillagers = new[]
            {
                new int[]{ 112, 7 },
                new int[]{ 113, 7 }
            };
            EventsDictionary.Add(freeVillagers, EventName.FreeVillagers);
        }


        internal override bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            bool goOut = false;
            switch (way)
            {
                case (int)EventName.ToSouthLadder:
                    goOut = true;
                    SouthLadder(ref player, ref story);
                    break;

                case (int)EventName.ToNorthLadder:
                    goOut = true;
                    NorthLadder(ref player, ref story);
                    break;

                case (int)EventName.OutsideMainCamp:
                    if (story.FreeVillagers && !story.HeadmanMainQuest.QuestCompleted)
                    {
                        story.HeadmanMainQuest.CompleteQuest();
                    }
                    goOut = true;
                    ToOutside(ref player, ref story);
                    break;

                case (int)EventName.FreeVillagers:
                    Item key = player.Inventory.playerItems.Find(item => item.Name == "Ключ от клетки");
                    if (key == null)
                        SlowWrite("Нужно найти ключ от клетки.", speed: 1, needClear: true, ableToSkip: true);
                    // if key in inventory
                    else if (story.FreeVillagers == false)
                    {
                        story.FreeVillagers = true;
                        SlowWrite("освобождаем жителей", needClear: true, ableToSkip: true);
                    }
                    else
                    {
                        story.FreeVillagers = false;
                        SlowWrite("Вы закрыли жителей обратно в клетку. Зачем?!", needClear: true, ableToSkip: true);
                        //SlowWrite("Нужно вывести освобожденных жителей за пределы лагеря.", needClear: true, ableToSkip: true);
                    }
                    break;

                default: break;

            }
            return goOut;
        }

        internal static void ToOutside(ref PlayerClass player, ref Story story)
        {
            HubEvents.ToOutside(ref player, ref story);
        }

        internal static void SouthLadder(ref PlayerClass player, ref Story story)
        {
            // MainCampThird
            Maps.GoToMap(ref player, ref story, ref MapList.MainCampThird, MapList.MainCampThird.PlayerPosX, MapList.MainCampThird.PlayerPosY);
        }
        
        internal static void NorthLadder(ref PlayerClass player, ref Story story)
        {
            // if FreeVillagers
            // MainCampFourth
            // else MainCampThird
            if (story.FreeVillagers)
                Maps.GoToMap(ref player, ref story, ref MapList.MainCampFourth, MapList.MainCampFourth.PlayerPosX, MapList.MainCampFourth.PlayerPosY);
            else
                Maps.GoToMap(ref player, ref story, ref MapList.MainCampThird, 103, 3);


        }
    }

    class MainCampThirdEvents: MapEvents
    {
        public MainCampThirdEvents(List<MapEnemy> enemies)
        {
            Enemies = enemies;

            MapString = "#############################################################################################################################\r\n#                          ##                                                                                               #\r\n#                         ##                             ###############################################################    #\r\n#                        ##                              #                                              |              #    #\r\n#                       ##                               #     ##########################################   ########## #    #\r\n#                     ##                                 #     #    #########   #########               #   ########## #    #\r\n|                      ##                                #     #    #       #   #       #               #   ########## #    #\r\n|                       ######                           #     #    #       #   #       #               #   ####--#### #    #\r\n|               ##           #########                   #     #    #########   #########   #############              #    #\r\n#               # ##               ##########            #     #                                                       #    #\r\n#               #  ##                       ##         ###     #                                                       #    #\r\n#               #   ####                                 #     #                            #############              #    #\r\n#               #      ###########                     ###     #                                        #              #    #\r\n#               #               ############             #     #                                        ################    #\r\n#              ##                                        #     #                                                       #    #\r\n#             ##                                         #     #                                                       #    #\r\n#            ##                                          #     #                                                       #    #\r\n#           ##                                           #     # #########                            #########        #    #\r\n#          ##                                            #     # #       #                            #       #        #    #\r\n#         ##                                             #     # #       #           #####            #       #        #    #\r\n#        ##                                              #     # #########           #####            #########        #    #\r\n#       ##                                               #     #                     #####                             #    #\r\n#     ###                                                #     ###################################################     #    #\r\n#    ###                                                 #                                                       |     #    #\r\n#  ###                                                   ###############################################################    #\r\n####                                                                                                                        #\r\n#                                                                                                                           #\r\n#############################################################################################################################";
            PlayerPosX = 112;
            PlayerPosY = 23;
            SpawnOnStartPosition = false;
            Triggers = new char[] { '|', '-' };

            int[][] toSouthLadder = new[]
{
                new int[]{ 113, 23 }
            };
            EventsDictionary.Add(toSouthLadder, EventName.ToSouthLadder);

            int[][] toNorthLadder = new[]
            {
                new int[]{ 104, 3 }
            };
            EventsDictionary.Add(toNorthLadder, EventName.ToNorthLadder);
        }

        internal override bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            bool goOut = false;
            switch (way)
            {
                case (int)EventName.ToSouthLadder:
                    ToMainCampSecond(ref player, ref story, 114, 23);
                    break;
                case (int)EventName.ToNorthLadder:
                    goOut = true;
                    ToMainCampSecond(ref player, ref story, 105, 3);
                    break;
                default: break;

            }
            return goOut;
        }

        internal static void ToMainCampSecond(ref PlayerClass player, ref Story story, int mapPlayerX, int mapPlayerY)
        {
            Maps.GoToMap(ref player, ref story, ref MapList.MainCampSecond, mapPlayerX, mapPlayerY);
        }
    }

    class MainCampFourthEvents: MapEvents
    {
        public MainCampFourthEvents(List<MapEnemy> enemies)
        {
            Enemies = enemies;

            MapString = "#############################################################################################################################\r\n#                          ##                                                                                               #\r\n#                         ##                             ###############################################################    #\r\n#                        ##                              #                                              |              #    #\r\n#                       ##                               #     ##########################################   ########## #    #\r\n#                     ##                                 #     #    #########   #########               #   ########## #    #\r\n|                      ##                                #     #    #       #   #       #               #   ########## #    #\r\n|                       ######                           #     #    #       #   #       #               #   ####--#### #    #\r\n|               ##           #########                   #     #    #########   #########   #############              #    #\r\n#               # ##               ##########            #     #                                                       #    #\r\n#               #  ##                       ##         ###     #                                                       #    #\r\n#               #   ####                                 #     #                            #############              #    #\r\n#               #      ###########                     ###     #                                        #              #    #\r\n#               #               ############             #     #                                        ################    #\r\n#              ##                                        #     #                                                       #    #\r\n#             ##                                         #     #                                                       #    #\r\n#            ##                                          #     #                                                       #    #\r\n#           ##                                           #     # #########                            #########        #    #\r\n#          ##                                            #     # #       #                            #       #        #    #\r\n#         ##                                             #     # #       #           #####            #       #        #    #\r\n#        ##                                              #     # #########           #####            #########        #    #\r\n#       ##                                               #     #                     #####                             #    #\r\n#     ###                                                #     ###################################################     #    #\r\n#    ###                                                 #                                                       |     #    #\r\n#  ###                                                   ###############################################################    #\r\n####                                                                                                                        #\r\n#                                                                                                                           #\r\n#############################################################################################################################";
            PlayerPosX = 103;
            PlayerPosY = 3;
            SpawnOnStartPosition = false;
            Triggers = new char[] { '|' };


            int[][] toSouthLadder = new[]
{
                new int[]{ 113, 23 }
            };
            EventsDictionary.Add(toSouthLadder, EventName.ToSouthLadder);

            int[][] toNorthLadder = new[]
            {
                new int[]{ 104, 3 }
            };
            EventsDictionary.Add(toNorthLadder, EventName.ToNorthLadder);

            int[][] findFriend = new[]
            {
                new int[]{ 87, 23 }
            };
            InvisibleEventsDictionary.Add(findFriend, EventName.FindFriend);
        }

        internal override bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            bool goOut = false;
            switch (way)
            {
                case (int)EventName.ToSouthLadder:
                    goOut = true;
                    ToMainCampFifth(ref player, ref story);
                    break;
                case (int)EventName.ToNorthLadder:
                    goOut = true;
                    ToMainCampSecond(ref player, ref story);
                    break;

                case (int)EventName.FindFriend:
                    SlowWrite("Смотри, там друг!", needClear: true, ableToSkip: true);
                    InvisibleEventsDictionary.Remove(InvisibleEventsDictionary.First().Key);
                    break;
                default: break;

            }
            return goOut;
        }

        internal static void ToMainCampSecond(ref PlayerClass player, ref Story story)
        {
            Maps.GoToMap(ref player, ref story, ref MapList.MainCampSecond, 105, 3);
        }

        internal static void ToMainCampFifth(ref PlayerClass player, ref Story story)
        {
            Maps.GoToMap(ref player, ref story, ref MapList.MainCampFifth, MapList.MainCampFifth.PlayerPosX, MapList.MainCampFifth.PlayerPosY);

        }
    }

    class MainCampFifthEvents: MapEvents
    {
        public MainCampFifthEvents(List<MapEnemy> enemies)
        {
            Enemies = enemies;

            MapString = "#############################################################################################################################\r\n#                          ##                                                                                               #\r\n#                         ##                             ###############################################################    #\r\n#                        ##                              #############################################  |              #    #\r\n#                       ##                               ################################################   ########## #    #\r\n#                     ##                                 #######    #########   #########               #   ########## #    #\r\n|                      ##                                #######    #       #   #       #               #   ########## #    #\r\n|                       ######                           #######    #       #   #       #               #   ####--#### #    #\r\n|               ##           #########                   #######    #########   #########   #############              #    #\r\n#               # ##               ##########            #######                                                       #    #\r\n#               #  ##                       ##         #######                                                         #    #\r\n#               #   ####                                                                    #############              #    #\r\n#               #      ###########                     #######                                          #              #    #\r\n#               #               ############             #######                                        ################    #\r\n#              ##                                        #######                                                       #    #\r\n#             ##                                         #######                                                       #    #\r\n#            ##                                          #######                                                       #    #\r\n#           ##                                           ####### #########                            #########        #    #\r\n#          ##                                            ####### #       #                            #       #        #    #\r\n#         ##                                             ####### #       #           #####            #       #        #    #\r\n#        ##                                              ####### #########           #####            #########        #    #\r\n#       ##                                               #######                     #####                             #    #\r\n#     ###                                                #########################################################     #    #\r\n#    ###                                                 ####################################################### |     #    #\r\n#  ###                                                   ###############################################################    #\r\n####                                                                                                                        #\r\n#                                                                                                                           #\r\n#############################################################################################################################";
            PlayerPosX = 114;
            PlayerPosY = 23;
            SpawnOnStartPosition = false;
            Triggers = new char[] { '|', '-' };

            int[][] toOutside = new[]
            {
                new int[]{ 0, 6 },
                new int[]{ 0, 7 },
                new int[]{ 0, 8 }
            };
            EventsDictionary.Add(toOutside, EventName.OutsideMainCamp);


            int[][] toSouthLadder = new[]
{
                new int[]{ 113, 23 }
            };
            EventsDictionary.Add(toSouthLadder, EventName.ToSouthLadder);

            int[][] toNorthLadder = new[]
            {
                new int[]{ 104, 3 }
            };
            EventsDictionary.Add(toNorthLadder, EventName.ToNorthLadder);

            int[][] friendDecision = new[]
            {
                new int[]{ 29, 9 },
                new int[]{ 29, 10 },
                new int[]{ 29, 11 }
            };
            InvisibleEventsDictionary.Add(friendDecision, EventName.FriendDecision);
        }

        internal override bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            bool goOut = false;
            switch (way)
            {
                case (int)EventName.ToSouthLadder:
                    goOut = true;
                    SouthLadder(ref player, ref story);
                    break;

                case (int)EventName.ToNorthLadder:
                    goOut = true;
                    NorthLadder(ref player, ref story);
                    break;

                case (int)EventName.OutsideMainCamp:
                    if (story.FreeVillagers && !story.HeadmanMainQuest.QuestCompleted)
                    {
                        story.HeadmanMainQuest.CompleteQuest();
                    }
                    goOut = true;
                    ToOutside(ref player, ref story);
                    break;

                case (int)EventName.FriendDecision:
                    SlowWrite("А нужно ли сейвить друга?", needClear: true, ableToSkip: true);
                    InvisibleEventsDictionary.Remove(InvisibleEventsDictionary.First().Key);
                    break;


                default: break;

            }
            return goOut;
        }

        internal static void SouthLadder(ref PlayerClass player, ref Story story)
        {
            // MainCampThird
            Maps.GoToMap(ref player, ref story, ref MapList.MainCampThird, MapList.MainCampThird.PlayerPosX, MapList.MainCampThird.PlayerPosY);
        }

        internal static void NorthLadder(ref PlayerClass player, ref Story story)
        {
            Maps.GoToMap(ref player, ref story, ref MapList.MainCampThird, 103, 3);
        }

        internal static void ToOutside(ref PlayerClass player, ref Story story)
        {
            HubEvents.ToOutside(ref player, ref story);
        }
    }
}
