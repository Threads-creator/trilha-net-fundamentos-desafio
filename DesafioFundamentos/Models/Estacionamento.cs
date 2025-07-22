using System.Text;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private int quantidadeVagas = 0;

        #region Relatórios
        private int quantidadePagamentos = 0;
        private decimal totalArrecadado = 0;
        private int totalHoras = 0;
        #endregion

        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora, int quantidadeVagas)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
            this.quantidadeVagas = quantidadeVagas;
        }

        private bool ValidarPlaca(string placa)
        {
            var padraoGerado = new StringBuilder();
            var padraoPlacaAntiga = "LLLNNNN";
            var padraoPlacaNova = "LLLNLNN";

            for (int i = 0; i < placa.Length; i++)
            {
                var letra = placa[i];
                if (Char.IsLetter(letra))
                    padraoGerado.Append("L");
                else if (Char.IsDigit(letra))
                    padraoGerado.Append("N");
                else
                    padraoGerado.Append("I"); //inválido
            }

            //verifica placa no padrão antigo - ABC1234
            if (padraoGerado.Equals(padraoPlacaAntiga))
                return true;
            //verifica placa no padrão novo - ABC1D23
            if (padraoGerado.Equals(padraoPlacaNova))
                return true;

            return false;
        }

        public void AdicionarVeiculo()
        {
            if (veiculos.Count >= quantidadeVagas)
            {
                Console.WriteLine("Estacionamento está cheio. Não é possível adicionar mais veículos.");
                return;
            }

            Console.WriteLine("Digite a placa do veículo para estacionar:");
            var placaVeiculo = Console.ReadLine();

            if (string.IsNullOrEmpty(placaVeiculo) || !ValidarPlaca(placaVeiculo))
            {
                Console.WriteLine("Placa inválida. Tente novamente.");
                return;
            }
            if (veiculos.Any(x => x.Equals(placaVeiculo, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Já existe um veículo estacionado com essa placa. Tente Novamente.");
                return;
            }

            veiculos.Add(placaVeiculo);
            Console.WriteLine($"Veículo {placaVeiculo} adicionado com sucesso.");
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            // Pedir para o usuário digitar a placa e armazenar na variável placa
            string placa = Console.ReadLine();

            // Verifica se o veículo existe
            if (veiculos.Any(x => x.Equals(placa, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");            
                int horas = Convert.ToInt32(Console.ReadLine());
                decimal valorTotal = precoInicial + precoPorHora * horas;

                totalArrecadado += valorTotal;
                totalHoras += horas;
                quantidadePagamentos++;

                veiculos.Remove(placa);

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                for (int i = 0; i < veiculos.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - Veiculo {veiculos[i]}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        public void ExibirRelatorioDoDia()
        {
            Console.WriteLine($"Ao fim do dia o estacionamento registrou:\n" 
                            +   $"- {quantidadePagamentos} pagamentos; \n"
                            +   $"- Cada veículo ficou cerca de {totalHoras/quantidadePagamentos} horas em média no estacionado; \n"
                            +   $"- Houve uma arrecadação de {totalArrecadado.ToString("C2")}; \n"
                            );
        }
    }
}
