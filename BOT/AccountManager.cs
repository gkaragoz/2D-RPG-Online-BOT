using Colorful;
using ShiftServer.Proto.RestModels;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace BOT {

    public class AccountManager {

        public static AccountManager instance;

        private Account _account;

        public AccountManager() {
            Console.WriteLine("...Initializing AccountManager", Color.LightSkyBlue);

            if (instance == null) {
                instance = this;
            }

            Console.WriteLine("...Successfully initialized AccountManager", Color.LightSeaGreen);
        }

        public void Initialize(Account account) {
            Console.WriteLine("Account saving...", Color.LightSkyBlue);

            this._account = account;

            CharacterManager.instance.onCharacterCreated = AddCharacter;
            CharacterManager.instance.onCharacterSelected = SelectCharacter;

            Console.WriteLine("Account saved successfully!\n", Color.LightSeaGreen);
        }

        public void SayInfo() {
            ColorAlternatorFactory alternatorFactory = new ColorAlternatorFactory();
            ColorAlternator alternator = alternatorFactory.GetAlternator(2, Color.Plum, Color.PaleVioletRed);

            Console.WriteLine("\n*ACCOUNT*");
            Console.WriteLine("...Gem: " + _account.gem);
            Console.WriteLine("...Gold: " + _account.gold);
            Console.WriteLine("...CHARACTERS (" + _account.characters.Count + ")");

            for (int ii = 0; ii < _account.characters.Count; ii++) {
                CharacterModel character = _account.characters[ii];

                Console.WriteLineAlternating("..." + character.name + " " + character.level + " Lv. " + character.exp, alternator);
            }

            Console.WriteLine("...Selected Character Name: " + _account.selected_char_name);
        }

        public CharacterModel GetCharacter(string name) {
            for (int ii = 0; ii < _account.characters.Count; ii++) {
                if (_account.characters[ii].name == name) {
                    return _account.characters[ii];
                }
            }
            return null;
        }

        private void AddCharacter(CharacterModel newCharacter) {
            _account.characters.Add(newCharacter);
        }

        private void SelectCharacter(string selectedCharacterName) {
            _account.selected_char_name = selectedCharacterName;
        }

    }
}