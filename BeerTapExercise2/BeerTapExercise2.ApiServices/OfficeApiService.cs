using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeerTapExercise2.Model;
using Dapper;
using IQ.Platform.Framework.WebApi;

namespace BeerTapExercise2.ApiServices
{
	public class OfficeApiService : IOfficeApiService
	{
		private BeerTapContext _db;
		private SqlConnection _sql;

		private const string BeerTapServiceConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BeerTapService;Integrated Security=true";
		private const string GetOfficesSp = "GetOffices";
		private const string GetOfficeByIdSp = "GetOfficeById";
		private const string UpdateOfficeSp = "UpdateOffice";
		private const string DeleteOfficeSp = "DeleteOffice";

		public OfficeApiService()
		{
			_db = new BeerTapContext();
			_db.Database.Log = s => Debug.WriteLine(s);

			_sql = new SqlConnection(BeerTapServiceConnectionString);
		}

		private Office GetOffice(int id, IRequestContext context)
		{
			Office office;
			using (_sql)
			{
				_sql.Open();
				office = _sql.Query<Office>(GetOfficeByIdSp, new {Id = id}, commandType: CommandType.StoredProcedure).SingleOrDefault();
			}

			if (office == null)
				throw context.CreateNotFoundHttpResponseException<Office>();

			return office;
		}

		private IEnumerable<Office> GetOffices()
		{
			IEnumerable<Office> offices;
			using (_sql)
			{
				_sql.Open();
				offices = _sql.Query<Office>(GetOfficesSp, commandType: CommandType.StoredProcedure);
			}
			return offices;
		}

		private void UpdateOffice(Office resource, IRequestContext context)
		{
			using (_sql)
			{
				_sql.Open();
				try
				{
					_sql.Execute(UpdateOfficeSp, new {Id = resource.Id, Name = resource.Name}, commandType: CommandType.StoredProcedure);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!OfficeExists(resource.Id))
						throw context.CreateNotFoundHttpResponseException<Office>();

					throw;
				}
			}
		}

		private void DeleteOffice(int id)
		{
			using (_sql)
			{
				_sql.Open();
				_sql.Execute(DeleteOfficeSp, new {Id = id}, commandType: CommandType.StoredProcedure);
			}
		}

		public Task<Office> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
		{
			return Task.FromResult(GetOffice(id, context));
		}

		public Task<IEnumerable<Office>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
		{
			return Task.FromResult(GetOffices());
		}

		public async Task<ResourceCreationResult<Office, int>> CreateAsync(Office resource, IRequestContext context,
			CancellationToken cancellation)
		{
			/*using (_sql)
	        {
		        _sql.Open();
				resource.Id=_sql.Query<int>(
						@"
                           insert Offices(Name)
                           values (@OfficeName)
select cast(scope_identity() as int)
                        ", new {OfficeName=resource.Name}).SingleOrDefault();
				
	        }
			
			var result = new ResourceCreationResult<Office, int>(resource);
	        return result;*/

			_db.Offices.Add(resource);
			await _db.SaveChangesAsync(cancellation);

			var result = new ResourceCreationResult<Office, int>(resource);

			return result;
		}

		public Task<Office> UpdateAsync(Office resource, IRequestContext context, CancellationToken cancellation)
		{
			UpdateOffice(resource, context);
			return Task.FromResult(resource);
		}

		public Task DeleteAsync(ResourceOrIdentifier<Office, int> input, IRequestContext context,
			CancellationToken cancellation)
		{
			var office = input.HasResource ? input.Resource : GetOffice(input.Id, context);
			if (office == null)
				throw context.CreateNotFoundHttpResponseException<Office>();

			DeleteOffice(office.Id);
			return Task.FromResult(input);
		}

		private bool OfficeExists(int id)
		{
			return GetOffices().Any(o => o.Id == id);
		}
	}
}