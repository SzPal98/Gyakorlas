using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VersionControl_3.Entities;

namespace VersionControl_3
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = Resource1.LastName; // label1
            
            button1.Text = Resource1.Add; // button1


            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = textBox1.Text,
                
            };
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Példányosít egyet a windows standard mentés ablakából
            SaveFileDialog sfd = new SaveFileDialog();

            // Opcionális rész
            sfd.InitialDirectory = Application.StartupPath; // Alapértelmezetten az exe fájlunk mappája fog megnyílni a dialógus ablakban
            sfd.Filter = "Comma Seperated Values (*.csv)|*.csv"; // A kiválasztható fájlformátumokat adjuk meg ezzel a sorral. Jelen esetben csak a csv-t fogadjuk el
            sfd.DefaultExt = "csv"; // A csv lesz az alapértelmezetten kiválasztott kiterjesztés
            sfd.AddExtension = true; // Ha ez igaz, akkor hozzáírja a megadott fájlnévhez a kiválasztott kiterjesztést, de érzékeli, ha a felhasználó azt is beírta és nem fogja duplán hozzáírni

            // Ez a sor megnyitja a dialógus ablakot és csak akkor engedi tovább futni a kódot, ha az ablakot az OK gombbal zárták be
            if (sfd.ShowDialog() != DialogResult.OK) return;

            // Az előző kódsor az alábbi két sor rövidített írásmódja
            // DialogResult eredmény = sfd.ShowDialog(); // A dialógusablak bezárása után visszakapunk egy DialogResult típusú értéket, mely az ablak bezárásnak körülményeit tárolja
            // if (eredmény != DialogResult.OK) return; // Ha a bezárás nem az OK gomb lenyomására következett be, akkor kilépünk a metódusból és nem hajtjuk végre a mentést

            // Ha a using blokk használatával példányosítunk egy osztályt akkor a példány csak a using blokk végéig létezik, utána törlésre kerül
            // StreamWriter és StreamReader használata esetén ezzel a módszerrel megspórolhatjuk a Close() metódus használatát és az írás / olvasási hibák egy részét is lekezeljük
            // A StreamWriter paraméterei:
            //    1) Fájlnév: mi itt azt a fájlnevet adjuk át, amit a felhasználó az sfd dialógusban megadott
            //    2) Append: ha igaz és már létezik ilyen fájl, akkor a végéhez fűzi a sorokat, ha hamis, akkor felülírja a létező fájlt
            //    3) Karakterkódolás: a magyar nyelvnek is megfelelő legáltalánosabb karakterkódolás az UTF8
            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                // Végigmegyünk a hallgató lista elemein
                foreach (var s in users)
                {
                    // Egy ciklus iterációban egy sor tartalmát írjuk a fájlba
                    // A StreamWriter Write metódusa a WriteLine-al szemben nem nyit új sort
                    // Így darabokból építhetjük fel a csv fájl pontosvesszővel elválasztott sorait
                    sw.Write(s.ID);
                    sw.Write(";");
                    sw.Write(s.FullName);
                    sw.WriteLine(); // Ez a sor az alábbi módon is írható: sr.Write("\n");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            User g = (User)listBox1.SelectedItem;
            var od = from x in users
                     where x.ID == g.ID
                     select x;

            users.Remove(od.FirstOrDefault());
        }
    }
}
