using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapExercise2.Model
{
    public class BeerTap : IStatefulResource<BeerTapState>, IIdentifiable<int>, IStatefulBeerTap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Volume { get; set; }
        public BeerTapState BeerTapState { get; set; }
        public int OfficeId { get; set; }
        [ForeignKey("OfficeId")]
        public Office Office { get; set; }
    }

    public enum BeerTapState
    {
        New,
        NotSoNew,
        AlmostGone,
        Dry
    }
}
