using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapExercise2.Model
{
    public class PourGlass : IStatelessResource, IIdentifiable<int>
    {
        public int Id { get; }

        public double ML { get; set; }

        public int OfficeId { get; set; }

        public int BeerTapId { get; set; }
    }
}
