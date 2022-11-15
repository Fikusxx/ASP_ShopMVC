using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMVC_Models;
using ShopMVC_Utility;
using System.ComponentModel.DataAnnotations;

namespace ShopMVC_DataAccess
{
    public class InquiryHeaderRepository : Repository<InquiryHeader>, IInquiryHeaderRepository
    {
        public InquiryHeaderRepository(ApplicationDbContext database) : base(database)
        { }

        public void Update(InquiryHeader obj)
        {
            db.InquiryHeaders.Update(obj);
        }
    }
}
