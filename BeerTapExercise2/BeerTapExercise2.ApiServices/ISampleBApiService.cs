using BeerTapExercise2.Model;
using IQ.Platform.Framework.WebApi;

namespace BeerTapExercise2.ApiServices
{
	public interface ISampleBApiService :
		IGetAResourceAsync<SampleResource, string>,
		IGetManyOfAResourceAsync<SampleResource, string>,
		ICreateAResourceAsync<SampleResource, string>,
		IUpdateAResourceAsync<SampleResource, string>,
		IDeleteResourceAsync<SampleResource, string>
	{
		SampleResource Get(int id);
	}
}
