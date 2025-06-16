using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Infrastructure.Scoping;
using Ganss.Xss;

using DVUA.Site.Models.Dtos;
using DVUA.Site.Models.Schemas;

namespace DVUA.Site.Controllers;

[ApiController]
[Route("/api/reviews")]
public class ProductReviewsController(
    IMemberManager memberManager,
    IScopeProvider scopeProvider) : Controller
{
    [HttpGet]
    public async Task<List<ProductReviewResponseDto>> GetReviews(int umbracoNodeId)
    {
        using var scope = scopeProvider.CreateScope();

        var queryResults = scope.Database.Fetch<ProductReviewSchema>(
            "SELECT * FROM ProductReviews WHERE UmbracoNodeId = @0", umbracoNodeId);

        scope.Complete();

        var responseDtos = await CreateResponseDtos(queryResults);
        return responseDtos;
    }

    [HttpGet("bymember")]
    public async Task<List<ProductReviewResponseDto>> GetReviews(string memberId)
    {
        using var scope = scopeProvider.CreateScope();

        var queryResults = scope.Database.Fetch<ProductReviewSchema>(
            "SELECT * FROM ProductReviews WHERE UmbracoMemberId = @0", memberId);

        scope.Complete();

        var responseDtos = await CreateResponseDtos(queryResults);
        return responseDtos;
    }

    [HttpPut]
    public async Task SubmitReview(ProductReviewRequestDto productReviewRequest)
    {
        var sanitizer = new HtmlSanitizer();
        var sanitized = sanitizer.Sanitize(productReviewRequest.Comment);
        if (!sanitized.Equals(productReviewRequest.Comment))
        {
            throw new ArgumentException("Malicious payload detected in product review.");
        }

        using var scope = scopeProvider.CreateScope();

        var currentMember = await memberManager.GetCurrentMemberAsync();

        scope.Database.Insert(new ProductReviewSchema
        {
            UmbracoNodeId = productReviewRequest.UmbracoNodeId,
            UmbracoMemberId = currentMember?.Id,
            Comment = productReviewRequest.Comment
        });

        scope.Complete();
    }

    private async Task<List<ProductReviewResponseDto>> CreateResponseDtos(List<ProductReviewSchema> queryResults)
    {
        var responseDtos = await Task.WhenAll(
            queryResults.Select(async review =>
            {
                var member = await memberManager.FindByIdAsync(review.UmbracoMemberId);
                return new ProductReviewResponseDto
                {
                    MemberEmail = member?.Email,
                    MemberDisplayName = member?.Name,
                    Comment = review.Comment
                };
            }));
        return responseDtos.ToList();
    }
}
