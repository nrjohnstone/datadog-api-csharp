using System;

namespace DataDog.Api.Screenboards.Contracts
{
    public class ScreenboardSummary
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public string Resource { get; set; }
        public bool ReadOnly { get; set; }
    }
}