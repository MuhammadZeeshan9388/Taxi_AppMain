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
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Taxi_AppMain
{
    public partial class frmCompanyCharges : UI.SetupBase
    {
        public struct COL_CompanyCharges
        {
            public static string Id = "Id";
            public static string Charges = "Charges";
            public static string CompanyName = "CompanyName";
            public static string Check = "Check";
            public static string MileageId = "MileageId";
            public static string WaitingTimeId = "Waiting TimeId";
            public static string ParkingChargesId = "Parking ChargesId";
            public static string ExtraChargesId = "Extra ChargesId";
            public static string FaresId = "FaresId";
            public static string PassengerId = "PassengerId";
            public static string SignatureId = "SignatureId";
            public static string Mileage = "Mileage";
            public static string WaitingTime = "Waiting Time";
            public static string ParkingCharges = "Parking Charges";
            public static string ExtraCharges = "Extra Charges";
            public static string Fares = "Fares";
            public static string Passenger = "Passenger";
            public static string Signature = "Signature";
            public static bool MileageIsvisible;
            public static bool WaitingTimeIsvisible;
            public static bool ParkingChargesIsvisible;
            public static bool ExtraChargesIsvisible;
            public static bool FaresIsvisible;
            public static bool PassengerIsvisible;
            public static bool SignatureIsvisible;

        }
        public static bool val = false;
        CompanyBO objMaster;

        public frmCompanyCharges()
        {
            InitializeComponent();
            objMaster = new CompanyBO();
            this.SetProperties((INavigation)objMaster);
            FormatExtraChargesGrid();
            this.Shown += new EventHandler(frmCompanyCharges_Shown);
        }

        void frmCompanyCharges_Shown(object sender, EventArgs e)
        {
            LoadAdditionalCharges();
            DisplayAdditionalCharges();
        }


        private void DisplayAdditionalCharges()
        {

            var xlist = (from a in General.GetQueryable<Gen_Company_ExtraCharge>(c => c.Id > 0)
                         select new
                         {
                             Id = a.Id,
                             CompanyId = a.CompanyId,
                             ChargeId = a.Charges,
                             IsChecked = true,
                             ChargesName = a.Gen_Charge != null ? a.Gen_Charge.ChargesName : null,
                         }).ToList();


            for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
            {

                foreach (var item in xlist)
                {

                    if (grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Id].Value.ToInt() == item.CompanyId && grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.MileageId].Value.ToInt() == item.ChargeId)
                    {
                        grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Mileage].Value = true;
                    }
                    if (grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Id].Value.ToInt() == item.CompanyId && grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.FaresId].Value.ToInt() == item.ChargeId)
                    {
                        grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Fares].Value = true;
                    }
                    if (grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Id].Value.ToInt() == item.CompanyId && grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.WaitingTimeId].Value.ToInt() == item.ChargeId)
                    {
                        grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.WaitingTime].Value = true;
                    }
                    if (grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Id].Value.ToInt() == item.CompanyId && grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.ParkingChargesId].Value.ToInt() == item.ChargeId)
                    {
                        grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.ParkingCharges].Value = true;
                    }
                    if (grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Id].Value.ToInt() == item.CompanyId && grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.ExtraChargesId].Value.ToInt() == item.ChargeId)
                    {
                        grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.ExtraCharges].Value = true;
                    }
                    if (grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Id].Value.ToInt() == item.CompanyId && grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.PassengerId].Value.ToInt() == item.ChargeId)
                    {
                        grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Passenger].Value = true;
                    }
                    if (grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Id].Value.ToInt() == item.CompanyId && grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.SignatureId].Value.ToInt() == item.ChargeId)
                    {
                        grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Signature].Value = true;
                    }

                }
            }

        }

        private void FormatExtraChargesGrid()
        {
            grdCompanyCharges.AllowAddNewRow = false;
            grdCompanyCharges.ShowGroupPanel = false;


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COL_CompanyCharges.Id;
            col.IsVisible = false;
            grdCompanyCharges.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COL_CompanyCharges.MileageId;
            col.IsVisible = false;
            grdCompanyCharges.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COL_CompanyCharges.ExtraChargesId;
            col.IsVisible = false;
            grdCompanyCharges.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COL_CompanyCharges.FaresId;
            col.IsVisible = false;
            grdCompanyCharges.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COL_CompanyCharges.ParkingChargesId;
            col.IsVisible = false;
            grdCompanyCharges.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COL_CompanyCharges.PassengerId;
            col.IsVisible = false;
            grdCompanyCharges.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COL_CompanyCharges.SignatureId;
            col.IsVisible = false;
            grdCompanyCharges.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COL_CompanyCharges.WaitingTimeId;
            col.IsVisible = false;
            grdCompanyCharges.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COL_CompanyCharges.CompanyName;
            col.HeaderText = "Company";
            col.ReadOnly = true;
            col.Width = 120;
            grdCompanyCharges.Columns.Add(col);

            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();

            cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COL_CompanyCharges.Mileage;
            cbcol.HeaderText = "Mileage";
            cbcol.Width = 100;
            cbcol.IsVisible = false;
            grdCompanyCharges.Columns.Add(cbcol);

            cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COL_CompanyCharges.WaitingTime;
            cbcol.HeaderText = "WaitingTime";
            cbcol.Width = 100;
            cbcol.IsVisible = false;
            grdCompanyCharges.Columns.Add(cbcol);

            cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COL_CompanyCharges.ParkingCharges;
            cbcol.HeaderText = "Parking Charges";
            cbcol.Width = 100;
            cbcol.IsVisible = false;
            grdCompanyCharges.Columns.Add(cbcol);

            cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COL_CompanyCharges.ExtraCharges;
            cbcol.HeaderText = "Extra Charges";
            cbcol.Width = 100;
            cbcol.IsVisible = false;
            grdCompanyCharges.Columns.Add(cbcol);

            cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COL_CompanyCharges.Fares;
            cbcol.HeaderText = "Fares";
            cbcol.Width = 100;
            cbcol.IsVisible = false;
            grdCompanyCharges.Columns.Add(cbcol);

            cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COL_CompanyCharges.Passenger;
            cbcol.HeaderText = "Passenger";
            cbcol.IsVisible = false;
            cbcol.Width = 100;
            grdCompanyCharges.Columns.Add(cbcol);

            cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COL_CompanyCharges.Signature;
            cbcol.IsVisible = false;
            cbcol.HeaderText = "Signature";
            cbcol.Width = 100;
            grdCompanyCharges.Columns.Add(cbcol);

            chkMileage.Visible = false;
            chkWaitingTime.Visible = false;
            chkParkingCharges.Visible = false;
            chkExtraCharges.Visible = false;
            chkFares.Visible = false;
            chkPassenger.Visible = false;
            chkSignature.Visible = false;
        }

        private void LoadAdditionalCharges()
        {
            try
            {

                var list1 = (from b in General.GetQueryable<Gen_Charge>(c => c.Id > 0)
                             select new
                             {
                                 Id = b.Id,
                                 ChargesName = b.ChargesName,
                                 IsVisible = b.IsVisible
                             }).ToList();
                foreach (var item in list1)
                {

                    if (item.ChargesName.ToStr() == "Mileage")
                    {
                        Gen_ChargesTypeId.MileageId = item.Id;
                        Gen_ChargesTypeId.MileageIsvisble = item.IsVisible.ToBool();
                        chkMileage.Visible = item.IsVisible.ToBool();
                    }
                    else if (item.ChargesName.ToStr() == "Waiting Time")
                    {
                        Gen_ChargesTypeId.WaitingTimeId = item.Id;
                        Gen_ChargesTypeId.WaitingTimeIsvisble = item.IsVisible.ToBool();
                        chkWaitingTime.Visible = item.IsVisible.ToBool();
                    }

                    else if (item.ChargesName.ToStr() == "Parking Charges")
                    {
                        Gen_ChargesTypeId.ParkingChargesId = item.Id;
                        Gen_ChargesTypeId.ParkingChargesIsvisble = item.IsVisible.ToBool();
                        chkParkingCharges.Visible = item.IsVisible.ToBool();
                    }

                    else if (item.ChargesName.ToStr() == "Extra Charges")
                    {
                        Gen_ChargesTypeId.ExtraChargesId = item.Id;
                        Gen_ChargesTypeId.ExtraChargesIsvisble = item.IsVisible.ToBool();
                        chkExtraCharges.Visible = item.IsVisible.ToBool();
                    }

                    else if (item.ChargesName.ToStr() == "Fares")
                    {
                        Gen_ChargesTypeId.FaresId = item.Id;
                        Gen_ChargesTypeId.FaresIsvisble = item.IsVisible.ToBool();
                        chkFares.Visible = item.IsVisible.ToBool();
                    }

                    else if (item.ChargesName.ToStr() == "Passenger")
                    {
                        Gen_ChargesTypeId.PassengerId = item.Id;
                        Gen_ChargesTypeId.PassengerIsvisble = item.IsVisible.ToBool();
                        chkPassenger.Visible = item.IsVisible.ToBool();
                    }

                    else if (item.ChargesName.ToStr() == "Signature")
                    {
                        Gen_ChargesTypeId.SignatureId = item.Id;
                        Gen_ChargesTypeId.SignatureIsvisible = item.IsVisible.ToBool();
                        chkSignature.Visible = item.IsVisible.ToBool();

                    }
                }


                for (int z = 0; z < grdCompanyCharges.Columns.Count; z++)
                {

                    var objs = list1.Where(c => c.ChargesName == grdCompanyCharges.Columns[z].Name).ToList();

                    if (objs.Count > 0)
                    {
                        if (grdCompanyCharges.Columns[z].Name == objs.FirstOrDefault().ChargesName)
                        {
                            grdCompanyCharges.Columns[z].IsVisible = objs.FirstOrDefault().IsVisible.ToBool();

                        }
                    }
                }


                var list = (from a in General.GetQueryable<Gen_Company>(c => c.Id > 0)
                            select new
                            {
                                Id = a.Id,
                                CompanyName = a.CompanyName,
                            }).ToList();

                int cnt = list.Count;
                grdCompanyCharges.RowCount = cnt;
                for (int s = 0; s < cnt; s++)
                {
                    grdCompanyCharges.Rows[s].Cells[COL_CompanyCharges.Id].Value = list[s].Id;
                    grdCompanyCharges.Rows[s].Cells[COL_CompanyCharges.CompanyName].Value = list[s].CompanyName;
                    grdCompanyCharges.Rows[s].Cells[COL_CompanyCharges.MileageId].Value = Gen_ChargesTypeId.MileageId;
                    grdCompanyCharges.Rows[s].Cells[COL_CompanyCharges.WaitingTimeId].Value = Gen_ChargesTypeId.WaitingTimeId;
                    grdCompanyCharges.Rows[s].Cells[COL_CompanyCharges.ParkingChargesId].Value = Gen_ChargesTypeId.ParkingChargesId;
                    grdCompanyCharges.Rows[s].Cells[COL_CompanyCharges.ExtraChargesId].Value = Gen_ChargesTypeId.ExtraChargesId;
                    grdCompanyCharges.Rows[s].Cells[COL_CompanyCharges.FaresId].Value = Gen_ChargesTypeId.FaresId;
                    grdCompanyCharges.Rows[s].Cells[COL_CompanyCharges.PassengerId].Value = Gen_ChargesTypeId.PassengerId;
                    grdCompanyCharges.Rows[s].Cells[COL_CompanyCharges.SignatureId].Value = Gen_ChargesTypeId.SignatureId;

                }

            }

            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnSaveCompanyCharges_Click(object sender, EventArgs e)
        {
            Save();
        }

        public override void Save()
        {
            try
            {

                var list = (from a in grdCompanyCharges.Rows
                            select new
                            {
                                Id = a.Cells[COL_CompanyCharges.Id].Value.ToInt(),

                                MileageId = a.Cells[COL_CompanyCharges.MileageId].Value.ToInt(),
                                Mileage = a.Cells[COL_CompanyCharges.Mileage].Value.ToBool(),

                                FaresId = a.Cells[COL_CompanyCharges.FaresId].Value.ToInt(),
                                Fares = a.Cells[COL_CompanyCharges.Fares].Value.ToBool(),

                                ParkingChargesId = a.Cells[COL_CompanyCharges.ParkingChargesId].Value.ToInt(),
                                ParkingCharges = a.Cells[COL_CompanyCharges.ParkingCharges].Value.ToBool(),

                                WaitingTimeId = a.Cells[COL_CompanyCharges.WaitingTimeId].Value.ToInt(),
                                WaitingTime = a.Cells[COL_CompanyCharges.WaitingTime].Value.ToBool(),

                                PassengerId = a.Cells[COL_CompanyCharges.PassengerId].Value.ToInt(),
                                Passenger = a.Cells[COL_CompanyCharges.Passenger].Value.ToBool(),

                                ExtraChargesId = a.Cells[COL_CompanyCharges.ExtraChargesId].Value.ToInt(),
                                ExtraCharges = a.Cells[COL_CompanyCharges.ExtraCharges].Value.ToBool(),

                                SignatureId = a.Cells[COL_CompanyCharges.SignatureId].Value.ToInt(),
                                Signature = a.Cells[COL_CompanyCharges.Signature].Value.ToBool(),

                            }).ToList();

                int CompanyId = 0;
                foreach (var item in list)
                {
                    CompanyId = item.Id;

                    var list2 = list.Where(c => c.Id == CompanyId).ToList();

                    var list3 = (from a in list2
                                 select new
                                 {
                                     CompanyId = a.Id,
                                     MileageId = a.Mileage == true ? a.MileageId.ToIntorNull() : null,
                                     FaresId = a.Fares == true ? a.FaresId.ToIntorNull() : null,
                                     ExtraChargesId = a.ExtraCharges == true ? a.ExtraChargesId.ToIntorNull() : null,
                                     WaitingTimeId = a.WaitingTime == true ? a.WaitingTimeId.ToIntorNull() : null,
                                     ParkingChargesId = a.ParkingCharges == true ? a.ParkingChargesId.ToIntorNull() : null,
                                     PassengerId = a.Passenger == true ? a.PassengerId.ToIntorNull() : null,
                                     SignatureId = a.Signature == true ? a.SignatureId.ToIntorNull() : null,
                                 }).ToList();

                    List<Gen_Company_ExtraCharge> objCharges = new List<Gen_Company_ExtraCharge>();

                    foreach (var item3 in list3)
                    {
                        if (item3.MileageId != null)
                        {
                            objCharges.Add(new Gen_Company_ExtraCharge { CompanyId = item3.CompanyId, Charges = item3.MileageId });
                        }
                        if (item3.FaresId != null)
                        {
                            objCharges.Add(new Gen_Company_ExtraCharge { CompanyId = item3.CompanyId, Charges = item3.FaresId });
                        }
                        if (item3.ParkingChargesId != null)
                        {
                            objCharges.Add(new Gen_Company_ExtraCharge { CompanyId = item3.CompanyId, Charges = item3.ParkingChargesId });
                        }
                        if (item3.ExtraChargesId != null)
                        {
                            objCharges.Add(new Gen_Company_ExtraCharge { CompanyId = item3.CompanyId, Charges = item3.ExtraChargesId });
                        }
                        if (item3.WaitingTimeId != null)
                        {
                            objCharges.Add(new Gen_Company_ExtraCharge { CompanyId = item3.CompanyId, Charges = item3.WaitingTimeId });
                        }
                        if (item3.PassengerId != null)
                        {
                            objCharges.Add(new Gen_Company_ExtraCharge { CompanyId = item3.CompanyId, Charges = item3.PassengerId });
                        }
                        if (item3.SignatureId != null)
                        {
                            objCharges.Add(new Gen_Company_ExtraCharge { CompanyId = item3.CompanyId, Charges = item3.SignatureId });
                        }
                    }
                    objMaster.GetByPrimaryKey(CompanyId);
                    objMaster.Edit();
                    string[] skipChargesProperties = { "Gen_Company", "Gen_Charge" };

                    IList<Gen_Company_ExtraCharge> savedChargesList = objMaster.Current.Gen_Company_ExtraCharges;

                    Utils.General.SyncChildCollection(ref savedChargesList, ref objCharges, "Id", skipChargesProperties);
                    objMaster.Save();
                    objMaster.Clear();
                }

                ENUtils.ShowMessage("Account Additional Charges saved successfully.");

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

        private void chkMileage_CheckedChanged(object sender, EventArgs e)
        {
            if (grdCompanyCharges.Columns[COL_CompanyCharges.Mileage].IsVisible)
            {
                if (chkMileage.Checked == true)
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Mileage].Value = true;//..CurrentCell.Value;
                        }
                    }
                }
                else
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Mileage].Value = false;//..CurrentCell.Value;

                        }
                    }
                }
            }
        }

        private void chkWaitingTime_CheckedChanged(object sender, EventArgs e)
        {
            if (grdCompanyCharges.Columns[COL_CompanyCharges.WaitingTime].IsVisible)
            {
                if (chkWaitingTime.Checked == true)
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.WaitingTime].Value = true;//..CurrentCell.Value;
                        }
                    }
                }
                else
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.WaitingTime].Value = false;//..CurrentCell.Value;

                        }
                    }
                }
            }
        }

        private void chkParkingCharges_CheckedChanged(object sender, EventArgs e)
        {
            if (grdCompanyCharges.Columns[COL_CompanyCharges.ParkingCharges].IsVisible)
            {
                if (chkParkingCharges.Checked == true)
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.ParkingCharges].Value = true;//..CurrentCell.Value;
                        }
                    }
                }
                else
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.ParkingCharges].Value = false;//..CurrentCell.Value;

                        }
                    }
                }
            }
        }

        private void chkExtraCharges_CheckedChanged(object sender, EventArgs e)
        {
            if (grdCompanyCharges.Columns[COL_CompanyCharges.ExtraCharges].IsVisible)
            {
                if (chkExtraCharges.Checked == true)
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.ExtraCharges].Value = true;//..CurrentCell.Value;
                        }
                    }
                }
                else
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.ExtraCharges].Value = false;//..CurrentCell.Value;

                        }
                    }
                }
            }
        }

        private void chkFares_CheckedChanged(object sender, EventArgs e)
        {
            if (grdCompanyCharges.Columns[COL_CompanyCharges.Fares].IsVisible)
            {
                if (chkFares.Checked == true)
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Fares].Value = true;//..CurrentCell.Value;
                        }
                    }
                }
                else
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Fares].Value = false;//..CurrentCell.Value;

                        }
                    }
                }
            }
        }

        private void chkPassenger_CheckedChanged(object sender, EventArgs e)
        {
            if (grdCompanyCharges.Columns[COL_CompanyCharges.Passenger].IsVisible)
            {
                if (chkPassenger.Checked == true)
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Passenger].Value = true;//..CurrentCell.Value;
                        }
                    }
                }
                else
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Passenger].Value = false;//..CurrentCell.Value;

                        }
                    }
                }
            }
        }

        private void chkSignature_CheckedChanged(object sender, EventArgs e)
        {
            if (grdCompanyCharges.Columns[COL_CompanyCharges.Signature].IsVisible)
            {
                if (chkSignature.Checked == true)
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Signature].Value = true;//..CurrentCell.Value;
                        }
                    }
                }
                else
                {
                    if (grdCompanyCharges.Rows.Count > 0)
                    {
                        for (int i = 0; i < grdCompanyCharges.Rows.Count; i++)
                        {
                            grdCompanyCharges.Rows[i].Cells[COL_CompanyCharges.Signature].Value = false;//..CurrentCell.Value;

                        }
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

