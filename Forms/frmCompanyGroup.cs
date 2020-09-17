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
using Telerik.WinControls.Enumerations;
using Taxi_BLL.Classes;
using Telerik.WinControls;


namespace Taxi_AppMain
{
    public partial class frmCompanyGroup : UI.SetupBase
    
       
    {
        public struct Col_Group

        {
            public static string ID = "Id";
            public static string NAME = "Name";
        
           
        }

        GroupBO ObjMaster;


        public frmCompanyGroup()
        {
            InitializeComponent();
            InitializeConstructor();
            grdCompanyGroup.CurrentRow = null;
            txtGroupName.Focus();

        }

        private void InitializeConstructor()
        {

            ObjMaster = new GroupBO();
            this.SetProperties((INavigation)ObjMaster);
            this.InitializeForm(this.Name);
            this.Shown += new EventHandler(frmAttribute_Shown);
            this.FormClosed += new FormClosedEventHandler(frmAttribute_FormClosed);
            //txtShortCutKey.Enabled = false;
            //txtShortCutKey.Text = "";
            this.KeyDown += new KeyEventHandler(frmAttribute_KeyDown);
            
            ObjMaster.SearchConditions = c => c.Id > 0;
            grdCompanyGroup.MasterTemplate.ShowRowHeaderColumn = false;
            
            grdCompanyGroup.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdCompanyGroup.AllowAddNewRow = false;
            grdCompanyGroup.ShowRowHeaderColumn = false;
            grdCompanyGroup.ShowGroupPanel = false;
            
            grdCompanyGroup.TableElement.AlternatingRowColor = Color.AliceBlue;
                        
            grdCompanyGroup.RowsChanging += new GridViewCollectionChangingEventHandler(grdAttributes_RowsChanging);
            grdCompanyGroup.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            OnNew();
            txtGroupName.Focus();
            //this.Load += new EventHandler(frmAttributes_Load);


        }

        void frmAttribute_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        void frmAttribute_Shown(object sender, EventArgs e)
        {
            grdCompanyGroup.CurrentRow = null;
            txtGroupName.Focus();
            
        }
        void frmAttribute_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose(true);

            GC.Collect();

        }

        private void LoadCompanyGroupGrid()
        {

            var query = (from a in General.GetQueryable<Gen_CompanyGroup>(null)
                  

                         select new
                         {
                             Id = a.Id,
                             Name = a.GroupName

                         }).OrderBy(c => c.Name);


            grdCompanyGroup.DataSource = query.ToList();

        }

        private void frmAttributes_Load(object sender, EventArgs e)
        {
            
            LoadCompanyGroupGrid();
            
            grdCompanyGroup.Columns[Col_Group.ID].IsVisible = false;
            grdCompanyGroup.Columns[Col_Group.NAME].Width = 300;


            if (this.CanDelete)
            {

                AddCommandColumn("btnDelete", "Delete", 70);
                //  grdUsers.AddDeleteColumn();
            }

            UI.GridFunctions.SetFilter(grdCompanyGroup);
            txtGroupName.Focus();

        }

        private void AddCommandColumn(string name, string headerText, int width)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = width;

            col.UseDefaultText = true;
            col.DefaultText = headerText;
            col.Name = name;
            grdCompanyGroup.Columns.Add(col);
            txtGroupName.Focus();

        }

        private void grdAttributes_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                if (this.CanDelete == false)
                {
                    // ENUtils.ShowMessage("Permission Denied");
                    e.Cancel = true;
                }
                else
                {
                    if (grdCompanyGroup.CurrentRow == null)
                        return;
                    GroupBO objMaster = new GroupBO();

                    try
                    {
                       

                        objMaster.GetByPrimaryKey(grdCompanyGroup.CurrentRow.Cells["Id"].Value.ToInt());
                        if (objMaster.Current != null)
                        {
                           // string Name = objMaster.Current.Name.ToStr();
                           // string ShortName = objMaster.Current.ShortName.ToStr();
                           // bool IsDefaultCheck = Convert.ToBoolean(ObjMaster.Current.IsDefault);

                            objMaster.Delete(objMaster.Current);

                            OnNew();


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

        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            RadGridView grid = gridCell.GridControl;
            if (gridCell.ColumnInfo.Name == "btnDelete")
            {

                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Group ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    grid.CurrentRow.Delete();
                    grid.Refresh();
                    txtGroupName.Text = "";
                    txtGroupName.Focus();
                }
            }

        }

        private void grdAttributes_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdCompanyGroup.CurrentRow == null) return;


            ObjMaster.GetByPrimaryKey(grdCompanyGroup.CurrentRow.Cells[Col_Group.ID].Value);
            DisplayRecord();

        }




        #region Overridden Methods

        public override void DisplayRecord()
        {
            if (ObjMaster.Current == null) return;

            txtGroupName.Text = ObjMaster.Current.GroupName;
                      
            grdCompanyGroup.CurrentRow = null;
            txtGroupName.Focus();

           // OnNew();
        }

        public override void AddNew()
        {
           OnNew();
        }

        public override void OnNew()
        {

            try
            {
                grdCompanyGroup.CurrentRow = null;
                ObjMaster.New();
            }
            catch
            {


            }
          //txtAtrributeName.CharacterCasing = CharacterCasing.Normal;
          //txtShortName.CharacterCasing = CharacterCasing.Normal;
          //txtAtrributeName.Focus();
        }

        public override void Save()
        {
            try
            {

                if (ObjMaster.PrimaryKeyValue == null)
                {
                    ObjMaster.New();
                }
                else
                {
                    ObjMaster.Edit();
                }

                ObjMaster.Current.GroupName = txtGroupName.Text.Trim();
                         
                ObjMaster.Save();

                PopulateData();
                OnNew();

                grdCompanyGroup.CurrentRow = null;
                txtGroupName.Focus();

            }
            catch (Exception ex)
            {
                if (ObjMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(ObjMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }
        }

        public override void PopulateData()
        {

            LoadCompanyGroupGrid();

        }



        #endregion

        private void btnOnNew_Click(object sender, EventArgs e)
        {

            txtGroupName.Text = "";
         
            //grdAttributes.Refresh();
            grdCompanyGroup.CurrentRow = null;
            txtGroupName.Focus();
                   
        }



    }
}
