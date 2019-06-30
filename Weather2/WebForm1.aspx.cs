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
using System.Text.RegularExpressions;

namespace Weather2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GetWeatherInfo(object sender, EventArgs e)
        {
            int number;
            bool success = Int32.TryParse(txtZip.Text, out number);
            int units = Convert.ToInt16(unitslist.SelectedValue);

            if (txtZip.Text.Length != 5 || success == false)
            {
                lblSuccess.Text = String.Format("Sorry, zip code is invalid. Please enter a valid 5-digit zipcode");
                tblWeather.Visible = true;
                return;

            }
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?zip={0},us&appid=d390c62cb0afef3565153459dbe0ba3d", txtZip.Text);
            try
            {
                WebRequest req = WebRequest.Create(url);

                WebResponse res = req.GetResponse();

                lblSuccess.Text = String.Format("Here's your local weather:");
            }
            catch
            {
                lblSuccess.Text = String.Format("Sorry, zip code can't be found. Please enter a valid 5-digit zipcode");
                tblWeather.Visible = true;
                return;
            }
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                RootObject weatherinfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);


                lblSuccess.Text = String.Format("Here's your local weather:");

                lblCity.Text = string.Format("{0}", weatherinfo.name);

                lblClouds.Text = string.Format(" {0}%", weatherinfo.clouds.all);

                if (units == 1)
                {
                    lblsummary.Text = string.Format(" {0} miles", Convert.ToInt16(weatherinfo.visibility / 1000 / 1.6));
                }
                else
                {
                    lblsummary.Text = string.Format(" {0} km", Convert.ToInt16(weatherinfo.visibility / 1000));

                }






                // temp is given in kelvins
                int kTemp = Convert.ToInt16(weatherinfo.main.temp);
                int cTemp = kTemp - 273;
                if (units == 1)
                {
                    lblTemp.Text = string.Format(" {0} ° F", (cTemp * 9 / 5) + 32);

                }
                else
                {
                    lblTemp.Text = string.Format(" {0} ° C", cTemp );

                }

                // wind is given in m / s
                if (units == 1)
                {
                    lblWind.Text = string.Format(" {0} mph", Convert.ToInt16(weatherinfo.wind.speed * 3.6 / 1.6));
                }
                else
                {
                    lblWind.Text = string.Format(" {0} km/h", Convert.ToInt16(weatherinfo.wind.speed * 3.6));
                }

                double windDeg = weatherinfo.wind.deg;
                string windCord = null;
                if (windDeg > 22.5 && windDeg < 67.5)
                {
                    windCord = "northeast";
                }
                else if (windDeg > 67.5 && windDeg < 112.5)
                {
                    windCord = "east";
                }
                else if (windDeg > 112.5 && windDeg < 157.5)
                {
                    windCord = "southeast";
                }
                else if (windDeg > 157.5 && windDeg < 202.5)
                {
                    windCord = "south";
                }
                else if (windDeg > 202.5 && windDeg < 247.5)
                {
                    windCord = "southwest";
                }
                else if (windDeg > 247.5 && windDeg < 292.5)
                {
                    windCord = "west";
                }
                else if (windDeg > 292.5 && windDeg < 337.5)
                {
                    windCord = "northwest";
                }
                else
                {
                    windCord = "north";
                }




                lblDir.Text = string.Format(" {0}", windCord);
                lblHumid.Text = string.Format(" {0}%", weatherinfo.main.humidity);

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
        public double deg { get; set; }
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