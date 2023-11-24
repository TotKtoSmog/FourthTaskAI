using System.ComponentModel.Design.Serialization;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace FourthTaskAI
{
    internal class PlayerAI : Player
    {
        private float[] v;
        private int[] var;
        private const float standartW = 0.3f;
        private const string path = "tableV.txt";
        internal PlayerAI(Cell team, string name) : base(team, name)
        {
            this.name = name;
            this.team = team;
            v = new float[19683];
            var = new int[19683];
            if (!File.Exists(path))
            {
                generateV();
                saveTable();
            }
            else
            {
                loadeTable();
            }
        }
        internal void setV(uint x11, uint x12, uint x13, uint x21, uint x22, uint x23, uint x31, uint x32, uint x33, float n)
        {
            uint index = ((((((((x11 * 3) + x12) * 3 + x13) * 3 + x21) * 3 + x22) * 3 + x23) * 3 + x31) * 3 + x32) * 3 + x33;
            v[index] = n;
        }
        private void generateV()
        {
            for (int i = 0; i < 19683; i++) v[i] = standartW;
        }
        private void saveTable()
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
                v = reader.ReadLine().Split(' ').Where(n => n!="").Select(n => float.Parse(n)).ToArray();
            }
        }
        internal override uint Turn()
        {
            return stepAI();
        }
        private uint stepAI()
        {
            int flag;
            int c_var = 0;
            for(int i = 0; i< 19683; i++)
            {
                flag = 0;
                Cell[] map = Map.getMap();
                for (int j = 0; j < 9; j++)
                {
                    if (map[j] == this.team) flag += 2;
                    else flag += 1;
                    if(flag == 1)
                    {
                        var[c_var] = i;
                        c_var++;
                    }
                }
            }
            float v_max;
            int v_num_max;
            return 0;
        }
    }
}
