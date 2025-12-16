using Proyecto_2_HTML.Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace Proyecto_2_HTML.Logica
{
    public class UsuarioLogica
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["GestionDB"]?.ToString();

        // MÉTODO LISTAR (READ)
        public static List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            if (cadena == null) return lista;

            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultarUsuarios", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lista.Add(new Usuario()
                        {
                            UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                            Nombre = dr["Nombre"].ToString(),
                            CorreoElectronico = dr["CorreoElectronico"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            // Asegúrate de que tu tabla tenga Rol, si no, borra esta línea
                            Rol = dr["Rol"].ToString()
                        });
                    }
                }
                catch (Exception) { return lista; }
            }
            return lista;
        }

        // MÉTODO REGISTRAR (CREATE) - VERSIÓN CORREGIDA CON CLAVE
        public static bool Registrar(Usuario obj)
        {
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_AgregarUsuario", oConexion);
                cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("Correo", obj.CorreoElectronico);
                cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                cmd.Parameters.AddWithValue("Clave", obj.Clave); // Aquí está la clave
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
                catch (Exception) { return false; }
            }
        }

        // MÉTODO VALIDAR (LOGIN) - VERSIÓN CORREGIDA CON CLAVE
        public static bool Validar(string correo, string clave)
        {
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarLogin", oConexion);
                cmd.Parameters.AddWithValue("Correo", correo);
                cmd.Parameters.AddWithValue("Clave", clave);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int contar = Convert.ToInt32(cmd.ExecuteScalar());
                    return contar > 0;
                }
                catch (Exception) { return false; }
            }
        }

        // MÉTODO OBTENER ROL
        public static string ObtenerRol(string correo)
        {
            string rol = "usuario";
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerRol", oConexion);
                cmd.Parameters.AddWithValue("Correo", correo);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    object resultado = cmd.ExecuteScalar();
                    if (resultado != null)
                    {
                        rol = resultado.ToString();
                    }
                }
                catch (Exception) { rol = "usuario"; }
            }
            return rol;
        }

        // MÉTODO MODIFICAR
        public static bool Modificar(Usuario obj)
        {
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ModificarUsuario", oConexion);
                cmd.Parameters.AddWithValue("UsuarioID", obj.UsuarioID);
                cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("Correo", obj.CorreoElectronico);
                cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { return false; }
            }
        }

        // MÉTODO ELIMINAR
        public static bool Eliminar(int id)
        {
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_BorrarUsuario", oConexion);
                cmd.Parameters.AddWithValue("UsuarioID", id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { return false; }
            }
        }
    }
}