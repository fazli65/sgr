using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SGRSalary.Model;

namespace SGRSalary.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void فیشحقوقیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Repository.Repository repo=new Repository.Repository();
            User u=new User(){Name="z",Family = "m"};
            repo.AddUser(u);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void شرکتToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pMToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void BtnCompany_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
