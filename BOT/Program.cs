using ShiftServer.Proto.RestModels;
using System;

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
                string userInput = Console.ReadLine();
                if (String.IsNullOrEmpty(userInput)) continue;

                switch (userInput) {
                    case "q":
                        runForever = false;
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

                CreateCharacter("TestBOT2", 0);
            }
        }

        static void CreateCharacter(string name, int classIndex) {
            Console.WriteLine("...Creating character");

            CharacterManager.instance.CreateCharacter(name, classIndex);
        }

        static void CreateSystems() {
            Console.WriteLine("Building systems...");

            NetworkManager networkManager = new NetworkManager();
            AccountManager accountManager = new AccountManager();
            CharacterManager characterManager = new CharacterManager();

            Console.WriteLine("Building completed!");
        }
    }

}
