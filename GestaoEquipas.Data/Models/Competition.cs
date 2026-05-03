namespace GestaoEquipas.Data.Models
{
    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = "Liga";
        public string Season { get; set; } = string.Empty;
    }
}
