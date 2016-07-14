using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StyleguideGenerator.Models.Data
{
    public class Section
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImportantDesc { get; set; }

        public Dictionary<string,string> Vars { get; set; }

        public List<SubSection> ChildList { get; set; }
    }

    public class SubSection :Section
    {
        public List<VarExample> VarExamples { get; set; }
    }

    public class SectionExample
    {
        
    }

    public class VarExample
    {
        public string Value { get; set; }

        public string Text { get; set; }

        public string Example { get; set; }
    }
}
