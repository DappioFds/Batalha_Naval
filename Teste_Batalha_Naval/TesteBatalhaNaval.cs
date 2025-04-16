using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Batalha_Naval;

namespace Batalha_Naval.Tests
{
    [TestClass]
    public class BatalhaNavalTests
    {
        [TestMethod]
        public void PodePosicionarNavio_DeveRetornarTrue_QuandoEspacoEstaLivre()
        {
            // Arrange
            var jogo = new BatalhaNaval();
            var tabuleiro = new char[10, 10];

            // Preenche o tabuleiro com água
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    tabuleiro[i, j] = '~';

            // Act
            bool resultado = jogo.PodePosicionarNavio(tabuleiro, 0, 0, 3, true); // horizontal, cabe

            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void PodePosicionarNavio_DeveRetornarFalse_QuandoUltrapassaBorda()
        {
            // Arrange
            var jogo = new BatalhaNaval();
            var tabuleiro = new char[10, 10];

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    tabuleiro[i, j] = '~';

            // Act
            bool resultado = jogo.PodePosicionarNavio(tabuleiro, 0, 8, 3, true); // vai além da borda

            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void PodePosicionarNavio_DeveRetornarFalse_QuandoJaTemNavio()
        {
            // Arrange
            var jogo = new BatalhaNaval();
            var tabuleiro = new char[10, 10];

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    tabuleiro[i, j] = '~';

            // Simula um navio em (0, 1)
            tabuleiro[0, 1] = 'N';

            // Act
            bool resultado = jogo.PodePosicionarNavio(tabuleiro, 0, 0, 3, true); // tentará passar por (0,1)

            // Assert
            Assert.IsFalse(resultado);
        }
    }
}
