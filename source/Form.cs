using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace SSB
{
    public partial class Form : System.Windows.Forms.Form
    {
        private bool dragging = false;
        private Point start = new Point();
        public Form()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            var url = textBox1.Text;
            if(url == "")
            {
                label3.Text = "Escreva um link!";
                return;
            }
            if (!url.StartsWith("https://www.roblox.com/games/"))
            {
                label3.Text = "Escreva um link válido!";
                return;
            }
            var web = new HtmlAgilityPack.HtmlWeb();
            var doc = web.Load(url);
            var innerHtml = doc.DocumentNode.InnerHtml;
            string[] link = { "https://tr.rbxcdn.com/" , "/Image/Jpeg" };
            string[] links = innerHtml.Split(link, System.StringSplitOptions.RemoveEmptyEntries);
            if(links.Length <= 1)
            {
                label3.Text = "Não foi possível acessar o link!";
            }
            else
            {
                var image_url = "https://tr.rbxcdn.com/" + links[1] + "/Image/Jpeg";
                WebRequest req = WebRequest.Create(image_url);
                WebResponse res = req.GetResponse();
                Stream imgStream = res.GetResponseStream();
                Image thumb = Image.FromStream(imgStream);
                imgStream.Close();
                pictureBox6.Image = thumb;
                pictureBox6.Visible = true;
                string[] title = { "<title>", "</title>" };
                string[] titulo = innerHtml.Split(title, System.StringSplitOptions.RemoveEmptyEntries);
                var titulo2 = titulo[1];
                var titulo_text = "";
                int i = 0;
                while (i != titulo2.Length - 9)
                {
                    if (titulo2[i] == '&' && titulo2[i + 1] == '#' && Char.IsNumber(titulo2[i + 2]) && Char.IsNumber(titulo2[i + 3]) && Char.IsNumber(titulo2[i + 4]) && Char.IsNumber(titulo2[i + 5]) && Char.IsNumber(titulo2[i + 6]) && Char.IsNumber(titulo2[i + 7]))
                    {
                        i += 9;
                    }
                    else
                    {
                        titulo_text += titulo2[i];
                        i++;
                    }
                }
                label3.Text = titulo_text;
                label4.Text = titulo_text;
                label5.Text = url;
                pictureBox7.Visible = true;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            start = new Point(e.X, e.Y);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point x = PointToScreen(e.Location);
                Location = new Point(x.X - this.start.X, x.Y - this.start.Y);
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            using (StreamWriter writer = new StreamWriter(deskDir + "\\" + label4.Text + ".url"))
            {
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=" + label5.Text);
                writer.WriteLine("IconIndex=0");
                writer.WriteLine("IconFile=" + System.Environment.CurrentDirectory + "\\logo.ico");
            }
            textBox1.Text = "";
            textBox1.Visible = false;
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Visible = true;
            label7.Text = deskDir;
            label7.Visible = true;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = true;
            pictureBox9.Visible = true;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            label6.Visible = false;
            label7.Visible = false;
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
            pictureBox8.Visible = false;
            pictureBox9.Visible = false;
        }
    }
}
