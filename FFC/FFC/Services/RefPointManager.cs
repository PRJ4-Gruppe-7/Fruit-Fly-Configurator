using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using FFC.Models;

namespace FFC.Services
{
    public class RefPointManager
    {
        IRestApiService restApiService;

        public RefPointManager(IRestApiService service)
        {
            restApiService = service;
        }

        public Task<ObservableCollection<Reference>> GetRefPointsAsync()
        {
            return restApiService.RefreshDataAsync();
        }

        public Task PostRefPointAsync(Reference refPoint, bool isNewItem = false)
        {
            return restApiService.PostReferenceAsync(refPoint);
        }

        public Task DeleteReferenceAsync(string id)
        {
            return restApiService.DeleteReferenceAsync(id);
        }

        public Task<Reference> GetSpecificRefPointAsync(string id)
        {
            return restApiService.GetSpecificRefID(id);
        }
    }
}
