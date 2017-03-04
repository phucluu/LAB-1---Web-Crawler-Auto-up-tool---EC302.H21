using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//Web Crawler lấy thông tin BestSellingBooks từ Tiki.vn
namespace WebCrwaler
{
    class Program
    {

        static void Main(string[] args)
        {

            MatchCollection matches;

            List<string> TITLE = new List<string>();
            List<string> BRAND = new List<string>();
            List<string> PRICE = new List<string>();
            List<string> REVIEW = new List<string>();
            List<string> DESCRIPTION = new List<string>();

            Console.WriteLine("Start Crawler...");


            TinTrinhLibrary.WebClient client = new TinTrinhLibrary.WebClient();
            string html = client.Get("https://tiki.vn/bestsellers/sach-truyen-tieng-viet/c316", "https://tiki.vn/bestsellers/sach-truyen-tieng-viet/c316", "");

            //File BestSellingBook.txt lưu trữ thông tin cần lấy từ website Tiki.vn
            StreamWriter text = new StreamWriter("BestSellingBooks.txt");

            //Get Title
            var title = "data-title=\"(.*?)\"";
            Regex re_title = new Regex(title);
            matches = re_title.Matches(html);


            foreach (Match correct in matches)
            {
                TITLE.Add(correct.Groups[1].Value);
            }

            //Console.WriteLine(matches.Count);


            //Get Brand
            var brand = "data-brand=\"(.*?)\"";
            Regex re_brand = new Regex(brand);
            matches = re_brand.Matches(html);

            foreach (Match correct in matches)
            {
                BRAND.Add(correct.Groups[1].Value);
            }

            //Console.WriteLine(matches.Count);


            //Get Price
            var price = "data-price=\"(.*?)\"";
            Regex re_price = new Regex(price);
            matches = re_price.Matches(html);

            foreach (Match correct in matches)
            {
                PRICE.Add(correct.Groups[1].Value);
            }

            //Console.WriteLine(matches.Count);


            //Get Review
            var review = "<p class=\"review\">(.*?)</p>";
            Regex re_review = new Regex(review);
            matches = re_review.Matches(html);

            foreach (Match correct in matches)
            {
                REVIEW.Add(correct.Groups[1].Value);
            }

            //Console.WriteLine(matches.Count);


            // Get Description
            var description = "<div class=\"description\">[\r\n]([^><]+)<";
            Regex re_description = new Regex(description);
            matches = re_description.Matches(html);


            foreach (Match correct in matches)
            {
                DESCRIPTION.Add(correct.Groups[1].Value.Trim());
            }

            //Console.WriteLine(matches.Count);

            for (int i = 0; i < TITLE.Count; i++)
            {
                text.WriteLine(i + 1 + ") " + "Tên sách: " + TITLE[i] + " | Tác giả: " + BRAND[i] + " | Số nhận xét: " + REVIEW[i] + " | Giá: " + PRICE[i] + "đ | Giới thiệu sách: " + DESCRIPTION[i]);
                text.WriteLine();
            }

            text.Close();
        }
    }
}
