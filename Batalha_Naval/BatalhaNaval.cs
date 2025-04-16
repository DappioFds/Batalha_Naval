using System;

namespace Batalha_Naval
{
   public class BatalhaNaval
    {
        private const int Tamanho = 10;
        private char[,] tabuleiroJogador = new char[Tamanho, Tamanho];
        private char[,] tabuleiroComputador = new char[Tamanho, Tamanho];
        private Random random = new Random();
        private int tentativas = 0;
        private bool[,] jogadasRealizadas = new bool[Tamanho, Tamanho];


        // Construtor
        public BatalhaNaval()
        {
            InicializarTabuleiro(tabuleiroJogador);
            InicializarTabuleiro(tabuleiroComputador);
            PosicionarNavios(tabuleiroComputador);
        }

        // Método para inicializar o tabuleiro
        private void InicializarTabuleiro(char[,] tabuleiro)
        {
            for (int i = 0; i < Tamanho; i++)
            {
                for (int j = 0; j < Tamanho; j++)
                {
                    tabuleiro[i, j] = '~';
                }
            }
        }

        // Método para posicionar os navios
        private void PosicionarNavios(char[,] tabuleiro)
        {
            PosicionarNavio(tabuleiro, 2); // Navios de 2 quadrados
            PosicionarNavio(tabuleiro, 2); // Navios de 2 quadrados
            PosicionarNavio(tabuleiro, 2); // Navios de 2 quadrados
            PosicionarNavio(tabuleiro, 2); // Navios de 2 quadrados
            PosicionarNavio(tabuleiro, 3); // Navios de 3 quadrados
            PosicionarNavio(tabuleiro, 3); // Navios de 3 quadrados
            PosicionarNavio(tabuleiro, 3); // Navios de 3 quadrados
            PosicionarNavio(tabuleiro, 4); // Navios de 4 quadrados
            PosicionarNavio(tabuleiro, 4); // Navios de 4 quadrados
        }

        // Método auxiliar para posicionar um navio de tamanho específico
        private void PosicionarNavio(char[,] tabuleiro, int tamanho)
        {
            bool posicionado = false;
            while (!posicionado)
            {
                int x = random.Next(Tamanho);
                int y = random.Next(Tamanho);
                bool horizontal = random.Next(2) == 0; // Decide se o navio será horizontal ou vertical

                if (PodePosicionarNavio(tabuleiro, x, y, tamanho, horizontal))
                {
                    // Coloca o navio no tabuleiro
                    for (int i = 0; i < tamanho; i++)
                    {
                        if (horizontal)
                        {
                            tabuleiro[x, y + i] = 'N';
                        }
                        else
                        {
                            tabuleiro[x + i, y] = 'N';
                        }
                    }
                    posicionado = true;
                }
            }
        }

        // Método para verificar se um navio pode ser posicionado na posição (x, y) com o tamanho especificado
        public bool PodePosicionarNavio(char[,] tabuleiro, int x, int y, int tamanho, bool horizontal)
        {
            // Verifica se o navio cabe no tabuleiro
            if (horizontal)
            {
                if (y + tamanho > Tamanho) return false; // Excede a borda direita
            }
            else
            {
                if (x + tamanho > Tamanho) return false; // Excede a borda inferior
            }

            // Verifica se há espaço disponível para o navio (sem colisões com outros navios)
            for (int i = 0; i < tamanho; i++)
            {
                if (horizontal)
                {
                    if (tabuleiro[x, y + i] == 'N') return false;
                }
                else
                {
                    if (tabuleiro[x + i, y] == 'N') return false;
                }
            }

            return true;
        }

        // Método para iniciar o jogo
        public void IniciarJogo()
        {
            Console.WriteLine("Bem-vindo ao Batalha Naval!");
            Console.WriteLine("Existe 4 Destroier (2 espaços)");
            Console.WriteLine("Existe 3 Cruzador (3 espaços)");
            Console.WriteLine("Existe 2 Navio-Tanque (4 espaços)");
            while (true)
            {
                MostrarTabuleiro(tabuleiroComputador, ocultarNavios: true);

                int linha, coluna;
                while (true)
                {
                    linha = PedirCoordenada("linha");
                    coluna = PedirCoordenada("coluna");

                    if (!jogadasRealizadas[linha, coluna])
                    {
                        jogadasRealizadas[linha, coluna] = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Você já fez essa jogada! Tente outra coordenada.");
                    }
                }

                tentativas++; // Incrementa apenas se a jogada for nova

                if (tabuleiroComputador[linha, coluna] == 'N')

                {
                    Console.WriteLine("Acertou um navio!");
                    tabuleiroComputador[linha, coluna] = 'X';
                }
                else
                {
                    Console.WriteLine("Errou!");
                    tabuleiroComputador[linha, coluna] = 'O';
                }

                if (VerificarVitoria(tabuleiroComputador))
                {
                    Console.WriteLine("Parabéns! Você venceu!");
                    Console.WriteLine($"Número total de tentativas: {tentativas}");
                    break;
                }
            }
        }

        // Método para verificar se o jogador venceu
        private bool VerificarVitoria(char[,] tabuleiro)
        {
            foreach (char celula in tabuleiro)
            {
                if (celula == 'N')
                {
                    return false;
                }
            }
            return true;
        }

        // Método para mostrar o tabuleiro
        private void MostrarTabuleiro(char[,] tabuleiro, bool ocultarNavios)
        {
            Console.WriteLine("Tabuleiro do Computador:");
            for (int i = 0; i < Tamanho; i++)
            {
                for (int j = 0; j < Tamanho; j++)
                {
                    if (ocultarNavios && tabuleiro[i, j] == 'N')
                    {
                        Console.Write(" ~ ");
                    }
                    else
                    {
                        Console.Write($" {tabuleiro[i, j]} ");
                    }
                }
                Console.WriteLine();
            }
        }

        // Método para pedir e validar a coordenada (linha ou coluna)
        private int PedirCoordenada(string tipo)
        {
            int coordenada;
            while (true)
            {
                Console.Write($"Digite a {tipo} (0 a 9): ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Entrada inválida. Por favor, insira um número entre 0 e 9.");
                    continue;
                }

                if (int.TryParse(input, out coordenada) && coordenada >= 0 && coordenada < 10)
                {
                    return coordenada;
                }
                else
                {
                    Console.WriteLine("Número inválido. Por favor, insira um número entre 0 e 9.");
                }
            }
        }
    }
}
