using System.ComponentModel.DataAnnotations;

namespace InfoBoard.Data.Models
{
    public class InfoBoardSettings
    {
        [Key] public int InfoBoardSettingIndex { get; set; }
        public string? SettingDescription { get; set; }
        public int SettingValueInt { get; set; }
        public string? SettingValueString { get; set; }
    }
}