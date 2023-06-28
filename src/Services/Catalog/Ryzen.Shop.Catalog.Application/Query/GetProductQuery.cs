using MediatR;
using Ryzen.Shop.Catalog.Domain;

namespace Ryzen.Shop.Catalog.Application.Query;

public record GetProductQuery(int ProductId) : IRequest<ProductDetailResponse>;

public record ProductDetailResponse(
    int Id,
    string Name,
    string Description,
    decimal Amount,
    int? PromotionID,

    PromotionType? Type,
    decimal? DiscountAmount,
    decimal? DiscountPercentage,
    decimal? MinimumSpend,
    bool? GetOneFree,
    decimal? SecondOneDiscountPercentage
    );
