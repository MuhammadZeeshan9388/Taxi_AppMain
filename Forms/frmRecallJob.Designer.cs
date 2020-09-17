namespace Taxi_AppMain
{
    partial class frmRecallJob
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
            this.lblDespatchHeading = new System.Windows.Forms.Label();
            this.btnTrack = new Telerik.WinControls.UI.RadButton();
            this.ddl_Driver = new Telerik.WinControls.UI.RadDropDownList();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDespatchHeading
            // 
            this.lblDespatchHeading.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblDespatchHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDespatchHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDespatchHeading.ForeColor = System.Drawing.Color.White;
            this.lblDespatchHeading.Location = new System.Drawing.Point(0, 0);
            this.lblDespatchHeading.Name = "lblDespatchHeading";
            this.lblDespatchHeading.Size = new System.Drawing.Size(371, 28);
            this.lblDespatchHeading.TabIndex = 13;
            this.lblDespatchHeading.Text = "Recover Job";
            this.lblDespatchHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTrack
            // 
            this.btnTrack.Location = new System.Drawing.Point(230, 130);
            this.btnTrack.Name = "btnTrack";
            this.btnTrack.Size = new System.Drawing.Size(130, 41);
            this.btnTrack.TabIndex = 1;
            this.btnTrack.Text = "Recover";
            this.btnTrack.Click += new System.EventHandler(this.btnTrack_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnTrack.GetChildAt(0))).Text = "Recover";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnTrack.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnTrack.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ddl_Driver
            // 
            this.ddl_Driver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Driver.Location = new System.Drawing.Point(101, 68);
            this.ddl_Driver.Name = "ddl_Driver";
            this.ddl_Driver.Size = new System.Drawing.Size(259, 23);
            this.ddl_Driver.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Select Driver";
            // 
            // frmRecallJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(371, 183);
            this.Controls.Add(this.lblDespatchHeading);
            this.Controls.Add(this.btnTrack);
            this.Controls.Add(this.ddl_Driver);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.Name = "frmRecallJob";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recover Job";
            ((System.ComponentModel.ISupportInitialize)(this.btnTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDespatchHeading;
        private Telerik.WinControls.UI.RadButton btnTrack;
        private Telerik.WinControls.UI.RadDropDownList ddl_Driver;
        private System.Windows.Forms.Label label2;
    }
}