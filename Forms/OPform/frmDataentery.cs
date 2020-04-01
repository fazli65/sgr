using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SGRSalary.Classes;

namespace SGRSalary.Forms.OPform
{
    public partial class frmDataentery : Form
    {
        public frmDataentery()
        {
            InitializeComponent();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
           DataTable dt= ExcelOprations.ImportFromExcel();
           dgvData.DataSource = dt;
        }
    }
}
