namespace QFamilyForum.Data
{
    public class GameState
    {
        public bool IsGameComplete { get; set; }
        public bool HasPlayerWonGame { get; set; }
        public int NumberOfGuesses { get; set; }
        public string ResultMessage { get; set; } = string.Empty;
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
