namespace RachaAiBlazor.Models
{
    public class Grupo
    {
        public string Nome { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal ValorPendente { get; set; }
        public bool NoVermelho { get; set; } // true = eu devo | false = eu recebo

        public string IconeCategoria => Categoria.ToLower() switch
        {
            "casa"     => "🏠",
            "lazer"    => "🎉",
            "viagem"   => "✈️",
            "contas"   => "📄",
            "mercado"  => "🛒",
            _          => "💰"
        };
    }
}
