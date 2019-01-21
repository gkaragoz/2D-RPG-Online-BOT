using ShiftServer.Proto.RestModels;
using System.Drawing;
using Console = Colorful.Console;

namespace BOT {

    public class Client {

        private NetworkManager _networkManager;
        private AccountManager _accountManager;
        private CharacterManager _characterManager;
        private RoomManager _roomManager;

        public Client() {
            Console.WriteLine("...Initializing Client", Color.LightSkyBlue);

            _networkManager = new NetworkManager();
            _characterManager = new CharacterManager(_networkManager);
            _roomManager = new RoomManager(_networkManager, _characterManager);
            _accountManager = new AccountManager(_characterManager);

            _roomManager.Initialize();

            LoginAsAGuest();

            Console.WriteLine("...Successfully initialized Client", Color.LightSeaGreen);
        }

        public void SayInfo() {
            Console.WriteLine("User ID: #" + _networkManager.UserID);
            Console.WriteLine("Session ID: #" + _networkManager.SessionID);
            _accountManager.SayInfo();
        }

        public void JoinRoom(string roomID) {
            _roomManager.JoinRoom(roomID);
        }

        public void LeaveRoom() {
            _roomManager.LeaveRoom();
        }

        public void Disconnect() {
            if (_networkManager.mss.IsConnected) {
                _networkManager.mss.Disconnect();
            }
        }

        private void LoginAsAGuest() {
            _networkManager.LoginAsAGuest(OnLoginAsAGuest);
        }

        private void OnLoginAsAGuest(bool success) {
            _networkManager.RequestAccountData(OnRequestAccountData);
        }

        private void OnRequestAccountData(Account accountDataResponse) {
            if (accountDataResponse.success) {
                _accountManager.Initialize(accountDataResponse);

                _characterManager.CreateCharacter(DB.Names.GetRandomName(), 0);

                SayInfo();
            }
        }

    }

}
