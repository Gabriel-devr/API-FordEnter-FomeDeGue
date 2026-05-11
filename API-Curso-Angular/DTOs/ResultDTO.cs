namespace API_Curso_Angular.DTOs {
    public class ResultDTO <T>{
        public T? Data { get; protected set;  }
        public List<string> Errors { get; protected set; } = new List<string>();

        public ResultDTO(T data, List<string> errors) {
            Data = data;
            Errors = errors;
        }

        public ResultDTO(T data) {
            Data=data;
        }

        public ResultDTO(List<string> errors) {
            Errors = errors;
        }

        public ResultDTO(string erro) {
            Errors.Add(erro);
        }
    }
}
