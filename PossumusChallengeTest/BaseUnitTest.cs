using API.PossumusChallenge.Utility;
using AutoMapper;
using Core.Infraestructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumusChallengeTest
{
    public class BaseUnitTest
    {
        public IHttpContextAccessor _httpContextAccessor;
        protected ApplicationDBContext ContextBuild(string nameDB) {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(nameDB).Options;

            var dbContext = new ApplicationDBContext(options, _httpContextAccessor);
            return dbContext;
        }
        protected IMapper ConfigureAutoMapper() {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapperProfile());
            });
            return config.CreateMapper();
        }
    }
}
