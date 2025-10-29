using guia8._1.Models;

namespace Guia8._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Cuenta> cuentas = new List<Cuenta>();
        OpenFileDialog openFileDialog = new OpenFileDialog();
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string nombre = tbNombre.Text;
            int dni = Convert.ToInt32(tbDni.Text);
            double importe = Convert.ToDouble(tbImporte.Text);

            Cuenta nuevaCuenta = new Cuenta(nombre, dni, importe);

            cuentas.Sort();
            int idx = cuentas.BinarySearch(nuevaCuenta);



            if (idx >= 0)
            {
                cuentas[idx].Nombre = nuevaCuenta.Nombre;
                cuentas[idx].Importe += nuevaCuenta.Importe;
            }
            else
            {
                cuentas.Add(nuevaCuenta);
            }

            tbNombre.Clear();
            tbDni.Clear();
            tbImporte.Clear();

            btnActualizar.PerformClick();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            lbVer.Items.Clear();

            if (cuentas.Count != -1)
            {
                foreach (Cuenta c in cuentas)
                {
                    lbVer.Items.Add(c);
                }
            }
        }

        private void lbVer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cuenta selectedCuenta = lbVer.SelectedItem as Cuenta;


            if (selectedCuenta != null)
            {
                tbNombre.Text = selectedCuenta.Nombre;
                tbDni.Text = selectedCuenta.Dni.ToString();
                tbImporte.Text = selectedCuenta.Importe.ToString();
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;

                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read); //Permite abrir el archivo en modo lectura
                StreamReader sr = new StreamReader(fs); //Lee el archivo linia a linia
                try
                {

                    while (sr.EndOfStream == false) //mientras no llega al final sigue .. ..
                    {
                        string linia = sr.ReadLine();
                        int dni = Convert.ToInt32(linia.Substring(0, 9).Trim());
                        string nombre = linia.Substring(9, 10).Trim();
                        double importe = Convert.ToDouble(linia.Substring(19, 9).Trim());

                        Cuenta nuevaCuenta = new Cuenta(nombre, dni, importe);

                        cuentas.Add(nuevaCuenta);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sr.Close();
                    fs.Close();
                }
            }
            btnActualizar.PerformClick();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                try
                {
                   

                    foreach (Cuenta c in cuentas)
                    {
                        string nombre = c.Nombre;
                        if (c.Nombre.Length > 10)
                        {
                            nombre = c.Nombre.Substring(0, 10);
                            
                        }
                        string linea = $"{c.Dni,+9}{nombre,-10}{c.Importe,+9:f2}";

                        sw.WriteLine(linea);


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sw.Close();
                    fs.Close();
                }

            }
        }
    }
}
