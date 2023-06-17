using AutoMapper.Configuration.Conventions;
using JobSeeker.DAL.Entities.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.BLL.Services.Parsers.Base
{
    public abstract class Parser
    {
        protected readonly WebClient _client = new();
        protected string _link;

        protected Parser()
        {
        }
    }
}
