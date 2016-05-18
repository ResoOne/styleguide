using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Sqlite;
using SFile = System.IO.File;
using StyleguideGenerator.Models;

using Microsoft.AspNet.Hosting;
using StyleguideGenerator.Modules;

namespace StyleguideGenerator.Controllers
{
    public class MainController : BaseController
    {
        public MainController(IHostingEnvironment hostEnvironment) : base(hostEnvironment)
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            string filename = "sss.css";
            string path = "/Src/Styles/" + filename;
            var g = UserName + "_" + Guid.NewGuid().ToString("N") + "_" + filename;
            string stream = SFile.ReadAllText(_appEnvironment.ApplicationBasePath + "/Src/Styles/sss.css");

            UnparsedFile unpfile = new UnparsedFile(filename, stream, g);

            var t = CssParseModule.Parse(unpfile);
            ViewBag.lines = t.SelectorsLines;

            //ViewBag.st = stream;

            using (var connection = new SqliteConnection("" + new SqliteConnectionStringBuilder { DataSource = "SggDb.db" }))
            {
                connection.Open();

                //using (var transaction = connection.BeginTransaction())
                //{
                //    var insertCommand = connection.CreateCommand();
                //    insertCommand.Transaction = transaction;
                //    insertCommand.CommandText = "INSERT INTO message ( text ) VALUES ( $text )";
                //    insertCommand.Parameters.AddWithValue("$text", "Hello, World!");
                //    insertCommand.ExecuteNonQuery();

                //    var selectCommand = connection.CreateCommand();
                //    selectCommand.Transaction = transaction;
                //    selectCommand.CommandText = "SELECT text FROM message";
                //    using (var reader = selectCommand.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {
                //            var message = reader.GetString(0);
                //            Console.WriteLine(message);
                //        }
                //    }

                //    transaction.Commit();
                //}
                Console.WriteLine("Connection Open");
            }


            var list = new List<string>();
            list.Add(Guid.NewGuid().ToString("N"));
            list.Add(Guid.NewGuid().ToString("N"));

            ViewBag.g = list;
            return View();
        }
    }
}


//var REGEX_SINGLE_PSEUDO_ELEMENT = @"[:]{1,2}(?:first\-(letter|line)|before|after|selection|value|choices|repeat\-(item|index)|outside|alternate|(line\-)?marker|slot\([_a-z0-9\-\+\.\\]*\))";
//var REGEX_PSEUDO_ELEMENTS = @"([:]{1,2}(?:first\-(letter|line)|before|after|selection|value|choices|repeat\-(item|index)|outside|alternate|(line\-)?marker|slot\([_a-z0-9\-\+\.\\]*\)))";
//var REGEX_PSEUDO_CLASSES_EXCEPT_NOT = @"([:](?:(link|visited|active|hover|focus|lang|root|empty|target|enabled|disabled|checked|default|valid|invalid|required|optional)|((in|out\-of)\-range)|(read\-(only|write))|(first|last|only|nth)(\-last)?\-(child|of\-type))(?:\([_a-z0-9\-\+\.\\]*\))?)";
//var REGEX_ATTR_SELECTORS = @"(\[\s*[_a-z0-9-:\.\|\\]+\s*(?:[~\|\*\^\$]?=\s*[\""\'][^\""\']*[\""\'])?\s*\])";
//var REGEX_ID_SELECTORS = @"(#[a-z]+[_a-z0-9-:\\]*)";
//var REGEX_CLASS_SELECTORS = @"(\.[_a-z]+[_a-z0-9-:\\]*)";
//var IMPORTANT_RULE = @"\!\s*important\s*$";
//var REGEX_TAG_RULE = @"(\b[.#]{0}(body|div|a|h1|h2|h3|h4|h5|p|span)\b)";
//Dictionary<string, MatchCollection> dic = new Dictionary<string, MatchCollection>();
//dic.Add(nameof(REGEX_SINGLE_PSEUDO_ELEMENT), new Regex(REGEX_SINGLE_PSEUDO_ELEMENT).Matches(stream));
//dic.Add(nameof(REGEX_PSEUDO_ELEMENTS), new Regex(REGEX_PSEUDO_ELEMENTS).Matches(stream));
//dic.Add(nameof(REGEX_PSEUDO_CLASSES_EXCEPT_NOT), new Regex(REGEX_PSEUDO_CLASSES_EXCEPT_NOT).Matches(stream));
//dic.Add(nameof(REGEX_ATTR_SELECTORS), new Regex(REGEX_ATTR_SELECTORS).Matches(stream));
//dic.Add(nameof(REGEX_ID_SELECTORS), new Regex(REGEX_ID_SELECTORS).Matches(stream));
//dic.Add(nameof(REGEX_CLASS_SELECTORS), new Regex(REGEX_CLASS_SELECTORS).Matches(stream));
//dic.Add(nameof(IMPORTANT_RULE), new Regex(IMPORTANT_RULE).Matches(stream));
//dic.Add(nameof(REGEX_TAG_RULE), new Regex(REGEX_TAG_RULE).Matches(stream));



//for (var i = 0; i < sp.Length - 1; i++)
//{
//    if (i % 2 == 0)
//    {
//        int j = i + 1;
//        var props = sp[j];
//        var set = new SelectorsLine();
//        set.Position = setIndex++;
//        set.Prop = new Properties() { Value = props };
//        set.Str = sp[i].Trim();

//        var sl_lines = set.Str.Split(new[] { ',' });
//        var lineIndex = 1;
//        for (var z = 0; z < sl_lines.Length; z++)
//        {
//            Selector line = new Selector();
//            var defl = lns.Find(l => l.Str == sl_lines[z]);
//            if (defl != null)
//            {
//                line = defl;
//                line.PropertiesSet.Values.Add(set.Prop);
//            }
//            else
//            {
//                line.Position = lineIndex++;
//                line.Str = sl_lines[z];
//                line.PropertiesSet.Values.Add(set.Prop);

//                var slr = line.Str.Split(new[] { ' ', '>', '+', '~' });
//                int slpos = 1;
//                for (var k = 0; k < slr.Length; k++)
//                {
//                    var sl = new SelectorUnit(slr[k]);
//                    sl.Position = slpos++;
//                    var txt = slr[k];
//                    int ix = 1, spos = 1;
//                    while (ix != -1 && txt.Length > 0)
//                    {
//                        var su = new SelectorPart();
//                        if (txt.Length > 1 && txt[1] == ':') ix = 2;
//                        ix = txt.IndexOfAny(arr, ix);
//                        if (ix != -1)
//                        {
//                            var ssss = txt.Substring(0, ix);
//                            if (ssss[ssss.Length - 1] == '(')
//                            {
//                                ix = txt.IndexOf(')') + 1;
//                                ssss = txt.Substring(0, ix);
//                            }
//                            txt = txt.Remove(0, ix);
//                            ix = 1;
//                            su.Title = ssss;
//                        }
//                        else su.Title = txt;
//                        su.Type = CheckSelectorPartType(su.Title);
//                        su.Position = spos++;
//                        sl.Parts.Add(su);
//                    }
//                    line.Units.Add(sl);
//                }
//                lns.Add(line);
//            }
//            set.Selectors.Add(line);
//        }
//    }
//}       