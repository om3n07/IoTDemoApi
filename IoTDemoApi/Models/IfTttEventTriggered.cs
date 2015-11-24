using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTDemoApi.Models
{
    public class IfTttEventTriggered
    {
        public int IfTttEventTriggeredId { get; set; }
        public DateTime DateCreated { get; set; }
        public string EventTriggered { get; set; }
    }
}