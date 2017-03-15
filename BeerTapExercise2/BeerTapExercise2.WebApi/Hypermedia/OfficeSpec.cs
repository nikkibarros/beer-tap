using BeerTapExercise2.Model;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTapExercise2.WebApi.Hypermedia
{
    public class OfficeSpec : SingleStateResourceSpec<Office, int>
    {
        public static ResourceUriTemplate Uri = ResourceUriTemplate.Create("Offices({id})");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Offices; }
        }

        public override IResourceStateSpec<Office, NullState, int> StateSpec
        {
            get
            {
	            return new SingleStateSpec<Office, int>()
	            {
		            Links =
		            {
			            CreateLinkTemplate(LinkRelations.BeerTaps.AddTap, BeerTapSpec.Uri.Many, c => c.Id)
		            },
		            Operations =
		            {
			            Get = ServiceOperations.Get,
			            Post = ServiceOperations.Update,
			            Delete = ServiceOperations.Delete,
			            InitialPost = ServiceOperations.Create
		            }
	            };
            }
        }
    }
}