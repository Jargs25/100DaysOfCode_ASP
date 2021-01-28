using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace _100DaysOfCode_ASP
{
    public class modelo_productos : conexion
    {
        SqlDataAdapter adapter;
        SqlCommand com;

        public modelo_productos()
        {
            EstablecerConexion();
        }

        public int AgregarProducto(productos oProducto)
        {
            com = new SqlCommand();

            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "AGREGAR_PRODUCTO";
            com.Parameters.Add("@nombre", SqlDbType.VarChar).Value = oProducto.nombre;
            com.Parameters.Add("@cantidad", SqlDbType.Int).Value = oProducto.cantidad;
            com.Parameters.Add("@precio", SqlDbType.Decimal).Value = oProducto.precio;
            com.Parameters.Add("@rutaImagen", SqlDbType.VarChar).Value = oProducto.rutaImagen;
            com.Connection = conn;
            conn.Open();
            int resultado = com.ExecuteNonQuery();
            conn.Close();

            return resultado;
        }
        public int ModificarProducto(productos oProducto)
        {
            com = new SqlCommand();

            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "MODIFICAR_PRODUCTO";
            com.Parameters.Add("@id", SqlDbType.Int).Value = oProducto.id;
            com.Parameters.Add("@nombre", SqlDbType.VarChar).Value = oProducto.nombre;
            com.Parameters.Add("@cantidad", SqlDbType.Int).Value = oProducto.cantidad;
            com.Parameters.Add("@precio", SqlDbType.Decimal).Value = oProducto.precio;
            com.Parameters.Add("@rutaImagen", SqlDbType.VarChar).Value = oProducto.rutaImagen;
            com.Connection = conn;
            conn.Open();
            int resultado = com.ExecuteNonQuery();
            conn.Close();

            return resultado;
        }
        public int EliminarProducto(int id)
        {
            com = new SqlCommand();

            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "ELIMINAR_PRODUCTO";
            com.Parameters.Add("@id", SqlDbType.Int).Value = id;

            com.Connection = conn;
            conn.Open();
            int resultado = com.ExecuteNonQuery();
            conn.Close();

            return resultado;
        }
        public DataTable BuscarProductos(productos oProducto)
        {
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter();
            com = new SqlCommand();

            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "BUSCAR_PRODUCTOS";
            com.Parameters.Add("@codigo", SqlDbType.VarChar).Value = oProducto.codigo == null? "": oProducto.codigo;
            com.Parameters.Add("@nombre", SqlDbType.VarChar).Value = oProducto.nombre == null ? "": oProducto.nombre;
            com.Parameters.Add("@cantidad", SqlDbType.VarChar).Value = oProducto.cantidad > 0 ? oProducto.cantidad.ToString() : "";
            com.Parameters.Add("@precio", SqlDbType.VarChar).Value = oProducto.precio > 0 ? oProducto.precio.ToString() : "";
            com.Connection = conn;
            conn.Open();
            adapter.SelectCommand = com;
            adapter.Fill(dt);
            conn.Close();

            return dt;
        }


    }
}