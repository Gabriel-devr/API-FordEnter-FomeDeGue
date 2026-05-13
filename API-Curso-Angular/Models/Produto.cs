using API_Curso_Angular.Models.Enums;

namespace API_Curso_Angular.Models {
    public class Produto {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ECategoria Categoria { get; set; }
        public string? Img { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public long QuantidadeEmEstoque { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
