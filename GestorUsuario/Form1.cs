using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;

namespace GestorUsuario
{
    public partial class Form1 : Form
    {
        ClassEntidad objent = new ClassEntidad();
        ClassNegocio objneg = new ClassNegocio();
        ClassDatos objdatos = new ClassDatos();
        SqlConnection connection = new SqlConnection(ClassDatos.cn);
        public Form1()
        {
            InitializeComponent();
        }

       

        void insertar()
        {
            try
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO gestor (objent.nombre, objent.correo, objent.telefono) VALUES (@nombre, @correo, @telefono)", connection))
                {
                    command.Parameters.AddWithValue("@nombre", objent.nombre);
                    command.Parameters.AddWithValue("@correo", objent.correo); ;
                    command.Parameters.AddWithValue("@telefono", objent.telefono);



                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        MessageBox.Show("Error al guardar los datos en la base de datos.");
                }



                using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM gestor", connection))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridView1.DataSource = table;
                }

                //btn_Cancelar_Click(sender, e);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error:" + ex);
            }

            connection.Close();

        }

        void Modificar()
        {
            connection.Open();

            string consula = "update gestor set nombre= '" + txtnombre.Text + "',correo='" + txtcorreo.Text + "',telefono='" + txttelefono.Text  + "' where idcliente='" + txtid.Text + "';";
            SqlCommand comando = new SqlCommand(consula, connection);
            int Ca;
            Ca = comando.ExecuteNonQuery();
            if (Ca > 0)
            {
                MessageBox.Show("Registro modificado");
            }

            connection.Close();


        }

        void limpiar()
        {
            txtid.Text = "";
            txtnombre.Text = "";
            txtcorreo.Text = "";
            txttelefono.Text = "";
            txtbuscar.Text = "";
            
        }

        void Eliminar()
        {
            connection.Open();

            string consulta = "delete from gestor where idcliente='" + txtid.Text + "';";
            SqlCommand comando = new SqlCommand(consulta, connection);
            comando.ExecuteNonQuery();
            MessageBox.Show("Registro eliminado");

            connection.Close();
        }

        void Mostrar()
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM gestor", connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridView1.DataSource = table;
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = objneg.N_listar_clientes();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void unoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void dosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Valinando
            if (txtnombre.Text == "")
                MessageBox.Show("Debe ingresar su nombre.");
            else if (txtcorreo.Text == "")
                MessageBox.Show("Debe ingresar su correo.");
            else if (txttelefono.Text == "")
                MessageBox.Show("Debe ingresar su numero de telefono.");

            objent.nombre = txtnombre.Text;
            objent.correo = txtcorreo.Text;
            objent.telefono = (txttelefono.Text);


            if (txtid.Text == "")
            {
                

                  if (MessageBox.Show("¿Deseas registrar a " + txtnombre.Text + "?", "Mensaje",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    
                  {
                    insertar();
                    limpiar();
                    pictureBox1.Image = null;
                  }
            }
        }

        private void tresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtid.Text != "")
            {
                if (MessageBox.Show("¿Deseas modificar a " + txtnombre.Text + "?", "Mensaje",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    Modificar();
                    limpiar();
                }
            }
        }

        private void cuatroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtid.Text != "")
            {
                if (MessageBox.Show("¿Deseas eliminar a " + txtnombre.Text + "?", "Mensaje",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    // mantenimiento("3", GetPictureBox1());
                    Eliminar();
                    limpiar();
                }
            }
        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            /*if(txtbuscar.Text != "")
            {
                objent.id = txtbuscar.Text;
                DataTable dt = new DataTable();
                dt = objneg.N_buscar_clientes(objent);
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = objneg.N_listar_clientes();
            }*/
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = dataGridView1.CurrentCell.RowIndex;

            txtid.Text = dataGridView1[0, fila].Value.ToString();
            txtnombre.Text = dataGridView1[1, fila].Value.ToString();
            txtcorreo.Text = dataGridView1[2, fila].Value.ToString();
            txttelefono.Text = dataGridView1[3, fila].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fo = new OpenFileDialog();
                DialogResult rs = fo.ShowDialog();
                if (rs == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(fo.FileName);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Coloque una imagen mas apropiada.");;
            }
            
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Obtener los datos de la fila seleccionada
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            objent.id = int.Parse(row.Cells[0].Value.ToString());
            objent.nombre = row.Cells[1].Value.ToString();
            objent.correo = row.Cells[2].Value.ToString();
            objent.telefono = row.Cells[3].Value.ToString();



            // Asignar los datos a los textbox o combobox correspondientes
            txtid.Text = objent.id.ToString();
            txtnombre.Text = objent.nombre.ToString();
            txtcorreo.Text = objent.correo.ToString();
            txttelefono.Text = objent.telefono.ToString();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (txtbuscar.Text == "")
                MessageBox.Show("Debe escribir lo que quiere buscar.");
            else
            {
                string valor = txtbuscar.Text;
                string query = "SELECT * FROM gestor WHERE nombre LIKE '%" + valor + "%' OR correo LIKE '%" + valor + "%' OR telefono LIKE '%" + valor + "%'";
                using (SqlConnection connection = new SqlConnection(ClassDatos.cn))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }

        }
    }
}
