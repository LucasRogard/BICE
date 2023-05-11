using BICE.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using BICE.CLIENT;
using System.Windows.Controls;
using BICE.CLIENT;

namespace BICE.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Client client = new Client("https://localhost:7042/", new System.Net.Http.HttpClient());

        private void AjouterMateriel(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var data = line.Split(';');
                        var dto = new BICE.CLIENT.Materiel_DTO()
                        {
                            Numero = data[0],
                            Denomination = data[1],
                            Categorie = data[2],
                            EstStocke = true,
                            NbUtilisation = int.Parse(data[3]),
                            NbMaxUtilisation = String.IsNullOrEmpty(data[4]) ? null : int.Parse(data[4]),
                            DateControle = data[5] == "" ? null : DateTime.Parse(data[5]),
                            DateExpiration = data[6] == "" ? null : DateTime.Parse(data[6])
                        };

                        if (client.MaterielGetByNumero(dto.Numero) == null)
                        {
                            client.MaterielAjouter(dto);
                        }
                        else
                        {
                            client.MaterielModifier(dto);
                        }
                    }
                }

            }

        }

        private void DeleteMateriel(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var client = new Client("https://localhost:7238/", new System.Net.Http.HttpClient());
                        var data = line.Split(';');
                        var dto = client.MaterielGetById(Int32.Parse(data[0]));
                        if (client.MaterielGetById(dto.Id) == null)
                        {
                            throw new Exception("Vous avez essayé de supprimer un matériel inexistant");
                        }
                        else
                        {
                            client.MaterielDelete(dto);
                        }
                    }
                }

            }

        }

        private void AjouterVehicule(object sender, RoutedEventArgs e)
        {
            TextBox immatriculation = FindName("Immatriculation") as TextBox;
            TextBox denomination = FindName("Denomination") as TextBox;
            TextBox numero = FindName("Numero") as TextBox;

            var dto = new BICE.CLIENT.Vehicule_DTO()
            {
                Immatriculation = immatriculation.Text,
                Denomination = denomination.Text,
                Numero = numero.Text,
            };

            if (client.VehiculeGetByNumero(dto.Numero) == null)
            {
                client.VehiculeAjouter(dto);
            }
            else
            {
                client.VehiculeModifier(dto);
            }
        }

        private void UploadButton_DelVehicule_Click(Object sender, RoutedEventArgs e)
        {
            TextBox idTextBox = FindName("supprId") as TextBox;

            var id = idTextBox.Text;
            var dto = client.VehiculeGetById(int.Parse(id));
            if (dto == null)
            {
                throw new Exception("Vous avez essayé de supprimer un véhicule inexistant");
            }
            else
            {
                client.VehiculeDelete(dto);
            }
        }
    }
}
