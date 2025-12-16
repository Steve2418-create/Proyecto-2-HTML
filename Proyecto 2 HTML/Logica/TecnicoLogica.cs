using Proyecto_2_HTML.Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_2_HTML.Logica
{
    public class TecnicosLogica
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["GestionDB"]?.ToString();

        // LEER (Listar Técnicos)
        public static List<Tecnico> Listar()
        {
            List<Tecnico> lista = new List<Tecnico>();
            if (cadena == null) return lista;

            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultarTecnicos", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lista.Add(new Tecnico()
                        {
                            TecnicoID = Convert.ToInt32(dr["TecnicoID"]),
                            Nombre = dr["Nombre"].ToString(),
                            Especialidad = dr["Especialidad"].ToString()
                        });
                    }
                }
                catch (Exception) { return lista; }
            }
            return lista;
        }

        // CREAR
        public static bool Registrar(Tecnico obj)
        {
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_AgregarTecnico", oConexion);
                cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("Especialidad", obj.Especialidad);
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

        // MODIFICAR
        public static bool Modificar(Tecnico obj)
        {
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ModificarTecnico", oConexion);
                cmd.Parameters.AddWithValue("TecnicoID", obj.TecnicoID);
                cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("Especialidad", obj.Especialidad);
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

        // ELIMINAR
        public static bool Eliminar(int id)
        {
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_BorrarTecnico", oConexion);
                cmd.Parameters.AddWithValue("TecnicoID", id);
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
    }
}