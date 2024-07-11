using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using InfoBoard.Data;
using InfoBoard.Hubs;
using System;
using InfoBoard.Data.Models;

namespace InfoBoard.Services
{
    public class InfoService
    {
        private readonly IHubContext<InfoHub> _context;
        InfoDBcontext dbContext = new InfoDBcontext();
        private readonly SqlTableDependency<InfoBoardData> _dependency;  // the listener
        private readonly string _connectionString;

        public InfoService(IHubContext<InfoHub> context)
        {
            _context = context;

            // TODO get from config
            _connectionString = "Data Source=PF3VL379\\SQLEXPRESS;Database=Snowflake;User Id=signal;Password=Thing098765;";

            // instantiate and start the listener
            _dependency = new SqlTableDependency<InfoBoardData>(_connectionString, "InfoBoardData");
            _dependency.OnChanged += Changed;  // when changes happen, run that method
            _dependency.Start();
        }

        private async void Changed(object sender, RecordChangedEventArgs<InfoBoardData> e)
        {
            // TODO  based on what has changed, call a different method

            if (e.Entity.InfoBoardType == 1)
            {
                var HsLines = await GetHS();
                // send out the new list to all that are listening to the "UpdateHandstack" on the hub
                await _context.Clients.All.SendAsync("UpdateHandstack", HsLines);
            }

            if (e.Entity.InfoBoardType == 2)
            {
                var WaitLines = await GetPalletsWaiting();
                //send out the new list to all that are listening to the "UpdateWait" on the hub
                await _context.Clients.All.SendAsync("UpdateWait", WaitLines);
            }
        }
        public async Task<List<InfoBoardData>> GetHS()
        {
            return await dbContext.InfoBoardData.AsNoTracking().ToListAsync();
        }

        public async Task<List<InfoBoardData>> GetPalletsWaiting()
        {
            return await dbContext.InfoBoardData.AsNoTracking().ToListAsync();
        }

    }
}
