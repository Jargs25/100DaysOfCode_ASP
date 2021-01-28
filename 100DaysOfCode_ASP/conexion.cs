using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace _100DaysOfCode_ASP
{
    public class conexion
    {
        protected SqlConnection conn;

        protected void EstablecerConexion()
        {
            string user = "TuUsuario";
            string pass = "TuContraseña*";
            string bd = "TuBaseDeDatos";
            string server = "TuServer";

            string sc = "User ID=" + user + ";Password=" + pass + ";Initial Catalog=" + bd + "; Server=" + server + ";";

            conn = new SqlConnection(sc);
        }
    }
}