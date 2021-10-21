using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PlannerApp.Components
{
    public partial class PlansList
    {
        [Inject]
        public IPlanService PlanService{ get; set; }


        [Inject]
        public HttpClient HttpClient{ get; set; }

        private bool isBusy = default;
        private string errorMessage = default;
        private int pageSize = 10;
        private int pageNumber= 1;
        private string query = String.Empty;
        private int totalPages = 1;

        private List<PlanSummary> plans = new();



        private async Task<PagedList<PlanSummary>> GetPlansAsync(string query= "test",int pageNumber = 1, int pageSize=10)
        {


            isBusy = true;

            try
            {
                 var result = await PlanService.GetPlansAsync(query, pageNumber, pageSize);

                //var response = await HttpClient.GetAsync($"/api/v2/plans?query={query}&pageNumber={pageNumber}&pageSize={pageSize}");

               // var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedList<PlanSummary>>>();

                plans = result.Value.Records.ToList();
                pageNumber = result.Value.Page;
                pageSize = result.Value.PageSize;
                totalPages = result.Value.TotalPages;
                return result.Value;

            }
            catch (ApiException ex)
            {
                errorMessage = ex.ApiErrorResponse.Message;

                throw;
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;

                throw;
            }

            isBusy = false;

            return null;
        }

        #region View Toggler
        private bool _isCardsViewEnabled = true;

        private void SetCardsView()
        {
            _isCardsViewEnabled = true;
        }

        private void SetTableView()
        {
            _isCardsViewEnabled = false;
        }

        #endregion 

    }
}
