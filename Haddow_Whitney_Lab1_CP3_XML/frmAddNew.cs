using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Haddow_Whitney_Lab1_CP3_XML
{
    public partial class frmAddNew : Form
    {
        public frmAddNew()
        {
            InitializeComponent();
        }
        private Patent patent = null;

        public Patent GetNewPatent()
        {
            this.ShowDialog(); //open this form
            return patent;
        }

        private bool IsValidData()
        {
            return Validator.IsPresent(txtNumber) &&
                   Validator.IsInt32(txtNumber) &&
                   Validator.IsPresent(txtAppNumber) &&
                   Validator.IsPresent(txtDescription) &&
                   Validator.IsPresent(txtFilingDate) &&
                   Validator.IsDateTime(txtFilingDate) &&
                   Validator.IsPresent(txtInventor);
        }

        //save all info as new patent object
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                patent = new Patent(
                    Convert.ToInt32(txtNumber.Text),
                    txtAppNumber.Text,
                    txtDescription.Text,
                    DateTime.Parse(txtFilingDate.Text),
                    txtInventor.Text,
                    txtInventor2.Text);
                this.Close();
            }
        }

        //close this form
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    } //end class
}
