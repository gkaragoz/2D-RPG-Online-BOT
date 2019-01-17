using ShiftServer.Proto.RestModels;
using System;

namespace BOT {

    class Program {
        static void Main(string[] args) {
            CreateSystems();

            NetworkManager.instance.LoginAsAGuest(OnLoginAsAGuest);

            Console.ReadLine();
        }

        private static void OnLoginAsAGuest(bool success) {
            NetworkManager.instance.RequestAccountData(OnRequestAccountData);
        }

        private static void OnRequestAccountData(Account accountDataResponse) {
            if (accountDataResponse.success) {
                AccountManager.instance.Initialize(accountDataResponse);

                CreateCharacter("TestBOT2", 0);

                AccountManager.instance.SayInfo();
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
