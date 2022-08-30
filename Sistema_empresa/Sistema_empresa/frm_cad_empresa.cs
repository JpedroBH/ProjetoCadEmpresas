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
    public partial class frm_cad_empresa : Form
    {
        string Tipo, Simples, Transmissoes, Rh_interno, Faturamento, Procuracoes, Habilitada;

        public frm_cad_empresa()
        {
            InitializeComponent();
        }

        SQLiteConnection con = new SQLiteConnection(@"Data Source=X:\DEPTO_TI\JOÃO\Bancos\SIST_EMPRESA\dados.db");

        public void Alterar()
        {
            txtApelido.Text = AuxClass.Apelido;
            txtNome.Text = AuxClass.Nome;
            txtCnpj.Text = AuxClass.Cnpj;
            txtData_faturamento.Text = AuxClass.Data_faturamento;
            txtData_certificado.Text = AuxClass.Data_procuracoes;
            txtData_saida.Text = AuxClass.Data_saida;
            txtData_entrada.Text = AuxClass.Data_entrada;
            txtData_inatividade.Text = AuxClass.Data_inatividade;
            txtData_BaixaCnpj.Text = AuxClass.Data_cnpj;
            txtData_Excl_Simples.Text = AuxClass.Data_simples;
            txtData_UltContrat.Text = AuxClass.Data_contrat;
            txtData_ValidadeCnd.Text = AuxClass.Data_cnd;
            txtTributacao.Text = AuxClass.Tributacao;
            txtGrupo_esocial.Text = AuxClass.grupo_esocial;
            txtValorCS.Text = AuxClass.Valor_capital;
            txtPasta_rede.Text = AuxClass.Pasta_rede;
            txtSituacao.Text = AuxClass.Situacao_fiscal;

            /* Tipo */ 
            if (AuxClass.Tipo == "Simples")
            {
                radioButton3.Checked = true;
                groupBox6.Enabled = false;
            }
            else if (AuxClass.Tipo == "Outro")
            {
                radioButton4.Checked = true;
                groupBox6.Enabled = true;
            }

            /* Excluida do Simples? */ 
            if (AuxClass.Simples == "S")
            {
                radioButton5.Checked = true;
            }
            else if (AuxClass.Simples == "N")
            {
                radioButton6.Checked = true;
            }

            /* Transmissões? */
            if (AuxClass.Transmissoes == "S")
            {
                radioButton1.Checked = true;
            }
            else if (AuxClass.Transmissoes == "N")
            {
                radioButton2.Checked = true;
            }

            /* Rh interno? */
            if (AuxClass.Rh_interno == "S")
            {
                radioButton7.Checked = true;
            }
            else if (AuxClass.Rh_interno == "N")
            {
                radioButton8.Checked = true;
            }

            /* Faturamento? */
            if (AuxClass.Faturamento == "S")
            {
                radioButton11.Checked = true;
            }
            else if (AuxClass.Faturamento == "N")
            {
                radioButton12.Checked = true;
            }

            /* Procuracoes? */
            int teste1 = AuxClass.Procuracoes.IndexOf("CAMILA");
            int teste2 = AuxClass.Procuracoes.IndexOf("ESCRITORIO");
            int teste3 = AuxClass.Procuracoes.IndexOf("PINHEIRO");

            if (teste1 > -1)
            {
                checkBox1.Checked = true;
            }
            if (teste2 > -1)
            {
                checkBox2.Checked = true;
            }
            if (teste3 > -1)
            {
                checkBox3.Checked = true;
            }

            /* Habilitada? */
            if (AuxClass.Habilitada == "S")
            {
                radioButton14.Checked = true;
            }
            else if (AuxClass.Habilitada == "N")
            {
                radioButton15.Checked = true;
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Procuracoes = "";

                if (checkBox1.Checked == true)
                {
                    Procuracoes = Procuracoes + " CAMILA";
                }
                if (checkBox2.Checked == true)
                {
                    Procuracoes = Procuracoes + " ESCRITORIO";
                }
                if (checkBox3.Checked == true)
                {
                    Procuracoes = Procuracoes + " PINHEIRO";
                }

                if (AuxClass.testaAlt == true)
                {
                    con.Open();
                    SQLiteCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update empresas set apelido='" + txtApelido.Text + "',nome='" + txtNome.Text + "',cnpj='" + txtCnpj.Text + "',tipo='" + Tipo + "',excluida_simples='" + Simples + "',transmissoes='" + Transmissoes + "',rh_interno='" + Rh_interno + "',faturamento='" + Faturamento + "',data_faturamento='" + txtData_faturamento.Text + "',procuracoes='" + Procuracoes + "',data_venci_certifi='" + txtData_certificado.Text + "',data_saida='" + txtData_saida.Text + "',data_entrada='" + txtData_entrada.Text + "',data_inatividade='" + txtData_inatividade.Text + "',data_baixa_cnpj='" + txtData_BaixaCnpj.Text + "',data_exc_simples='" + txtData_Excl_Simples.Text + "',ultima_op_contrat='" + txtData_UltContrat.Text + "',validade_cnd='" + txtData_ValidadeCnd.Text + "',valor_capital='" + txtValorCS.Text + "',tributacao='" + txtTributacao.Text + "',grupo_esocial='" + txtGrupo_esocial.Text + "',habilitada_sistema='" + Habilitada + "',pasta_rede='" + txtPasta_rede.Text + "',situacao_fiscal='" + txtSituacao.Text + "'where nome like '%" + AuxClass.Nome + "%'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Atualizado com exito");
                    con.Close();
                }
                else
                {
                    con.Open();
                    SQLiteCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into empresas (apelido, nome, cnpj, tipo, excluida_simples, transmissoes, rh_interno, faturamento, data_faturamento, procuracoes, data_venci_certifi, data_saida, data_entrada, data_inatividade, data_baixa_cnpj, data_exc_simples, ultima_op_contrat, validade_cnd, valor_capital, tributacao, grupo_esocial, habilitada_sistema, pasta_rede, situacao_fiscal) values('" + txtApelido.Text + "','" + txtNome.Text + "','" + txtCnpj.Text + "','" + Tipo + "','" + Simples + "','" + Transmissoes + "','" + Rh_interno + "','" + Faturamento + "','" + txtData_faturamento.Text + "','" + Procuracoes + "','" + txtData_certificado.Text + "','" + txtData_saida.Text + "','" + txtData_entrada.Text + "','" + txtData_inatividade.Text + "','" + txtData_BaixaCnpj.Text + "','" + txtData_Excl_Simples.Text + "','" + txtData_UltContrat.Text + "','" + txtData_ValidadeCnd.Text + "','" + txtValorCS.Text + "','" + txtTributacao.Text + "','" + txtGrupo_esocial.Text + "','" + Habilitada + "','" + txtPasta_rede.Text + "','" + txtSituacao.Text + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Dados salvos");
                }
                this.Close();
            }
            catch
            {
                MessageBox.Show("Falha ao cadastrar!");
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Tipo = "Simples";
            groupBox6.Enabled = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            txtData_Excl_Simples.Enabled = false;
            txtData_Excl_Simples.Text = "";
            Simples = "";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Tipo = "Outro";
            groupBox6.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Simples = "S";
            txtData_Excl_Simples.Enabled = true;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            Simples = "N";
            txtData_Excl_Simples.Enabled = false;
            txtData_Excl_Simples.Text = "";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Transmissoes = "S";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Transmissoes = "N";
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            Rh_interno = "S";
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            Rh_interno = "N";
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            Faturamento = "N";
            txtData_faturamento.Enabled = false;
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            Faturamento = "S";
            txtData_faturamento.Enabled = true;
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            Habilitada = "S";
        }

        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            Habilitada = "N";
        }
    }
}
