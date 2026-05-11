namespace API_Curso_Angular.Models {
    public class ItemPedido {
        public long Id { get; set; }
        public long ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        public double ValorUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}
