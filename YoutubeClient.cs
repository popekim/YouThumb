using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouThumb
{
    public class YoutubeClient
    {
        public bool IsConnected
        {
            get { return mAccessToken != null; }
        }

        private const string REDIRECT_URI = "urn:ietf:wg:oauth:2.0:oob";

        private string mAccessToken;
        private string mRefreshToken;
        private WebBrowser mWebBrowser;

        // NOTE: using Windows.Form.Timer to access WebBrowser control from mainthread.
        private Timer mLoginChecker = new Timer();

        public YoutubeClient(WebBrowser webBrowser)
        {
            mWebBrowser = webBrowser;
            mLoginChecker.Tick += new EventHandler(onTimedEvent);
        }

        public void Login()
        {
            const string OAUTH_URI = "https://accounts.google.com/o/oauth2/auth";
            const string SCOPE = "https://www.googleapis.com/auth/youtube";

            var clientID = UserSettings.Get(UserSettings.CLIENT_ID);
            string uri = $"{OAUTH_URI}?client_id={clientID}&redirect_uri={REDIRECT_URI}&response_type=code&scope={SCOPE}";

            mWebBrowser.Visible = true;
            mWebBrowser.Url = new Uri(uri);

            mLoginChecker.Start();
        }

        public async Task LogoutAsync()
        {
            if (IsConnected)
            {
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://accounts.google.com");

                var response = await httpClient.GetAsync($"o/oauth2/revoke?token={mAccessToken}").ConfigureAwait(false);
                Debug.Assert(response.IsSuccessStatusCode);
                mAccessToken = null;
            }
        }

        public async Task Test()
        {
            var httpClient = createWebClient();
            var testResponse = await httpClient.GetAsync("/youtube/v3/channels?part=contentDetails&mine=true");
            var testJson = await testResponse.Content.ReadAsStringAsync();

            const string uploadedPlaylist = "";
            httpClient = createWebClient();
            testResponse = await httpClient.GetAsync($"/youtube/v3/playlistItems?part=contentDetails&playlistId={uploadedPlaylist}");
            testJson = await testResponse.Content.ReadAsStringAsync();
        }

        private HttpClient createWebClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://www.googleapis.com");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {mAccessToken}");

            return httpClient;
        }

        private async void onTimedEvent(object sender, EventArgs e)
        {
            // autocode is returned through browser title
            const string AUTHCODE_PARAM = "Success code=";

            string title = mWebBrowser.DocumentTitle;
            if (title.Contains(AUTHCODE_PARAM))
            {
                mLoginChecker.Stop();

                string authCode = title.Substring(AUTHCODE_PARAM.Length);
                await requestAccessTokenAsync(authCode);
            }
        }

        // TODO: reconfirm
        // async void is okay for Event Handler.
        private async Task requestAccessTokenAsync(string authCode)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://accounts.google.com");

            var parameters = new List<KeyValuePair<string, string>>(5);
            parameters.Add(new KeyValuePair<string, string>("code", authCode));
            parameters.Add(new KeyValuePair<string, string>("client_id", UserSettings.Get(UserSettings.CLIENT_ID)));
            parameters.Add(new KeyValuePair<string, string>("client_secret", UserSettings.Get(UserSettings.CLIENT_SECRET)));
            parameters.Add(new KeyValuePair<string, string>("redirect_uri", REDIRECT_URI));
            parameters.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));

            var response = await httpClient.PostAsync("o/oauth2/token", new FormUrlEncodedContent(parameters));

            if (response.IsSuccessStatusCode)
            {
                mWebBrowser.Visible = false;

                var responseBody = await response.Content.ReadAsStringAsync();
                var googleAuthResponse = JsonConvert.DeserializeObject<GoogleAuthResponse>(responseBody);
                mAccessToken = googleAuthResponse.access_token;
                mRefreshToken = googleAuthResponse.refresh_token;

                await Test();
            }
        }
    }
}
