using BeerTapExercise2.Model;
using IQ.Platform.Framework.WebApi;

namespace BeerTapExercise2.ApiServices
{
    public interface IPourGlassApiService : ICreateAResourceAsync<PourGlass, int>
    {
    }
}
