using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using consoleTextRPG;
using static consoleTextRPG.Program;

namespace ConsoleHub
{
    internal class Hub
    {
        internal static void ToHub(ref PlayerClass player)
        {
            SlowWrite($"...");
            SlowWrite($"{player.Name}, узнав о том, что его родной деревне угрожает опасность, незамедлительно отправился на помощь.");
            SlowWrite($"На третьи сутки пути он наконец прибывает на место.");
            SlowWrite($"Первым делом {player.Name} решает навестить старосту деревни, чтобы узнать подробности.");
            SlowWrite($"Однако староста нашёл его раньше...");
            SlowWrite($"{player.Name}!", teller: "Староста");
            SlowWrite($"Наконец ты прибыл..", teller: "Староста");
            SlowWrite($"Здравствуй, рад тебя видеть. Рассказывай, что случилось.", teller: player.Name);
            SlowWrite($"На нашу деревню уже давно нападают ____. До недавних пор это происходило не так часто, и мы справлялись своими силами, но..", teller: "Староста");
            SlowWrite($"Но в последнее время они будто с цепи сорвались! Мужики поговаривают, что видели ___, думаю его/ее появление как-то с этим связано.", teller: "Староста");
            SlowWrite($"___?! Если это правда, то у нас неприятности, мягко говоря.", teller: player.Name);
            SlowWrite($"Ты сможешь нам с этим помочь?", teller: "Староста");
            // Вариативный ответ
            SlowWrite($"Я сделаю все что в моих силах.", teller: player.Name);
            SlowWrite($"Но для начала мне нужно подготовиться. У вас есть где достать зелья?", teller: player.Name);
            SlowWrite($"Конечно. Загляни к торговцу, скажи что от меня, он даст тебе парочку.", teller: "Староста");
            SlowWrite($"Отлично, отправлюсь прямо сейчас.", teller: player.Name);
            SlowWrite($"Дай знать, когда выяснишь что-нибудь. И.. Береги себя!", teller: "Староста");
        }
    }
}