using System;
using System.Collections.Generic;
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

        public Task<List<Reference>> GetRefPointsAsync()
        {
            return restApiService.RefreshDataAsync();
        }

        public Task PostRefPointAsync(Reference refPoint, bool isNewItem = false)
        {
            return restApiService.PostReferenceAsync(refPoint, isNewItem);
        }
    }
}
