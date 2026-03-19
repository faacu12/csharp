using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class LOGIN : Form
    {
        BLL.UsuarioService service = new BLL.UsuarioService();
        public LOGIN()
        {
            InitializeComponent();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CREATE sign = new CREATE();
            sign.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool valida = false;
            string user = textBox1.Text;
            string password = textBox2.Text;
            string hashp = service.TraerHash(user);
            if (hashp != null)
            {
                valida = service.VerifyPassword(password,hashp);
            }
            if (valida)
            {
                Form1 form = new Form1();
                form.Show();
            }
            else
            {
                MessageBox.Show("Contraseña Incorrecta! Ingrese de nuevo");
                textBox2.Clear();
            }
        }
    }
}
