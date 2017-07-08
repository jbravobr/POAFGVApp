using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace POAFGVApp
{
    public class BotConnector : IBotConnector
    {
        private Conversation _lastConversation;
        private string _directLineKey = "Rlp-XmRmpzw.cwA.D74.9OB3oLVS9inlPl9WbqYMbPVpOXdkKNUrBQiMP8086pk";
        private string conversationId;
        private string token;
        private bool _isBotConnected { get; set; }


        public BotConnector()
        {
        }

        public bool IsBotConnectorConfigured()
        {
            return _isBotConnected;
        }

        public async Task Setup()
        {
            try
            {
                if (App.AppHttpClient == null)
                {
                    App.AppHttpClient = new HttpClient()
                    {
                        BaseAddress = new Uri("https://directline.botframework.com/")
                    };
                    App.AppHttpClient.DefaultRequestHeaders.Accept.Clear();
                    App.AppHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    App.AppHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _directLineKey);
                }
                var response = await App.AppHttpClient.PostAsync("/api/tokens/conversation", null);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    token = JsonConvert.DeserializeObject<string>(result.Result);

                    App.AppHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    response = await App.AppHttpClient.PostAsync("/api/conversations", null);

                    if (response.IsSuccessStatusCode)
                    {
                        var conversationInfo = await response.Content.ReadAsStringAsync();
                        _lastConversation = new Conversation();
                        _lastConversation.ConversationId = JsonConvert.DeserializeObject<Conversation>(conversationInfo).ConversationId;

                        _isBotConnected = true;
                    }

                    _isBotConnected = true;
                }

                _isBotConnected = false;
            }
            catch (Exception ex)
            {
                _isBotConnected = false;
                throw ex;
            }
        }

        public async Task<BotMessage> SendMessage(string username, string messageText)
        {
            try
            {
                var messageToSend = new BotMessage() { From = username, Text = messageText };
                var contentPost = new StringContent(JsonConvert.SerializeObject(messageToSend), Encoding.UTF8, "application/json");
                var conversationUrl = $"https://directline.botframework.com/api/conversations/{_lastConversation.ConversationId}/messages/";

                var response = await App.AppHttpClient.PostAsync(conversationUrl, contentPost);

                var messageInfo = await response.Content.ReadAsStringAsync();

                var messagesReceived = await App.AppHttpClient.GetAsync(conversationUrl);
                var messagesReceivedData = await messagesReceived.Content.ReadAsStringAsync();
                var messagesRoot = JsonConvert.DeserializeObject<BotMessageRoot>(messagesReceivedData);
                var messages = messagesRoot.Messages;

                return messages.Last();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}