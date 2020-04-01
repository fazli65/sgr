using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SGRSalary.Forms
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        public void ClearControls()
        {
            foreach (Control ctrl in pnlControls.Controls.OfType<Control>())
            {
                if (ctrl is TextEdit txtCtl)
                    txtCtl.Text = string.Empty;
                if (ctrl is ComboBoxEdit cmCtrl)
                    cmCtrl.SelectedItem = null ;
            }
        }

        public virtual void ConfirmProcess() { }
        public virtual void NewEntity() {  }
        public virtual void EditEntity() { }
        public virtual void DeleteEntity() { }


        #region Menus
        private void TsMenuConfirm_Click(object sender, EventArgs e)
        {
            ConfirmProcess();
        }

        private void TsMenuNew_Click(object sender, EventArgs e)
        {
            NewEntity();
        }

        private void TsMenuEdit_Click(object sender, EventArgs e)
        {
            EditEntity();
        }

        private void TsMenuDelete_Click(object sender, EventArgs e)
        {
            DeleteEntity();
        }
        #endregion
    }
}
