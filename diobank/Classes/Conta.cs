using diobank.Enum;

namespace diobank.Classes
{
    public class Conta
    {
        public string Nome { get; private set; }
        public double Saldo { get; private set; }
        public double Credito { get; private set; }
        public eTipoConta Tipo { get; private set; }
        public Conta(string nome, double saldo, double credito, eTipoConta tipo)
        {
            Nome = nome;
            Saldo = saldo;
            Credito = credito;
            Tipo = tipo;
        }
    }
}
