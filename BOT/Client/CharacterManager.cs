using ShiftServer.Proto.RestModels;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace BOT {
    public class CharacterManager {

        public Action<CharacterModel> onCharacterCreated;
        public Action<string> onCharacterSelected;

        public CharacterModel SelectedCharacter { get { return _selectedCharacter; } }

        private NetworkManager _networkManager;
        private CharacterModel _selectedCharacter;
        private CharacterCreator _characterCreator;

        public CharacterManager(NetworkManager networkManager) {
            Console.WriteLine("...Initializing CharacterManager", Color.LightSkyBlue);

            this._networkManager = networkManager;

            _characterCreator = new CharacterCreator(_networkManager);

            Console.WriteLine("...Successfully initialized CharacterManager", Color.LightSeaGreen);
        }

        public void CreateCharacter(string name, int classIndex) {
            CharacterModel createdCharacter = _characterCreator.CreateCharacter(name, classIndex);
            Console.WriteLine("Character created: " + createdCharacter.name + " (" + createdCharacter.class_index + ")", Color.Bisque);

            if (createdCharacter == null) {
                return;
            }

            onCharacterCreated?.Invoke(createdCharacter);

            SelectCharacter(createdCharacter);
        }

        public void SelectCharacter(CharacterModel selectedCharacter) {
            RequestCharSelect RequestSelectCharacter = new RequestCharSelect();
            RequestSelectCharacter.char_name = selectedCharacter.name;
            RequestSelectCharacter.session_id = _networkManager.SessionID;

            APIConfig.SelectCharacterPostMethod(RequestSelectCharacter, (CharSelect charSelectResponse) => {
                if (charSelectResponse.success) {
                    Console.WriteLine(APIConfig.SUCCESS_TO_SELECT_CHARACTER + "\n", Color.LightSeaGreen);
                    this._selectedCharacter = selectedCharacter;
                    onCharacterSelected?.Invoke(selectedCharacter.name);
                    Console.WriteLine("Character selected: " + _selectedCharacter.name + " (" + _selectedCharacter.class_index + ") " + _selectedCharacter.level + " Lv. Exp:" + _selectedCharacter.exp, Color.Bisque);
                } else {
                    Console.WriteLine(APIConfig.ERROR_SELECT_CHARACTER + "\n", Color.OrangeRed);
                }
            });
        }

    }
}
