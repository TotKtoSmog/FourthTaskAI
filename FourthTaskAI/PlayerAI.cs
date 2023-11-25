using System.ComponentModel.Design.Serialization;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace FourthTaskAI
{
    internal class PlayerAI : Player
    {
        private double[] v;
        private int[] variant;
        private int[] steps_m;
        private int step;
        private int countGame = 0;
        private const double standartW = 0.3;
        private const double A = 0.3;
        private const string path = "tableV.txt";
        internal PlayerAI(Cell team, string name) : base(team, name)
        {
            this.name = name;
            this.team = team;
            v = new double[19683];
            variant = new int[9];
            steps_m = new int[9];
            step = 0;
            if (!File.Exists(path))
            {
                generateV();
                saveTable();
            }
            else
                loadeTable();
        }
        internal void setV(uint x11, uint x12, uint x13, uint x21, uint x22, uint x23, uint x31, uint x32, uint x33, float n)
        {
            uint index = ((((((((x11 * 3) + x12) * 3 + x13) * 3 + x21) * 3 + x22) * 3 + x23) * 3 + x31) * 3 + x32) * 3 + x33;
            v[index] = n;
        }
        private void generateV()
        {
            for (int i = 0; i < 19683; i++)
                v[i] = standartW;
        }
        internal void saveTable()
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                for(int i = 0; i < v.Length; i++)
                    writer.Write($"{v[i]} ");
            }
        }
        private void loadeTable()
        {
            using (StreamReader reader = new StreamReader(path, false))
            {
                v = reader.ReadLine().Split(' ').Where(n => n!="").Select(n => double.Parse(n)).ToArray();
            }
        }
        internal override uint Turn()
        {
            return stepAI();
        }
        private int getIndex(int x11, int x12, int x13, int x21, int x22, int x23, int x31, int x32, int x33)
        {
            return ((((((((x11 * 3) + x12) * 3 + x13) * 3 + x21) * 3 + x22) * 3 + x23) * 3 + x31) * 3 + x32) * 3 + x33;
        }
        private uint stepAI()
        {
            if (countGame != Map.getCountGame())
                resetValue();
            int flag;
            int c_var = 0;
            for (int j = 0; j < 9; j++)
            {
                Cell[] map = Map.getMap().Clone() as Cell[];
                if (map[j].value == Map.getEmpty().value)
                {
                    map[j] = team;
                    variant[c_var] = getIndex(map[0].value, map[1].value, map[2].value,
                        map[3].value, map[4].value, map[5].value,
                        map[6].value, map[7].value, map[8].value);
                }
                else
                    variant[c_var] = -1;
                c_var ++;
            }
            double v_max = -1;
            int v_num_max = -1;
            if (c_var > 1)
            {
                for (int i = 0; i < c_var; i++)
                {
                    if (variant[i] == -1) continue;
                    if (v[variant[i]] > v_max)
                    {
                        v_num_max = i;
                        v_max = v[variant[i]];
                    }
                }
            } 
            steps_m[step] = variant[v_num_max];
            step++;
            for (int i = 0; i < variant.Length - 1; i++)
                variant[i] = 0;
            return (uint)v_num_max;
        }
        private void resetValue()
        {
            for (int i = 0; i < steps_m.Length - 1; i++)
                steps_m[i] = 0;
            step = 0;
        }
        private void learning(int res)
        {
            for (int i = 0; i < step; i++)
                v[steps_m[i]] += A * (res - v[steps_m[i]]);
        }
        internal override void AddCountWin()
        {
            base.AddCountWin();
            learning(1);
        }
        internal override void AddCountLose()
        {
            base.AddCountLose();
            learning(0);
        }
    }
}
