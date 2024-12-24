using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace Tesla_CanToptan
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_DosayaSec_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files|*.bif";
                openFileDialog.Title = "Fatura Dosyasını Seçin";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        string[] faturaVerisi = File.ReadAllLines(filePath, Encoding.GetEncoding("ISO-8859-9"));
                        List<Fatura> faturalar = ParseFaturalar(faturaVerisi);

                        gridControl1.DataSource = faturalar;
                        gridControl1.ForceInitialize();
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
                        OncekiOdeme = ParseDecimal(line.Substring(108, 12).Trim()),
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

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigureGridControl();
        }

        private void ConfigureGridControl()
        {
            GridView masterView = new GridView(gridControl1);
            gridControl1.MainView = masterView;

            masterView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "TarihSaat", Caption = "Tarih Saat", Visible = true });
            masterView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "CariKodu", Caption = "Müşteri Kod", Visible = true });
            masterView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "CariUnvan", Caption = "Müşteri", Visible = true });
            masterView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "FaturaNumarasi", Caption = "Fatura Numarası", Visible = true });
            masterView.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn() { FieldName = "OncekiOdeme", Caption = "Toplam İndirim", Visible = true });
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

        private void MasterView_MasterRowGetChildList(object sender, MasterRowGetChildListEventArgs e)
        {
            GridView view = sender as GridView;
            List<Fatura> faturalar = (List<Fatura>)gridControl1.DataSource;

            Fatura selectedFatura = faturalar[e.RowHandle];
            e.ChildList = selectedFatura.Kalemler;
        }

        private void MasterView_MasterRowEmpty(object sender, MasterRowEmptyEventArgs e)
        {
            GridView view = sender as GridView;
            List<Fatura> faturalar = (List<Fatura>)gridControl1.DataSource;

            Fatura selectedFatura = faturalar[e.RowHandle];
            e.IsEmpty = selectedFatura.Kalemler.Count == 0;
        }

        private void MasterView_MasterRowGetRelationName(object sender, MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "Kalemler";
        }

        private void MasterView_MasterRowGetRelationCount(object sender, MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void Btn_Temizle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            // Veri kaynağını sıfırla
            gridControl1.DataSource = null;

            // Boş bir liste ata
            gridControl1.DataSource = new List<Fatura>();

            // GridControl'i yeniden başlat
            gridControl1.ForceInitialize();

            // Kullanıcıya bilgilendirme mesajı
            XtraMessageBox.Show("Tablo temizlendi.");
        }

    }

    public class Fatura
    {
        public DateTime? TarihSaat { get; set; }
        public string CariKodu { get; set; }
        public string CariUnvan { get; set; }
        public string FaturaNumarasi { get; set; }
        public decimal OncekiOdeme { get; set; }
        public int OdemeDurumu { get; set; }
        public List<FaturaKalemi> Kalemler { get; set; } = new List<FaturaKalemi>();
    }

    public class FaturaKalemi
    {
        public string UrunKodu { get; set; }
        public string UrunAdi { get; set; }
        public decimal BirimFiyat { get; set; }
        public int KDVOrani { get; set; }
        public decimal KDVliBirimFiyat { get; set; }
        public decimal Miktar { get; set; }
        public decimal ToplamTutar { get; set; }
    }
}
