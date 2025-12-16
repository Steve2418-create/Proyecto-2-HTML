using Proyecto_2_HTML.Modelos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


    public class EquiposLogica
    {
        // Cadena de conexión
        private static string cadena = ConfigurationManager.ConnectionStrings["GestionDB"]?.ToString();

        // 1. MÉTODO LISTAR (LEER)
        public static List<Equipo> Listar()
        {
            List<Equipo> lista = new List<Equipo>();
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                // Usamos consulta directa para no complicarnos con SPs de lectura
                SqlCommand cmd = new SqlCommand("SELECT * FROM Equipos", oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lista.Add(new Equipo()
                        {
                            EquipoID = Convert.ToInt32(dr["EquipoID"]),
                            TipoEquipo = dr["TipoEquipo"].ToString(),
                            Modelo = dr["Modelo"].ToString(),
                            UsuarioID = Convert.ToInt32(dr["UsuarioID"])
                        });
                    }
                }
                catch (Exception) { }
            }
            return lista;
        }

        // 2. MÉTODO REGISTRAR (CREAR) -> ESTE FALTABA
        public static bool Registrar(Equipo obj)
        {
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_AgregarEquipo", oConexion);
                cmd.Parameters.AddWithValue("TipoEquipo", obj.TipoEquipo);
                cmd.Parameters.AddWithValue("Modelo", obj.Modelo);
                cmd.Parameters.AddWithValue("UsuarioID", obj.UsuarioID);
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

        // 3. MÉTODO MODIFICAR (EDITAR) -> ESTE TAMBIÉN FALTABA
        public static bool Modificar(Equipo obj)
        {
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ModificarEquipo", oConexion);
                cmd.Parameters.AddWithValue("EquipoID", obj.EquipoID);
                cmd.Parameters.AddWithValue("TipoEquipo", obj.TipoEquipo);
                cmd.Parameters.AddWithValue("Modelo", obj.Modelo);
                cmd.Parameters.AddWithValue("UsuarioID", obj.UsuarioID);
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

        // 4. MÉTODO ELIMINAR (BORRAR)
        public static bool Eliminar(int id)
        {
            using (SqlConnection oConexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_BorrarEquipo", oConexion);
                cmd.Parameters.AddWithValue("EquipoID", id);
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
