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
    public partial class Page1 : ContentPage
    {
        public IList<ruangan> ruangans { get; set; }
        public Page1()
        {
            InitializeComponent();
            data_list_ruangan();
        }

        public class ruangan
        {
            public string nama_ruang { get; set; } public string kapasitas { get; set; }
        }




        private void  data_list_ruangan()
        {
            var koneksi = new MySqlConnection(Properties.Resources.db_con);
            koneksi.Open();
            var cmd = new MySqlCommand("select * from room", koneksi);
            var rd = cmd.ExecuteReader();

            ruangans = new List<ruangan>();
            while (rd.Read())
            {
                ruangans.Add(new ruangan
                {
                    kapasitas = rd.GetString("kapasitas").ToString(),
                    nama_ruang = rd.GetString("nama_ruang").ToString()

                }
                    );
            }
            rd.Close();
            listview_ruang.ItemsSource = ruangans;
        }

        private void view_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            ImageButton X = (ImageButton)sender;
            string ruang_select = X.CommandParameter.ToString();

            Application.Current.Properties["page1_ruang"] = ruang_select;
            Navigation.PushAsync(new Edit_Ruang());
        }

        private void edit_Clicked(object sender, EventArgs e)
        {

        }

        private async void delete_Clicked(object sender, EventArgs e)
        {
            ImageButton y = (ImageButton)sender;
            string ruang_select = y.CommandParameter.ToString();

            var ans = await DisplayAlert("Konfirmasi hapus?", "Yakin untuk menghapus barang?", "Yes", "No");
            if (ans == true)
            {
                var koneksi = new MySqlConnection(Properties.Resources.db_con);
                koneksi.Open();
                var cmd = new MySqlCommand("delete from room where nama_ruang = '" + ruang_select + "'", koneksi);
                var rd = cmd.ExecuteReader();

                data_list_ruangan();
                await DisplayAlert("Informasi", "Ruangan telah terhapus", "OK");
            }
            else
            {
                return;
            }
        }
    }
}