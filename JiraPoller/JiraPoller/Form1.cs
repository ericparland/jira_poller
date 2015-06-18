using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace JiraPoller
{
    public partial class Form1 : Form
    {
        Timer t = new Timer();
        Timer loop = new Timer();
       
        // Create a request using a URL that can receive a post. 


        void post_http()
        {
            String url = System.IO.File.ReadAllText(@"url.txt");
            WebRequest request = WebRequest.Create(url);
            // Set the Method property of the request to POST.
            request.Method = "POST";
            request.Headers["Authorization"] = System.IO.File.ReadAllText(@"authorization.txt");
            // Create POST data and convert it to a byte array.
            string postData = System.IO.File.ReadAllText(@"jql.txt");
            byte[] byteArray = Encoding.UTF8.GetBytes (postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream ();
            // Write the data to the request stream.
            dataStream.Write (byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close ();
            // Get the response.
            WebResponse response = request.GetResponse ();
            // Display the status.
            Console.WriteLine (((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream ();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader (dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd ();
            reader.Close ();
            dataStream.Close ();
            response.Close ();
            //JObject jsonResponseFromServer = JObject.Parse(responseFromServer);

            RootObject parsedJsonResponseFromServer = new RootObject();
            parsedJsonResponseFromServer = JsonConvert.DeserializeObject<RootObject>(responseFromServer);
            label1.Text = parsedJsonResponseFromServer.total.ToString();
            if (parsedJsonResponseFromServer.total != 0) 
            {
                t.Start();
                label1.BackColor = System.Drawing.Color.Red;
            }
            else 
            {
                t.Stop();
                label1.BackColor = System.Drawing.Color.Green;
                label1.Visible = true;
            }            
             
             
             
        }


        public Form1()
        {
            InitializeComponent();
            label1.Text = " ";
            t.Interval = 400;
            t.Tick += new EventHandler(t_Tick);
            loop.Interval = 30000;
            loop.Tick += new EventHandler(action_post_http);
            loop.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        void t_Tick(object sender, EventArgs e)
        {
            label1.Visible = !label1.Visible;
        }

        void action_post_http(object sender, EventArgs e)
        {
            post_http();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            post_http();
        }
    }
}

public class SearchResult
{
    public int startAt { get; set; }
    public int maxResults { get; set; }
    public string total { get; set; }
    public DataSet issues { get; set; }
}

public class Customfield13850
{
    public string self { get; set; }
    public string value { get; set; }
    public string id { get; set; }
}

public class Issuetype
{
    public string self { get; set; }
    public string id { get; set; }
    public string description { get; set; }
    public string iconUrl { get; set; }
    public string name { get; set; }
    public bool subtask { get; set; }
}

public class Customfield13851
{
    public string self { get; set; }
    public string value { get; set; }
    public string id { get; set; }
}

public class AvatarUrls
{
    public string __invalid_name__16x16 { get; set; }
    public string __invalid_name__48x48 { get; set; }
}

public class Reporter
{
    public string self { get; set; }
    public string name { get; set; }
    public string emailAddress { get; set; }
    public AvatarUrls avatarUrls { get; set; }
    public string displayName { get; set; }
    public bool active { get; set; }
}

public class AvatarUrls2
{
    public string __invalid_name__16x16 { get; set; }
    public string __invalid_name__48x48 { get; set; }
}

public class Project
{
    public string self { get; set; }
    public string id { get; set; }
    public string key { get; set; }
    public string name { get; set; }
    public AvatarUrls2 avatarUrls { get; set; }
}

public class Customfield13856
{
    public string self { get; set; }
    public string value { get; set; }
    public string id { get; set; }
}

public class Votes
{
    public string self { get; set; }
    public int votes { get; set; }
    public bool hasVoted { get; set; }
}

public class Customfield13250
{
    public string self { get; set; }
    public string value { get; set; }
    public string id { get; set; }
}

public class Watches
{
    public string self { get; set; }
    public int watchCount { get; set; }
    public bool isWatching { get; set; }
}

public class Customfield10500
{
    public string self { get; set; }
    public string value { get; set; }
    public string id { get; set; }
}

public class Progress
{
    public int progress { get; set; }
    public int total { get; set; }
    public int? percent { get; set; }
}

public class Priority
{
    public string self { get; set; }
    public string iconUrl { get; set; }
    public string name { get; set; }
    public string id { get; set; }
}

public class Status
{
    public string self { get; set; }
    public string description { get; set; }
    public string iconUrl { get; set; }
    public string name { get; set; }
    public string id { get; set; }
}

public class Customfield10480
{
    public string self { get; set; }
    public string value { get; set; }
    public string id { get; set; }
}

public class Aggregateprogress
{
    public int progress { get; set; }
    public int total { get; set; }
    public int? percent { get; set; }
}

public class Resolution
{
    public string self { get; set; }
    public string id { get; set; }
    public string description { get; set; }
    public string name { get; set; }
}

public class AvatarUrls3
{
    public string __invalid_name__16x16 { get; set; }
    public string __invalid_name__48x48 { get; set; }
}

public class Assignee
{
    public string self { get; set; }
    public string name { get; set; }
    public string emailAddress { get; set; }
    public AvatarUrls3 avatarUrls { get; set; }
    public string displayName { get; set; }
    public bool active { get; set; }
}

public class Fields
{
    public string summary { get; set; }
    public string customfield_11951 { get; set; }
    public Customfield13850 customfield_13850 { get; set; }
    public Issuetype issuetype { get; set; }
    public string customfield_13852 { get; set; }
    public Customfield13851 customfield_13851 { get; set; }
    public object customfield_13854 { get; set; }
    public object customfield_13853 { get; set; }
    public object customfield_12952 { get; set; }
    public int? timespent { get; set; }
    public object customfield_10430 { get; set; }
    public Reporter reporter { get; set; }
    public string created { get; set; }
    public object customfield_13050 { get; set; }
    public object customfield_10621 { get; set; }
    public object customfield_11253 { get; set; }
    public List<string> customfield_11052 { get; set; }
    public Project project { get; set; }
    public object customfield_10420 { get; set; }
    public object customfield_10054 { get; set; }
    public object customfield_10053 { get; set; }
    public object customfield_10051 { get; set; }
    public object customfield_10630 { get; set; }
    public object lastViewed { get; set; }
    public object customfield_13855 { get; set; }
    public Customfield13856 customfield_13856 { get; set; }
    public object timeoriginalestimate { get; set; }
    public string customfield_11952 { get; set; }
    public object customfield_13667 { get; set; }
    public object customfield_13660 { get; set; }
    public object customfield_13661 { get; set; }
    public object customfield_10580 { get; set; }
    public object customfield_13662 { get; set; }
    public Votes votes { get; set; }
    public string resolutiondate { get; set; }
    public object duedate { get; set; }
    public Customfield13250 customfield_13250 { get; set; }
    public object customfield_13658 { get; set; }
    public object customfield_13657 { get; set; }
    public Watches watches { get; set; }
    public object customfield_13659 { get; set; }
    public object customfield_13655 { get; set; }
    public object customfield_13656 { get; set; }
    public object customfield_10375 { get; set; }
    public object customfield_13653 { get; set; }
    public object customfield_13654 { get; set; }
    public object customfield_13651 { get; set; }
    public object customfield_10374 { get; set; }
    public object customfield_13652 { get; set; }
    public object customfield_10370 { get; set; }
    public object customfield_10951 { get; set; }
    public object customfield_10590 { get; set; }
    public Customfield10500 customfield_10500 { get; set; }
    public object customfield_12957 { get; set; }
    public object customfield_10170 { get; set; }
    public object customfield_12955 { get; set; }
    public object customfield_12953 { get; set; }
    public int? timeestimate { get; set; }
    public Progress progress { get; set; }
    public object customfield_12851 { get; set; }
    public object customfield_11656 { get; set; }
    public object customfield_10490 { get; set; }
    public string updated { get; set; }
    public string description { get; set; }
    public Priority priority { get; set; }
    public List<object> issuelinks { get; set; }
    public object customfield_10471 { get; set; }
    public List<object> subtasks { get; set; }
    public Status status { get; set; }
    public List<object> labels { get; set; }
    public string customfield_11650 { get; set; }
    public int workratio { get; set; }
    public object customfield_11651 { get; set; }
    public Customfield10480 customfield_10480 { get; set; }
    public Aggregateprogress aggregateprogress { get; set; }
    public string customfield_11352 { get; set; }
    public object customfield_10640 { get; set; }
    public object customfield_13350 { get; set; }
    public Resolution resolution { get; set; }
    public List<object> fixVersions { get; set; }
    public object customfield_11450 { get; set; }
    public object customfield_13357 { get; set; }
    public object customfield_13356 { get; set; }
    public object aggregatetimeoriginalestimate { get; set; }
    public object customfield_10125 { get; set; }
    public object customfield_13150 { get; set; }
    public object customfield_10637 { get; set; }
    public object customfield_13552 { get; set; }
    public string customfield_10750 { get; set; }
    public string customfield_10651 { get; set; }
    public string customfield_10650 { get; set; }
    public object customfield_10652 { get; set; }
    public Assignee assignee { get; set; }
    public int? aggregatetimeestimate { get; set; }
    public object customfield_10440 { get; set; }
    public object customfield_13750 { get; set; }
    public int? aggregatetimespent { get; set; }
}

public class Issue
{
    public string expand { get; set; }
    public string id { get; set; }
    public string self { get; set; }
    public string key { get; set; }
    public Fields fields { get; set; }
}

public class RootObject
{
    public string expand { get; set; }
    public int startAt { get; set; }
    public int maxResults { get; set; }
    public int total { get; set; }
    public List<Issue> issues { get; set; }
}