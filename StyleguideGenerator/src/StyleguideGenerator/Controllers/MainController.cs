using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Sqlite;
using SFile = System.IO.File;
using Microsoft.Extensions.PlatformAbstractions;

namespace StyleguideGenerator.Controllers
{
    public class MainController : Controller
    {
        private readonly IApplicationEnvironment _appEnvironment;

        public MainController(IApplicationEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            string stream = SFile.ReadAllText(_appEnvironment.ApplicationBasePath+"/Src/Styles/sss.css");
            ViewBag.st = stream;            

            var sp = stream.Split(new[] { '{', '}' });
            Dictionary<string,List<Selector>> spp = new Dictionary<string, List<Selector>>(sp.Length / 2 + 1);
            spp.Add("class", new List<Selector>());
            spp.Add("id", new List<Selector>());
            spp.Add("browser pref", new List<Selector>());
            spp.Add("attr", new List<Selector>());
            spp.Add("tag", new List<Selector>());


            var j = 0;
            for(var i = 0; i < sp.Length-1; i++)
            {
                if (i % 2 == 0)
                {
                    if (sp[i].StartsWith("."))
                    {
                        spp.ElementAt(0).Value.Add(new Selector(sp[i], sp[i++]));
                    }
                    else if (sp[i].StartsWith("#"))
                    {
                        spp.ElementAt(1).Value.Add(new Selector(sp[i], sp[i++]));
                    }
                    else if (sp[i].StartsWith("-"))
                    {
                        spp.ElementAt(2).Value.Add(new Selector(sp[i], sp[i++]));
                    }
                    else if (sp[i].StartsWith("["))
                    {
                        spp.ElementAt(3).Value.Add(new Selector(sp[i], sp[i++]));
                    }
                    else
                    {
                        spp.ElementAt(4).Value.Add(new Selector(sp[i], sp[i++]));
                    }
                }                    
            }

            

            ViewBag.sp = spp;            
            using (var connection = new SqliteConnection("" +new SqliteConnectionStringBuilder{DataSource = "SggDb.db"}))
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

    public class Selector
    {
        public string selector { get; set; }

        public string values { get; set; }

        public Selector(string s, string v)
        {
            selector = s;
            values = v;
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