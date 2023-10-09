using System;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CoinPerGemCalc
{
    public partial class Form1 : Form
    {
        public static double cookieinstasell, coinspergem, usdpergem, dollarworthofgems, coinsperdollar;
        public static double recievedcoins, cookiesinpackage, coinswithcookies;
        public static long unixtime;

        private void package_1_Click(object sender, EventArgs e)
        {
            calc(5.94, 675);
        }

        private void package_2_Click(object sender, EventArgs e)
        {
            calc(11.89, 1390);
        }

        private void package_3_Click(object sender, EventArgs e)
        {
            calc(29.74, 3600);
        }

        private void package_4_Click(object sender, EventArgs e)
        {
            calc(59.49, 7300);
        }

        private void package_5_Click(object sender, EventArgs e)
        {
            calc(118.99, 16400);
        }

        private void calc(double priceinstore, double geminpackage)
        {
            coinspergem = Math.Round(cookieinstasell / 325, 2);
            usdpergem = priceinstore / geminpackage;
            dollarworthofgems = 1 / usdpergem;
            coinsperdollar = dollarworthofgems * coinspergem;
            recievedcoins = Math.Round(coinsperdollar * priceinstore, 2);
            cookiesinpackage = geminpackage / 325;
            coinswithcookies = (int) cookiesinpackage * cookieinstasell;
            label5.Text = "Coins recieved by selling cookies recieved in this Package: " +
                          Math.Round(coinswithcookies, 2);
            label2.Text = "Coins per Gem: " + coinspergem;
            label3.Text = "Coins worth of gems in this Package: " + recievedcoins;
        }
        
        public static string apiresponse, cookieconv, unixconv;
        public static dynamic cookiesellraw, unixdyn;
        public static WebClient client = new WebClient();

        private void Form1_Load(object sender, EventArgs e)
        {
            apiresponse = client.DownloadString("https://api.hypixel.net/skyblock/bazaar");
            var apijson = JsonConvert.DeserializeObject<dynamic>(apiresponse);
            cookiesellraw = apijson.products.BOOSTER_COOKIE.quick_status.sellPrice;
            unixdyn = apijson.lastUpdated;
            unixconv = unixdyn.ToString();
            unixtime = long.Parse(unixconv);
            label4.Text = "Last API Update: " + DateTimeOffset.FromUnixTimeMilliseconds(unixtime).LocalDateTime;
            cookieconv = cookiesellraw.ToString();
            cookieinstasell = Math.Round(double.Parse(cookieconv), 2);
            label1.Text = "Booster Cookie Sell Price: " + cookieinstasell;
             
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public Form1()
        {

            InitializeComponent();
        }
    }
}
