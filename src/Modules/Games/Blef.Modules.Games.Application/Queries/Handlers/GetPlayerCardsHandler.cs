﻿using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Queries.Handlers;

internal sealed class GetPlayerCardsHandler : IQueryHandler<GetPlayerCards, GetPlayerCards.Result>
{
    private readonly IGamesRepository _games;

    public GetPlayerCardsHandler(IGamesRepository games) =>
        _games = games;

    public Task<GetPlayerCards.Result> Handle(GetPlayerCards query)
    {
        var game = _games.Get(query.GameId);
        var cards = game.GetCards(query.PlayerId);
        return Task.FromResult(new GetPlayerCards.Result(cards));
    }
}