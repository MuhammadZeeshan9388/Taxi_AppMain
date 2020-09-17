using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using System.Drawing;
using Telerik.WinControls.UI.Export.ExcelML;
using System.IO;
using Utils;
using Telerik.WinControls;
using System.ComponentModel;
using Taxi_BLL;

namespace Taxi_AppMain
{

    public class StyleDataRowConditionalFormattingObject
    {

     
        private string _ConditionFormattingColumnName;

        public string ConditionFormattingColumnName
        {
            get { return _ConditionFormattingColumnName; }
            set { _ConditionFormattingColumnName = value; }
        }


        private string _TValue;

        public string TValue
        {
            get { return _TValue; }
            set { _TValue = value; }
        }
        private Color _RowBackColor;

        public Color RowBackColor
        {
            get { return _RowBackColor; }
            set { _RowBackColor = value; }
        }
        private Color _RowForeColor;

        public Color RowForeColor
        {
            get { return _RowForeColor; }
            set { _RowForeColor = value; }
        }




    }


    public class ClsExportGridView
    {
        BackgroundWorker worker = null;



        StyleDataRowConditionalFormattingObject _ConditionalFormattingObject = null;

        public StyleDataRowConditionalFormattingObject ConditionalFormattingObject
        {
            get { return _ConditionalFormattingObject; }
            set { _ConditionalFormattingObject = value; }
        }



        private ExportToExcelML _Export;

        public ExportToExcelML Export
        {
            get { return _Export; }
            set { _Export = value; }
        }
        private string _Heading;

        public string Heading
        {
            get { return _Heading; }
            set { _Heading = value; }
        }
        private RadGridView _Grid;

        public RadGridView Grid
        {
            get { return _Grid; }
            set { _Grid = value; }
        }
        private int _TitleFontSize;

        public int TitleFontSize
        {
            get { return _TitleFontSize; }
            set { _TitleFontSize = value; }
        }
        private Color _TitleForeColor;

        public Color TitleForeColor
        {
            get { return _TitleForeColor; }
            set { _TitleForeColor = value; }
        }
        private Color _TitleBackColor;

        public Color TitleBackColor
        {
            get { return _TitleBackColor; }
            set { _TitleBackColor = value; }
        }
        private Color _HeaderForeColor;

        public Color HeaderForeColor
        {
            get { return _HeaderForeColor; }
            set { _HeaderForeColor = value; }
        }
        private Color _HeaderBackColor;

        public Color HeaderBackColor
        {
            get { return _HeaderBackColor; }
            set { _HeaderBackColor = value; }
        }
        private string _FileName;

        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }


        private bool _ApplyCellFormatting;

        public bool ApplyCellFormatting
        {
            get { return _ApplyCellFormatting; }
            set { _ApplyCellFormatting = value; }
        }


        private bool _ApplyCustomCellFormatting;

        public bool ApplyCustomCellFormatting
        {
            get { return _ApplyCustomCellFormatting; }
            set { _ApplyCustomCellFormatting = value; }
        }










        public ClsExportGridView(RadGridView grid, string filePath)
        {

            this._Grid = grid;

            this.Export = new ExportToExcelML(this.Grid);
            this.Export.ExportVisualSettings = true;
            this.Export.HiddenColumnOption = HiddenOption.ExportAsHidden;
            this.Export.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;

            this.Export.ExcelCellFormatting += new Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventHandler(export_ExcelCellFormatting);
            this.Export.ExcelTableCreated += new Telerik.WinControls.UI.Export.ExcelML.ExcelTableCreatedEventHandler(export_ExcelTableCreated);

            this.TitleFontSize = 14;
            this.TitleBackColor = Color.Red;
            this.TitleForeColor = Color.White;

            this.HeaderBackColor = Color.Black;
            this.HeaderForeColor = Color.White;
            this.FileName = filePath;


        }





        public bool ExportExcel()
        {

            try
            {
                IsAsync = false;
                Export.FileExtension = "xml";
                this.Export.RunExport(Grid, FileName, Telerik.WinControls.UI.Export.ExcelMaxRows._65536, false);


                return true;


            }
            catch (Exception ex)
            {

                RadMessageBox.Show(ex.Message);
                return false;
            }
        }



        private bool IsAsync;
        RadProgressBar objProgressBar;
        private decimal ProgressCnter = 0;


        public bool ExportExcelAsync()
        {

            try
            {
                Export.FileExtension = "xml";
              

                if (worker == null)
                {
                    worker = new BackgroundWorker();

                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                }

                int visibleCols = this.Grid.Columns.Where(c => c.IsVisible == true).Count();
                ProgressCnter = this.Grid.Rows.Count * visibleCols;


            
                worker.RunWorkerAsync();

                return true;


            }
            catch (Exception ex)
            {

                RadMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool ExportExcelAsync(RadProgressBar progressBar)
        {

            try
            {
                Export.FileExtension = "xml";
                this.objProgressBar = progressBar;

                if (worker == null)
                {
                    worker = new BackgroundWorker();

                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                }

                int visibleCols = this.Grid.Columns.Where(c => c.IsVisible == true).Count();
                ProgressCnter = this.Grid.Rows.Count * visibleCols;


                //     this.objProgressBar.Maximum = ProgressCnter;
                ProgressCnter = 100 / ProgressCnter;
                this.objProgressBar.Value1 = 0;
                worker.RunWorkerAsync();

                return true;


            }
            catch (Exception ex)
            {

                RadMessageBox.Show(ex.Message);
                return false;
            }
        }



        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            IsAsync = true;
          //  this.Export.RunExport(Grid, FileName, Telerik.WinControls.UI.Export.ExcelMaxRows._1048576, false);
            this.Export.RunExport(Grid, FileName, Telerik.WinControls.UI.Export.ExcelMaxRows._1048576, false);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.objProgressBar.Value1 = 0;
            UpdateProgressValue = 0.00m;
            ProgressCnter = 0.00m;
            //RadMessageBox.Show("Export Successfully!");
            RadDesktopAlert alert = new RadDesktopAlert();
            alert.CaptionText = "Export";
            alert.ContentText = "<html> <b><span style=font-size:medium><color=Blue>Export Successfully</span></b></html>";
            alert.Show();
        }

        void export_ExcelTableCreated(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelTableCreatedEventArgs e)
        {
            if (this.Heading.ToStr().Trim().Length > 0)
            {
                // string headerText = "Data Range : " + string.Format("{0:dd-MMM-yy}", dtpFromDate2.Value) + " to " + string.Format("{0:dd-MMM-yy}", dtpToDate2.Value);
                SingleStyleElement style = ((ExportToExcelML)sender).AddCustomExcelRow(e.ExcelTableElement, 30, this.Heading);
                style.FontStyle.Bold = true;

                if (ApplyCustomCellFormatting)
                    this.TitleFontSize = 12;

                style.FontStyle.Size = this.TitleFontSize; //18;
                style.FontStyle.Color = this.TitleForeColor; //Color.White;
                style.InteriorStyle.Color = this.TitleBackColor; //Color.Red;
                style.InteriorStyle.Pattern = InteriorPatternType.Solid;
                style.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
                style.AlignmentElement.VerticalAlignment = VerticalAlignmentType.Center;
            }
        }

        int cnt = 0;
        decimal UpdateProgressValue = 0;

        void export_ExcelCellFormatting(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventArgs e)
        {

            if (ApplyCellFormatting)
            {
                if (ApplyCustomCellFormatting == true)
                {
                    if (e.GridRowInfoType.Name == "GridViewDataRowInfo")
                    {

                        e.ExcelStyleElement.InteriorStyle.Color = Color.White;
                            e.ExcelStyleElement.InteriorStyle.Pattern = Telerik.WinControls.UI.Export.ExcelML.InteriorPatternType.Solid;

                            int borderWeight = 3;

                            if (e.GridRowIndex > 5)
                                borderWeight = 1;


                            e.ExcelStyleElement.Borders.Add(new BorderStyles() { Color = Color.SteelBlue, LineStyle = LineStyle.Continuous, Weight = borderWeight, PositionType = PositionType.Bottom });
                            e.ExcelStyleElement.Borders.Add(new BorderStyles() { Color = Color.SteelBlue, LineStyle = LineStyle.Continuous, Weight = borderWeight, PositionType = PositionType.Left });
                            e.ExcelStyleElement.Borders.Add(new BorderStyles() { Color = Color.SteelBlue, LineStyle = LineStyle.Continuous, Weight = borderWeight, PositionType = PositionType.Right });
    
                        e.ExcelStyleElement.FontStyle.Bold = true;
                                e.ExcelStyleElement.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;


                            if (this.Grid.Rows[e.GridRowIndex].Cells[e.GridColumnIndex].Tag != null)
                    {

                        if (this.Grid.Rows[e.GridRowIndex].Cells[e.GridColumnIndex].Tag.ToInt() == Enums.PAYMENT_TYPES.CASH && this.Grid.Rows[e.GridRowIndex].Cells[e.GridColumnIndex].Value.ToDecimal() > 25)
                        {
                            e.ExcelStyleElement.FontStyle.Color = Color.Red;
                        }
                        else if (this.Grid.Rows[e.GridRowIndex].Cells[e.GridColumnIndex].Tag.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD || this.Grid.Rows[e.GridRowIndex].Cells[e.GridColumnIndex].Tag.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD_PAID)
                        {

                            e.ExcelStyleElement.FontStyle.Color = Color.Green;
                        }
                        else if (this.Grid.Rows[e.GridRowIndex].Cells[e.GridColumnIndex].Tag.ToInt() != Enums.PAYMENT_TYPES.CASH &&
                            this.Grid.Rows[e.GridRowIndex].Cells[e.GridColumnIndex].Tag.ToInt() != Enums.PAYMENT_TYPES.CREDIT_CARD &&
                            this.Grid.Rows[e.GridRowIndex].Cells[e.GridColumnIndex].Tag.ToInt() != Enums.PAYMENT_TYPES.CREDIT_CARD_PAID)
                        {

                            e.ExcelStyleElement.FontStyle.Color = Color.Blue;
                        }

                    }

                        //if(.v
                        //    e.ExcelStyleElement.FontStyle.Color = this.ConditionalFormattingObject.RowForeColor;


                        


                        //if (this.Grid.Columns[e.GridColumnIndex] is GridViewDecimalColumn)
                        //    e.ExcelStyleElement.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Right;
                    }

                }
                else
                {

                    e.ExcelStyleElement.AlignmentElement.WrapText = false;
                    //    e.ExcelStyleElement.FontStyle.Bold = true;
                    if (e.GridRowInfoType.Name == "GridViewTableHeaderRowInfo")
                    {
                        e.ExcelStyleElement.FontStyle.Color = this.HeaderForeColor;
                        e.ExcelStyleElement.FontStyle.Bold = true;
                        e.ExcelStyleElement.InteriorStyle.Pattern = Telerik.WinControls.UI.Export.ExcelML.InteriorPatternType.Solid;
                        e.ExcelStyleElement.InteriorStyle.Color = this.HeaderBackColor;
                    }

                    else if (e.GridRowInfoType.Name == "GridViewDataRowInfo")
                    {
                        if (this.ConditionalFormattingObject != null && this.Grid.Rows[e.GridRowIndex].Cells[this.ConditionalFormattingObject.ConditionFormattingColumnName].Value.ToStr() == this.ConditionalFormattingObject.TValue.ToStr())
                        {
                            e.ExcelStyleElement.InteriorStyle.Color = this.ConditionalFormattingObject.RowBackColor;
                            e.ExcelStyleElement.InteriorStyle.Pattern = Telerik.WinControls.UI.Export.ExcelML.InteriorPatternType.Solid;
                            e.ExcelStyleElement.FontStyle.Bold = true;
                            e.ExcelStyleElement.FontStyle.Color = this.ConditionalFormattingObject.RowForeColor;


                        }


                        if (this.Grid.Columns[e.GridColumnIndex] is GridViewDecimalColumn)
                            e.ExcelStyleElement.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Right;
                    }

                    else if (e.GridRowInfoType.Name == "GridViewSummaryRowInfo")
                    {
                        e.ExcelStyleElement.FontStyle.Color = Color.Black;
                        e.ExcelStyleElement.FontStyle.Bold = true;
                        e.ExcelStyleElement.InteriorStyle.Pattern = Telerik.WinControls.UI.Export.ExcelML.InteriorPatternType.Solid;
                        e.ExcelStyleElement.InteriorStyle.Color = Color.Gainsboro;

                        if (this.Grid.Columns[e.GridColumnIndex] is GridViewDecimalColumn)
                            e.ExcelStyleElement.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Right;
                    }
                }


            }


            cnt++;
            UpdateProgress();

        }

        private void UpdateProgress()
        {
            if (objProgressBar == null)
                return;
            try
            {
                UpdateProgressValue += ProgressCnter;


                if (objProgressBar.Value1 <= 100)
                {
                    if (UpdateProgressValue >= 1)
                    {
                        objProgressBar.Value1 = objProgressBar.Value1 + 1;
                        UpdateProgressValue = 0.00m;
                    }
                }
                else
                {
                    objProgressBar.Value1 = 100;
                }
            }
            catch
            {

            }


        }

    }
}
