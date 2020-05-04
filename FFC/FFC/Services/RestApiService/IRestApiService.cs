using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FFC.Models;

namespace FFC.Services
{
    public interface IRestApiService
    {
        //POST reference point
        Task PostReferenceAsync(Reference item);

        //GET reference point
        Task<ObservableCollection<Reference>> RefreshDataAsync();

        // DELETE reference point by ID
        Task DeleteReferenceAsync(string id);

        //GET reference point by ID
        Task<Reference> GetSpecificRefID(string id);

        //DELETE all reference points in database
        Task DeleteAllRereferenceAsync();

        //PUT table in database
        Task PutReferenceAsync();
    }
}