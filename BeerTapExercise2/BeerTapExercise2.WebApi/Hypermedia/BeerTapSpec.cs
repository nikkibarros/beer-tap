using System.Collections.Generic;
using BeerTapExercise2.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapExercise2.WebApi.Hypermedia
{
    public class BeerTapSpec : ResourceSpec<BeerTap, BeerTapState, int>
    {
        public static ResourceUriTemplate Uri = ResourceUriTemplate.Create("Offices({OfficeId})/BeerTaps({id})");

        public override string EntrypointRelation
        {
            get { return LinkRelations.BeerTaps.BeerTapsUrl; }
        }

        protected override IEnumerable<ResourceLinkTemplate<BeerTap>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, Uri, c => c.OfficeId, c => c.Id);
        }

        protected override IEnumerable<IResourceStateSpec<BeerTap, BeerTapState, int>> GetStateSpecs()
        {
            yield return new ResourceStateSpec<BeerTap, BeerTapState, int>(BeerTapState.New)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.BeerTaps.PourGlass, PourGlassSpec.Uri, c => c.OfficeId, c => c.Id),
                    CreateLinkTemplate(LinkRelations.BeerTaps.RemoveTap, Uri, c => c.OfficeId, c => c.Id)
                },
                Operations = new StateSpecOperationsSource<BeerTap, int>()
                {
                    InitialPost = ServiceOperations.Create,
                    Delete = ServiceOperations.Delete
                }
            };
            yield return new ResourceStateSpec<BeerTap, BeerTapState, int>(BeerTapState.NotSoNew)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.BeerTaps.PourGlass, PourGlassSpec.Uri, c => c.OfficeId, c => c.Id),
                    CreateLinkTemplate(LinkRelations.BeerTaps.RemoveTap, Uri, c => c.OfficeId, c => c.Id)
                },
                Operations = new StateSpecOperationsSource<BeerTap, int>()
                {
                    InitialPost = ServiceOperations.Create,
                    Delete = ServiceOperations.Delete
                }
            };
            yield return new ResourceStateSpec<BeerTap, BeerTapState, int>(BeerTapState.AlmostGone)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.BeerTaps.PourGlass, PourGlassSpec.Uri, c => c.OfficeId, c => c.Id),
                    CreateLinkTemplate(LinkRelations.BeerTaps.ReplaceKeg, ReplaceKegSpec.Uri, c => c.OfficeId, c => c.Id),
                    CreateLinkTemplate(LinkRelations.BeerTaps.RemoveTap, Uri, c => c.OfficeId, c => c.Id)
                },
                Operations = new StateSpecOperationsSource<BeerTap, int>()
                {
                    InitialPost = ServiceOperations.Create,
                    Delete = ServiceOperations.Delete
                }
            };
            yield return new ResourceStateSpec<BeerTap, BeerTapState, int>(BeerTapState.Dry)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.BeerTaps.ReplaceKeg, ReplaceKegSpec.Uri, c => c.OfficeId, c => c.Id),
                    CreateLinkTemplate(LinkRelations.BeerTaps.RemoveTap, Uri, c => c.OfficeId, c => c.Id)
                },
                Operations = new StateSpecOperationsSource<BeerTap, int>()
                {
                    InitialPost = ServiceOperations.Create,
                    Delete = ServiceOperations.Delete
                }
            };
        }
    }
}