namespace FourthTaskAI
{
    internal class Cell
    {
        internal int value { get; set; }
        internal char texture { get; set; }
        internal Cell() { }
        internal Cell(int value, char texture)
        {
            this.value = value;
            this.texture = texture;
        }
    }
}
