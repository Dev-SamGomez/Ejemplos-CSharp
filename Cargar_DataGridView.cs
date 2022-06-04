//Cargar un DataGridView con un Datatable
private void LoadGrid()
{
    try
    {
        string query = $"SELECT * FROM Vendors WHERE IDVendor = '{VendorID}'";
        Datatable Table;
        SqlCommand cmd;
        SqlDataReader dr;
        cmd = new SqlCommand(query, cnn);
        cmd.CommandType = CommandType.Text;
        cnn.Open();
        dr = cmd.ExecuteReader;
        Table.Load(dr);
        cnn.Close();
        if (Table.Rows.Count > 0)
            YouDataGridView.DataSource = Table;
        else
            YouDataGridView.DataSource = null;
    }
    catch (Exception ex)
    {
        cnn.Close();
        MessageBox.Show(ex.Message + Constants.vbNewLine + ex.ToString());
    }
}

// Cargar un DataGridView con una Lista
private void LoadGridWithList()
{
    try
    {
        if (ListItems.Count > 0 | ListItems != null) //Verificamos mediante una validacion que la lista con la que llenaremos la Grid no este vacia.
            YouDataGridView.DataSource = ListItems.ToList(); // Si la lista contiene mas de 0 elementos entonces usamos la propiedad de la Grid DataSource y la cargamos con la lista.
        else
            YouDataGridView.DataSource = null;
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message + Constants.vbNewLine + ex.ToString());
    }
}

//Cargar un DatagridView desde un archivo de Excel
    private void LoadGridByExcel()
    {
        try
        {
            // En este metodo, cargaremos un Excel usando la clase OpenDialog, esta para generar una ventana donde seleccionaremos un archivo
            // de Excel y, pasemos la informacion a la DataGridView
            Datatable Table = new Datatable();
            Windows.Forms.OpenFileDialog OpenFileDlg = new Windows.Forms.OpenFileDialog(); // Generamos la Clase OpenFileDialog
            DialogResult result = OpenFileDlg.ShowDialog(); // El archivo seleccionado, lo grabaremos en una variable de la Clase Dialog Result
            string path = OpenFileDlg.FileName; // Este guardamos el Path
            string archivo = path.ToString();
            OpenFileDlg = null; // Matamos el Open Dialog
// Generamos la conexion, esta sera la de Microsoft OLEDB v.12
// OJO: Asegurate de tener instalada este framework en los equipos clientes de lo contrario tendras excepciones con esta conexion
// Puedes descargarla gratis en la pagina de Microsoft
            string stringConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + archivo + "';Extended Properties=\"Excel 12.0;HDR=YES\"";
            OleDbConnection MyConnection = new OleDbConnection(stringConnection);
            // Una vez hecha la conexion, al ser consulta tipo Access, generamos la query seleccionand la Sheet1
            // OJO: asegurate de que sea este el nombre de la hoja, puedes cambiarla o, en los ejemplos de Trabajos con Excel, podras encontrar la forma de como extraer previamente
            // el nombre de la hoja y como ponerla en la query como parametro.
            System.Data.OleDb.OleDbDataAdapter MyCommand = new System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sheet1$]", MyConnection);
            MyConnection.Open();
            MyCommand.Fill(Table); // Llenamos la DataTable
            MyConnection.Close();
            if (Table.Rows.Count > 0)
                YouDataGridView.DataSource = Table;
            else
                YouDataGridView.DataSource = null;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    // Cargar una DataGridView con un filtro
    // En este ejemplo, la problematica a resolver es, cada vez que el usuario escriba en un TextBox, este vaya filtrando en una Query directo a la 
    // DB y a su vez, los resultados se vayan reflejando en la DataGridView

    private void txtBuscador_TextChanged_1(object sender, EventArgs e)
    {
        // Cada vez que ingrese un dato el usuario, esta al ser el event Changed del TextBox, enviara al Metodo filterData
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
        string filter = (TextBox)sender.Text;
        if (filter.Trim() != string.Empty)
            filterData(filter);
        Cursor.Current = System.Windows.Forms.Cursors.Default;
    }

    public void filtrarDatos(string buscar)
    {
        try
        {
            SqlDataAdapter da1 = new SqlDataAdapter(); // Declaramos la clase SqlDataAdapter
            string query = "SELECT * FROM Costumers WHERE Costumer like @filter"; // Generamos la query
            da1 = new SqlDataAdapter(query, cnn);
            da1.SelectCommand.Parameters.AddWithValue("@filter", string.Format("%{0}%", buscar));
            // Enviamos los parametros, el primero siendo el que se encuentra en la query
            // El segundo el formato que se le dara, usando REGEX, el Tercero el parametro que enviamos desde que el evento TextChanged le envio a este metodo

            DataTable table = new DataTable();
            da1.Fill(table); // Llenamos el DataTable

            {
                YouDataGridView.DataSource = table;
                YouDataGridView.ClearSelection();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
