using ShiftServer.Proto.RestModels;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace BOT {

    class Program {

        static void Main(string[] args) {
            CreateSystems();

            NetworkManager.instance.LoginAsAGuest(OnLoginAsAGuest);

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
                        NetworkManager.mss.Disconnect();
                        break;
                    case "--help":
                        Console.WriteLine("\nCOMMANDS", Color.LemonChiffon);
                        Console.WriteLine("-|- Entry: {0,-20} | Command: {1,-5} |",
                            "Quit",
                            "q",
                            Color.LemonChiffon);
                        Console.WriteLine("-|- Entry: {0,-20} | Command: {1,-5} |",

                            "Account Info",
                            "acc",
                            Color.LemonChiffon);
                        Console.WriteLine("-|- Entry: {0,-20} | Command: {1,-5} |",

                            "Clear Console",
                            "cls",
                            Color.LemonChiffon);
                        break;
                    case "acc":
                        AccountManager.instance.SayInfo();
                        break;
                    case "cls":
                        Console.Clear();
                        break;
                }
            }
        }

        private static void OnLoginAsAGuest(bool success) {
            NetworkManager.instance.RequestAccountData(OnRequestAccountData);
        }

        private static void OnRequestAccountData(Account accountDataResponse) {
            if (accountDataResponse.success) {
                AccountManager.instance.Initialize(accountDataResponse);

                CharacterManager.instance.CreateCharacter(DB.Names.GetRandomName(), 0);

                AccountManager.instance.SayInfo();

                RoomManager.instance.JoinRoom("123");
            }
        }

        static void CreateSystems() {
            Console.WriteLine("Building systems...", Color.LightSkyBlue);

            NetworkManager networkManager = new NetworkManager();
            AccountManager accountManager = new AccountManager();
            CharacterManager characterManager = new CharacterManager();
            RoomManager roomManager = new RoomManager();
            DB database = new DB();

            RoomManager.instance.Initialize();

            Console.WriteLine("Building completed!\n", Color.LawnGreen);
        }
    }

}
