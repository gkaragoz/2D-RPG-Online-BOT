using ShiftServer.Proto.RestModels;
using System;

namespace BOT {

    public class AccountManager {

        public static AccountManager instance;

        private Account _account;

        public AccountManager() {
            Console.WriteLine("...Initializing AccountManager");

            if (instance == null) {
                instance = this;
            }

            Console.WriteLine("...Successfully initialized AccountManager");
        }

        public void Initialize(Account account) {
            Console.WriteLine("...Account saving");

            this._account = account;

            Console.WriteLine("...Account saved successfully");
        }

        public void SayInfo() {
            Console.WriteLine("*ACCOUNT*");
            Console.WriteLine("Gem: " + _account.gem);
            Console.WriteLine("Gold: " + _account.gold);
            Console.WriteLine("...CHARACTERS (" + _account.characters.Count + ")");

            for (int ii = 0; ii < _account.characters.Count; ii++) {
                CharacterModel character = _account.characters[ii];

                Console.WriteLine(character.name + " " + character.level + " Lv. " + character.exp);
            }

            Console.WriteLine("Selected Character Name: " + _account.selected_char_name);
        }

    }
}