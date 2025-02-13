using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Tesla_CanToptan
{
    public partial class Frm_hata_ : XtraForm
    {
        
        public List<string> ErrorMessages { get; set; }

        
        public Frm_hata_(List<string> errorMessages)
        {
            InitializeComponent();
            ErrorMessages = errorMessages;
        }

        private void Frm_hata__Load(object sender, EventArgs e)
        {
           
            var data = ErrorMessages
                .Select(msg => new { HataMesaji = msg })
                .ToList();

            gridControl1.DataSource = data;
        }
    }
}
