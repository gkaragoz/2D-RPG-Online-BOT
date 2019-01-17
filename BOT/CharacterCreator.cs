using ShiftServer.Proto.RestModels;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace BOT {

    public class CharacterCreator {

        public int tryCounter = 0;
        public int tryMax = 5;

        public CharacterCreator() {
            Console.WriteLine("...Initializing CharacterCreator", Color.LightSkyBlue);

            Console.WriteLine("...Successfully initialized CharacterCreator", Color.LightSeaGreen);
        }

        public CharacterModel CreateCharacter(string name, int classIndex) {
            CharacterModel createdCharacter = null;
            bool tryToCreate = true;

            do {
                if (tryMax < ++tryCounter) {
                    break;
                }

                Console.WriteLine("Trying to create a character...(" + tryCounter + ")", Color.LightSkyBlue);

                RequestCharAdd requestCreateCharacter = new RequestCharAdd();
                requestCreateCharacter.class_index = classIndex;
                requestCreateCharacter.char_name = name;
                requestCreateCharacter.session_id = NetworkManager.instance.SessionID;

                APIConfig.CreateCharacterPostMethod(requestCreateCharacter, (CharAdd charAddResponse) => {
                    if (charAddResponse.success) {
                        Console.WriteLine(APIConfig.SUCCESS_TO_CREATE_CHARACTER + "\n", Color.MediumPurple);

                        tryToCreate = false;

                        createdCharacter = charAddResponse.character;
                    } else {
                        Console.WriteLine(APIConfig.ERROR_CREATE_CHARACTER, Color.OrangeRed);
                        Console.WriteLine(charAddResponse.error_message + "\n", Color.OrangeRed);

                        name = DB.Names.GetRandomName();
                    }
                });
            } while (tryToCreate);

            return createdCharacter;
        }

    }

}
