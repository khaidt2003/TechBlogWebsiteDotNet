using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TechBlogWebsite.Help
{
    public static class Functions
    {
        public static string ConvertToUnSign(string text)
        {
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            text = text.Replace(" ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        // As the text the: "<span class='glyphicon glyphicon-plus'></span>" can be entered
        public static MvcHtmlString NoEncodeActionLink(this HtmlHelper htmlHelper,
                                             string text, string title, string action,
                                             string controller,
                                             object routeValues = null,
                                             object htmlAttributes = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = text;
            builder.Attributes["title"] = title;
            builder.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));

            return MvcHtmlString.Create(builder.ToString());
        }
        public static string GetTimeAgo(this DateTime dateTime)
        {
                var timeSpan = DateTime.Now - dateTime;

                if (timeSpan <= TimeSpan.FromSeconds(60))
                    return $"{timeSpan.Seconds} seconds ago";
                else if (timeSpan <= TimeSpan.FromMinutes(60))
                    return timeSpan.Minutes > 1 ? $"{timeSpan.Minutes} minutes ago" : "a minute ago";
                else if (timeSpan <= TimeSpan.FromHours(24))
                    return timeSpan.Hours > 1 ? $"{timeSpan.Hours} hours ago" : "an hour ago";
                else if (timeSpan <= TimeSpan.FromDays(30))
                    return timeSpan.Days > 1 ? $"{timeSpan.Days} days ago" : "yesterday";
                else if (timeSpan <= TimeSpan.FromDays(365))
                    return timeSpan.Days > 30 ? $"{timeSpan.Days / 30} months ago" : "a month ago";
                else
                    return timeSpan.Days > 365 ? $"{timeSpan.Days / 365} years ago" : "a year ago";
         }
        public static string ToVietnameseDateString(DateTime date)
        {
            var dayOfWeek = new CultureInfo("vi-VN").DateTimeFormat.GetDayName(date.DayOfWeek);
            return $"Thứ {dayOfWeek} Ngày {date.Day} Tháng {date.Month} Năm {date.Year} Lúc {date.ToString("hh:mm:ss tt")}";
        }
        public static string ReplaceHtmlTagsAndTruncate(string input, int wordLimit)
        {
            if (string.IsNullOrEmpty(input)) return input;

            // Replace all HTML tags with an empty string and remove line breaks
            string replaced = Regex.Replace(input, @"<[^>]+>", "").Replace("\r", "").Replace("\n", "");

            // Replace spaces with &nbsp; to prevent line breaks
            replaced = replaced.Replace(" ", "&nbsp;");

            // Truncate the text to the specified word limit
            var words = replaced.Split(new[] { "&nbsp;" }, StringSplitOptions.None);
            if (words.Length <= wordLimit) return replaced;

            var truncatedWords = words.Take(wordLimit);
            return string.Join("&nbsp;", truncatedWords) + "...";
        }


    }
}