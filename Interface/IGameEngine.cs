using QFamilyForum.Data;

namespace QFamilyForum.Interface
{
    public interface IGameEngine
    {
        int NUMBER_OF_ALLLOWED_GUESSES{ get;set;}
        int WORDLE_LENGTH { get;set; }
        void NewGame();
        GameState EnterGuess(string guess);
        GameGrid GetGameGrid();
        GameState GetGameState();
    }
}
