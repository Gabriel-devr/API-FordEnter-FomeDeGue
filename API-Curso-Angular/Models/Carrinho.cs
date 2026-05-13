namespace API_Curso_Angular.Models {
    public class Carrinho {
        public long Id { get; set; }
        public List<ItemCarrinho> Items { get; set; } = new List<ItemCarrinho>();
        public long ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public string? Descricao { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
        public decimal ValorTotal { get; set; }

    }
}
