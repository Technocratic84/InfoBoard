using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using InfoBoard.Data;
using InfoBoard.Data.Models;
using InfoBoard.Shared;
using System;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace InfoBoard.Shared
{
    public class TableChangeNotifier
    {
        public readonly string _infoConnectionString;        
        private readonly string _tableName;
        private SqlTableDependency<InfoBoardData> _tableDependency;
        private DateTime LastRelease;
        private DateTime LastReset;
        private DateTime LastLog;
        private string LastDescription;

        public TableChangeNotifier(string infoConnectionString, string tableName)
        {
            _infoConnectionString = infoConnectionString;            
            _tableName = tableName;
        }

        public void StartMonitoring()
        {
            _tableDependency = new SqlTableDependency<InfoBoardData>(_infoConnectionString, _tableName);
            _tableDependency.OnChanged += TableDependency_Changed;
            _tableDependency.Start();
            Log.Information("___________  IIS/ASP TableChangeNotifier started monitoring  ____________");
            LastRelease = DateTime.Now;
            LastReset = DateTime.Now;
            LastLog = DateTime.Now;
        }
        public void StopMonitoring()
        {
            if (_tableDependency != null)
            {
                _tableDependency.Stop();
                _tableDependency.OnChanged -= TableDependency_Changed;
                _tableDependency.Dispose();
                Log.Information("^^^^^^^^^^^  IIS/ASP TableChangeNotifier stopped monitoring  ^^^^^^^^^^^");
            }
        }
        private void TableDependency_Changed(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<InfoBoardData> e)
        {
            if (LastLog.AddMilliseconds(333) < DateTime.Now || LastDescription != e.Entity.Data01Note)  // no need to spam this, dozens can come through in a second
            {
                Log.Debug("TableChangeNotifier detected change...  " + e.Entity.Data01Note);
                LastDescription = e.Entity.Data01Note;
                LastLog = DateTime.Now;
            }

            if (e.Entity.Data01 == "wut")  // TODO:EDIT
            {
                //  if timestamp is less than 2 seconds ago, skip 
                if (LastRelease.AddSeconds(2) < DateTime.Now)
                {
                    LastRelease = DateTime.Now;

                    Log.Information(">> TableChangeNotifier : Things");

                    var InfoContextOptions = new DbContextOptionsBuilder<InfoDBcontext>()
                        .UseSqlServer(_infoConnectionString)
                        .EnableDetailedErrors(true)
                        .Options;

                    using var infoContext = new InfoDBcontext();

                    // DO THINGS

                }
            }
            else
            {
                if (e.Entity.Data01 == "Something else")
                {
                    if (LastReset.AddMinutes(5) < DateTime.Now)
                    {
                        LastReset = DateTime.Now;
                        Log.Information(">> TableChangeNotifier : other things");

                       // DO OTHER THINGS
                    }
                }
            }
        }
    }
}
