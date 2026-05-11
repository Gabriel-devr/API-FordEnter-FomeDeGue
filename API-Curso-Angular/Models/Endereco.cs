namespace API_Curso_Angular.Models {
    public class Endereco {
        public long Id { get; set; }
        public string CEP { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty ;
        public string Rua { get; set; } = string.Empty ;
        public int? Numero { get; set; }
        public string? Descricao { get; set; } = string.Empty;
        public long ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
    }
}
