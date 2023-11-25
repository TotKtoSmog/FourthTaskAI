namespace FourthTaskAI
{
    internal class Player
    {
        internal Cell team;
        internal string name;
        private uint countWin;
        private uint countLose;
        internal virtual uint Turn()
        {
            uint Turn;
            do
            {
                Turn = Convert.ToUInt32(Console.ReadLine()) - 1;
            } while (!RulseTurn(Turn));
            return Turn;
        }
        private bool RulseTurn(uint index) => Map.getMap()[index].value == Map.getEmpty().value;
        internal Player(Cell team, string name)
        {
            this.name = name;
            this.team = team;
            countLose = 0;
            countWin = 0;
        }
        internal uint GetCountWin() => countWin;
        internal uint GetCountLose() => countLose;
        internal virtual void AddCountWin() => countWin++;
        internal virtual void AddCountLose() => countLose++;
    }
}
