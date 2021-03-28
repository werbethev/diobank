using diobank.Enum;
using System;
using System.Text;

namespace diobank.Classes
{
    public class Conta
    {
        #region Propriedades
        public double Credito { get; private set; }
        public string Nome { get; private set; }
        public double Saldo { get; private set; }
        public eTipoConta Tipo { get; private set; }
        #endregion

        #region Construtores
        public Conta(string nome, double saldo, double credito, eTipoConta tipo)
        {
            Nome = nome;
            Saldo = saldo;
            Credito = credito;
            Tipo = tipo;
        }
        #endregion
        #region Metodos
        public void Depositar(double valorDeposito)
        {
            if (valorDeposito <= 0)
            {
                Console.WriteLine($"O valor {valorDeposito.ToString("0.00")} é inválido!");
                return;
            }

            Saldo += valorDeposito;
            Console.WriteLine($"Saldo atual da conta de {Nome} é {Saldo}");
        }

        public bool Sacar(double valorSaque)
        {
            if (valorSaque <= 0)
            {
                Console.WriteLine($"O valor {valorSaque.ToString("0.00")} é inválido!");
                return false;
            }

            if (Saldo - valorSaque < -Credito)
            {
                Console.WriteLine("Saldo insuficiente!");
                return false;
            }

            Saldo -= valorSaque;
            Console.WriteLine($"Saldo atual da conta de {Nome} é {Saldo}");

            return true;
        }

        public void Transferir(double valorTransferencia, Conta contaDestino)
        {
            if (valorTransferencia <= 0)
            {
                Console.WriteLine($"O valor {valorTransferencia.ToString("0.00")} é inválido!");
                return;
            }

            if (Sacar(valorTransferencia))
                contaDestino.Depositar(valorTransferencia);
        }

        #region Sobreescrita
        public override string ToString()
        {
            return new StringBuilder()
                .AppendFormat("TipoConta {0} | ", Tipo)
                .AppendFormat("Nome {0} | ", Nome)
                .AppendFormat("Saldo {0} | ", Saldo.ToString("0.00"))
                .AppendFormat("Crédito {0}", Credito.ToString("0.00"))
                .ToString();
        }
        #endregion
        #endregion
    }
}
