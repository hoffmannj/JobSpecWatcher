using System;

namespace JobSpecCollector.Data
{
    public class JobSpec
    {
        public int? Id { get; set; }


        public string Title { get; set; }

        public string Link { get; set; }

        public string Source { get; set; }

        public Guid? Guid { get; set; }

        public DateTime? PubDate { get; set; }

        public string Description { get; set; }

        public string SpecText { get; set; }

    }
}