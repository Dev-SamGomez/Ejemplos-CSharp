class SurroundingClass
{
    private void EnviaCorreo()
    {
        try
        {
            string FromTO = "mailTo@example.com"; // Marcamos el correo destino
            string SendBy = "no-reply@example.com"; // Marcamos el correo que envia
            string Date = DateTime.Now.ToString("dd-MMM-yyyy");
            string FileAttach = CreaCSV(tabla); // Invocamos metodo que crea el archivo excel
            // Cuerpo del correo
            string SendMail = "Hello, this is an email sent from a desktop program in C# language, attaching an excel.";

            // Declaramos la clase MailMessage
            System.Net.Mail.MailMessage _Message = new System.Net.Mail.MailMessage();
            // Declaramos la clase SmtpClient
            System.Net.Mail.SmtpClient _SMTP = new System.Net.Mail.SmtpClient();

            // En la clase SMTP, con el metodo Credentials, creamos una nueva clase de NetorkCredential
            // en esta le enviaremos en el construnctor, el correo que envia y su contraseña
            _SMTP.Credentials = new System.Net.NetworkCredential(SendBy, "YourPassword");
            // Marcamos el Host, OJO: este deberas poner el host de tu provedor de correo
            _SMTP.Host = "smtp.example.com";
            // Seleccionas el puerto y habilitas el envio con certificado SSL
            _SMTP.Port = 587;
            _SMTP.EnableSsl = true;

            // En el metodo de la clase que declaramos _Message, en su constructor, seleccionamos el correo destino
            _Message.To.Add(FromTO);
            _Message.From = new System.Net.Mail.MailAddress(SendBy, "", System.Text.Encoding.UTF8);
            _Message.Subject = "No Reply"; // Seleccionamos el Subject
            _Message.SubjectEncoding = System.Text.Encoding.UTF8;

            _Message.Body = SendMail; // Colocamos cuerpo del correo
            _Message.BodyEncoding = System.Text.Encoding.UTF8;
            _Message.Priority = System.Net.Mail.MailPriority.High;
            if (FileAttach != "")
            {
                System.Net.Mail.Attachment oAttachment = new System.Net.Mail.Attachment(FileAttach);
                _Message.Attachments.Add(oAttachment);
            }
            _Message.IsBodyHtml = false;
            // ENVIO
            _SMTP.Send(_Message); // Enviamos
        }
        catch (Exception ex)
        {
            Interaction.MsgBox(ex.Message.ToString());
        }
    }
    private void CreaCSV(DataTable aTabla)
    {
        DateTime DateNow = (DateTime)DateTime.Now;
        string NameFileCreated = "name of the file created on the day " + Convert.ToDateTime(DateNow.ToString()); // Colocamos el nombre del archivo
        string SetPath = Path.GetTempPath() + ArchivoNombre; // Creamos el archivo temporal
        if (aTabla.Rows.Count > 0)
        {
            try
            {
                string AUX = "";
                FileStream fs = File.Create(SetPath); // Enviamos la direccion temporal a la clase FileStream
                string HeadersText = "Aqui los encabezados del DataTable" + Constants.vbNewLine;
                byte[] Titles = new UTF8Encoding(true).GetBytes(HeadersText); // Creamos el array con los encabezados del DataTable
                fs.Write(Titles, 0, Titles.Length);
                HeadersText = "";
                for (int NM = 0; NM <= aTabla.Rows.Count - 1; NM++)
                {
                    AUX = aTabla.Rows(NM).Item("Title").ToString.Trim(); // Limpiamos los encabezados
                    HeadersText += AUX + ",";
                    HeadersText += Constants.vbNewLine;
                    byte[] info = new UTF8Encoding(true).GetBytes(HeadersText);
                    fs.Write(info, 0, info.Length); // y los escribimos en el documento
                }
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + Constants.vbNewLine + "Error in CreaCSV", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        if (SetPath != "")
            return SetPath;// Retornamos el archivo temporal
        return null;
    }
}