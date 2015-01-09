using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeEngine
{
    public class Engine
    {
        int lastPlayer;
        public char[,] marks;
        public string playedPositions;

        public char firstPlayerMark;

        public GameStatus winningGameStatus;

        GameBoardChecker gameBoardChecker = GameBoardChecker.Instance;

        public void Restart()
        {
            this.lastPlayer = -1;
            this.firstPlayerMark = 'z';
            this.marks = new char[3,3];
            this.playedPositions = "fffffffff";
        }

        public enum GameStatus
        {
            Invalid,
            Started,
            Player1Wins,
            Player2Wins
        }

        public GameStatus Play(int playerNumber, int x, int y, char mark)
        {
            if (playerNumber == this.lastPlayer)
            {
                return GameStatus.Invalid;
            }

            if (this.firstPlayerMark == 'z')
            {
                this.firstPlayerMark = mark;
            }

            // Remember the player number
            this.lastPlayer = playerNumber;

            // The play should be valid
            if (x <= 0 || x > 3) return GameStatus.Invalid;
            if (y < 1 || y > 3) return GameStatus.Invalid;

            if (this.playedPositions[(x - 1) + (y - 1)*3] == 't')
                return GameStatus.Invalid;
            else
            {
                var index = (x - 1) + (y - 1)*3;
                this.playedPositions = this.playedPositions.Substring(0, index) + 't' +
                    this.playedPositions.Substring(index + 1, this.playedPositions.Length - index - 1);
            }
            
            // Remember the mark          

            // To find out who has one the game we need to check all of the rows and all of the columns
            // and all of the diagonals to see if a single player has marked the cells.

            GameStatus gameStatus;

            try
            {
                this.gameBoardChecker.CheckGameBoard(this, marks, x, y, mark);

                if (GameBoardValidate(out gameStatus)) return gameStatus;

                return this.winningGameStatus;
            }
            catch (GameNotOverException)
            {
                if (GameBoardValidate(out gameStatus)) return gameStatus;

                return GameStatus.Started;
            }
        }

        bool GameBoardValidate(out GameStatus gameStatus)
        {
            gameStatus = GameStatus.Started;

            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    if (this.playedPositions[(i - 1) + 3*(j - 1)] == 't')
                    {                        
                        if (this.marks[i - 1, j - 1] != 'x' && this.marks[i - 1, j - 1] != 'o')
                        {
                            // Illegal
                            {
                                gameStatus = GameStatus.Invalid;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
