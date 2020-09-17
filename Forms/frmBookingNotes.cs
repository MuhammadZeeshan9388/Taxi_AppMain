using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;
using DAL;
using UI;
using Telerik.WinControls.UI;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Taxi_AppMain.Classes;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;
using System.Collections;

namespace Taxi_AppMain
{
    public partial class frmBookingNotes : UI.SetupBase
    {
        int BookingId = 0;
        public IList<Booking_Note> Notes { get; set; }
        public frmBookingNotes(int id)
        {
            BookingId = id;
            InitializeComponent();
            FormateNotetGride();
            grdNotes.CellDoubleClick += new GridViewCellEventHandler(grdNotes_CellDoubleClick);
            grdNotes.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            if (BookingId != 0)
            {
                DisplayRecords();
            }
        }

        BookingBO objNotes = new BookingBO();

        void DisplayRecords()
        {

           // Booking b = new Booking();
            
            objNotes.GetByPrimaryKey(BookingId);


            var data1 = objNotes.Current.Booking_Notes.OrderBy(c=> c.Id);

            if (objNotes.Current.Booking_Notes.Count() > 0)
            {
                GridViewRowInfo row = null;

                for (int i = 0; i < objNotes.Current.Booking_Notes.Count(); i++)
                {
                   row= grdNotes.Rows.AddNew();
                    
                    row.Cells[COL_NOTE.NOTES].Value = objNotes.Current.Booking_Notes[i].notes.ToStr();
                    row.Cells[COL_NOTE.ID].Value = objNotes.Current.Booking_Notes[i].Id.ToInt();
                    row.Cells[COL_NOTE.MASTERID].Value = BookingId;
                 

                }

                grdNotes.CurrentRow = null;
                
            }
        }
        public struct COL_NOTE
        {
            public static string ID = "ID";
            public static string MASTERID = "MASTERID";

            public static string NOTES = "NOTES";


        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNotes();
            grdNotes.Columns["NOTES"].Width = 500;
        }

        private void AddNotes()
        {

            try
            {
                string Note = txtNotes.Text.ToString();
                

                string error = string.Empty;
                
                if (Note == "")
                {
                    error += "Required : Note";
                }
                

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }


                GridViewRowInfo row = null;

                if (grdNotes.CurrentRow != null)
                {
                    row = grdNotes.CurrentRow;
                }

                else
                {
                    row = grdNotes.Rows.AddNew();
                }
                row.Cells[COL_NOTE.NOTES].Value = Note;

                txtNotes.Text = string.Empty;
                grdNotes.CurrentRow = null;
            }
            catch (Exception ex)
            {

            }
        }
        private void FormateNotetGride()
        {
            grdNotes.AllowAutoSizeColumns = true;
            grdNotes.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdNotes.AllowAddNewRow = false;
            grdNotes.ShowGroupPanel = false;
            grdNotes.AutoCellFormatting = true;
            grdNotes.ShowRowHeaderColumn = false;



            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_NOTE.ID;
            grdNotes.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_NOTE.MASTERID;
            grdNotes.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.HeaderText = "Note";
            col.IsVisible = true;
            col.Name = COL_NOTE.NOTES;
            col.Width = 500;
            grdNotes.Columns.Add(col);



            grdNotes.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;


            grdNotes.AddDeleteColumn();
            grdNotes.Columns["btnDelete"].Width = 70;

          //  UI.GridFunctions.SetFilter(grdNotes);

            grdNotes.TableElement.RowHeight = 100;

        }
        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete Note. ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    //objNotes.GetByPrimaryKey(grdNotes.CurrentRow.Cells["Id"].Value.ToLong());
                    //if (objNotes.Current != null)
                    //{

                    //    objNotes.Delete(objNotes.Current);
                    //    RadGridView grid = gridCell.GridControl;
                    //    grid.CurrentRow.Delete();


                    //}
                    //else
                    //{
                        RadGridView grid = gridCell.GridControl;
                        grid.CurrentRow.Delete();
                   // }


                }
            }

        }
        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
            //var list = (from row in grdNotes.Rows.Cast<DataGridViewRow>()
            //            from cell in row.Cells.Cast<DataGridViewCell>()
            //            select new
            //            {
            //                ID = 
            //            }).ToList();
        }
        
        void grdNotes_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewDataRowInfo)
            {
                txtNotes.Text = e.Row.Cells[COL_NOTE.NOTES].Value.ToString();

            }
        }
        public override void Save()
        {
            try
            {

                
                if (objNotes.PrimaryKeyValue != null)
                {
                    objNotes.Edit();


                    string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };

                    IList<Booking_Note> savedList3 = objNotes.Current.Booking_Notes;
                    List<Booking_Note> listofDetail3 = (from r in grdNotes.Rows

                                                        select new Booking_Note
                                                              {
                                                                  Id = r.Cells[COL_NOTE.ID].Value.ToInt(),
                                                                  BookingId = BookingId,
                                                                  notes = r.Cells[COL_NOTE.NOTES].Value.ToStr(),
                                                              }).ToList();


                    Utils.General.SyncChildCollection(ref savedList3, ref listofDetail3, "Id", skipProperties);



                    objNotes.Save();


                }
                this.Close();

            }
            catch (Exception ex)
            {
                if (objNotes.Errors.Count > 0)
                    ENUtils.ShowMessage(objNotes.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (objNotes.PrimaryKeyValue!=null || grdNotes.Rows.Count > 0 )
            {
                Save();
            }
            //else
            //{
            //    ENUtils.ShowMessage("Requried: atleast one Note");
            //}
        }

        private void frmBookingNotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();

            }
        } 
    }
}
