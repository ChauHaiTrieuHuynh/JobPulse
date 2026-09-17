using AutoMapper;
using JobPulse.Data.Models;
using JobPulse.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Service.MapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<JobPostingDto, JobPosting>().ReverseMap();
        }
    }
}
