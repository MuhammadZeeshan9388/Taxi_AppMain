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
using Telerik.WinControls.UI;
using Taxi_AppMain.Classes;
using System.Data.Linq;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmShuttleZones : UI.SetupBase
    {
        public struct COLS
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string Area = "Area";

            public static string PostCode = "PostCode";



        }


        ShuttleZoneBO objMaster;
        public frmShuttleZones()
        {
            InitializeComponent();

            objMaster = new ShuttleZoneBO();
            this.SetProperties((INavigation)objMaster);
        
            this.FormClosing += new FormClosingEventHandler(frmZones_FormClosing);
            
           this.Shown += new EventHandler(frmZones_Shown);
           grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
        grdLister.CommandCellClick+=new CommandCellClickEventHandler(grdLister_CommandCellClick);
        grdLister.RowsChanging += new GridViewCollectionChangingEventHandler(Grid_RowsChanging);
           LoadAllowedPickups();
           FormatGrid();
           
        }

        private void LoadAllowedPickups()
        {
            int CurrentZoneId=objMaster.PrimaryKeyValue.ToInt();

            grdAllowedPickups.Rows.Clear();

            var list = General.GetQueryable<Gen_ShuttleZone>(c=>(CurrentZoneId==0 || c.Id!=CurrentZoneId)).ToList();


            foreach (var item in list)
            {
               var row  = grdAllowedPickups.Rows.AddNew();

               row.Cells["AllowedZoneId"].Value = item.Id;
               row.Cells["AllowedZoneName"].Value = item.ZoneName;
               row.Cells["PostCodes"].Value = item.PostCodes;

            }


          


        }



        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row != null && e.Row is GridViewDataRowInfo)
            {
                objMaster.GetByPrimaryKey( e.Row.Cells["Id"].Value.ToInt());
                LoadAllowedPickups();
                DisplayRecord();

            }
        }

        void frmZones_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

        public override void PopulateData()
        {


            LoadZonesList();
        }


        private void LoadZonesList()
        {
            try
            {

                var data1 = General.GetQueryable<Gen_ShuttleZone>(null).OrderBy(c => c.OrderNo).AsEnumerable();


                var query = from a in data1



                            select new
                            {
                                Id = a.Id,
                                Sno = a.OrderNo,
                                ZoneName = a.ZoneName,
                                PostCodes = a.PostCodes,


                            };

                grdLister.DataSource = query.ToList();



               
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }


       

        void frmZones_Shown(object sender, EventArgs e)
        {
            try
            {
                LoadZonesList();

            //    grdLister.AddEditColumn();


                grdLister.Columns["Id"].IsVisible = false;
                grdLister.Columns["Sno"].IsVisible = false;

                grdLister.Columns["ZoneName"].HeaderText = "Zone Name";
                grdLister.Columns["PostCodes"].HeaderText = "PostCode(s)";


                // this.SetRefreshingProperties(AppVars.BLData.GetCommand(query), grdLister, false);

                grdLister.Columns["ZoneName"].Width = 150;
                grdLister.Columns["PostCodes"].Width = grdLister.Width - 300;

                if (this.CanDelete)
                {
                    grdLister.AddDeleteColumn();
                    grdLister.Columns["btnDelete"].Width = 70;
                }

                txtZoneName.Focus();
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }

   
     

        #region Overridden Methods


      

        public override void DisplayRecord()
        {
            if (objMaster.Current == null) return;

            txtZoneName.Text = objMaster.Current.ZoneName;
           

            grdPostCodes.Rows.Clear();
            GridViewRowInfo row=null;
            foreach (var item in objMaster.Current.Gen_ShuttleZone_AssociatedPostCodes.OrderBy(c=>c.OrderNo.ToInt()))
	        {
                row=grdPostCodes.Rows.AddNew();
                row.Cells[COLS.Id].Value=item.Id;
                row.Cells[COLS.MasterId].Value=item.ZoneId;
             //   row.Cells[COLS.Area].Value = item.AreaName.ToStr();
                row.Cells[COLS.PostCode].Value=item.PostCode;
        		 
	        }

            grdPostCodes.CurrentRow = null;


            foreach (var item in objMaster.Current.Gen_ShuttleZone_AllowedZones)
            {
                row=   grdAllowedPickups.Rows.FirstOrDefault(c=>c.Cells["AllowedZoneId"].Value.ToInt()==item.AllowedZoneId);

                if (row != null)
                {
                    row.Cells[COLS.Id].Value = item.Id;
                    row.Cells[COLS.MasterId].Value = item.ZoneId;
                    //   row.Cells[COLS.Area].Value = item.AreaName.ToStr();
                    row.Cells["IsAllowed"].Value = true;

                }
                else
                {
                    row.Cells[COLS.Id].Value = 0;
                    row.Cells[COLS.MasterId].Value = null;
                    //   row.Cells[COLS.Area].Value = item.AreaName.ToStr();
                    row.Cells["IsAllowed"].Value = false;


                }


            

            }



        }




        public override void Save()
        {
        

            try
            {
       


                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                }
                else
                {
                    objMaster.Edit();
                }

                objMaster.Current.ZoneName = txtZoneName.Text.Trim();
                objMaster.Current.PostCodes=string.Join(",",grdPostCodes.Rows.Select(c=>c.Cells[COLS.PostCode].Value.ToStr()).ToArray<string>());


              //  objMaster.Current.IsBase = chkBase.Checked;
          

            

                string[] skipProperties=new string[]{"Gen_ShuttleZone","Gen_ShuttleZone1"};


                int orderNo = 1;
                IList<Gen_ShuttleZone_AssociatedPostCode> savedList = objMaster.Current.Gen_ShuttleZone_AssociatedPostCodes;
                List<Gen_ShuttleZone_AssociatedPostCode> listofDetail = (from r in grdPostCodes.Rows
                                                            //where r.Cells[COL_DOCUMENT.FILENAME].Value.ToStr()!=string.Empty
                                                                         select new Gen_ShuttleZone_AssociatedPostCode
                                                         {
                                                              Id = r.Cells[COLS.Id].Value.ToInt(),
                                                              ZoneId = r.Cells[COLS.MasterId].Value.ToInt(),
                                                               AreaName=objMaster.Current.ZoneName,
                                                              PostCode  = r.Cells[COLS.PostCode].Value.ToStr().Trim(),
                                                              OrderNo=orderNo++
                                                         }).ToList();           


                Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);



                RadListDataItem item = new RadListDataItem();
             



                IList<Gen_ShuttleZone_AllowedZone> savedList2 = objMaster.Current.Gen_ShuttleZone_AllowedZones;
                List<Gen_ShuttleZone_AllowedZone> listofDetail2 = (from r in grdAllowedPickups.Rows.Where(c=>c.Cells["IsAllowed"].Value.ToBool())
                                                                         //where r.Cells[COL_DOCUMENT.FILENAME].Value.ToStr()!=string.Empty
                                                                   select new Gen_ShuttleZone_AllowedZone
                                                                         {
                                                                             Id = r.Cells[COLS.Id].Value.ToInt(),
                                                                             ZoneId = r.Cells[COLS.MasterId].Value.ToInt(),
                                                                              AllowedZoneId = r.Cells["AllowedZoneId"].Value.ToIntorNull()
                                                                             
                                                                         }).ToList();


                Utils.General.SyncChildCollection(ref savedList2, ref listofDetail2, "Id", skipProperties);

            
                objMaster.Save();

                PopulateData();

          //      PDAClass.SaveZone(objMaster.Current);
            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }
        }


        //private void SaveToServer()
        //{
        //    try
        //    {
        //        if (AppVars.objPolicyConfiguration.EnablePDA.ToBool() == false) return;

        //        string clientId = AppVars.objPolicyConfiguration.DefaultClientId.ToStr();
        //        using (PDADataContext db = new PDADataContext())
        //        {

        //            Zone objZone = db.Zones.FirstOrDefault(c => c.ZoneName == objMaster.Current.ZoneName && c.ClientId == clientId);

        //            if (objZone == null)
        //            {
        //                objZone = new Zone();
        //            }


        //            objZone.ZoneName = objMaster.Current.ZoneName;
        //            objZone.PostCode = objMaster.Current.PostCode;

        //            objZone.ClientId = clientId;


        //            objZone.Zone_AssociatedPostCodes.Clear();

        //            objZone.Zone_AssociatedPostCodes.AddRange(from r in objMaster.Current.Gen_Zone_AssociatedPostCodes
        //                                                      //where r.Cells[COL_DOCUMENT.FILENAME].Value.ToStr()!=string.Empty
        //                                                      select new Zone_AssociatedPostCode
        //                                                      {
        //                                                          //   Id = r.Id,
        //                                                          //   ZoneId = r.ZoneId,
        //                                                          AreaName = r.AreaName,
        //                                                          PostCode = r.PostCode,
        //                                                          OrderNo = r.OrderNo
        //                                                      });




        //            if (objZone.Id == 0)
        //            {
        //                //      objZone.Id   = objMaster.Current.Id;
        //                db.Zones.InsertOnSubmit(objZone);
        //            }

        //            db.SubmitChanges();
        //        }

        //    }
        //    catch (Exception ex)
        //    {


        //    }

        //}

        public override void  AddNew()
        {
         	 OnNew();
        }

        public override void  OnNew()
        {
 	        grdPostCodes.Rows.Clear();
            txtZoneName.Focus();
        }

        #endregion

   

        private void FormatGrid()
        {
            grdPostCodes.AllowAutoSizeColumns = true;
            grdPostCodes.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdPostCodes.AllowAddNewRow = false;
            grdPostCodes.ShowGroupPanel = false;
            grdPostCodes.AutoCellFormatting = true;
            grdPostCodes.ShowRowHeaderColumn = false;

            //grdDocuments.CommandCellClick += new CommandCellClickEventHandler(grdDocuments_CommandCellClick);


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.Id;
            grdPostCodes.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.MasterId;
            grdPostCodes.Columns.Add(col);


            //col = new GridViewTextBoxColumn();
            //col.HeaderText = "Area";
            //col.Name = COLS.Area;
            //col.ReadOnly = true;
           
            //col.Width = 120;
            //grdPostCodes.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Post Code";
            col.Name = COLS.PostCode;
            col.Width = 100;
            col.ReadOnly = true;

            grdPostCodes.Columns.Add(col);

            UI.GridFunctions.AddDeleteColumn(grdPostCodes);

        }

        private void frmZones_FormClosed(object sender, FormClosedEventArgs e)
        {
         

            General.DisposeForm(this);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearPostCode();
        }

        private void ClearPostCode()
        {

            grdPostCodes.CurrentRow = null;
            txtPostCode.Text = string.Empty;
        //    txtArea.Text = string.Empty;

            txtPostCode.Focus();

        }

        private void btnAddPostCode_Click(object sender, EventArgs e)
        {
            AddPostCode();
        }

        private void AddPostCode()
        {
          //  string area = txtArea.Text.ToStr().Trim();
            string postCode = txtPostCode.Text.ToStr().Trim();
           


            string error = string.Empty;

         

            if (string.IsNullOrEmpty(postCode))
            {
                error += "Required : Post Code";
            }       

            if (grdPostCodes.CurrentRow != null && grdPostCodes.CurrentRow is GridViewNewRowInfo)
                grdPostCodes.CurrentRow = null;


            int rowIndex = grdPostCodes.CurrentRow != null ? grdPostCodes.CurrentRow.Index : -1;


            if (grdPostCodes.Rows.Count(c => c.Cells[COLS.PostCode].Value.ToStr() == postCode
                && c.Index != rowIndex) > 0)
            {
                error += "Post Code already exist..";

            }
            else
            {
                int zoneid= objMaster.PrimaryKeyValue.ToInt();

                if (General.GetQueryable<Gen_Zone_AssociatedPostCode>(c => c.PostCode == postCode && (c.ZoneId != zoneid || zoneid == 0)).Count() > 0)
                {
                    error += "This PostCode is already entered in other zone";
                }
            }

            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;
            }


            GridViewRowInfo row = null;
            if (grdPostCodes.CurrentRow != null)
            {
                row = grdPostCodes.CurrentRow;
             
            }

            else
            {
                row = grdPostCodes.Rows.AddNew();

            }
        //    row.Cells[COLS.Area].Value = area;
            row.Cells[COLS.PostCode].Value = postCode;



            ClearPostCode();




        }

        private void grdPostCodes_DoubleClick(object sender, EventArgs e)
        {
            if (grdPostCodes.CurrentRow != null && grdPostCodes.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdPostCodes.CurrentRow;
              //  txtArea.Text = row.Cells[COLS.Area].Value.ToStr();

                txtPostCode.Text = row.Cells[COLS.PostCode].Value.ToStr();


            }
        }

        private void txtZoneName_Validated(object sender, EventArgs e)
        {
            txtZoneName.Text = txtZoneName.Text.ToStr().Trim().ToProperCase();
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            MoveRow(true);
        }


        private void MoveRow(bool moveUp) 
        {
            GridViewRowInfo currentRow = this.grdPostCodes.CurrentRow;
            if (currentRow == null)
            {
                return;
            }
            int index = moveUp ? currentRow.Index - 1 : currentRow.Index + 1;
            if (index < 0 || index >= this.grdPostCodes.RowCount) 
            {
                return; 
            }
            this.grdPostCodes.Rows.Move(index, currentRow.Index);
            this.grdPostCodes.CurrentRow = this.grdPostCodes.Rows[index]; 
        }

        private void btnMoveDownZone_Click(object sender, EventArgs e)
        {
            MoveRow(false);
        }

        private void grdLister_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
            {



                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Zone ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {


                    RadGridView grid = gridCell.GridControl;
                    grid.CurrentRow.Delete();
                }
            }
          
        }

        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                objMaster = new ShuttleZoneBO();

                try
                {

                    objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());

                    if (objMaster.Current != null)
                    {

                        objMaster.Delete(objMaster.Current);


                    }

                }
                catch (Exception ex)
                {
                    if (objMaster.Errors.Count > 0)
                        ENUtils.ShowMessage(objMaster.ShowErrors());
                    else
                    {
                        ENUtils.ShowMessage(ex.Message);

                    }
                    e.Cancel = true;

                }

            }
        }

        
    }



}
