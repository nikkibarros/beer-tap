using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.AspNet;
using IQ.Platform.Framework.WebApi.Services.Security;

namespace BeerTapExercise2.ApiServices.Security
{

    public class BeerTapExercise2ApiUser : ApiUser<UserAuthData>
    {
        public BeerTapExercise2ApiUser(string name, Option<UserAuthData> authData)
            : base(authData)
        {
            Name = name;
        }

        public string Name { get; private set; }

    }

    public class BeerTapExercise2UserFactory : ApiUserFactory<BeerTapExercise2ApiUser, UserAuthData>
    {
        public BeerTapExercise2UserFactory() :
            base(new HttpRequestDataStore<UserAuthData>())
        {
        }

        protected override BeerTapExercise2ApiUser CreateUser(Option<UserAuthData> auth)
        {
            return new BeerTapExercise2ApiUser("BeerTapExercise2 user", auth);
        }
    }

}
