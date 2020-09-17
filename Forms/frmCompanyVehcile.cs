using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Telerik.WinControls.UI;
using Taxi_BLL;
using Taxi_Model;
using DAL;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls;
using System.IO;
using System.Diagnostics;
using Taxi_BLL.Classes;

namespace Taxi_AppMain
{
    public partial class frmCompanyVehcile : UI.SetupBase
    {
        CompanyVehcileBO objMaster = null;
        
        string plateExpiryPath = string.Empty;
        string plateExpiryPath2 = string.Empty;
        string plateExpiryPath3 = string.Empty;
        string MOTExpiryPath = string.Empty;
        string insuranceExpiryPath = string.Empty;
        string roadTaxExpiryPath = string.Empty;
        string logBookDocumentPath = string.Empty;

        public frmCompanyVehcile()
        {
            InitializeComponent();
            InitializeConstructor();
            ComboFunctions.FillSubCompanyCombo(ddlSubCompany);
            ComboFunctions.FillVehicleTypeCombo(ddlVehicleType);
            ComboFunctions.FillVehicleColorsCombo(ddlVehicleColor);
            ComboFunctions.FillFuelTypeCombo(ddlFuelType);


            btnInsuranceExpiryBrowsing.Click += new EventHandler(btnInsuranceExpiryBrowsing_Click);
            btnMOTExpiryBrowsing.Click += new EventHandler(btnMOTExpiryBrowsing_Click);
            btnRoadTaxExpiryBrowsing.Click += new EventHandler(btnRoadTaxExpiryBrowsing_Click);
            btnPlateExpiryBrowsing.Click += new EventHandler(btnPlateExpiryBrowsing_Click);
            btnPlateExpiryBrowsing2.Click += new EventHandler(btnPlateExpiryBrowsing2_Click);
            btnPlateExpiryBrowsing3.Click += new EventHandler(btnPlateExpiryBrowsing3_Click);
            btnVehicleBookBrowsing.Click += new EventHandler(btnVehicleBookBrowsing_Click);

            btnPlateExpiryView.Click += new EventHandler(btnPlateExpiryView_Click);
            btnPlateExpiryView2.Click += new EventHandler(btnPlateExpiryView2_Click);
            btnPlateExpiryView3.Click += new EventHandler(btnPlateExpiryView3_Click);
            btnMOTExpiryView.Click += new EventHandler(btnMOTExpiryView_Click);
            btnInsuranceExpiryView.Click += new EventHandler(btnInsuranceExpiryView_Click);
            btnRoadTaxExpiryView.Click += new EventHandler(btnRoadTaxExpiryView_Click);
            btnVehicleBookView.Click += new EventHandler(btnVehicleBookView_Click);

            btnInsuranceExpiryClear.Click += new EventHandler(btnInsuranceExpiryClear_Click);
            btnMOTExpiryClear.Click += new EventHandler(btnMOTExpiryClear_Click);
            btnPlateExpiryClear.Click += new EventHandler(btnPlateExpiryClear_Click);
            btnPlateExpiryClear2.Click += new EventHandler(btnPlateExpiryClear2_Click);
            btnPlateExpiryClear3.Click += new EventHandler(btnPlateExpiryClear3_Click);
            btnRoadTaxExpiryClear.Click += new EventHandler(btnRoadTaxExpiryClear_Click);
            btnVehicleBookClear.Click += new EventHandler(btnVehicleBookClear_Click);

        }

        void btnPlateExpiryClear3_Click(object sender, EventArgs e)
        {
            txtPlatePath3.Text = string.Empty;


            if (string.IsNullOrEmpty(txtPlatePath3.Text))
            {
                //plateExpiryPath = objMaster.Current.PLateExpiryPath.ToString();
                btnPlateExpiryBrowsing3.BackColor = Color.Transparent;
            }
        }

        void btnPlateExpiryClear2_Click(object sender, EventArgs e)
        {
            txtPlatePath2.Text = string.Empty;


            if (string.IsNullOrEmpty(txtPlatePath2.Text))
            {
                //plateExpiryPath = objMaster.Current.PLateExpiryPath.ToString();
                btnPlateExpiryBrowsing2.BackColor = Color.Transparent;
            }
        }

        void btnPlateExpiryView3_Click(object sender, EventArgs e)
        {
            try
            {

                DocumentExpiry();

                if (txtPlatePath3.Text != string.Empty)
                {
                    Process.Start(txtPlatePath3.Text);
                }
                else
                {
                    ENUtils.ShowMessage("File does not exist");
                }

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage("File missing");
            }
        }

        void btnPlateExpiryView2_Click(object sender, EventArgs e)
        {
            try
            {

                DocumentExpiry();

                if (txtPlatePath2.Text != string.Empty)
                {
                    Process.Start(txtPlatePath2.Text);
                }
                else
                {
                    ENUtils.ShowMessage("File does not exist");
                }

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage("File missing");
            }
        }

        void btnPlateExpiryBrowsing3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.FileName.Length > 0)
                    {
                        plateExpiryPath3 = openFileDialog1.FileName;
                        txtPlatePath3.Text = openFileDialog1.FileName;
                        DocumentExpiry();
                    }
                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.ToString());
            }
        }

        void btnPlateExpiryBrowsing2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.FileName.Length > 0)
                    {
                        plateExpiryPath2 = openFileDialog1.FileName;
                        txtPlatePath2.Text = openFileDialog1.FileName;
                        DocumentExpiry();
                    }
                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.ToString());
            }
        }

        void btnVehicleBookClear_Click(object sender, EventArgs e)
        {
            txtLogBookDocPath.Text = string.Empty;
                                    
        }

        void btnVehicleBookView_Click(object sender, EventArgs e)
        {
            try
            {
                              

                if (txtLogBookDocPath.Text != string.Empty)
                {
                    Process.Start(txtLogBookDocPath.Text);
                }
                else
                {
                    ENUtils.ShowMessage("File does not exist");
                }


            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage("File missing");
            }
        }

        void btnVehicleBookBrowsing_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.FileName.Length > 0)
                    {
                        logBookDocumentPath = openFileDialog1.FileName;
                        txtLogBookDocPath.Text = openFileDialog1.FileName;
                        
                    }
                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.ToString());
            }
        }

        void btnRoadTaxExpiryClear_Click(object sender, EventArgs e)
        {
         
            txtRoadPath.Text = string.Empty;


            if (string.IsNullOrEmpty(txtRoadPath.Text))
            {
                //roadTaxExpiryPath = objMaster.Current.RoadTaxExpPath.ToString();
                btnRoadTaxExpiryBrowsing.BackColor = Color.Transparent;
            }
        
        }

        void btnPlateExpiryClear_Click(object sender, EventArgs e)
        {
             txtPlatePath.Text = string.Empty;


             if (string.IsNullOrEmpty(txtPlatePath.Text))
             {
                 //plateExpiryPath = objMaster.Current.PLateExpiryPath.ToString();
                 btnPlateExpiryBrowsing.BackColor = Color.Transparent;
             }

            
        }

        void btnMOTExpiryClear_Click(object sender, EventArgs e)
        {

             txtMOTPath.Text = string.Empty;

             if (string.IsNullOrEmpty(txtMOTPath.Text))
             {
                 //txtMOTPath.Text = objMaster.Current.MOTExpiryPath.ToString();
                 btnMOTExpiryBrowsing.BackColor = Color.Transparent;
             }

        }

        void btnInsuranceExpiryClear_Click(object sender, EventArgs e)
        {
            txtInsurancePath.Text = string.Empty;

            if (string.IsNullOrEmpty(txtInsurancePath.Text))
            {
                //plateExpiryPath = objMaster.Current.PLateExpiryPath.ToString();
                btnInsuranceExpiryBrowsing.BackColor = Color.Transparent;
            }
        }

        void btnRoadTaxExpiryView_Click(object sender, EventArgs e)
        {
            try
            {

               
                    DocumentExpiry();

                    if (txtRoadPath.Text != string.Empty)
                    {
                        Process.Start(txtRoadPath.Text);
                    }
                    else
                    {
                        ENUtils.ShowMessage("File does not exist");
                    }

                
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage("File missing");
            }
        }

        void btnInsuranceExpiryView_Click(object sender, EventArgs e)
        {
            try
            {

               
                    DocumentExpiry();

                    if (txtInsurancePath.Text != string.Empty)
                    {
                        Process.Start(txtInsurancePath.Text);
                    }
                    else
                    {
                        ENUtils.ShowMessage("File does not exist");
                    }

               
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage("File missing");
            }
        }

        void btnMOTExpiryView_Click(object sender, EventArgs e)
        {
            try
            {

             

                    DocumentExpiry();

                    if (txtMOTPath.Text != string.Empty)
                    {
                        Process.Start(txtMOTPath.Text);
                    }
                    else
                    {
                        ENUtils.ShowMessage("File does not exist");
                    }

                
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage("File missing");
            }
        }

        void btnPlateExpiryView_Click(object sender, EventArgs e)
        {

            try
            {

                    DocumentExpiry();

                    if (txtPlatePath.Text != string.Empty)
                    {
                        Process.Start(txtPlatePath.Text);
                    }
                    else
                    {
                        ENUtils.ShowMessage("File does not exist");
                    }
                
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage("File missing");
            }
        }

        void btnPlateExpiryBrowsing_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.FileName.Length > 0)
                    {
                        plateExpiryPath = openFileDialog1.FileName;
                        txtPlatePath.Text = openFileDialog1.FileName; 
                        DocumentExpiry();
                    }
                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.ToString());
            }
        }

        void btnRoadTaxExpiryBrowsing_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.FileName.Length > 0)
                    {
                        roadTaxExpiryPath = openFileDialog1.FileName;
                        txtRoadPath.Text = openFileDialog1.FileName; 
                        DocumentExpiry();
                    }
                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.ToString());
            }
        }

        void btnMOTExpiryBrowsing_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.FileName.Length > 0)
                    {
                        MOTExpiryPath = openFileDialog1.FileName;
                        txtMOTPath.Text = openFileDialog1.FileName; 
                        DocumentExpiry();
                    }
                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.ToString());
            }
        }

        void btnInsuranceExpiryBrowsing_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.FileName.Length > 0)
                    {
                        insuranceExpiryPath = openFileDialog1.FileName;
                        txtInsurancePath.Text = openFileDialog1.FileName;   
                        DocumentExpiry();
                    }
                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.ToString());
            }
        }

        //private void DocumentExpiry()
        //{
        //    try
        //    {

            
        //    using (TaxiDataContext db = new TaxiDataContext())
        //    {
        //        var Fleetmaster = db.Fleet_Masters.Where(c => c.Id == objMaster.Current.Id);

        //        if (Fleetmaster.Count() > 0)
        //        {

        //            if (Fleetmaster.FirstOrDefault().RoadTaxExpPath != null && Fleetmaster.FirstOrDefault().RoadTaxExpPath.ToString() != string.Empty)
        //            {
        //                roadTaxExpiryPath = Fleetmaster.FirstOrDefault().RoadTaxExpPath.ToString();
        //                btnRoadTaxExpiryBrowsing.BackColor = Color.GreenYellow;
        //            }
        //            if (Fleetmaster.FirstOrDefault().InsuranceExpiryPath != null && Fleetmaster.FirstOrDefault().InsuranceExpiryPath.ToString() != string.Empty)
        //            {
        //                insuranceExpiryPath = Fleetmaster.FirstOrDefault().InsuranceExpiryPath.ToString();
        //                btnInsuranceExpiryBrowsing.BackColor = Color.GreenYellow;
        //            }
        //            if (Fleetmaster.FirstOrDefault().MOTExpiryPath != null && Fleetmaster.FirstOrDefault().MOTExpiryPath.ToString() != string.Empty)
        //            {
        //                MOTExpiryPath = Fleetmaster.FirstOrDefault().MOTExpiryPath.ToString();
        //                btnMOTExpiryBrowsing.BackColor = Color.GreenYellow;
        //            }
        //            if (Fleetmaster.FirstOrDefault().PLateExpiryPath != null && Fleetmaster.FirstOrDefault().PLateExpiryPath.ToString() != string.Empty)
        //            {
        //                plateExpiryPath = Fleetmaster.FirstOrDefault().PLateExpiryPath.ToString();
        //                btnPlateExpiryBrowsing.BackColor = Color.GreenYellow;
        //            }
        //        }
                
        //    }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        private void DocumentExpiry()
        {
            try
            {


                if (!string.IsNullOrEmpty(txtRoadPath.Text))
                {
                    roadTaxExpiryPath = txtRoadPath.Text;
                    btnRoadTaxExpiryBrowsing.BackColor = Color.GreenYellow;
                }
                if (!string.IsNullOrEmpty(txtInsurancePath.Text))
                {
                    insuranceExpiryPath = txtInsurancePath.Text;
                    btnInsuranceExpiryBrowsing.BackColor = Color.GreenYellow;
                }
                if  (!string.IsNullOrEmpty( txtMOTPath.Text))
                {
                    MOTExpiryPath = txtMOTPath.Text;
                    btnMOTExpiryBrowsing.BackColor = Color.GreenYellow;
                }
                if (!string.IsNullOrEmpty(txtPlatePath.Text))
                {
                    plateExpiryPath = txtPlatePath.Text;
                    btnPlateExpiryBrowsing.BackColor = Color.GreenYellow;
                }
                if (!string.IsNullOrEmpty(txtPlatePath2.Text))
                {
                    plateExpiryPath2 = txtPlatePath2.Text;
                    btnPlateExpiryBrowsing2.BackColor = Color.GreenYellow;
                }
                if (!string.IsNullOrEmpty(txtPlatePath3.Text))
                {
                    plateExpiryPath3 = txtPlatePath3.Text;
                    btnPlateExpiryBrowsing3.BackColor = Color.GreenYellow;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
       
      
        private void frmDriverRent_Load(object sender, EventArgs e)
        {

        }
        private void frmDriverRent_Shown(object sender, EventArgs e)
        {
            
        }
        private void InitializeConstructor()
        {

            dtpFrontLeft.Value = DateTime.Now.ToDate();
            dtpFrontRight.Value = DateTime.Now.ToDate();
            dtpRearLeft.Value = DateTime.Now.ToDate();
            dtpRearRight.Value = DateTime.Now.ToDate();

            txtFrontLeftMileage.Value = 0;
            txtFrontRightMileage.Value = 0;
            txtRearLeftMileage.Value = 0;
            txtRearRightMileage.Value = 0;

            dtpManfDate.Value = DateTime.Now.ToDate();
            dtpServicesDate.Value = DateTime.Now.ToDate();



            dtpRoadTaxExp.Value = DateTime.Now.ToDate();
            dtpMOTExp.Value = DateTime.Now.ToDate();
            dtpPlateExp.Value = DateTime.Now.ToDate();
            dtpPlateEx2.Value = DateTime.Now.ToDate();
            dtpPlateEx3.Value = DateTime.Now.ToDate();
            dtpInsuranceExpiry.Value = DateTime.Now.ToDate();
            objMaster = new CompanyVehcileBO();
            this.SetProperties((INavigation)objMaster);

            //objTyreTransaction = new CompanyVehicleTyreTransactionBO();
            //this.SetProperties((INavigation)objTyreTransaction);
           
        }

        
        private void AddDropdown()
        {
            
        }
        

        protected override void OnClosed(EventArgs e)
        {
            General.RefreshListWithoutSelected<frmCompanyInvoiceList>("frmCompanyInvoiceList1");

        }


        public override void Save()
        {
            OnSave();
        }
        private void OnSave()
        {
            int SubCompanyId = ddlSubCompany.SelectedValue.ToInt();
     
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


                //if (objTyreTransaction.PrimaryKeyValue == null)
                //{
                //    objTyreTransaction.New();
                //}
                //else
                //{
                //    objTyreTransaction.Edit();
                //}

                objMaster.Current.VehicleNo = txtVehNo.Text.Trim();
                objMaster.Current.VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
                objMaster.Current.VehicleColor = ddlVehicleColor.Text.ToStr().Trim();
                objMaster.Current.FuelTypeId = ddlFuelType.SelectedValue.ToIntorNull();
                objMaster.Current.VehicleMake = txtVehMake.Text.Trim();
                objMaster.Current.VehicleModel = txtVehModel.Text.Trim();
                objMaster.Current.VehicleOwner = txtVehOwner.Text.Trim();

                objMaster.Current.ManufactureDate = dtpManfDate.Value.ToDateTime();
                objMaster.Current.ServicesDate = dtpServicesDate.Value.ToDateTime();

                objMaster.Current.RoadTaxExpDate = dtpRoadTaxExp.Value.ToDateTimeorNull();
                objMaster.Current.MOTExpiryDate = dtpMOTExp.Value.ToDateTimeorNull();
                objMaster.Current.PLateExpiryDate = dtpPlateExp.Value.ToDateTimeorNull();
                objMaster.Current.PLateExpiryDate2 = dtpPlateEx2.Value.ToDateTimeorNull();
                objMaster.Current.PLateExpiryDate3 = dtpPlateEx3.Value.ToDateTimeorNull();
                objMaster.Current.InsuranceExpiry = dtpInsuranceExpiry.Value.ToDateTimeorNull();
                objMaster.Current.Plateno = txtPlateNo.Text.Trim();
                objMaster.Current.plateno2 = txtPlateNo2.Text.Trim();
                objMaster.Current.plateno3 = txtPlateNo3.Text.Trim();
                //
                objMaster.Current.PartDetails = txtPartDetails.Text.Trim();
                objMaster.Current.Parts = numParts.Value.ToDecimal();
                objMaster.Current.Labour = numLabour.Value.ToDecimal();
                objMaster.Current.TyreChangeMileage = numTyreChangeMileage.Value.ToDecimal();
                objMaster.Current.TyresChanged = txtTyresChanged.Text.Trim();
                objMaster.Current.CostofTyres = numCostofTyres.Value.ToDecimal();
                objMaster.Current.SubCompanyId = ddlSubCompany.SelectedValue.ToInt();
                objMaster.Current.PLateExpiryPath = txtPlatePath.Text;
                objMaster.Current.PLateExpiryPath2 = txtPlatePath2.Text;
                objMaster.Current.PLateExpiryPath3 = txtPlatePath3.Text;
                objMaster.Current.InsuranceExpiryPath = txtInsurancePath.Text;
                objMaster.Current.MOTExpiryPath = txtMOTPath.Text;
                objMaster.Current.RoadTaxExpPath = txtRoadPath.Text;
                objMaster.Current.VehicleID = txtVehicleId.Text;
                objMaster.Current.LogBookNo = txtVehicleLogBookNo.Text;
                objMaster.Current.LogBookPath = txtLogBookDocPath.Text;
                objMaster.Current.Notes = txtNotes.Text;
                objMaster.Current.Notes2 = txtNotes2.Text;
                objMaster.Current.Notes3 = txtNotes3.Text;

                if (objMaster.Current.Fleet_Master_TyreTransactions.Count() == 0)
                {
                    objMaster.Current.Fleet_Master_TyreTransactions.Add(new Fleet_Master_TyreTransaction
                    {
                        FleetMasterId = objMaster.Current.Id,
                           
                            FrontRightDate = dtpFrontRight.Value.ToDateTimeorNull(),
                            FrontLeftDate = dtpFrontLeft.Value.ToDateTimeorNull(),
                            RearRightDate = dtpRearRight.Value.ToDateTimeorNull(),
                            RearLeftDate = dtpRearLeft.Value.ToDateTimeorNull(),
                            FrontRightMileage = txtFrontRightMileage.Value,
                            FrontLeftMileage = txtFrontLeftMileage.Value,
                            RearRightMileage = txtRearRightMileage.Value,
                            RearLeftMileage = txtRearLeftMileage.Value,

                        //ClientId = objClient.PrimaryKeyValue.ToInt(),
                        //EmailId = txtEmailId.Text.Trim(),
                        //Password = txtLogMeInPassword.Text.Trim()
                    });
                }
                else
                {

                    objMaster.Current.Fleet_Master_TyreTransactions[0].FrontRightDate = dtpFrontRight.Value.ToDateTimeorNull();
                    objMaster.Current.Fleet_Master_TyreTransactions[0].FrontLeftDate = dtpFrontLeft.Value.ToDateTimeorNull();
                    objMaster.Current.Fleet_Master_TyreTransactions[0].RearRightDate = dtpRearRight.Value.ToDateTimeorNull();
                    objMaster.Current.Fleet_Master_TyreTransactions[0].RearLeftDate = dtpRearLeft.Value.ToDateTimeorNull();

                    objMaster.Current.Fleet_Master_TyreTransactions[0].FrontRightMileage = txtFrontRightMileage.Value;
                    objMaster.Current.Fleet_Master_TyreTransactions[0].FrontLeftMileage = txtFrontLeftMileage.Value;
                    objMaster.Current.Fleet_Master_TyreTransactions[0].RearRightMileage = txtRearRightMileage.Value;

                    objMaster.Current.Fleet_Master_TyreTransactions[0].RearLeftMileage = txtRearLeftMileage.Value;
                   
                }

                
                objMaster.Save();

                objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);

                DisplayRecord();
                General.RefreshListWithoutSelected<frmCompanyVehcileList>("frmCompanyVehcileList1");


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


        public override void DisplayRecord()
        {
            if (objMaster.Current == null) return;


            ddlVehicleColor.SelectedText = "";

            ddlVehicleType.SelectedValue = objMaster.Current.VehicleTypeId;
            txtVehNo.Text = objMaster.Current.VehicleNo;
            txtVehOwner.Text = objMaster.Current.VehicleOwner.ToStr();
            txtVehModel.Text = objMaster.Current.VehicleModel.ToStr();
            txtVehMake.Text = objMaster.Current.VehicleMake.ToStr();
            ddlVehicleColor.SelectedText = objMaster.Current.VehicleColor.ToStr();
            ddlFuelType.SelectedValue = objMaster.Current.FuelTypeId;

            dtpManfDate.Value = objMaster.Current.ManufactureDate;
            dtpServicesDate.Value = objMaster.Current.ServicesDate;
            
            dtpRoadTaxExp.Value = objMaster.Current.RoadTaxExpDate;
            dtpMOTExp.Value = objMaster.Current.MOTExpiryDate;
            dtpPlateExp.Value = objMaster.Current.PLateExpiryDate;
            dtpPlateEx2.Value = objMaster.Current.PLateExpiryDate2;
            dtpPlateEx3.Value = objMaster.Current.PLateExpiryDate3;
            dtpInsuranceExpiry.Value = objMaster.Current.InsuranceExpiry;
            txtPlateNo.Text = objMaster.Current.Plateno;
            txtPlateNo2.Text = objMaster.Current.plateno2;
            txtPlateNo3.Text = objMaster.Current.plateno3;
            ddlSubCompany.SelectedValue = objMaster.Current.SubCompanyId;
            //
            txtPartDetails.Text = objMaster.Current.PartDetails;
            numParts.Value = objMaster.Current.Parts.ToDecimal(); ;
            numLabour.Value = objMaster.Current.Labour.ToDecimal();
            numTyreChangeMileage.Value = objMaster.Current.TyreChangeMileage.ToDecimal();
            txtTyresChanged.Text = objMaster.Current.TyresChanged;
            numCostofTyres.Value = objMaster.Current.CostofTyres.ToDecimal();
            txtLogBookDocPath.Text = objMaster.Current.LogBookPath;
            txtVehicleId.Text = objMaster.Current.VehicleID;
            txtVehicleLogBookNo.Text = objMaster.Current.LogBookNo;

            txtNotes.Text = objMaster.Current.Notes;
            txtNotes2.Text = objMaster.Current.Notes2;
            txtNotes3.Text = objMaster.Current.Notes3;

            if (!string.IsNullOrEmpty(objMaster.Current.PLateExpiryPath))
            {
                txtPlatePath.Text = objMaster.Current.PLateExpiryPath;
            }
            if (!string.IsNullOrEmpty(objMaster.Current.PLateExpiryPath2))
            {
                txtPlatePath2.Text = objMaster.Current.PLateExpiryPath2;
            }
            if (!string.IsNullOrEmpty(objMaster.Current.PLateExpiryPath3))
            {
                txtPlatePath3.Text = objMaster.Current.PLateExpiryPath3;
            }
            if (!string.IsNullOrEmpty(objMaster.Current.RoadTaxExpPath))
            {
                txtRoadPath.Text = objMaster.Current.RoadTaxExpPath;
            }
            if (!string.IsNullOrEmpty(objMaster.Current.MOTExpiryPath))
            {
                txtMOTPath.Text = objMaster.Current.MOTExpiryPath;
            }
            if (!string.IsNullOrEmpty(objMaster.Current.InsuranceExpiryPath))
            {
                txtInsurancePath.Text = objMaster.Current.InsuranceExpiryPath;
            }

            DocumentExpiry();

            if (objMaster.Current.Fleet_Master_TyreTransactions.Count > 0)
            {
                dtpRearLeft.Value = objMaster.Current.Fleet_Master_TyreTransactions[0].RearLeftDate;
                dtpRearRight.Value = objMaster.Current.Fleet_Master_TyreTransactions[0].RearRightDate;
                dtpFrontLeft.Value = objMaster.Current.Fleet_Master_TyreTransactions[0].FrontLeftDate;
                dtpFrontRight.Value = objMaster.Current.Fleet_Master_TyreTransactions[0].FrontRightDate;

                txtRearLeftMileage.Value = objMaster.Current.Fleet_Master_TyreTransactions[0].RearLeftMileage.ToDecimal();
                txtRearRightMileage.Value = objMaster.Current.Fleet_Master_TyreTransactions[0].RearRightMileage.ToDecimal();
                txtFrontLeftMileage.Value = objMaster.Current.Fleet_Master_TyreTransactions[0].FrontLeftMileage.ToDecimal();
                txtFrontRightMileage.Value = objMaster.Current.Fleet_Master_TyreTransactions[0].FrontRightMileage.ToDecimal();

            }

           
        }




        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            OnSave();

        }
        public override void AddNew()
        {
            OnNew();
        }
        public override void OnNew()
        {
            dtpManfDate.Value = DateTime.Now.ToDate();
            dtpServicesDate.Value = DateTime.Now.ToDate();

            dtpRoadTaxExp.Value = DateTime.Now.ToDate();
            dtpMOTExp.Value = DateTime.Now.ToDate();
            dtpPlateExp.Value = DateTime.Now.ToDate();
            dtpPlateEx2.Value = DateTime.Now.ToDate();
            dtpPlateEx3.Value = DateTime.Now.ToDate();
            dtpInsuranceExpiry.Value = DateTime.Now.ToDate();


            txtPlatePath.Text = string.Empty;
            txtPlatePath2.Text = string.Empty;
            txtPlatePath3.Text = string.Empty;
            txtInsurancePath.Text = string.Empty;
            txtMOTPath.Text = string.Empty;
            txtRoadPath.Text = string.Empty;
            
            txtVehicleId.Text = string.Empty;
            txtVehicleLogBookNo.Text = string.Empty;
            txtLogBookDocPath.Text = string.Empty;

            btnRoadTaxExpiryBrowsing.BackColor = Color.Transparent;
            btnInsuranceExpiryBrowsing.BackColor = Color.Transparent;
            btnMOTExpiryBrowsing.BackColor = Color.Transparent;
            btnPlateExpiryBrowsing.BackColor = Color.Transparent;
            btnPlateExpiryBrowsing2.BackColor = Color.Transparent;
            btnPlateExpiryBrowsing3.BackColor = Color.Transparent;

            txtNotes.Text = string.Empty;
            txtNotes2.Text = string.Empty;
            txtNotes3.Text = string.Empty;

            txtPlateNo.Text = string.Empty;
            txtPlateNo2.Text = string.Empty;
            txtPlateNo3.Text = string.Empty;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dtpManfDate.Value = DateTime.Now.ToDate();
            dtpServicesDate.Value = DateTime.Now.ToDate();

            dtpRoadTaxExp.Value = DateTime.Now.ToDate();
            dtpMOTExp.Value = DateTime.Now.ToDate();
            dtpPlateExp.Value = DateTime.Now.ToDate();
            dtpPlateEx2.Value = DateTime.Now.ToDate();
            dtpPlateEx3.Value = DateTime.Now.ToDate();
            dtpInsuranceExpiry.Value = DateTime.Now.ToDate();
            ddlVehicleType.SelectedIndex = 0;
            txtVehNo.Text = string.Empty;
            txtVehOwner.Text = string.Empty;
            txtVehModel.Text = string.Empty;
            txtVehMake.Text = string.Empty;
            ddlVehicleColor.SelectedIndex = 0;
            ddlFuelType.SelectedIndex = 0;

            txtVehicleId.Text = string.Empty;
            txtVehicleLogBookNo.Text = string.Empty;
            txtLogBookDocPath.Text = string.Empty;
            txtNotes.Text = string.Empty;
            txtNotes2.Text = string.Empty;
            txtNotes3.Text = string.Empty;

            txtPlatePath.Text = string.Empty;
            txtPlatePath2.Text = string.Empty;
            txtPlatePath3.Text = string.Empty;
            txtInsurancePath.Text = string.Empty;
            txtMOTPath.Text = string.Empty;
            txtRoadPath.Text = string.Empty;

            btnRoadTaxExpiryBrowsing.BackColor = Color.Transparent;
            btnInsuranceExpiryBrowsing.BackColor = Color.Transparent;
            btnMOTExpiryBrowsing.BackColor = Color.Transparent;
            btnPlateExpiryBrowsing.BackColor = Color.Transparent;
            btnPlateExpiryBrowsing2.BackColor = Color.Transparent;
            btnPlateExpiryBrowsing3.BackColor = Color.Transparent;
            //
            txtPartDetails.Text = string.Empty;
            numParts.Value = 0;
            numLabour.Value = 0;
            numTyreChangeMileage.Value = 0;
            txtTyresChanged.Text = string.Empty;
            numCostofTyres.Value = 0;
            txtPlateNo.Text = string.Empty;
            txtPlateNo2.Text = string.Empty;
            txtPlateNo3.Text = string.Empty;

        }

  



    }
}
