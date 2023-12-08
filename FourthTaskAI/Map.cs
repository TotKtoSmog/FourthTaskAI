namespace FourthTaskAI
{
    internal class Map
    {
        private Player player1;
        private Player player2;
        static private Cell Empty;
        static private Cell[] cell;
        static private int countGames;
        private ushort status;
        private ushort countStep;
        static internal int getCountGame() => countGames;
        static internal Cell[] getMap() => cell;
        static internal Cell getEmpty() => Empty;
        internal Map(Player player1, Player player2) 
        { 
            cell = new Cell[9];
            countGames = 0;
            status = 1;
            countStep = 0;
            this.player1 = player1;
            this.player2 = player2;
            Empty = new Cell(0, '#');
        }
        internal void Start()
        {
            if(countGames <= 10)
            {
                FillingMap();
                while (countStep <= 9 && status == 1)
                {
                    Update();
                }
                Console.WriteLine("Игра окончена");
                Thread.Sleep(2000);
                Restart();
            }
        }
        internal void FillingMap()
        {
            int i = -1;
            while(i++ < cell.Length - 1)
                cell[i] = Empty;
        }
        private Player TurnPlayer()
            => countStep % 2 == 0? player1 : player2;
        internal void Show(Player currentPlayer)
        {
            Console.Clear();
            Console.WriteLine($"Сыграно игр : {countGames} | игрок {player1.name} = {player1.GetCountWin()} | игрок  {player2.name} = {player2.GetCountWin()}");
            Console.WriteLine($"Ход игрока: {currentPlayer.name} игра за {currentPlayer.team.texture}");
            ShowMap();
        }
        private void ShowMap()
        {
            for (int i = 0; i < cell.Length; i++)
            {
                if (i % 3 == 0)
                    Console.WriteLine();
                Console.Write($"{cell[i].texture} ");
            }
            Console.WriteLine();
        }
        internal void Update()
        {
            Player currentPlayer = TurnPlayer();
            Show(currentPlayer);
            uint index = currentPlayer.Turn();
            SetValueMap(index, currentPlayer);
            countStep++;
            if (countStep >= 5)
                CheckedWin(currentPlayer);
            
        }
        internal static bool CheckedWinPlayer(Player currentPlayer, Cell[] cells) 
        {
            int currentPV = currentPlayer.team.value;
            if (cells[0].value == currentPV && cells[1].value == currentPV && cells[2].value == currentPV ||
                cells[3].value == currentPV && cells[4].value == currentPV && cells[5].value == currentPV ||
                cells[6].value == currentPV && cells[7].value == currentPV && cells[8].value == currentPV ||
                cells[0].value == currentPV && cells[4].value == currentPV && cells[8].value == currentPV ||
                cells[2].value == currentPV && cells[4].value == currentPV && cells[6].value == currentPV ||
                cells[0].value == currentPV && cells[3].value == currentPV && cells[6].value == currentPV ||
                cells[1].value == currentPV && cells[4].value == currentPV && cells[7].value == currentPV ||
                cells[2].value == currentPV && cells[5].value == currentPV && cells[8].value == currentPV)
            {
                return true;
            }
            else return false;
        }
        private void CheckedWin(Player currentPlayer)
        {
            if (CheckedWinPlayer(currentPlayer, cell))
                EndGame(currentPlayer);
            if(countStep == 9)
                EndGame(null);
        }
        private void ShowWinner(Player currentPlayer)
        {
            Show(currentPlayer);
            Console.WriteLine($"Поедил {currentPlayer.team.texture} поздравим {currentPlayer.name}");
        }
        private void ShowDraw()
        {
            Console.WriteLine("Победила дружба");
        }
        private void EndGame(Player currentPlayer)
        {
            if(currentPlayer == null)
            {
                ShowDraw();
            }
            else
            {
                currentPlayer.AddCountWin();
                if (currentPlayer.team == player1.team) player2.AddCountLose();               
                else player1.AddCountLose();
                ShowWinner(currentPlayer);
            }
            countGames++;
            status = 0;
        }
        private void SetValueMap(uint index, Player currentPlayer)
            => cell[index] = currentPlayer.team;
        private void Restart()
        {
            countStep = 0;
            status = 1;
            Start();
        }
    }
}
