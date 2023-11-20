namespace FourthTaskAI
{
    internal class Map
    {
        private Player player1;
        private Player player2;
        private Cell Empty;
        private Cell[] cell;
        private uint countGames;
        private ushort status;
        private ushort countStep;
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
            FillingMap();
            while (countStep <= 9 && status == 1)
            {
                Update();
            }
            Thread.Sleep(200);
            Restart();
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
        }
        internal void Update()
        {
            Player currentPlayer = TurnPlayer();
            Show(currentPlayer);
            uint index = GetTurn();
            SetValueMap(index, currentPlayer);
            countStep++;
            if (countStep >= 5)
                CheckedWin(currentPlayer);
            
        }
        private uint GetTurn()
        {
            uint Turn;
            do
            {
               Turn = Convert.ToUInt32(Console.ReadLine()) - 1;
            } while (!RulseTurn(Turn));
            return Turn;
        }
        private void CheckedWin(Player currentPlayer)
        {
            int currentPV = currentPlayer.team.value;
            if (cell[0].value == currentPV && cell[1].value == currentPV && cell[2].value == currentPV ||
                cell[3].value == currentPV && cell[4].value == currentPV && cell[5].value == currentPV ||
                cell[6].value == currentPV && cell[7].value == currentPV && cell[8].value == currentPV ||
                cell[0].value == currentPV && cell[4].value == currentPV && cell[8].value == currentPV ||
                cell[2].value == currentPV && cell[4].value == currentPV && cell[6].value == currentPV ||
                cell[0].value == currentPV && cell[3].value == currentPV && cell[6].value == currentPV ||
                cell[1].value == currentPV && cell[4].value == currentPV && cell[7].value == currentPV ||
                cell[2].value == currentPV && cell[5].value == currentPV && cell[8].value == currentPV )
            {
                EndGame(currentPlayer);
            }
            if(countStep == 9)
                EndGame(null);
        }
        private void ShowWinner(Player currentPlayer)
        {
            Console.WriteLine($"Победил игрок: {currentPlayer.name}, играющий: {currentPlayer.team.texture}");
            Console.WriteLine($"Итоговый счет: {player1.name} = {player1.GetCountWin() } | {player2.name} = {player2.GetCountWin()}");
            ShowMap();
        }
        private void ShowDraw()
        {
            Console.WriteLine("Победила дружба");
            ShowMap();
        }
        private void EndGame(Player currentPlayer)
        {
            if(currentPlayer == null)
            {
                ShowDraw();
            }
            else
            {
                if (currentPlayer == player1)
                {
                    player1.AddCountWin();
                    player2.AddCountLose();
                    ShowWinner(player1);
                }
                else
                {
                    player2.AddCountWin();
                    player1.AddCountLose();
                    ShowWinner(player2);
                }
            }
            countGames++;
            status = 0;
        }
        private bool RulseTurn(uint index)
            => cell[index].value == Empty.value;
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
