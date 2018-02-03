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
    public partial class statement : Form
    {
        public statement()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(708, 453);
        }

        //定义资产数据结构
        public struct zc
        {
            public string assetname;
            public double assetvalue;
        }

        //定义威胁数据结构
        public class wx
        {
            public string menacename = "";
            public double menacescore = 0;

        }

        //定义脆弱性可利用性数据结构
        public struct vulnerability_dou
        {
            public string vulnerabilityname { get; set; }
            public int dou { get; set; }
        }

        //定义脆弱性数据结构
        public struct crx
        {
            public string assetname { get; set; }
            public string vulnerability_name { get; set; }
            public double vf { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //资产名数组
            zc[] asset = new zc[29];
            //for (int i_ = 0; i_ < 30; i_++)
            //{
            //    asset[i_] = new zc();
            //}
            int i = 0;
            string strcon = ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(strcon);
            conn.Open();
            MySqlCommand select = new MySqlCommand("select assetname from asset", conn);
            MySqlDataReader select_read = select.ExecuteReader();
            while (select_read.Read())
            {
                asset[i].assetname = select_read["assetname"].ToString();
                i += 1;
            }
            conn.Close();

            //威胁列表数组
            /*link[] link = new link[18];
            for(int i_=0;i_<18;i_++)
            {
                link[i_] = new link();
            }*/

            wx[] menace = new wx[16];
            for (int i_ = 0; i_ < 16; i_++)
            {
                menace[i_] = new wx();
            }
            string[] _menacename = new string[16] {"操作错误","滥用授权","特权升级","行为抵赖",
                "拒绝服务","恶意代码","篡改","社会工程","垃圾邮件","窃听数据","物理破坏","网络仿冒",
                "意外故障","通信中断","电源中断","灾难"};

            for (i = 0; i < 16; i++)
            {
                menace[i].menacename = _menacename[i];
                //link[i].menacename = _menacename[i];
            }

            //脆弱性列表数组
            vulnerability_dou[] vulnerablity = new vulnerability_dou[16];
            for (int i_ = 0; i_ < 16; i_++)
            {
                vulnerablity[i_] = new vulnerability_dou();
            }
            vulnerablity[0].vulnerabilityname = "存在弱口令"; vulnerablity[0].dou = 4;
            vulnerablity[1].vulnerabilityname = "权限分配不明晰"; vulnerablity[1].dou = 3;
            vulnerablity[2].vulnerabilityname = "设备质量不过关"; vulnerablity[2].dou = 2;
            vulnerablity[3].vulnerabilityname = "没及时更新病毒库"; vulnerablity[3].dou = 3;
            vulnerablity[4].vulnerabilityname = "对硬件的保护疏忽"; vulnerablity[4].dou = 2;
            vulnerablity[5].vulnerabilityname = "没有坚固的防御机制"; vulnerablity[5].dou = 4;
            vulnerablity[6].vulnerabilityname = "没有专业的安全专家"; vulnerablity[6].dou = 3;
            vulnerablity[7].vulnerabilityname = "信息审查过程不严密"; vulnerablity[7].dou = 4;
            vulnerablity[8].vulnerabilityname = "系统和软件配置不当"; vulnerablity[8].dou = 2;
            vulnerablity[9].vulnerabilityname = "安全管理机制不合理"; vulnerablity[9].dou = 3;
            vulnerablity[10].vulnerabilityname = "没有数据备份等补救措施"; vulnerablity[10].dou = 3;
            vulnerablity[11].vulnerabilityname = "没有灵敏的应急响应机制"; vulnerablity[11].dou = 3;
            vulnerablity[12].vulnerabilityname = "使用的通信加密方式落后"; vulnerablity[12].dou = 3;
            vulnerablity[13].vulnerabilityname = "无日志文件或日志不完整"; vulnerablity[13].dou = 2;
            vulnerablity[14].vulnerabilityname = "开启过多无谓的端口和服务"; vulnerablity[14].dou = 3;
            vulnerablity[15].vulnerabilityname = "系统没有及时更新或修补漏洞"; vulnerablity[15].dou = 4;
            
            /*威胁对应脆弱性列表
            link[0].value[0] = 1; link[0].value[1] = 1;
            link[1].value[0] = 1; link[1].value[1] = 1;
            link[2].value[0] = 1; link[2].value[1] = 1;
            link[3].value[0] = 1; link[3].value[1] = 1;
            link[4].value[0] = 1; link[4].value[1] = 1;
            link[5].value[0] = 1; link[5].value[1] = 1;
            link[6].value[0] = 1; link[6].value[1] = 1;
            link[7].value[0] = 1; link[7].value[1] = 1;
            link[8].value[0] = 1; link[8].value[1] = 1;
            link[9].value[0] = 1; link[9].value[1] = 1;
            link[10].value[0] = 1; link[10].value[1] = 1;
            link[11].value[0] = 1; link[11].value[1] = 1;
            link[12].value[0] = 1; link[12].value[1] = 1;
            link[13].value[0] = 1; link[13].value[1] = 1;
            link[14].value[0] = 1; link[14].value[1] = 1;
            link[15].value[0] = 1; link[15].value[1] = 1;
            link[16].value[0] = 1; link[16].value[1] = 1;
            link[17].value[0] = 1; link[17].value[1] = 1;*/

            //每一项脆弱性的漏洞数目总和
            int[] sum = new int[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //二维数组保存若干个资产和脆弱性构成的漏洞总数对该脆弱性漏洞总和的比例，即第i个资产的第j个脆弱性有x个漏洞，它占比为x/sum
            double[,] temp = new double[16, 16];
            double[] dou_num = new double[16];
            //临时参数，保存资产名及脆弱性名，方便传入数据库语句编写
            string[] assetname_temp = new string[29];
            string[] vulnerabilityname_temp = new string[16];
            
            conn.Open();
            //循环脆弱性列表
            int j = 0;
            for (i = 0; i < vulnerablity.Length; i++)
            {
                //临时变量用于循环，flag的值用于写入判断
                j = 0;
                int i_ = 0;
                
                Boolean Flag1 = false;
                MySqlCommand select1 = new MySqlCommand("select assetname,vulnerability_name,weight_num from vulnerability where vulnerability_name='" + vulnerablity[i].vulnerabilityname + "'", conn);
                MySqlDataReader datareader1 = select1.ExecuteReader();
                //从数据库中提取该脆弱性的所有项
                while (datareader1.Read())
                {
                    //循环累加到sum（漏洞总数）
                    sum[i] += Convert.ToInt16(datareader1["weight_num"]);
                    Flag1 = true;

                }
                datareader1.Close();

                //若存在该脆弱性的项，开始计算脆弱性占比及脆弱因子
                if (Flag1 == true)
                {
                    MySqlCommand select2 = new MySqlCommand("select assetname,vulnerability_name,weight_num from vulnerability where vulnerability_name='" + vulnerablity[i].vulnerabilityname + "'", conn);
                    MySqlDataReader datareader2 = select2.ExecuteReader();
                    while (datareader2.Read())
                    {
                        //double转换为实数
                        temp[i, j] = (double)Convert.ToInt16(datareader2["weight_num"]) / sum[i];
                        temp[i, j] = Math.Round(temp[i, j], 2);
                        dou_num[j] = temp[i, j] * vulnerablity[i].dou;
                        dou_num[j] = Math.Round(dou_num[j], 2);
                        assetname_temp[j] = datareader2["assetname"].ToString();
                        vulnerabilityname_temp[j] = datareader2["vulnerability_name"].ToString();
                        //j为一个临时累加器                        
                        j += 1;
                    }
                    datareader2.Close();
                    for (i_ = 0; i_ < j; ++i_)
                    {

                        //更新表中的漏洞占比及弱点因子dou_num[i_]
                        string cmd = "update vulnerability set coefficient=" + temp[i, i_] + ",vf=" + dou_num[i_] + " where assetname='" + assetname_temp[i_] + "' and vulnerability_name='" + vulnerabilityname_temp[i_] + "'";
                        MySqlCommand update = new MySqlCommand(cmd, conn);
                        update.ExecuteNonQuery();
                    }
                }

            }
            conn.Close();

            //临时参数，保存资产名及脆弱性名及威胁名，方便传入数据库语句编写
            zc[] asset_temp = new zc[29];
            wx[,] menace_temp = new wx[29, 16];

            for (int i_ = 0; i_ < 29; i_++)
            {
                for (int j__ = 0; j__ < 16; j__++)
                    menace_temp[i_, j__] = new wx();
            }

            crx[] crx_temp = new crx[29];

            //读取资产放入asset_temp类
            conn.Open();
            Boolean Flag = true;
            while (Flag == true)
            {
                j = 0;
                string cmd = "select assetname,assetvalue from asset where assetvalue>0";
                MySqlCommand select1 = new MySqlCommand(cmd, conn);
                MySqlDataReader asset_read = select1.ExecuteReader();
                while (asset_read.Read())
                {
                    asset_temp[j].assetname = asset_read["assetname"].ToString();
                    asset_temp[j].assetvalue = Convert.ToDouble(asset_read["assetvalue"]);
                    j++;
                }
                Flag = false;
                asset_read.Close();
            }

            int[] j_ = new int[j] ;
            for(i=0;i<j;i++)
            {
                j_[i] = 0;
            }
            //读取某资产的威胁放入menace_temp类
            for (i = 0; i < j; i++)
            {
                
                string cmd = "select assetname,menacename,threat from menace where assetname='" + asset_temp[i].assetname + "'";
                MySqlCommand select1 = new MySqlCommand(cmd, conn);
                MySqlDataReader menace_read = select1.ExecuteReader();
                while (menace_read.Read())
                {
                    menace_temp[i, j_[i]].menacename = menace_read["menacename"].ToString();
                    menace_temp[i, j_[i]].menacescore = Convert.ToDouble(menace_read["threat"]);
                    j_[i]++;
                }
                menace_read.Close();
            }

            //读取所有脆弱性放入crx
            i = 0;
            string cmd1 = "select assetname,vulnerability_name,vf from vulnerability";
            MySqlCommand select3 = new MySqlCommand(cmd1, conn);
            MySqlDataReader vulnerability_read = select3.ExecuteReader();
            while (vulnerability_read.Read())
            {
                crx_temp[i].assetname = vulnerability_read["assetname"].ToString();
                crx_temp[i].vulnerability_name = vulnerability_read["vulnerability_name"].ToString();
                crx_temp[i].vf = Convert.ToDouble(vulnerability_read["vf"]);
                i++;
            }
            vulnerability_read.Close();
            
            //ia,ib为循环变量
            //fxzmz=风险值名字,fxz=风险值,zhfxz=综合风险值,fxzz=风险总值
            //j为实际资产数量,j_[i]为实际威胁数
            int ia = 0;
            int ib = 0;
            int ic = 0;
            
            string[][][] fxzmz = new string[asset_temp.Length][][];
            string[,] fxdj = new string[j, 16];
            double[,,] fxz = new double[j,16,16];
            double[,] zhfxz = new double[j,16];
            double[] fxzz = new double[asset_temp.Length];
            for (int aaa = 0; aaa < asset_temp.Length; aaa++)
            {
                fxzz[aaa] = 0;
            }


            double[,] vf = new double[29,16];
            int[] flag = new int[16];
            for(ia=0;ia<16;ia++)
            {
                flag[ia] = 0;
            }
            string cmd2 = "select assetname,vulnerability_name,vf from vulnerability";
            MySqlCommand select4 = new MySqlCommand(cmd2, conn);
            MySqlDataReader vulnerability_read1 = select4.ExecuteReader();
            //威胁对应脆弱性关系
            while (vulnerability_read1.Read())
            {
                if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[0].vulnerabilityname)
                {
                    vf[flag[0], 0] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[0]++;
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[1].vulnerabilityname)
                {
                    vf[flag[1], 1] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[1]++; 
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[2].vulnerabilityname)
                {
                    vf[flag[2], 2] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[2]++; 
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[3].vulnerabilityname)
                {
                    vf[flag[3], 3] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[3]++;
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[4].vulnerabilityname)
                {
                    vf[flag[4], 4] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[4]++;
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[5].vulnerabilityname)
                {
                    vf[flag[5], 5] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[5]++;
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[6].vulnerabilityname)
                {
                    vf[flag[6], 6] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[6]++;
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[7].vulnerabilityname)
                {
                    vf[flag[7], 7] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[7]++;
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[8].vulnerabilityname)
                {
                    vf[flag[8], 8] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[8]++;
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[9].vulnerabilityname)
                {
                    vf[flag[9], 9] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[9]++;
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[10].vulnerabilityname)
                {
                    vf[flag[10], 10] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[10]++;
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[11].vulnerabilityname)
                {
                    vf[flag[11], 11] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[11]++;
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[12].vulnerabilityname)
                {
                    vf[flag[12], 12] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[12]++;
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[13].vulnerabilityname)
                {
                    vf[flag[13], 13] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[13]++;
                }
                else if (vulnerability_read1["vulnerability_name"].ToString() == vulnerablity[14].vulnerabilityname)
                {
                    vf[flag[14], 14] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[14]++;
                }
                else
                {
                    vf[flag[15], 15] = Convert.ToDouble(vulnerability_read1["vf"]);
                    flag[15]++;
                }
            }
            vulnerability_read1.Close();

            
            for (ia = 0; ia < j; ia++)
            {
                double zcjz = asset_temp[ia].assetvalue;
                for (ib = 0; ib < j_[ia]; ib++)
                {
                    double wxyz = menace_temp[ia, ib].menacescore;
                    for(ic = 0;ic < i;ic++)
                    {
                        if(asset_temp[ia].assetname == crx_temp[ic].assetname)
                        {
                            fxz[ia,ib,ic] = zcjz * wxyz * crx_temp[ic].vf;
                            zhfxz[ia, ib] = Math.Max(fxz[ia,ib,ic],zhfxz[ia,ib]);
                        }
                        
                    }

                    if (zhfxz[ia, ib] > 20)
                        fxdj[ia, ib] = "极高";
                    else if (zhfxz[ia, ib] > 15 && zhfxz[ia, ib] < 20)
                        fxdj[ia, ib] = "高";
                    else if (zhfxz[ia, ib] > 10 && zhfxz[ia, ib] < 15)
                        fxdj[ia, ib] = "中";
                    else if (zhfxz[ia, ib] > 5 && zhfxz[ia, ib] < 10)
                        fxdj[ia, ib] = "低";
                    else
                        fxdj[ia, ib] = "极低";

                    
                                        
                }
            }

            

            Label[] asname = new Label[j+1];
            asname[0] = new Label();
            asname[0].Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular);
            asname[0].AutoSize = true;
            asname[0].Location = new Point(label1.Location.X,label1.Location.Y+30);
            asname[0].Text = asset_temp[0].assetname;
            Controls.Add(asname[0]);

            Label[] asvalue = new Label[j + 1];
            asvalue[0] = new Label();
            asvalue[0].Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular);
            asvalue[0].AutoSize = true;
            asvalue[0].Location = new Point(label5.Location.X, label5.Location.Y+30);
            asvalue[0].Text = asset_temp[0].assetvalue.ToString("f2"); ;
            //Controls.Add(asvalue[0]);

            for (ia = 0; ia < j; ia++)
            {
                asname[ia+1] = new Label();
                asname[ia+1].Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular);
                asname[ia+1].AutoSize = true;
                asname[ia+1].Location = new Point(label1.Location.X,asname[ia].Location.Y+30*j_[ia]); ;
                asname[ia+1].Text = asset_temp[ia+1].assetname;
                Controls.Add(asname[ia+1]);

                asvalue[ia+1] = new Label();
                asvalue[ia+1].Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular);
                asvalue[ia + 1].AutoSize = true;
                asvalue[ia + 1].Location = new Point(label5.Location.X, asname[ia].Location.Y + 30 * j_[ia]);
                asvalue[ia + 1].Text = asset_temp[ia+1].assetvalue.ToString("f2"); ;
                Controls.Add(asvalue[ia]);

                for (ib = 0; ib < j_[ia]; ib++)
                {
                    int a = asname[ia].Location.Y + 30 * ib;
                    Label mename = new Label();
                    mename.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular);
                    mename.AutoSize = true;
                    mename.Location = new Point(label2.Location.X, a);
                    mename.Text = menace_temp[ia, ib].menacename;
                    Controls.Add(mename);

                    Label mevalue = new Label();
                    mevalue.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular);
                    mevalue.AutoSize = true;
                    mevalue.Location = new Point(label6.Location.X,a);
                    mevalue.Text = menace_temp[ia, ib].menacescore.ToString("f2");
                    Controls.Add(mevalue);

                    Label zhfxzlabel = new Label();
                    zhfxzlabel.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular);
                    zhfxzlabel.AutoSize = true;
                    zhfxzlabel.Location = new Point(label3.Location.X, a);
                    zhfxzlabel.Text = zhfxz[ia, ib].ToString("f2");
                    Controls.Add(zhfxzlabel);

                    Label fxdjlabel = new Label();
                    fxdjlabel.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular); ;
                    fxdjlabel.AutoSize = true;
                    fxdjlabel.Location = new Point(label4.Location.X, a);
                    fxdjlabel.Text = fxdj[ia,ib].ToString();
                    Controls.Add(fxdjlabel);                    
                }
                
            }
                       
        }

        private void statement_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            asset asset = new asset();
            asset.Close();
        }
    }
 }