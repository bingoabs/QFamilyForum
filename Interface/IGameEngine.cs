using QFamilyForum.Data;

namespace QFamilyForum.Interface
{
    public interface IGameEngine
    {
        int NUMBER_OF_ALLLOWED_GUESSES{ get;set;}
        int WORDLE_LENGTH { get;set; }
        GameState NewGame();
        GameState EnterGuess(string guess);
        int GetNumberOfGuesses();
        GameGrid GetGameGrid();
    }
}
