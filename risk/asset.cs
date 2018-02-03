using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace risk
{
    public partial class asset : Form
    {
        public asset()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(562, 262);
        }

        private void asset_Load(object sender, EventArgs e)
        {
            //连接数据库，读取数据附加到comboBox1上
            string strcon = ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(strcon);
            conn.Open();
            MySqlCommand select = new MySqlCommand("select assetname from asset", conn);
            MySqlDataReader select_read = select.ExecuteReader();
            while (select_read.Read())
            {
                comboBox1.Items.Add(select_read["assetname"]);
            }
            conn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //获取comboBox上的数据
                String name = comboBox1.Text;
                int sec_sco = Convert.ToInt32(comboBox3.Text);
                int int_sco = Convert.ToInt32(comboBox4.Text);
                int usa_sco = Convert.ToInt32(comboBox5.Text);

                //资产计算模型：Ai = Round1{ log2[(2Aic + 2Aii + 2Aia)/ 3]}
                double assetvalue = Math.Round(Math.Log(((Math.Pow(2, sec_sco) + Math.Pow(2, int_sco) + Math.Pow(2, usa_sco)) / 3), 2), 1);

                //连接数据库，并传回值

                string strcon = ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(strcon);
                conn.Open();
                String cmd0 = "update asset set securit=" + sec_sco + ",integrity=" + int_sco + ",usability=" + usa_sco + ",assetvalue=" + assetvalue + " where assetname='" + name + "'";
                MySqlCommand cmd = new MySqlCommand(cmd0, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("添加数据成功，点击确定进入" + name + "的威胁评估！","提示");

                //执行后窗口变化
                menace menace = new menace(name);
                menace.Show();

            }
            catch
            {
                MessageBox.Show("输入错误！","提示");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = comboBox1.Text;
            vulnerability vulnerability = new vulnerability(name);
            vulnerability.Show();
        }
                
        private void button3_Click(object sender, EventArgs e)
        {
            statement statement = new statement();
            statement.Show();
            this.Hide(); 
        }
    }
}
