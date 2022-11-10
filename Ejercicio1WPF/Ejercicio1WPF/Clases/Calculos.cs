using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ejercicio1WPF
{
    class Calculos
    {

        private decimal resultado;
        private List<int> listaValores;
        private List<string> listaOperaciones;
        private int idNum;
        private int idOper;

        public Calculos()
        {
            this.idNum = 1;
            this.idOper = 1;
            this.resultado = 0;

            this.listaValores = new List<int>();
            this.listaOperaciones = new List<string>();
        }

        public void IntroducirOperacion(Conexion conn)
        {
            conn.AbrirBdd();
            listaValores.Add(Convert.ToInt32(conn.DarDato("SELECT nNum FROM TValor WHERE nNumID = " + idNum)));
            idNum++;

            listaOperaciones.Add(conn.DarDato("SELECT cOperacion FROM TOperacion WHERE nOperID = " + idOper));
            idOper++;
            conn.CerrarBdd();
        }

        public void Resetear()
        {

            listaOperaciones.Clear();
            listaValores.Clear();
        }

        public void MostrarResultado(TextBlock textH, TextBlock textR)
        {
            listaValores.Add(Convert.ToInt32(textR.Text));
            int contOper = 0;
            resultado = listaValores[0];

            for (int i = 1; i < listaValores.Count; i++)
            {
                switch (listaOperaciones[contOper])
                {
                    case "+":
                        resultado = resultado + listaValores[i];
                        break;
                    case "-":
                        resultado = resultado - listaValores[i];
                        break;
                    case "*":
                        resultado = resultado * listaValores[i];
                        break;
                    case "/":
                        resultado = resultado / listaValores[i];
                        break;
                }
                contOper++;
            }

            textH.Text = "";
            textR.Text = Math.Round(resultado, 3).ToString();
            listaOperaciones.Clear();
            listaValores.Clear();
        }
    }
}
