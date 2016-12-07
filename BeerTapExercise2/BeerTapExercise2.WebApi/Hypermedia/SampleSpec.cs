using BeerTapExercise2.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;

namespace BeerTapExercise2.WebApi.Hypermedia
{
    public class SampleSpec : SingleStateResourceSpec<SampleResource, string>
    {

        public static ResourceUriTemplate Uri = ResourceUriTemplate.Create("SampleResources({id})");

        public override string EntrypointRelation
        {
            get { return LinkRelations.SampleResource; }
        }


        // IResourceStateSpec is not required but can be overridden to define custom operations and links.
        // See example below...
        //
        //public override IResourceStateSpec<SampleResource, NullState, string> StateSpec
        //{
        //    get
        //    {
        //        return
        //            new SingleStateSpec<SampleResource, string>
        //            {
        //                Links =
        //                {
        //                    CreateLinkTemplate(LinkRelations.SampleResource2, OrganizationSpec2.Uri, r => r.Id),
        //                },

        //                Operations =
        //                {
        //                    Get = ServiceOperations.Get,
        //                    InitialPost = ServiceOperations.Create,
        //                    Post = ServiceOperations.Update,
        //                    Delete = ServiceOperations.Delete,
        //                },
        //            };
        //    }
        //}

    }
}