using ShiftServer.Proto.RestModels;
using System;

namespace BOT {
    public class CharacterManager {

        public static CharacterManager instance;

        public CharacterModel SelectedCharacter { get { return _selectedCharacter; } }

        private CharacterModel _selectedCharacter;

        public CharacterManager() {
            Console.WriteLine("...Initializing CharacterManager");

            if (instance == null) {
                instance = this;
            }

            Console.WriteLine("...Successfully initialized CharacterManager");
        }

    }
}
