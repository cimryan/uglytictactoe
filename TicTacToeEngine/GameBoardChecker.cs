using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeEngine
{
    class GameNotOverException : Exception
    {
    }

    class GameBoardChecker
    {

        static GameBoardChecker instance;

        public static GameBoardChecker Instance
        {
            get
            {
                if (GameBoardChecker.instance == null)
                {
                    GameBoardChecker.instance = new GameBoardChecker();
                }

                return instance;
            }
        }

        private GameBoardChecker()
        {
        }

        public void CheckGameBoard(Engine engine, char[,] marks, int x, int y, char mark)
        {
            engine.marks[x - 1, y - 1] = mark;

            for (int i = 1; i < 4; i++)
            {
                bool columnWin = true;
                char firstChar = 'x';

                for (int j = 1; j < 4; j++)
                {
                    if (j == 1)
                    {
                        firstChar = engine.marks[i - 1, 0];
                    }

                    if (columnWin)
                    {
                        if (engine.marks[i - 1, j - 1] != firstChar || engine.playedPositions[(i -1) + 3 * (j - 1)] == 'f')
                        {
                            columnWin = false;
                        }
                    }
                }

                if (columnWin)
                {
                    if (engine.marks[i - 1, 0] == engine.firstPlayerMark)
                    {
                        engine.winningGameStatus = Engine.GameStatus.Player1Wins;
                        return;
                    }
                    else
                    {
                        engine.winningGameStatus = Engine.GameStatus.Player2Wins;
                        return;
                    }
                }
            }

            for (int i = 1; i < 4; i++)
            {
                bool rowWin = true;
                char firstChar = 'x';

                for (int j = 1; j < 4; j++)
                {
                    if (j == 1)
                    {
                        firstChar = engine.marks[0, i - 1];
                    }

                    if (rowWin)
                    {
                        if (engine.marks[j - 1, i - 1] != firstChar || engine.playedPositions[(j - 1) + 3 * (i - 1)] != 't')
                        {
                            rowWin = false;
                        }
                    }

                }

                if (rowWin)
                {
                    if (engine.marks[0, i - 1] == engine.firstPlayerMark)
                    {
                        engine.winningGameStatus = Engine.GameStatus.Player1Wins;
                        return;
                    }
                }

                if (rowWin)
                {
                    if (engine.marks[0, i - 1] != engine.firstPlayerMark)
                    {
                        engine.winningGameStatus = Engine.GameStatus.Player2Wins;
                        return;
                    }
                }
            }

            if (engine.playedPositions[0] == 't' && engine.playedPositions[4] == 't' && engine.playedPositions[8] == 't')
            {
                if (marks[0, 0] == marks[1, 1] && marks[1, 1] == marks[2, 2])
                {
                    if (marks[0, 0] == engine.firstPlayerMark)
                    {
                        engine.winningGameStatus = Engine.GameStatus.Player1Wins;
                        return;
                    }
                    else
                    {
                        engine.winningGameStatus = Engine.GameStatus.Player2Wins;
                        return;
                    }
                }
            }

            if (marks[2, 0] == marks[1, 1] && marks[1, 1] == marks[0, 2])
            {
                if (engine.playedPositions[2] == 't' && engine.playedPositions[4] == 't' && engine.playedPositions[6] == 't')
                {
                    if (marks[1, 1] == engine.firstPlayerMark)
                    {
                        engine.winningGameStatus = Engine.GameStatus.Player1Wins;
                        return;
                    }
                    else
                    {
                        engine.winningGameStatus = Engine.GameStatus.Player2Wins;
                        return;
                    }
                }
            }

            throw new GameNotOverException();
        }
    }
}
