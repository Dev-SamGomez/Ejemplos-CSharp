//Crear lista a partir de una clase con Objetos
public List<OrdenesDeCompra> ListOrdenesDeCompra = new List<OrdenesDeCompra>();

//Agregar por medio de un Datatable
private void InsertTag(string Tag)
{
    try
    {
        SqlDataReader dr;
        SqlCommand cmd;
        DataTable t = new DataTable();
        cmd = new SqlCommand("select Orden_de_Compra,Cliente,Pais from OrdenesDeCompra", cnn);
        cmd.CommandType = CommandType.Text;
        cnn.Open();
        dr = cmd.ExecuteReader;
        t.Load(dr);
        if (t.Rows.Count > 0)
        {
            List<OrdenesDeCompra> ls = t.AsEnumerable().Select(m => new OrdenesDeCompra() {
                Orden_de_Compra = m.Field<string>("Orden_de_Compra"),
                Cliente = m.Field<string>("Cliente"),
                Pais = m.Field<string>("Pais")
                });
            ListOrdenesDeCompra.AddRange(ls);
        }
        cnn.Close();
    }
    catch (Exception ex)
    {
        cnn.Close();
        Interaction.MsgBox(ex.Message);
    }
}

//Agregar desde elementos
OrdenesDeCompra OrdenCompra = new OrdenesDeCompra();
    OrdenCompra.Orden_de_Compra = 10;
    OrdenCompra.Cliente = "Samuel";
    OrdenCompra.Pais = "Mexico";
    ListOrdenesDeCompra.AddRange(OrdenCompra);

//Clase de objetos para la lista
public class OrdenesDeCompra
{
    public string Orden_de_Compra { get; set; }
    public string Cliente { get; set; }
    public string Pais { get; set; }
}

// Filtrar con LINQ un elemento de la lista //
// Nos dira cuantas veces se encuentra //
var Filtro1 = ListOrdenesDeCompra.Where(d => d.Pais.Equals("Mexico")).ToList().Count();
// Seleccionar un elemento
string Filtro2 = ListOrdenesDeCompra.Where(d => d.Pais.Equals("Mexico")).Select(valor => valor.Pais.Equals("Mexico")).ToString;

 // Recorrer con ForEach
    // OJO: No es mutable
    ListOrdenesDeCompra.ForEach(item =>
    {
        return null;
    }
    );

    // Obtener el indice de un valor de la lista
    int index = ListOrdenesDeCompra.FindIndex(d => d.Cliente.Equals("Samuel"));