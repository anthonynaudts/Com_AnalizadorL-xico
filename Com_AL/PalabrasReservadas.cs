using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_AL
{
    public class PalabrasReservadas
    {
        public string AnalizarPR(string analizado)
        {
            string dato = "";

            string[,] PalabrasReservadas = { 
                        { "=", "Signo de igual" },
                        { "+", "Signo de suma" },
                        { "-", "Signo de resta" },
                        { "*", "Signo de multiplicación" },
                        { "/", "Signo de división" },
                        { ";", "Símbolo de cierre de linea" },
                        { ">", "Signo de mayor" },
                        { "<", "Signo de menor" },
                        { "{", "Llave inicio" },
                        { "}", "Llave final" },
                        { "(", "Paréntesis inicio" },
                        { ")", "Paréntesis final" },
                        { "&&", "AND" },
                        { "||", "OR" },
                        { "INICIO", "P/R de inicio" },
                        { "FIN", "P/R de fin" },
                        { "ENT", "P/R de tipo entero" },
                        { "CAD", "P/R de tipo cadena" },
                        { "SI", "P/R SI" },
                        { "SINO", "P/R SI NO" },
                        { "PARA", "P/R PARA" },
                        { "HACER", "P/R HACER" },
                        { "MIENTRAS", "P/R MIENTRAS" },
                        { "MOSTRAR", "P/R Mostrar" },
                        { "LEER", "P/R Leer" },
                        { ",", "Coma" },
                        { ".", "Punto" } };

            for (int i = 0; i < PalabrasReservadas.GetLength(0); i++)
            {
                if (PalabrasReservadas[i, 0] == analizado)
                {
                    dato = PalabrasReservadas[i, 1];
                    break;
                }
            }

            if (dato == "")
            {
                int valor = 0;
                int validar = 0;

                if (int.TryParse(analizado, out valor))
                {
                    dato = "Numero";
                    validar = 1;
                }

                if (analizado.StartsWith("'") && analizado.EndsWith("'"))
                {
                    dato = "Texto";
                    validar = 1;
                }

                if (validar == 0)
                {
                    dato = "Identificador";
                }
            }
            
            return dato;
            
        }
    }
}
