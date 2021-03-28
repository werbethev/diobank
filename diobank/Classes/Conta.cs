using diobank.Enum;
using System;

namespace diobank.Classes
{
    public class Conta
    {
        public double Credito { get; private set; }
        public string Nome { get; private set; }
        public double Saldo { get; private set; }
        public eTipoConta Tipo { get; private set; }
        public Conta(string nome, double saldo, double credito, eTipoConta tipo)
        {
            Nome = nome;
            Saldo = saldo;
            Credito = credito;
            Tipo = tipo;
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
    }
}
