using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarWarsExplorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnGetPlanet_Click(object sender, EventArgs e)
        {
            try
            {
                int planetId = int.Parse(txtPlanetId.Text);
                string url = $"https://swapi.py4e.com/api/planets/{planetId}/";

                // Fetch and deserialize the planet data
                var planet = await ApiHelper.GetDataAsync<Planet>(url);

                // Display the planet data
                lblName.Text = $"Name: {planet.name}";
                lblClimate.Text = $"Climate: {planet.climate}";
                lblTerrain.Text = $"Terrain: {planet.terrain}";
                lblGravity.Text = $"Gravity: {planet.gravity}";
                lblPopulation.Text = $"Population: {planet.population}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void btnGetPerson_Click(object sender, EventArgs e)
        {
            try
            {
                int personId = int.Parse(txtPersonId.Text);
                string url = $"https://swapi.py4e.com/api/people/{personId}/";

                // Fetch and deserialize the person data
                var person = await ApiHelper.GetDataAsync<Person>(url);

                // Display the person data
                lblPersonName.Text = $"Name: {person.name}";
                lblHeight.Text = $"Height: {person.height}";
                lblMass.Text = $"Mass: {person.mass}";
                lblBirthYear.Text = $"Birth Year: {person.birth_year}";

                // Clear the starship list and add new ones
                lstStarships.Items.Clear();
                foreach (var starshipUrl in person.starships)
                {
                    var starship = await ApiHelper.GetDataAsync<Starship>(starshipUrl);
                    lstStarships.Items.Add(starship.name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void btnGetSpecies_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "https://swapi.py4e.com/api/species/";
                List<Species> allSpecies = new List<Species>();

                // Fetch all species (handle pagination if needed)
                while (!string.IsNullOrEmpty(url))
                {
                    var speciesResult = await ApiHelper.GetDataAsync<SpeciesResult>(url);
                    allSpecies.AddRange(speciesResult.results);
                    url = speciesResult.next;
                }

                // Display all species
                lstSpecies.Items.Clear();
                foreach (var species in allSpecies)
                {
                    lstSpecies.Items.Add($"{species.name} - {species.classification} - {species.language}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
