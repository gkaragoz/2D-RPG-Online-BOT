using Newtonsoft.Json;
using ShiftServer.Proto.RestModels;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Drawing;
using Console = Colorful.Console;

namespace BOT {

    public static class APIConfig {

        public enum LoginResults {
            ERROR_GET_SESSION_ID = 10010,
            ERROR_GET_GUEST_SESSION = 10011,
            ERROR_GET_ACCOUNT_DATA = 10020,

            SUCCESS_GET_SESSION_ID = 00010,
            SUCCESS_GET_GUEST_SESSION = 00011,
            SUCCESS_GET_ACCOUNT_DATA = 00020
        }

        public static string ERROR_INVALID_JSON = "04106";

        public static string URL_SessionID = "http://192.168.1.2:5000/api/auth/login";
        public static string URL_AccountData = "http://192.168.1.2:5000/api/user/account";
        public static string URL_GuestLogin = "http://192.168.1.2:5000/api/auth/guestlogin";
        public static string URL_CreateCharacter = "http://192.168.1.2:5000/api/char/add";
        public static string URL_SelectCharacter = "http://192.168.1.2:5000/api/char/select";

        public static string ATTEMP_TO_GET_GUEST_SESSION = "ATTEMP to get Guest Session!";
        public static string ATTEMP_TO_GET_SESSION_ID = "ATTEMP to get sessionID!";
        public static string ATTEMP_TO_GET_ACCOUNT_INFO = "ATTEMP to get account informations!";
        public static string ATTEMP_TO_CREATE_CHARACTER = "ATTEMP to create new character!";
        public static string ATTEMP_TO_SELECT_CHARACTER = "ATTEMP to select a character!";

        public static string ERROR_GET_GUEST_SESSION = "ERROR on getting Guest Session!";
        public static string ERROR_GET_SESSION_ID = "ERROR on getting sessionID!";
        public static string ERROR_GET_ACCOUNT_INFO = "ERROR on getting account informations!";
        public static string ERROR_CREATE_CHARACTER = "ERROR on create new character!";
        public static string ERROR_SELECT_CHARACTER = "ERROR on select a character!";

        public static string SUCCESS_GET_GUEST_SESSION = "SUCCESS on getting Guest Session!";
        public static string SUCCESS_GET_SESSION_ID = "SUCCESS on getting sessionID!";
        public static string SUCCESS_GET_ACCOUNT_INFO = "SUCCESS on getting account informations!";
        public static string SUCCESS_TO_CREATE_CHARACTER = "SUCCESS to create new character!";
        public static string SUCCESS_TO_SELECT_CHARACTER = "SUCCESS to select a character!";

        private static readonly HttpClient client = new HttpClient();

        public static void GuestLoginPostMethod(RequestGuestAuth requestData, Action<Auth> callback) {
            Console.WriteLine(ATTEMP_TO_GET_GUEST_SESSION, Color.Orange);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL_GuestLogin);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                streamWriter.Write(JsonConvert.SerializeObject(requestData));
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                var result = streamReader.ReadToEnd();

                callback(JsonConvert.DeserializeObject<Auth>(result));
            }
        }

        public static void AccountDataPostMethod(RequestAccountData requestData, Action<Account> callback) {
            Console.WriteLine(ATTEMP_TO_GET_ACCOUNT_INFO, Color.Orange);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL_AccountData);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                streamWriter.Write(JsonConvert.SerializeObject(requestData));
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                var result = streamReader.ReadToEnd();

                callback(JsonConvert.DeserializeObject<Account>(result));
            }
        }

        public static void CreateCharacterPostMethod(RequestCharAdd requestCharAdd, Action<CharAdd> callback) {
            Console.WriteLine(ATTEMP_TO_CREATE_CHARACTER, Color.Orange);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL_CreateCharacter);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                streamWriter.Write(JsonConvert.SerializeObject(requestCharAdd));
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                var result = streamReader.ReadToEnd();

                callback(JsonConvert.DeserializeObject<CharAdd>(result));
            }
        }

    }
}