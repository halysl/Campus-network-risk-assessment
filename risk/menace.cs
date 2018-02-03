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
    public partial class menace : Form
    {
        public menace(string s)
        {
            //将asset的comboBox1.Text通过构造函数传递过来
            InitializeComponent();
            this.label2.Text = s;
        }

        //声明一个16*5的comboBox的组件数组
        public ComboBox[,] menacelist = new ComboBox[16, 5];
        
        //创建释放16*5的comboBox组件的函数
        private void AddCombobox(CheckedListBox clb, int temp, ComboBox[,] ex, int score)
        {
            for (int j = 0; j < temp; j++)
            {
                for (int i = 0; i < clb.Items.Count; i++)
                {
                    ComboBox cb = new ComboBox();
                    for (int i_ = 0; i_ < score + 1; i_++)
                    {
                        cb.Items.Add(i_.ToString());
                    }
                    cb.Size = new Size(40, 50);
                    cb.Enabled = false;
                    cb.Location = new Point(150 + j * 50, 115 + 24 * i);
                    this.Controls.Add(cb);
                    ex[i, j] = cb;
                }
            }
        }

        //当页面读取时，添加comboBox数组
        private void menace_Load(object sender, EventArgs e)
        {
            AddCombobox(checkedListBox1, 5, menacelist, 3);
        }

        //判定18*5的comboBox组件的状态函数
        private void FindComboBox(CheckedListBox cb, ComboBox[,] ex, int t)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (cb.GetItemChecked(i))
                {
                    for (int j = 0; j < t; j++)
                    {
                        ex[i, j].Enabled = true;
                    }
                }
                else
                {
                    for (int j = 0; j < t; j++)
                    {
                        ex[i, j].Enabled = false;
                        ex[i, j].Text = null;
                    }
                }
            }
        }

        //当checkedlist的check属性发生改变时，判断comboBox组件是否可被操作
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindComboBox(checkedListBox1, menacelist, 5);
        }

        //确定按钮事件
        private void button1_Click(object sender, EventArgs e)
        {
            //声明需要传递给数据库的数组，为了直接传递过去而不用频繁的开启关闭数据库
            string asset_name = "";
            string[] menace_name = new string[16];
            string[] cmd = new string[16];
            double[] threat = new double[16];
            int[] d = new int[16];
            int[] r = new int[16];
            int[] ea = new int[16];
            int[] a = new int[16];
            int[] da = new int[16];
            int[] i_list = new int[16];
            

            try
            {
                //循环整个checkedlist的状态，通过GetItemChecked属性判断下一步操作
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        //获取数据
                        asset_name = label2.Text;
                        menace_name[i] = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                        d[i] = Convert.ToInt32(menacelist[i, 0].Text);
                        r[i] = Convert.ToInt32(menacelist[i, 1].Text);
                        ea[i] = Convert.ToInt32(menacelist[i, 2].Text);
                        a[i] = Convert.ToInt32(menacelist[i, 3].Text);
                        da[i] = Convert.ToInt32(menacelist[i, 4].Text);
                        threat[i] = Math.Round(Math.Log(((Math.Pow(2, d[i]) + Math.Pow(2, r[i]) + Math.Pow(2, ea[i]) + Math.Pow(2, a[i]) + Math.Pow(2, da[i])) / 5), 2), 3);
                        i_list[i] = 1;
                    }
                }
                //连接数据库
                string strcon = ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(strcon);
                conn.Open();
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (i_list[i] == 1)
                    {
                        cmd[i] = "INSERT INTO menace(assetname,menacename,d,r,e,a,da,threat) VALUES('" + asset_name + "','" + menace_name[i] + "'," + d[i] + "," + r[i] + "," + ea[i] + "," + a[i] + "," + da[i] + "," +threat[i]+ ")";
                        MySqlCommand cmda = new MySqlCommand(cmd[i], conn);
                        cmda.ExecuteNonQuery();
                                                
                    }
                }
                conn.Close();
            }
            catch
            {
                MessageBox.Show("出现未知错误！","提示");
            }
            finally
            {
                MessageBox.Show("添加数据成功，点击确定回到资产评估。","提示");
            }

            this.Close();         
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
