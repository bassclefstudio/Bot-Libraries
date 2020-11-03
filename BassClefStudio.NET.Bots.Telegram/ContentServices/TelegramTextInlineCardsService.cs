using BassClefStudio.NET.Bots.Inline;
using BassClefStudio.NET.Bots.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.InlineQueryResults;

namespace BassClefStudio.NET.Bots.Telegram.ContentServices
{
    public class TelegramTextInlineCardsService : IBotInlineCardService<TelegramBotService>
    {
        public bool CanSend(InlineCards cards)
        {
            return cards.Query is TelegramInlineQuery
                && cards.Cards.All(c => c is TextInlineCard);
        }

        public async Task<bool> SendCardsAsync(TelegramBotService service, InlineCards cards)
        {
            var telegramQuery = cards.Query as TelegramInlineQuery;
            var textCards = cards.Cards.OfType<TextInlineCard>();

            List<InlineQueryResultBase> inlineQueryResults = new List<InlineQueryResultBase>();
            int index = 0;
            foreach (var card in textCards)
            {
                inlineQueryResults.Add(new InlineQueryResultArticle(
                    index.ToString(),
                    card.Title,
                    new InputTextMessageContent(card.Title))
                {
                    Description = card.Description
                });
                index++;
            }

            if (cards.MoreAction != null)
            {
                await service.BotClient.AnswerInlineQueryAsync(
                    telegramQuery.QueryId,
                    inlineQueryResults,
                    isPersonal: true,
                    switchPmText: cards.MoreAction.DisplayName,
                    switchPmParameter: cards.MoreAction.Id);
            }
            else
            {
                await service.BotClient.AnswerInlineQueryAsync(
                    telegramQuery.QueryId,
                    inlineQueryResults);
            }

            return true;
        }
    }
}
