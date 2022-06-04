private void btnCopyCWO_Click(object sender, EventArgs e)
{
    try
    {
        if (YouDataGridView.RowCount > 0)
        {
            // Usando la propiedad ClipboardCopyMode de la DataGridView, hacemos todo disponible en conjunto con los encabezados
            YouDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj; // Creamos un objeto
            YouDataGridView.SelectAll(); // Seleccionamos toda la DataGridView

            // Al objeto creado el agregamos lo que pusimos como disponible arriba, todo el contenido en el ClipBoard
            dataObj = YouDataGridView.GetClipboardContent();

            // Seteamos el metodo SetDataObject de la Clase Clipboard, la cual en su constructor ya viene la informacion de la DataGridView
            Clipboard.SetDataObject(dataObj);
            YouDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            Interaction.MsgBox("Added Grid to ClipBoard!");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Error Added To ClipBoard");
    }
}
