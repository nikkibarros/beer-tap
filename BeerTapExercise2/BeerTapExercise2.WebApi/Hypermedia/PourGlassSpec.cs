using System.Collections.Generic;
using BeerTapExercise2.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapExercise2.WebApi.Hypermedia
{
    public class PourGlassSpec : SingleStateResourceSpec<PourGlass, int>
    {
        public static ResourceUriTemplate Uri =
            ResourceUriTemplate.Create("Offices({OfficeId})/BeerTaps({BeerTapId})/PourGlass");

        public override string EntrypointRelation
        {
            get { return LinkRelations.BeerTaps.PourGlass; }
        }

        protected override IEnumerable<ResourceLinkTemplate<PourGlass>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, Uri, c => c.OfficeId, c => c.BeerTapId);
        }

        public override IResourceStateSpec<PourGlass, NullState, int> StateSpec
        {
            get
            {
                return new SingleStateSpec<PourGlass, int>()
                {
                    Operations = new StateSpecOperationsSource<PourGlass, int>()
                    {
                        InitialPost = ServiceOperations.Create
                    }
                };
            }
        }
    }
}