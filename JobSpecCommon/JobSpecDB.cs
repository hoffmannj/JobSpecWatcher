using JobSpecCommon.Dto;
using System;
using System.Collections.Generic;

namespace JobSpecCommon
{
	public interface JobSpecDB
	{
		List<ReturnData> Query(string[] skills, int lastNDays, int resultDays);

        List<string> GetSkills();
    }
}
