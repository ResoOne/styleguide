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

        public string HighlightedDescription { get; set; }

        public string SubDescription { get; set; }

        public string ImportantDesc { get; set; }

        public List<SectionOptionsGroup> OptionsGroups { get; set; }
        
        public List<Sample> Samples { get; set; }  

        public List<Section> ChildList { get; set; }
    }
}
