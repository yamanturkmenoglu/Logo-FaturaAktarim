using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Tesla_CanToptan
{
    public partial class Frm_SatışTanımları : DevExpress.XtraEditors.XtraForm
    {
        public Frm_SatışTanımları()
        {
            InitializeComponent();
        }

        SqlConnectionClass bgl = new SqlConnectionClass(); 

        private void Frm_SatışTanımları_Load(object sender, EventArgs e)
        {
            InsertData();       
            LoadData();         
            ConfigureGrid();   
            LoadLocalSettings(); 

           
            Txt_MalzemeK.EditValueChanged += (s, args) => SaveLocalSettings();
            Txt_FirmaKHK.EditValueChanged += (s, args) => SaveLocalSettings();
            Txt_BayiKHK.EditValueChanged += (s, args) => SaveLocalSettings();
        }
        private void InsertData()
        {
            // 'using' ile SqlConnection nesnesi yönetimi sağlanır, bağlantı sonunda otomatik kapanır.
            using (SqlConnection connection = bgl.baglanti())
            {
                var malzemeKodu = Properties.Settings.Default.MalzemeKodu; // 'PM' veya başka bir değer
                string logoDatabase = ConfigurationManager.AppSettings["LogoDatabase"]; // 'GODB'
                string firmaNumarasi = ConfigurationManager.AppSettings["FirmaNumarasi"];

                // Dinamik olarak veritabanı adı ekleyerek SQL sorgusunu oluşturuyoruz
                string query = $@"
            INSERT INTO [TNM.MALZEME FİYAT] (CODE, NAME)
            SELECT CODE, NAME
            FROM {logoDatabase}..LG_{firmaNumarasi}_ITEMS
            WHERE SPECODE = @MalzemeKodu
            AND NOT EXISTS (
                SELECT 1
                FROM [TNM.MALZEME FİYAT]
                WHERE CODE = LG_024_ITEMS.CODE
            )";

                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MalzemeKodu", malzemeKodu);

                    
                    command.ExecuteNonQuery();
                }
            }
        }




        private void LoadData()
        {
            using (SqlConnection connection = bgl.baglanti())
            {
                string query = "SELECT ID, CODE, NAME, BAYI_SATIS_FIYATI FROM [TNM.MALZEME FİYAT]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);  
                gridControl1.DataSource = table;  
            }
        }

        
        private void ConfigureGrid()
        {
            gridView1.Columns["BAYI_SATIS_FIYATI"].Caption = "Bayi Satış Fiyatı"; 
            gridView1.Columns["CODE"].Caption = "Malzeme Kodu";  
            gridView1.Columns["NAME"].Caption = "Malzeme Açıklaması";  
            gridView1.Columns["ID"].Visible = false;

            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            gridView1.OptionsView.EnableAppearanceOddRow = true;
            gridView1.Appearance.OddRow.BackColor = Color.White;
            gridView1.Appearance.HeaderPanel.Font = new Font("Tahoma", 10, FontStyle.Bold);
            gridView1.Appearance.HeaderPanel.ForeColor = Color.DarkBlue;
            gridView1.Appearance.Row.Font = new Font("Tahoma", 10);
            gridView1.Appearance.Row.ForeColor = Color.Black;

           
            gridView1.Columns["BAYI_SATIS_FIYATI"].Visible = true;
            gridView1.Columns["BAYI_SATIS_FIYATI"].Caption = "Bayi Satış Fiyatı";  
            gridView1.Columns["BAYI_SATIS_FIYATI"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns["BAYI_SATIS_FIYATI"].DisplayFormat.FormatString = "c2"; 

            
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn()
            {
                FieldName = "İşlemler",
                Caption = "İşlemler",
                Visible = true
            });

            
            RepositoryItemButtonEdit buttonEdit = new RepositoryItemButtonEdit();
            buttonEdit.Buttons[0].Caption = "..."; 
            buttonEdit.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            buttonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            buttonEdit.Buttons[0].Width = 20; 
            buttonEdit.ButtonClick += ButtonEdit_ButtonClick;
            buttonEdit.Buttons[0].Appearance.BackColor = Color.LightBlue;
            buttonEdit.Buttons[0].Appearance.ForeColor = Color.Black;

           
            gridView1.Columns["İşlemler"].ColumnEdit = buttonEdit;
        }

       
        private void ButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            if (rowHandle >= 0)
            {
                string code = gridView1.GetRowCellValue(rowHandle, "CODE").ToString();
                string name = gridView1.GetRowCellValue(rowHandle, "NAME").ToString();
                decimal? bayiSatisFiyati = gridView1.GetRowCellValue(rowHandle, "BAYI_SATIS_FIYATI") as decimal?;

                // Kullanıcıdan yeni fiyatı al
                string input = Microsoft.VisualBasic.Interaction.InputBox(
                    $"'{name}' için yeni bayi satış fiyatını girin:",
                    "Bayi Satış Fiyatı Güncelle",
                    bayiSatisFiyati?.ToString() ?? "0");

                if (decimal.TryParse(input, out decimal yeniFiyat))
                {
                    UpdateFiyatInDatabase(code, yeniFiyat);
                    LoadData(); 
                }
            }
        }

      
        private void UpdateFiyatInDatabase(string code, decimal yeniFiyat)
        {
            using (SqlConnection connection = bgl.baglanti())
            {
                string query = "UPDATE [TNM.MALZEME FİYAT] SET BAYI_SATIS_FIYATI = @YeniFiyat WHERE CODE = @Code";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@YeniFiyat", yeniFiyat);
                command.Parameters.AddWithValue("@Code", code);
                command.ExecuteNonQuery();  
            }
        }

        
        private void LoadLocalSettings()
        {
            Txt_MalzemeK.Text = Properties.Settings.Default.MalzemeKodu;
            Txt_FirmaKHK.Text = Properties.Settings.Default.FirmaKodu;
            Txt_BayiKHK.Text = Properties.Settings.Default.BayiKodu;
        }

       
        private void SaveLocalSettings()
        {
            Properties.Settings.Default.MalzemeKodu = Txt_MalzemeK.Text;
            Properties.Settings.Default.FirmaKodu = Txt_FirmaKHK.Text;
            Properties.Settings.Default.BayiKodu = Txt_BayiKHK.Text;
            Properties.Settings.Default.Save(); 
        }
    }
}
