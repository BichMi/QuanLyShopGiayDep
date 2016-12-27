using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration; 

namespace BaiTapLon_LTCSDL
{
    public partial class fManage : Form
    {
        SqlConnection cnn;
        SqlDataAdapter da, daa, adapter;
        string cnstr;
        DataSet ds, datasetmoi ;
        DataSet dataset;
        SqlCommandBuilder cb;
        DataTable Order;
        DataTable oder2;
        public static string quyen;//admin?staff
        public fManage()
        {
            InitializeComponent();
            cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            cnn = new SqlConnection(cnstr);
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccount f = new fAccount();
            this.Hide();
            f.ShowDialog();            
            this.Show();
        }

        private void addminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void fManage_Load(object sender, EventArgs e)
        {
            //phan quyen
            if (quyen=="admin")
            {
                addminToolStripMenuItem.Enabled = true;
            }
            GetDataToComboboxKhachHang();
            GetDataToComboboxNhanVien();
            GetDataToComboboxSanPham();
            GetDataToComboboxLoaiSP();
            Layhet();
           //GetDataSetCTHD();
           //GetDataSetHoaDon();
        }

        private DataSet GetDataSetSP(string sql)
        {
            try
            {
                daa = new SqlDataAdapter(sql, cnn);
                cb = new SqlCommandBuilder(daa);
                ds = new DataSet();
                daa.Fill(ds);
                return ds;

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                cnn.Close();
            }
        }
        private void GetDataToComboboxKhachHang()
        {
            cnn.Open();
            try
            {
                DataTable dt = new DataTable();
                string sql = @"SELECT * FROM KhachHang";
                da = new SqlDataAdapter(sql, cnn);
                //cb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(dt);
                cbMaKH.DataSource = dt;
                cbMaKH.DisplayMember = "MaKH";
                cbMaKH.ValueMember = "MaKH";
                txtTenKH.DataBindings.Add("Text", dt, "TenKH");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void GetDataToComboboxNhanVien()
        {
            cnn.Open();
            try
            {
                DataTable dttt = new DataTable();
                string sql = @"SELECT * FROM NhanVien";
                da = new SqlDataAdapter(sql, cnn);
               // cb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(dttt);
                cbMaNV.DataSource = dttt;
                cbMaNV.DisplayMember = "MaNV";
                cbMaNV.ValueMember = "MaNV";
                txtTenNV.DataBindings.Add("Text", dttt, "Ten");

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void GetDataToComboboxSanPham()
        {
            try
            {
                DataTable dtt = new DataTable();
                string sql = @"SELECT * FROM SanPham";
                daa = new SqlDataAdapter(sql, cnn);
               // cb = new SqlCommandBuilder(da);
                ds = new DataSet();
                daa.Fill(dtt);
                cbMaSP.DataSource = dtt;
                cbMaSP.DisplayMember = "MaSP";
                cbMaSP.ValueMember = "MaSP";
                txtTenSP.DataBindings.Add("Text", dtt, "TenSP");
                txtDonViTinh.DataBindings.Add("Text", dtt, "DonViTinh");
                txtDonGia.DataBindings.Add("Text", dtt, "DonGia");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void GetDataToComboboxLoaiSP()
        {
            try
            {
                DataTable dtt = new DataTable();
                string sql = @"SELECT * FROM LoaiSanPham";
                daa = new SqlDataAdapter(sql, cnn);
               // cb = new SqlCommandBuilder(da);
                ds = new DataSet();
                daa.Fill(dtt);
                cbMaLoaiSP.DataSource = dtt;
                cbMaLoaiSP.DisplayMember = "MaLoaiSP";
                cbMaLoaiSP.ValueMember = "MaLoaiSP";
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void ThanhTien()
        {
            float dongia = float.Parse(txtDonGia.Text);
            int soluong = int.Parse(txtSoLuong.Text);
            float chietkhau = float.Parse(txtChietKhau.Text);
            float thanhtien = (dongia * soluong);
            txtThanhTien.Text = thanhtien.ToString();          
            float vat = float.Parse(txtVat.Text);
            float tienvat = thanhtien * (vat / 100);
            txtTienVAT.Text = tienvat.ToString();
            float tienchietkhau = (thanhtien + tienvat) * (chietkhau / 100);
            txtTongTienCK.Text = tienchietkhau.ToString();
            float tongtien = thanhtien + tienvat - tienchietkhau;
            txtTongTien.Text = tongtien.ToString();
        }

        //private void GetDataSetHoaDon()
        //{

        //    cnn.Open();
        //    try
        //    {
        //        string sql = @"SELECT * FROM HoaDon ";

        //        da = new SqlDataAdapter(sql, cnn);
        //        cb = new SqlCommandBuilder(da);
        //        ds = new DataSet();

        //        da.Fill(ds);
        //        dgvHDChiTietHD.DataSource = ds.Tables[0];
        //        Order = ds.Tables[0];

        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        cnn.Close();
        //    }
        //}

        //private void GetDataSetCTHD()
        //{

        //    cnn.Open();
        //    try
        //    {
        //        string sql = @"SELECT * FROM ChiTietHoaDon ";

        //        daa = new SqlDataAdapter(sql, cnn);
        //        cb = new SqlCommandBuilder(daa);
        //        dataset = new DataSet();

        //        daa.Fill(dataset);
        //        dgvHDChiTietHD.DataSource = dataset.Tables[0];
        //        oder2 = dataset.Tables[0];

        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        cnn.Close();
        //    }
        //}

        
         
        private void btnTim_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM SanPham WHERE ";
            if (radTheoMa.Checked == true)
                sql += "MaSP = N'"+txtTim.Text+"'";
            else if (radTheoTen.Checked == true)
                sql += "TenSP LIKE '%"+txtTim.Text+"%'";
            dgvTimSP.DataSource = GetDataSetSP(sql).Tables[0];
        }

        //private void ThemBangHD()
        //{

        //    cnn.Open();

        //    try
        //    {

        //        DataRow tt = datasetmoi.Tables[0].NewRow();

        //        tt["MaHD"] = txtMaHD.Text;
        //        tt["MaKH"] = cbMaKH.SelectedValue;
        //        tt["MaNV"] = cbMaNV.SelectedValue;
        //        tt["NgayDatHang"] = Convert.ToDateTime(dtpNgayDatHang.Value).ToShortDateString();
        //        tt["NgayGiaoHang"] = Convert.ToDateTime(dtpNgayGiaoHang.Value).ToShortDateString();
        //        tt["GiaTriHD"] = float.Parse(txtTongTien.Text);
        //        datasetmoi.Tables[0].Rows.Add(tt);


        //    }
        //    catch (SqlException)
        //    {
        //        MessageBox.Show("Không thể thêm thông tin vào hóa đơn vào cơ sở dữ liệu!", "Thông Báo");

        //    }
        //    finally
        //    {
        //        cnn.Close();
        //    }
        //}

        //private void ThemBangCTHD()
        //{
        //    cnn.Open();
        //    try
        //    {

        //        DataRow tt = datasetmoi.Tables[0].NewRow();
        //        tt["MaCTHD"] = txtMaCTHD.Text;
        //        tt["MaHD"] = txtMaHD.Text;
        //        tt["MaSP"] = cbMaSP.SelectedValue;
        //        tt["SoLuong"] = int.Parse(txtSoLuong.Text);
        //        tt["DonGia"] = txtDonGia.Text;
        //        tt["ChietKhau"] = float.Parse(txtChietKhau.Text);
        //        tt["Vat"] = float.Parse(txtVat.Text);
        //        datasetmoi.Tables[0].Rows.Add(tt);

        //    }
        //    catch (SqlException)
        //    {

        //        MessageBox.Show("Không thể thêm dữ liệu vào bảng CTHD", "THông Báo");
        //    }
        //    finally
        //    {
        //        cnn.Close();
        //    }
        //}
        private void Layhet()
        {
            cnn.Open();
            try
            {
                string sql = @"SELECT * FROM HoaDon , ChiTietHoaDon WHERE HoaDon.MaHD = ChiTietHoaDon.MaHD ";

                adapter = new SqlDataAdapter(sql, cnn);
                cb = new SqlCommandBuilder(adapter);
                datasetmoi = new DataSet();
                adapter.Fill(datasetmoi);
                dgvHDChiTietHD.DataSource = datasetmoi.Tables[0];
                Order = datasetmoi.Tables[0];

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //kiem tra du lieu
            if (txtMaHD.Text=="")
            {
                MessageBox.Show("Mã HĐ rỗng. Vui lòng nhập dữ liệu");
                return;
            }

            ThanhTien();
            cnn.Open();

            try
            {
            //    ThemBangHD();
            //    ThemBangCTHD();
                DataRow tt = datasetmoi.Tables[0].NewRow();

                tt["MaHD"] = txtMaHD.Text;
                tt["MaKH"] = cbMaKH.SelectedValue;
                tt["MaNV"] = cbMaNV.SelectedValue;
                tt["NgayDatHang"] = Convert.ToDateTime(dtpNgayDatHang.Value).ToShortDateString();
                tt["NgayGiaoHang"] = Convert.ToDateTime(dtpNgayGiaoHang.Value).ToShortDateString();
                tt["GiaTriHD"] = float.Parse(txtTongTien.Text);

                tt["MaCTHD"] = txtMaCTHD.Text;
                tt["MaHD"] = txtMaHD.Text;
                tt["MaSP"] = cbMaSP.SelectedValue;
                tt["SoLuong"] = int.Parse(txtSoLuong.Text);
                tt["DonGia"] = txtDonGia.Text;
                tt["ChietKhau"] = float.Parse(txtChietKhau.Text);
                tt["Vat"] = float.Parse(txtVat.Text);
                datasetmoi.Tables[0].Rows.Add(tt);


            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể thêm thông tin vào hóa đơn vào cơ sở dữ liệu!", "Thông Báo");

            }
            finally
            {
                cnn.Close();
            }
        }
        private void LuuHD()
        {
            cnn.Open();
            try
            {
                string sql = @"SELECT * FROM HoaDon ";

                da = new SqlDataAdapter(sql, cnn);

                ds = new DataSet();
                da.Fill(ds);

                DataRow tt = ds.Tables[0].NewRow();

                tt["MaHD"] = txtMaHD.Text;
                tt["MaKH"] = cbMaKH.SelectedValue;
                tt["MaNV"] = cbMaNV.SelectedValue;
                tt["NgayDatHang"] = Convert.ToDateTime(dtpNgayDatHang.Value).ToShortDateString();
                tt["NgayGiaoHang"] = Convert.ToDateTime(dtpNgayGiaoHang.Value).ToShortDateString();
                tt["GiaTriHD"] = float.Parse(txtTongTien.Text);
                ds.Tables[0].Rows.Add(tt);
                cb = new SqlCommandBuilder(da);
                da.Update(ds);
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Không thể lưu hóa đơn");
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
            finally
            {
                cnn.Close();
            }

        }
        private void LuuCTHD()
        {
            cnn.Open();
            try
            {
                string sql = @"SELECT * FROM ChiTietHoaDon ";

                daa = new SqlDataAdapter(sql, cnn);

                dataset = new DataSet();
                daa.Fill(dataset);

                DataRow tt = dataset.Tables[0].NewRow();
                tt["MaCTHD"] = txtMaCTHD.Text;
                tt["MaHD"] = txtMaHD.Text;
                tt["MaSP"] = cbMaSP.SelectedValue;
                tt["SoLuong"] = int.Parse(txtSoLuong.Text);
                tt["DonGia"] = txtDonGia.Text;
                tt["ChietKhau"] = float.Parse(txtChietKhau.Text);
                tt["Vat"] = float.Parse(txtVat.Text);
                dataset.Tables[0].Rows.Add(tt);
                cb = new SqlCommandBuilder(daa);
                daa.Update(dataset);
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
           
            LuuHD();
            LuuCTHD();  
            
        }

      
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ds.RejectChanges();
        }
      
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtTienKhachTra.Text == "")
                {
                    float tongtin = float.Parse(txtTongTien.Text);
                    txtTienKhachTra.Text = tongtin.ToString();
                }
                float tienkhachtra = float.Parse(txtTienKhachTra.Text);
                float tongtien2 = float.Parse( txtTongTien.Text);
                float tientrakhach = tienkhachtra - tongtien2;
                txtTienTraLai.Text = tientrakhach.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        public object txt { get; set; }

        private void cbMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenKH.Text = cbMaKH.DisplayMember;
        }

        private void fManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

       
    }
}
