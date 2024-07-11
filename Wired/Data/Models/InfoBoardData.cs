using System.ComponentModel.DataAnnotations;

namespace InfoBoard.Data.Models
{
    public class InfoBoardData
    {
        [Key] public int InfoBoardDataIndex { get; set; }
        public int InfoBoardType { get; set; }
        public string? InfoBoardDescription { get; set; }
        public string? Data01 { get; set; }
        public string? Data01Note { get; set; }
        public string? Data02 { get; set; }
        public string? Data02Note { get; set; }
        public string? Data03 { get; set; }
        public string? Data03Note { get; set; }
        public DateTime? Time { get; set; }
        public string? TimeNote { get; set; }
    }
}