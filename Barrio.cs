using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace simulacro_parcial
{
    internal class Barrio
    {
        OleDbConnection conector;
        OleDbCommand comando;
        OleDbDataAdapter adaptador;
        DataTable tabla;
        public Barrio()
        {
            conector = new OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=REMISYA.mdb");
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Choferes";

            adaptador = new OleDbDataAdapter(comando);

            tabla = new DataTable();
            adaptador.Fill(tabla);

            DataColumn[] vec = new DataColumn[1];
            vec[0] = tabla.Columns["chofer"];
            tabla.PrimaryKey = vec;
        }
        public string NombreDeBarrio(int NumeroBarrio)
        {
            DataRow fila = tabla.Rows.Find(NumeroBarrio);
            return fila["nombre"].ToString();

        }
        public int[] devolverBarrio()
        {
            int[] barrios = new int[11];
            int i = 0;
            foreach (DataRow fila in tabla.Rows)
            {
                barrios[i] = int.Parse(fila["barrio"].ToString());
                i++;
            }
            return barrios;
        }
        public DataTable GetData()
        {
            return tabla;
        }
        public void grabar(string nombre, int barrio) 
        {
            try
            {
                foreach (DataRow datos in tabla.Rows)
                {

                    if (barrio == int.Parse(datos["barrio"].ToString()) && nombre == datos["nombre"].ToString())
                    {
                        MessageBox.Show("Hola");
                    }
                    else
                    {
                        tabla.Rows.Add(barrio, nombre);
                        OleDbCommandBuilder cb = new OleDbCommandBuilder(adaptador);
                        adaptador.Update(tabla);

                    }


                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nombre exitente");
            }
        }
    }
}
