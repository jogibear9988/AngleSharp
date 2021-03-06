﻿namespace Performance
{
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            UrlTest.UseBuffer = true;

            var tests = new List<ITest>
            {
                UrlTest.For("http://www.amazon.com").Result,
                UrlTest.For("http://www.blogspot.com").Result,
                UrlTest.For("http://www.smashing.com").Result,
                UrlTest.For("http://www.youtube.com").Result,
                UrlTest.For("http://www.weibo.com").Result,
                UrlTest.For("http://en.wikipedia.org").Result,
                UrlTest.For("http://www.w3.org").Result,
                UrlTest.For("http://www.yahoo.com").Result,
                UrlTest.For("http://www.google.com").Result,
                UrlTest.For("http://www.linkedin.com").Result,
                UrlTest.For("http://www.pinterest.com").Result,
                UrlTest.For("http://news.google.com").Result,
                UrlTest.For("http://www.baidu.com").Result,
                UrlTest.For("http://www.codeproject.com").Result,
                UrlTest.For("http://www.ebay.com").Result,
                UrlTest.For("http://www.msn.com").Result,
                UrlTest.For("http://www.nbc.com").Result,
                UrlTest.For("http://www.qq.com").Result,
                UrlTest.For("http://www.florian-rappl.de").Result,
                UrlTest.For("http://www.stackoverflow.com").Result,
                UrlTest.For("http://www.html5rocks.com/en").Result,
                UrlTest.For("http://www.live.com").Result,
                UrlTest.For("http://www.taobao.com").Result,
                UrlTest.For("http://www.huffingtonpost.com").Result,
                UrlTest.For("http://www.wordpress.org").Result,
                UrlTest.For("http://www.myspace.com").Result,
                UrlTest.For("http://www.flickr.com").Result,
                UrlTest.For("http://www.godaddy.com").Result,
                UrlTest.For("http://www.reddit.com").Result,
                UrlTest.For("http://www.nytimes.com").Result
            };

            var parsers = new List<IHtmlParser>
            {
                new AngleSharpParser(),
                new CsQueryParser(),
                new AgilityPackParser()
            };

            //Majestic is neither HTML5 conform, nor building a realistic DOM structure.
            //Therefore Majestic has been excluded. You could, however, just re-enable
            //it by uncommenting the following line.
            //parsers.Add(new MajesticParser());

            var testsuite = new TestSuite
            {
                Parsers = parsers,
                Tests = tests,
                NumberOfRepeats = 5,
                NumberOfReRuns = 1
            };

            testsuite.Run();
        }
    }
}
