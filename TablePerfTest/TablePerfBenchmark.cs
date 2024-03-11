using System.Data;
using BenchmarkDotNet.Attributes;

namespace TablePerfTest;

[MemoryDiagnoser]
public class TablePerfBenchmark
{
    [Params(1_000, 100_000, 1_0)]
    public int amount;

    private DataTable _sourceTable;

    private SharedTableColumn[] _tableColumns;
    
    [Benchmark]
    public int FillDictionaryTable()
    {
        var table = new DictionaryTable(_tableColumns);

        table.AddRows(_sourceTable);
        
        return table.Size;
    }

    [Benchmark]
    public int FillRowBasedTable()
    {
        var table = new RowBasedTable(_tableColumns);
        
        table.AddRows(_sourceTable);

        return table.Size;
    }

    [Benchmark]
    public int FillColumnBasedTable()
    {
        var table = new ColumnBasedTable(amount, _tableColumns);
        
        table.AddRows(_sourceTable);

        return table.Size;
    }

    [GlobalSetup]
    public void SetupData()
    {
        _tableColumns = new[]
        {
            new SharedTableColumn("RND.KEYID", "record_key_id", "Record Key ID","KEY" ),
            new SharedTableColumn("RND.DT", "record_date", "Record Date", "DATETIME"),
            new SharedTableColumn("RND.DURATION", "record_duration", "Record Duration", "TIME"),
            new SharedTableColumn("RND.TYPE", "record_type", "Record Type", "LIST"),
            new SharedTableColumn("RND.DESCRIPTION", "record_description", "Record Description", "TEXT"),
            new SharedTableColumn("RND.MODIFIED_DT", "record_modified_on", "Record Modified On", "DATETIME"),
            new SharedTableColumn("EXT.EXT_24", "record_wbso", "Record Wbso", "BOOL"),
            new SharedTableColumn("CONTACT.KEYID", "record_user_key_id", "Record User Key ID", "KEY"),
            new SharedTableColumn("TASK.KEYID", "record_task_key_id", "Record Task Key ID", "KEY"),
            new SharedTableColumn("TICKET.KEYID", "record_ticket_key_id", "Record Ticket Key ID", "KEY"),
            new SharedTableColumn("INVOICELINE.KEYID", "record_invoice_line_key_id", "Record Invoice Line Key ID", "KEY"),
            new SharedTableColumn("ABSENCETYPE.TYPE", "record_absence_type", "Record Absence Type", "TEXT"),
            new SharedTableColumn("ABSENCETYPE.NAME", "record_absence_name", "Record Absence Name", "TEXT"),
            new SharedTableColumn("ANALYTICS.BUDGET", "record_analytics_budget", "Record Analytics Budget", "CUR"),
            new SharedTableColumn("ANALYTICS.COST", "record_analytics_cost", "Record Analytics Cost", "CUR"),
            new SharedTableColumn("ANALYTICS.VALUE", "record_analytics_value", "Record Analytics Value", "CUR"),
            new SharedTableColumn("ANALYTICS.MARGIN", "record_analytics_margin", "Record Analytics Margin", "CUR"),
            new SharedTableColumn("ANALYTICS.RATE", "record_analytics_rate", "Record Analytics Rate", "CUR"),        };
        
        
        _sourceTable = new DataTable("Table1");
        foreach (var column in _tableColumns)
        {
            _sourceTable.Columns.Add(column.Name);    
        }
        
        for (var i = 0; i < amount; i++)
        {
            var row = _sourceTable.NewRow();
            row["RND.KEYID"] = i;
            row["RND.DT"] = DateTime.Now;
            row["RND.DURATION"] = TimeSpan.FromHours(i);
            row["RND.TYPE"] = i.ToString();
            row["RND.DESCRIPTION"] = i.ToString();
            row["RND.MODIFIED_DT"] = DateTime.Now;
            row["EXT.EXT_24"] = true;
            row["CONTACT.KEYID"] = i;
            row["TASK.KEYID"] = i;
            row["TICKET.KEYID"] = i;
            row["INVOICELINE.KEYID"] = i;
            row["ABSENCETYPE.TYPE"] = i.ToString();
            row["ABSENCETYPE.NAME"] = i.ToString();
            row["ANALYTICS.BUDGET"] = (float)i;
            row["ANALYTICS.COST"] = (float)i;
            row["ANALYTICS.VALUE"] = (float)i;
            row["ANALYTICS.MARGIN"] = (float)i;
            row["ANALYTICS.RATE"] = (float)i;
            _sourceTable.Rows.Add(row);
        }
    }
}