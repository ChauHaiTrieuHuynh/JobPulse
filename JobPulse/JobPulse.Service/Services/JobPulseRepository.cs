using JobPulse.Service.DTOs;
using Microsoft.Extensions.Logging;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using JobPulse.Data.Models;
using AutoMapper;

namespace JobPulse.Service.Services
{
    public class JobPulseRepository : IJobPulseRepository
    {
        private readonly IJobSource _jobSource;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public JobPulseRepository(IJobSource jobSource, ILogger<JobPulseRepository> logger, IMapper mapper)
        {
            _jobSource = jobSource;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IEnumerable<JobPostingDto>> GetLinkedInJobPostingClient()
        {
            try
            {
                string html = await _jobSource.FetchJobAsync();
                IEnumerable<JobPostingDto> listJob = await AnalyzeHtmlJobPosting(html);

                //To Do to add or doing something else
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get Job Source ");
                //sending warning email later
                //To Do List
                throw;
            }
        }
        private async Task<IEnumerable<JobPostingDto>> AnalyzeHtmlJobPosting(string html)
        {
            try
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);

                HtmlNode? rootlist = document.DocumentNode.SelectSingleNode("//ul[contains(@class, 'jobs-search__results-list')]");

                if (rootlist == null)
                {
                    return await Task.FromResult<IEnumerable<JobPostingDto>>(new List<JobPostingDto>());
                }
                HtmlNodeCollection? jobNodes = rootlist.SelectNodes("./li");

                if (jobNodes == null)
                {
                    return await Task.FromResult<IEnumerable<JobPostingDto>>(new List<JobPostingDto>());
                }

                List<JobPostingDto> jobs = new List<JobPostingDto>();
                foreach (HtmlNode jobNode in jobNodes)
                {
                    HtmlNode cardNode = jobNode.SelectSingleNode(".//div[contains(@class, 'job-search-card')]");
                    if (cardNode == null)
                    {
                        continue;
                    }

                    string jobId = cardNode
                        .GetAttributeValue("data-entity-urn", "")
                        .Replace("urn:li:jobPosting:", "");
                     
                    string title = jobNode
                        .SelectSingleNode(".//h3[contains(@class, 'base-search-card__title')]")
                        ?.InnerText.Trim() ?? "";

                    string company = jobNode
                        .SelectSingleNode(".//h4[contains(@class, 'base-search-card__subtitle')]")
                        ?.InnerText.Trim() ?? "";

                    string location = jobNode
                        .SelectSingleNode(".//span[contains(@class, 'job-search-card__location')]")
                        ?.InnerText.Trim() ?? "";

                    string url = jobNode
                        .SelectSingleNode(".//a[contains(@class, 'base-card__full-link')]")
                        ?.GetAttributeValue("href", "") ?? "";

                    string postedDate = jobNode
                        .SelectSingleNode(".//time")
                        ?.GetAttributeValue("datetime", "") ?? "";

                    JobPostingDto linkedInJob = new JobPostingDto
                    {
                        ExternalJobId = jobId,
                        Title = title,
                        Company = company,
                        Location = location,
                        JobUrl = url,
                        PostedDate = postedDate
                    };
                    jobs.Add(linkedInJob);
                    //_logger.LogInformation(
                    // "Job: {JobId} | {Title} | {Company} | {Location} | {PostedDate} | {JobUrl}",
                    // linkedInJob.ExternalJobId,
                    // linkedInJob.Title,
                    // linkedInJob.Company,
                    // linkedInJob.Location,
                    // linkedInJob.PostedDate, 
                    // linkedInJob.JobUrl);
                }
                return await Task.FromResult<IEnumerable<JobPostingDto>>(jobs);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
