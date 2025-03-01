namespace HangMan.GameLogic
{
    public static class HangmanWordDatabase
    {
        private static readonly Dictionary<string, List<string>> WordsByCategory = new()
        {
            { "Animals", new List<string> { "elephant", "giraffe", "kangaroo", "dolphin", "penguin", "tiger", "zebra", "rhinoceros", "chimpanzee", "hippopotamus", "leopard", "crocodile", "ostrich", "flamingo", "armadillo", "porcupine", "walrus", "meerkat", "platypus", "jaguar" } },
            { "Fruits", new List<string> { "apple", "banana", "cherry", "mango", "pineapple", "strawberry", "watermelon", "blueberry", "kiwi", "peach", "pomegranate", "grapefruit", "blackberry", "cantaloupe", "raspberry", "dragonfruit", "apricot", "fig", "plum", "lychee" } },
            { "Countries", new List<string> { "canada", "brazil", "germany", "france", "japan", "india", "norway", "sweden", "australia", "mexico", "argentina", "italy", "russia", "china", "spain", "switzerland", "southafrica", "thailand", "portugal", "netherlands" } },
            { "Sports", new List<string> { "soccer", "basketball", "tennis", "cricket", "baseball", "golf", "hockey", "swimming", "cycling", "badminton", "volleyball", "rugby", "tabletennis", "archery", "gymnastics", "surfing", "fencing", "karate", "taekwondo", "skiing" } },
            { "Colors", new List<string> { "red", "blue", "green", "yellow", "purple", "orange", "violet", "indigo", "magenta", "turquoise", "teal", "gold", "silver", "maroon", "cyan", "navy", "peach", "beige", "ivory", "lavender" } },
            { "Technology", new List<string> { "computer", "internet", "software", "hardware", "robotics", "cybersecurity", "database", "encryption", "artificial", "blockchain", "algorithm", "javascript", "python", "machinelearning", "nanotechnology", "semiconductor", "processor", "automation", "virtualreality", "quantumcomputing" } },
            { "Professions", new List<string> { "doctor", "engineer", "teacher", "lawyer", "scientist", "architect", "electrician", "plumber", "mechanic", "pharmacist", "astronaut", "firefighter", "journalist", "psychologist", "dentist", "carpenter", "chef", "veterinarian", "pilot", "accountant" } },
            { "Mythology", new List<string> { "zeus", "poseidon", "hades", "athena", "apollo", "artemis", "hercules", "medusa", "thor", "odin", "loki", "anubis", "horus", "ishtar", "minotaur", "pegasus", "phoenix", "kraken", "cerberus", "faun" } },
            { "Planets", new List<string> { "mercury", "venus", "earth", "mars", "jupiter", "saturn", "uranus", "neptune", "pluto", "exoplanet", "asteroid", "comet", "meteor", "galaxy", "nebula", "quasar", "supernova", "blackhole", "cosmos", "orbit" } },
            { "Vehicles", new List<string> { "car", "bicycle", "motorcycle", "airplane", "helicopter", "submarine", "spaceship", "train", "bus", "truck", "scooter", "hovercraft", "cruise", "yacht", "skateboard", "jet", "zeppelin", "tram", "rickshaw", "bulldozer" } },
            { "Clothing", new List<string> { "shirt", "pants", "jacket", "sweater", "scarf", "gloves", "socks", "shoes", "hat", "belt", "tie", "dress", "skirt", "jeans", "boots", "sneakers", "shorts", "umbrella", "raincoat", "vest" } },
            { "Elements", new List<string> { "hydrogen", "oxygen", "carbon", "gold", "silver", "iron", "helium", "neon", "platinum", "sulfur", "chlorine", "nitrogen", "aluminum", "zinc", "copper", "uranium", "mercury", "titanium", "beryllium", "lithium" } },
            { "Body Parts", new List<string> { "heart", "lungs", "kidney", "liver", "brain", "stomach", "spine", "skull", "muscle", "bone", "vein", "nerve", "elbow", "knee", "ankle", "finger", "tooth", "eyelash", "eyebrow", "wrist" } },
            { "Movies", new List<string> { "inception", "titanic", "avatar", "godfather", "matrix", "gladiator", "jaws", "rocky", "psycho", "frozen", "moana", "avengers", "interstellar", "joker", "up", "zootopia", "dune", "shrek", "coco", "eternals" } },
            { "Weapons", new List<string> { "sword", "dagger", "pistol", "rifle", "bow", "crossbow", "katana", "nunchaku", "spear", "mace", "shield", "cannon", "grenade", "bazooka", "machinegun", "flamethrower", "scimitar", "tomahawk", "boomerang", "trident" } },
            { "Festivals", new List<string> { "christmas", "halloween", "easter", "thanksgiving", "diwali", "hanukkah", "ramadan", "holi", "carnival", "oktoberfest", "newyear", "mardi gras", "valentine", "independence", "eid", "saint patrick", "bastille", "dayofthedead", "kwanzaa", "chinese new year" } }
        };

        private static readonly Random random = new();

        public static (string originalWord, string maskedWord) GetRandomWord()
        {
            var allWords = new List<string>();
            foreach (var category in WordsByCategory.Values)
            {
                allWords.AddRange(category);
            }

            string selectedWord = allWords[random.Next(allWords.Count)];
            string maskedWord = MaskWord(selectedWord);

            return (selectedWord.ToUpper(), maskedWord.ToUpper());
        }

        /// <summary>
        /// Masks random letters in a word with underscores.
        /// </summary>
        private static string MaskWord(string word)
        {
            char[] maskedArray = word.ToCharArray();
            int maskCount = Math.Max(1, word.Length / 2); // Mask at least 1 letter, up to 50% of the word

            int masked = 0;
            while (masked < maskCount)
            {
                int randomIndex = random.Next(word.Length);
                if (maskedArray[randomIndex] != '*') // Ensure it hasn't been masked yet
                {
                    maskedArray[randomIndex] = '*';
                    masked++;
                }
            }

            return new string(maskedArray);
        }


        /// <summary>
        /// Gets a random word from a specific category.
        /// </summary>
        public static string GetWordByCategory(string category)
        {
            if (WordsByCategory.ContainsKey(category))
            {
                var words = WordsByCategory[category];
                return words[random.Next(words.Count)];
            }
            throw new ArgumentException("Invalid category. Available categories: " + string.Join(", ", WordsByCategory.Keys));
        }

        /// <summary>
        /// Returns all available categories.
        /// </summary>
        public static List<string> GetCategories()
        {
            return new List<string>(WordsByCategory.Keys);
        }
    }
}
