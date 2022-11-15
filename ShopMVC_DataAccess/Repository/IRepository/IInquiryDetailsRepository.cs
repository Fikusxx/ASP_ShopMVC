using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMVC_Models;

namespace ShopMVC_DataAccess
{
    public interface IInquiryDetailsRepository : IRepository<InquiryDetails>
    {
        public void Update(InquiryDetails inquiryDetails);
    }
}
