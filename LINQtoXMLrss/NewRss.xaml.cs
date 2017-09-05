using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Xml.Linq;

namespace LINQtoXMLrss
{
    /// <summary>
    /// Interaction logic for NewRss.xaml
    /// </summary>
    public partial class NewRss : Window
    {

        ChangeRSS myOwner;
        public NewRss()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            myOwner.listBoxLinks.Items.Add( textBoxLink.Text );

            myOwner.myOwner.xmlDoc.Descendants("links").First().Add(new XElement("link", textBoxLink.Text));
            myOwner.myOwner.xmlDoc.Save("links.xml");
            myOwner.myOwner.clear_tbHeadersRSS();
            myOwner.myOwner.loadRSSto_tbLinks(textBoxLink.Text);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myOwner = Owner as ChangeRSS;
        }
    }
}
