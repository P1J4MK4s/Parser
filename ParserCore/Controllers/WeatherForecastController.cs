using AngleSharp;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Mvc;
using ParserCore.Models;
using System;
using System.Linq;

namespace ParserCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ItemsContext db;
        public WeatherForecastController(ItemsContext context)
        {
            db = context;
        }

        [HttpGet]
        public string Get()
        {
            string outString = "";
            string path = "https://www.avtoall.ru/catalog/paz-20/avtobusy-36/paz_672m-393/";
            HtmlAgilityPack.HtmlWeb connection = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument page = connection.Load(path);
            var div = page.DocumentNode.SelectSingleNode(".//ul[@class='catalog-groups-tree']");
            foreach (var list in div.SelectNodes(".//li"))
            {
                var Big = list.SelectSingleNode(".//a[@href='#']");
                if (Big != null)
                {
                    Tree dbTree = new Tree();
                    dbTree.Name = Big.InnerText;
                    db.Trees.Add(dbTree);
                    foreach (var item in list.SelectNodes(".//li"))
                    {
                        var middle = item.SelectSingleNode(".//a[@href='#']");
                        if (middle != null)
                        {
                            FirstSub firstSub = new FirstSub();
                            firstSub.Name = middle.InnerText;
                            firstSub.TreeId = dbTree.Id; //?
                            db.FirstSubs.Add(firstSub);
                            foreach (var smal in item.SelectNodes(".//li"))
                            {
                                var small = smal.SelectSingleNode(".//a[@data-id]");
                                if (small != null)
                                {
                                    SecondSub secondSub = new SecondSub();
                                    secondSub.IdFirstSub = firstSub.Id;//?
                                    secondSub.Link = small.GetAttributeValue("href", "NULL");
                                    secondSub.Name = small.InnerText;
                                    db.SecondSubs.Add(secondSub);
                                }
                            }
                        }
                    }
                }
                db.SaveChanges();
            }
            foreach (var sub in db.SecondSubs.ToList())
            {
                var link = sub.Link;
                Item item = new Item();
                HtmlAgilityPack.HtmlDocument subPage = connection.Load("https://www.avtoall.ru" + link);
                var number = subPage.DocumentNode.SelectSingleNode("//th[@class='number']");
                if (number.InnerText != "Номер детали")
                {
                    item.Number = number.InnerText;
                }
                var name = subPage.DocumentNode.SelectSingleNode(".//th[@class='name']");
                if (name.InnerText != "Наименование")
                {
                    item.Name = name.InnerText;
                }
                var count = subPage.DocumentNode.SelectSingleNode(".//th[@class='count']");
                if (count.InnerText != "Кол-во на модель")
                {
                    item.Count = count.InnerText;
                }
                var image = subPage.DocumentNode.SelectSingleNode(".//img[@id='picture_img']");
                var srcImage = image.GetAttributeValue("src", "NULL");
                if (srcImage.Length != 0)
                {
                    item.ImageSrc = srcImage;
                }
                item.SecondSubId = sub.Id;
                item.SoldOut = false;
                db.Items.Add(item);
                db.SaveChanges();
                Item itemAbs = new Item();
                var absrows = subPage.DocumentNode.SelectNodes(".//tr[@class='part']");
                foreach (var row in absrows)
                {
                    var absNumber = row.SelectSingleNode(".//td[@class='number']");
                    itemAbs.Number = absNumber.InnerText;
                    var absName = row.SelectSingleNode(".//td[@class='name']");
                    itemAbs.Name = absName.InnerText;
                    var absCount = row.SelectSingleNode(".//td[@class='count']");
                    itemAbs.Count = absCount.InnerText;
                    itemAbs.SecondSubId = sub.Id;
                    itemAbs.SoldOut = true;
                    if (itemAbs.Number != null && itemAbs.Name != null && itemAbs.Count != null)
                    {
                        db.Items.Add(itemAbs);
                        //db.SaveChanges();
                    }
                }
                foreach (var a in subPage.DocumentNode.SelectNodes(".//td[@colspan='4']"))
                {
                    foreach (var c in a.SelectNodes(".//div[@class='price-list']"))
                    {
                        foreach (var i in c.SelectNodes(".//div[@class='list-compact']"))
                        {
                            foreach (var k in i.SelectNodes(".//div[@class='item item-elem']"))
                            {
                                Part part = new Part();
                                part.SecondSubId = sub.Id;
                                foreach (var z in k.SelectNodes(".//div[@class='decr']"))
                                {
                                    foreach (var w in z.SelectNodes(".//strong[@class='item-name']"))
                                    {
                                        var t = w.SelectSingleNode(".//a");
                                        part.FullName = t.InnerText;
                                    }
                                }
                                foreach (var m in k.SelectNodes(".//div[@class='image']"))
                                {
                                    foreach (var x in m.SelectNodes(".//a[@href]"))
                                    {
                                        var w = x.SelectSingleNode(".//img[@class='lazy']");
                                        var srcThisImage = w.GetAttributeValue("src", "NULL");
                                        part.ImageSrc = srcThisImage;
                                    }
                                }
                                foreach (var q in k.SelectNodes(".//div[@class='right-block']"))
                                {
                                    foreach (var n in q.SelectNodes(".//div[@class='price']"))
                                    {
                                        var o = n.SelectSingleNode(".//b[@class='price-internet']");
                                        part.Price = o.InnerText;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return outString;
        }
    }
}