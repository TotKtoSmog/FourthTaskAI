namespace FourthTaskAI
{
    internal class Player
    {
        internal Cell team;
        internal string name;
        private uint countWin;
        private uint countLose;
        internal Player(Cell team, string name)
        {
            this.name = name;
            this.team = team;
            countLose = 0;
            countWin = 0;
        }
        internal uint GetCountWin() => countWin;
        internal uint GetCountLose() => countLose;
        internal void AddCountWin() => countWin++;
        internal void AddCountLose() => countLose++;
    }
}
