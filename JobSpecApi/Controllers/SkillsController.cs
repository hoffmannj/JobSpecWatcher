using JobSpecCommon;
using JobSpecCommon.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobSpecApi.Controllers
{
    public class SkillsController : ApiController
    {
        private JobSpecCommon.JobSpecDB _db;

        public SkillsController(JobSpecCommon.JobSpecDB db)
        {
            _db = db;
        }

        public SkillsController() : this(JobSpecApi.App_Start.DIConfig.Get<JobSpecCommon.JobSpecDB>()) { }

        // GET api/skills
        public ApiResult<string> Get()
        {
            var result = new ApiResult<string>
            {
                Data = new List<string>(),
                ErrorMessages = new List<string>()
            };
            try {
                result.Data = _db.GetSkills();
            }
            catch(Exception ex)
            {
                result.HasError = true;
                result.ErrorMessages.Add("Error during data retrieval");
            }
            return result;
        }
    }
}
