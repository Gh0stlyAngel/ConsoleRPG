using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using consoleTextRPG;
using ConsoleFight;
using static ConsoleFight.Fight;
using static consoleTextRPG.Program;
using ConsoleShop;

namespace ConsoleHub
{
    internal class Hub
    {
        internal static void ToHub(ref PlayerClass player, ref Story story)
        {
            ToHeadman(ref player);
            bool inHub = true;
            int wins = 0;


            while (inHub)
            {
                List<ConsoleKey> actions = new List<ConsoleKey> { ConsoleKey.D1, ConsoleKey.D2 };
                ConsoleColor actionColor;
                Console.Clear();
                SlowWrite("1. toShop", needClear: false, speed:0);
                SlowWrite("2. toFight", needClear: false, speed: 0);


                if (player.Inventory.playerItems.Find(item => item.Name == "Зелье лечения").AmountOfItems > 0)
                {
                    actions.Add(ConsoleKey.D3);
                    SlowWrite("3. useHealingPotion", needClear: false, speed: 0);
                }
                else
                    SlowWrite("3. useHealingPotion", textColor: ConsoleColor.DarkYellow, needClear: false, speed: 0);


                if (player.Inventory.playerItems.Find(item => item.Name == "Зелье маны").AmountOfItems > 0)
                {
                    actions.Add(ConsoleKey.D4);
                    SlowWrite("4. useManaPotion", needClear: false, speed: 0);
                }
                else
                    SlowWrite("4. useManaPotion", textColor: ConsoleColor.DarkYellow, needClear: false, speed: 0);


                SlowWrite("C. ShowStats", needClear: false, speed: 0);
                SlowWrite("J. ShowJournal", needClear: false, speed: 0);
                ConsoleKey playerAction = GetPlayerAction(actions, true);
                
                switch (playerAction)
                {
                    case ConsoleKey.D1:
                        Shop.ToShop(ref player, ref story);
                        break;
                    case ConsoleKey.D2:
                        BaseEnemy baseEnemy = new BaseEnemy("Базовый противник", 50, 15, 2);
                        StartFight(ref player, baseEnemy);
                        if (player.HP <= 0)
                            inHub = false;
                        else
                            wins++;
                        break;
                    case ConsoleKey.D3:
                        HealingPotion healingPotion = (HealingPotion)player.Inventory.playerItems.Find(item => item.Name == "Зелье лечения").RemoveItem();
                        player.RestoreHP(healingPotion.RestoreValue);
                        SlowWrite($"{player.Name} использует зелье лечения. Восстановлено {healingPotion.RestoreValue} здоровья.", speed: 1);

                        break;
                    case ConsoleKey.D4:
                        ManaPotion manaPotion = (ManaPotion)player.Inventory.playerItems.Find(item => item.Name == "Зелье маны").RemoveItem();
                        player.RestoreMP(manaPotion.RestoreValue);
                        SlowWrite( $"{player.Name} использует зелье маны. Восстановлено {manaPotion.RestoreValue} маны.", speed: 1);
                        break;

                    case ConsoleKey.C:
                        player.ShowStats();
                        Console.ReadKey(true);
                        break;

                    case ConsoleKey.J:
                        story.ShowJournal();
                        break;
                    default: break;
                }
                

            }
            while (true)
            {

                Console.Clear();
                Console.WriteLine($"Класс: {player.Name}\nПобед {wins}");
                Console.ReadKey(true);

            }
        }

        internal static void ToHeadman(ref PlayerClass player)
        {
/*            SlowWrite($"Первым делом {player.Name} решает навестить старосту деревни, чтобы узнать подробности.");
            SlowWrite($"Однако староста нашёл его раньше...");*/
            SlowWrite($"{player.NickName}!", teller: "Староста");
            SlowWrite($"Наконец ты прибыл..", teller: "Староста");
            SlowWrite($"Здравствуй, рад тебя видеть. Рассказывай, что случилось.", teller: player.NickName);
            SlowWrite($"На нашу деревню в последнее время повадились нападать культисты, по ночам. До недавних пор это происходило не так часто, и мы справлялись своими силами, но..", teller: "Староста");
            SlowWrite($"Но в последнее время они будто с цепи сорвались! Мужики поговаривают о каком-то некроманте, думаю его появление как-то с этим связано.", teller: "Староста");
            SlowWrite($"Некроманте?! Если это правда, то у нас неприятности, мягко говоря.", teller: player.NickName);
            SlowWrite($"Я давно знаком с твоими родителями и наслышан про твой.. Дар. Если все так, то ты еднственный, кто способен повлиять на ситуацию. Прошу, помоги нам.", teller: "Староста");
            // Вариативный ответ
            SlowWrite($"Я сделаю все что в моих силах.", teller: player.NickName);
            SlowWrite($"В таком случае тебе нужно подготовиться.", teller: "Староста");
            SlowWrite($"Вижу тебе последняя вылазка далась с трудом, сами собой раны нескоро затянутся.. Вот, выпей это.", teller: "Староста");
            SlowWrite($"{player.NickName} взял сосуд с красной жидкостью и опустошил его в несколько глотков.");
            SlowWrite($"Восстановлено {player.MaxHP - player.HP} здоровья");
            player.RestoreHP(player.MaxHP - player.HP);
            SlowWrite($"Не вино конечно, а? Зато на ноги быстро ставит!", teller: "Староста");
            SlowWrite($"Загляни к торговцу, скажи что от меня, он выдаст тебе несколько зелий, на всякий случай. А после жду тебя тут.", teller: "Староста");
        }

        internal static void HeadmanMainQuest(ref PlayerClass player, ref Story story)
        {
            SlowWrite($"А, уже справился? Отлично, теперь к делу.", teller: "Староста");
            SlowWrite($"Какое-то время назад у нас начали пропадать сначала скот и мелкое имущество, но недавно утром мы не досчитались нескольких наших жителей.", teller: "Староста");
            SlowWrite($"Я знаю, что в этом замешаны эти чертовы культисты, которые недавно объявились в наших краях.", teller: "Староста");
            SlowWrite($"Прошу тебя, проникни в их лагерь и спаси наших людей!", teller: "Староста");
            SlowWrite($"Постараюсь вернуть их к рассвету.", teller: player.NickName);
            SlowWrite($"Рассчитываю на тебя. Береги себя!", teller: "Староста");
            story.HeadmanMainQuest.StartQuest();
        }

        internal static void ComeToHome(ref PlayerClass player, string nickName, ref Story story)
        {
            SlowWrite($"{player.NickName} первым делом прибежал домой, дабы убедиться, что с его родными все в порядке. Он застал матушку, которая латала раны его отцу, и брата, который помогал ей. «Выбор» Кинуться обнимать родных или сначала выяснить причину, по которой мама вызвала именно его.");
            SlowWrite($"Достав письмо, {player.NickName} решил узнать у матушки, чем же таким он отличается от других, что она назвала его последней надеждой деревни. ");
            SlowWrite($"Матушка кивнула родным, попросив их покинуть комнату, дабы объясниться с сыном.");
            SlowWrite($"{player.NickName}, знал ли ты, что когда-то я относилась к древнему роду «Придумать название»? Наш род отличался силой и интеллектом, которые особо ярко проявлялись в мужчинах. Но ты, {player.NickName}, особенный. Все потому, что при твоем рождении наши жрецы заметили в тебе силу, которая могла поменять баланс добра и зла.");
            SlowWrite($"В связи с чем, мы с твоим отцом провели особый обряд, который частично запечатал твою силу. Но к великому счастью, со временем, как ты креп, печать слабела. И сейчас, тебе уже вполне по силам начать учиться тому, что знал мой клан. ");

            HealingPotion healingPotion = new HealingPotion();
            ManaPotion manaPotion = new ManaPotion();

            PlayerClass warrior = PlayerClassFactory.CreateInstance(1, nickName);
            PlayerClass sorcerer = PlayerClassFactory.CreateInstance(2, nickName);
            PlayerClass slayer = PlayerClassFactory.CreateInstance(3, nickName);
            PlayerClass archer = PlayerClassFactory.CreateInstance(4, nickName);

            int chosenClass = PlayerPick(warrior, sorcerer, slayer, archer);
            player = null;
            player = PlayerClassFactory.CreateInstance(chosenClass, nickName);
            player.Inventory.AppendItem(healingPotion);
            player.Inventory.AppendItem(manaPotion);

            SlowWrite("Сын, теперь, когда ты узнал о своем происхождении, и своих запечатанных силах, тебе нужно узнать ещё одну вещь. Чтобы полностью снять печать, тебе придется собрать артефакт, при помощи которого твои силы и были запечатаны.");
            SlowWrite("Одну часть артефакта мы хранили дома, в секретном месте, а вот вторую его часть мы унесли в пещеру недалеко от деревни и оставили стража охранять место хранения артефакта. Так как ты являешься нашим сыном, то страж должен тебя без проблем пропустить.");
            SlowWrite("Отправляйся немедленно, а мы пока займемся подготовкой первой части артефакта. Нельзя терять ни минуты.");
            
            story.FirstVisitHomeQuest.PassQuest();

            story.SealMainQuest.StartQuest();

        }

        internal static void ToCave(ref PlayerClass player, ref Story story)
        {
            SlowWrite("...");
            SlowWrite($"По пришествии в пещеру, которую указали родители, {player.NickName} замечает в дальней части существо, которое охраняло какой-то пьедестал.");
            SlowWrite($"Подходя ближе, {player.NickName} заметил, что это существо подняло голову и смотрело на {player.NickName}, от этого взгляда по коже пошла мелкая дрожь.");
            SlowWrite($"Хоть мама и сказала, что все должно быть легко, но что-то внутри кричало об опасности. Сделав ещё два шага, {player.NickName} еле успел отпрыгнуть.");
            SlowWrite($"Страж, который только что был подле пьедестала, в мгновение ока оказался рядом и пытался убить. От той невозмутимости не осталось и следа.");
            SlowWrite($"Встав, {player.NickName} начал пятиться назад, смотря в эти кроваво-красные глаза, но страж не шел следом, как будто бы что-то мешало ему пройти, будто бы что-то держало его, как поводок.");
            SlowWrite($"...");
            SlowWrite($"Вернувшись домой {player.NickName} рассказал родителям о произошедшем. Это повергло родителей в ужас. Сев на кухне, семья начали рассуждать, с чем может быть связано такое поведение стража.");
            SlowWrite($"Посидев и подумав, мама вспомнила, что пару дней назад ощутила всплеск темной магии в округе, и предположила, что такое поведение стража может быть связано с высокой чувствительностью стража к магии.");
            SlowWrite($"По решению семейного совета было принято решение об убийстве стража и {player.NickName} нужно сделать это как можно скорее.");
            SlowWrite($"Но прежде, чем {player.NickName} отправится на бой со стражем, было также решено использовать подготовленную часть артефакта, чтобы ослабить печать. И силы, которая высвободится, должно быть достаточно, чтобы одолеть стража.");
            SlowWrite($"...");
            SlowWrite($"Утром следующего дня {player.NickName} снова пришел к логову стража для своей первой битвы с новыми силами, которые он открыл в себе. ");
            SlowWrite($"Это свирепый противник, но для того, чтобы узнать, какие силы сокрыты внутри {player.NickName}, нужно рискнуть!");
            FightWithoutAbilities.StartFight(ref player);
            SlowWrite($"Одержав победу над стражем, {player.NickName} подходит к пьедесталу и капает капельку крови на артефакт, чтобы тот признал его.");
            SlowWrite($"Забрав артефакт {player.NickName} отправился поскорее домой.");
            SlowWrite($"...");
            SlowWrite($"Дойдя до дверей дома вымотанным и голодным, {player.NickName} увидел родителей, которые встречали его. Проведя сына в комнату, усадили на кровать и забрали из рук вторую часть артефакта. ");
            SlowWrite($"Утро следующего дня..");
            SlowWrite($"Дав сыну отдохнуть, родители провели его в подвальное помещение, на полу которого были нарисованы руны.");
            SlowWrite($"Руны были похожи на те, которые {player.NickName} видел на части артефакта, которую принес домой.");
            SlowWrite($"...");
            SlowWrite($"Проведя ритуал, родители подошли к {player.NickName} и поинтересовались его самочувствием. И хотели узнать, ощутил ли он изменения в своем организме.");
            SlowWrite($"{player.NickName} ощутил изменения сразу. {player.Name} начал чувствовать себя сильнее..");
            story.SealMainQuest.PassQuest();
        }

        internal static void ShopQuest(PlayerClass player, ref Story story)
        {
            SlowWrite("А что с запасами? Может я смогу помочь?", teller: player.NickName);
            SlowWrite("Мне поставляли припасы из города торговые караваны, но с недавних пор окрестные территории обходят десятой дорогой после..", teller: "Торговец");
            SlowWrite("Не буду томить и перейду сразу к делу.", teller: "Торговец");
            SlowWrite("Слышал я, что обломки обозов находили около моста, недалеко от деревни. Не мог бы ты проверить, может там осталось что ценное? А я тебе скидку на товары сделаю, если справишься.", teller: "Торговец");
            story.TraderQuest.StartQuest();
            SlowWrite("Буду иметь в виду. До встречи!", teller: player.NickName);
            SlowWrite("Вы уходите.");
        }

        internal static void ShopGetPotions(ref PlayerClass player, ref Story story)
        {
            story.FirstShopVisit = true;
            SlowWrite("Приветствую в лавке!", teller: "Торговец");
            SlowWrite("Привет. Я от старосты, он сказал, что я смогу получить тут несколько зелий.", teller: player.NickName);
            SlowWrite($"Ох, так ты {player.NickName}? Мы очень ждали тебя!", teller: "Торговец");
            SlowWrite("Да, конечно, я смогу выдать кое-что из своих запасов, но к сожалению они на исходе..", teller: "Торговец");
            SlowWrite("Вот, держи.", teller: "Торговец");
            player.Inventory.playerItems.Find(item => item.Name == "Зелье лечения").AddItem();
            player.Inventory.playerItems.Find(item => item.Name == "Зелье маны").AddItem();
            SlowWrite("Получено Зелье лечения.");
            SlowWrite("Получено Зелье маны.");
            SlowWrite("Благодарю!", teller: player.NickName);

            Console.Clear();
            SlowWrite("1. Спросить про запасы", needClear: false);
            SlowWrite("2. Попрощаться", needClear: false);
            List<ConsoleKey> actions = NumberOfActions(2);
            ConsoleKey playerAction = GetPlayerAction(actions);


            switch (playerAction)
            {

                case ConsoleKey.D1:
                    ShopQuest(player, ref story);
                    break;

                case ConsoleKey.D2:
                    SlowWrite("На первое время этого хватит. До встречи!", teller: player.NickName);
                    SlowWrite("Вы уходите.");
                    break;

                default: break;
            }
        }
    }
}