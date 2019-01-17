using ShiftServer.Proto.RestModels;
using System;

namespace BOT {
    public class CharacterManager {

        public static CharacterManager instance;

        public Action<CharacterModel> onCharacterCreated;
        public Action<string> onCharacterSelected;

        public CharacterModel SelectedCharacter { get { return _selectedCharacter; } }

        private CharacterModel _selectedCharacter;
        private CharacterCreator _characterCreator;

        public CharacterManager() {
            Console.WriteLine("...Initializing CharacterManager");

            if (instance == null) {
                instance = this;
            }

            _characterCreator = new CharacterCreator();

            Console.WriteLine("...Successfully initialized CharacterManager");
        }

        public void CreateCharacter(string name, int classIndex) {
            CharacterModel createdCharacter = _characterCreator.CreateCharacter(name, classIndex);

            if (createdCharacter == null) {
                return;
            }

            onCharacterCreated?.Invoke(createdCharacter);

            SelectCharacter(createdCharacter);
            Console.WriteLine("Character created: " + createdCharacter.name + "(" + createdCharacter.class_index + ")");
        }

        public void SelectCharacter(CharacterModel selectedCharacter) {
            this._selectedCharacter = selectedCharacter;
            onCharacterSelected?.Invoke(selectedCharacter.name);
            Console.WriteLine("Character selected: " + _selectedCharacter.name + "(" + _selectedCharacter.class_index + ")" + _selectedCharacter.level + " Lv. Exp:" + _selectedCharacter.exp);
        }

    }
}
