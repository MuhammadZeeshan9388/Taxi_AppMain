using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_Model;
using Taxi_BLL;
using DAL;
using Utils;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmVehicleOrder : UI.SetupBase
    {
        VehicleTypeBO objMaster = null;
        public frmVehicleOrder()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmVehicleOrder_Load);
            objMaster = new VehicleTypeBO();


            this.SetProperties((INavigation)objMaster);
        }

        void frmVehicleOrder_Load(object sender, EventArgs e)
        {
            var list = General.GetQueryable<Fleet_VehicleType>(null).OrderBy(c => c.OrderNo).ToList();


            Font font = new Font("Tahoma", 9, FontStyle.Regular);
            RadListDataItem listItem = null;
            foreach (var item in list)
            {

                listItem = new RadListDataItem();
                listItem.Font = font;
                listItem.Text = item.VehicleType;
                listItem.Value = item.Id;

                lstVehicleOrder.Items.Add(listItem);

            }


            if (lstVehicleOrder.Items.Count > 0)
                lstVehicleOrder.Items[0].Selected = true;

            //lstZones.DataSource = list;
            //lstZones.DisplayMember = "Zone";
            //lstZones.ValueMember = "Id";


        }


        private void MoveUp(RadListControl listBox)
        {
            if (listBox.Items.Count < 2)
            {
                return;
            }
            if (listBox.SelectedItem == null)
            {
                return;
            }
            if (listBox.SelectedIndex == 0)
            {
                return;
            }
            RadListDataItem item = listBox.SelectedItem;
            int index = listBox.SelectedIndex;
            listBox.Items.Remove(item);
            listBox.Items.Insert(index - 1, item);
            listBox.SelectedItem = item;
        }


        private void MoveDown(RadListControl listBox)
        {
            if (listBox.Items.Count < 2)
            {
                return;
            }
            if (listBox.SelectedItem == null)
            {
                return;
            }
            if (listBox.SelectedIndex == listBox.Items.Count - 1)
            {
                return;
            }
            RadListDataItem item = listBox.SelectedItem;
            int index = listBox.SelectedIndex;
            listBox.Items.Remove(item);
            listBox.Items.Insert(index + 1, item);
            listBox.SelectedItem = item;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            MoveUp(lstVehicleOrder);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {

        }

        private void btnMoveDownZone_Click(object sender, EventArgs e)
        {
            MoveDown(lstVehicleOrder);
        }

        private  void Save()
        {


            try
            {
                int orderNo = 1;



                if (lstVehicleOrder.Items[0].Value.ToInt() != AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt()) 
                {

                    ENUtils.ShowMessage("Default Vehicle should be on Top in the order");
                    return;

                }

              
                foreach (RadListDataItem item in lstVehicleOrder.Items)
                {

                    //    objMaster = new ZoneBO();
                    objMaster.GetByPrimaryKey(item.Value);

                    if (objMaster.Current != null)
                    {
                        objMaster.CheckDataValidation = false;
                        objMaster.Current.OrderNo = orderNo++;

                        objMaster.Save();
                    }
                }


                General.RefreshListWithoutSelected<frmVehicleTypeList>("frmVehicleTypeList1");

                Close();

                //General.LoadZoneList();

            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objMaster.ShowErrors());

                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }




            }
       }

        private void btnMoveUp_Click_1(object sender, EventArgs e)
        {
            MoveUp(lstVehicleOrder);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

     

       
    }
}
