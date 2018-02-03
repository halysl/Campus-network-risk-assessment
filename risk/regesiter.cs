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
    public partial class regesiter : Form
    {
        public regesiter()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //从窗体获取数据
                String name = textBox1.Text;
                String pwd = textBox2.Text;
                //建立mysql连接，并执行sql语句
                string strcon = ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(strcon);
                conn.Open();
                MySqlCommand useradd = new MySqlCommand("insert into user(username, password) values('" + name + "','" + pwd + "')", conn);
                useradd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("注册成功，请重新登陆！", "提示");
                //窗体切换
                login login = new login();
                login.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("注册信息有误，请重新注册！","提示");
            }
            
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            login login = new login();
            login.Show();
            this.Close();
        }

        private void regesiter_Load(object sender, EventArgs e)
        {

        }
    }
}
