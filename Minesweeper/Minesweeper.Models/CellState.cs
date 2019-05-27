namespace Minesweeper.Models
{
    public enum CellState
    {
        Untouched,
        Opened,
        Neighbour,
        FlaggedAsMine,
        Mine
    }
}