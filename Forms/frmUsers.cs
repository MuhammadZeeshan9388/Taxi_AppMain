using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using UI;
using Taxi_BLL;

using Taxi_Model;
using Telerik.WinControls;
using Utils;
using Telerik.WinControls.UI;

namespace Taxi_AppMain
{
    public partial class frmUsers : SetupBase
    {
        public struct Col_Users
        {
            public static string ID = "Id";
            public static string PHOTO = "Photo";

            public static string USERNAME = "UserName";
            public static string EMAIL = "Email";
            public static string PHONE = "Phone";
            public static string Group = "Group";


        }
        UserBO objMaster;

        public frmUsers()
        {
            InitializeComponent();
            objMaster=new UserBO();
            objMaster.SearchConditions = c => c.Id > 0;
            grdUsers.MasterTemplate.ShowRowHeaderColumn = false;

            this.SetProperties((INavigation)objMaster);


            grdUsers.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdUsers.AllowAddNewRow = false;
            grdUsers.ShowRowHeaderColumn = false;
            grdUsers.ShowGroupPanel = false;


            grdUsers.TableElement.AlternatingRowColor = Color.AliceBlue;

            FillCombos();
            FormatExtensionGrid();
            grdUsers.RowsChanging += new GridViewCollectionChangingEventHandler(grdUsers_RowsChanging);
            grdUsers.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            this.chkCallReceiver.ToggleStateChanged += new StateChangedEventHandler(chkCallReceiver_ToggleStateChanged);
            OnNew();
        }

        void chkCallReceiver_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkCallReceiver.Checked)
            {
                grdExtension.Visible = true;
            }
            else
            {
                grdExtension.Rows.Clear();
                grdExtension.Visible = false;
            }
        }


        private void AddCommandColumn(string name, string headerText, int width)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = width;

            col.UseDefaultText = true;
            col.DefaultText = headerText;
            col.Name = name;
            grdUsers.Columns.Add(col);

        }
        public struct COLS
        {
            public static string Id = "Id";
            public static string UserId = "ClientId";
            public static string UserExtension = "UserExtension";
        }
        public void FormatExtensionGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();

            col.Name = COLS.UserId;
            col.IsVisible = false;
            //col.ReadOnly = false;
            grdExtension.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdExtension.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.UserExtension;
            col.HeaderText = "User Extension";
            col.Width = 150;
            //col.ReadOnly = false;
            grdExtension.Columns.Add(col);
        }


        private void LoadUserGrid()
        {

            var query = from a in General.GetQueryable<UM_User>(c=>c.SecurityGroupId!=null && c.UserName!="eurosoft")
                       // join b in AppVars.BLData.GetAll<UM_SecurityGroup>()
                     //   on a.SecurityGroupId equals b.Id
                        select new
                        {
                            Id = a.Id,
                            //      Photo = Functions.byteArrayToImage(a.Photo),
                            UserName = a.UserName,
                            Email = a.Email,
                            Phone = a.Phone,
                            Group =a.UM_SecurityGroup.GroupName// b.GroupName
                        };


            grdUsers.DataSource = query.ToList();
           // this.SetRefreshingProperties(AppVars.BLData.GetCommand(query), grdUsers, true);


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
                }
            }
           
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
        

            LoadUserGrid();

        
         //   grdUsers.Columns["ColDelete"].Width = 70;
            grdUsers.Columns[Col_Users.ID].IsVisible = false;
            //       grdUsers.Columns[Col_Users.PHOTO].ImageLayout= ImageLayout.Stretch;

            grdUsers.Columns[Col_Users.USERNAME].Width = 250;
            grdUsers.Columns[Col_Users.EMAIL].Width = 180;
            grdUsers.Columns[Col_Users.PHONE].Width = 100;
            grdUsers.Columns[Col_Users.Group].Width = 200;


            if (this.CanDelete)
            {

                AddCommandColumn("btnDelete", "Delete", 70);
              //  grdUsers.AddDeleteColumn();
            }

            UI.GridFunctions.SetFilter(grdUsers);

        }

        void grdUsers_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
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
                    UserBO objMaster = new UserBO();

                    try
                    {


                        objMaster.GetByPrimaryKey(grdUsers.CurrentRow.Cells["Id"].Value.ToInt());
                        if (objMaster.Current != null)
                        {
                            string userName = objMaster.Current.UserName.ToStr();
                            string password = objMaster.Current.Passwrd.ToStr();
                            string groupName = objMaster.Current.UM_SecurityGroup != null ? objMaster.Current.UM_SecurityGroup.GroupName : "";
                           
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

        public override void  RefreshData()
        {
            PopulateData();
        }

        public override void PopulateData()
        {

            LoadUserGrid();
 
        }


       

        private void FillCombos()
        {
            ComboFunctions.FillSecGroupsComboExcRoot(ddlGroups);
            ComboFunctions.FillSubCompanyCombo(ddlSubCompany);

            ThemeList themeList = ThemeResolutionService.GetAvailableThemes();
            if (themeList.Count() > 0)
            {
                ddlThemes.DisplayMember = "ThemeName";
                ddlThemes.ValueMember = "ThemeName";
                ddlThemes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                ddlThemes.DataSource = themeList.Distinct().Where(t=>!t.ThemeName.EndsWith("*") && t.ThemeName!="BreezeExtended").OrderBy(t=>t.ThemeName).ToList();
            }
          
            ddlThemes.SelectedIndex = -1;
            ddlThemes.NullText="Select";
            ddlGroups.SelectedIndex = -1;


            if (ddlSubCompany.Items.Count > 0)
                chkAllowTransferBooking.Visible = true;

        }




        #region Overriden Methods
       

        public override void DisplayRecord()
        {

            if (objMaster.Current != null)
            {
                txtId.Text = objMaster.Current.Id.ToStr(); ;
                txtUserName.Text = objMaster.Current.UserName;
                txtPassword.Text = objMaster.Current.Passwrd;
                txtCPassword.Text = objMaster.Current.Passwrd;
                txtPhone.Text = objMaster.Current.Phone;
                txtEmail.Text = objMaster.Current.Email;

                chkTelephonist.Checked = objMaster.Current.Fax.ToStr() == "1" ? true : false;
                // txtFax.Text = objMaster.Current.Fax;


                chkSpecial.Checked = objMaster.Current.ConfirmPasswrd.ToStr() == "1" ? true : false;
                chkShowDrvFilter.Checked = objMaster.Current.ShowDriverFilter.ToBool();
                chkShowBookingFilter.Checked = objMaster.Current.ShowBookingFilter.ToBool();

                chkShowAllBookings.Checked = objMaster.Current.ShowAllBookings.ToBool();
                chkShowAllDrivers.Checked = objMaster.Current.ShowAllDrivers.ToBool();


                chkCallReceiver.Checked = objMaster.Current.AvailableUser.ToBool();

                if (objMaster.Current.Photo != null)
                    pictureBox1.Image = General.byteArrayToImage(objMaster.Current.Photo.ToArray());
                else
                    pictureBox1.Image = null;
                
                ddlThemes.SelectedValue = objMaster.Current.ThemeName;
                ddlGroups.SelectedValue = objMaster.Current.SecurityGroupId.ToIntorNull();

                ddlSubCompany.SelectedValue = objMaster.Current.SubcompanyId;

                chkActive.Checked = objMaster.Current.IsActive.ToBool();
                chkAllowTransferBooking.Checked = objMaster.Current.TransferBooking.ToBool();

                grdExtension.Rows.Clear();
                if (chkCallReceiver.Checked)
                {
                    var list = (from a in General.GetQueryable<UM_UserExtension>(c => c.UserId == objMaster.Current.Id)
                                select new
                                {
                                    Id = a.Id,
                                    UserId = a.UserId,
                                    UserExtension = a.UserExtension
                                }).ToList();
                    GridViewRowInfo row = null;
                    foreach (var item in list)
                    {
                        row = grdExtension.Rows.AddNew();
                        row.Cells[COLS.Id].Value = item.Id;
                        row.Cells[COLS.UserId].Value = item.UserId;
                        row.Cells[COLS.UserExtension].Value = item.UserExtension;
                    }
                }
                
            }


        }

        public override void AddNew()
        {
            OnNew();
            
        }


        public override void OnNew()
        {
       
            txtId.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtCPassword.Text = string.Empty;
            txtEmail.Text = string.Empty;
            chkTelephonist.Checked = false;
          //  txtFax.Text = string.Empty;
            txtPhone.Text = string.Empty;
            ddlGroups.SelectedIndex = -1;
            ddlThemes.SelectedIndex = 1;
            pictureBox1.Image = null;
            txtUserName.Focus();
            chkActive.Checked = true;
            chkSpecial.Checked = false;
            chkAllowTransferBooking.Checked = false;
            grdExtension.Rows.Clear();
            ddlSubCompany.SelectedValue = General.GetObject<Gen_SubCompany>(c => c.IsSysGen != null && c.IsSysGen == true).DefaultIfEmpty().Id;

        }
     

        public override void Save()
        {
         
            try
            {
            

                if (txtId.Text == string.Empty)
                {
                   

                    objMaster.New();
                    objMaster.Current.AddBy = this.LoginUser.LuserId.ToInt();
          
                    objMaster.Current.AddOn = DateTime.Now;
                 
            
                }
                else
                {
                      objMaster.Edit();
                      objMaster.Current.EditBy = this.LoginUser.LuserId.ToInt();
                
                      objMaster.Current.EditOn = DateTime.Now;         
                   
                }


              
                


                objMaster.Current.UserName = txtUserName.Text.ToStr();
                objMaster.Current.Passwrd = txtPassword.Text.ToStr();
                objMaster.Current.AvailableUser = chkCallReceiver.Checked;


                objMaster.Current.ConfirmPasswrd = chkSpecial.Checked ? "1" : "0";
                objMaster.Current.Phone = txtPhone.Text.ToStr();
                objMaster.Current.Fax = chkTelephonist.Checked ? "1" : "0";
                objMaster.Current.Email = txtEmail.Text.ToStr();
                objMaster.Current.ThemeName ="ControlDefault";
                objMaster.Current.SecurityGroupId = ddlGroups.SelectedValue.ToIntorNull();

                objMaster.Current.ShowBookingFilter = chkShowBookingFilter.Checked;
                objMaster.Current.ShowDriverFilter = chkShowDrvFilter.Checked;

                objMaster.Current.ShowAllBookings = chkShowAllBookings.Checked;
                objMaster.Current.ShowAllDrivers = chkShowAllDrivers.Checked;


                if (pictureBox1.Image != null)
                {

                    objMaster.Current.Photo = General.imageToByteArray(pictureBox1.Image);
                }
                else
                    objMaster.Current.Photo = null;

        
                objMaster.Current.IsActive = chkActive.Checked;

                objMaster.Current.SubcompanyId = ddlSubCompany.SelectedValue.ToIntorNull();

                objMaster.ConfirmPwd = txtCPassword.Text.ToStr();

                objMaster.Current.TransferBooking = chkAllowTransferBooking.Checked;
                if (chkCallReceiver.Checked && grdExtension.Visible)
                {
                    string[] skipProperties = { "UM_Users" };
                    IList<UM_UserExtension> savedList = objMaster.Current.UM_UserExtensions;
                    List<UM_UserExtension> savedExtension = (from saved in grdExtension.Rows
                                                             select new UM_UserExtension
                                                             {
                                                                 Id = saved.Cells[COLS.Id].Value.ToInt(),
                                                                 UserId = saved.Cells[COLS.UserId].Value.ToInt(),
                                                                 UserExtension = saved.Cells[COLS.UserExtension].Value.ToStr(),

                                                             }).ToList();
                    Utils.General.SyncChildCollection(ref savedList, ref savedExtension, "Id", skipProperties);
                }
                else
                {
                    objMaster.Current.UM_UserExtensions.Clear();
                }
                objMaster.Save();


                PopulateData();
                OnNew();

             

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


        public override void Delete()
        {
            try
            {
                if (objMaster.Current == null) return;

                 objMaster.Delete(objMaster.Current);
      

               
                OnNew();
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


        #endregion




        private void grdUsers_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdUsers.CurrentRow == null) return;
         
       
            objMaster.GetByPrimaryKey(grdUsers.CurrentRow.Cells[Col_Users.ID].Value);
            DisplayRecord();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" + "All files (*.*)|*.*";
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);



            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            frmPreview frm = new frmPreview();
            frm.ImageFile = pictureBox1.Image;
            frm.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = null;
        }

        private void frmUsers_Shown(object sender, EventArgs e)
        {
           
            this.txtUserName.Focus();

          
        }

        private void btnViewTheme_Click(object sender, EventArgs e)
        {

            string themeName=ddlThemes.SelectedValue.ToStr().ToLower().Trim();

            ChangeApplicationTheme(themeName);
        }
        private void ChangeApplicationTheme(string themeName)
        {
            if(string.IsNullOrEmpty(themeName))
                return;

         /*   switch (themeName)
            {
                case "Office2010Black":
                    new Office2010BlackTheme();
                    break;
                case "Office2010Blue":
                    new Office2010BlueTheme();
                    break;
                case "Office2010SIlver":
                    new Office2010SilverTheme();
                    break;
            }*/

            ThemeResolutionService.ApplicationThemeName = themeName;
        }



      


    }
}
