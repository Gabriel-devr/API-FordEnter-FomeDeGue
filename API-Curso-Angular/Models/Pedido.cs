using API_Curso_Angular.Models.Enums;

namespace API_Curso_Angular.Models {
    public class Pedido {
        public long Id { get; set; }
        public long NumeroDePedido { get; set; }

        public List<ItemPedido> Items { get; set; } = new List<ItemPedido>();
        

        public long ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public long EnderecoId { get; set; }
        public Endereco Endereco { get; set; } = null!;

        public DateTime CriadoEm {  get; set; } = DateTime.Now;
        public DateTime AtualizadoEm { get; set; } = DateTime.Now;

        public EStatusPagamento StatusPagamento { get; set; } = EStatusPagamento.Pendente;
        public EStatusPedido StatusPedido { get; set; } = EStatusPedido.Processando;

        public decimal ValorTotal { get; set; }
    }
}
