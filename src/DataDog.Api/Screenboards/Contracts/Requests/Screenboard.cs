using System;
// ReSharper disable InconsistentNaming

namespace DataDog.Api.Screenboards.Contracts.Requests
{
    public class Screenboard
    {
        private string _description;

        public Screenboard()
        {
            template_variables = string.Empty;
            description = string.Empty;
        }

        public string board_title { get; set; }

        public string description {
            get { return _description; }
            set
            {
                if (value == null)
                    throw new InvalidOperationException();

                _description = value;
            }
        }
        public object widgets { get; set; }
        public object template_variables { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public bool read_only { get; set; }                
    }
}