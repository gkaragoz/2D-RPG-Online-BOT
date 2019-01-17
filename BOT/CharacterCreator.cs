using ShiftServer.Proto.RestModels;
using System;

namespace BOT {

    public class CharacterCreator {

        public CharacterCreator() {
            Console.WriteLine("...Initializing CharacterCreator");

            Console.WriteLine("...Successfully initialized CharacterCreator");
        }

        public CharacterModel CreateCharacter(string name, int classIndex) {
            RequestCharAdd requestCreateCharacter = new RequestCharAdd();
            requestCreateCharacter.class_index = classIndex;
            requestCreateCharacter.char_name = name;
            requestCreateCharacter.session_id = NetworkManager.instance.SessionID;

            CharAdd createdCharacter = null;

            APIConfig.CreateCharacterPostMethod(requestCreateCharacter, (CharAdd charAddResponse) => {
                if (charAddResponse.success) {
                    Console.WriteLine(APIConfig.SUCCESS_TO_CREATE_CHARACTER);
                } else {
                    Console.WriteLine(APIConfig.ERROR_CREATE_CHARACTER);
                    Console.WriteLine(charAddResponse.error_message);
                }

                createdCharacter = charAddResponse;
            });

            return createdCharacter.character;
        }

    }

}
