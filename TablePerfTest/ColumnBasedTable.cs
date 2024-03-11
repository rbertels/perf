using System.Data;

namespace TablePerfTest;

public class ColumnBasedTable
{
    private readonly SharedTableColumn[] _columns;

    private readonly List<object>[] _data;
    
    public int Size => _data[0].Count; // bad!
    
    public ColumnBasedTable(int cap, params SharedTableColumn[] columns)
    {
        _columns = columns;

        _data = new List<object>[_columns.Length];
        for (var i = 0; i < _columns.Length; i++)
        {
            _data[i] = new List<object>(cap);
        }
    }
    
    public void AddRows(DataTable table)
    {
        foreach (DataRow dataRow in table.Rows)
        {
            for(var i = 0; i < _columns.Length; i++)
            {
                _data[i].Add(dataRow[_columns[i].Name]);
            }
        }
    }
}