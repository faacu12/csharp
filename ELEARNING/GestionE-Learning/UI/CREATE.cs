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
    public partial class CREATE : Form
    {
        BLL.UsuarioService service = new BLL.UsuarioService();
        public CREATE()
        {
            InitializeComponent();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LOGIN log = new LOGIN();
            this.Hide();
            log.Show();
        }

     

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            BE.Usuario u = new BE.Usuario();
            u.Nombre = txtNombre.Text;
            u.Apellido = txtApellido.Text;
            u.User = txtUser.Text;
            string hashp = service.HashPassword(txtPassword.Text);
            u.PasswordHash = hashp;
            u.Activo = 1;
            u.Rol = txtRol.Text;
            int res = service.Insertar(u);
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}
