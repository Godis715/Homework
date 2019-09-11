using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickTackToe
{
    class GameModel
    {
        public enum GameState { aquaPlayer, yellowPlayer, winAque, winYellow, deadHeat }

        public enum CellState { nan = 0, aqua, yellow }

        public GameState CurrentGameState { get; set; }

        private CellState[][] field;

        private int turns;

        public GameModel()
        {
            field = new CellState[3][];
            for (int i = 0; i < field.Length; ++i)
            {
                field[i] = new CellState[3];
            }

            CurrentGameState = GameState.aquaPlayer;

            turns = 0;
        }

        public void CellClick(int x, int y)
        {
            if (field[x][y] != CellState.nan)
            {
                return;
            }

            if (CurrentGameState == GameState.aquaPlayer)
            {
                field[x][y] = CellState.aqua;               
                ++turns;


                CheckState(x, y, field[x][y]);
                if (CurrentGameState < GameState.winAque)
                {
                    CurrentGameState = GameState.yellowPlayer;
                }

            }
            else if (CurrentGameState == GameState.yellowPlayer)
            {
                field[x][y] = CellState.yellow;              
                ++turns;


                CheckState(x, y, field[x][y]);

                if (CurrentGameState < GameState.winAque)
                {
                    CurrentGameState = GameState.aquaPlayer;
                }
            }

        }

        private void CheckState(int x, int y, CellState currentState)
        {

            int count = 0;
            for (int i = 0; i < field.Length; ++i)
            {
                if (field[x][i] == currentState)
                {
                    ++count;
                } 
            }
            if (count == 3)
            {
                CurrentGameState += 2;
                return;
            }

            count = 0;
            for (int i = 0; i < field.Length; ++i)
            {
                if (field[i][y] == currentState)
                {
                    ++count;
                }
            }
            if (count == 3)
            {
                CurrentGameState += 2;
                return;
            }

            if ((field[0][0] == currentState &&
                field[1][1] == currentState &&
                field[2][2] == currentState) ||
                (field[0][2] == currentState &&
                field[1][1] == currentState &&
                field[2][0] == currentState))
            {
                CurrentGameState += 2;
                return;
            }
            if (turns >= 9)
            {
                CurrentGameState = GameState.deadHeat;
                return;
            }
        }

        public String GetGameState()
        {
            switch (CurrentGameState)
            {
                case GameState.aquaPlayer:
                    return "Aqua player's turn";
                case GameState.yellowPlayer:
                    return "Yellow player's turn";
                case GameState.winAque:
                    return "Aqua player won";
                case GameState.winYellow:
                    return "Yellow player won";
                case GameState.deadHeat:
                    return "Dead heat =(";
                default:
                    return "й";
            }
        }
        
        public CellState[][] GetCellStates()
        {
            return field;
        }
    }

}
