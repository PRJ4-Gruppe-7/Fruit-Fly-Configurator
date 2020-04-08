using System.Collections.Generic;
using System.Threading.Tasks;
using FFC.Models;

namespace FFC.Services
{
    public interface IRestApiService
    {
        Task PostReferenceAsync(Reference item, bool isNewItem = false);

        Task<List<Reference>> RefreshDataAsync();
    }
}