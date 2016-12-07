using System.Collections.Generic;
using BeerTapExercise2.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapExercise2.WebApi.Hypermedia
{
    public class ReplaceKegSpec : SingleStateResourceSpec<ReplaceKeg, int>
    {
        public static ResourceUriTemplate Uri =
            ResourceUriTemplate.Create("Offices({OfficeId})/BeerTaps({BeerTapId})/ReplaceKeg");

        public override string EntrypointRelation
        {
            get { return LinkRelations.BeerTaps.ReplaceKeg; }
        }

        protected override IEnumerable<ResourceLinkTemplate<ReplaceKeg>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, Uri, c => c.OfficeId, c => c.BeerTapId);
        }

        public override IResourceStateSpec<ReplaceKeg, NullState, int> StateSpec
        {
            get
            {
                return new SingleStateSpec<ReplaceKeg, int>()
                {
                    Operations = new StateSpecOperationsSource<ReplaceKeg, int>()
                    {
                        InitialPost = ServiceOperations.Create
                    }
                };
            }
        }
    }
}