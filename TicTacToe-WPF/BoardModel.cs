using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_WPF
{
    public enum FieldType
    {
        None,
        X,
        O
    };

    class BoardModel
    {
        //TODO: change to parameterized size
        private int gameSize = 3;
        public List<FieldType> PlayerSigns
        {
            get;
        }

        public int GameSize
        {
            get;
        }
        public FieldType[] Board;

        private int getIndex(int column, int row)
        {
            return row * gameSize + column;
        }

        public BoardModel()
        {
            PlayerSigns = new List<FieldType>
            {
                FieldType.None,
                FieldType.X,
                FieldType.O
            };

            for (int i = 0; i < gameSize*gameSize; ++i)
            {
                Board[i] = FieldType.None;
            }
        }
    }
}
