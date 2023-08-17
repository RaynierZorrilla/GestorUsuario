using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Capa_Entidad;

namespace Capa_Datos
{
    public class ClassDatos
    {
        public static string cn = "server=LAPTOP-UFFK3NJM;integrated security=true;database=Pracfinal";
        public SqlConnection conectarbd = new SqlConnection();

        public ClassDatos()
        {
            conectarbd.ConnectionString = cn;
        }


       

       
    }
}
