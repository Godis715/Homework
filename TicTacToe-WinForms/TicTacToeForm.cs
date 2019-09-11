using System;
using System.Windows.Forms;

namespace TicTacToe
{
	public partial class TicTacToeForm : Form
	{
		public TicTacToeForm()
		{
			InitializeComponent();
			game = new Game();
		}
		Game game;
		private void Cell_Click(object sender, EventArgs e)
		{
			var state = game.IsFinished();
			if (state.Item1) {
				return;
			}

			Button button = (Button)sender;
			int butNum = Convert.ToInt32(button.Tag);
			if (button.Text != "") {
				return;
			}
			button.Text = game.CurrentPlayer.ToString();
			game.PlayerAction(butNum / 3, butNum % 3);
			state = game.IsFinished();
			if (state.Item1)
			{
				gameState.Text = state.Item2.ToString() + " won";
			}
			else
			{
				gameState.Text = "player " + game.CurrentPlayer.ToString();
			}
		}
	}
}
