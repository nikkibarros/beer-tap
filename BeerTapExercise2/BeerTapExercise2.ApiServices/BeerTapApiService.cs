using System.Collections.Generic;
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
    public class BeerTapApiService : IBeerTapApiService
    {
        BeerTapContext db = new BeerTapContext();

        public async Task<BeerTap> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {
            var beerTap = await db.BeerTaps.Include(b => b.Office).SingleOrDefaultAsync(b => b.Id == id, cancellation);
            if (beerTap == null)
            {
                throw context.CreateNotFoundHttpResponseException<BeerTap>();
            }

            return beerTap;
        }

        public Task<IEnumerable<BeerTap>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            var officeId =
                context.UriParameters.GetByName<int>("OfficeId")
                    .EnsureValue(
                        () =>
                            context.CreateHttpResponseException<BeerTap>(
                                "The officeId must be supplied in the URI", HttpStatusCode.BadRequest));
            var beerTapsInOffice = db.BeerTaps.Where(b => b.OfficeId == officeId);
            return Task.FromResult(beerTapsInOffice.AsEnumerable());
        }

        public async Task<ResourceCreationResult<BeerTap, int>> CreateAsync(BeerTap resource, IRequestContext context,
            CancellationToken cancellation)
        {
            db.BeerTaps.Add(resource);
            await db.SaveChangesAsync(cancellation);

            db.Entry(resource).Reference(x => x.Office).Load();

            var result = new ResourceCreationResult<BeerTap, int>(resource);

            return result;
        }

        public async Task<BeerTap> UpdateAsync(BeerTap resource, IRequestContext context, CancellationToken cancellation)
        {
            db.Entry(resource).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync(cancellation);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerTapExists(resource.Id))
                {
                    throw context.CreateNotFoundHttpResponseException<BeerTap>();
                }
                else
                {
                    throw;
                }
            }

            return resource;
        }

        public Task DeleteAsync(ResourceOrIdentifier<BeerTap, int> input, IRequestContext context,
            CancellationToken cancellation)
        {
            var beerTap = input.HasResource ? input.Resource : db.BeerTaps.Find(input.Id);
            if (beerTap == null)
            {
                throw context.CreateNotFoundHttpResponseException<BeerTap>();
            }

            db.BeerTaps.Remove(beerTap);
            db.SaveChangesAsync(cancellation);

            return Task.FromResult(input);
        }

        private bool BeerTapExists(int id)
        {
            return db.BeerTaps.Count(e => e.Id == id) > 0;
        }
    }
}