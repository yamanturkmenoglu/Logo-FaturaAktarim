using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
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

        SqlConnectionClass bgl = new SqlConnectionClass(); // SqlConnectionClass'tan nesne oluştur

        private void Frm_SatışTanımları_Load(object sender, EventArgs e)
        {
            InsertData();       // Veritabanına veri ekle
            LoadData();         // Veriyi GridControl'e yükle
            ConfigureGrid();    // GridControl'de butonlu editör yapılandır
            LoadLocalSettings(); // Lokal hafızada kaydedilen değerleri yükle

            // TextEdit kontrollerinde değişiklik yapıldığında kaydetme işlemi
            Txt_MalzemeK.EditValueChanged += (s, args) => SaveLocalSettings();
            Txt_FirmaKHK.EditValueChanged += (s, args) => SaveLocalSettings();
            Txt_BayiKHK.EditValueChanged += (s, args) => SaveLocalSettings();
        }

        // Veritabanına INSERT işlemi
        private void InsertData()
        {
            using (SqlConnection connection = bgl.baglanti())
            {
                string query = @"
                    INSERT INTO [TNM.MALZEME FİYAT] (CODE, NAME)
                    SELECT CODE, NAME
                    FROM GODB..LG_024_ITEMS
                    WHERE SPECODE = 'PM'
                    AND NOT EXISTS (
                        SELECT 1
                        FROM [TNM.MALZEME FİYAT]
                        WHERE [TNM.MALZEME FİYAT].CODE = LG_024_ITEMS.CODE
                    )";

                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();  // INSERT sorgusunu çalıştır
            }
        }

        // GridControl verilerini yükler
        private void LoadData()
        {
            using (SqlConnection connection = bgl.baglanti())
            {
                string query = "SELECT ID, CODE, NAME, BAYI_SATIS_FIYATI FROM [TNM.MALZEME FİYAT]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);  // Veriyi DataTable'a al
                gridControl1.DataSource = table;  // GridControl'e veri yükle
            }
        }

        // GridControl'deki butonlu ve fiyat gösteren editörü yapılandırır
        private void ConfigureGrid()
        {
            gridView1.Columns["BAYI_SATIS_FIYATI"].Caption = "Bayi Satış Fiyatı";  // Başlık değiştirme
            gridView1.Columns["CODE"].Caption = "Malzeme Kodu";  // Başlık değiştirme
            gridView1.Columns["NAME"].Caption = "Malzeme Açıklaması";  // Başlık değiştirme
            gridView1.Columns["ID"].Visible = false;

            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            gridView1.OptionsView.EnableAppearanceOddRow = true;
            gridView1.Appearance.OddRow.BackColor = Color.White;
            gridView1.Appearance.HeaderPanel.Font = new Font("Tahoma", 10, FontStyle.Bold);
            gridView1.Appearance.HeaderPanel.ForeColor = Color.DarkBlue;
            gridView1.Appearance.Row.Font = new Font("Tahoma", 10);
            gridView1.Appearance.Row.ForeColor = Color.Black;

            // Yeni bir kolon ekleyelim, "BAYI_SATIS_FIYATI" kolonunu gösterecek
            gridView1.Columns["BAYI_SATIS_FIYATI"].Visible = true;
            gridView1.Columns["BAYI_SATIS_FIYATI"].Caption = "Bayi Satış Fiyatı";  // Kolon başlığı
            gridView1.Columns["BAYI_SATIS_FIYATI"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridView1.Columns["BAYI_SATIS_FIYATI"].DisplayFormat.FormatString = "c2"; // İki ondalıklı format

            // "İşlemler" adında yeni bir kolon ekliyoruz
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn()
            {
                FieldName = "İşlemler",
                Caption = "İşlemler",
                Visible = true
            });

            // Butonlu editör oluşturuyoruz
            RepositoryItemButtonEdit buttonEdit = new RepositoryItemButtonEdit();
            buttonEdit.Buttons[0].Caption = "...";  // Butonun üzerindeki metin
            buttonEdit.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            buttonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            buttonEdit.Buttons[0].Width = 20; // Butonun genişliğini küçült
            buttonEdit.ButtonClick += ButtonEdit_ButtonClick;
            buttonEdit.Buttons[0].Appearance.BackColor = Color.LightBlue;
            buttonEdit.Buttons[0].Appearance.ForeColor = Color.Black;

            // "İşlemler" kolonuna buton ekliyoruz
            gridView1.Columns["İşlemler"].ColumnEdit = buttonEdit;
        }

        // Buton tıklama olayını işler
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
                    UpdateFiyatInDatabase(code, yeniFiyat); // Veritabanında güncelle
                    LoadData(); // GridControl'ü yeniden yükle
                }
            }
        }

        // Fiyatı veritabanında günceller
        private void UpdateFiyatInDatabase(string code, decimal yeniFiyat)
        {
            using (SqlConnection connection = bgl.baglanti())
            {
                string query = "UPDATE [TNM.MALZEME FİYAT] SET BAYI_SATIS_FIYATI = @YeniFiyat WHERE CODE = @Code";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@YeniFiyat", yeniFiyat);
                command.Parameters.AddWithValue("@Code", code);
                command.ExecuteNonQuery();  // Veritabanında güncelleme işlemi
            }
        }

        // Lokal hafızada saklanan değerleri yükler
        private void LoadLocalSettings()
        {
            Txt_MalzemeK.Text = Properties.Settings.Default.MalzemeKodu;
            Txt_FirmaKHK.Text = Properties.Settings.Default.FirmaKodu;
            Txt_BayiKHK.Text = Properties.Settings.Default.BayiKodu;
        }

        // Kullanıcının girdiği değerleri lokal hafızaya kaydeder
        private void SaveLocalSettings()
        {
            Properties.Settings.Default.MalzemeKodu = Txt_MalzemeK.Text;
            Properties.Settings.Default.FirmaKodu = Txt_FirmaKHK.Text;
            Properties.Settings.Default.BayiKodu = Txt_BayiKHK.Text;
            Properties.Settings.Default.Save(); // Değişiklikleri kaydet
        }
    }
}
