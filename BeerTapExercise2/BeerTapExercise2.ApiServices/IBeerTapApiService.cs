using BeerTapExercise2.Model;
using IQ.Platform.Framework.WebApi;

namespace BeerTapExercise2.ApiServices
{
    public interface IBeerTapApiService :
        IGetAResourceAsync<BeerTap, int>,
        IGetManyOfAResourceAsync<BeerTap, int>,
        ICreateAResourceAsync<BeerTap, int>,
        IUpdateAResourceAsync<BeerTap, int>,
        IDeleteResourceAsync<BeerTap, int>
    {
    }
}
