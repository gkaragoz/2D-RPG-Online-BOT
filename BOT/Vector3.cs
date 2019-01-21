using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT {

    public class Vector3 {
        public float posX;
        public float posY;
        public float posZ;

        public Vector3(float posX, float posY, float posZ) {
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
        }

        public static Vector3 Right { get { return new Vector3(1, 0, 0); } }
        public static Vector3 Left { get { return new Vector3(-1, 0, 0); } }
        public static Vector3 Up { get { return new Vector3(0, 1, 0); } }
        public static Vector3 Down { get { return new Vector3(0, -1, 0); } }
        public static Vector3 Forward { get { return new Vector3(0, 0, 1); } }
        public static Vector3 Backward { get { return new Vector3(0, 0, -1); } }
        public static Vector3 Zero { get { return new Vector3(0, 0, 0); } }
        public static Vector3 One { get { return new Vector3(1, 1, 1); } }

    }

}
