using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Sistema_empresa
{

    public partial class frm_empresa : Form
    {
        SQLiteDataReader dr;
        SQLiteConnection con = new SQLiteConnection(@"Data Source=X:\DEPTO_TI\JOÃO\Bancos\SIST_EMPRESA\dados.db");
        Microsoft.Office.Interop.Excel.Application XcelApp = new Microsoft.Office.Interop.Excel.Application();

        public frm_empresa()
        {
            InitializeComponent();
            CarregaDados();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            AuxClass.testaAlt = false;
            frm_cad_empresa f2 = new frm_cad_empresa();
            f2.Show();

            f2.Closed += F2_Closed;
        }

        private void F2_Closed(object sender, EventArgs e)
        {
            CarregaDados();
        }

        public void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Deseja excluir o registro??", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    string row = dataGridView1.CurrentRow.Cells["nome"].Value.ToString();
                    SQLiteCommand cmd = new SQLiteCommand("Delete from empresas where nome='" + row + "'", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro eliminado!");
                    con.Close();
                    CarregaDados();
                }
            }
            catch
            {
                MessageBox.Show("Falha ao excluir registro!");
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            try
            {
                AuxClass.Nome = dataGridView1.CurrentRow.Cells["nome"].Value.ToString();

                con.Open();
                SQLiteCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "Select * from empresas where nome like '%" + AuxClass.Nome + "%'";
                dr = cmd1.ExecuteReader();
                dr.Read();

                if (!dr.IsDBNull(0))
                {
                    AuxClass.Apelido = dr.GetString(0);
                }

                if (!dr.IsDBNull(1))
                {
                    AuxClass.Nome = dr.GetString(1);
                }

                if (!dr.IsDBNull(2))
                {
                    AuxClass.Cnpj = dr.GetString(2);
                }

                if (!dr.IsDBNull(3))
                {
                    AuxClass.Tributacao = dr.GetString(3);
                }

                if (!dr.IsDBNull(4))
                {
                    AuxClass.grupo_esocial = dr.GetString(4);
                }

                if (!dr.IsDBNull(5))
                {
                    AuxClass.Habilitada = dr.GetString(5);
                }

                if (!dr.IsDBNull(6))
                {
                    AuxClass.Pasta_rede = dr.GetString(6);
                }

                if (!dr.IsDBNull(7))
                {
                    AuxClass.Situacao_fiscal = dr.GetString(7);
                }

                if (!dr.IsDBNull(8))
                {
                    AuxClass.Data_cnd = dr.GetString(8);
                }

                if (!dr.IsDBNull(9))
                {
                    AuxClass.Procuracoes = dr.GetString(9);
                }

                if (!dr.IsDBNull(10))
                {
                    AuxClass.Tipo = dr.GetString(10);
                }

                if (!dr.IsDBNull(11))
                {
                    AuxClass.Simples = dr.GetString(11);
                }

                if (!dr.IsDBNull(12))
                {
                    AuxClass.Transmissoes = dr.GetString(12);
                }

                if (!dr.IsDBNull(13))
                {
                    AuxClass.Rh_interno = dr.GetString(13);
                }

                if (!dr.IsDBNull(14))
                {
                    AuxClass.Faturamento = dr.GetString(14);
                }

                if (!dr.IsDBNull(15))
                {
                    AuxClass.Data_faturamento = dr.GetString(15);
                }

                if (!dr.IsDBNull(16))
                {
                    AuxClass.Data_procuracoes = dr.GetString(16);
                }

                if (!dr.IsDBNull(17))
                {
                    AuxClass.Data_entrada = dr.GetString(17);
                }

                if (!dr.IsDBNull(18))
                {
                    AuxClass.Data_saida = dr.GetString(18);
                }

                if (!dr.IsDBNull(19))
                {
                    AuxClass.Data_inatividade = dr.GetString(19);
                }

                if (!dr.IsDBNull(20))
                {
                    AuxClass.Data_cnpj = dr.GetString(20);
                }

                if (!dr.IsDBNull(21))
                {
                    AuxClass.Data_simples = dr.GetString(21);
                }

                if (!dr.IsDBNull(22))
                {
                    AuxClass.Data_contrat = dr.GetString(22);
                }

                if (!dr.IsDBNull(23))
                {
                    AuxClass.Valor_capital = dr.GetString(23);
                }

                AuxClass.testaAlt = true;

                dr.Close();
                con.Close();
                frm_cad_empresa f2 = new frm_cad_empresa();
                f2.Alterar();
                f2.Show();
                f2.Closed += F2_Closed;

            }
            catch
            {
                MessageBox.Show("Não é possível alterar o registro!");
            }
        }

        public void CarregaDados()
        {
            con.Open();
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select apelido, nome, cnpj, tributacao, grupo_esocial, habilitada_sistema, pasta_rede, situacao_fiscal from empresas";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        public void escreveGrid()
        {
            con.Open();
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = consulta;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

            Int32 Conta = dataGridView1.Rows.Count;
            label3.Text = Conta.ToString();
        }

        String consulta = "";

        public void button4_Click(object sender, EventArgs e)
        {
            try{

                consulta = "Select apelido, nome, cnpj, tributacao, grupo_esocial, habilitada_sistema, pasta_rede, situacao_fiscal from empresas where nome like nome ";

                if (string.IsNullOrEmpty(txtApelido.Text) == false)
                {
                    consulta = consulta + " and apelido like '" + txtApelido.Text + "%'";
                }

                if (string.IsNullOrEmpty(txtNome.Text) == false)
                {
                    consulta = consulta + " and nome like '" + txtNome.Text + "%'";
                }

                if (string.IsNullOrEmpty(txtCpf.Text) == false)
                {
                    consulta = consulta + " and cpf like '" + txtCpf.Text + "%'";
                }

                if (checkBox1.Checked)
                {
                    consulta = consulta + " and rh_interno like '" + "S" + "%'";
                }
                escreveGrid();
            }
            catch
            {
                con.Close();
                MessageBox.Show("Defina um filtro válido!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CarregaDados();
            label3.Text = "___";
            txtApelido.Text = "";
            txtNome.Text = "";
            txtCpf.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                try
                {
                    XcelApp.Application.Workbooks.Add(Type.Missing);

                    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                    {
                        XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    XcelApp.Columns.AutoFit();
                    XcelApp.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                    XcelApp.Quit();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            /*DateTime date = DateTime.Today;
            string dataHoje = date.ToString("dd/MM/yyyy");*/
            String dataHoje = txtData.Text;
            consulta = "Select apelido, nome, cnpj, tributacao, grupo_esocial, habilitada_sistema, pasta_rede, situacao_fiscal from empresas where strftime('%y/%m/%d','validade_cnd') <='" + dataHoje + "'";
            escreveGrid();
        }
    }
}
