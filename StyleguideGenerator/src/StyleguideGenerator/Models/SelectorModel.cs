using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StyleguideGenerator.Models
{
    

    public enum SelectorType
    {
        Non,
        Tag,
        ID,
        Class,
        Attribute,
        VendorPref,
        Pseudo,
        Sub,
        Bind
    }

    public class SelectorUnit
    {
        public string Name { get; set; }
        public SelectorType Type { get; set; }

        public Guid Ident { get; set; }

        public int Index { get; set; }

        public SelectorUnit():this("")
        {
        }

        public SelectorUnit(string name)
        {
            Name = name;            
            Type = SelectorType.Non;
            Ident = new Guid();
        }
    }


    public class Selector
    {
        public string Text { get; set; }

        public List<SelectorUnit> Units { get; set; }        

        public Guid Ident { get; set; }

        public int Index { get; set; }

        public Selector() : this("")
        {
        }
        public Selector(string s)
        {
            Text = s;
            Ident = new Guid();
            Units = new List<SelectorUnit>();
        }
    }

    public class SelectorProrepty
    {
        public string Value { get; set; }
    }

    public class Properties
    {
        public List<SelectorProrepty> Values;

        public Properties()
        {
            Values = new List<SelectorProrepty>();
        }

        public Properties(SelectorProrepty v)
        {
            Values = new List<SelectorProrepty>() { v };
        }
    }

    public class SelectorsLine
    {
        public string StrLine;

        public List<Selector> Selectors;

        public Properties Properties { get; set; }

        public int Index;

        public SelectorsLine()
        {
            Selectors = new List<Selector>();
            StrLine = "";
            Properties = new Properties();
        }
    }

    public class SelectorsLineSet
    {
        public int Index { get; set; }

        public string Str { get; set; }

        public List<SelectorsLine> Set { get; set; }

        public SelectorProrepty Prop { get; set; }

        public SelectorsLineSet()
        {
            Set = new List<SelectorsLine>();
        }
    }

    public class STree
    {
        public List<SNode> Nodes;

        public STree()
        {
            Nodes = new List<SNode>();
        }
    }

    public class SNode
    {
        public SNode Parent;

        public List<SNode> Childs;

        public SNode()
        {
            Parent = null;
            Childs = new List<SNode>();
        }
    }
}
