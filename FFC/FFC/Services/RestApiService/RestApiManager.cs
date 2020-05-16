using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using FFC.Models;

namespace FFC.Services
{
    public class RestApiManager
    {
        IRestApiService restApiService;

        public RestApiManager(IRestApiService service)
        {
            restApiService = service;
        }

        //Gets all reference points from database
        public Task<ObservableCollection<Reference>> GetRefPointsAsync()
        {
            return restApiService.RefreshDataAsync();
        }

        //Posts reference point in database
        public Task PostRefPointAsync(Reference refPoint)
        {
            return restApiService.PostReferenceAsync(refPoint);
        }

        //Deletes a specific reference point according to ID from database
        public Task DeleteReferenceAsync(string id)
        {
            return restApiService.DeleteReferenceAsync(id);
        }

        //Gets specific reference point from database
        public Task<Reference> GetSpecificRefPointAsync(string id)
        {
            return restApiService.GetSpecificRefIDAsync(id);
        }

        //Deletes all reference points from database
        public Task DeleteAllRefPointsAsync()
        {
            return restApiService.DeleteAllRereferenceAsync();
        }

        //Reseeds table at complete deletion
        public Task PutRefPointAsync()
        {
            return restApiService.PutReferenceAsync();
        }
    }
}
