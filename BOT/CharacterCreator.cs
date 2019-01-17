using ShiftServer.Proto.RestModels;
using System;

namespace BOT {

    public class CharacterCreator {

        public int tryCounter = 0;
        public int tryMax = 5;

        public CharacterCreator() {
            Console.WriteLine("...Initializing CharacterCreator");

            Console.WriteLine("...Successfully initialized CharacterCreator");
        }

        public CharacterModel CreateCharacter(string name, int classIndex) {
            CharacterModel createdCharacter = null;
            bool tryToCreate = true;

            do {
                if (tryMax < ++tryCounter) {
                    break;
                }

                Console.WriteLine("Trying to create a character...(" + tryCounter + ")");

                RequestCharAdd requestCreateCharacter = new RequestCharAdd();
                requestCreateCharacter.class_index = classIndex;
                requestCreateCharacter.char_name = name;
                requestCreateCharacter.session_id = NetworkManager.instance.SessionID;

                APIConfig.CreateCharacterPostMethod(requestCreateCharacter, (CharAdd charAddResponse) => {
                    if (charAddResponse.success) {
                        Console.WriteLine(APIConfig.SUCCESS_TO_CREATE_CHARACTER + "\n");

                        tryToCreate = false;

                        createdCharacter = charAddResponse.character;
                    } else {
                        Console.WriteLine(APIConfig.ERROR_CREATE_CHARACTER);
                        Console.WriteLine(charAddResponse.error_message + "\n");

                        name = DB.Names.GetRandomName();
                    }
                });
            } while (tryToCreate);

            return createdCharacter;
        }

    }

}
