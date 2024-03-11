using System.Data;

namespace TablePerfTest;

public class RowBasedTable
{
    private readonly SharedTableColumn[] _columns;

    private readonly IList<object[]> _rows = new List<object[]>();
    
    public int Size => _rows.Count;

    public RowBasedTable(params SharedTableColumn[] columns)
    {
        _columns = columns;
    }
    
    public void AddRows(DataTable table)
    {
        foreach (DataRow dataRow in table.Rows)
        {
            var tableRow = new Object[_columns.Length];
            for(var i = 0; i < _columns.Length; i++)
            {
                tableRow[i] = dataRow[_columns[i].Name];
            }
            _rows.Add(tableRow);
        }
    }
    
}