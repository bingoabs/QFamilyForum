namespace QFamilyForum.Data
{
    public class CellState
    {
        public bool isClicked { get; set; } = false;
        public bool isMine { get; set; } = false;
        public int nearbyMineCount { get; set; } = 0;
    }
}
