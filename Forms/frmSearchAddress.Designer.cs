namespace Taxi_AppMain
{
    partial class frmSearchAddress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlTop = new Telerik.WinControls.UI.RadPanel();
            this.btnMap = new Telerik.WinControls.UI.RadButton();
            this.txtAddress = new UI.AutoCompleteTextBox();
            this.lblViaLoc = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblViaLoc)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTop.ForeColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            // 
            // 
            // 
            this.pnlTop.RootElement.ForeColor = System.Drawing.Color.White;
            this.pnlTop.Size = new System.Drawing.Size(522, 36);
            this.pnlTop.TabIndex = 204;
            this.pnlTop.Text = "Search Address";
            this.pnlTop.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMap
            // 
            this.btnMap.Location = new System.Drawing.Point(339, 167);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(130, 28);
            this.btnMap.TabIndex = 203;
            this.btnMap.Text = "Show Map";
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.White;
            this.txtAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress.DefaultHeight = 60;
            this.txtAddress.DefaultWidth = 370;
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.ForceListBoxToUpdate = false;
            this.txtAddress.FormerValue = "";
            this.txtAddress.Location = new System.Drawing.Point(100, 73);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            // 
            // 
            // 
            this.txtAddress.RootElement.StretchVertically = true;
            this.txtAddress.SelectedItem = null;
            this.txtAddress.Size = new System.Drawing.Size(369, 60);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.TabStop = false;
            this.txtAddress.Values = null;
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.txtAddress.GetChildAt(0))).StretchVertically = true;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtAddress.GetChildAt(0).GetChildAt(2))).Width = 1F;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtAddress.GetChildAt(0).GetChildAt(2))).ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtAddress.GetChildAt(0).GetChildAt(2))).ForeColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtAddress.GetChildAt(0).GetChildAt(2))).ForeColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtAddress.GetChildAt(0).GetChildAt(2))).InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtAddress.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            // 
            // lblViaLoc
            // 
            this.lblViaLoc.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViaLoc.Location = new System.Drawing.Point(10, 73);
            this.lblViaLoc.Name = "lblViaLoc";
            this.lblViaLoc.Size = new System.Drawing.Size(67, 22);
            this.lblViaLoc.TabIndex = 201;
            this.lblViaLoc.Text = "Address:";
            // 
            // frmSearchAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(522, 211);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.btnMap);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblViaLoc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchAddress";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Address";
            this.Load += new System.EventHandler(this.frmViaPoints_Load);
            this.Shown += new System.EventHandler(this.frmViaPoints_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSearchAddress_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblViaLoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Telerik.WinControls.UI.RadPanel pnlTop;
        private Telerik.WinControls.UI.RadButton btnMap;
        private UI.AutoCompleteTextBox txtAddress;
        private Telerik.WinControls.UI.RadLabel lblViaLoc;

    }
}
