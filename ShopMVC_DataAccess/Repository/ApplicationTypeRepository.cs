using ShopMVC_Models;

namespace ShopMVC_DataAccess
{
	public class ApplicationTypeRepository : Repository<ApplicationType>, IApplicationTypeRepository
	{
		public ApplicationTypeRepository(ApplicationDbContext database) : base(database)
		{ }

		public void Update(ApplicationType obj)
		{
			var appType = db.ApplicationTypes.FirstOrDefault(x => x.Id == obj.Id);

			if (appType != null)
			{
				appType.Name = obj.Name;
				db.ApplicationTypes.Update(appType);
			}
		}
	}
}
