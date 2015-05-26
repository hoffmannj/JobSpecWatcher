using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSpecDB
{
    public class DB : JobSpecCommon.JobSpecDB
    {
        static DB()
        {
            Database.SetInitializer<JobSpecDB.Data.Specs>(null);
        }

        public List<JobSpecCommon.Dto.ReturnData> Query(string[] skills, int lastNDays, int resultDays)
        {
            var d1 = DateTime.Now.AddDays(-lastNDays);
            var d2 = DateTime.Now.AddDays(-resultDays);
            using (var db = new Data.Specs())
            {
                var l = (from word in db.words
                         join jsword in db.jobspecwords
                             on word.wordId equals jsword.wordId
                         join js in db.jobspecs
                             on jsword.jobspecId equals js.jobspecId
                         where
                             word.skillName != null &&
                             skills.Contains(word.skillName) &&
                             js.pubdate >= d1
                         group word by word.skillName into s
                         select new
                         {
                             skill = s.Key,
                             count = s.Count()
                         }).OrderByDescending(e => e.count).Take(5);

                var qq = (from word in db.words
                          join jsword in db.jobspecwords
                              on word.wordId equals jsword.wordId
                          join js in db.jobspecs
                              on jsword.jobspecId equals js.jobspecId
                          join ll in l
                              on word.skillName equals ll.skill
                          where
                              js.pubdate >= d2
                          select new { skill = word.skillName, date = js.pubdate, jsid = js.jobspecId }
                        ).ToList();
                return qq.Select(q => new { skill = q.skill, date = q.date.Date, jsid = q.jsid })
                    .GroupBy(x => new { x.skill, x.date })
                    .OrderBy(x => x.Key.date)
                    .Select(x => new JobSpecCommon.Dto.ReturnData
                    {
                        Skill = x.Key.skill,
                        Date = x.Key.date,
                        Count = x.Count()
                    }).ToList();
            }
        }

        public List<string> GetSkills()
        {
            using (var db = new Data.Specs())
            {
                return (from word in db.words
                        where word.skillName != null
                        select word.skillName).Distinct().ToList();
            }
        }
    }
}
