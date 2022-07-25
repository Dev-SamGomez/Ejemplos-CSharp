//Con LINQ

internal class Program
{
    private static void Main(string[] args)
    {
    }

    private static DataTable RemoveDuplicatesRecords(DataTable dt)
    {
        var UniqueRows = dt.AsEnumerable().Distinct(DataRowComparer.Default);
        DataTable dt2 = UniqueRows.CopyToDataTable();
        return dt2;
    }
}