using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Memory
{
	public partial class MainPage : ContentPage
	{
        private string[] dominio = {"A","B","C","D","E",
                                    "F","G","H","I","J",
                                    "K","L","M","N","O",
                                    "P","Q","R","S","T",
                                    "U","V","W","X","Y","Z"};//26
        private string[] ambito = {"00","01","02","03","04",
                                   "10","11","12","13","14",
                                   "20","21","22","23","24",
                                   "30","31","32","33","34",
                                   "40","41","42","43","44" };
        private Dictionary<string, string> juego;
        private string anteriorKey;
        private string anteriorValue;
        private int puntaje;
        public MainPage()
        {
            InitializeComponent();
            start();
        }
        public void start()
        {
            HashSet<int> letrasRandoms = new HashSet<int>();
            HashSet<int> posicionesRandoms = new HashSet<int>();
            Random rad = new Random();

            for (; letrasRandoms.Count < 12;)
            {
                letrasRandoms.Add(rad.Next(0, 25));
            }
            for (; posicionesRandoms.Count < 25;)
            {
                posicionesRandoms.Add(rad.Next(0, 25));
            }

            puntaje = 0;
            anteriorKey = "";
            anteriorValue = "";
            juego = new Dictionary<string, string>();
            for (int i = 0, j = 0; i < 24; i++, j++)
            {
                if (j == 12) j = 0;
                juego.Add(ambito[posicionesRandoms.ElementAt(i)], dominio[letrasRandoms.ElementAt(j)]);
            }

            for (int i = 0; i<ambito.Length;i++) {
                this.FindByName<Button>("b" + ambito[i]).BackgroundColor = Color.FromHex("#959494");//gris
                this.FindByName<Button>("b" + ambito[i]).Text = "";
            }

        }
        void restart(object sender, EventArgs e) {
            start();
            this.FindByName<Button>("reiniciar").IsVisible = false;
            etiqueta.Text = "inicio";
        }

        void buttonHandler(object sender, EventArgs e)
        {

            if (juego.Count == 0)
            {
                etiqueta.Text = "Fin del Juego su puntaje es: "+puntaje;
                this.FindByName<Button>("reiniciar").IsVisible = true;
                return;
            }

            var b = sender as Button;
            var posicion = b.AutomationId as string;

            if (juego.ContainsKey(posicion))
            {

                b.Text = juego[posicion];
                b.BackgroundColor = Color.FromHex("#FFED02");//amarillo
                
                
                if (anteriorValue == "") {
                    anteriorKey = posicion;
                    anteriorValue = juego[posicion];
                }
                else if (anteriorValue == juego[posicion] && anteriorKey != posicion)
                {//si hay un par
                    juego.Remove(anteriorKey);
                    juego.Remove(posicion);
                    anteriorValue = "";
                    anteriorKey = "";
                    puntaje++;
                    etiqueta.Text ="pts: "+ puntaje.ToString();
                }
                else if (anteriorValue != juego[posicion])
                {//si el par no coinside
                    
                    this.FindByName<Button>("b" + anteriorKey).BackgroundColor = Color.FromHex("#959494");//gris
                    this.FindByName<Button>("b" + anteriorKey).Text = "";
                    //b.BackgroundColor = Color.FromHex("#959494");//gris
                    //b.Text = "";
                    anteriorKey = posicion;
                    anteriorValue = juego[posicion];
                    puntaje--;
                    etiqueta.Text = "pts: " + puntaje.ToString();
                }
                
            }
        }

    }
}
