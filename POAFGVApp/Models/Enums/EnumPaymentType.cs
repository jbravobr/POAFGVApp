using System;
namespace POAFGVApp
{
    public enum EnumPaymentType
    {
        [EnumDescription("Cartão de Crédito")]
        CreditCard = 0,
        [EnumDescription("Cartão de Débito")]
        Debit = 1,
        [EnumDescription("Dinheiro")]
        Cash = 2
    }
}
