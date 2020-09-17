using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_BLL;
using Taxi_Model;
using Utils;

namespace Taxi_AppMain
{
    public partial class frmSageExport : Form
    {
       


        public frmSageExport()
        {
            InitializeComponent();
          
        
            this.Load += new EventHandler(frmSageExport_Load);


        }

        void frmSageExport_Load(object sender, EventArgs e)
        {

            dtpFromDate.Value = DateTime.Now.AddDays(-7).Date;
            dtpToDate.Value = DateTime.Now.Date;
          
        }

        private void PopulateGrid()
        {

            try
            {
                DateTime? frmDate = dtpFromDate.Value;
                DateTime? tillDate = dtpToDate.Value;


                if (frmDate == null)
                    frmDate = DateTime.Now.AddDays(-7).Date;

                if (tillDate == null)
                    tillDate = DateTime.Now.Date;


                var query = from a in General.GetQueryable<Invoice>(c => c.InvoiceTypeId == Enums.INVOICE_TYPE.ACCOUNT
                               && (c.InvoiceDate >= frmDate && c.InvoiceDate <= tillDate))
                            orderby a.InvoiceDate descending

                            select new
                            {
                                InvoiceType = "SI",
                                AccountCode = a.Gen_Company.CompanyCode,
                                SageNo = txtsageno.Text,
                                BlankField = "",
                                InvoiceDate = string.Format("{0:dd/MM/yyyy}", a.InvoiceDate),
                                InvoiceNo = a.InvoiceNo,
                                SoftwareName = "Cab Treasure",
                          //      InvoiceTotal = Math.Round(Convert.ToDecimal(string.Format("{0:0.00}", a.InvoiceTotal)), 2),
                                InvoiceTotal = a.InvoiceTotal,
                                TaxCode = txttaxcode.Text,
                                Vat = (a.InvoiceTotal * 20) / 100
                            };

                grdLister.DataSource = query.ToList();
            }
            catch(Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            try
            {




                 saveFileDialog1.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";

                saveFileDialog1.Title = "Save File";
                saveFileDialog1.FileName = "SageInvoice";


                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    PopulateGrid();

                    grdLister.Columns["InvoiceType"].Width = 70;
                    grdLister.Columns["InvoiceType"].HeaderText = "invoice type";

                    grdLister.Columns["AccountCode"].Width = 70;
                    grdLister.Columns["AccountCode"].HeaderText = "account code";

                    grdLister.Columns["SageNo"].Width = 50;
                    grdLister.Columns["SageNo"].HeaderText = "sage no.";

                
                    grdLister.Columns["BlankField"].HeaderText = "";

                    grdLister.Columns["InvoiceDate"].Width = 60;
                    grdLister.Columns["InvoiceDate"].HeaderText = "invoice date";

                    grdLister.Columns["InvoiceNo"].Width = 70;
                    grdLister.Columns["InvoiceNo"].HeaderText = "invoice npo.";

                    grdLister.Columns["SoftwareName"].Width = 120;
                    grdLister.Columns["SoftwareName"].HeaderText = "software name.";

                    grdLister.Columns["InvoiceTotal"].Width = 70;
                    grdLister.Columns["InvoiceTotal"].HeaderText = "invoice total";

                    grdLister.Columns["TaxCode"].Width = 50;
                    grdLister.Columns["TaxCode"].HeaderText = "tax code";

                    grdLister.Columns["Vat"].Width = 40;
                    grdLister.Columns["Vat"].HeaderText = "vat";



                    string heading = string.Empty;
                    //  heading = string.Format("{0:dd/MM/yyyy hh:mm tt}", dtF) + " till " + string.Format("{0:dd/MM/yyyy hh:mm tt}", dtT);

                    ClsExportGridView obj = new ClsExportGridView(grdLister, saveFileDialog1.FileName);
                    obj.ApplyCellFormatting = false;
                    obj.ApplyCustomCellFormatting = true;
                    //  obj.Heading = heading;
                    obj.ExportExcelAsync();
                }
            }
            catch
            {

            }
        }
    }
}
