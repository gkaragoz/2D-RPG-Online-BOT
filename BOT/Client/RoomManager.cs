using System;
using System.Collections;
using System.Drawing;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace BOT {

    public class RoomManager {

        private NetworkManager _networkManager;
        private CharacterManager _characterManager;

        public string CurrentRoomID { get; set; }

        public RoomManager(NetworkManager networkManager, CharacterManager characterManager) {
            Console.WriteLine("...Initializing RoomManager", Color.LightSkyBlue);

            this._networkManager = networkManager;
            this._characterManager = characterManager;

            Console.WriteLine("...Successfully initialized RoomManager", Color.LightSeaGreen);
        }

        public void Initialize() {
            _networkManager.mss.AddEventListener(MSPlayerEvent.RoomUpdate, OnRoomUpdated);

            _networkManager.mss.AddEventListener(MSPlayerEvent.CreatePlayer, OnPlayerCreated);

            _networkManager.mss.AddEventListener(MSServerEvent.RoomJoin, OnRoomJoinSuccess);
            _networkManager.mss.AddEventListener(MSServerEvent.RoomJoinFailed, OnRoomJoinFailed);

            _networkManager.mss.AddEventListener(MSServerEvent.RoomCreate, OnRoomCreated);
            _networkManager.mss.AddEventListener(MSServerEvent.RoomCreateFailed, OnRoomCreateFailed);

            _networkManager.mss.AddEventListener(MSServerEvent.RoomLeave, OnRoomLeaveSuccess);
            _networkManager.mss.AddEventListener(MSServerEvent.RoomLeaveFailed, OnRoomLeaveFailed);
        }

        public void CreateRoom(int maxUserCount) {
            _networkManager.ConnectToGameplayServer();

            while (_networkManager.mss.IsConnected != true) { };

            Console.WriteLine("Trying to create a room.", Color.GreenYellow);

            ShiftServerData data = new ShiftServerData();

            RoomData roomData = new RoomData();
            roomData.Room = new MSSRoom();

            roomData.Room.Name = _characterManager.SelectedCharacter.name + "\'s Room";
            roomData.Room.IsPrivate = false;
            roomData.Room.MaxUserCount = maxUserCount;

            data.RoomData = roomData;

            _networkManager.mss.SendMessage(MSServerEvent.RoomCreate, data);
        }

        public void JoinRoom(string roomID) {
            _networkManager.ConnectToGameplayServer();

            while (_networkManager.mss.IsConnected != true) { };

            Console.WriteLine("Trying to join room(" + roomID + ")", Color.GreenYellow);

            ShiftServerData data = new ShiftServerData();

            RoomData roomData = new RoomData();
            roomData.Room = new MSSRoom();
            roomData.Room.Id = roomID;

            data.RoomData = roomData;

            _networkManager.mss.SendMessage(MSServerEvent.RoomJoin, data);
        }

        public void LeaveRoom() {
            Console.WriteLine("Trying to leave room(" + CurrentRoomID + ")", Color.GreenYellow);

            ShiftServerData data = new ShiftServerData();

            RoomData roomData = new RoomData();
            roomData.Room = new MSSRoom();
            roomData.Room.Id = CurrentRoomID;

            data.RoomData = roomData;

            _networkManager.mss.SendMessage(MSServerEvent.RoomLeave, data);
        }

        private void OnRoomUpdated(ShiftServerData data) {
        }

        private void OnPlayerCreated(ShiftServerData data) {
            Console.WriteLine("\nPlayer created!\n", data, Color.LawnGreen);
        }

        private void OnRoomJoinSuccess(ShiftServerData data) {
            Console.WriteLine("\nRoom join success!\n", data, Color.LawnGreen);

            CurrentRoomID = data.RoomData.Room.Id;
        }

        private void OnRoomJoinFailed(ShiftServerData data) {
            Console.WriteLine("\nRoom join failed!\n", data, Color.OrangeRed);

            CurrentRoomID = "";
        }

        private void OnRoomCreated(ShiftServerData data) {
            Console.WriteLine("\nRoom created!\n", data, Color.LawnGreen);

            CurrentRoomID = data.RoomData.Room.Id;
        }

        private void OnRoomCreateFailed(ShiftServerData data) {
            Console.WriteLine("\nRoom create failed!\n", data, Color.OrangeRed);

            CurrentRoomID = "";
        }

        private void OnRoomLeaveSuccess(ShiftServerData data) {
            Console.WriteLine("\nRoom leave success!\n", data, Color.LawnGreen);

            CurrentRoomID = "";
        }

        private void OnRoomLeaveFailed(ShiftServerData data) {
            Console.WriteLine("\nRoom leave failed!\n", data, Color.OrangeRed);
        }
    }

}
