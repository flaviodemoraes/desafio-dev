using System.ComponentModel;

namespace DesafioDev.Operacoes.Domain.Enuns
{
    public enum TipoOperacaoDescricaoEnum
    {
        [Description("Débito")]
        [DefaultValue("Débito")]
        Debito,

        [Description("Boleto")]
        Boleto,

        [Description("Financiamento")]
        Financiamento,

        [Description("Crédito")]
        Credito,

        [Description("RecebimentoEmprestimo")]
        RecebimentoEmprestimo,

        [Description("Vendas")]
        Vendas,

        [Description("RecebimentoTED")]
        RecebimentoTED,

        [Description("RecebimentoDOC")]
        RecebimentoDOC,

        [Description("Aluguel")]
        Aluguel
    }
}
