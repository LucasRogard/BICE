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
using Microsoft.SharePoint.News.DataModel;

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
                        var ligne = reader.ReadLine();
                        var colonne = ligne.Split(';');
                        var dto = new BICE.CLIENT.Materiel_DTO()
                        {
                            Numero = colonne[0],
                            Denomination = colonne[1],
                            Categorie = colonne[2],
                            EstStocke = true,
                            NbUtilisation = String.IsNullOrEmpty(colonne[3]) ? null : int.Parse(colonne[3]),
                            NbMaxUtilisation = String.IsNullOrEmpty(colonne[4]) ? null : int.Parse(colonne[4]),
                            DateControle = colonne[5] == "" ? null : DateTime.Parse(colonne[5]),
                            DateExpiration = colonne[6] == "" ? null : DateTime.Parse(colonne[6])
                        };

                        if (client.MaterielGetByNumero(dto.Numero) == null)
                        {
                            client.MaterielAjouter(dto);
                        }else
                        {
                            client.MaterielModifier(dto);
                        }
                    }
                }
            }
        }

        private void AjouterVehicule(object sender, RoutedEventArgs e)
        {
            TextBox TextBoxDenomination = FindName("Denomination") as TextBox;
            TextBox TextBoxImmatriculation = FindName("Immatriculation") as TextBox;
            TextBox TextBoxNumero = FindName("Numero") as TextBox;

            var dto = new BICE.CLIENT.Vehicule_DTO()
            {
                Immatriculation = TextBoxImmatriculation.Text,
                Denomination = TextBoxDenomination.Text,
                Numero = TextBoxNumero.Text,
            };

            if (client.VehiculeGetByNumero(dto.Numero) == null)
            {
                client.VehiculeAjouter(dto);
            }else
            {
                client.VehiculeModifier(dto);
            }
        }

        private void DeleteVehicule(Object sender, RoutedEventArgs e)
        {
            TextBox VehiculeId = FindName("IdDelete") as TextBox;
            var id = VehiculeId.Text;
            var dto = client.VehiculeGetById(int.Parse(id));
            dto.Actif = false;
            if (dto == null)
            {
                throw new Exception("Aucun véhicule ne porte cet id");
            }else
            {
                client.VehiculeModifier(dto);
            }
        }

        private void ModifierVehiculeIdMateriel(object sender, RoutedEventArgs e)
        {
            TextBox VehiculeId = FindName("VehiculeId") as TextBox;
            var id = int.Parse(VehiculeId.Text);
            var vehicule_dto = client.VehiculeGetById(id);
            if (vehicule_dto == null) { 
                throw new Exception("Aucun véhicule ne porte cet id"); 
            }
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    while (!reader.EndOfStream)
                    {
                        var ligne = reader.ReadLine();
                        var colonne = ligne.Split(';');
                        var materiel_dto = client.MaterielGetByNumero(colonne[0]);

                        if (materiel_dto != null)
                        {
                            materiel_dto.VehiculeId = id;
                            client.MaterielModifier(materiel_dto);
                        }
                    }
                }

            }

        }
        private void RetourIntervention(object sender, RoutedEventArgs e)
        {
            TextBox VehiculeId = FindName("VehiculeId2") as TextBox;
            var id = int.Parse(VehiculeId.Text);
            var vehicule_dto = client.VehiculeGetById(id);
            if (vehicule_dto == null)
            {
                throw new Exception("Aucun véhicule ne porte cet id");
            }
            Microsoft.Win32.OpenFileDialog MaterielUtilise = new Microsoft.Win32.OpenFileDialog();
            Microsoft.Win32.OpenFileDialog MaterielNonUtilise = new Microsoft.Win32.OpenFileDialog();
            bool? result = MaterielUtilise.ShowDialog();
            if (result == true)
            {
                using (StreamReader reader = new StreamReader(MaterielUtilise.FileName))
                {
                    while (!reader.EndOfStream)
                    {
                        var ligne = reader.ReadLine();
                        var colonne = ligne.Split(';');
                        var materiel_dto = client.MaterielGetByNumero(colonne[0]);

                        if (materiel_dto != null)
                        {
                            materiel_dto.NbUtilisation += 1;
                            if (materiel_dto.NbUtilisation >= materiel_dto.NbMaxUtilisation)
                            {
                                materiel_dto.EstStocke= false;
                            }
                            client.MaterielModifier(materiel_dto);
                        }
                    }
                }

            }
            result = MaterielNonUtilise.ShowDialog();
            if (result == true)
            {
                using (StreamReader reader = new StreamReader(MaterielUtilise.FileName))
                {
                    while (!reader.EndOfStream)
                    {
                        var ligne = reader.ReadLine();
                        var colonne = ligne.Split(';');
                        var materiel_dto = client.MaterielGetByNumero(colonne[0]);
                        if (materiel_dto != null)
                        {
                            materiel_dto.VehiculeId = id;
                            client.MaterielModifier(materiel_dto);
                        }
                    }
                }

            }
            TextBox Denomination = FindName("Denomination") as TextBox;
            TextBox Description = FindName("Description") as TextBox;
            var intervention_dto = new BICE.CLIENT.Intervention_DTO()
            {
                Date= DateTime.Now,
                Denomination = Denomination.Text,
                Description = Description.Text
                
            };
            client.InterventionAjouter(intervention_dto);
        }

        private void ExportMateriel(object sender, RoutedEventArgs e)
        {
            var streamWriter = new StreamWriter("C:\\Users\\yughi\\Documents\\ExportMateriel\\Export_Materiel.csv");

            var listMateriel = (List<BICE.CLIENT.Materiel_DTO>)client.MaterielGetAll();

            string NouvelleLigne = Environment.NewLine;

            foreach (var materiel in listMateriel)
            {
                if (materiel.EstStocke) 
                {
                    var row = string.Join(";", new List<string>()
                    {
                        materiel.Id.ToString(),
                        materiel.Denomination.ToString(),
                        materiel.Categorie.ToString(),
                        materiel.Numero.ToString(),
                        materiel.EstStocke.ToString(),
                        materiel.VehiculeId?.ToString()?? "",
                        materiel.NbUtilisation?.ToString()?? "",
                        materiel.NbMaxUtilisation?.ToString()?? "",
                        materiel.DateExpiration?.ToString()?? "",
                        materiel.DateControle?.ToString()?? ""
                    });
                    streamWriter.Write(row + NouvelleLigne);
                }
            };
            streamWriter.Close();
        }

        private void ExportMaterielInutilisable(object sender, RoutedEventArgs e)
        {
            var streamWriter = new StreamWriter("C:\\Users\\yughi\\Documents\\ExportMateriel\\Export_Materiel_Inutilisable.csv");

            var listMateriel = (List<BICE.CLIENT.Materiel_DTO>)client.MaterielGetAll();

            string NouvelleLigne = Environment.NewLine;

            foreach (var materiel in listMateriel)
            {
                if (materiel.EstStocke && materiel.NbUtilisation >= materiel.NbMaxUtilisation)
                {
                    var row = string.Join(";", new List<string>()
                    {
                        materiel.Id.ToString(),
                        materiel.Denomination.ToString(),
                        materiel.Categorie.ToString(),
                        materiel.Numero.ToString(),
                        materiel.EstStocke.ToString(),
                        materiel.VehiculeId?.ToString()?? "",
                        materiel.NbUtilisation?.ToString()?? "",
                        materiel.NbMaxUtilisation?.ToString()?? "",
                        materiel.DateExpiration?.ToString()?? "",
                        materiel.DateControle?.ToString()?? ""
                    });
                    streamWriter.Write(row + NouvelleLigne);
                }
            };
            streamWriter.Close();
        }

        private void ExportMaterielControle(object sender, RoutedEventArgs e)
        {
            var streamWriter = new StreamWriter("C:\\Users\\yughi\\Documents\\ExportMateriel\\Export_Materiel_Controle.csv");

            var listMateriel = (List<BICE.CLIENT.Materiel_DTO>)client.MaterielGetAll();

            string NouvelleLigne = Environment.NewLine;

            foreach (var materiel in listMateriel)
            {
                var date = DateTime.Now;
                var dateNow = date.ToString("dd/mm/yyyy");
                var dateControle = materiel.DateControle.ToString();
                if (dateControle == dateNow)
                {
                    var row = string.Join(";", new List<string>()
                    {
                        materiel.Id.ToString(),
                        materiel.Denomination.ToString(),
                        materiel.Categorie.ToString(),
                        materiel.Numero.ToString(),
                        materiel.EstStocke.ToString(),
                        materiel.VehiculeId?.ToString()?? "",
                        materiel.NbUtilisation?.ToString()?? "",
                        materiel.NbMaxUtilisation?.ToString()?? "",
                        materiel.DateExpiration?.ToString()?? "",
                        materiel.DateControle?.ToString()?? ""
                    });
                    streamWriter.Write(row + NouvelleLigne);
                }
            };
            streamWriter.Close();
        }

    }
}
