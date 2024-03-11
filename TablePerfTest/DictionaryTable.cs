using System.Data;

namespace TablePerfTest;



public class DictionaryTableRow : Dictionary<string, object>
{
    
}
    

public class DictionaryTable
{
    private readonly SharedTableColumn[] _columns;

    private readonly IList<DictionaryTableRow> _rows = new List<DictionaryTableRow>();

    public int Size => _rows.Count;

    public DictionaryTable(params SharedTableColumn[] columns)
    {
        _columns = columns;
    }

    public void AddRows(IEnumerable<Dictionary<string, object>> rows)
    {
        foreach (var kvp in rows)
        {
            var row = new DictionaryTableRow();
            foreach (var key in kvp.Keys)
            {
                row[key] = kvp[key]; // :(
            }
            _rows.Add(row);
        }
    }

    public void AddRows(DataTable table)
    {
        foreach (DataRow dataRow in table.Rows)
        {
            var tableRow = new DictionaryTableRow();
            foreach (DataColumn c in table.Columns)
            {
                tableRow[c.ColumnName] = dataRow[c.ColumnName];
            }
            _rows.Add(tableRow);
        }
    }
}