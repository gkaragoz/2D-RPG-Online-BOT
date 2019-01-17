using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using Console = Colorful.Console;

namespace BOT {

    public class DB {

        public const string NAMES_FILE = "names";
        public const string NAMES_FILE_TYPE = ".json";

        public static Names Names { get; private set; }

        public DB() {
            Console.WriteLine("...Initializing Database", Color.LightSkyBlue);

            Names = new Names();

            LoadNames();

            Console.WriteLine("...Successfully initialized Database", Color.LightSeaGreen);
        }

        private void LoadNames() {
            Console.WriteLine("...Loading name database", Color.LightSkyBlue);

            if (File.Exists("names.json")) {
                using (StreamReader r = new StreamReader(NAMES_FILE + NAMES_FILE_TYPE)) {
                    string json = r.ReadToEnd();

                    Names = JsonConvert.DeserializeObject<Names>(json);

                    Console.WriteLine("...(" + Names.data.Count + ") Names found!", Color.LightSeaGreen);
                }
            } else {
                Console.WriteLine("DB ERROR: File not found! " + NAMES_FILE + NAMES_FILE, Color.OrangeRed);
            }
        }

    }

    public class Names {
        public List<string> data;

        public string GetRandomName() {
            if (data != null) {
                Random rnd = new Random();
                int index = rnd.Next(data.Count);
                return data[index];
            } else {
                return string.Empty;
            }
        }
    }

}
