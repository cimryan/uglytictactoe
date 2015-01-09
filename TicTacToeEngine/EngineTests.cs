using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TicTacToeEngine
{
    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        public void Test1()
        {
            Engine engine = new Engine();

            engine.Restart();

            Engine.GameStatus gameStatus = engine.Play(1, 1, 1, 'x');

            Assert.AreEqual(gameStatus, Engine.GameStatus.Started);
        }

        [TestMethod]
        public void Test2()
        {
            Engine engine = new Engine();

            engine.Restart();

            engine.Play(1, 1, 1, 'x');
            engine.Play(2, 1, 2, 'o');
            engine.Play(1, 2, 1, 'x');
            engine.Play(2, 2, 2, 'o');
            Engine.GameStatus gameStatus = engine.Play(1, 3, 1, 'x');

            Assert.AreEqual(gameStatus, Engine.GameStatus.Player1Wins);
        }
    }
}
