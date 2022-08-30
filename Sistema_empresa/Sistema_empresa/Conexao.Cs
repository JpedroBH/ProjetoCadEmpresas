using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Sistema_empresa
{
    class Conexao
    {
        private static string connString = @"Data Source=X:\DEPTO_TI\JOÃO\Bancos\SIST_EMPRESA\dados.db";
        private static SQLiteConnection conn = null;
        public static SQLiteConnection obterConexao()
        {
            conn = new SQLiteConnection(connString);
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                conn = null;
                MessageBox.Show("Conexão não estabelecida!!");
            }
            return conn;
        }
        public static void fecharConexao()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
    }
}