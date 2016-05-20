using System;
using System.Collections.Generic;
using StyleguideGenerator.Models;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;

namespace StyleguideGenerator.Modules
{
    /// <summary>
    /// Модуль разбора css файла    
    /// </summary>
    public static class CssParseModule
    {
        private static readonly char[] _unitSplitChars = new[] { ' ', '>', '+', '~' };
        private static readonly char[] _partsSplitChars = new [] { '.', ':', '#', '[' };
        private static readonly string _selectorSplitStr = @"(\s+(?!(\>|\+|\~))|\>|\s*\+\s*|\~)";

        public static ParsedFile Parse(UnparsedFile file)
        {
            var parsedFile = new ParsedFile(file);            
            return parsedFile;
        }
        public static ParsedFile Parse(string name,string content)
        {
            var parsedFile = new ParsedFile(name);
            parsedFile.SelectorsLines = Parse(content);
            return parsedFile;
        }

        public static List<SelectorsLine> Parse(string content)
        {
            StringBuilder sb = new StringBuilder();
            var cindex = 0;
            while (cindex != -1)
            {
                var si = content.IndexOf(@"/*",cindex);
                var ei = -1;
                if (si != -1)
                {
                    ei = content.IndexOf(@"*/", si);
                    if (ei!=-1)
                        sb.Append(content.Substring(cindex, si - cindex));
                    else
                        sb.Append(content.Substring(cindex, content.Length - cindex));
                }
                else
                {
                    sb.Append(content.Substring(cindex, content.Length - cindex));
                }                
                cindex = ei ==-1 ? -1 : ei+2;                
            }
            content = sb.ToString();
            content = Regex.Replace(content, @"(\s+|\\n|\\r|\\t)"," ");
            var sp = content.Split(new[] { '{', '}' });
            List<SelectorsLine> filelines = new List<SelectorsLine>(sp.Length / 2 + 1);
            List<Selector> fileselectors = new List<Selector>(sp.Length / 2 + 1);

            var linePosition = 1;
            for (var i = 0; i < sp.Length - 1; i++)
            {
                
                if (i % 2 == 0)
                {
                    var line = new SelectorsLine();
                    line.Position = linePosition++;
                    var props = sp[i+1];
                    line.Prop = new Properties() { Value = props };
                    line.Str = sp[i].Trim();
                    line.Selectors = SplitSelectorsLine(line.Str,line.Prop,n => fileselectors.Find(l => l.Str == n));
                    filelines.Add(line);
                    fileselectors.AddRange(line.Selectors);      
                }
            }
            return filelines;
        }

        /// <summary>
        /// Разбор строки селекторов на селекторы
        /// </summary>
        /// <param name="selectorsLineStr">Строка с селекторами</param>
        /// <param name="lineprop">Свойства</param>
        /// <param name="repeatCheck">Функция проверки на повторение</param>
        /// <returns>Список селекторов</returns>
        private static List<Selector> SplitSelectorsLine(string selectorsLineStr,Properties lineprop, Func<string, Selector> repeatCheck)
        {
            var splitresult = selectorsLineStr.Split(new[] { ',' },StringSplitOptions.RemoveEmptyEntries);
            var selectors = new List<Selector>();
            for (var i = 0; i < splitresult.Length; i++)
            {
                var selector = repeatCheck(splitresult[i]);
                if (selector==null)
                {
                    selector = new Selector();                                   
                    selector.Str = splitresult[i].Trim();                    
                    selector.Units = SplitSelector(selector.Str);
                    //selector.Position = position++;
                }
                selector.PropertiesSet.Values.Add(lineprop);
                selectors.Add(selector);
            }
            return selectors;
        }
        /// <summary>
        /// Разбор селектора на простые селекторы
        /// </summary>
        /// <param name="selectorStr">Строка селектора</param>
        /// <returns>Список простых селекторов</returns>
        private static List<SelectorUnit> SplitSelector(string selectorStr)
        {           
            string[] regresult = Regex.Split(selectorStr, _selectorSplitStr).Where(e => e.Length > 0).ToArray();            
            if (regresult.Length == 0) return null;            
            var units = new List<SelectorUnit>();
            var funit = new SelectorUnit(regresult[0]);
            funit.Parts = SplitSelectorUnit(funit.Text);
            funit.Position = 1;
            units.Add(funit);
            int index = 1, position = 2;
            for (; index < regresult.Length; index++)
            {
                if (index % 2 == 0)
                {
                    var unit = new SelectorUnit(regresult[index]);
                    unit.Parts = SplitSelectorUnit(unit.Text.Trim());
                    unit.Position = position++;
                    unit.Connect = CheckSelectorUnitConnect(regresult[index - 1].Trim());
                    units.Add(unit);
                }
            }
            return units;
        }
        /// <summary>
        /// Разбор простого селектора на части
        /// </summary>
        /// <param name="unitStr">Строка простого селектора</param>
        /// <returns>Список частей простого селектора</returns>
        private static List<SelectorPart> SplitSelectorUnit(string unitStr)
        {
            int index = 1, position = 1;
            var parts = new List<SelectorPart>();
            while (index != -1 && unitStr.Length > 0)
            {
                var part = new SelectorPart();
                if (unitStr.Length > 1 && unitStr[1] == ':') index = 2;
                index = unitStr.IndexOfAny(_partsSplitChars, index);
                if (index != -1)
                {
                    var partstr = unitStr.Substring(0, index);
                    if (partstr[partstr.Length - 1] == '(')
                    {
                        index = unitStr.IndexOf(')') + 1;
                        partstr = unitStr.Substring(0, index);
                    }
                    part.Title = partstr;
                    unitStr = unitStr.Remove(0, index);
                    index = 1;
                    
                }
                else part.Title = unitStr;

                part.Type = CheckSelectorPartType(part.Title);
                part.Position = position++;
                parts.Add(part);
            }
            return parts;
        }
        

        private static SelectorPartType CheckSelectorPartType(string s)
        {
            if (s.StartsWith("."))
            {
                return SelectorPartType.Class;
            }
            else if (s.StartsWith("#"))
            {
                return SelectorPartType.ID;
            }
            else if (s.StartsWith("-"))
            {
                return SelectorPartType.VendorPref;
            }
            else if (s.StartsWith("["))
            {
                return SelectorPartType.Attribute;
            }
            else if (s.EndsWith(":before") || s.EndsWith(":after"))
            {
                return SelectorPartType.Pseudo;
            }
            else if (s.StartsWith("::"))
            {
                return SelectorPartType.VendorPref;
            }
            else if (s.StartsWith(":"))
            {
                return SelectorPartType.Sub;
            }
            else
            {
                return SelectorPartType.Tag;
            }
            //return SelectorType.Non;
        }

        private static SelectorUnitsConnect CheckSelectorUnitConnect(string s)
        {
            if (s.StartsWith(">"))
            {
                return SelectorUnitsConnect.Child;
            }
            else if (s.StartsWith("+"))
            {
                return SelectorUnitsConnect.Neighbor;
            }
            else if (s.StartsWith("~"))
            {
                return SelectorUnitsConnect.Next;
            }
            else
            {
                return SelectorUnitsConnect.Descend;
            }
        }
    }
}
