using System.Threading.Tasks;
using PandaTechEShop.Models.Complaint;
using Refit;

namespace PandaTechEShop.Services.Complaint
{
    public interface IComplaintApi
    {
        [Post("/api/Complaints")]
        Task RegisterComplaint([Body] ComplaintInfo complaint, [Authorize("Bearer")] string token);
    }
}
