using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace email_sender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label6.Text = "Caution: Email providers may ban or terminate your \naccount if your account is seen as being used for \nspam.";
        }

        MailMessage message = new MailMessage();  
        SmtpClient client = new SmtpClient();
        Timer timer = new Timer();
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                message.From = new MailAddress(textBox4.Text);
                message.Subject = textBox2.Text;
                message.Body = textBox3.Text;
                if(textBox6.Text != "")
                    message.Attachments.Add(new Attachment(textBox6.Text));

                foreach (string s in textBox1.Text.Split(';'))
                    message.To.Add(s);

                client.Credentials = new NetworkCredential(textBox4.Text, textBox5.Text);

                if (textBox4.Text.Substring(textBox4.Text.IndexOf('@')) == "@gmail.com")
                {
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;
                }

                if (textBox4.Text.Substring(textBox4.Text.IndexOf('@')) == "@yahoo.com")
                {
                    client.Host = "smtp.mail.yahoo.com";
                    client.Port = 587;
                    client.EnableSsl = true;
                }

                if (textBox4.Text.Substring(textBox4.Text.IndexOf('@')) == "@hotmail.com" 
                    || textBox4.Text.Substring(textBox4.Text.IndexOf('@')) == "@live.com"
                    || textBox4.Text.Substring(textBox4.Text.IndexOf('@')) == "@outlook.com")
                {                                           
                    client.Host = "smtp.live.com";
                    client.Port = 587;
                    client.EnableSsl = true;
                }

                progressBar1.Maximum = (int)numericUpDown1.Value;
                progressBar1.Value = 0;
                int counter = 0;

                while (counter < numericUpDown1.Value)
                {
                    timer1.Start();
                    client.Send(message);
                    counter++;
                    timer1.Stop();
                    progressBar1.Value = counter;
                }

            }

            catch
            {
                MessageBox.Show("There was an error. Make sure you have typed in the correct info and that you have a working internet connection.", "Error");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox6.Text = ofd.FileName;
            }
        }
    }
}
