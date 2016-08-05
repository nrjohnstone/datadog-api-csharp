using System;

namespace DataDog.Api.Screenboards.Contracts.Responses
{
    public class ScreenboardSummary
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }

        public string Resource { get; set; }
        public bool ReadOnly { get; set; }
    }
}