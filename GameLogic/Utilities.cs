namespace HangMan.GameLogic
{
    public class Utilities
    {
        public static List<int> GetCharacterPositions(string input, char target)
        {
            List<int> positions = new List<int>();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == target)
                {
                    positions.Add(i);
                }
            }

            return positions;
        }
    }
}
