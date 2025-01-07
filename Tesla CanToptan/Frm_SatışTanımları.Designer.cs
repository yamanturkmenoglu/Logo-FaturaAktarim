
namespace Tesla_CanToptan
{
    partial class Frm_SatışTanımları
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SatışTanımları));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_FirmaKHK = new DevExpress.XtraEditors.TextEdit();
            this.Txt_BayiKHK = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.Txt_MalzemeK = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.Txt_TasiciKod = new DevExpress.XtraEditors.TextEdit();
            this.Btn_Güncelle = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_FirmaKHK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_BayiKHK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_MalzemeK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_TasiciKod.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(875, 531);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(157)))), ((int)(((byte)(227)))));
            this.label2.Location = new System.Drawing.Point(4, 573);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Firma Kar Kdv Hizmet Kodu:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(157)))), ((int)(((byte)(227)))));
            this.label1.Location = new System.Drawing.Point(12, 544);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Bayi Kar Kdv Hizmet Kodu:";
            // 
            // Txt_FirmaKHK
            // 
            this.Txt_FirmaKHK.Location = new System.Drawing.Point(172, 569);
            this.Txt_FirmaKHK.Name = "Txt_FirmaKHK";
            this.Txt_FirmaKHK.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Txt_FirmaKHK.Properties.Appearance.Options.UseFont = true;
            this.Txt_FirmaKHK.Size = new System.Drawing.Size(205, 24);
            this.Txt_FirmaKHK.TabIndex = 32;
            // 
            // Txt_BayiKHK
            // 
            this.Txt_BayiKHK.Location = new System.Drawing.Point(172, 537);
            this.Txt_BayiKHK.Name = "Txt_BayiKHK";
            this.Txt_BayiKHK.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Txt_BayiKHK.Properties.Appearance.Options.UseFont = true;
            this.Txt_BayiKHK.Size = new System.Drawing.Size(205, 24);
            this.Txt_BayiKHK.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(157)))), ((int)(((byte)(227)))));
            this.label3.Location = new System.Drawing.Point(417, 544);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Malzeme Özel Kodu:";
            // 
            // Txt_MalzemeK
            // 
            this.Txt_MalzemeK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_MalzemeK.Location = new System.Drawing.Point(551, 537);
            this.Txt_MalzemeK.Name = "Txt_MalzemeK";
            this.Txt_MalzemeK.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Txt_MalzemeK.Properties.Appearance.Options.UseFont = true;
            this.Txt_MalzemeK.Size = new System.Drawing.Size(205, 24);
            this.Txt_MalzemeK.TabIndex = 35;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(157)))), ((int)(((byte)(227)))));
            this.label4.Location = new System.Drawing.Point(456, 576);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Taşıycı kodu:";
            // 
            // Txt_TasiciKod
            // 
            this.Txt_TasiciKod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_TasiciKod.Location = new System.Drawing.Point(551, 570);
            this.Txt_TasiciKod.Name = "Txt_TasiciKod";
            this.Txt_TasiciKod.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Txt_TasiciKod.Properties.Appearance.Options.UseFont = true;
            this.Txt_TasiciKod.Size = new System.Drawing.Size(205, 24);
            this.Txt_TasiciKod.TabIndex = 37;
            // 
            // Btn_Güncelle
            // 
            this.Btn_Güncelle.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Güncelle.ImageOptions.Image")));
            this.Btn_Güncelle.Location = new System.Drawing.Point(771, 552);
            this.Btn_Güncelle.Name = "Btn_Güncelle";
            this.Btn_Güncelle.Size = new System.Drawing.Size(80, 36);
            this.Btn_Güncelle.TabIndex = 39;
            this.Btn_Güncelle.Text = "Güncelle";
            this.Btn_Güncelle.Click += new System.EventHandler(this.Btn_Güncelle_Click);
            // 
            // Frm_SatışTanımları
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 605);
            this.Controls.Add(this.Btn_Güncelle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Txt_TasiciKod);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Txt_MalzemeK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_FirmaKHK);
            this.Controls.Add(this.Txt_BayiKHK);
            this.Controls.Add(this.gridControl1);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("Frm_SatışTanımları.IconOptions.LargeImage")));
            this.MaximizeBox = false;
            this.Name = "Frm_SatışTanımları";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Satış Tanımları";
            this.Load += new System.EventHandler(this.Frm_SatışTanımları_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_FirmaKHK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_BayiKHK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_MalzemeK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_TasiciKod.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit Txt_FirmaKHK;
        private DevExpress.XtraEditors.TextEdit Txt_BayiKHK;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit Txt_MalzemeK;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit Txt_TasiciKod;
        private DevExpress.XtraEditors.SimpleButton Btn_Güncelle;
    }
}