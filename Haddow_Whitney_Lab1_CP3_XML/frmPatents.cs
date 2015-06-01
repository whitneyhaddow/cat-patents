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
    public partial class frmPatents : Form
    {
        public frmPatents()
        {
            InitializeComponent();
        }

        private List<Patent> patents = new List<Patent>();

        private void Form1_Load(object sender, EventArgs e)
        {
            //add columns to listview     
            lstPatents.Columns.Add("Patent Number", 90, HorizontalAlignment.Left);
            lstPatents.Columns.Add("Application Number", 110, HorizontalAlignment.Left);
            lstPatents.Columns.Add("Description", 180, HorizontalAlignment.Left);
            lstPatents.Columns.Add("Filing Date", 75, HorizontalAlignment.Left);
            lstPatents.Columns.Add("Inventor", 120, HorizontalAlignment.Left);
            lstPatents.Columns.Add("Inventor 2", 120, HorizontalAlignment.Left);

            patents = PatentDB.GetPatents(); //read XML file for patent data
            FillPatentListView(); 

        }   

        //Fill list view with data from XML file
        private void FillPatentListView()
        {
            lstPatents.Items.Clear();
            foreach (Patent patent in patents) 
            {
                string patentString = patent.GetPatentString("#");
                char[] delimiter = { '#' };
                string[] patentStrings = patentString.Split(delimiter); //split into attributes

                ListViewItem item = new ListViewItem(patentStrings);
                //ListViewItem item = new ListViewItem(patentStrings[0], 0);

               // item.SubItems.Add(patentStrings[1]); //App Number
               // item.SubItems.Add(patentStrings[2]); //Description
               // item.SubItems.Add(patentStrings[3]); //Filing Date
               // item.SubItems.Add(patentStrings[4]); //Inventor
                //if (patentStrings[5] == "") //if there is no second Inventor
                //{
                //    item.SubItems.Add("N/A");
                //}
                //else
                //    item.SubItems.Add(patentStrings[5]); //Inventor 2

                lstPatents.Items.Add(item); 
            }
        }

        //add new patent object
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddNew newPatentForm = new frmAddNew(); //create new instance of second form
            Patent patent = newPatentForm.GetNewPatent();
            if (patent != null) 
            {
                patents.Add(patent); //add to list
                PatentDB.SavePatents(patents); //save to XML file
                FillPatentListView();
            }
        }

        //close application
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
