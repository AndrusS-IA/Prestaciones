using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoPrestaciones
{
    public partial class Form1 : Form
    {

        private Button botonImprimir = new Button();

        private PrintDocument printDocument1 = new PrintDocument();

        public double SueldoMensual;
        public int diario = 30;
        public double SueldoReal;
        public double SueldoDiario;
        public double SueldoDiarioReal;
        public int tiempo;
        public double cesantia;
        public int diasC;
        public int diasC2;
        public double preaviso;
        public int diasA;
        public int diasA2;
        public double subtotal;
        public double dvacaciones;
        public double valorV;
        public double TotalPagar;
        public int tiempo13;
        public int tiempo14;
        public double aguinaldo;
        public double catorceavo;

        public Form1()
        {
            InitializeComponent();
            //Inicializa la funcion de imprimir el formulario
            botonImprimir.Text = "Imprimir Formulario";

            botonImprimir.Click += new EventHandler(printButton_Click);

            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Calculo de Sueldos Mensuales y Diarios

            SueldoMensual = double.Parse(textBoxSueldo.Text);

            SueldoDiario = SueldoMensual / diario;
            SueldoReal = (SueldoMensual * 14) / 12;
            SueldoDiarioReal = SueldoReal / diario;

            textBoxDiario.Text = SueldoDiario.ToString("0,0.00");
            textBoxSueldoReal.Text = SueldoReal.ToString("0,0.00");
            textBoxDiarioReal.Text = SueldoDiarioReal.ToString("0,0.00");

            //Objeto para calculo de Fechas
            String Str = Diferencia(dateTimePicker2.Value, dateTimePicker1.Value);
            label1.Text = Str;

            //Calculo de Tiempo para Cesantia, Preaviso
            DateTime fechaInicio = dateTimePicker1.Value.Date;
            DateTime fechaFinal = dateTimePicker2.Value.Date;
            TimeSpan tspan = fechaFinal - fechaInicio;
            tiempo = tspan.Days;

            if (comboBox1.Text == "Empleador")
            {
            //Calculo de Cesantia
            if (tiempo >= 90 && tiempo <= 180)
            {
                    diasC = 10;
                    cesantia = diasC * SueldoDiarioReal;
                    textBoxDiasC.Text = diasC.ToString();
                    textBoxValorC.Text = cesantia.ToString("0,0.00");
                }
              else if (tiempo >= 210 && tiempo <= 359)
                {
                  diasC = 20;
                  cesantia = diasC * SueldoDiarioReal;
                  textBoxDiasC.Text = diasC.ToString();
                  textBoxValorC.Text = cesantia.ToString("0,0.00");
                 }

               else if (tiempo >= 360 && tiempo <= 99075)
                {
                 diasC2 = tiempo/360;
                diasC = diasC2 * 30;
                 cesantia = diasC * SueldoDiarioReal;
                 textBoxDiasC.Text = diasC.ToString();
                 textBoxValorC.Text = cesantia.ToString("0,0.00");
               }

            //Calculo de Preaviso
            if (tiempo <= 89)
            {
                diasA = 1;
                preaviso = diasA * SueldoDiarioReal;
                textBoxPreA.Text = diasA.ToString();
                textBoxValorP.Text = preaviso.ToString("0,0.00");
            }
            else if (tiempo >= 90 && tiempo <= 180)
            {
                diasA = 7;
                preaviso = diasA * SueldoDiarioReal;
                textBoxPreA.Text = diasA.ToString();
                textBoxValorP.Text = preaviso.ToString("0,0.00");
            }
            else if (tiempo >= 210 && tiempo <= 359)
            {
                diasA = 14;
                preaviso = diasA * SueldoDiarioReal;
                textBoxPreA.Text = diasA.ToString();
                textBoxValorP.Text = preaviso.ToString("0,0.00");
            }

            else if (tiempo >= 360 && tiempo <= 720)
            {
                diasA = 30;
                preaviso = diasA * SueldoDiarioReal;
                textBoxPreA.Text = diasA.ToString();
                textBoxValorP.Text = preaviso.ToString("0,0.00");
            }

            else if (tiempo > 720)
            {
                diasA = 60;
                preaviso = diasA * SueldoDiarioReal;
                textBoxPreA.Text = diasA.ToString();
                textBoxValorP.Text = preaviso.ToString("0,0.00");
            }

            //Subtotal Cesantia + Preaviso
            subtotal = preaviso + cesantia;
            textBoxSubT.Text = subtotal.ToString("0,0.00");

            //Calculo de Vacaciones
            dvacaciones = double.Parse(textBoxDiasV.Text);

            valorV = dvacaciones * SueldoDiarioReal;
            textBoxValorV.Text = valorV.ToString("0,0.00");

            //Calculo de Aguinaldo y Catorceavo
            DateTime fechaInicio2 = new DateTime(2019,1,1);
            DateTime fechaFinal2 = dateTimePicker2.Value.Date;
            TimeSpan tspan2 = fechaFinal2 - fechaInicio2;
            tiempo13 = tspan2.Days;

            aguinaldo = tiempo13 * (SueldoMensual / 360);
            textBoxAguinaldo.Text = tiempo13.ToString();
            textBoxPaguinaldo.Text = aguinaldo.ToString("0,0.00");

            DateTime fechaInicio3 = new DateTime(2019,7,1);
            DateTime fechaFinal3 = dateTimePicker2.Value.Date;
            TimeSpan tspan3 = fechaFinal3 - fechaInicio3;
            tiempo14 = tspan3.Days;

            catorceavo = tiempo14 * (SueldoMensual / 360);
            textBoxCatorceavo.Text = tiempo14.ToString();
            textBoxPCatorceavo.Text = catorceavo.ToString("0,0.00");



            //Total a Pagar
            TotalPagar = valorV + subtotal + aguinaldo + catorceavo;
            textBoxTotalPagar.Text = TotalPagar.ToString("0,0.00");
            }

            else //Modo Trabajador
            {
                //Calculo de Preaviso
                if (tiempo <= 89)
                {
                    diasA = 1;
                    preaviso = diasA * SueldoDiarioReal;
                    textBoxPreA.Text = diasA.ToString();
                    textBoxValorP.Text = preaviso.ToString("0,0.00");
                }
                else if (tiempo >= 90 && tiempo <= 180)
                {
                    diasA = 7;
                    preaviso = diasA * SueldoDiarioReal;
                    textBoxPreA.Text = diasA.ToString();
                    textBoxValorP.Text = preaviso.ToString("0,0.00");
                }
                else if (tiempo >= 210 && tiempo <= 359)
                {
                    diasA = 14;
                    preaviso = diasA * SueldoDiarioReal;
                    textBoxPreA.Text = diasA.ToString();
                    textBoxValorP.Text = preaviso.ToString("0,0.00");
                }

                else if (tiempo >= 360 && tiempo <= 720)
                {
                    diasA = 30;
                    preaviso = diasA * SueldoDiarioReal;
                    textBoxPreA.Text = diasA.ToString();
                    textBoxValorP.Text = preaviso.ToString("0,0.00");
                }

                else if (tiempo > 720)
                {
                    diasA = 60;
                    preaviso = diasA * SueldoDiarioReal;
                    textBoxPreA.Text = diasA.ToString();
                    textBoxValorP.Text = preaviso.ToString("0,0.00");
                }

                //Subtotal  Preaviso
                subtotal = preaviso;
                textBoxSubT.Text = subtotal.ToString("0,0.00");

                //Calculo de Vacaciones
                dvacaciones = double.Parse(textBoxDiasV.Text);

                valorV = dvacaciones * SueldoDiarioReal;
                textBoxValorV.Text = valorV.ToString("0,0.00");

                //Calculo de Aguinaldo y Catorceavo
                DateTime fechaInicio2 = new DateTime(2019, 1, 1);
                DateTime fechaFinal2 = dateTimePicker2.Value.Date;
                TimeSpan tspan2 = fechaFinal2 - fechaInicio2;
                tiempo13 = tspan2.Days;

                aguinaldo = tiempo13 * (SueldoMensual / 360);
                textBoxAguinaldo.Text = tiempo13.ToString();
                textBoxPaguinaldo.Text = aguinaldo.ToString("0,0.00");

                DateTime fechaInicio3 = new DateTime(2019, 7, 1);
                DateTime fechaFinal3 = dateTimePicker2.Value.Date;
                TimeSpan tspan3 = fechaFinal3 - fechaInicio3;
                tiempo14 = tspan3.Days;

                catorceavo = tiempo14 * (SueldoMensual / 360);
                textBoxCatorceavo.Text = tiempo14.ToString();
                textBoxPCatorceavo.Text = catorceavo.ToString("0,0.00");



                //Total a Pagar
                TotalPagar = valorV + subtotal + aguinaldo + catorceavo;
                textBoxTotalPagar.Text = TotalPagar.ToString("0,0.00");

            }
        }

        //Calculo de Fechas (Años, Meses y Dias)
        public String Diferencia(DateTime newdt, DateTime olddt)
        {
            int anos;
            int meses;
            int dias;
            string str = "";

            anos = (newdt.Year - olddt.Year);
            meses = (newdt.Month - olddt.Month);
            dias = (newdt.Day - olddt.Day);

            if (meses < 0)
            {
                anos -= 1;
                meses += 12;
            }

            if (dias < 0)
            {
                meses -= 1;
                int DiasAno = newdt.Year;
                int DiasMes = newdt.Month;

                if ((newdt.Month - 1) == 0)
                {
                    DiasAno = DiasAno - 1;
                    DiasMes = 12;
                }
                else
                {
                    DiasMes = DiasMes - 1;
                }

                dias += DateTime.DaysInMonth(DiasAno, DiasMes);
            }

            if (anos < 0)
            {
                return "La fecha inicial es mayor a la fecha final";
            }

            if (anos > 0)
            {
                if (anos == 1)
                    textBoxAnos.Text = anos.ToString();
                else
                    textBoxAnos.Text = anos.ToString();

            }

            if (meses > 0)
            {
                if (meses == 1)
                    textBoxMeses.Text = meses.ToString();
                else
                    textBoxMeses.Text = meses.ToString();

            }

            if (dias > 0)
            {
                if (dias == 1)
                    textBoxDias.Text = dias.ToString();
                else
                    textBoxDias.Text = dias.ToString();

          
            }

            return str;
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }
        //Resetear el programa
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        //Boton y codigo para imprimir pantalla
        private void printButton_Click(object sender, EventArgs e)
        {
            CapturarPantalla();

            printDocument1.Print();
        }
        Bitmap imagen;
        //Capturar Pantalla
        private void CapturarPantalla()
        {
            Graphics g = this.CreateGraphics();
            Size s = this.Size;
            imagen = new Bitmap(s.Width, s.Height, g);
            Graphics g2 = Graphics.FromImage(imagen);
            g2.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }
        //Imprimir el documento
        private void printDocument1_PrintPage(System.Object sender,
               System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(imagen, 0, 0);
        }
    }
    }


