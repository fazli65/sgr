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
using SGRSalary.Repository;

namespace SGRSalary.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RepositorySgr repo = new RepositorySgr();
            var p = repo.GetPersons();
        }

    }
}
