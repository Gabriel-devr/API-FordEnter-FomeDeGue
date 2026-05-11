namespace API_Curso_Angular.Models {
    public class ItemCarrinho {
        public long Id { get; set; }
        public long ProdutoId { get; set; }
        public Produto Produto { get; set; } = new Produto();
        public int Quantidade { get; set; }
    }
}
