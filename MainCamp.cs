using ConsoleShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static consoleTextRPG.Program;

namespace consoleTextRPG
{
    internal class MainCampFirst : MapEvents
    {
        public MainCampFirst()
        {
            MapString = "#############################################################################################################################\r\n#                          ##                                                                                               #\r\n#                         ##                             ###############################################################    #\r\n#                        ##                              ############################################# |               #    #\r\n#                       ##                               ################################################   ########## #    #\r\n#                     ##                                 #######    #########   #########               #   ########## #    #\r\n|                      ##                                #######    #       #   #       #               #   ########## #    #\r\n|                       ######                           #######    #       #   #       #               #   ####--#### #    #\r\n|               ##           #########                   #######    #########   #########   #############              #    #\r\n#               # ##               ##########            #######                                                       #    #\r\n#               #  ##                       ##         #######                                                         #    #\r\n#               #   ####                                                                    #############              #    #\r\n#               #      ###########                     #######                                          #              #    #\r\n#               #               ############             #######                                        ################    #\r\n#              ##                                        #######                                                       #    #\r\n#             ##                                         #######                                                       #    #\r\n#            ##                                          #######                                                       #    #\r\n#           ##                                           ####### #########                            #########        #    #\r\n#          ##                                            ####### #       #                            #       #        #    #\r\n#         ##                                             ####### #       #           #####            #       #        #    #\r\n#        ##                                              ####### #########           #####            #########        #    #\r\n#       ##                                               #######                     #####                             #    #\r\n#     ###                                                #########################################################     #    #\r\n#    ###                                                 ####################################################### |     #    #\r\n#  ###                                                   ###############################################################    #\r\n####                                                                                                                        #\r\n#                                                                                                                           #\r\n#############################################################################################################################";
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
            
        }

        internal static void ToOutside(ref PlayerClass player, ref Story story)
        {

        }

    }


    class MainCampSecond: MapEvents
    {
        public MainCampSecond()
        {
            MapString = "#############################################################################################################################\r\n#                          ##                                                                                               #\r\n#                         ##                             ###############################################################    #\r\n#                        ##                              ############################################# |               #    #\r\n#                       ##                               ################################################   ########## #    #\r\n#                     ##                                 #######    #########   #########               #   ########## #    #\r\n|                      ##                                #######    #       #   #       #               #   ########## #    #\r\n|                       ######                           #######    #       #   #       #               #   ####--#### #    #\r\n|               ##           #########                   #######    #########   #########   #############              #    #\r\n#               # ##               ##########            #######                                                       #    #\r\n#               #  ##                       ##         #######                                                         #    #\r\n#               #   ####                                                                    #############              #    #\r\n#               #      ###########                     #######                                          #              #    #\r\n#               #               ############             #######                                        ################    #\r\n#              ##                                        #######                                                       #    #\r\n#             ##                                         #######                                                       #    #\r\n#            ##                                          #######                                                       #    #\r\n#           ##                                           ####### #########                            #########        #    #\r\n#          ##                                            ####### #       #                            #       #        #    #\r\n#         ##                                             ####### #       #           #####            #       #        #    #\r\n#        ##                                              ####### #########           #####            #########        #    #\r\n#       ##                                               #######                     #####                             #    #\r\n#     ###                                                #########################################################     #    #\r\n#    ###                                                 ####################################################### |     #    #\r\n#  ###                                                   ###############################################################    #\r\n####                                                                                                                        #\r\n#                                                                                                                           #\r\n#############################################################################################################################";
            PlayerPosX = 12;
            PlayerPosY = 6;
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
                new int[]{ 103, 3 }
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
                    goOut = true;
                    ToOutside(ref player, ref story);
                    break;

                case (int)EventName.MainCampThird: 
                    goOut = true;

                    break;

                case (int)EventName.MainCampFourth:
                    goOut = true;

                    break;

                default: break;

            }
            return goOut;
        }

        internal static void ToOutside(ref PlayerClass player, ref Story story)
        {

        }

        internal static void SouthLadder(ref PlayerClass player, ref Story story)
        {
           // MainCampThird
        }
        
        internal static void NorthLadder(ref PlayerClass player, ref Story story)
        {
            // if FreeVillagers
            // MainCampFourth
            // else MainCampThird
        }
    }

    class MainCampThird: MapEvents
    {
        public MainCampThird()
        {
            MapString = "#############################################################################################################################\r\n#                          ##                                                                                               #\r\n#                         ##                             ###############################################################    #\r\n#                        ##                              #                                              |              #    #\r\n#                       ##                               #     ##########################################   ########## #    #\r\n#                     ##                                 #     #    #########   #########               #   ########## #    #\r\n|                      ##                                #     #    #       #   #       #               #   ########## #    #\r\n|                       ######                           #     #    #       #   #       #               #   ####--#### #    #\r\n|               ##           #########                   #     #    #########   #########   #############        %     #    #\r\n#               # ##               ##########            #     #                                    %                  #    #\r\n#               #  ##                       ##         ###     #                                    %                  #    #\r\n#               #   ####                         %       #     #                            #############              #    #\r\n#               #      ###########                     ###     #                                        #              #    #\r\n#               #               ############             #     #                                        ################    #\r\n#              ##                                        #     #                                                       #    #\r\n#             ##                                         #     #                                                       #    #\r\n#            ##                                          #     #                                                       #    #\r\n#           ##                                           #     # #########             %              #########        #    #\r\n#          ##                                            #     # #       #                            #       #        #    #\r\n#         ##                                             #     # #       #           #####            #       #        #    #\r\n#        ##                                              #     # #########       %   #####   %        #########        #    #\r\n#       ##                                               #     #                     #####                             #    #\r\n#     ###                                                #     ###################################################     #    #\r\n#    ###                                                 #                                                       |     #    #\r\n#  ###                                                   ###############################################################    #\r\n####                                                                                                                        #\r\n#                                                                                                                           #\r\n#############################################################################################################################";
            PlayerPosX = 12;
            PlayerPosY = 6;
            SpawnOnStartPosition = false;
            Triggers = new char[] { '|', '-' };

            int[][] toSouthLadder = new[]
{
                new int[]{ 113, 23 }
            };
            EventsDictionary.Add(toSouthLadder, EventName.ToSouthLadder);

            int[][] toNorthLadder = new[]
            {
                new int[]{ 103, 3 }
            };
            EventsDictionary.Add(toNorthLadder, EventName.ToNorthLadder);
        }

        internal override bool StartEvent(ref PlayerClass player, ref Story story, string nickName, int way)
        {
            bool goOut = false;
            switch (way)
            {
                case (int)EventName.MainCampSecond:
                case (int)EventName.OutsideMainCamp:
                    goOut = true;
                    ToMainCampSecond(ref player, ref story);
                    break;
                default: break;

            }
            return goOut;
        }

        internal static void ToMainCampSecond(ref PlayerClass player, ref Story story)
        {

        }
    }
}
