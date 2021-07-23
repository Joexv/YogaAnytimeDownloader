using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace YogaAnytimeDownloader
{
	public class Media
	{
		public string playlist { get; set; }
		public string image { get; set; }
		public string title { get; set; }
		public string description { get; set; }

		public string[] videoURLs { get; set; }
	}
	public static class HTML_Parser
    {
        static string YogaBase = @"https://www.yogaanytime.com/";

        public static string GetHTML(string URL, CookieContainer CC)
        {
			using (BetterWebClient web1 = new BetterWebClient(CC))
            {
				string data = web1.DownloadString(URL);
				return data;
			}
		}

		public static void Download(string URL, string Name)
        {
			using (WebClient web1 = new WebClient())
			{
				web1.DownloadFile(URL, Name);
			}
		}

		public static List<string> FilterVideoURLs(string html)
		{
			List<string> list = new List<string>();
			Regex regex = new Regex("(?:href)=[\"|']?(.*?)[\"|'|>]+", RegexOptions.Singleline | RegexOptions.CultureInvariant);
			if (regex.IsMatch(html))
			{
				foreach (Match match in regex.Matches(html))
				{
					if (match.Groups[1].Value.Contains(@"/class-view/"))
						list.Add(YogaBase + match.Groups[1].Value);
				}
			}
			return list;
		}

		public static List<string> GetVideoURLs(string html)
        {
			List<string> list = new List<string>();
			Regex regex = new Regex("(?:src)=[\"|']?(.*?)[\"|'|>]+", RegexOptions.Singleline | RegexOptions.CultureInvariant);
			if (regex.IsMatch(html))
			{
				foreach (Match match in regex.Matches(html))
				{
					if(match.Groups[1].Value.Contains(@"/js/display_video_jwplayer_js.cfm?"))
						list.Add(YogaBase + match.Groups[1].Value);
				}
			}
			return list;
		}

		public static Media ConvertJS2JSON(string JS, string VideoQuality = "720p")
        {
			File.WriteAllText("Test.txt", JS);
			Media data = new Media();
			string Playlist = Between(JS, "sources\": [", "],");
			data.playlist = Between(Playlist, "HLS\", \"file\": \"", "h264-") + $"h264-{VideoQuality}.m3u8?subtitles=en";
			Console.WriteLine(data.playlist);

			data.image = Between(JS, "image\": \"//", ".jpg\",") + ".jpg";
			Console.WriteLine(data.image);

			data.title = ReplaceInvalidChars(Between(JS.Replace("\"", "").Replace(",", ""), "title: ", "		description"));
			Console.WriteLine(data.title);

			data.description = Between(JS, "\"description\": \"", ".\",");
			Console.WriteLine(data.description);
			
			return data;
        }

		public static string Between(string STR, string FirstString, string LastString)
		{
			string FinalString;
			try
			{
				int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
				int Pos2 = STR.IndexOf(LastString);
				FinalString = STR.Substring(Pos1, Pos2 - Pos1);
			}
			catch { FinalString = ""; }
			return FinalString;
		}

		public static string ReplaceInvalidChars(string filename)
		{
			return string.Join("", filename.Split(Path.GetInvalidFileNameChars()));
		}

        //href="/class-view/248/video/Yoga-Slow-Wake-Up-by-Alana-Mitnick"

        //<script type = 'text/javascript' src="/js/display_video_jwplayer_js.cfm?
        //img=//images.yogaanytime.com/2014/11/03/full_alana_141018_YA10247-65299.jpg&
        //fn=2014/10_14/Classes/Alana/alana_141018_YA10247_preroll_med.mp4
        //&my_video_id=102121&my_customer_id=-1&my_key=-1&vvi=3791815&uses_chapters=N&num_chapters=1&my_res=unknown&my_class_id=175&vt=F&chapter=1&my_autoplay=false&position=0&media_div=mediaspace"></script>

        /*
		 * https://www.yogaanytime.com/js/display_video_jwplayer_js.cfm?
		 * img=//images.yogaanytime.com/2014/11/03/full_alana_141018_YA10247-65299.jpg&
		 * fn=2014/10_14/Classes/Alana/alana_141018_YA10247_preroll_med.mp4&
		 * my_video_id=102121&
		 * my_customer_id=291067&
		 * my_key=D65E068C-4FC1-4EC6-B7E3-C453AF32F83C&
		 * vvi=3791953&
		 * uses_chapters=N&num_chapters=1&my_res=unknown&my_class_id=175&vt=F&chapter=1&my_autoplay=false&position=0&media_div=mediaspace
		 * The response provides all needed information to rip media. Token, expiration etc.
		 */

        /*
         * This works for free videos only. Paid ones do not have the full JWPlayer URL in the HTML
		 * Step 1 - Extract all links from Seasons page
		 * Step 2 - Clean up links and only show the class-view links
		 * Step 3 - Extract video URL from JWPlayer Script
		 * Step 4 - Run CURL GET on provided URL
		 * Step 5 - Parse needed information from response JS
		 * 
		 * 
		 * The below would be great in theory if the Token didnt change everytime you tried to access the page
		 * <div class="tm-audio-player-thumbnail" style="background-image: url(//images.yogaanytime.com/2017/01/05/full_alana_141018_YA10241_ccupdate_1080-32554.jpg);">
		 * 
		 * https://www.yogaanytime.com/js/display_video_jwplayer_js.cfm?
		 * img=//images.yogaanytime.com/2017/01/05/full_alana_141018_YA10241_ccupdate_1080-32554.jpg
		 * &fn=2014/10_14/Classes/Alana/alana_141018_YA10241_cc-update_med.mp4&
		 * my_video_id=102667&
		 * my_customer_id=-1&
		 * my_key=D65E068C-4FC1-4EC6-B7E3-C453AF32F83C&
		 * vvi=3793637&
		 * uses_chapters=N&num_chapters=1&my_res=unknown&my_class_id=173&vt=F&chapter=1&my_autoplay=false&position=0&media_div=mediaspace
		 * Generate custom URL using the background-image in the website HTML
		 * 
		 * from the image URL get URL between yogaanytime.com and _ccupdate
		 * 2017/01/05/full_alana_141018_YA10241
		 * Remove full_ and remove everything between alana and the 10241
		 * 2017/01/05/alana_10241
		 * inject this into the free url that we pulled.
		 * 
		 * "type": "hls", "label": "Auto-HLS", "file": 
		 * "https://media-fastly.yogaanytime.com/
		 * 1627048099_d1c56cf894fa0f70723e4f45dbf7c7c7eb168c18
		 * /2017/01/05/alana_10241/video/h264-,504p,720p,360p,216p.m3u8?subtitles=en"
		 * 
		 * Pull videos as before
		 */
    }

    /// <summary>
    /// A (slightly) better version of .Net's default <see cref="WebClient"/>.
    /// The extra features include:
    /// ability to disable automatic redirect handling,
    /// sessions through a cookie container,
    /// indicate to the webserver that GZip compression can be used,
    /// exposure of the HTTP status code of the last request,
    /// exposure of any response header of the last request,
    /// ability to modify the request before it is send.
    /// </summary>
    /// <seealso cref="System.Net.WebClient" />
    public class BetterWebClient : WebClient
    {
        private WebRequest _request = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BetterWebClient" /> class.
        /// </summary>
        /// <param name="cookies">The cookies. If set to <c>null</c> a container will be created.</param>
        /// <param name="autoRedirect">if set to <c>true</c> the client should handle the redirect automatically. Default value is <c>true</c></param>
        public BetterWebClient(CookieContainer cookies = null, bool autoRedirect = true)
        {
            CookieContainer = cookies ?? new CookieContainer();
            AutoRedirect = autoRedirect;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to automatically redirect when a 301 or 302 is returned by the request.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the client should handle the redirect automatically; otherwise, <c>false</c>.
        /// </value>
        public bool AutoRedirect { get; set; }

        /// <summary>
        /// Gets or sets the cookie container. This contains all the cookies for all the requests.
        /// </summary>
        /// <value>
        /// The cookie container.
        /// </value>
        public CookieContainer CookieContainer { get; set; }

        /// <summary>
        /// Gets the cookies header (Set-Cookie) of the last request.
        /// </summary>
        /// <value>
        /// The cookies or <c>null</c>.
        /// </value>
        public string Cookies
        {
            get { return GetHeaderValue("Set-Cookie"); }
        }

        /// <summary>
        /// Gets the location header for the last request.
        /// </summary>
        /// <value>
        /// The location or <c>null</c>.
        /// </value>
        public string Location
        {
            get { return GetHeaderValue("Location"); }
        }

        /// <summary>
        /// Gets the status code. When no request is present, <see cref="HttpStatusCode.Gone"/> will be returned.
        /// </summary>
        /// <value>
        /// The status code or <see cref="HttpStatusCode.Gone"/>.
        /// </value>
        public HttpStatusCode StatusCode
        {
            get
            {
                var result = HttpStatusCode.Gone;

                if (_request != null)
                {
                    var response = base.GetWebResponse(_request) as HttpWebResponse;

                    if (response != null)
                    {
                        result = response.StatusCode;
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Gets or sets the setup that is called before the request is done.
        /// </summary>
        /// <value>
        /// The setup.
        /// </value>
        public Action<HttpWebRequest> Setup { get; set; }

        /// <summary>
        /// Gets the header value.
        /// </summary>
        /// <param name="headerName">Name of the header.</param>
        /// <returns>The value.</returns>
        public string GetHeaderValue(string headerName)
        {
            if (_request != null)
            {
                return base.GetWebResponse(_request)?.Headers?[headerName];
            }

            return null;
        }

        /// <summary>
        /// Returns a <see cref="T:System.Net.WebRequest" /> object for the specified resource.
        /// </summary>
        /// <param name="address">A <see cref="T:System.Uri" /> that identifies the resource to request.</param>
        /// <returns>
        /// A new <see cref="T:System.Net.WebRequest" /> object for the specified resource.
        /// </returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            _request = base.GetWebRequest(address);

            var httpRequest = _request as HttpWebRequest;

            if (_request != null)
            {
                httpRequest.AllowAutoRedirect = AutoRedirect;
                httpRequest.CookieContainer = CookieContainer;
                httpRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                Setup?.Invoke(httpRequest);
            }

            return _request;
        }
    }
}
