using diobank.Classes;
using diobank.Enum;
using Xunit;
using Xunit.Sdk;

namespace diobankTeste.Classes
{
    public class ContaTeste
    {
        [Fact]
        public void Construtor_PropriedadesDevemSerIguaisAoQueFoiPassado()
        {
            const string nomeConta = "Conta 1";
            const double saldo = 10;
            const double credito = 20;
            const eTipoConta pessoaFisica = eTipoConta.PessoaFisica;
            var conta = new Conta(nomeConta, saldo, credito, pessoaFisica);

            Assert.Equal(nomeConta, conta.Nome);
            Assert.Equal(saldo, conta.Saldo);
            Assert.Equal(credito, conta.Credito);
            Assert.Equal(pessoaFisica, conta.Tipo);
        }

        [Theory]
        [InlineData(0, 0, 100, false)]
        [InlineData(50, 0, 100, true)]
        [InlineData(150, 0, 100, false)]
        [InlineData(100, 0, 100, true)]
        [InlineData(150, 50, 100, true)]
        [InlineData(50, 50, 0, true)]
        [InlineData(100, 50, 0, false)]
        public void Sacar(double valorSaque, double saldo, double credito, bool resultadoEsperado)
        {
            var conta = new Conta("", saldo, credito, eTipoConta.PessoaFisica);

            bool retorno = conta.Sacar(valorSaque);

            Assert.Equal(resultadoEsperado, retorno);
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(10, 90)]
        [InlineData(100, 0)]
        [InlineData(150, -50)]
        [InlineData(200, -100)]
        [InlineData(250, 100)]//saque inválido continua mesmo saldo
        public void Sacar_DeveAlterarValorSaldo(double valorSaque, double saldoEsperado)
        {
            var conta = new Conta("", 100, 100, eTipoConta.PessoaFisica);

            conta.Sacar(valorSaque);

            Assert.Equal(saldoEsperado, conta.Saldo);
        }

        [Fact]
        public void Sacar_VariosSquesMesmaCnotaDeveAlterarSaldo()
        {
            var conta = new Conta("", 100, 100, eTipoConta.PessoaFisica);

            conta.Sacar(10);
            Assert.Equal(90, conta.Saldo);

            conta.Sacar(80);
            Assert.Equal(10, conta.Saldo);

            conta.Sacar(10);
            Assert.Equal(0, conta.Saldo);

            conta.Sacar(50);
            Assert.Equal(-50, conta.Saldo);
        }
    }
}
