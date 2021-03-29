using diobank.Enums;
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
            if (!ValidarValores(valorDeposito)) return;

            Saldo += valorDeposito;
            Console.WriteLine($"Saldo atual da conta de {Nome} é {Saldo}");
        }

        public bool Sacar(double valorSaque)
        {
            if (!ValidarValores(valorSaque)) return false;

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
            if (!ValidarValores(valorTransferencia)) return;

            if (Sacar(valorTransferencia))
                contaDestino.Depositar(valorTransferencia);
        }

        private bool ValidarValores(double valor) 
        {
            if (valor <= 0)
            {
                Console.WriteLine($"O valor {valor.ToString("0.00")} é inválido!");
                return false;
            }
            return true;
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
