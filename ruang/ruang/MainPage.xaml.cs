using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Xamarin.Forms;

namespace ruang
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

        }

        private void B_Save_Clicked(object sender, EventArgs e)
        {
            MySqlConnection koneksi = new MySqlConnection(Properties.Resources.db_con);
            koneksi.Open();

            MySqlCommand cmd = new MySqlCommand("insert into room(nama_ruang, kapasitas) values('" + t_name.Text + "', '" + t_kapasitas.Text + "')", koneksi);
            var rd = cmd.ExecuteReaderAsync();


            DisplayAlert("Info", "Data telah tersimpan", "OK");
            Navigation.PushAsync(new Page1());
        }

        private void edit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page1());
        }
    }
}
