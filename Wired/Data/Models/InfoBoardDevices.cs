using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoBoard.Data.Models
{
    public class InfoBoardDevices
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int InfoBoardDeviceIndex { get; set; }
        public string? IPaddress { get; set; }
        public int BoardDataIndex { get; set; }
        public string? BoardURL { get; set; }
        public bool? ShowClock { get; set; }
        public int? ThemeIndex { get; set; }
        public string? Description { get; set; }
    }
}