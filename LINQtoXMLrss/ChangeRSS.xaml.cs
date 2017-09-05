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

using System.Linq;
using System.Xml.Linq;

namespace LINQtoXMLrss
{
    /// <summary>
    /// Interaction logic for ChangeRSS.xaml
    /// </summary>
    public partial class ChangeRSS : Window
    {
        public MainWindow myOwner;
        NewRss newRss;
        public ChangeRSS()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if(listBoxLinks.SelectedIndex > -1)
            {
                myOwner.clear_tbHeadersRSS();

                myOwner.loadRSSto_tbLinks(listBoxLinks.SelectedItem.ToString());
             
            }
            this.Close();
        }

        private void buttonAddRSS_Click(object sender, RoutedEventArgs e)
        {
            newRss = new NewRss();
            newRss.Owner = this;
            newRss.ShowDialog();
            // 
            // зупинився на тому шо треба з лістБоксЛінкс закинути  ХМЛ або з ньюРСС зразу закидати в хмл і тут 
            // перезчитувати зразу
            // таке шось
            // і ше треба поправити там де опис і вибір(лістБокс походу) бо не змінює при іншій ссилці 
        }

        private void listBoxLinks_Loaded(object sender, RoutedEventArgs e)
        {

            var linkElements = myOwner.xmlDoc.Descendants("link").
                Select(x => x.Value.ToString()).ToList();

            foreach(var item in linkElements)
            {
                listBoxLinks.Items.Add(item);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myOwner = Owner as MainWindow;
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if( listBoxLinks.SelectedIndex > -1)
            {
                myOwner.xmlDoc.Descendants("links")
                    .Elements("link")
                    .Where(x => x.Value == listBoxLinks.SelectedItem)
                    .Remove();
                
                
                listBoxLinks.Items.Remove(listBoxLinks.SelectedItem);
                
            }
        }
    }
}
