using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StyleguideGenerator.Modules;

namespace StyleguideGenerator.Models
{
    
    /// <summary>
    /// Тип селектора
    /// </summary>
    public enum SelectorPartType
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

    public enum SelectorUnitsConnect
    {
        First,
        Descend,
        Child,
        Neighbor,
        Next
    }

    /// <summary>
    /// Свойство селектора
    /// </summary>
    public class Properties
    {
        public string Value { get; set; }
    }

    /// <summary>
    /// Список свойств селекторов
    /// </summary>
    public class PropertiesSet
    {
        public List<Properties> Values;

        public PropertiesSet()
        {
            Values = new List<Properties>();
        }

        public PropertiesSet(Properties v)
        {
            Values = new List<Properties>() { v };
        }
    }

    /// <summary>
    /// Часть простого селектора
    /// </summary>
    public class SelectorPart
    {
        /// <summary>
        /// Текст
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Тип
        /// </summary>
        public SelectorPartType Type { get; set; }

        /// <summary>
        /// Позиция
        /// </summary>
        public int Position { get; set; }
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        /// 
        public Guid Ident { get; set; }

        public SelectorPart():this("")
        {
        }
        public SelectorPart(string title)
        {
            Title = title;
            Type = SelectorPartType.Non;
            Ident = Guid.NewGuid();
        }
    }

    /// <summary>
    /// Простой селектор
    /// </summary>
    public class SelectorUnit
    {
        /// <summary>
        /// Текст
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Части простого селектора
        /// </summary>
        public List<SelectorPart> Parts { get; set; }

        public SelectorUnitsConnect Connect { get; set; }

        /// <summary>
        /// Позиция
        /// </summary>
        public int Position { get; set; }
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Ident { get; set; }        

        public SelectorUnit() : this("")
        {
        }
        public SelectorUnit(string s)
        {
            Text = s;
            Ident = Guid.NewGuid();
            Parts = new List<SelectorPart>();
            Connect = SelectorUnitsConnect.First;
        }
    }
        
    /// <summary>
    /// Селектор
    /// </summary>
    public class Selector
    {
        /// <summary>
        /// Строка селектора
        /// </summary>
        public string Str;
        /// <summary>
        /// Простые селекторы
        /// </summary>
        public List<SelectorUnit> Units;
        /// <summary>
        /// Свойства 
        /// </summary>
        public PropertiesSet PropertiesSet { get; set; }
        ///// <summary>
        ///// Позиция
        ///// </summary>
        //public int Position { get; set; }

        public Selector()
        {
            Units = new List<SelectorUnit>();
            Str = "";
            PropertiesSet = new PropertiesSet();
        }
    }

    /// <summary>
    /// Строка селекторов
    /// </summary>
    public class SelectorsLine
    {
        /// <summary>
        /// Строка
        /// </summary>
        public string Str { get; set; }
        /// <summary>
        /// Список селекторов
        /// </summary>
        public List<Selector> Selectors { get; set; }
        /// <summary>
        /// Свойства
        /// </summary>
        public Properties Prop { get; set; }
        /// <summary>
        /// Позиция
        /// </summary>
        public int Position { get; set; }

        public SelectorsLine()
        {
            Selectors = new List<Selector>();
        }
    }

    

    public class UnparsedFile
    {
        public string Name { get; set; }

        public string FilesystemName { get; set; }

        public string Content { get; set; }

        public UnparsedFile(string name,string content,string fsname)
        {
            Name = name;
            Content = content;
            FilesystemName = fsname;
        }        
    }
    public class ParsedFile
    {
        public string Name { get; set; }

        public string FilesystemName { get; set; }

        public List<SelectorsLine> SelectorsLines { get; set; }

        UnparsedFile unparsedFile { get; set; }

        public ParsedFile(string name)
        {
            Name = name;
            SelectorsLines = new List<SelectorsLine>();
            unparsedFile = null;
        }
        public ParsedFile(UnparsedFile upfile)
        {
            Name = upfile.Name;
            FilesystemName = upfile.FilesystemName;
            SelectorsLines = CssParseModule.Parse(upfile.Content);
            unparsedFile = upfile;
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
