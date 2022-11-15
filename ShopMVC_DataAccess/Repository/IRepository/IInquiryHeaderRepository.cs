using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMVC_Models;

namespace ShopMVC_DataAccess
{
    public interface IInquiryHeaderRepository : IRepository<InquiryHeader>
    {
        public void Update(InquiryHeader inquiryHeader);
    }
}
