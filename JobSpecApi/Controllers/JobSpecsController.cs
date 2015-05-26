using JobSpecApi.Models;
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
    public class JobSpecsController : ApiController
    {
        private JobSpecCommon.JobSpecDB _db;

        public JobSpecsController(JobSpecCommon.JobSpecDB db)
        {
            _db = db;
        }

        public JobSpecsController() : this(JobSpecApi.App_Start.DIConfig.Get<JobSpecCommon.JobSpecDB>()) { }

        // GET api/jobspecs?Skills=c%23&LastNDays=7&ResultDays=90
        // skills should be URL Encoded
        public ApiResult<ReturnData> Get([FromUri]CallParams parameters)
        {
            var result = new ApiResult<ReturnData>
            {
                Data = new List<ReturnData>(),
                ErrorMessages = new List<string>()
            };

            if (parameters == null) return result;
            if (string.IsNullOrEmpty(parameters.Skills)) return result;
            if (parameters.LastNDays <= 0) return result;
            if (parameters.ResultDays <= 0) return result;
            var skills = parameters.Skills.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (skills.Length == 0) return result;

            try {
                result.Data = _db.Query(skills, parameters.LastNDays, parameters.ResultDays);
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
