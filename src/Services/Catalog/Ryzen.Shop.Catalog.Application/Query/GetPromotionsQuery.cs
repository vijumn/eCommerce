using MediatR;
using Ryzen.Shop.Catalog.Domain;

namespace Ryzen.Shop.Catalog.Application.Query;

public record GetPromotionsQuery(int[] ProductIds) : IRequest<PromotionResponse[]>;

public record PromotionResponse(
    int? PromotionId,
    decimal Amount,
    int? ProductId,

    PromotionType? Type,
    decimal? DiscountAmount,
    decimal? DiscountPercentage,
    decimal? MinimumSpend,
    bool? GetOneFree,
    decimal? SecondOneDiscountPercentage
    );
