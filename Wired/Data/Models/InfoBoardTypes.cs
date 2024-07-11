using System.ComponentModel.DataAnnotations;

namespace InfoBoard.Data.Models
{
    public class InfoBoardTypes
    {
        [Key] public int InfoBoardTypeIndex { get; set; }        
        public string InfoBoardDescription { get; set; }        
    }
}



