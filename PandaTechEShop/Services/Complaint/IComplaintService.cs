using System;
using System.Threading.Tasks;
using PandaTechEShop.Models.Complaint;

namespace PandaTechEShop.Services.Complaint
{
    public interface IComplaintService
    {
        Task<bool> RegisterComplaintAsync(ComplaintInfo complaint);
    }
}
