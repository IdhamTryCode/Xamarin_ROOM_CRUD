using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ruang
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Edit_Ruang : ContentPage
    {
        string ruang = Convert.ToString(Application.Current.Properties["page1_ruang"]);
        public Edit_Ruang()
        {
            InitializeComponent();
            t_ruang.Text = ruang;
            Task.Delay(2000);
            data_ruang();
        }
        private void data_ruang()
        {
            var koneksi = new MySqlConnection(Properties.Resources.db_con);
            koneksi.Open();
            var cmd = new MySqlCommand("select * from room where nama_ruang='" + t_ruang.Text + "'", koneksi);
            var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                t_kapasitas.Text = rd.GetString("kapasitas").ToString();
                t_ruang.Text = rd.GetString("nama_ruang").ToString();

            }
        }

        private void b_update_Clicked(object sender, EventArgs e)
        {
            if (t_ruang.Text == null)
            {
                t_ruang.Focus();
            }
            else if (t_kapasitas.Text == null)
            {
                t_kapasitas.Focus();
            }
            else
            {
                var koneksi = new MySqlConnection(Properties.Resources.db_con);
                koneksi.Open();
                var cmd = new MySqlCommand("update room set nama_ruang='" + t_ruang.Text + "', kapasitas = '"+ t_kapasitas.Text + "'where nama_ruang='" + t_ruang.Text + "'", koneksi);
                var rd = cmd.ExecuteReader();

                DisplayAlert("Information", "Data telah diupdate", "OK");
                Navigation.PushAsync(new Page1());
            }
        }
    }
}