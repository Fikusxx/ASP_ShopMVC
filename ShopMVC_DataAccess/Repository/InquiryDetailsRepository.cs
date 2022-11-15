using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMVC_Models;
using ShopMVC_Utility;
using System.ComponentModel.DataAnnotations;

namespace ShopMVC_DataAccess
{
    public class InquiryDetailsRepository : Repository<InquiryDetails>, IInquiryDetailsRepository
    {
        public InquiryDetailsRepository(ApplicationDbContext database) : base(database)
        { }

        public void Update(InquiryDetails obj)
        {
            db.InquiryDetails.Update(obj);
        }
    }
}
