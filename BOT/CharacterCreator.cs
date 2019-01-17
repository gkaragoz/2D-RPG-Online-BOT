using ShiftServer.Proto.RestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT {

    public class CharacterCreator {

        public CharacterCreator() {

        }

        public void CreateCharacter() {
            RequestCharAdd requestCreateCharacter = new RequestCharAdd();
            requestCreateCharacter.class_index = 0;
            requestCreateCharacter.char_name = "TestName";
            requestCreateCharacter.session_id = NetworkManager.instance.SessionID;

            APIConfig.CreateCharacterPostMethod(requestCreateCharacter, OnCreateCharacterResponse);
        }

        private void OnCreateCharacterResponse(CharAdd data) {

        }
    }

}
