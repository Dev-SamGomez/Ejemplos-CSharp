private void EnviaCorreo()
{
    try
    {
        string FromTO = "mailTo@example.com"; // Marcamos el correo destino
        string SendBy = "no-reply@example.com"; // Marcamos el correo que envia
                                                // Cuerpo del correo
        string SendMail = "Hello, this is an email sent from a desktop program in C# language.";

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

        _Message.IsBodyHtml = false;
        // ENVIO
        _SMTP.Send(_Message); // Enviamos
    }
    catch (Exception ex)
    {
        Interaction.MsgBox(ex.Message.ToString());
    }
}
