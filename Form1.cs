using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;


namespace addressBook
{
    public partial class Form1 : Form
    {
        
        private static string connString = "server=127.0.0.1;port=3306;database=contact;username=root;password=1070218; SslMode=none;";
        MySqlConnection conn = new MySqlConnection(connString);

        private int Fid;

        public Form1()
        {
            InitializeComponent();

            this.getInfo();

        }

        private void getInfo()
        {
            String sql = "select pid, pname, pphone, paddress from contact_test3";
            //MessageBox.Show(sql);
            MySqlConnection cnn = new MySqlConnection(connString);
            cnn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            MySqlDataReader reader = cmd.ExecuteReader();
            try
            {
                this.listView1.Items.Clear();
                while (reader.Read())
                {
                    string[] subItems = new string[]{
                   reader.GetInt32(0).ToString(),
                   reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3)};
                    this.listView1.Items.Add(new ListViewItem(subItems));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count == 0)
                return;
            if (this.listView1.FocusedItem == null)
                return;
            ListViewItem item = listView1.SelectedItems[0];
            txtName.Text = item.SubItems[1].Text;
            txtPhone.Text = item.SubItems[2].Text;
            txtAddress.Text = item.SubItems[3].Text;
            this.Fid = int.Parse(item.SubItems[0].Text);
            
        }


        private void enableInfo()
        {
            this.clearInfo();
            this.getInfo();
            this.txtAddress.Enabled = true;
            this.txtName.Enabled = true;
        }
        private void clearInfo()
        {
            this.listView1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                try
                {
                    addInfo(this.txtName.Text, this.txtPhone.Text, this.txtAddress.Text);
                    MessageBox.Show("添加成功。");
    		        this.getInfo();
                    this.enableInfo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
                try
                {
                    changeInfo(Fid, this.txtName.Text, this.txtPhone.Text, this.txtAddress.Text);
                    MessageBox.Show("修改成功。");
	                this.getInfo();
                    this.enableInfo();
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                addInfo(this.txtName.Text, this.txtPhone.Text, this.txtAddress.Text);
                MessageBox.Show("添加成功。");
                this.getInfo();
                this.enableInfo();
                this.txtName.Text = "";
                this.txtPhone.Text = "";
                this.txtAddress.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        public void addInfo(string name, string phone, string address)
        {
            string sql = "insert into contact_test3(pname,pphone,paddress) values("+"'"+name+"'"+","+"'"+phone+"'"+","+"'"+address+"'"+")";

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        
        public void changeInfo(int id, string name, string phone, string address)
        {
            string sql = "update contact_test3 set pname='" + name + "',pphone='" + phone + "',paddress = '"+address+"'";
            sql += "where pid=" + id;
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                changeInfo(Fid, this.txtName.Text, this.txtPhone.Text, this.txtAddress.Text);
                MessageBox.Show("修改成功。");
                this.getInfo();
                this.enableInfo();
                this.txtName.Text = "";
                this.txtPhone.Text = "";
                this.txtAddress.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void button5_Click(object sender, EventArgs e)
        {
            int infoID = this.getSelectID();
            if (infoID == 0 || infoID == null)
            {
                MessageBox.Show("请先选中联系人信息！");
                return;
            }
            try
            {
                string sql = "delete from contact_test3 where pid=" + infoID.ToString();
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("删除成功！");
                this.getInfo();
                this.enableInfo();
                this.txtName.Text = "";
                this.txtPhone.Text = "";
                this.txtAddress.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int getSelectID()
        {
            return this.Fid;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
