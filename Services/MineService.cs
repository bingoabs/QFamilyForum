using QFamilyForum.Data;

namespace QFamilyForum.Services
{
    public enum State
    {
        Success,
        Failure,
        KeepGoing
    }
    public class MineService
    {
        private int rows = 5;
        private int cols = 5;
        private int mines = 5;
        private List<List<CellState>> boardState = new List<List<CellState>>();
        private State state = State.KeepGoing;
        public List<List<CellState>> CreateGame()
        {
            List<int> mineLocations = GetNumbersFromArray(rows * cols, mines);
            boardState = new List<List<CellState>>();
            state = State.KeepGoing;
            for (int ridx = 0; ridx < rows; ridx++)
            {
                var row = new List<CellState>();
                for (int cidx = 0; cidx < cols; cidx++)
                {
                    int mineCount = CountNearbyMine(mineLocations, ridx, cidx);
                    if (mineLocations.Contains(ridx * cols + cidx))
                    {
                        row.Add(new CellState()
                        {
                            isClicked = false,
                            isMine = true,
                            nearbyMineCount = mineCount
                        });
                    }
                    else
                    {
                        row.Add(new CellState()
                        {
                            isClicked = false,
                            isMine = false,
                            nearbyMineCount = mineCount
                        });
                    }
                }
                boardState.Add(row);
            }
            return boardState;
        }
        public List<List<CellState>> GetBoardState()
        {
            return boardState;
        }
        public State UpdateClickCell(int idx)
        {
            int ridx = idx / cols;
            int cidx = idx % cols;
            boardState[ridx][cidx].isClicked = true;

            if (boardState[ridx][cidx].isMine)
            {
                state = State.Failure;
                MarkAllCellClicked();
            }
            else
            {
                if (ClickAllSafeCell())
                {
                    state = State.Success;
                    MarkAllCellClicked();
                }
                else
                    state = State.KeepGoing;
            }
            return state;
        }

        private void MarkAllCellClicked()
        {
            for (int ridx = 0; ridx < rows; ridx++)
            {
                for (int cidx = 0; cidx < cols; cidx++)
                {
                    boardState[ridx][cidx].isClicked = true;
                }
            }
        }

        // 检查是否是最后一个安全空格
        private bool ClickAllSafeCell()
        {
            for (int ridx = 0; ridx < rows; ridx++)
            {
                for (int cidx = 0; cidx < cols; cidx++)
                {
                    if (!boardState[ridx][cidx].isClicked && !boardState[ridx][cidx].isMine)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // 计算九宫格区域内的雷数
        private int CountNearbyMine(List<int> mineLocations, int ridx, int cidx)
        {
            var nearbyCells = new List<int>();
            for (int i = -1; i < 2; i++)
            {
                var curr_ridx = ridx + i;
                if (curr_ridx < 0 || curr_ridx >= rows)
                    continue;
                for (int j = -1; j < 2; j++)
                {
                    var curr_cidx = cidx + j;
                    if (curr_cidx < 0 || curr_cidx >= cols)
                        continue;
                    nearbyCells.Add(curr_ridx * cols + curr_cidx);
                }
            }
            return nearbyCells.Where(x => mineLocations.Contains(x)).Count();
        }

        private List<int> GetNumbersFromArray(int maxNumber, int requiredNumber)
        {
            var numLocation = new List<int>();
            Random random = new Random();
            for (int i = 0; i < requiredNumber; i++)
            {
                int location = random.Next(0, maxNumber);
                while (numLocation.Contains(location))
                {
                    location = random.Next(0, maxNumber);
                }
                numLocation.Add(location);
            }
            return numLocation;
        }
    }
}
