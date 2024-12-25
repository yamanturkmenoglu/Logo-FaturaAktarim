//using DevExpress.XtraEditors;
//using DevExpress.XtraEditors.Repository;
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Windows.Forms;

//namespace Tesla_CanToptan
//{
//    public partial class Frm_SatışTanımları : DevExpress.XtraEditors.XtraForm
//    {
//        public Frm_SatışTanımları()
//        {
//            InitializeComponent();
//        }

//        SqlConnectionClass bgl = new SqlConnectionClass(); // SqlConnectionClass'tan nesne oluştur

//        private void Frm_SatışTanımları_Load(object sender, EventArgs e)
//        {
//            LoadData();       // Veriyi GridControl'e yükle
//            ConfigureGrid();  // GridControl'de butonlu editör yapılandır
//        }

//        private void LoadData()
//        {
//            // Bağlantıyı açıyoruz ve veriyi alıyoruz
//            using (SqlConnection connection = bgl.baglanti())
//            {
//                string query = "SELECT ID, CODE, NAME, BAYI_SATIS_FIYATI FROM [TNM.MALZEME FİYAT]";
//                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
//                DataTable table = new DataTable();
//                adapter.Fill(table);  // Veriyi DataTable'a al
//                gridControl1.DataSource = table;  // GridControl'e veri yükle
//            }
//        }

//        private void ConfigureGrid()
//        {
//            RepositoryItemButtonEdit buttonEdit = new RepositoryItemButtonEdit();
//            buttonEdit.Buttons[0].Caption = "...";
//            buttonEdit.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
//            buttonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
//            buttonEdit.ButtonClick += ButtonEdit_ButtonClick;

//            gridView1.Columns["BAYI_SATIS_FIYATI"].ColumnEdit = buttonEdit;
//        }

//        private void ButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
//        {
//            // Seçilen satırı al
//            int rowHandle = gridView1.FocusedRowHandle;
//            if (rowHandle >= 0)
//            {
//                string code = gridView1.GetRowCellValue(rowHandle, "CODE").ToString();
//                string name = gridView1.GetRowCellValue(rowHandle, "NAME").ToString();
//                decimal? bayiSatisFiyati = gridView1.GetRowCellValue(rowHandle, "BAYI_SATIS_FIYATI") as decimal?;

//                // Yeni fiyatı kullanıcıdan al
//                string input = Microsoft.VisualBasic.Interaction.InputBox(
//                    $"'{name}' için yeni bayi satış fiyatını girin:",
//                    "Bayi Satış Fiyatı Güncelle",
//                    bayiSatisFiyati?.ToString() ?? "0");

//                if (decimal.TryParse(input, out decimal yeniFiyat))
//                {
//                    UpdateFiyatInDatabase(code, yeniFiyat); // Veritabanında güncelle
//                    LoadData(); // GridControl'ü yeniden yükle
//                }
//            }
//        }

//        private void UpdateFiyatInDatabase(string code, decimal yeniFiyat)
//        {
//            // Bağlantıyı açıyoruz ve fiyatı güncelliyoruz
//            using (SqlConnection connection = bgl.baglanti())
//            {
//                string query = "UPDATE [TNM.MALZEME FİYAT] SET BAYI_SATIS_FIYATI = @YeniFiyat WHERE CODE = @Code";
//                SqlCommand command = new SqlCommand(query, connection);
//                command.Parameters.AddWithValue("@YeniFiyat", yeniFiyat);
//                command.Parameters.AddWithValue("@Code", code);
//                command.ExecuteNonQuery();  // Veritabanında güncelleme işlemi
//            }
//        }
//    }
//}
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Data;
using System.Data.SqlClient;
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
    }
}
