using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Proyecto_2_HTML.Modelos;

namespace Proyecto_2_HTML.Logica
{
    public class ReparacionLogica
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["GestionDB"]?.ToString();

        public static List<Reparacion> Listar()
        {
            List<Reparacion> lista = new List<Reparacion>();
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultarReparaciones", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lista.Add(new Reparacion()
                        {
                            ReparacionID = Convert.ToInt32(dr["ReparacionID"]),
                            EquipoID = Convert.ToInt32(dr["EquipoID"]),
                            FechaSolicitud = Convert.ToDateTime(dr["FechaSolicitud"]),
                            Estado = dr["Estado"].ToString(),
                            // Esto viene del JOIN en SQL
                            ModeloEquipo = dr["Modelo"].ToString()
                        });
                    }
                }
                catch (Exception) { }
            }
            return lista;
        }

        public static bool Registrar(Reparacion obj)
        {
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_AgregarReparacion", oConexion);
                cmd.Parameters.AddWithValue("EquipoID", obj.EquipoID);
                cmd.Parameters.AddWithValue("Estado", obj.Estado);
                cmd.CommandType = CommandType.StoredProcedure;

                oConexion.Open();
                // Si falla aquí, el Controlador capturará el mensaje exacto
                int filas = cmd.ExecuteNonQuery();
                return filas > 0;
            }
        }


    }
}