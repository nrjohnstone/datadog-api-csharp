using System;

namespace DataDog.Api.Screenboards.Contracts.Responses
{
    public class Screenboard
    {
        public string BoardTitle { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public bool ReadOnly { get; set; }
        public string BoardBgtype { get; set; }
        public DateTimeOffset Created { get; set; }
        public string OriginalTitle { get; set; }
        public DateTimeOffset Modified { get; set; }
        public bool Templated { get; set; }
        public bool Shared { get; set; }
        public int Id { get; set; }
        public bool TitleEdited { get; set; }
        public object Widgets { get; set; }
    }
}