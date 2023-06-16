using QFamilyForum.Data;

namespace QFamilyForum.Interface
{
    public class GameEngine : IGameEngine
    {
        private static int number_of_allowed_guesses = 6;
        private static int wordle_length = 5;
        public int NUMBER_OF_ALLLOWED_GUESSES {
            get => number_of_allowed_guesses; 
            set => number_of_allowed_guesses = value;
        }
        public int WORDLE_LENGTH {
            get => wordle_length;
            set => wordle_length = value;
        }
        private string wordle = string.Empty;
        private GameGrid gameGrid = new GameGrid();
        private GameState gameState = new GameState();
        private readonly IWordleGenerator _wordleGenerator;
        public GameEngine(IWordleGenerator wordleGenerator)
        {
            _wordleGenerator = wordleGenerator;
        }
        public GameGrid GetGameGrid() => gameGrid;
        public GameState GetGameState() => gameState;

        public void NewGame()
        {
            wordle = _wordleGenerator.GenerateSelectedWordle();
            WORDLE_LENGTH = wordle.Length;
            gameState = new GameState()
            {
                IsGameComplete = false,
                HasPlayerWonGame = false,
                NumberOfGuesses = 0,
                ResultMessage = string.Empty,
                ErrorMessages = new List<string>()
            };
            gameGrid = new GameGrid()
            {
                Guesses = new string[NUMBER_OF_ALLLOWED_GUESSES, WORDLE_LENGTH],
                GuessHintColours = new string[NUMBER_OF_ALLLOWED_GUESSES, WORDLE_LENGTH],
            };
            // should to have a better way to fill the array...
            for(int i = 0; i < NUMBER_OF_ALLLOWED_GUESSES; i++)
            {
                for(int j = 0; j < WORDLE_LENGTH; j++)
                {
                    gameGrid.GuessHintColours[i, j] = i == 0 ? GuessHintColor.ToFill : GuessHintColor.NotFill;
                }
            }
        }
        private bool IsNotNumber(string guess)
        {
            return guess.Any(c => !char.IsDigit(c));
        }

        public GameState EnterGuess(string guess)
        {
            gameState.NumberOfGuesses += 1;

            if (string.IsNullOrEmpty(guess) || guess.Length != WORDLE_LENGTH || IsNotNumber(guess))
                return GetInvalidGuessEntry(guess);

            string guessInLowerCase = guess.ToLower();

            UpdateGameGrid(gameState.NumberOfGuesses - 1, guessInLowerCase);

            if (guessInLowerCase.Equals(wordle))
                return PlayerWonGame(guessInLowerCase);
            if(gameState.NumberOfGuesses == NUMBER_OF_ALLLOWED_GUESSES)
                return GameOver(guessInLowerCase);
            // game continue
            return ContuinueGame(guessInLowerCase);
        }

        private GameState PlayerWonGame(string guess)
        {
            gameState.IsGameComplete = true;
            gameState.HasPlayerWonGame = true;
            gameState.ResultMessage = $"Congratulations you correctly guessed the selected wordle of {wordle} in {gameState.NumberOfGuesses} guess{(gameState.NumberOfGuesses > 1 ? "es" : "")}";
            return gameState;
        }

        private GameState GameOver(string guess)
        {
            gameState.IsGameComplete = true;
            gameState.HasPlayerWonGame = false;
            gameState.ResultMessage = $"Unlucky you didn't guess the selected wordle of {wordle}";
            return gameState;
        }

        private GameState ContuinueGame(string guessInLowerCase)
        {
            for (int idx = 0; idx < guessInLowerCase.Length; idx++)
            {
                gameGrid.GuessHintColours[gameState.NumberOfGuesses, idx] = GuessHintColor.ToFill;
            }
            return gameState;
        }
        private void UpdateGameGrid(int current_line_index, string guessInLowerCase)
        {
            for(int idx = 0; idx < guessInLowerCase.Length; idx++)
            {
                gameGrid.Guesses[current_line_index, idx] = guessInLowerCase[idx].ToString();
                if(guessInLowerCase[idx] == wordle[idx])
                    gameGrid.GuessHintColours[current_line_index, idx] = GuessHintColor.Correct;
                else if(guessInLowerCase[idx] < wordle[idx])
                    gameGrid.GuessHintColours[current_line_index, idx] = GuessHintColor.Lower;
                else
                    gameGrid.GuessHintColours[current_line_index, idx] = GuessHintColor.Higher;
            }
        }
        private GameState GetInvalidGuessEntry(string guess)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(guess))
            {
                errors.Add("No guess entered");
            }
            else if (guess.Length != WORDLE_LENGTH)
            {
                errors.Add($"The guess entered has not got enough letters.  Guess should have {WORDLE_LENGTH} letters");
            }

            if (IsNotNumber(guess))
            {
                errors.Add("The guess entered is invalid as it is not numbers");
            }
            gameState.ErrorMessages = errors;

            return gameState;
        }
    }
}
