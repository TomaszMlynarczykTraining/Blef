using Blef.Modules.Games.Domain.Entities;

namespace Blef.Modules.Games.Domain.Tests;

public class DeckStub : IDeck
{
    private readonly Queue<Card> _queue = new();

    public DeckStub(IEnumerable<Card> cards)
    {
        foreach (var card in cards)
        {
            _queue.Enqueue(card);
        }
    }

    public Card DealCard()
    {
        return _queue.Dequeue();
    }
}