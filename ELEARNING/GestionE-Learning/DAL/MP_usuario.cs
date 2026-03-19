using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_usuario : MAPPER<BE.Usuario>
    {
        internal Acceso acceso;
        public override int Agregar(Usuario obj)
        {
            acceso = new Acceso();
            acceso.Abrir();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(acceso.CrearParameter("@nombre", obj.Nombre));
            parameters.Add(acceso.CrearParameter("@apellido", obj.Apellido));
            parameters.Add(acceso.CrearParameter("@user", obj.User));
            parameters.Add(acceso.CrearParameter("@pass", obj.PasswordHash));
            parameters.Add(acceso.CrearParameter("@rol", obj.Rol));
            int res = acceso.Escribir("INSERTARUSUARIO",parameters);
            acceso.Cerrar();
            return res;
        }

        public override int Borrar(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public override List<Usuario> Listar()
        {
            throw new NotImplementedException();
        }

        public override int Modificar(Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}
