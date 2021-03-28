﻿using diobank.Enum;
using System;

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
        #endregion
    }
}
