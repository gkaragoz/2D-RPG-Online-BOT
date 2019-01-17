using ShiftServer.Client;
using ShiftServer.Client.Data.Entities;
using ShiftServer.Proto.RestModels;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace BOT {

    public class NetworkManager {

        public static NetworkManager instance;

        public static ManaShiftServer mss;

        public string SessionID { get; set; }
        public string UserID { get; set; }

        private string _hostName = "127.0.0.0";

        private int _port = 1337;

        private ConfigData _cfg;

        private const string CONNECT = "Trying connect to the server... ";
        private const string ON_CONNECTION_SUCCESS = "Connection success!";
        private const string ON_CONNECTION_FAILED = "Connection failed!";
        private const string ON_CONNECTION_LOST = "Connection lost!";

        public NetworkManager() {
            Console.WriteLine("...Initializing NetworkManager", Color.LightSkyBlue);

            if (instance == null) {
                instance = this;
            }

            mss = new ManaShiftServer();
            mss.AddEventListener(MSServerEvent.Connection, OnConnectionSuccess);
            mss.AddEventListener(MSServerEvent.ConnectionFailed, OnConnectionFailed);
            mss.AddEventListener(MSServerEvent.ConnectionLost, OnConnectionLost);

            Console.WriteLine("...Successfully initialized NetworkManager", Color.LightSeaGreen);
        }

        public void ConnectToGameplayServer() {
            Console.WriteLine(CONNECT);

            _cfg = new ConfigData();
            _cfg.Host = _hostName;
            _cfg.Port = _port;
            _cfg.SessionID = SessionID;

            mss.Connect(_cfg);
        }

        public void LoginAsAGuest(Action<bool> success) {
            RequestGuestAuth requestGuestAuth = new RequestGuestAuth();
            requestGuestAuth.guest_id = "";

            APIConfig.GuestLoginPostMethod(requestGuestAuth, (Auth authResponse) => {
                if (authResponse.success) {
                    Console.WriteLine(APIConfig.SUCCESS_GET_SESSION_ID + " " + authResponse.session_id + "\n", Color.MediumPurple);
                    SessionID = authResponse.session_id;
                    UserID = authResponse.user_id;
                } else {
                    Console.WriteLine(APIConfig.ERROR_GET_SESSION_ID + "\n", Color.OrangeRed);
                }

                success(authResponse.success);
            });
        }

        public void RequestAccountData(Action<Account> callback) {
            RequestAccountData requestAccountData = new RequestAccountData();
            requestAccountData.session_id = SessionID;

            APIConfig.AccountDataPostMethod(requestAccountData, (Account accountDataResponse) => {
                if (accountDataResponse.success) {
                    Console.WriteLine(APIConfig.SUCCESS_GET_ACCOUNT_INFO + "\n", Color.MediumPurple);
                } else {
                    Console.WriteLine(APIConfig.ERROR_GET_ACCOUNT_INFO + "\n", Color.OrangeRed);
                }

                callback(accountDataResponse);
            });
        }

        private void OnConnectionSuccess(ShiftServerData data) {
            Console.WriteLine(ON_CONNECTION_SUCCESS + data);
        }

        private void OnConnectionFailed(ShiftServerData data) {
            Console.WriteLine(ON_CONNECTION_FAILED + data, Color.OrangeRed);
        }

        private void OnConnectionLost(ShiftServerData data) {
            Console.WriteLine(ON_CONNECTION_LOST + data, Color.OrangeRed);
        }

    }
}