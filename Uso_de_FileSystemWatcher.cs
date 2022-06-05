class SurroundingClass
{
    SurroundingClass()
    {
        FSW = new System.IO.FileSystemWatcher();
    }

    // Creamos un evento publico de la clase FileSystemWatcher
    private System.IO.FileSystemWatcher _FSW;

    public System.IO.FileSystemWatcher FSW
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            return _FSW;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            if (_FSW != null)
            {
                _FSW.Created -= FSW_Created;
                _FSW.Changed -= FSW_Changed;
                _FSW.Created -= FSW_Created;
                _FSW.Deleted -= FSW_Deleted;
                _FSW.Renamed -= FSW_Renamed;
            }

            _FSW = value;
            if (_FSW != null)
            {
                _FSW.Created += FSW_Created;
                _FSW.Changed += FSW_Changed;
                _FSW.Created += FSW_Created;
                _FSW.Deleted += FSW_Deleted;
                _FSW.Renamed += FSW_Renamed;
            }
        }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    Cursor.Current = Cursors.WaitCursor;
    Try()
    Cursor.Current = Cursors.Default;
    }

    public void Try()
    {
        FSW.Path = @"C:\Try"; // Le especificamos la ruta a vigilar
        FSW.IncludeSubdirectories = true; // Especificamos a true todos los subdirectorios de esa carpeta
        FSW.EnableRaisingEvents = true; // Habilitamos todos los eventos contenidos dentro de nuestra clase
    }

    // Eventos
    private void FSW_Created(object sender, System.IO.FileSystemEventArgs e)
    {
        Interaction.MsgBox("Se ha creado un nuevo fichero " + e.Name, MsgBoxStyle.Information);
    }

    private void FSW_Changed(object sender, System.IO.FileSystemEventArgs e)
    {
        Interaction.MsgBox("Se ha modificado un fichero " + e.Name, MsgBoxStyle.Information);
    }

    private void FSW_Created(object sender, System.IO.FileSystemEventArgs e)
    {
        Interaction.MsgBox("Se ha creado un fichero " + e.Name, MsgBoxStyle.Information);
    }

    private void FSW_Deleted(object sender, System.IO.FileSystemEventArgs e)
    {
        Interaction.MsgBox("Se ha eliminado un fichero " + e.Name, MsgBoxStyle.Information);
    }

    private void FSW_Renamed(object sender, System.IO.RenamedEventArgs e)
    {
        Interaction.MsgBox("Se ha Cambiado el nombre de un fichero de " + e.OldName + " a " + e.Name, MsgBoxStyle.Information);
    }
}