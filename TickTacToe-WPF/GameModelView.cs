using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace TickTackToe
{

    public class TurnCommand : ICommand
    {
        private GameModelView modelView;

        public TurnCommand(GameModelView _modelView)
        {
            modelView = _modelView;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            if (((String)parameter).Equals("Restart"))
            {
                modelView.Restart();
                return;
            }

            String[] coord = ((String)parameter).Split(new char[] { ' ' });

            int x = Convert.ToInt32(coord[0]);
            int y = Convert.ToInt32(coord[1]);

            modelView.Turn(x, y);
        }
    }

    public class GameModelView : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private GameModel model;

        public String GameState { get; set; }

        public SolidColorBrush[][] SolidColor { get; set; }

        public TurnCommand TurnCmd { get; set; }

        public GameModelView()
        {
            model = new GameModel();

            TurnCmd = new TurnCommand(this);

            SolidColor = new SolidColorBrush[3][];
            for (int i = 0; i < SolidColor.Length; ++i)
            {
                SolidColor[i] = new SolidColorBrush[3];
                for (int j = 0; j < SolidColor[i].Length; ++j) {
                    SolidColor[i][j] = new SolidColorBrush();
                }
            }

            Refresh();
        }

        private void Refresh()
        {
            var field = model.GetCellStates();

            for (int i = 0; i < field.Length; i++)
            {
                for (int j = 0; j < field[i].Length; j++)
                {
                    switch (field[i][j]) {
                        case GameModel.CellState.aqua:
                            SolidColor[i][j].Color = Colors.Aqua;
                            break;
                        case GameModel.CellState.yellow:
                            SolidColor[i][j].Color = Colors.Yellow;
                            break;
                        case GameModel.CellState.nan:
                            SolidColor[i][j].Color = Colors.White;
                            break;

                    }
                }
            }

            GameState = model.GetGameState();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GameState)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SolidColor)));

        }

        public void Turn(int x, int y)
        {
            model.CellClick(x, y);
            Refresh();



        }

        public void Restart()
        {
            model = new GameModel();
            
            Refresh();

        }

    }
}
