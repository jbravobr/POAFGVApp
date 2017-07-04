using System;
namespace POAFGVApp
{
    public enum EnumPaymentType
    {
        [EnumDescription("Cartão de Crédito")]
        CreditCard = 0,
        [EnumDescription("Cartão de Débito")]
        DebitCard = 1,
        [EnumDescription("Dinheiro")]
        Cash = 2,

        [EnumDescription("Cartao de Credito")]
        Credit = 3,
        [EnumDescription("Carta de Debito")]
        Debit = 4
    }
}
