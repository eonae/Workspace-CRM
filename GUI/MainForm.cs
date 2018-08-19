using System;
using System.Windows.Forms;
using DataAccessLibrary;
using HelperLibrary;
using CRM.BL;

namespace GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Model model = new Model();

            lvRecords.Items.AddRange(Model.Filter.GetRecords(tbxSearch.Text).ToListViewItems());

            tbxSearch.TextChanged += (sender, args) =>
            {
                lvRecords.Items.Clear();
                lvRecords.Items.AddRange(Model.Filter.GetRecords(tbxSearch.Text).ToListViewItems());
            };
        }
    }
}
