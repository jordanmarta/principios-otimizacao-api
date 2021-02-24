namespace PrincipiosOtimizacao
{
    public class Produto
    {
        public string CodProduto { get; set; }
        public string NomeProduto { get; set; }
        public double Preco { get; set; }
        public double? TesteValorNuloDouble { get; set; }
        public int? TesteValorNuloInt { get; set; }
        public int? TesteValorNuloString { get; set; }
    }
}
