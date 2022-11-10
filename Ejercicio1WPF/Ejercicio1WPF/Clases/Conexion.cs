using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace Ejercicio1WPF
{
    class Conexion
    {
        SqlConnection conn;

        public Conexion()
        {
            this.conn = new SqlConnection("Data Source=LAPTOP-812783H7;Initial Catalog=Calculadora;Integrated Security=True");
        }

        public void AbrirBdd()
        {
            this.conn.Open();
        }

        public void CerrarBdd()
        {
            this.conn.Close();
        }

        public void Consulta(string query)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = query;
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string DarDato(string query)
        {
            string dato = "";

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = query;
                command.Connection = conn;
                dato = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dato;
        }
    }
}
