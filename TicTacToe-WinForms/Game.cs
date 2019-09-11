using System;

namespace TicTacToe
{
	class InvalidArgumentException : Exception
	{
		public InvalidArgumentException(string m) : base(m)
		{

		}
	}

	class Game
	{
		// O <-> false
		// X <-> true
		public char CurrentPlayer { get; private set; }

		public Game() {
			field = new char[3,3];
			for (int i = 0; i < 3; ++i) {
				for (int j = 0; j < 3; ++j) {
					field[i, j] = '-';
				}
			}
			CurrentPlayer = 'O';
		}
		public char[,] field;
		public void PlayerAction(int x, int y)
		{
			if (x > 2 || x < 0 || y > 2 || y < 0) {
				throw new InvalidArgumentException("Position out of field");
			}

			field[x, y] = CurrentPlayer;
			CurrentPlayer = (CurrentPlayer == 'O') ? 'X' : 'O';
		}

		// returns (<isFinished>, <winner>)
		public (bool, char) IsFinished()
		{
			bool isFinished = false;
			for (int i = 0; i < 3; ++i)
			{
				isFinished = (field[i, 0] == 'O' && field[i, 1] == 'O' && field[i, 2] == 'O');
				isFinished = isFinished || (field[0, i] == 'O' && field[1, i] == 'O' && field[2, i] == 'O');
				if (isFinished) {
					return (true, 'O');
				}

				isFinished = (field[i, 0] == 'X' && field[i, 1] == 'X' && field[i, 2] == 'X');
				isFinished = isFinished || (field[0, i] == 'X' && field[1, i] == 'X' && field[2, i] == 'X');
				if (isFinished)
				{
					return (true, 'X');
				}
			}
			isFinished = (field[0, 0] == 'O' && field[1, 1] == 'O' && field[2, 2] == 'O');
			isFinished = isFinished || (field[2, 0] == 'O' && field[1, 1] == 'O' && field[0, 2] == 'O');
			if (isFinished) {
				return (true, 'O');
			}
			isFinished = (field[0, 0] == 'X' && field[1, 1] == 'X' && field[2, 2] == 'X');
			isFinished = isFinished || (field[2, 0] == 'X' && field[1, 1] == 'X' && field[0, 2] == 'X');
			if (isFinished)
			{
				return (true, 'X');
			}
			return (false, '-');
		}
	}
}
