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


namespace Taxi_AppMain
{
    public partial class frmManageLocations : UI.SetupBase
    {
        List<Gen_LocationType> objLocationType = new List<Gen_LocationType>();
        bool IsLocationStateChange = false;
     
        public frmManageLocations()
        {
            InitializeComponent();

            FormatGrid();
            FormatLocationGrid();
            FormatDeleteLocationGrid();
            FillCombo();
            this.Shown += new EventHandler(frmZones_Shown);
           this.btnSaveClose.Click += new EventHandler(btnSaveClose_Click);
           this.btnSave.Click += new EventHandler(btnSave_Click);
           this.btnExit1.Click += new EventHandler(btnExit1_Click);
           
            this.ddlLocations.SelectedValueChanged += new EventHandler(ddlLocations_SelectedValueChanged);
            this.ddlLocationTypes2.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlLocationTypes2_SelectedIndexChanged);

            this.btnGetAll.Click += new EventHandler(btnGetAll_Click);
            this.chkIncludeAll.CheckedChanged += new EventHandler(chkIncludeAll_CheckedChanged);
            this.chkExcludeAll.CheckedChanged += new EventHandler(chkExcludeAll_CheckedChanged);
            this.chkAllLocations.CheckedChanged += new EventHandler(chkAllLocations_CheckedChanged);
            this.chkAllPostCodes.CheckedChanged += new EventHandler(chkAllPostCodes_CheckedChanged);
            this.grdDeleteLocations.ValueChanged += new EventHandler(grdDeleteLocations_ValueChanged);
            this.btnExit2.Click += new EventHandler(btnExit2_Click);
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
        }

        void btnExit2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void grdDeleteLocations_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                RadCheckBoxEditor radio = sender as RadCheckBoxEditor;
               // GridViewRowInfo row = grdDeleteLocations.CurrentRow ;
                if (radio != null)
                {

                    bool Status = false;//grdDeleteLocations.CurrentRow.Cells[COLSD.Delete].Value.ToBool();
                    if (radio.Value.ToStr().Equals("On"))
                    {

                        Status = true;
                    }
                    else
                    {
                        Status = false;
                    }
                    //if (Status)
                    //{
                    //    Status = false;
                    //}
                    //else
                    //{
                    //    Status = true;
                    //}
                    string PostCode = grdDeleteLocations.CurrentRow.Cells[COLSD.HalfPostCode].Value.ToStr();
                    grdLocations.BeginUpdate();
                    for (int i = 0; i < grdLocations.Rows.Count; i++)
                    {
                        if (grdLocations.Rows[i].Cells[COLSD.HalfPostCode].Value.ToStr() == PostCode)
                        {
                            grdLocations.Rows[i].Cells[COLSD.Delete].Value = Status;
                        }
                    }
                    grdLocations.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }



        
        void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteAddress();
        }
        private void DeleteAddress()
        {
            try
            {
                string Error = string.Empty;
                int LocationTypeId = ddlLocationTypes2.SelectedValue.ToInt();
                if (grdLocations.Rows.Where(c => c.Cells[COLSD.Delete].Value.ToBool() == true).Count() == 0)//|| grdDeleteLocations.Rows.Where(c => c.Cells[COLSD.Delete].Value.ToBool() == true).Count() == 0)
                {
                    Error = "Please select address to delete";
                }
                if (LocationTypeId == 0)
                {
                    if (String.IsNullOrEmpty(Error))
                    {
                        Error = "Required : Location Types";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required : Location Types";
                    }
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }
                //var DeleteAddressList=grdLocations.Rows.Where(c=>c.Cells[COLSD.Delete].Value.ToBool()==true).Select(c)
                string DeleteIds = string.Join(",", (grdLocations.Rows.Where(c =>
                             (c.Cells[COLSD.Delete].Value.ToBool() == true )
                            )
                            .Select(c => c.Cells[COLSD.Id].Value.ToString()).ToArray<string>()));
                string DeleteAddress = "delete from Gen_Locations where Id in(" + DeleteIds + ")";
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.stp_RunProcedure(DeleteAddress);
                 
                }
                DisplayDeleteLocation(LocationTypeId);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


      

        void chkAllPostCodes_CheckedChanged(object sender, EventArgs e)
        {
            grdDeleteLocations.CurrentRow = null;
            bool Status = chkAllPostCodes.Checked.ToBool();
            for (int i = 0; i < grdDeleteLocations.Rows.Count; i++)
            {
                grdDeleteLocations.Rows[i].Cells[COLSD.Delete].Value = Status;
            }
            SelectAddressByPostCodeToDelete(Status);
        }
        private void SelectAddressByPostCodeToDelete(bool Status)
        {
            try
            {
                var SelectPostCodeList = grdDeleteLocations.Rows.Where(c => c.Cells[COLSD.Delete].Value.ToBool() == true).Select(c => c.Cells[COLSD.HalfPostCode].Value).ToList();
                foreach (var item in SelectPostCodeList)
                {
                    string PostCode = item.ToStr();
                    //var AddressList = grdLocations.Rows.Where(c=>c.Cells[COLSD.HalfPostCode].Value==PostCode).Select(c => c.Cells[COLSD.Id].Value).ToList();
                    grdLocations.BeginUpdate();
                    for (int i = 0; i < grdLocations.Rows.Count; i++)
                    {
                        //grdLocations.Rows[]
                        if (grdLocations.Rows[i].Cells[COLSD.HalfPostCode].Value.ToStr() == PostCode)
                        {
                            grdLocations.Rows[i].Cells[COLSD.Delete].Value = Status;
                        }
                    }
                    grdLocations.EndUpdate();
                }
                if (Status == false)
                {
                    grdLocations.BeginUpdate();
                    for (int i = 0; i < grdLocations.Rows.Count; i++)
                    {
                        grdLocations.Rows[i].Cells[COLSD.Delete].Value = Status;
                    }
                    grdLocations.EndUpdate();
                }
                chkAllLocations.Checked = Status;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void chkAllLocations_CheckedChanged(object sender, EventArgs e)
        {
            grdLocations.CurrentRow = null;
            bool Statte = chkAllLocations.Checked.ToBool();
            for (int i = 0; i < grdLocations.Rows.Count; i++)
            {
                grdLocations.Rows[i].Cells[COLSD.Delete].Value = Statte;
            }
        }

        void ddlLocationTypes2_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            int LocationTypesId = ddlLocationTypes2.SelectedValue.ToInt();
            if (LocationTypesId > 0)
            {
                DisplayDeleteLocation(LocationTypesId);
            }
        }

        public struct COLS
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string Select = "Select";
            public static string Exclude = "Exclude";
            public static string AddressName = "AddressName";
            public static string LocationName = "LocationName";
            public static string LocationTypeId = "LocationTypeId";
            public static string PostCode = "PostCode";
            public static string ShortCutKey = "ShortCutKey";
            public static string IsExists = "IsExists";

        }

        public struct COLSD
        {
            public static string Id = "Id";
            public static string Delete = "Delete";
            public static string Address = "Address";
            public static string HalfPostCode = "HalfPostCode";
        }
        private void FormatLocationGrid()
        {
            grdLocations.AllowAutoSizeColumns = true;
            grdLocations.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdLocations.AllowAddNewRow = false;
            grdLocations.ShowGroupPanel = false;
            grdLocations.AutoCellFormatting = true;
            grdLocations.ShowRowHeaderColumn = false;
            grdLocations.EnableFiltering = true;
            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLSD.Delete;
            cbcol.HeaderText = "";
            cbcol.Width = 50;
            grdLocations.Columns.Add(cbcol);
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLSD.Id;
            grdLocations.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Width = 220;
            col.Name = COLSD.Address;
            col.ReadOnly = true;
            grdLocations.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLSD.HalfPostCode;
            grdLocations.Columns.Add(col);
        }
        private void FormatDeleteLocationGrid()
        {
            grdDeleteLocations.AllowAutoSizeColumns = true;
            grdDeleteLocations.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdDeleteLocations.AllowAddNewRow = false;
            grdDeleteLocations.ShowGroupPanel = false;
            grdDeleteLocations.AutoCellFormatting = true;
            grdDeleteLocations.ShowRowHeaderColumn = false;
            grdDeleteLocations.EnableFiltering = true;
            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLSD.Delete;
            cbcol.HeaderText = "";
            cbcol.Width = 50;
            grdDeleteLocations.Columns.Add(cbcol);
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLSD.HalfPostCode;
            col.HeaderText = "Post Code";
            col.ReadOnly = true;
            col.Width = 120;
            grdDeleteLocations.Columns.Add(col);
        }
        private string ShowPostCodes(string PostCode)
        {
            try
            {
                string HalfPostCode = string.Empty;

                for (int i = 0; i < PostCode.Length; i++)
                {
                    if (PostCode[i].ToStr().IsNumeric() == true)
                    {
                        //objList.Add(Word);
                        break;
                    }
                    else
                    {
                        HalfPostCode += PostCode[i].ToStr();
                    }
                }
                return HalfPostCode;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void DisplayDeleteLocation(int Id)
        {
            try
            {
                var list = (from a in General.GetQueryable<Gen_Location>(c => c.LocationTypeId == Id)
                            select new
                            {
                                Id = a.Id,
                                PostCode = a.PostCode,
                                Address = a.Address
                            }).ToList();
                grdLocations.RowCount = list.Count;
                grdLocations.BeginUpdate();
                for (int i = 0; i < list.Count; i++)
                {
                    grdLocations.Rows[i].Cells[COLSD.Delete].Value = false;
                    grdLocations.Rows[i].Cells[COLSD.Id].Value = list[i].Id;
                    grdLocations.Rows[i].Cells[COLSD.HalfPostCode].Value = ShowPostCodes(list[i].PostCode);
                    grdLocations.Rows[i].Cells[COLSD.Address].Value = list[i].Address;
                }
                grdLocations.EndUpdate();

                var listPostCodes = grdLocations.Rows.Select(c => c.Cells[COLSD.HalfPostCode].Value).Distinct().ToList();
                grdDeleteLocations.RowCount = listPostCodes.Count;
                grdDeleteLocations.BeginUpdate();
                for (int i = 0; i < listPostCodes.Count; i++)
                {
                    grdDeleteLocations.Rows[i].Cells[COLSD.HalfPostCode].Value = listPostCodes[i];
                    grdDeleteLocations.Rows[i].Cells[COLSD.Delete].Value = false;
                }
                grdDeleteLocations.EndUpdate();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            string Error = string.Empty;
            int LocationTypeId = ddlLocations.SelectedValue.ToInt();
            if (grdPostCodes.Rows.Where(c => c.Cells[COLS.Select].Value.ToBool() == true).Count() == 0 && grdPostCodes.Rows.Where(c => c.Cells[COLS.Exclude].Value.ToBool() == true).Count() == 0)
            {
                Error = "Please select Address";
            }
            if (LocationTypeId == 0)
            {
                if (String.IsNullOrEmpty(Error))
                {
                    Error = "Required : Location Types";
                }
                else
                {
                    Error += Environment.NewLine + "Required : Location Types";
                }
            }
            if (!string.IsNullOrEmpty(Error))
            {
                ENUtils.ShowMessage(Error);
                return;
            }
            Save();
            Display(LocationTypeId);
        }

        void chkExcludeAll_CheckedChanged(object sender, EventArgs e)
        {
            bool IsExclude = chkExcludeAll.Checked.ToBool();
            for (int i = 0; i < grdPostCodes.Rows.Count; i++)
            {
                    grdPostCodes.Rows[i].Cells[COLS.Exclude].Value = IsExclude;
            }
        }

        void chkIncludeAll_CheckedChanged(object sender, EventArgs e)
        {
            bool IsInclude = chkIncludeAll.Checked.ToBool();
            //for (int i = 0; i < grdPostCodes.Rows.Count; i++)
            //{
            //    grdPostCodes.Rows[i].Cells[COLS.Select].Value = IsInclude;
            //}

            grdPostCodes.BeginUpdate();
            grdPostCodes.Rows.ToList().ForEach(c => c.Cells[COLS.Select].Value = IsInclude);
            grdPostCodes.EndUpdate();
        }


        void btnGetAll_Click(object sender, EventArgs e)
        {
            GetAllLocations();
        }
        private void GetAllLocations()
        {
            try
            {
                int Id = ddlLocations.SelectedValue.ToInt();
                string LocationType = ddlLocations.Text;
                string Error = string.Empty;
                if (Id == 0)
                {
                    Error = "Required : Location Type";
                }

                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }
                  
                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                               var list = db.stp_GetRoadLevelLocations(LocationType).Select(c => c.LocationName).AsEnumerable<string>();
                               // var list = db.stp_getRoadLevelLocations(LocationType).Select(c => c.LocationName).AsEnumerable<string>();
                                var glist=grdPostCodes.Rows.Select(c=>c.Cells[COLS.AddressName].Value.ToStr()).AsEnumerable<string>();
                                var finalList = list.Except(glist).ToList();



                                if (grdPostCodes.Rows.Count == 0)
                                {
                                    grdPostCodes.RowCount = finalList.Count;
                                    grdPostCodes.BeginUpdate();
                                    for (int i = 0; i < finalList.Count; i++)
                                    {
                                        grdPostCodes.Rows[i].Cells[COLS.AddressName].Value = finalList[i];
                                        //grdPostCodes.Rows[i].Cells[COLS.Select].Value = false;
                                    }

                                    grdPostCodes.EndUpdate();

                                }
                                else
                                {

                                    GridViewRowInfo row;

                                    grdPostCodes.BeginUpdate();
                                    for (int i = 0; i < finalList.Count(); i++)
                                    {
                                        //if (grdPostCodes.Rows.Where(c => c.Cells[COLS.AddressName].Value.ToStr() == finalList[i].ToStr()).Count() == 0)
                                        //{
                                        row = grdPostCodes.Rows.AddNew();
                                        row.Cells[COLS.AddressName].Value = finalList[i];
                                        // grdPostCodes.Rows[i].Cells[COLS.IsExists].Value = false;
                                        //    }

                                    }

                                    grdPostCodes.EndUpdate();

                                }
                               
                            }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public void Display(int Id)
        {
            try
            {
                    var list = (from a in General.GetQueryable<Gen_Location>(c => c.LocationTypeId == Id)
                                select new
                                {
                                    Id = a.Id,
                                    LocationName = a.LocationName,
                                    LocationTypeId = a.LocationTypeId,
                                    PostCode = a.PostCode,
                                    ShortCutKey = a.ShortCutKey,
                                    Address = a.Address
                                }).ToList();

                    grdPostCodes.RowCount = list.Count;
                    grdPostCodes.BeginUpdate();
                    for (int i = 0; i < list.Count; i++)
                    {

                        grdPostCodes.Rows[i].Cells[COLS.Select].Value = true;
                        grdPostCodes.Rows[i].Cells[COLS.Exclude].Value = false;
                        grdPostCodes.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                        grdPostCodes.Rows[i].Cells[COLS.LocationName].Value = list[i].LocationName;
                        grdPostCodes.Rows[i].Cells[COLS.LocationTypeId].Value = list[i].LocationTypeId;
                        grdPostCodes.Rows[i].Cells[COLS.PostCode].Value = list[i].PostCode;
                        grdPostCodes.Rows[i].Cells[COLS.ShortCutKey].Value = list[i].ShortCutKey;
                        grdPostCodes.Rows[i].Cells[COLS.AddressName].Value = list[i].Address;
                    }

                    grdPostCodes.EndUpdate();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        void ddlLocations_SelectedValueChanged(object sender, EventArgs e)
        {
              int Id = ddlLocations.SelectedValue.ToInt();

              if (Id > 0)
              {
                  Display(Id);
              }
        }
       

        void btnSaveClose_Click(object sender, EventArgs e)
        {
            string Error = string.Empty;
            int LocationTypeId = ddlLocations.SelectedValue.ToInt();
            if (grdPostCodes.Rows.Where(c => c.Cells[COLS.Select].Value.ToBool() == true).Count() == 0 && grdPostCodes.Rows.Where(c => c.Cells[COLS.Exclude].Value.ToBool() == true).Count() == 0)
            {
                Error = "Please select Address";
            }
            if (LocationTypeId == 0)
            {
                if (String.IsNullOrEmpty(Error))
                {
                    Error = "Required : Location Types";
                }
                else
                {
                    Error += Environment.NewLine + "Required : Location Types";
                }
            }
            if (!string.IsNullOrEmpty(Error))
            {
                ENUtils.ShowMessage(Error);
                return;
            }
            Save();
            this.Close();
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        void frmZones_Shown(object sender, EventArgs e)
        {
            var list=General.GetQueryable<Gen_LocationType>(c=>c.Id>0).ToList();
           foreach (var item in list)
        	{
            objLocationType.Add(new Gen_LocationType { Id = item.Id, LocationType = item.LocationType, ShortCutKey = item.ShortCutKey });
           }
            
        }
        private void FillCombo()
        {
            ComboFunctions.FillLocationTypeCombo(ddlLocations);
            ComboFunctions.FillLocationTypeCombo(ddlLocationTypes2);
        }

        #region Overridden Methods


      

        



        public override void Save()
        {
            try
            {

                int LocationTypeId = ddlLocations.SelectedValue.ToInt();

             
                string ExcludeIds = string.Join(",", (grdPostCodes.Rows.Where(c =>
                              (c.Cells[COLS.Exclude].Value.ToBool() == true && (c.Cells[COLS.Id].Value.ToInt() > 0))
                             )
                             .Select(c => c.Cells[COLS.Id].Value.ToString()).ToArray<string>()));
                

                var Selectlist = (from a in grdPostCodes.Rows.Where(c => (c.Cells[COLS.Select].Value.ToBool() == true) && (c.Cells[COLS.Id].Value.ToInt()==0))
                                  select new
                                  {
                                      AddressName = a.Cells[COLS.AddressName].Value
                                  }).ToList();


                string ExcludeList = string.Empty; //"delete from Gen_Locations where Id in("+ExcludeIds+");";   
                if (!string.IsNullOrEmpty(ExcludeIds))
                {
                    ExcludeList = "delete from Gen_Locations where Id in(" + ExcludeIds + ");";
                }


                StringBuilder sb = new StringBuilder();
                string PostCode = string.Empty;
                string ShortKey = string.Empty;
                string LocationName = string.Empty;
                sb.Append(ExcludeList);
                foreach (var item in Selectlist)
                {
                    PostCode=General.GetPostCodeMatch(item.AddressName.ToStr()).Trim();
                    LocationName=item.AddressName.ToStr();

                    if(PostCode.Length > 0)
                        LocationName=LocationName.Replace(PostCode,"").ToStr().Trim();

                    if(LocationName.Contains("'"))
                          LocationName = LocationName.Replace("'", "").Trim();


                    ShortKey = objLocationType.Where(c => c.Id == LocationTypeId).FirstOrDefault().ShortCutKey.ToStr().ToLower();
                    ShortKey += " " + LocationName[0].ToStr().ToLower();
                    sb.Append("insert into Gen_Locations (LocationName,LocationTypeId,Address,PostCode,ShortCutKey) values('"+LocationName+"',"+LocationTypeId+",'"+item.AddressName.ToStr().Replace("'","").Trim()+"','"+PostCode+"','"+ShortKey+"');");
                }


                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.stp_RunProcedure(sb.ToStr());
                }

                
              
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
          
        }


    
        public override void  AddNew()
        {
         	 OnNew();
        }

        public override void  OnNew()
        {
 	        grdPostCodes.Rows.Clear();
            //txtPostCode.Focus();
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
            grdPostCodes.EnableFiltering = true;

            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS.Select;
            cbcol.HeaderText = "";
            cbcol.Width = 50;
            grdPostCodes.Columns.Add(cbcol);
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.Id;
            grdPostCodes.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.MasterId;
            grdPostCodes.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Address";
            col.Name = COLS.AddressName;
            col.Width = 260;
            col.ReadOnly = true;
            grdPostCodes.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.LocationName;
            col.IsVisible = false;
            grdPostCodes.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.LocationTypeId;
            col.IsVisible = false;
            grdPostCodes.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.PostCode;
            col.IsVisible = false;
            grdPostCodes.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.ShortCutKey;
            col.IsVisible = false;
            grdPostCodes.Columns.Add(col);

            cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS.Exclude;
            cbcol.HeaderText = "";
            cbcol.Width = 50;
            grdPostCodes.Columns.Add(cbcol);
            cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS.IsExists;
            cbcol.IsVisible = false;
            grdPostCodes.Columns.Add(cbcol);
            grdPostCodes.CommandCellClick += new CommandCellClickEventHandler(grdPostCodes_CommandCellClick);

           // UI.GridFunctions.AddDeleteColumn(grdPostCodes);
            //GridViewCommandColumn cmdcol = new GridViewCommandColumn();
            //cmdcol.Width = 70;

            //cmdcol.Name = "coldelete";
            //cmdcol.UseDefaultText = true;
            //cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            //cmdcol.DefaultText = "Delete";
            //cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
          
            //grdPostCodes.Columns.Add(cmdcol);



        }

        void grdPostCodes_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "coldelete")
            {
                RadGridView grid = gridCell.GridControl;
                grid.CurrentRow.Delete();
            }   
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

        }


        private void grdPostCodes_DoubleClick(object sender, EventArgs e)
        {
            if (grdPostCodes.CurrentRow != null && grdPostCodes.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdPostCodes.CurrentRow;
              
                //txtPostCode.Text = row.Cells[COLS.PostCode].Value.ToStr();


            }
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
    }
}
