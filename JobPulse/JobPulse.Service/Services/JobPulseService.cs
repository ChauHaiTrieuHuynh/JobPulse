using AutoMapper;
using HtmlAgilityPack;
using JobPulse.Data.Models;
using JobPulse.Data.Repositories;
using JobPulse.Service.DTOs;
using JobPulse.Service.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.Services
{
    public class JobPulseService : IJobPulseService
    {
        private readonly IJobSource _jobSource;
        private readonly IDataRepository<JobPosting> _repo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public JobPulseService(
            IJobSource jobSource, 
            ILogger<JobPulseService> logger, 
            IMapper mapper, 
            IDataRepository<JobPosting> repo)
        {
            _jobSource = jobSource;
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<IEnumerable<JobPostingDto>> GetLinkedInJobPostingClient()
        {
            try
            {
                string html = await _jobSource.FetchJobAsync();
                IEnumerable<JobPostingDto> listJob = await NormalizeHtmlJobPosting(html, JobSourceEnum.LinkedIn);
                return listJob;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get Job Source ");
                //sending warning email later
                //To Do List
                throw;
            }
        }

        public async Task<IEnumerable<JobPosting>> FilterNewJobsAsync
            (IEnumerable<JobPostingDto> linkedInJobsPosting, 
             IEnumerable<JobPostingDto> indeedJobPosting)
        {
            try
            {
                //default is empty collection so null is not happen
                IEnumerable<JobPostingDto> allJobs = indeedJobPosting.Concat(linkedInJobsPosting);
                List<JobPosting> newJobs = new ();
                foreach (var jobDto in allJobs)
                {
                    //get by id and check
                    JobPosting? existingJob = await _repo.GetByIdAndTypeAsync(jobDto.ExternalJobId, jobDto.JobSourceType);
                    if (existingJob == null)
                    {
                        JobPosting job = _mapper.Map<JobPosting>(jobDto);
                        newJobs.Add(job);
                    }
                }
                return newJobs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<IEnumerable<JobPostingDto>> NormalizeHtmlJobPosting(string html, JobSourceEnum jobSourceType)
        {
            try
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);
                List<JobPostingDto> jobs = new List<JobPostingDto>();
                switch (jobSourceType)
                {
                    case JobSourceEnum.LinkedIn:
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
                                PostedDate = postedDate,
                                JobSourceType = (int)JobSourceEnum.LinkedIn
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
                        break;

                    case JobSourceEnum.Indeed:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(
                            nameof(jobSourceType),
                            jobSourceType,
                            "Unsupported job source.");
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
