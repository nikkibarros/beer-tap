using IQ.Platform.Framework.WebApi.AspNet;
using IQ.Platform.Framework.WebApi.AspNet.Handlers;
using IQ.Platform.Framework.WebApi.Services.Security;
using BeerTapExercise2.ApiServices.Security;

namespace BeerTapExercise2.WebApi.Handlers
{
    public class PutYourApiSafeNameUserContextProvidingHandler
            : ApiSecurityContextProvidingHandler<BeerTapExercise2ApiUser, NullUserContext>
    {

        public PutYourApiSafeNameUserContextProvidingHandler(
            IStoreDataInHttpRequest<BeerTapExercise2ApiUser> apiUserInRequestStore)
            : base(new BeerTapExercise2UserFactory(), CreateContextProvider(), apiUserInRequestStore)
        {
        }

        static ApiUserContextProvider<BeerTapExercise2ApiUser, NullUserContext> CreateContextProvider()
        {
            return
                new ApiUserContextProvider<BeerTapExercise2ApiUser, NullUserContext>(_ => new NullUserContext());
        }
    }
}