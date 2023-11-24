namespace FourthTaskAI
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Player p1 = new Player(new Cell(1, 'X'), "ggg");
            Player p2 = new Player(new Cell(2, '0'), "fff");
            
            
            PlayerAI pAI = new PlayerAI(new Cell(2, '0'), "fff");
            Map game = new Map(p1, pAI);
            game.Start();
            Console.WriteLine("бебеб");

        }
    }
}