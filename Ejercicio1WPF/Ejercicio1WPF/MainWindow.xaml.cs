using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ejercicio1WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Calculos calc;
        Conexion conn;
        string valor;
        int idValor;
        int idOper;

        public MainWindow()
        {

            this.calc = new Calculos();
            this.conn = new Conexion();
            this.valor = "";
            this.idValor = 1;
            this.idOper = 1;
            conn.AbrirBdd();
            conn.Consulta("DELETE FROM TValor");
            conn.Consulta("DELETE FROM TOperacion");
            conn.CerrarBdd();
            InitializeComponent();

        }

        /// <summary>
        /// Botones de numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Numeros_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            resultado.Text += b.Content;

            valor += b.Content;

        }

        /// <summary>
        /// Boton de igual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIgual_Click(object sender, RoutedEventArgs e)
        {
            calc.MostrarResultado(historico, resultado);
        }

        /// <summary>
        /// Botones de operaciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperacion_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if (resultado.Text != "")
            {
                string queryV = "INSERT INTO TValor VALUES (" + idValor + ", " +  Convert.ToInt32(valor)  + ")";
                conn.AbrirBdd();
                conn.Consulta(queryV);
                conn.CerrarBdd();
                idValor++;
                valor = "";

                string queryO = "INSERT INTO TOperacion VALUES (" + idOper + ", '" + b.Content + "')";
                conn.AbrirBdd();
                conn.Consulta(queryO);
                conn.CerrarBdd();
                resultado.Text += b.Content;
                historico.Text += resultado.Text;
                resultado.Text = "";
                idOper++;

                calc.IntroducirOperacion(conn);
            }
        }

        /// <summary>
        /// Boton resetear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

            historico.Text = "";
            resultado.Text = "";


            conn.AbrirBdd();
            string query = "DELETE FROM TValor";
            conn.Consulta(query);
            query = "DELETE FROM TOperacion";
            conn.Consulta(query);
            conn.CerrarBdd();


            calc.Resetear();
        }
    }
}
