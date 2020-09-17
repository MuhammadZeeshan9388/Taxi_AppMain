namespace Taxi_AppMain
{
    partial class frmAddRemDescription
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
            this.txtRemovalDescription = new System.Windows.Forms.RichTextBox();
            this.radLabel28 = new Telerik.WinControls.UI.RadLabel();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRemovalDescription
            // 
            this.txtRemovalDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemovalDescription.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemovalDescription.Location = new System.Drawing.Point(12, 28);
            this.txtRemovalDescription.Name = "txtRemovalDescription";
            this.txtRemovalDescription.Size = new System.Drawing.Size(341, 126);
            this.txtRemovalDescription.TabIndex = 220;
            this.txtRemovalDescription.Text = "";
            // 
            // radLabel28
            // 
            this.radLabel28.AutoSize = false;
            this.radLabel28.BackColor = System.Drawing.Color.SteelBlue;
            this.radLabel28.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel28.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel28.ForeColor = System.Drawing.Color.White;
            this.radLabel28.Location = new System.Drawing.Point(0, 0);
            this.radLabel28.Name = "radLabel28";
            // 
            // 
            // 
            this.radLabel28.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel28.Size = new System.Drawing.Size(365, 22);
            this.radLabel28.TabIndex = 219;
            this.radLabel28.Text = "Description";
            this.radLabel28.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            this.btnSave.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSave.Location = new System.Drawing.Point(243, 171);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 54);
            this.btnSave.TabIndex = 221;
            this.btnSave.Text = "Save and Close";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.TextWrap = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Text = "Save and Close";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmAddRemDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(365, 233);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtRemovalDescription);
            this.Controls.Add(this.radLabel28);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddRemDescription";
            this.ShowIcon = false;
            this.Text = "Add/View Description";
            ((System.ComponentModel.ISupportInitialize)(this.radLabel28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtRemovalDescription;
        private Telerik.WinControls.UI.RadLabel radLabel28;
        private Telerik.WinControls.UI.RadButton btnSave;
    }
}