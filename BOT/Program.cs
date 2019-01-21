using ShiftServer.Proto.RestModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Console = Colorful.Console;

namespace BOT {

    class Program {

        private static Client _client;

        static void Main(string[] args) {
            CreateSystems();

            Run();
        }

        public static void Run() {
            bool runForever = true;

            while (runForever) {
                Console.WriteLine("\nCMD Commands --help", Color.LawnGreen);

                string userInput = Console.ReadLine();
                if (String.IsNullOrEmpty(userInput)) continue;

                switch (userInput) {
                    case "q":
                        runForever = false;

                        _client.Disconnect();
                        break;
                    case "--help":
                        Console.WriteLine("\nCOMMANDS", Color.LemonChiffon);
                        Console.WriteLine("-|- Entry: {0,-20} | Command: {1,-12} |",

                            "Quit",
                            "q",
                            Color.LemonChiffon);
                        Console.WriteLine("-|- Entry: {0,-20} | Command: {1,-12} |",

                            "Join Room",
                            "join-room",
                            Color.LemonChiffon);
                        Console.WriteLine("-|- Entry: {0,-20} | Command: {1,-12} |",

                            "Leave Room",
                            "leave-room",
                            Color.LemonChiffon);
                        Console.WriteLine("-|- Entry: {0,-20} | Command: {1,-12} |",

                            "Account Info",
                            "acc",
                            Color.LemonChiffon);
                        Console.WriteLine("-|- Entry: {0,-20} | Command: {1,-12} |",

                            "Clear Console",
                            "cls",
                            Color.LemonChiffon);
                        break;
                    case "join-room":
                        _client.JoinRoom("123");
                        break;
                    case "leave-room":
                        _client.LeaveRoom();
                        break;
                    case "acc":
                        _client.SayInfo();
                        break;
                    case "cls":
                        Console.Clear();
                        break;
                }

                Thread.Sleep(10);
            }
        }

        static void CreateSystems() {
            Console.WriteLine("Building systems...", Color.LightSkyBlue);

            DB database = new DB();

            _client = new Client();

            Console.WriteLine("Building completed!\n", Color.LawnGreen);
        }

    }

}
