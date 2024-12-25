using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace Tesla_CanToptan
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Main()
        {
            InitializeComponent();
        }
 SqlConnectionClass bgl = new SqlConnectionClass();
        private void Btn_DosayaSec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteDataFromDatabase();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files|*.bif";
                openFileDialog.Title = "Fatura Dosyasını Seçin";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        LB_DosyaAdı.Text = Path.GetFileName(filePath); 
                        LB_Yol.Text = filePath;
                        string[] faturaVerisi = File.ReadAllLines(filePath, Encoding.GetEncoding("ISO-8859-9"));
                        List<Fatura> faturalar = ParseFaturalar(faturaVerisi);
                      
                        InsertDataIntoDatabase(faturalar);
                       
                        List<Fatura> faturalarFromDb = GetFaturalarFromDatabase();
                        gridControl1.DataSource = faturalarFromDb;
                        gridControl1.ForceInitialize();
                        LB_FaturaSayısı.Text = faturalarFromDb.Count.ToString();
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Dosya okuma hatası: {ex.Message}");
                    }
                }
            }
        }

        private List<Fatura> ParseFaturalar(string[] faturaVerisi)
        {
            Fatura currentFatura = null;
            List<Fatura> faturalar = new List<Fatura>();

            foreach (string line in faturaVerisi)
            {
                string cleanLine = line.Trim();

                if (cleanLine.StartsWith("@5"))
                {
                    currentFatura = ParseFatura(cleanLine);
                    if (currentFatura != null)
                        faturalar.Add(currentFatura);
                }
                else if (cleanLine.StartsWith("@6") && currentFatura != null)
                {
                    var kalem = ParseKalem(cleanLine);
                    if (kalem != null)
                        currentFatura.Kalemler.Add(kalem);
                }
            }

            return faturalar;
        }

        private Fatura ParseFatura(string line)
        {
            try
            {
                if (line.Length >= 148)
                {
                    return new Fatura
                    {
                        TarihSaat = ParseDateTime(line.Substring(4, 14)),
                        CariKodu = line.Substring(27, 10).Trim(),
                        CariUnvan = line.Substring(38, 41).Trim(),
                        FaturaNumarasi = line.Substring(79, 16).Trim(),
                        ToplamIndirim = ParseDecimal(line.Substring(108, 12).Trim()),
                        OdemeDurumu = ParseInt(line.Substring(147, 1).Trim())
                    };
                }
                else
                {
                    XtraMessageBox.Show("Fatura satırı beklenenden kısa: " + line);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Fatura parse hatası: {ex.Message}\nSatır: {line}");
            }
            return null;
        }

        private FaturaKalemi ParseKalem(string line)
        {
            try
            {
                if (line.Length >= 140)
                {
                    decimal birimFiyat = ParseDecimal(line.Substring(105, 12).Trim());
                    decimal kdvOrani = ParseInt(line.Substring(118, 4).Trim());
                    decimal kdvliBirimFiyat = birimFiyat * (1 + kdvOrani / 100);

                    return new FaturaKalemi
                    {
                        UrunKodu = line.Substring(30, 15).Trim(),
                        UrunAdi = line.Substring(45, 51).Trim(),
                        Miktar = ParseDecimal(line.Substring(96, 8).Trim()),
                        BirimFiyat = birimFiyat,
                        KDVOrani = (int)kdvOrani,
                        KDVliBirimFiyat = kdvliBirimFiyat,
                        ToplamTutar = ParseDecimal(line.Substring(123, 12).Trim())
                    };
                }
                else
                {
                    XtraMessageBox.Show("Kalem satırı beklenenden kısa: " + line);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Kalem parse hatası: {ex.Message}\nSatır: {line}");
            }
            return null;
        }

        private decimal ParseDecimal(string value)
        {
            if (decimal.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
            return 0;
        }

        private int ParseInt(string value)
        {
            return int.TryParse(value, out var result) ? result : 0;
        }

        private DateTime? ParseDateTime(string value)
        {
            if (DateTime.TryParseExact(value, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out var result))
            {
                return result;
            }
            return null;
        }

        private void InsertDataIntoDatabase(List<Fatura> faturalar)
        {
            using (SqlConnection connection = bgl.baglanti())
            {
                foreach (var fatura in faturalar)
                {
                    // HRK.FaturaBasliklari tablosuna veri ekleme
                    string insertFaturaBaslikQuery = @"
                INSERT INTO HRK.FaturaBasliklari (TarihSaat, CariKodu, CariUnvan, FaturaNumarasi, ToplamIndirim, OdemeDurumu)
                VALUES (@TarihSaat, @CariKodu, @CariUnvan, @FaturaNumarasi, @ToplamIndirim, @OdemeDurumu);
                SELECT SCOPE_IDENTITY();";

                    SqlCommand command = new SqlCommand(insertFaturaBaslikQuery, connection);
                    command.Parameters.AddWithValue("@TarihSaat", fatura.TarihSaat ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CariKodu", fatura.CariKodu);
                    command.Parameters.AddWithValue("@CariUnvan", fatura.CariUnvan);
                    command.Parameters.AddWithValue("@FaturaNumarasi", fatura.FaturaNumarasi);
                    command.Parameters.AddWithValue("@ToplamIndirim", fatura.ToplamIndirim);
                    command.Parameters.AddWithValue("@OdemeDurumu", fatura.OdemeDurumu);

                    int faturaId = Convert.ToInt32(command.ExecuteScalar());

                    // HRK.FaturaKalemleri tablosuna veri ekleme
                    foreach (var kalem in fatura.Kalemler)
                    {
                        string insertKalemQuery = @"
                    INSERT INTO HRK.FaturaKalemleri (FaturaId, UrunKodu, UrunAdi, Miktar, BirimFiyat, KDVOrani, KDVliBirimFiyat, ToplamTutar)
                    VALUES (@FaturaId, @UrunKodu, @UrunAdi, @Miktar, @BirimFiyat, @KDVOrani, @KDVliBirimFiyat, @ToplamTutar);";

                        SqlCommand kalemCommand = new SqlCommand(insertKalemQuery, connection);
                        kalemCommand.Parameters.AddWithValue("@FaturaId", faturaId);
                        kalemCommand.Parameters.AddWithValue("@UrunKodu", kalem.UrunKodu);
                        kalemCommand.Parameters.AddWithValue("@UrunAdi", kalem.UrunAdi);
                        kalemCommand.Parameters.AddWithValue("@Miktar", kalem.Miktar);
                        kalemCommand.Parameters.AddWithValue("@BirimFiyat", kalem.BirimFiyat);
                        kalemCommand.Parameters.AddWithValue("@KDVOrani", kalem.KDVOrani);
                        kalemCommand.Parameters.AddWithValue("@KDVliBirimFiyat", kalem.KDVliBirimFiyat);
                        kalemCommand.Parameters.AddWithValue("@ToplamTutar", kalem.ToplamTutar);

                        kalemCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private List<Fatura> GetFaturalarFromDatabase()
        {
            List<Fatura> faturalar = new List<Fatura>();

            using (SqlConnection connection = bgl.baglanti())
            {
                string selectQuery = @"
            SELECT FaturaId, TarihSaat, CariKodu, CariUnvan, FaturaNumarasi, ToplamIndirim, OdemeDurumu 
            FROM HRK.FaturaBasliklari";

                SqlCommand command = new SqlCommand(selectQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Fatura fatura = new Fatura
                    {
                        FaturaId = reader.GetInt32(0),
                        TarihSaat = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1),
                        CariKodu = reader.GetString(2),
                        CariUnvan = reader.GetString(3),
                        FaturaNumarasi = reader.GetString(4),
                        ToplamIndirim = reader.GetDecimal(5),
                        OdemeDurumu = reader.GetInt32(6),
                        Kalemler = new List<FaturaKalemi>()
                    };

                    faturalar.Add(fatura);
                }
                reader.Close();

                // Fatura kalemlerini ekleyelim
                foreach (var fatura in faturalar)
                {
                    string selectKalemQuery = "SELECT * FROM HRK.FaturaKalemleri WHERE FaturaId = @FaturaId";
                    SqlCommand kalemCommand = new SqlCommand(selectKalemQuery, connection);
                    kalemCommand.Parameters.AddWithValue("@FaturaId", fatura.FaturaId);
                    SqlDataReader kalemReader = kalemCommand.ExecuteReader();

                    while (kalemReader.Read())
                    {
                        FaturaKalemi kalem = new FaturaKalemi
                        {
                            UrunKodu = kalemReader.GetString(2),
                            UrunAdi = kalemReader.GetString(3),
                            Miktar = kalemReader.GetDecimal(4),
                            BirimFiyat = kalemReader.GetDecimal(5),
                            KDVOrani = kalemReader.GetInt32(6),
                            KDVliBirimFiyat = kalemReader.GetDecimal(7),
                            ToplamTutar = kalemReader.GetDecimal(8)
                        };

                        fatura.Kalemler.Add(kalem);
                    }
                    kalemReader.Close();
                }
            }

            return faturalar;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigureGridControl();
            DateTime DataSetDateTime = DateTime.Now;
            LB_Tarih.Text = DataSetDateTime.ToString("dd.MM.yyyy HH:mm:ss");
            
        }

        private void ConfigureGridControl()
        {
            GridView masterView = new GridView(gridControl1);
            gridControl1.MainView = masterView;
       

            masterView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "TarihSaat", Caption = "Tarih Saat", Visible = true });
            masterView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "CariKodu", Caption = "Müşteri Kod", Visible = true });
            masterView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "CariUnvan", Caption = "Müşteri", Visible = true });
            masterView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "FaturaNumarasi", Caption = "Fatura Numarası", Visible = true });
            masterView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "ToplamIndirim", Caption = "Toplam İndirim", Visible = true });
            masterView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "OdemeDurumu", Caption = "Durum", Visible = true });

            GridView detailView = new GridView(gridControl1);
            detailView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "UrunKodu", Caption = "Malzeme Kodu", Visible = true });
            detailView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "UrunAdi", Caption = "Malzeme Adı", Visible = true });
            detailView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "BirimFiyat", Caption = "Birim Fiyat", Visible = true });
            detailView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "KDVOrani", Caption = "KDV", Visible = true });
            detailView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "KDVliBirimFiyat", Caption = "KDV'li Birim Fiyat", Visible = true });
            detailView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "Miktar", Caption = "Miktar", Visible = true });
            detailView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "ToplamTutar", Caption = "Toplam Tutar", Visible = true });

            GridLevelNode gridLevelNode = new GridLevelNode
            {
                LevelTemplate = detailView,
                RelationName = "Kalemler"
            };
            gridControl1.LevelTree.Nodes.Add(gridLevelNode);

            masterView.MasterRowGetChildList += MasterView_MasterRowGetChildList;
            masterView.MasterRowEmpty += MasterView_MasterRowEmpty;
            masterView.MasterRowGetRelationName += MasterView_MasterRowGetRelationName;
            masterView.MasterRowGetRelationCount += MasterView_MasterRowGetRelationCount;


            masterView.OptionsView.EnableAppearanceEvenRow = true;
            masterView.OptionsView.EnableAppearanceOddRow = true;
            masterView.Appearance.EvenRow.BackColor = System.Drawing.Color.LightBlue;
            masterView.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            masterView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
            masterView.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.DarkBlue;
            masterView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10);
            masterView.Appearance.Row.ForeColor = System.Drawing.Color.Black;

            // Kolon başlıklarını ortala
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in masterView.Columns)
            {
                column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }

            // Detay view stil ayarları
            detailView.OptionsView.EnableAppearanceEvenRow = true;
            detailView.OptionsView.EnableAppearanceOddRow = true;
            detailView.Appearance.EvenRow.BackColor = System.Drawing.Color.LightGreen;
            detailView.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            detailView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
            detailView.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.DarkGreen;
            detailView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10);
            detailView.Appearance.Row.ForeColor = System.Drawing.Color.Black;

            // Kolon başlıklarını ortala
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in detailView.Columns)
            {
                column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }

            // Grid görünüm ayarları
            gridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            gridControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
        }

        private void MasterView_MasterRowEmpty(object sender, MasterRowEmptyEventArgs e)
        {
            e.IsEmpty = false;
        }

        private void MasterView_MasterRowGetRelationCount(object sender, MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void MasterView_MasterRowGetRelationName(object sender, MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "Kalemler";
        }

        private void MasterView_MasterRowGetChildList(object sender, MasterRowGetChildListEventArgs e)
        {
            GridView view = sender as GridView;

           
            Fatura selectedFatura = (Fatura)view.GetRow(e.RowHandle);

           
            e.ChildList = selectedFatura?.Kalemler;
        }


        private void Btn_Temizle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DeleteDataFromDatabase();

                XtraMessageBox.Show("Tablo temizlendi ve veritabanı sıfırlandı.");
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Hata: {ex.Message}");
                }
        }
private void DeleteDataFromDatabase()
{
    using (SqlConnection connection = bgl.baglanti())
    {
        string disableForeignKeyQuery = "ALTER TABLE HRK.FaturaBasliklari NOCHECK CONSTRAINT ALL";
        SqlCommand disableFKCommand = new SqlCommand(disableForeignKeyQuery, connection);
        disableFKCommand.ExecuteNonQuery();

        string deleteKalemQuery = "DELETE FROM HRK.FaturaKalemleri";
        SqlCommand kalemCommand = new SqlCommand(deleteKalemQuery, connection);
        kalemCommand.ExecuteNonQuery();

        string deleteFaturaQuery = "DELETE FROM HRK.FaturaBasliklari";
        SqlCommand faturaCommand = new SqlCommand(deleteFaturaQuery, connection);
        faturaCommand.ExecuteNonQuery();

        string enableForeignKeyQuery = "ALTER TABLE HRK.FaturaBasliklari CHECK CONSTRAINT ALL";
        SqlCommand enableFKCommand = new SqlCommand(enableForeignKeyQuery, connection);
        enableFKCommand.ExecuteNonQuery();

        string resetIdentityQuery1 = "DBCC CHECKIDENT ('HRK.FaturaKalemleri', RESEED, 0)";
        SqlCommand resetIdentityCommand1 = new SqlCommand(resetIdentityQuery1, connection);
        resetIdentityCommand1.ExecuteNonQuery();

        string resetIdentityQuery2 = "DBCC CHECKIDENT ('HRK.FaturaBasliklari', RESEED, 0)";
        SqlCommand resetIdentityCommand2 = new SqlCommand(resetIdentityQuery2, connection);
        resetIdentityCommand2.ExecuteNonQuery();
    }

    LB_DosyaAdı.Text = null;
    LB_Yol.Text = null;
    LB_FaturaSayısı.Text = null;

    gridControl1.DataSource = null;
    gridControl1.DataSource = new List<Fatura>();
    gridControl1.ForceInitialize();
}
        Frm_SatışTanımları frm;
        private void Btn_SatisTanımları_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm = new Frm_SatışTanımları();
            frm.Show();
        }
    }

    public class Fatura
    {
        public int FaturaId { get; set; }
        public DateTime? TarihSaat { get; set; }
        public string CariKodu { get; set; }
        public string CariUnvan { get; set; }
        public string FaturaNumarasi { get; set; }
        public decimal ToplamIndirim { get; set; }
        public int OdemeDurumu { get; set; }
        public List<FaturaKalemi> Kalemler { get; set; } = new List<FaturaKalemi>();
    }

    public class FaturaKalemi
    {
        public int KalemId { get; set; }
        public int FaturaId { get; set; }
        public string UrunKodu { get; set; }
        public string UrunAdi { get; set; }
        public decimal Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public int KDVOrani { get; set; }
        public decimal KDVliBirimFiyat { get; set; }
        public decimal ToplamTutar { get; set; }
    }

}
