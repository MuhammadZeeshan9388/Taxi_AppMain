using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using DAL;
using Utils;
using Taxi_BLL.Classes;



namespace Taxi_AppMain
{
    public partial class frmBookingAttributesList : Form
    {

        string ColumnName = string.Empty;
        int columnnumber = 0;
        public struct Col_BookingAttributesList
        {
            public static string Id = "Id";
            public static string DetailId = "DetailId";


            public static string Name = "Name";
            public static string ShortName = "ShortName";
            public static string Default = "Default";

        }

        private void FormatBookingAttributesListGrid()
        {


            //grdAttributesList.AllowAddNewRow = false;
            ////   grdDetails.AllowEditRow = false;
            ////grdVehicleAttributes.AutoCellFormatting = true;
            //grdAttributesList.ShowGroupPanel = false;

            grdAttributesList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            grdAttributesList.AllowUserToResizeRows = false;

            grdAttributesList.RowHeadersVisible = false;

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = Col_BookingAttributesList.Id;
            col.Visible = false;
            grdAttributesList.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.Name = Col_BookingAttributesList.DetailId;
            col.Visible = false;
            grdAttributesList.Columns.Add(col);


            DataGridViewCheckBoxColumn col1 = new DataGridViewCheckBoxColumn();
            col1.HeaderText = "Default";
            col1.Name = Col_BookingAttributesList.Default;
            col1.HeaderText = "";
            col1.Width = 50;
            //col1.ReadOnly = true;
            grdAttributesList.Columns.Add(col1);


            col = new DataGridViewTextBoxColumn();
            col.HeaderText = "Name";
            col.Name = Col_BookingAttributesList.Name;
            col.Width = 270;
            col.ReadOnly = true;
            grdAttributesList.Columns.Add(col);


            col = new DataGridViewTextBoxColumn();
            col.HeaderText = "ShortName";
            col.Name = Col_BookingAttributesList.ShortName;
            col.Visible = false;
            // col.ReadOnly = true;
            grdAttributesList.Columns.Add(col);


            //grdAttributesList.MasterTemplate.ShowRowHeaderColumn = false;
            //UI.GridFunctions.SetFilter(grdAttributesList);
            //grdAttributesList.AllowEditRow = true;


        }

        void txtFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (ColumnName == "Name")
                {
                    //grdLister.Rows.Cast<DataGridViewRow>().Where(c=>c.Cells["RefNumber"].Value.ToStr().Contains(txtFilter.Text)==false ).ToList().ForEach(c=>c.Visible=false);

                    if (txtFilter.Text == "")
                    {
                        for (int i = 0; i < grdAttributesList.Rows.Count; i++)
                        {
                            grdAttributesList.Rows[i].Visible = true;
                        }
                        //(grdAttributesList.DataSource as DataTable).DefaultView.RowFilter = null;
                    }
                    else
                    {

                        for (int i = 0; i < grdAttributesList.Rows.Count; i++)
                        {
                            //  grdAttributesList.Rows[i].Cells[Col_BookingAttributesList.ShortName].Value.ToStr()
                            if (grdAttributesList.Rows[i].Cells[Col_BookingAttributesList.Name].Value.ToStr().Contains(txtFilter.Text.Trim()))
                            {
                                grdAttributesList.Rows[i].Visible = true;
                            }
                            else
                            {

                                grdAttributesList.Rows[i].Visible = false;
                                //row.Visible = false;
                            }
                        }
                    
                    }

                }
             
            }
            catch (Exception ex)
            {

            }
        }

        void txtFilter_LostFocus(object sender, EventArgs e)
        {
            txtFilter.Visible = false;
            grdAttributesList.Columns[columnnumber].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

        }

        void grdAttributesList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            //grdAttributesList.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdAttributesList.Columns[columnnumber].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            columnnumber = e.ColumnIndex;

            ColumnName = grdAttributesList.Columns[columnnumber].Name;

            if (grdAttributesList.Rows.Count == 0)
            {
                txtFilter.Visible = false;
                return;
            }

            //if (ColumnName != "Re-Call" && ColumnName != "Re-Dispatch" && ColumnName != "Delete" && ColumnName != "Fare" && ColumnName != "PickupDate")
            if (ColumnName != "Default")
            {
                txtFilter.Visible = true;
                Rectangle rect = grdAttributesList.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                txtFilter.Location = rect.Location;
                txtFilter.Size = rect.Size;
                grdAttributesList.Controls.Add(txtFilter);
                txtFilter.LostFocus += new EventHandler(txtFilter_LostFocus);
                txtFilter.TextChanged += new EventHandler(txtFilter_TextChanged);
                grdAttributesList.Columns[columnnumber].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter;
                txtFilter.Focus();
            }
        }

        public string input_values;



        public frmBookingAttributesList(string values)
        {
            this.input_values = values;

            InitializeComponent();
            InitializeConstructor();
            FormatBookingAttributesListGrid();
            LoadBookingAttributesListGrid();


            grdAttributesList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            grdAttributesList.AllowUserToResizeRows = false;

            grdAttributesList.RowHeadersVisible = false;

            this.grdAttributesList.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(grdAttributesList_ColumnHeaderMouseClick);
            txtFilter.LostFocus += new EventHandler(txtFilter_LostFocus);
            txtFilter.TextChanged += new EventHandler(txtFilter_TextChanged);
        }

        private void InitializeConstructor()
        {

            this.FormClosed += new FormClosedEventHandler(frmAttribute_FormClosed);
            this.KeyDown += new KeyEventHandler(frmAttribute_KeyDown);
        }

        void frmAttribute_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.End)
            {
                SaveAndClose();
            }
        }

        void frmAttribute_FormClosed(object sender, FormClosedEventArgs e)
        {
           // this.Dispose(true);

          //  GC.Collect();

        }

        private void LoadBookingAttributesListGrid()
        {

            try
            {
                var list = General.GetQueryable<Gen_Attribute>(null).OrderBy(c => c.Name).ToList();

                
                grdAttributesList.RowCount = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    grdAttributesList.Rows[i].Cells[Col_BookingAttributesList.Id].Value = list[i].Id;
                    grdAttributesList.Rows[i].Cells[Col_BookingAttributesList.Default].Value = list[i].IsDefault;
                    grdAttributesList.Rows[i].Cells[Col_BookingAttributesList.Name].Value = list[i].Name + " [" + list[i].ShortName + "]";
                    grdAttributesList.Rows[i].Cells[Col_BookingAttributesList.ShortName].Value = list[i].ShortName;
                }



                //S,BC,SALO 
                string[] attributeValuesArr = input_values.ToStr().Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in attributeValuesArr)
                {
                    foreach (DataGridViewRow row in grdAttributesList.Rows)
                    {
                        if (row.Cells[Col_BookingAttributesList.ShortName].Value.ToStr().ToLower().Trim() == item.ToLower().Trim())
                            row.Cells[Col_BookingAttributesList.Default].Value = true;
                    }

                    //var attrRow = grdAttributesList.Rows.FirstOrDefault(c => c.Cells[Col_BookingAttributesList.ShortName].Value.ToStr().ToLower().Trim() == item.ToLower().Trim());

                    //if (attrRow != null)
                    //{
                    //    attrRow.Cells[Col_BookingAttributesList.Default].Value = true;
                    //}
                }

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);


            }


        }





        private void SaveAndClose()
        {

            try
            {

                input_values = string.Join(",", grdAttributesList.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[Col_BookingAttributesList.Default].Value.ToBool() == true).Select(c => c.Cells[Col_BookingAttributesList.ShortName].Value.ToStr()).ToArray<string>());

                //input_values = string.Join(",", grdAttributesList.Rows.Where(c => c.Cells[Col_BookingAttributesList.Default].Value.ToBool() == true).Select(c => c.Cells[Col_BookingAttributesList.ShortName].Value.ToStr()).ToArray<string>());

                this.Close();

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);


            }



        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            SaveAndClose();


        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
