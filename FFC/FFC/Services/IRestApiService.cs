using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FFC.Models;

namespace FFC.Services
{
    public interface IRestApiService
    {
        Task PostReferenceAsync(Reference item);

        Task<List<Reference>> RefreshDataAsync();

        Task DeleteReferenceAsync(string id);

        Task<Reference> GetSpecificRefID(string id);
    }
}