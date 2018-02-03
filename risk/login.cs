using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;

using MySql.Data;
using MySql.Data.MySqlClient;

using System.Configuration;

namespace risk
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //从窗体获取数据
                String name = textBox1.Text;
                String passwd = textBox2.Text;

            //建立数据库连接并执行sql语句
            string strcon = ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(strcon);
                conn.Open();
                MySqlCommand select = new MySqlCommand("select username from user where username='" + name + "' and password='" + passwd + "'", conn);
                //判定用户是否存在，并确定能否登录
                MySqlDataReader select_read = select.ExecuteReader();
                if (select_read.Read())
                {
                    if(radioButton1.Checked == true)
                    {
                        asset asset = new asset();
                        asset.Show();
                        this.Hide();

                        DialogResult dr = MessageBox.Show("是否删除数据库？", "提示", MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.OK)
                        {
                            //用户选择确认的操作
                            string strcon1 = ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
                            MySqlConnection conn1 = new MySqlConnection(strcon1);
                            conn1.Open();
                            string cmd = "UPDATE asset SET securit=0,integrity=0,usability=0,assetvalue=0;DELETE FROM menace;DELETE FROM vulnerability;";
                            MySqlCommand delete = new MySqlCommand(cmd, conn1);
                            delete.ExecuteNonQuery();
                            MessageBox.Show("数据库已清空。", "提示");
                            conn1.Close();
                        }
                        else if (dr == DialogResult.Cancel)
                        {
                            //用户选择取消的操作
                        }
                    }
                    else
                    {
                        statement statement =  new statement();
                        statement.Show();
                        this.Hide();
                    }
                    
                }
                else
                {
                    MessageBox.Show("账号或密码输入错误！","提示");
                }
                conn.Close();

            
                

                
            }
            catch
            {
              MessageBox.Show("程序出现错误，请重新登陆或注册！", "提示");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            regesiter regesiter = new regesiter();
            regesiter.Show();
            this.Hide();
        }
    }
}
