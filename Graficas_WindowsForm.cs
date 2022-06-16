/*Llenaremos una grafica sencilla de windows forms a partir de una consulta a la DB, en el ejemplo sera a partir de un store procedure
en el repositorio de SQL podran encontrar dicho store procedure
*/

//Comenzamos creando arrays
ArrayList Categoria = New ArrayList();
ArrayList VentaTotal = New ArrayList();

Private void Graficando(){
    cmd = New SqlCommand("sp_Ventas", cnn);
    cmd.ComandType = ComandType.StoreProcedure;
    cnn.Open();
    dr = cmd.ExecuteReader();
    If (dr.HasRows()) {
    while (dr.Read())
    {
        Categoria.Add(dr.GetString(0));
        VentaTotal.Add(dr.GetInt32(1));
    }
    }
    dr.Close();
    cnn.Close();
    if (Categoria != Null && VentaTotal != Null)
    {
        Chart1.Series[0].Points.DataBindXY(Categoria, VentaTotal);
    } else
    {
        Chart1.Series[0].Points.DataBindXY(0,0);
    }
}
