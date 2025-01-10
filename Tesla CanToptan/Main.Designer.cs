
namespace Tesla_CanToptan
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.Btn_DosayaSec = new DevExpress.XtraBars.BarButtonItem();
            this.Btn_LogoAktar = new DevExpress.XtraBars.BarButtonItem();
            this.Btn_Temizle = new DevExpress.XtraBars.BarButtonItem();
            this.Btn_SatisTanımları = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LB_FaturaSayısı = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LB_Yol = new System.Windows.Forms.Label();
            this.LB_DosyaAdı = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LB_Tarih = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Com_isyeri = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.Com_Ambar = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Com_Fabrika = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Com_Bolum = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Com_isyeri.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Com_Ambar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Com_Fabrika.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Com_Bolum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.Btn_DosayaSec,
            this.Btn_LogoAktar,
            this.Btn_Temizle,
            this.Btn_SatisTanımları});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 5;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(1135, 147);
            // 
            // Btn_DosayaSec
            // 
            this.Btn_DosayaSec.Caption = "Dosya Aktar";
            this.Btn_DosayaSec.Id = 1;
            this.Btn_DosayaSec.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("Btn_DosayaSec.ImageOptions.LargeImage")));
            this.Btn_DosayaSec.Name = "Btn_DosayaSec";
            this.Btn_DosayaSec.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Btn_DosayaSec_ItemClick);
            // 
            // Btn_LogoAktar
            // 
            this.Btn_LogoAktar.Caption = "Logo\'ya Aktar";
            this.Btn_LogoAktar.Id = 2;
            this.Btn_LogoAktar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("Btn_LogoAktar.ImageOptions.LargeImage")));
            this.Btn_LogoAktar.Name = "Btn_LogoAktar";
            this.Btn_LogoAktar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Btn_LogoAktar_ItemClick);
            // 
            // Btn_Temizle
            // 
            this.Btn_Temizle.Caption = "Temizle";
            this.Btn_Temizle.Id = 3;
            this.Btn_Temizle.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("Btn_Temizle.ImageOptions.LargeImage")));
            this.Btn_Temizle.Name = "Btn_Temizle";
            this.Btn_Temizle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Btn_Temizle_ItemClick);
            // 
            // Btn_SatisTanımları
            // 
            this.Btn_SatisTanımları.Caption = "Satış Tanımları ";
            this.Btn_SatisTanımları.Id = 4;
            this.Btn_SatisTanımları.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("Btn_SatisTanımları.ImageOptions.LargeImage")));
            this.Btn_SatisTanımları.Name = "Btn_SatisTanımları";
            this.Btn_SatisTanımları.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Btn_SatisTanımları_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Faturalar";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.Btn_DosayaSec);
            this.ribbonPageGroup1.ItemLinks.Add(this.Btn_LogoAktar);
            this.ribbonPageGroup1.ItemLinks.Add(this.Btn_SatisTanımları);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Fatura İşlemleri";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.Btn_Temizle);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 147);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.ribbonControl1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1135, 516);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(157)))), ((int)(((byte)(227)))));
            this.panel1.Controls.Add(this.LB_FaturaSayısı);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.LB_Yol);
            this.panel1.Controls.Add(this.LB_DosyaAdı);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.LB_Tarih);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1135, 37);
            this.panel1.TabIndex = 8;
            // 
            // LB_FaturaSayısı
            // 
            this.LB_FaturaSayısı.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LB_FaturaSayısı.AutoSize = true;
            this.LB_FaturaSayısı.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LB_FaturaSayısı.ForeColor = System.Drawing.Color.White;
            this.LB_FaturaSayısı.Location = new System.Drawing.Point(851, 11);
            this.LB_FaturaSayısı.Name = "LB_FaturaSayısı";
            this.LB_FaturaSayısı.Size = new System.Drawing.Size(0, 13);
            this.LB_FaturaSayısı.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(757, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 14);
            this.label5.TabIndex = 6;
            this.label5.Text = "Fatura Sayısı:";
            // 
            // LB_Yol
            // 
            this.LB_Yol.AutoSize = true;
            this.LB_Yol.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LB_Yol.ForeColor = System.Drawing.Color.White;
            this.LB_Yol.Location = new System.Drawing.Point(1099, 11);
            this.LB_Yol.Name = "LB_Yol";
            this.LB_Yol.Size = new System.Drawing.Size(0, 13);
            this.LB_Yol.TabIndex = 5;
            // 
            // LB_DosyaAdı
            // 
            this.LB_DosyaAdı.AutoSize = true;
            this.LB_DosyaAdı.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LB_DosyaAdı.ForeColor = System.Drawing.Color.White;
            this.LB_DosyaAdı.Location = new System.Drawing.Point(613, 12);
            this.LB_DosyaAdı.Name = "LB_DosyaAdı";
            this.LB_DosyaAdı.Size = new System.Drawing.Size(0, 13);
            this.LB_DosyaAdı.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(1015, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "Dosya Yolu:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(535, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Dosya Adı:";
            // 
            // LB_Tarih
            // 
            this.LB_Tarih.AutoSize = true;
            this.LB_Tarih.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LB_Tarih.ForeColor = System.Drawing.Color.White;
            this.LB_Tarih.Location = new System.Drawing.Point(57, 13);
            this.LB_Tarih.Name = "LB_Tarih";
            this.LB_Tarih.Size = new System.Drawing.Size(0, 13);
            this.LB_Tarih.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tarih: ";
            // 
            // Com_isyeri
            // 
            this.Com_isyeri.Location = new System.Drawing.Point(56, 12);
            this.Com_isyeri.MenuManager = this.ribbonControl1;
            this.Com_isyeri.Name = "Com_isyeri";
            this.Com_isyeri.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Com_isyeri.Size = new System.Drawing.Size(133, 20);
            this.Com_isyeri.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.Com_Ambar);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.Com_Fabrika);
            this.panel2.Controls.Add(this.Com_Bolum);
            this.panel2.Controls.Add(this.Com_isyeri);
            this.panel2.Location = new System.Drawing.Point(253, 66);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(397, 81);
            this.panel2.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(203, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 14);
            this.label8.TabIndex = 18;
            this.label8.Text = "Ambar";
            // 
            // Com_Ambar
            // 
            this.Com_Ambar.Location = new System.Drawing.Point(256, 38);
            this.Com_Ambar.MenuManager = this.ribbonControl1;
            this.Com_Ambar.Name = "Com_Ambar";
            this.Com_Ambar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Com_Ambar.Size = new System.Drawing.Size(133, 20);
            this.Com_Ambar.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(3, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 14);
            this.label7.TabIndex = 16;
            this.label7.Text = "Bölüm";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(203, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 14);
            this.label6.TabIndex = 15;
            this.label6.Text = "Fabrika";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(3, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 14);
            this.label4.TabIndex = 14;
            this.label4.Text = "İşyeri";
            // 
            // Com_Fabrika
            // 
            this.Com_Fabrika.Location = new System.Drawing.Point(256, 13);
            this.Com_Fabrika.MenuManager = this.ribbonControl1;
            this.Com_Fabrika.Name = "Com_Fabrika";
            this.Com_Fabrika.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Com_Fabrika.Size = new System.Drawing.Size(133, 20);
            this.Com_Fabrika.TabIndex = 13;
            // 
            // Com_Bolum
            // 
            this.Com_Bolum.Location = new System.Drawing.Point(56, 38);
            this.Com_Bolum.MenuManager = this.ribbonControl1;
            this.Com_Bolum.Name = "Com_Bolum";
            this.Com_Bolum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Com_Bolum.Size = new System.Drawing.Size(133, 20);
            this.Com_Bolum.TabIndex = 12;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 663);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.ribbonControl1);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("Main.IconOptions.LargeImage")));
            this.IsMdiContainer = true;
            this.Name = "Main";
            this.Ribbon = this.ribbonControl1;
            this.Text = "Tesla CanToptan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Com_isyeri.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Com_Ambar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Com_Fabrika.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Com_Bolum.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem Btn_DosayaSec;
        private DevExpress.XtraBars.BarButtonItem Btn_LogoAktar;
        private DevExpress.XtraBars.BarButtonItem Btn_Temizle;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarButtonItem Btn_SatisTanımları;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LB_Tarih;
        private System.Windows.Forms.Label LB_Yol;
        private System.Windows.Forms.Label LB_DosyaAdı;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LB_FaturaSayısı;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.ComboBoxEdit Com_isyeri;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.ComboBoxEdit Com_Fabrika;
        private DevExpress.XtraEditors.ComboBoxEdit Com_Bolum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.ComboBoxEdit Com_Ambar;
    }
}

