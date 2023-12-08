using System.Formats.Asn1;

namespace FourthTaskAI
{
    internal class PlayerBot : Player
    {
        internal PlayerBot(Cell team, string name) : base(team, name)
        {
            this.team = team;
            this.name = name;
        }
        private int GetWinPosition(List<int> position)
        {
            for (int i = 0; i < position.Count; i++)
            {
                Cell[] temp_map = (Cell[])Map.getMap().Clone();
                temp_map[position[i]] = team;
                if (Map.CheckedWinPlayer(this, temp_map))
                    return i;
            }
            return -1;
        }
        private uint Step()
        {
            List<int> position = new List<int>();
            Cell[] temp_map = Map.getMap();
            for (int i = 0; i < temp_map.Length; i++)
                if (temp_map[i] == Map.getEmpty())
                    position.Add(i);
            int index = -1;
            if(temp_map.Count(n => n == team) > 1)
                index = GetWinPosition(position);
            if(index == -1)
            {
                Random random = new Random();
                index = random.Next(position.Count);
            }
            return (uint)position[index];
        }
        internal override uint Turn()
        {
            return Step();
        }
    }
}
