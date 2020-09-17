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
    public partial class frmPlotZone : UI.SetupBase
    {
        ZoneBO objMaster = null;
        public frmPlotZone()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmPlotZone_Load);
            objMaster = new ZoneBO();


            this.SetProperties((INavigation)objMaster);
        }

        void frmPlotZone_Load(object sender, EventArgs e)
        {
            var list = (from a in General.GetQueryable<Gen_Zone>(c=>c.MinLatitude!=null)
                        orderby a.OrderNo
                        select new
                        {
                            Id=a.Id,
                            Zone=a.ZoneName + " [" + a.PostCode + "]",

                        }).ToList();


            Font font = new Font("Tahoma", 9, FontStyle.Regular);
            RadListDataItem listItem = null;
            foreach (var item in list)
            {

                listItem = new RadListDataItem();
                listItem.Font = font;
                listItem.Text = item.Zone;
                listItem.Value = item.Id;

                lstZones.Items.Add(listItem);
                  
            }


            if (lstZones.Items.Count > 0)
                lstZones.Items[0].Selected = true;

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
            MoveUp(lstZones);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
         
        }

        private void btnMoveDownZone_Click(object sender, EventArgs e)
        {
            MoveDown(lstZones);
        }

        public override void Save()
        {
                

            try
            {
                int orderNo=1;
                foreach (RadListDataItem item in lstZones.Items)
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


                General.LoadZoneList();

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
    }
}
