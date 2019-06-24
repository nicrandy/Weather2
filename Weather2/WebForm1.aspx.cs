using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Weather2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GetWeatherInfo(object sender, EventArgs e)
        {
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?zip={0},us&appid=d390c62cb0afef3565153459dbe0ba3d", txtZip.Text);
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                RootObject weatherinfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);
                //Temp weatherinfo = (new JavaScriptSerializer()).Deserialize<Temp>(json);
                //var weatherinfo = JsonConvert.DeserializeObject<Temp>(json);
                // need to get correct data ??? how???

                lblCity.Text = string.Format("{0}", weatherinfo.name);




                double kTemp = weatherinfo.main.temp;
                double cTemp = kTemp - 273.15;
                lblTemp.Text = string.Format("{0} degrees celcius", cTemp);
                lblWind.Text = string.Format("{0} km/h", weatherinfo.wind.speed);

                int windDeg = weatherinfo.wind.deg;
                string windCord = null;
                if (windDeg > 22.5 && windDeg < 67.5)
                {
                    windCord = "NE";
                }
                else if (windDeg > 67.5 && windDeg < 112.5)
                {
                    windCord = "E";
                }
                else if (windDeg > 112.5 && windDeg < 157.5)
                {
                    windCord = "SE";
                }
                else if (windDeg > 157.5 && windDeg < 202.5)
                {
                    windCord = "S";
                }
                else if (windDeg > 202.5 && windDeg < 247.5)
                {
                    windCord = "SW";
                }
                else if (windDeg > 247.5 && windDeg < 292.5)
                {
                    windCord = "W";
                }
                else if (windDeg > 292.5 && windDeg < 337.5)
                {
                    windCord = "NW";
                }
                else
                {
                    windCord = "N";
                }




                lblDir.Text = string.Format(" {0}", windCord);
                lblHumid.Text = string.Format("{0}%", weatherinfo.main.humidity);

                tblWeather.Visible = true;

            }

        }
    }



    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class RootObject
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
}