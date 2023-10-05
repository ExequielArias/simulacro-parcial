using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simulacro_parcial
{
    public partial class frmSimulacro : Form
    {
        Barrio oBarrio;
        DataTable tBarrio;
        Choferes oChofer;
        DataTable tChoferes;
        Viajes oViaje;
        DataTable tViaje;
        public frmSimulacro()
        {
            InitializeComponent();
        }

        private void cmdVer_Click(object sender, EventArgs e)
        {
            decimal importeTotal = 0;
            dgvFiltro.Rows.Clear();
            label1.Text = "";
            foreach (DataRow fila in tViaje.Rows)
            {
                if (int.Parse(comboBox2.SelectedValue.ToString()) == int.Parse(fila["chofer"].ToString()))
                {
                    DateTime mes = Convert.ToDateTime(fila["fecha"].ToString());
                    DateTime año = Convert.ToDateTime(fila["fecha"].ToString());


                    string desde = oBarrio.NombreDeBarrio(int.Parse(fila["desdebarrio"].ToString()));
                    string hasta = oBarrio.NombreDeBarrio(int.Parse(fila["hastabarrio"].ToString()));

                    if (int.Parse(mes.ToString("MM")) == (comboBox1.SelectedIndex + 1) && int.Parse(año.ToString("yyyy")) == int.Parse(textBox1.Text.ToString()))
                    {
                        DateTime fecha = Convert.ToDateTime(fila["fecha"].ToString());
                        dgvFiltro.Rows.Add(fecha.ToString("dd/MM/yyyy"), desde, hasta, fila["km"], fila["importe"]);
                        importeTotal += Convert.ToDecimal(fila["importe"].ToString());
                        label4.Text = "Importe total: $ " + importeTotal.ToString();
                    }
                }
            }
        }

        private void frmSimulacro_Load(object sender, EventArgs e)
        {
            oBarrio = new Barrio();
            tBarrio = oBarrio.GetData();
            oChofer = new Choferes();
            tChoferes = oChofer.GetData();
            oViaje = new Viajes();
            tViaje = oViaje.GetData();

            comboBox2.DisplayMember = "nombre";
            comboBox2.ValueMember = "chofer";
            comboBox2.DataSource = tChoferes;

            textBox1.Text = DateTime.Now.ToString("yyyy");
            int mesActual = DateTime.Now.Month;
            comboBox1.SelectedIndex = mesActual - 1;
            label4.Text = "";
        }
    }
}
