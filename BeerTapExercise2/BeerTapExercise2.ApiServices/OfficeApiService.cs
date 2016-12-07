using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeerTapExercise2.Model;
using IQ.Platform.Framework.WebApi;

namespace BeerTapExercise2.ApiServices
{
    public class OfficeApiService : IOfficeApiService
    {
        BeerTapContext db = new BeerTapContext();

        public Task<Office> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {
            var office = db.Offices.FindAsync(cancellation, id);
            if (office == null)
            {
                throw context.CreateNotFoundHttpResponseException<Office>();
            }

            return office;
        }

        public Task<IEnumerable<Office>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            return Task.FromResult(db.Offices.AsEnumerable());
        }

        public Task<ResourceCreationResult<Office, int>> CreateAsync(Office resource, IRequestContext context,
            CancellationToken cancellation)
        {
            db.Offices.Add(resource);
            db.SaveChangesAsync(cancellation);

            var result = new ResourceCreationResult<Office, int>(resource);

            return Task.FromResult(result);
        }

        public Task<Office> UpdateAsync(Office resource, IRequestContext context, CancellationToken cancellation)
        {
            db.Entry(resource).State = EntityState.Modified;

            try
            {
                db.SaveChangesAsync(cancellation);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficeExists(resource.Id))
                {
                    throw context.CreateNotFoundHttpResponseException<Office>();
                }
                else
                {
                    throw;
                }
            }

            return Task.FromResult(resource);
        }

        public Task DeleteAsync(ResourceOrIdentifier<Office, int> input, IRequestContext context,
            CancellationToken cancellation)
        {
            return new Task(() =>
            {
                var office = input.HasResource ? input.Resource : db.Offices.Find(input.Id);
                if (office == null)
                {
                    throw context.CreateNotFoundHttpResponseException<Office>();
                }

                db.Offices.Remove(office);
                db.SaveChangesAsync(cancellation);
            });
        }

        private bool OfficeExists(int id)
        {
            return db.Offices.Count(e => e.Id == id) > 0;
        }
    }
}