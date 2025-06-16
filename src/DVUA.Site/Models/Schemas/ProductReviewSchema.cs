using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace DVUA.Site.Models.Schemas;

[TableName("ProductReviews")]
[PrimaryKey("Id", AutoIncrement = true)]
[ExplicitColumns]
public class ProductReviewSchema
{
    [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
    [Column("Id")]
    public int Id { get; set; }

    [Column("UmbracoNodeId")]
    public int UmbracoNodeId { get; set; }

    [Column("UmbracoMemberId")]
    public string UmbracoMemberId { get; set; }

    [Column("Comment")]
    [SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
    public string Comment { get; set; }
}
