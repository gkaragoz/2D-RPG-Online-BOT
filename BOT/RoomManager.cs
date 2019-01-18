using System;
using System.Collections;
using System.Drawing;
using Console = Colorful.Console;

namespace BOT {

    public class RoomManager {

        public static RoomManager instance;

        public RoomManager() {
            Console.WriteLine("...Initializing RoomManager", Color.LightSkyBlue);

            if (instance == null) {
                instance = this;
            }

            Console.WriteLine("...Successfully initialized RoomManager", Color.LightSeaGreen);
        }

        public void Initialize() {
            NetworkManager.mss.AddEventListener(MSPlayerEvent.RoomUpdate, OnRoomUpdated);

            NetworkManager.mss.AddEventListener(MSPlayerEvent.CreatePlayer, OnPlayerCreated);

            NetworkManager.mss.AddEventListener(MSServerEvent.RoomJoin, OnRoomJoinSuccess);
            NetworkManager.mss.AddEventListener(MSServerEvent.RoomJoinFailed, OnRoomJoinFailed);

            NetworkManager.mss.AddEventListener(MSServerEvent.RoomCreate, OnRoomCreated);
            NetworkManager.mss.AddEventListener(MSServerEvent.RoomCreateFailed, OnRoomCreateFailed);

            NetworkManager.mss.AddEventListener(MSServerEvent.RoomLeave, OnRoomLeaveSuccess);
            NetworkManager.mss.AddEventListener(MSServerEvent.RoomLeaveFailed, OnRoomLeaveFailed);
        }

        public void CreateRoom(int maxUserCount) {
            NetworkManager.instance.ConnectToGameplayServer();

            while (NetworkManager.mss.IsConnected != true) { };

            Console.WriteLine("Trying to create a room.", Color.GreenYellow);

            ShiftServerData data = new ShiftServerData();

            RoomData roomData = new RoomData();
            roomData.Room = new MSSRoom();

            roomData.Room.Name = CharacterManager.instance.SelectedCharacter.name + "\'s Room";
            roomData.Room.IsPrivate = false;
            roomData.Room.MaxUserCount = maxUserCount;

            data.RoomData = roomData;

            NetworkManager.mss.SendMessage(MSServerEvent.RoomCreate, data);
        }

        public void JoinRoom(string roomID) {
            NetworkManager.instance.ConnectToGameplayServer();

            while (NetworkManager.mss.IsConnected != true) { };

            Console.WriteLine("Trying to join room(" + roomID + ")", Color.GreenYellow);

            ShiftServerData data = new ShiftServerData();

            RoomData roomData = new RoomData();
            roomData.Room = new MSSRoom();
            roomData.Room.Id = roomID;

            data.RoomData = roomData;

            NetworkManager.mss.SendMessage(MSServerEvent.RoomJoin, data);
        }

        private void OnRoomUpdated(ShiftServerData data) {
            Console.WriteLine("\nRoom update!\n", data, Color.LawnGreen);
        }

        private void OnPlayerCreated(ShiftServerData data) {
            Console.WriteLine("\nPlayer created!\n", data, Color.LawnGreen);
        }

        private void OnRoomJoinSuccess(ShiftServerData data) {
            Console.WriteLine("\nRoom join success!\n", data, Color.LawnGreen);
        }

        private void OnRoomJoinFailed(ShiftServerData data) {
            Console.WriteLine("\nRoom join failed!\n", data, Color.OrangeRed);
        }

        private void OnRoomCreated(ShiftServerData data) {
            Console.WriteLine("\nRoom created!\n", data, Color.LawnGreen);
        }

        private void OnRoomCreateFailed(ShiftServerData data) {
            Console.WriteLine("\nRoom create failed!\n", data, Color.OrangeRed);
        }

        private void OnRoomLeaveSuccess(ShiftServerData data) {
            Console.WriteLine("\nRoom leave success!\n", data, Color.LawnGreen);
        }

        private void OnRoomLeaveFailed(ShiftServerData data) {
            Console.WriteLine("\nRoom leave failed!\n", data, Color.OrangeRed);
        }
    }

}
