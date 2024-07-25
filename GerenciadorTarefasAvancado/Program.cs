using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace GerenciadorTarefasAvancado
{
    class Program
    {
        static List<Tarefa> tarefas = new List<Tarefa>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Sistema de Gerenciamento de Tarefas");
                Console.WriteLine("1. Adicionar Tarefa");
                Console.WriteLine("2. Listar Tarefas");
                Console.WriteLine("3. Buscar Tarefa por Nome");
                Console.WriteLine("4. Remover Tarefa");
                Console.WriteLine("5. Atualizar Status da Tarefa");
                Console.WriteLine("6. Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarTarefa();
                        break;
                    case "2":
                        ListarTarefas();
                        break;
                    case "3":
                        BuscarTarefa();
                        break;
                    case "4":
                        RemoverTarefa();
                        break;
                    case "5":
                        AtualizarStatusTarefa();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AdicionarTarefa()
        {
            Console.Clear();
            Console.Write("Digite o nome da tarefa: ");
            string nome = Console.ReadLine();
            Console.Write("Digite a categoria da tarefa: ");
            string categoria = Console.ReadLine();
            Console.Write("Digite a prioridade da tarefa (Baixa, Média, Alta): ");
            string prioridade = Console.ReadLine();
            Console.Write("Digite a descrição da tarefa: ");
            string descricao = Console.ReadLine();

            tarefas.Add(new Tarefa
            {
                Nome = nome,
                Categoria = categoria,
                Prioridade = prioridade,
                Descricao = descricao,
                Status = "Pendente",
                DataCriacao = DateTime.Now
            });

            Console.WriteLine("Tarefa adicionada com sucesso! Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        static void ListarTarefas()
        {
            Console.Clear();
            if (tarefas.Count == 0)
            {
                Console.WriteLine("Nenhuma tarefa adicionada.");
            }
            else
            {
                Console.WriteLine("Lista de Tarefas:");
                foreach (var tarefa in tarefas)
                {
                    Console.WriteLine($"Nome: {tarefa.Nome}, Categoria: {tarefa.Categoria}, Prioridade: {tarefa.Prioridade}, Status: {tarefa.Status}, Criada em: {tarefa.DataCriacao}");
                    Console.WriteLine($"Descrição: {tarefa.Descricao}");
                }
            }
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        static void BuscarTarefa()
        {
            Console.Clear();
            Console.Write("Digite o nome da tarefa para buscar: ");
            string nome = Console.ReadLine();

            var tarefasEncontradas = tarefas.Where(t => t.Nome.IndexOf(nome, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            if (tarefasEncontradas.Count == 0)
            {
                Console.WriteLine("Nenhuma tarefa encontrada com o nome informado.");
            }
            else
            {
                Console.WriteLine("Tarefas Encontradas:");
                foreach (var tarefa in tarefasEncontradas)
                {
                    Console.WriteLine($"Nome: {tarefa.Nome}, Categoria: {tarefa.Categoria}, Prioridade: {tarefa.Prioridade}, Status: {tarefa.Status}, Criada em: {tarefa.DataCriacao}");
                    Console.WriteLine($"Descrição: {tarefa.Descricao}");
                }
            }
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        static void RemoverTarefa()
        {
            Console.Clear();
            Console.Write("Digite o nome da tarefa que deseja remover: ");
            string nome = Console.ReadLine();

            var tarefaParaRemover = tarefas.FirstOrDefault(t => t.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

            if (tarefaParaRemover != null)
            {
                tarefas.Remove(tarefaParaRemover);
                Console.WriteLine("Tarefa removida com sucesso!");
            }
            else
            {
                Console.WriteLine("Nenhuma tarefa encontrada com o nome informado.");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        static void AtualizarStatusTarefa()
        {
            Console.Clear();
            Console.WriteLine("Lista de Tarefas:");
            ListarTarefas();

            Console.Write("Digite o nome da tarefa para atualizar o status: ");
            string nome = Console.ReadLine();

            var tarefaParaAtualizar = tarefas.FirstOrDefault(t => t.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

            if (tarefaParaAtualizar != null)
            {
                string novoStatus = "";
                bool statusValido = false;

                while (!statusValido)
                {
                    Console.WriteLine($"Status atual da tarefa: {tarefaParaAtualizar.Status}");
                    Console.Write("Digite o novo status da tarefa (Pendente, Em Progresso, Concluída): ");
                    novoStatus = Console.ReadLine().ToLowerInvariant().Trim();

                    switch (novoStatus)
                    {
                        case "pendente":
                        case "em progresso":
                        case "concluída":
                            tarefaParaAtualizar.Status = CapitalizeFirstLetter(novoStatus);
                            statusValido = true;
                            Console.WriteLine("Status atualizado com sucesso!");
                            break;
                        default:
                            Console.WriteLine("Status inválido. Digite novamente.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Nenhuma tarefa encontrada com o nome informado.");
            }

            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
            return textInfo.ToTitleCase(input);
        }
    }

    class Tarefa
    {
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Prioridade { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}