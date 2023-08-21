namespace DesafioDev.Operacoes.Applications.ViewModel
{
    public class OperacaoViewModel
    {
        public Guid OperacaoId { get; set; }
        public int TipoTransacaoId { get; set; }
        public Guid LojaId { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public decimal Valor { get; set; }
        public string Cpf { get; set; }
        public string CartaoTransacao { get; set; }
        public string HoraOcorrencia { get; set; }
        public TipoTransacaoViewModel TipoTransacao { get; set; }
        public LojaViewModel Loja { get; set; }
    }
}
