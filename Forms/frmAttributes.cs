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
    public partial class frmAttributes : UI.SetupBase
    
       
    {
        public struct Col_Attributes
        {
            public static string ID = "Id";


            public static string NAME = "Name";
            public static string SHORTNAME = "ShortName";
            public static string Default = "Default";
           
        }

        AttributesBO ObjMaster;


        public frmAttributes()
        {
            InitializeComponent();
            InitializeConstructor();
            grdAttributes.CurrentRow = null;
            txtAtrributeName.Focus();

        }

        private void InitializeConstructor()
        {

            ObjMaster = new AttributesBO();
            this.SetProperties((INavigation)ObjMaster);
            this.InitializeForm(this.Name);
            this.Shown += new EventHandler(frmAttribute_Shown);
            this.FormClosed += new FormClosedEventHandler(frmAttribute_FormClosed);
            //txtShortCutKey.Enabled = false;
            //txtShortCutKey.Text = "";
            this.KeyDown += new KeyEventHandler(frmAttribute_KeyDown);
            
            ObjMaster.SearchConditions = c => c.Id > 0;
            grdAttributes.MasterTemplate.ShowRowHeaderColumn = false;



            grdAttributes.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdAttributes.AllowAddNewRow = false;
            grdAttributes.ShowRowHeaderColumn = false;
            grdAttributes.ShowGroupPanel = false;


            grdAttributes.TableElement.AlternatingRowColor = Color.AliceBlue;



            grdAttributes.RowsChanging += new GridViewCollectionChangingEventHandler(grdAttributes_RowsChanging);
            grdAttributes.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            OnNew();
            txtAtrributeName.Focus();
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
            grdAttributes.CurrentRow = null;
            txtAtrributeName.Focus();
            
        }
        void frmAttribute_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose(true);

            GC.Collect();

        }

        private void LoadAttributesGrid()
        {

            var query = (from a in General.GetQueryable<Gen_Attribute>(null)
                  

                         select new
                         {
                             Id = a.Id,
                             Name = a.Name + " [" + a.ShortName + "]",
                             ShortName = a.ShortName,
                             Default = a.IsDefault,


                         }).OrderBy(c => c.Name);


            grdAttributes.DataSource = query.ToList();

        }

        private void frmAttributes_Load(object sender, EventArgs e)
        {


            LoadAttributesGrid();


            grdAttributes.Columns[Col_Attributes.ID].IsVisible = false;

            grdAttributes.Columns[Col_Attributes.NAME].Width = 300;
            grdAttributes.Columns[Col_Attributes.SHORTNAME].Width = 250;
            grdAttributes.Columns[Col_Attributes.SHORTNAME].IsVisible = false;
            grdAttributes.Columns[Col_Attributes.Default].IsVisible = false;



            if (this.CanDelete)
            {

                AddCommandColumn("btnDelete", "Delete", 70);
                //  grdUsers.AddDeleteColumn();
            }

            UI.GridFunctions.SetFilter(grdAttributes);
            txtAtrributeName.Focus();

        }

        private void AddCommandColumn(string name, string headerText, int width)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = width;

            col.UseDefaultText = true;
            col.DefaultText = headerText;
            col.Name = name;
            grdAttributes.Columns.Add(col);
            txtAtrributeName.Focus();

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
                    if (grdAttributes.CurrentRow == null)
                        return;
                    AttributesBO objMaster = new AttributesBO();

                    try
                    {
                       


                        objMaster.GetByPrimaryKey(grdAttributes.CurrentRow.Cells["Id"].Value.ToInt());
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

                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a user ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    grid.CurrentRow.Delete();
                    grid.Refresh();
                    txtAtrributeName.Text = "";
                    txtShortName.Text = "";
                    chkIsDefault.Checked = false;
                    txtAtrributeName.Focus();
                }
            }

        }

        private void grdAttributes_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdAttributes.CurrentRow == null) return;


            ObjMaster.GetByPrimaryKey(grdAttributes.CurrentRow.Cells[Col_Attributes.ID].Value);
            DisplayRecord();

        }




        #region Overridden Methods

        public override void DisplayRecord()
        {
            if (ObjMaster.Current == null) return;

            txtAtrributeName.Text = ObjMaster.Current.Name;
            txtShortName.Text = ObjMaster.Current.ShortName;
            if (ObjMaster.Current.IsDefault == true)
            {
                chkIsDefault.Checked = Convert.ToBoolean(ObjMaster.Current.IsDefault);
            }
            else
            {
                chkIsDefault.Checked = false;
            }

            grdAttributes.CurrentRow = null;
            txtAtrributeName.Focus();

            OnNew();
        }

        public override void AddNew()
        {
           OnNew();
        }

        public override void OnNew()
        {

            try
            {
                grdAttributes.CurrentRow = null;
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

                ObjMaster.Current.Name = txtAtrributeName.Text.Trim();
                ObjMaster.Current.ShortName = txtShortName.Text.Trim();
               // ObjMaster.Current.AttributeCategoryId = Enums.ATTRIBUTE_CATEGORY.Driver;
                if (chkIsDefault.Checked == true)
                {
                    ObjMaster.Current.IsDefault = chkIsDefault.Checked;
                }
                else 
                {
                    ObjMaster.Current.IsDefault = false;
                }
                ObjMaster.Save();

                PopulateData();
                OnNew();

                grdAttributes.CurrentRow = null;
                txtAtrributeName.Focus();



               

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

            LoadAttributesGrid();

        }


        #endregion

        private void btnOnNew_Click(object sender, EventArgs e)
        {

            txtAtrributeName.Text = "";
            txtShortName.Text = "";
            //grdAttributes.Refresh();
            grdAttributes.CurrentRow = null;
            txtAtrributeName.Focus();
            chkIsDefault.Checked = false;
        
        }

        private void radLabel2_Click(object sender, EventArgs e)
        {

        }

        private void txtAtrributeName_TextChanged(object sender, EventArgs e)
        {

        }

       

      

       



    }
}
