namespace Backend
{
    public record Erro(string mensagem) {
        public static Erro None = new Erro(string.Empty);
    }

    public class Resultado<T>
    {
        public T? Sucesso { get; }
        public Erro Erro { get; }

        public bool FoiSucesso => Sucesso != null;
        public bool TemErro => !FoiSucesso;

        private Resultado(T? successo, Erro erro)
        {
            if (successo is not null && erro != Erro.None || successo is null && erro == Erro.None)
            {
                throw new Exception($"Erro '{nameof(erro)}' inválido");
            }

            Sucesso = successo;
            Erro = erro;
        }

        public static Resultado<T> Ok(T t)
        {
            return new Resultado<T>(t, Erro.None);
        }

        public static Resultado<T> Falha(string mensagemErro)
        {
            return new Resultado<T>(default, new Erro(mensagemErro));
        }
    }
}