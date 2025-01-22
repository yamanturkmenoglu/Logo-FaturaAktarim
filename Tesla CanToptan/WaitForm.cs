using DevExpress.XtraWaitForm;
using System;

namespace Tesla_CanToptan
{
    public partial class WaitForm : DevExpress.XtraWaitForm.WaitForm 
    {
        public WaitForm()
        {
            InitializeComponent();
            
            this.progressPanel1.Caption = "Lütfen Bekleyin...";
            this.progressPanel1.Description = "Yükleniyor...";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        #region Methods

        public void UpdateCaption(string caption)
        {
            this.progressPanel1.Caption = caption;
        }

        public void UpdateDescription(string description)
        {
            this.progressPanel1.Description = description;
        }

        #endregion
    }
}
