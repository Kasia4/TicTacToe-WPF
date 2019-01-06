using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TicTacToe_WPF
{
    class BoardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<FieldType> board;
        private bool playerWon;
        private bool gameStarted;
        private FieldType currentPlayer;
        private RelayCommand makeMoveCommand;
        BoardModel boardModel;

        public BoardViewModel(BoardModel model)
        {
            boardModel = model;
            board = new ObservableCollection<FieldType>(model.Board);
        }

        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<FieldType> Board
        {
            get
            {
                return board;
            }
            set
            {
                board = value;
                OnPropertyChanged();
            }
        }

        public bool PlayerWon
        {
            get { return playerWon; }
            set
            {
                playerWon = value;
                OnPropertyChanged();
            }
        }

        public bool GameStarted
        {
            get { return gameStarted; }
            set
            {
                gameStarted = value;
                OnPropertyChanged();
            }
        }

        public FieldType CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }
            set
            {
                currentPlayer = value;
                OnPropertyChanged();
            }
        }

        private void StartGame()
        {
            if (!GameStarted && CurrentPlayer != FieldType.None)
            {
                GameStarted = true;
            }
        }
        private void MakeMove(int[] coordinates)
        {
            int index = getIndex(coordinates[0], coordinates[1]);
            if (GameStarted && Board[index] == FieldType.None)
            {
                Board[index] = CurrentPlayer;
            }

        }

        private void checkGameOver(int[] coordinates)
        {
            if (isRowFilled(coordinates[0]) || isColumnFilled(coordinates[1]) || isDiagonalFilled())
            {
                //Set GameStarted to false?
                PlayerWon = true;
            }

            else if (containsEmptyField())
            {
                switchPlayers();
            }
            else
            {
                GameStarted = false;
            }
        }

        private void switchPlayers()
        {
            CurrentPlayer = CurrentPlayer == FieldType.O ? FieldType.X : FieldType.O;
        }

        private bool isRowFilled(int rowNumber)
        {
            //assume that board is 3x3
            if (Board[getIndex(rowNumber, 0)] != FieldType.None &&
                Board[getIndex(rowNumber, 0)] == Board[getIndex(rowNumber, 1)] &&
                Board[getIndex(rowNumber, 0)]  == Board[getIndex(rowNumber, 2)])
            {
                return true;
            }
            return false; 
        }
        private bool isColumnFilled(int columnNumber)
        {
            //assume that board is 3x3
            if (Board[getIndex(0, columnNumber)] != FieldType.None &&
                Board[getIndex(0, columnNumber)] == Board[getIndex(1, columnNumber)] &&
                Board[getIndex(0, columnNumber)] == Board[getIndex(2, columnNumber)])
            {
                return true;
            }
            return false;
        }

        private bool isDiagonalFilled()
        {
            //assume that board is 3x3
            if (Board[getIndex(0, 0)] != FieldType.None &&
                Board[getIndex(0, 0)] == Board[getIndex(1, 1)] &&
                Board[getIndex(0, 0)] == Board[getIndex(2, 2)])
            {
                return true;
            }

            //assume that board is 3x3
            if (Board[getIndex(0, 2)] != FieldType.None &&
                Board[getIndex(0, 2)] == Board[getIndex(1, 1)] &&
                Board[getIndex(0, 2)] == Board[getIndex(2, 0)])
            {
                return true;
            }
            return false;
        }

        private bool containsEmptyField()
        {
            foreach(FieldType field in Board)
            {
                if (field == FieldType.None)
                {
                    return true;
                }
            }
            return false;
        }

        //TODO: change the GameSize property usage and remove redunduncy of getIndex method
        private int getIndex(int row, int column)
        {
            return row * boardModel.GameSize + column;
        }

    }
}
