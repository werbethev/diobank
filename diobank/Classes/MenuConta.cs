using diobank.Enums;
using System;
using System.Collections.Generic;

namespace diobank.Classes
{
    public class MenuConta
    {
		public const string CaracterSair = "X";
		public const string CaracterLimparTela = "C";
		public static List<Conta> listContas = new List<Conta>();

		public static void Depositar()
		{
			Console.Write("Digite o número da conta: ");
			int indiceConta = int.Parse(Console.ReadLine());

			Console.Write("Digite o valor a ser depositado: ");
			double valorDeposito = double.Parse(Console.ReadLine());

			listContas[indiceConta].Depositar(valorDeposito);
		}
		public static void ExecutarOpcao(string opcaoUsuario)
		{
			eOpcoesMenuConta opcaoConta = (eOpcoesMenuConta)Enum.Parse(typeof(eOpcoesMenuConta), opcaoUsuario);

			switch (opcaoConta)
			{
				case eOpcoesMenuConta.Listar:
					ListarContas();
					break;
				case eOpcoesMenuConta.Inserir:
					InserirConta();
					break;
				case eOpcoesMenuConta.Transferir:
					Transferir();
					break;
				case eOpcoesMenuConta.Sacar:
					Sacar();
					break;
				case eOpcoesMenuConta.Depositar:
					Depositar();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		public static void InserirConta()
		{
			Console.WriteLine("Inserir nova conta");

			Console.Write($"Digite {(int)eTipoConta.PessoaFisica} para Conta Fisica ou {(int)eTipoConta.PessoaJuridica} para Juridica: ");
			int entradaTipoConta = int.Parse(Console.ReadLine());

			Console.Write("Digite o Nome do Cliente: ");
			string entradaNome = Console.ReadLine();

			Console.Write("Digite o saldo inicial: ");
			double entradaSaldo = double.Parse(Console.ReadLine());

			Console.Write("Digite o crédito: ");
			double entradaCredito = double.Parse(Console.ReadLine());

			Conta novaConta = new Conta(tipo: (eTipoConta)entradaTipoConta,
										saldo: entradaSaldo,
										credito: entradaCredito,
										nome: entradaNome);

			listContas.Add(novaConta);
		}
		public static void ListarContas()
		{
			Console.WriteLine("Listar contas");

			if (listContas.Count == 0)
			{
				Console.WriteLine("Nenhuma conta cadastrada.");
				return;
			}

			for (int i = 0; i < listContas.Count; i++)
			{
				Conta conta = listContas[i];
				Console.Write("#{0} - ", i);
				Console.WriteLine(conta);
			}
		}
		public static void MostrarMenu()
		{
			string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != CaracterSair)
			{
				if (opcaoUsuario.ToUpper() == CaracterLimparTela)
					Console.Clear();
				ExecutarOpcao(opcaoUsuario);
				opcaoUsuario = ObterOpcaoUsuario();
			}
		}
		public static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Bank a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine($"{(int)eOpcoesMenuConta.Listar}- Listar contas");
			Console.WriteLine($"{(int)eOpcoesMenuConta.Inserir}- Inserir nova conta");
			Console.WriteLine($"{(int)eOpcoesMenuConta.Transferir}- Transferir");
			Console.WriteLine($"{(int)eOpcoesMenuConta.Sacar}- Sacar");
			Console.WriteLine($"{(int)eOpcoesMenuConta.Depositar}- Depositar");
			Console.WriteLine($"{CaracterLimparTela} - Limpar Tela");
			Console.WriteLine($"{CaracterSair} - Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
		public static void Sacar()
		{
			Console.Write("Digite o número da conta: ");
			int indiceConta = int.Parse(Console.ReadLine());

			Console.Write("Digite o valor a ser sacado: ");
			double valorSaque = double.Parse(Console.ReadLine());

			listContas[indiceConta].Sacar(valorSaque);
		}
		public static void Transferir()
		{
			Console.Write("Digite o número da conta de origem: ");
			int indiceContaOrigem = int.Parse(Console.ReadLine());

			Console.Write("Digite o número da conta de destino: ");
			int indiceContaDestino = int.Parse(Console.ReadLine());

			Console.Write("Digite o valor a ser transferido: ");
			double valorTransferencia = double.Parse(Console.ReadLine());

			listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino]);
		}
	}
}
