﻿using System.Net.Http;
using System.Threading.Tasks;

using MEOT.lib.Containers;
using MEOT.lib.Objects;
using MEOT.lib.WebObjects;
using MEOT.Web.Clients.Base;

namespace MEOT.Web.Clients
{
    public class APIHttpClient : IHttpClient
    {
        public APIHttpClient(HttpClient httpClient) : base(httpClient)
        {
        }
        
        public async Task<Malware[]> GetMalwareDashboardAsync() => await GetAsync<Malware[]>("MalwareDashboard");

        public async Task<MalwareAnalysisContainer> GetAnalysisAsync(string query) => await GetAsync<MalwareAnalysisContainer>($"MalwareAnalysis/{query}");

        public async Task<VendorContainer> GetVendorDataAsync(string vendorName) =>
            await GetAsync<VendorContainer>($"VendorBreakdown/{vendorName}");

        public async Task<TrendingAnalysisContainer> GetTrendingDashboardAsync() =>
            await GetAsync<TrendingAnalysisContainer>("TrendingAnalysis");

        public async void AddMalware(NewMalwareRequestItem malwareItem) => await PostAsync<NewMalwareRequestItem>("MalwareManagement", malwareItem);
    }
}