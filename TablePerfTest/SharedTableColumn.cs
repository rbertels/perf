namespace TablePerfTest;

public class SharedTableColumn
{
    public string Name { get; set; }
    
    public string InternalName { get; set; }
    
    public string Caption { get; set; }
    
    public string Type { get; set; }
    
    public int Width { get; set; }

    public SharedTableColumn(string name, string internalName, string caption, string type, int width = 0)
    {
        Name = name;
        InternalName = internalName;
        Caption = caption;
        Type = type;
        Width = width;
    }
}