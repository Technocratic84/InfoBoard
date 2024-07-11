using InfoBoard.Data.Models;
using Microsoft.AspNetCore.SignalR;

namespace InfoBoard.Hubs
{
    public class InfoHub : Hub
    {
        public async Task UpdateHandstack(List<InfoBoardData> HsLines)
        {
            await Clients.All.SendAsync("UpdateHandstack", HsLines);
        }
        public async Task UpdateWait(InfoBoardData newData)
        {
            await Clients.All.SendAsync("UpdateWait", newData);
        }
    }
}
