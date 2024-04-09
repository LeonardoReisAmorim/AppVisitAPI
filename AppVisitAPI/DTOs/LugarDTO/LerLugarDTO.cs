namespace AppVisitAPI.DTOs.LugarDTO
{
    public class LerLugarDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int ArquivoId { get; set; }
        public int CidadeId { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public string Cidade { get; set; }
        public string NomeArquivo { get; set; }
        public string InstrucoesUtilizacaoVR { get; set; }
    }
}
