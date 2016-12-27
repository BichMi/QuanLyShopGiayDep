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
    public partial class fAdmin : Form
    {
        SqlConnection cnn;
        SqlDataAdapter da;
        string cnstr;
        DataSet ds;
        SqlCommandBuilder cb;
        DataTable Order;
        public fAdmin()
        {
            InitializeComponent();
            cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            cnn = new SqlConnection(cnstr);
        }
       
        /// <summary>
        ///                       KHÁCH HÀNG
        /// </summary>

        private void GetDataSetKH()
        {
            cnn.Open();
            try
            {
                string sql = @"SELECT * FROM KhachHang";
                da = new SqlDataAdapter(sql, cnn);
                cb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds);
                dgvkhachhang.DataSource = ds.Tables[0];

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

        private void btnthemkh_Click(object sender, EventArgs e)
        {
            cnn.Open();
            try
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["MaKH"] = txtMaKH.Text;
                dr["HoKH"] = txtHoKH.Text;
                dr["TenKH"] = txtTenKH.Text;
                dr["QueQuan"] = txtQueQuan.Text;
                dr["DiaChi"] = txtDiaChi.Text;
                dr["Email"] = txtEmail.Text;
                dr["DienThoai"] = txtDienThoai.Text;
                ds.Tables[0].Rows.Add(dr);
                /////////////

            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể thêm khách hàng vào cơ sở dữ liệu!", "Thông Báo");

            }
            finally
            {
                cnn.Close();
            }
        }

        private void btnxoakh_Click(object sender, EventArgs e)
        {
            cnn.Open();
            try
            {
                if (dgvkhachhang.Rows.Count > 0)
                {
                    int index = dgvkhachhang.CurrentRow.Index;
                    DataGridViewRow cr = dgvkhachhang.Rows[index];
                    dgvkhachhang.Rows.Remove(cr);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể xóa dữ liệu!", "Thông Báo");

            }
            finally
            {
                cnn.Close();
            }
        }

        private void btnsuakh_Click(object sender, EventArgs e)
        {
            cnn.Open();
            try
            {
                if(dgvkhachhang.Rows.Count > 0)
                {
                    Order = ds.Tables[0];
                    int index = dgvkhachhang.CurrentRow.Index;
                    DataRow dr = Order.Rows[index];// du lieu dong  =  gia tri dong hien tai
                    dr.BeginEdit();// bat dau sua
                    
                    dr["HoKH"] = txtHoKH.Text;
                    dr["TenKH"] = txtTenKH.Text;
                    dr["QueQuan"] = txtQueQuan.Text;
                    dr["DiaChi"] = txtDiaChi.Text;
                    dr["Email"] = txtEmail.Text;
                    dr["DienThoai"] = txtDienThoai.Text;
                    dr.EndEdit();// ket thuc sua
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể sửa dữ liệu!", "Thông Báo");

            }
            finally
            {
                cnn.Close();
            }
        }

        private void btnluukh_Click(object sender, EventArgs e)
        {
            da.Update(ds); 
        }

        private void btnhuykh_Click(object sender, EventArgs e)
        {
            ds.Tables[0].RejectChanges();
        }

        private void dgvkhachhang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvkhachhang.CurrentRow != null)
            {
                txtMaKH.Text = dgvkhachhang.CurrentRow.Cells["MaKH"].Value.ToString();
                txtHoKH.Text = dgvkhachhang.CurrentRow.Cells["HoKH"].Value.ToString();
                txtTenKH.Text = dgvkhachhang.CurrentRow.Cells["TenKH"].Value.ToString();
                txtQueQuan.Text = dgvkhachhang.CurrentRow.Cells["QueQuan"].Value.ToString();
                txtDiaChi.Text = dgvkhachhang.CurrentRow.Cells["DiaChi"].Value.ToString();
                txtEmail.Text = dgvkhachhang.CurrentRow.Cells["Email"].Value.ToString();
                txtDienThoai.Text = dgvkhachhang.CurrentRow.Cells["DienThoai"].Value.ToString();

            }
        }

        //*************************

        private void fAdmin_Load(object sender, EventArgs e)
        { 
            
            GetDataSetKH();
        }

        //********************************
        /// <summary>
        ///                         SẢN PHẨM
        /// </summary>

        private void GetDataSetSP()
        {
            try
            {
                string sql = @"SELECT * FROM SanPham";
                da = new SqlDataAdapter(sql, cnn);
                cb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds);
                dgvsanpham.DataSource = ds.Tables[0];
               
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
                DataTable dt=new DataTable();
                string sql = @"SELECT * FROM LoaiSanPham";
                da = new SqlDataAdapter(sql, cnn);
                cb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(dt);
                cmbLoaiSP.DataSource = dt;
                cmbLoaiSP.DisplayMember = "TenLoaiSP";
                cmbLoaiSP.ValueMember= "MaLoaiSP";                
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

        private void btnthemsp_Click(object sender, EventArgs e)
        {
            cnn.Open();
            try
            { 
                DataRow tt = ds.Tables[0].NewRow();
                tt["MaSP"] = txtmasp.Text;
                tt["TenSP"] = txttensp.Text;
                tt["MaLoaiSP"] = cmbLoaiSP.SelectedValue;
                tt["DonViTinh"] = txtdonvitinh.Text;
                tt["DonGia"] = txtdongia.Text;
                tt["SoLuong"] = txtsoluong.Text;
                ds.Tables[0].Rows.Add(tt);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể thêm Sản Phẩm vào cơ sở dữ liệu!", "Thông Báo");

            }
            finally
            {
                cnn.Close();
            }
        }

        private void btnxoasp_Click(object sender, EventArgs e)
        {
            cnn.Open();
            try
            {
                if (dgvsanpham.Rows.Count > 0)
                {
                    int index = dgvsanpham.CurrentRow.Index;
                    DataGridViewRow cr = dgvsanpham.Rows[index];
                    dgvsanpham.Rows.Remove(cr);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể xóa dữ liệu!", "Thông Báo");

            }
            finally
            {
                cnn.Close();
            }
        }

        private void btnsuasp_Click(object sender, EventArgs e)
        {
            cnn.Open();
            try
            {
                if (dgvsanpham.Rows.Count > 0)
                {
                    Order = ds.Tables[0];
                    int index = dgvsanpham.CurrentRow.Index;
                    DataRow dr = Order.Rows[index];// du lieu dong  =  gia tri dong hien tai
                    dr.BeginEdit();// bat dau sua
                    dr["TenSP"] = txttensp.Text;
                    dr["MaLoaiSP"] = cmbLoaiSP.SelectedValue;
                    dr["DonViTinh"] = txtdonvitinh.Text;
                    dr["DonGia"] = txtdongia.Text;
                    dr["SoLuong"] = txtsoluong.Text;
                    dr.EndEdit();// ket thuc sua
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể sửa dữ liệu!", "Thông Báo");

            }
            finally
            {
                cnn.Close();
            }
        }

        private void btnluusp_Click(object sender, EventArgs e)
        {
            da.Update(ds);
        }

        
        private void btnhuysp_Click(object sender, EventArgs e)
        {
            ds.Tables[0].RejectChanges();
        }

        private void dgvsanpham_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvsanpham.CurrentRow != null)
            {
                txtmasp.Text = dgvsanpham.CurrentRow.Cells["MaSP"].Value.ToString();
                txttensp.Text = dgvsanpham.CurrentRow.Cells["TenSP"].Value.ToString();
                cmbLoaiSP.SelectedValue= dgvsanpham.CurrentRow.Cells["MaLoaiSP"].Value.ToString();
                txtdonvitinh.Text = dgvsanpham.CurrentRow.Cells["DonViTinh"].Value.ToString();
                txtdongia.Text = dgvsanpham.CurrentRow.Cells["DonGia"].Value.ToString();
                txtsoluong.Text = dgvsanpham.CurrentRow.Cells["SoLuong"].Value.ToString();
            }
        }


       //************ CHUYỂN TABCONTROL

        private void tabAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabAdmin.SelectedTab.Name == "tabCustommer")
            {                            
                GetDataSetKH();
            }
            else if (tabAdmin.SelectedTab.Name == "tabProduct")
            {
                GetDataToComboboxLoaiSP();
                GetDataSetSP();
            }
            else if(tabAdmin.SelectedTab.Name == "tabTypeProduct")
            {
                GetDataSetLoaiSP();
            }
            else if (tabAdmin.SelectedTab.Name == "tabAccount")
            {
                GetDataSetTaiKhoan();
                
            }
            else if (tabAdmin.SelectedTab.Name == "tabHoaDon" )
            {
                GetDataSetHoaDon();
            }
        }

       /// <summary>
       ///                        LOẠI SẢN PHẨM  
       /// </summary>
     
        private void btnthemlsp_Click(object sender, EventArgs e)
        {
            cnn.Open();
            try
            {
                DataRow tt = ds.Tables[0].NewRow();
                tt["MaLoaiSP"] = txtMaloaiSP.Text;
                tt["TenLoaiSP"] = txtTenLoaisp.Text;
                ds.Tables[0].Rows.Add(tt);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể thêm Sản Phẩm vào cơ sở dữ liệu!", "Thông Báo");

            }
            finally
            {
                cnn.Close();
            }
        }

        private void btnxoalsp_Click(object sender, EventArgs e)
        {
            cnn.Open();
            try
            {
                if (dgvsanpham.Rows.Count > 0)
                {
                    int index = dgvLoaisp.CurrentRow.Index;
                    DataGridViewRow cr = dgvLoaisp.Rows[index];
                    dgvLoaisp.Rows.Remove(cr);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể xóa dữ liệu!", "Thông Báo");

            }
            finally
            {
                cnn.Close();
            }
        }

        private void btnsualsp_Click(object sender, EventArgs e)
        {
            cnn.Open();
            try
            {
                if (dgvLoaisp.Rows.Count > 0)
                {
                    Order = ds.Tables[0];
                    int index = dgvLoaisp.CurrentRow.Index;
                    DataRow dr = Order.Rows[index];// du lieu dong  =  gia tri dong hien tai
                    dr.BeginEdit();// bat dau sua
                    dr["TenLoaiSP"] = txtTenLoaisp.Text;
                    dr.EndEdit();// ket thuc sua
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể sửa dữ liệu!", "Thông Báo");

            }
            finally
            {
                cnn.Close();
            }
        }

        private void btnluulsp_Click(object sender, EventArgs e)
        {
            da.Update(ds);
        }

        private void btnhuylsp_Click(object sender, EventArgs e)
        {
            ds.Tables[0].RejectChanges();
        }

        private void GetDataSetLoaiSP()
        {
            cnn.Open();
            try
            {
                string sql = @"SELECT * FROM LoaiSanPham";
                da = new SqlDataAdapter(sql, cnn);
                cb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds);
                dgvLoaisp.DataSource = ds.Tables[0];

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

        private void dgvLoaisp_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLoaisp.CurrentRow != null)
            {
                txtMaloaiSP.Text = dgvLoaisp.CurrentRow.Cells["MaLoaiSP"].Value.ToString();
                txtTenLoaisp.Text = dgvLoaisp.CurrentRow.Cells["TenLoaiSP"].Value.ToString();
            }
        }

        /// <summary>
        ///                            TÀI KHOẢN
        /// </summary>


        private void GetDataSetTaiKhoan()
        {
            cnn.Open();
            try
            {
                string sql = @"SELECT * FROM TaiKhoan";
                da = new SqlDataAdapter(sql, cnn);
                cb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds);
                dgvtaikhoan.DataSource = ds.Tables[0];
                
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
             
        private void btnthemtk_Click(object sender, EventArgs e)
        {
            cnn.Open();
            try
            {
                DataRow tt = ds.Tables[0].NewRow();
                tt["TenDangNhap"] = txtusername.Text;
                tt["TenHienThi"] = txtdisplayName.Text;
                tt["ChucDanh"] = txtChucDanh.Text;
                tt["MatKhau"] = txtMatKhau.Text;
                
                ds.Tables[0].Rows.Add(tt);
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể thêm Tài Khoản vào cơ sở dữ liệu!", "Thông Báo");
            }
            finally
            {
                cnn.Close();
            }
        }

        private void btnxoatk_Click(object sender, EventArgs e)
        {
            cnn.Open();
            try
            {
                if (dgvtaikhoan.Rows.Count > 0)
                {
                    int index = dgvtaikhoan.CurrentRow.Index;
                    DataGridViewRow dr = dgvtaikhoan.Rows[index];
                    dgvtaikhoan.Rows.Remove(dr);

                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể xóa dữ liệu!", "Thông Báo");

            }
            finally
            {
                cnn.Close();
            }
            
        }

        private void btnsuatk_Click(object sender, EventArgs e)
        {
            cnn.Open();
            try
            {
                if (dgvtaikhoan.Rows.Count > 0)
                {
                    Order = ds.Tables[0];
                    int index = dgvtaikhoan.CurrentRow.Index;
                    DataRow dr = Order.Rows[index];// du lieu dong  =  gia tri dong hien tai
                    dr.BeginEdit();// bat dau sua
                    dr["TenDangNhap"] = txtusername.Text;
                    dr["TenHienThi"] = txtdisplayName.Text;
                    dr["MatKhau"] = txtMatKhau.Text;
                    dr["ChucDanh"] = txtChucDanh.Text;
                    dr.EndEdit();// ket thuc sua
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Không thể sửa dữ liệu!", "Thông Báo");

            }
            finally
            {
                cnn.Close();
            }
        }

        private void btnluutk_Click(object sender, EventArgs e)
        {
            da.Update(ds);
        }

        private void btnhuytk_Click(object sender, EventArgs e)
        {
            ds.Tables[0].RejectChanges();
        }

        private void dgvtaikhoan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvtaikhoan.CurrentRow != null)
            {
                txtusername.Text = dgvtaikhoan.CurrentRow.Cells["TenDangNhap"].Value.ToString();
                txtdisplayName.Text = dgvtaikhoan.CurrentRow.Cells["TenHienthi"].Value.ToString();
                txtChucDanh.Text = dgvtaikhoan.CurrentRow.Cells["ChucDanh"].Value.ToString();               
                txtMatKhau.Text = dgvtaikhoan.CurrentRow.Cells["MatKhau"].Value.ToString();
            }
        }

        private void btnResetPass_Click(object sender, EventArgs e)
        {
            fAccount f = new fAccount();
            this.Hide();
            f.ShowDialog();
        }
        /// <summary>
        ///                                  Hóa Đơn
        /// </summary>
       private void GetDataSetHoaDon()
       {

           cnn.Open();
           try
           {
               string sql = @"SELECT * FROM HoaDon";
               
               da = new SqlDataAdapter(sql, cnn);
               cb = new SqlCommandBuilder(da);
               ds = new DataSet();
               da.Fill(ds);
               dgvChiTietHoaDon.DataSource = ds.Tables[0];
               Order = ds.Tables[0];
               cbMaHD.DataSource = Order;
               cbMaHD.DisplayMember = "MaHD";
               cbMaHD.ValueMember = "MaHD";
               txtHDKH.DataBindings.Add("Text", Order, "MaKH");
               txtHDNV.DataBindings.Add("Text", Order, "MaNV");
               dtpNgayDatHang.DataBindings.Add("Text", Order, "NgayDatHang");
               dtpNgayGiaoHang.DataBindings.Add("Text", Order, "NgayGiaoHang");
               
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

       private void cbMaHD_SelectedIndexChanged(object sender, EventArgs e)
       {
           string sql = "SELECT * FROM ChiTietHoaDon WHERE MaHD = '" + cbMaHD.Text + "'";
           da = new SqlDataAdapter(sql, cnn);
           cb = new SqlCommandBuilder(da);
           ds = new DataSet();
           da.Fill(ds);
           dgvChiTietHoaDon.DataSource = ds.Tables[0];
       }

       private void dgvtaikhoan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
       {
            if (dgvtaikhoan.Columns[e.ColumnIndex].Index==2 && e.Value != null)
            {
                dgvtaikhoan.Rows[e.RowIndex].Tag = e.Value;
                e.Value = new String('*', e.Value.ToString().Length);
            }
       }//  Tài Khoản
        /// <summary>
        ///                          THỐNG KÊ
        /// </summary>

       private void btnThongKe_Click(object sender, EventArgs e)
       {
           cnn.Open();
           try
           {
               SqlCommand cmd = new SqlCommand("ThongKeHoaDon", cnn);
               cmd.CommandType = CommandType.StoredProcedure;

               cmd.Parameters.Add("@NgayDat", SqlDbType.DateTime, 11);
               cmd.Parameters["@NgayDat"].Value = Convert.ToDateTime(dtpNgayDatTK.Value).ToShortDateString();
               cmd.Parameters.Add("@NgayGiao", SqlDbType.DateTime, 11);
               cmd.Parameters["@NgayGiao"].Value = Convert.ToDateTime(dtpNgayGiaoTK.Value).ToShortDateString();

               using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
               {
                   DataTable dt = new DataTable();
                   adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                   adapter.Fill(dt);
                   dgvThongKe.DataSource = dt;
               }

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

       private void fAdmin_FormClosing(object sender, FormClosingEventArgs e)
       {
           if (MessageBox.Show("Bạn muốn thoát?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
           {
               e.Cancel = true;
           }
       }
     
        
    }
}
