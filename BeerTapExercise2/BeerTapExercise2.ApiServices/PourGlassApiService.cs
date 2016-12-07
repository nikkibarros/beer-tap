using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BeerTapExercise2.Model;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi;

namespace BeerTapExercise2.ApiServices
{
    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    public class PourGlassApiService : IPourGlassApiService
    {
        BeerTapContext db = new BeerTapContext();

        public Task<ResourceCreationResult<PourGlass, int>> CreateAsync(PourGlass resource, IRequestContext context,
            CancellationToken cancellation)
        {
            var officeId =
                context.UriParameters.GetByName<int>("OfficeId")
                    .EnsureValue(
                        () =>
                            context.CreateHttpResponseException<BeerTap>(
                                "The officeId must be supplied in the URI", HttpStatusCode.BadRequest));
            var beerTapId =
                context.UriParameters.GetByName<int>("BeerTapId")
                    .EnsureValue(
                        () =>
                            context.CreateHttpResponseException<BeerTap>(
                                "The BeerTapId must be supplied in the URI", HttpStatusCode.BadRequest));

            var beerTap = db.BeerTaps.Include(b => b.Office).SingleOrDefault(b => b.Id == beerTapId);
            if (beerTap == null)
            {
                throw context.CreateNotFoundHttpResponseException<BeerTap>();
            }

            beerTap.Volume -= resource.ML;

            if (beerTap.Volume == 0)
            {
                beerTap.BeerTapState = BeerTapState.Dry;
            }
            else if (beerTap.Volume < 50)
            {
                beerTap.BeerTapState = BeerTapState.AlmostGone;
            }
            else if (beerTap.Volume < 100)
            {
                beerTap.BeerTapState = BeerTapState.NotSoNew;
            }
            else
            {
                beerTap.BeerTapState = BeerTapState.New;
            }

            db.Entry(beerTap).State = EntityState.Modified;

            try
            {
                db.SaveChangesAsync(cancellation);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerTapExists(beerTap.Id))
                {
                    throw context.CreateNotFoundHttpResponseException<BeerTap>();
                }
                else
                {
                    throw;
                }
            }

            return Task.FromResult(new ResourceCreationResult<PourGlass, int>(resource));
        }

        private bool BeerTapExists(int id)
        {
            return db.BeerTaps.Count(e => e.Id == id) > 0;
        }
    }
}