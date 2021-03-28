using diobank.Classes;
using diobank.Enum;
using Xunit;

namespace diobankTeste.Classes
{
    public class ContaTeste
    {
        #region Construtor
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
        #endregion

        #region Sacar       
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
        public void Sacar_DeveSubtrairValorSaqueDoValorSaldo(double valorSaque, double saldoEsperado)
        {
            var conta = new Conta("", 100, 100, eTipoConta.PessoaFisica);

            conta.Sacar(valorSaque);

            Assert.Equal(saldoEsperado, conta.Saldo);
        }

        [Fact]
        public void Sacar_VariosSaquesMesmaContaDeveSubtrairSaldo()
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
        #endregion

        #region Depositar
        [Fact]
        public void Depositar_ValorDeposito0NaoDeveAlterarSaldo()
        {
            const double saldoInicial = 0;
            var conta = new Conta("", saldoInicial, 0, eTipoConta.PessoaFisica);

            conta.Depositar(0);

            Assert.Equal(saldoInicial, conta.Saldo);
        }

        [Fact]
        public void Depositar_ValorDepositoNegativoNaoDeveAlterarSaldo()
        {
            const double saldoInicial = 0;
            var conta = new Conta("", saldoInicial, 0, eTipoConta.PessoaFisica);

            conta.Depositar(-20);

            Assert.Equal(saldoInicial, conta.Saldo);

        }

        [Fact]
        public void Depositar_ValorDepositoDeveSomarValorDepositoSaldo()
        {
            const double valorDeposito = 20;
            var conta = new Conta("", 0, 0, eTipoConta.PessoaFisica);

            conta.Depositar(valorDeposito);

            Assert.Equal(valorDeposito, conta.Saldo);
        }

        [Fact]
        public void Depositar_VariosDepositosMesmaContaDeveSomarValoresAoSaldo()
        {
            var conta = new Conta("", -50, 0, eTipoConta.PessoaFisica);

            conta.Depositar(20);
            Assert.Equal(-30, conta.Saldo);

            conta.Depositar(30);
            Assert.Equal(0, conta.Saldo);

            conta.Depositar(50);
            Assert.Equal(50, conta.Saldo);
        }
        #endregion

        #region ToString
        [Fact]
        public void ToString_NaoDeveRetornarVazioOuNulo()
        {
            var conta = new Conta("", 100, 0, eTipoConta.PessoaFisica);

            var retorno = conta.ToString();

            Assert.NotNull(retorno);
            Assert.NotEmpty(retorno);
        }

        [Fact]
        public void ToString_DeveRetornarTextoDeAcordoComPropriedadesPassadas()
        {
            const string nome = "Conta Teste";
            const double saldo = 100;
            const double credito = 200;
            const eTipoConta tipo = eTipoConta.PessoaJuridica;
            var conta = new Conta(nome, saldo, credito, tipo);

            var retorno = conta.ToString();

            var retornoEsperado = $"TipoConta {tipo} | Nome {nome} | Saldo {saldo.ToString("0.00")} | Crédito {credito.ToString("0.00")}";
            Assert.Equal(retornoEsperado, retorno);
        }
        #endregion

        #region Transferir
        [Fact]
        public void Transferir_ValorTranferencia0OuNegativoNaoDeveAlterarSaldoDasContas()
        {
            const double saldoInicialContaOrigem = 100;
            const double saldoInicialContaDestino = 0;
            var contaOrigem = new Conta("", saldoInicialContaOrigem, 0, eTipoConta.PessoaFisica);
            var contaDestino = new Conta("", saldoInicialContaDestino, 0, eTipoConta.PessoaFisica);

            contaOrigem.Transferir(0, contaDestino);
            contaOrigem.Transferir(-30, contaDestino);

            Assert.Equal(saldoInicialContaOrigem, contaOrigem.Saldo);
            Assert.Equal(saldoInicialContaDestino, contaDestino.Saldo);
        }

        [Fact]
        public void Transferir_DeveSubtrairDeUmaContaSomarNaOutra()
        {
            const double valorTransferencia = 10.65;
            var contaOrigem = new Conta("", 100, 0, eTipoConta.PessoaFisica);
            var contaDestino = new Conta("", 0, 0, eTipoConta.PessoaFisica);

            contaOrigem.Transferir(valorTransferencia, contaDestino);

            Assert.Equal(89.35, contaOrigem.Saldo);
            Assert.Equal(valorTransferencia, contaDestino.Saldo);
        }

        [Fact]
        public void Transferir_UsandoValorCreditoDeveSubtrairDeUmaContaSomarNaOutra()
        {
            const double valorTransferencia = 50;
            var contaOrigem = new Conta("", 0, 100, eTipoConta.PessoaFisica);
            var contaDestino = new Conta("", 0, 0, eTipoConta.PessoaFisica);

            contaOrigem.Transferir(valorTransferencia, contaDestino);

            Assert.Equal(-50, contaOrigem.Saldo);
            Assert.Equal(valorTransferencia, contaDestino.Saldo);
        }

        [Fact]
        public void Transferir_VariasTransferenciasDeveSubtrairValoresDeUmaContaSomarNaOutra()
        {
            var contaOrigem = new Conta("", 100, 100, eTipoConta.PessoaFisica);
            var contaDestino = new Conta("", 0, 0, eTipoConta.PessoaFisica);

            contaOrigem.Transferir(90, contaDestino);
            Assert.Equal(10, contaOrigem.Saldo);
            Assert.Equal(90, contaDestino.Saldo);

            contaOrigem.Transferir(20, contaDestino);
            Assert.Equal(-10, contaOrigem.Saldo);
            Assert.Equal(110, contaDestino.Saldo);
        }
        #endregion
    }
}
