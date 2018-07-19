using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.Sandbox.Model
{
    public class QueueResult
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string[] Msgs { get; set; }
    }
}
