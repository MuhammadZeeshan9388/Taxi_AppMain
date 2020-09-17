using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI;
using Taxi_BLL;

using Taxi_Model;
using DAL;
using Telerik.WinControls.UI;
using Utils;

namespace Taxi_AppMain
{
    public partial class frmSecGroupAuthorities : SetupBase
    {
        IQueryable<UM_Form> listForms;
        IQueryable<UM_Function> listFunctions;
        IQueryable<UM_FormFunction> listFormFunctions;


        SecGroupBO objMaster;
        List<UM_SecurityGroup_Permission> listofAuthorities;

      //  BLInfo<UM_Form,Taxi_Model.TaxiDataContext> dataInfo;

        private bool FormLoaded = false;

        public frmSecGroupAuthorities()
        {
            InitializeComponent();

         //   dataInfo = new BLInfo<UM_Form, TaxiDataContext>();

            ListofFormTemplates = new List<UM_Form_Template>();

            objMaster = new SecGroupBO();
            listofAuthorities = new List<UM_SecurityGroup_Permission>();

            tv_Forms.NodeMouseDown+=new RadTreeView.TreeViewMouseEventHandler(tv_Forms_NodeMouseDown);
            tv_Forms.SelectedNodeChanged += new RadTreeView.RadTreeViewEventHandler(tv_Forms_SelectedNodeChanged);
            this.Shown += new EventHandler(frmSecGroupAuthorities_Shown);
        }

        void frmSecGroupAuthorities_Shown(object sender, EventArgs e)
        {
            //   this.CurrentFormId = ENUtils.GetFormId(this.Name);

            this.SetProperties((INavigation)objMaster);

            FillCombos();
            LoadLists();
            PopulateFormsListTreeView();
            InitializeTemplatesGrid();
            InitializeFunctionsGrid();

            SelectFirstNode();

            FormLoaded = true;

            grdFunctions.TableElement.AlternatingRowColor = Color.AliceBlue;
            grdFunctions.TableElement.TableHeaderHeight = 20;
            grdFunctions.TableElement.BackColor = Color.AliceBlue;

        }


        private void frmSecGroupAuthorities_Load(object sender, EventArgs e)
        {
            ////   this.CurrentFormId = ENUtils.GetFormId(this.Name);

            //this.SetProperties((INavigation)objMaster);

            //FillCombos();
            //LoadLists();
            //PopulateFormsListTreeView();
            //InitializeFunctionsGrid();

            //SelectFirstNode();

            //FormLoaded = true;

            //grdFunctions.TableElement.AlternatingRowColor = Color.AliceBlue;
            //grdFunctions.TableElement.TableHeaderHeight = 20;
            //grdFunctions.TableElement.BackColor = Color.AliceBlue;


        }

        void tv_Forms_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            int formId = !e.Node.IsRootNode ? Convert.ToInt32(e.Node.Name) : 0;
            LoadGridFormFunctions(formId);
        }

       


        private void SelectFirstNode()
        {

            if (tv_Forms.Nodes.Count > 0)
            {
                tv_Forms.Nodes[0].Nodes[0].Selected = true;

                LoadGridFormFunctions(tv_Forms.SelectedNode.Name.ToInt());

            }
        }


        private void InitializeTemplatesGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.HeaderText = "Template";
            col.Width = 150;
            col.ReadOnly = true;
            col.Name = COLS_TEMPLATES.TEMPLATENAME;
            grdReportTemplates.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS_TEMPLATES.TEMPLATEID;
            grdReportTemplates.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS_TEMPLATES.FORMID;
            grdReportTemplates.Columns.Add(col);


            GridViewCheckBoxColumn colChk = new GridViewCheckBoxColumn();
            colChk.HeaderText = "Default";
            colChk.Name = COLS_TEMPLATES.ISDEFAULT;
            grdReportTemplates.Columns.Add(colChk);

        }

        private void InitializeFunctionsGrid()
        {
            GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
            col.HeaderText = "";
            col.Name = "ColCheckAction";
            grdFunctions.Columns.Add(col);

        }

        private void LoadFormListTreeView()
        {


        }


        private void LoadLists()
        {
            //listForms = dataInfo.GetAll<UM_Form>(null).ToList();
            //listFunctions = dataInfo.GetAll<UM_Function>(null).ToList();
            //listFormFunctions = dataInfo.GetAll<UM_FormFunction>(null).ToList();

            listForms = General.GetQueryable<UM_Form>(null);
            listFunctions = General.GetQueryable<UM_Function>(null);
            listFormFunctions =General.GetQueryable<UM_FormFunction>(null);
        }


        private void FillCombos()
        {
            ComboFunctions.FillSecGroupsCombo(ddlGroups);

        }



        private void PopulateFormsListTreeView()
        {
            RadTreeNode treeNodeModule = null;

            foreach (var form in listForms.OrderBy(m=>m.ModuleId))
            {

                if (tv_Forms.Nodes.Find(form.ModuleId.ToString(), false).Count() == 0)
                {
                    treeNodeModule = CreateModuleNode(form.ModuleId.ToInt(), form.UM_Module.ModuleName);
                    if (treeNodeModule.IsRootNode)
                    {

                        var forms = listForms.Where(o => o.ModuleId == form.ModuleId);
                        if (forms.Count() > 0)
                        {
                            List<RadTreeNode> formNodes = CreateFormNode(forms.OrderBy(c=>c.FormTitle));
                            treeNodeModule.Nodes.AddRange(formNodes);
                        }
                    }
                    tv_Forms.Nodes.Add(treeNodeModule);
                }

            }

            tv_Forms.ExpandAll();

            
        }


        private RadTreeNode CreateModuleNode(int moduleId, string moduleTitle)
        {
            RadTreeNode treeNode = new RadTreeNode();
            treeNode.Name = moduleId.ToString();
            Font font = new Font("Tahoma", 11, FontStyle.Bold);
            treeNode.ForeColor = Color.DimGray;
            treeNode.Font = font;
            treeNode.Text = moduleTitle;
            return treeNode;
        }

        private List<RadTreeNode> CreateFormNode(IQueryable<UM_Form> listFrms)
        {

            List<RadTreeNode> treeFormNodes = new List<RadTreeNode>();
            Font font = new Font("Tahonma", 10, FontStyle.Bold);
            foreach (var item in listFrms)
            {
                RadTreeNode treeNodeForm = new RadTreeNode();
                treeNodeForm.ForeColor = Color.SteelBlue;
                treeNodeForm.Name = item.Id.ToString();
                treeNodeForm.Text = item.FormTitle;
                treeNodeForm.Tag = item.FormName;
                treeNodeForm.Font = font;

                treeFormNodes.Add(treeNodeForm);
            }

            return treeFormNodes;
        }




        private void tv_Forms_NodeMouseDown(object sender, RadTreeViewMouseEventArgs e)
        {

            int formId = !e.Node.IsRootNode ? Convert.ToInt32(e.Node.Name) : 0;
            LoadGridFormFunctions(formId);
        }


        private void LoadGridFormFunctions(int formId)
        {



            try
            {

                int iFuncId = 0;
                int iFuncId2 = 0;
                var list2 = (from a in listFormFunctions
                             where a.FormId == formId
                             orderby a.UM_Function.OrderId
                             select new
                             {
                                 Id = default(long),
                                 GroupId = default(int?),
                                 FormFunctionId = a.Id,
                                 FormId = a.FormId,
                                 FunctionId = a.FunctionId,
                                 FunctionName = a.UM_Function.FunctionName

                             }).ToList();



                var templatesList = General.GetQueryable<UM_Form_Template>(c => c.FormId == formId).Select(args => new
                                                                          {
                                                                              TemplateId = args.Id,
                                                                              FormId = args.FormId,
                                                                              Template = args.TemplateName,
                                                                              Default = args.IsDefault
                                                                          }).ToList();


                grdReportTemplates.Rows.Clear();
                if (templatesList.Count > 0)
                {
                    tableLayoutPanel1.RowStyles[1].Height = 50;
                    GridViewRowInfo row = null;
                    foreach (var item in templatesList)
                    {
                        row = grdReportTemplates.Rows.AddNew();

                        row.Cells[COLS_TEMPLATES.TEMPLATEID].Value = item.TemplateId;
                        row.Cells[COLS_TEMPLATES.TEMPLATENAME].Value = item.Template;
                        row.Cells[COLS_TEMPLATES.FORMID].Value = item.FormId;
                        row.Cells[COLS_TEMPLATES.ISDEFAULT].Value = item.Default.ToBool();
                    }




                }
                else
                {

                    tableLayoutPanel1.RowStyles[1].Height = 0;

                }



                grdFunctions.DataSource = list2;

                grdFunctions.Columns["FormFunctionId"].IsVisible = false;

                grdFunctions.Columns["FunctionId"].IsVisible = false;
                grdFunctions.Columns["Id"].IsVisible = false;
                grdFunctions.Columns["FormId"].IsVisible = false;
                grdFunctions.Columns["GroupId"].IsVisible = false;

                grdFunctions.Columns["FunctionName"].Width = 300;
                grdFunctions.Columns["FunctionName"].HeaderText = "Function";
                grdFunctions.Columns["FunctionName"].TextAlignment = ContentAlignment.MiddleCenter;

                for (int i = 0; i < grdFunctions.Rows.Count; i++)
                {

                    iFuncId2 = Convert.ToInt32(grdFunctions.Rows[i].Cells["FunctionId"].Value);
                    iFuncId = Convert.ToInt32(grdFunctions.Rows[i].Cells["FormFunctionId"].Value);

                    grdFunctions.Rows[i].Cells["FunctionName"].Value = listFunctions.Where(f => f.Id == iFuncId2).FirstOrDefault().FunctionName;

                    if (listofAuthorities.Where(o => o.FormFunctionId == iFuncId).Count() > 0)
                    {
                        grdFunctions.CurrentRow = grdFunctions.Rows[i];
                        grdFunctions.Rows[i].Cells["ColCheckAction"].Value = true;
                    }
                    else
                    {
                        grdFunctions.CurrentRow = grdFunctions.Rows[i];
                        grdFunctions.Rows[i].Cells["ColCheckAction"].Value = false;
                    }

                }

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }

        
                 

        }


        public struct COLS_TEMPLATES
        {
            public static string TEMPLATEID = "TEMPLATEID";
            public static string TEMPLATENAME = "TEMPLATENAME";
            public static string FORMID = "FORMID";
            public static string ISDEFAULT = "ISDEFAULT";



        }




        private List<UM_Form_Template> _ListofFormTemplates;

        public List<UM_Form_Template> ListofFormTemplates
        {
            get { return _ListofFormTemplates; }
            set { _ListofFormTemplates = value; }
        }


        private void grdFunctions_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            UM_SecurityGroup_Permission obj;

            try
            {
                if (grdFunctions.CurrentColumn.Name == "ColCheckAction")
                {
                    int formId = Convert.ToInt32(tv_Forms.SelectedNode.Name);
                    int formfunctionId = Convert.ToInt32(grdFunctions.CurrentRow.Cells["FormFunctionId"].Value);
                    obj = listofAuthorities.FindAll(f => f.FormFunctionId == formfunctionId).FirstOrDefault();
                    if (obj != null && (e.NewValue.ToStr().ToLower() == "off" || e.NewValue.ToStr().ToLower() == "false"))
                    {
                        listofAuthorities.Remove(obj);
                    }
                    else if (obj == null && (e.NewValue.ToStr().ToLower() == "on" || e.NewValue.ToStr().ToLower() == "true"))
                    {
                        obj = new UM_SecurityGroup_Permission();
                        obj.FormFunctionId = formfunctionId;

                      //  if (pubSecId != 0)
                         //   obj.groupId = pubSecId;


                        listofAuthorities.Add(obj);
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }



        #region Overridden Methods

        public override void Save()
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                 

                    objMaster.New();
                }
                else
                {


                    if (objMaster.Current != null)
                    {
                        objMaster.GetByPrimaryKey(txtId.Text.ToInt());
                        objMaster.Edit();


                    }
                }

              
                string[] skipProperties = { "UM_SecurityGroup", "UM_FormFunction" };
                IList<UM_SecurityGroup_Permission> savedList =objMaster.Current.UM_SecurityGroup_Permissions;
                Utils.General.SyncChildCollection(ref savedList, ref listofAuthorities, "Id", skipProperties);
            

                objMaster.Save();
                SaveFormTemplates();
           //     ENUtils.ShowMessage("Save Successful");
                objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                DisplayRecord();
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

        private void SaveFormTemplates()
        {
            if (ListofFormTemplates.Count > 0)
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    foreach (var item in ListofFormTemplates)
                    {

                        db.stp_UpdateFormTemplate(item.Id, item.IsDefault.ToBool());

                    }

                }


            }
            ListofFormTemplates.Clear();

        }

        public override void AddNew()
        {
            OnNew();
          


        }

        public override void OnNew()
        {
            ddlGroups.SelectedValue = null;
            listofAuthorities.Clear();
            txtId.Text = string.Empty;
            objMaster = new SecGroupBO();
            SelectFirstNode();
        }




        public override void Delete()
        {

            try
            {
               
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


        public override void DisplayRecord()
        {
            if (objMaster.Current != null)
            {

                txtId.Text = objMaster.Current.Id.ToStr();
                ddlGroups.SelectedValue = objMaster.Current.Id;
                listofAuthorities = objMaster.Current.UM_SecurityGroup_Permissions.ToList();

                SelectFirstNode();


            }
        }


        #endregion

        private void ddlGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!FormLoaded) return;

            objMaster.GetByPrimaryKey(ddlGroups.SelectedValue.ToInt());
            DisplayRecord();

         
        }

        private void grdReportTemplates_ValueChanging(object sender, ValueChangingEventArgs e)
        {

          

            try
            {
                if (e.NewValue != null && e.NewValue is Boolean)
                {
                    if (grdReportTemplates.CurrentColumn.Name == COLS_TEMPLATES.ISDEFAULT)
                    {

                        int templateId = grdReportTemplates.CurrentRow.Cells[COLS_TEMPLATES.TEMPLATEID].Value.ToInt();
                        new TaxiDataContext().stp_UpdateFormTemplate(templateId, e.NewValue.ToBool());


                        //if(e.NewValue.ToBool())
                        //{
                        //    grdReportTemplates.ValueChanging -= new ValueChangingEventHandler(grdReportTemplates_ValueChanging);

                        //    grdReportTemplates.Rows.Where(c => c.Index != grdReportTemplates.CurrentRow.Index).ToList().ForEach(c => c.Cells[COLS_TEMPLATES.ISDEFAULT].Value = false);

                        //    grdReportTemplates.ValueChanging+=new ValueChangingEventHandler(grdReportTemplates_ValueChanging);
                        //}

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }






    }
}
