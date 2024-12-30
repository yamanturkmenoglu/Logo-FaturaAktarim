using DevExpress.XtraEditors;
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
            using (SqlConnection connection = bgl.baglanti())
            {
                var malzemeKodu = Properties.Settings.Default.MalzemeKodu;
                string logoDatabase = ConfigurationManager.AppSettings["LogoDatabase"];
                string firmaNumarasi = ConfigurationManager.AppSettings["FirmaNumarasi"];

                string query = $@"
                INSERT INTO [TNM.MALZEME FİYAT] (CODE, NAME)
                SELECT CODE, NAME
                FROM {logoDatabase}..LG_{firmaNumarasi}_ITEMS
                WHERE SPECODE = @MalzemeKodu
                AND NOT EXISTS (
                    SELECT 1
                    FROM [TNM.MALZEME FİYAT]
                    WHERE CODE = LG_{firmaNumarasi}_ITEMS.CODE
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

            gridView1.Columns["BAYI_SATIS_FIYATI"].OptionsColumn.AllowEdit = true;

            gridView1.CellValueChanged += GridView1_CellValueChanged;
            gridView1.KeyDown += GridView1_KeyDown;
        }

        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "BAYI_SATIS_FIYATI")
            {
                int rowHandle = e.RowHandle;
                string code = gridView1.GetRowCellValue(rowHandle, "CODE").ToString();
                if (decimal.TryParse(e.Value?.ToString(), out decimal yeniFiyat))
                {
                    UpdateFiyatInDatabase(code, yeniFiyat);
                }
            }
        }

        private void GridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Shift)
            {
                int nextRow = gridView1.FocusedRowHandle + 1;
                if (nextRow < gridView1.RowCount)
                {
                    gridView1.FocusedRowHandle = nextRow;
                    gridView1.FocusedColumn = gridView1.Columns["BAYI_SATIS_FIYATI"];
                    e.Handled = true;
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
