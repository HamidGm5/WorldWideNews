﻿using WorldWideNews.API.Entities;

namespace WorldWideNews.API.Repository.Interfaces
{
    public interface IReporterRepository
    {
        Task<ICollection<Reporter>> GetReporters();
        Task<Reporter> GetReporterByID(int ReporterID);
        Task<ICollection<Reporter>> GetAgencyReporters(string ReporterName);
        Task<bool> AddNewReporter(Reporter NewReporter);
        Task<bool> UpdateReporter(Reporter NewReporter);
        Task<bool> DeleteReporter(int ID);
        Task<bool> Save();
    }
}