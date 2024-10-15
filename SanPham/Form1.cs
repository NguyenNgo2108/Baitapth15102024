using System.Data;
using System.Data.SqlClient;

namespace SanPham
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tbMaSP_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = laySanPham().Tables[0];
        }

        DataSet laySanPham()
        {
            DataSet data = new DataSet();

            String query = "SELECT * FROM SanPham";

            using (SqlConnection con = new SqlConnection(KetNoi.chuoiKN))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(data);
            }
            return data;
        }

        private void tbTenSP_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbDonGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbHinhAnh_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbMoTaNgan_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbMoTaChiTiet_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các TextBox và ComboBox
                string maSP = tbMaSP.Text; // Lấy mã sản phẩm từ TextBox
                string tenSP = tbTenSP.Text;
                decimal donGia;
                if (!decimal.TryParse(tbDonGia.Text, out donGia))
                {
                    MessageBox.Show("Đơn giá không hợp lệ.");
                    return;
                }
                string hinhAnh = tbHinhAnh.Text;
                string moTaNgan = tbMoTaNgan.Text;
                string moTaChiTiet = tbMoTaChiTiet.Text;
                string loaiSP = cbLoaiSP.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(loaiSP))
                {
                    MessageBox.Show("Vui lòng chọn loại sản phẩm.");
                    return;
                }

                // Thêm sản phẩm vào cơ sở dữ liệu
                string query = "INSERT INTO SanPham (MaSP, TenSP, DonGia, HinhAnh, MoTaNgan, MoTaChiTiet, LoaiSP) VALUES (@MaSP, @TenSP, @DonGia, @HinhAnh, @MoTaNgan, @MoTaChiTiet, @LoaiSP)";

                using (SqlConnection con = new SqlConnection(KetNoi.chuoiKN))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", maSP); // Thêm tham số cho mã sản phẩm
                        cmd.Parameters.AddWithValue("@TenSP", tenSP);
                        cmd.Parameters.AddWithValue("@DonGia", donGia);
                        cmd.Parameters.AddWithValue("@HinhAnh", hinhAnh);
                        cmd.Parameters.AddWithValue("@MoTaNgan", moTaNgan);
                        cmd.Parameters.AddWithValue("@MoTaChiTiet", moTaChiTiet);
                        cmd.Parameters.AddWithValue("@LoaiSP", loaiSP);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm sản phẩm thành công!");
                    }
                }

                // Cập nhật lại DataGridView
                dataGridView1.DataSource = laySanPham().Tables[0];

                // Xóa các TextBox và ComboBox
                tbMaSP.Clear();
                tbTenSP.Clear();
                tbDonGia.Clear();
                tbHinhAnh.Clear();
                tbMoTaNgan.Clear();
                tbMoTaChiTiet.Clear();
                cbLoaiSP.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
