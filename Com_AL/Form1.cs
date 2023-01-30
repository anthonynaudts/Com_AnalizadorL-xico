using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Com_AL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PalabrasReservadas PalabrasReservadas = new PalabrasReservadas();
        public void Analizar()
        {
            string Codigo = txtCodigo.Text;

            char[] salto = { '\n' };
            char[] limitador = { ' ' };

            string[] ArrayCodigo = Codigo.Split(salto);

            for(int i = 0; i < ArrayCodigo.Length; i++)
            {
                string[] palabra = ArrayCodigo[i].Split(limitador);
                
                for(int j = 0; j < palabra.Length; j++)
                {
                    palabra[j] = palabra[j].Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");

                    if (palabra[j] != "")
                    {
                        DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();

                        row.Cells[0].Value = PalabrasReservadas.AnalizarPR(palabra[j]);
                        row.Cells[1].Value = palabra[j];
                        row.Cells[2].Value = j + 1;
                        row.Cells[3].Value = i + 1;
                        dataGridView1.Rows.Add(row);
                    }
                }
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            limpiar();
            Analizar();
        }

        public void limpiar()
        {
            dataGridView1.Rows.Clear();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
            txtCodigo.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivos txt|*.txt";
            openFileDialog1.FileName = "Seleccione un archivo de Texto";
            openFileDialog1.Title = "Programa de Lectura";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader fp;
                string filename = openFileDialog1.FileName;
                fp = new StreamReader(filename);
                System.IO.StreamReader sr = new System.IO.StreamReader(filename, System.Text.Encoding.Default);
                string texto = sr.ReadToEnd();
                sr.Close();
                txtCodigo.Text = texto;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Ficheros TXT|*.txt";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter escrito = File.CreateText(saveFile.FileName);
                String contenido = txtCodigo.Text;
                escrito.Write(contenido.ToString());
                escrito.Flush();
                escrito.Close();
            }
        }
    }
}
