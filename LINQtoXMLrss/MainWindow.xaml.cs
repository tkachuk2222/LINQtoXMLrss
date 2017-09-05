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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace LINQtoXMLrss
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        XDocument xDoc;
        List<News> news = new List<News>();
        ChangeRSS changeRSS;
        public XDocument xmlDoc;

        public class News
        {
            public string Title { set; get; }
            public string Link { set; get; }
            public string Descr { set; get; }
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void addLink_Click(object sender, RoutedEventArgs e)
        {
            changeRSS = new ChangeRSS();
            changeRSS.Owner = this;
            changeRSS.ShowDialog();
        }

        public void loadRSSto_tbLinks(string link)
        {
            xDoc = XDocument.Load(link);
            var el = xDoc.Descendants("item").
                Select(x => new News
                {
                    Title = x.Element("title").Value.ToString(),
                    Link = x.Element("link").Value.ToString(),
                    Descr = x.Element("description").Value.ToString()
                });
            news = el.ToList();
            foreach (var i in news)
            {
                tbLinks.Items.Add(i.Title);
            }
        }

        public void clear_tbHeadersRSS()
        {
            tbLinks.Items.Clear();
        }

        private void tbLinks_Loaded(object sender, RoutedEventArgs e)
        {
            loadRSSto_tbLinks("http://24tv.ua/rss/all.xml");
        }

        private void tbLinks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbLinks.SelectedValue != null)
            {
                int i = tbLinks.SelectedIndex;
                var el = news[i];
                tbDescription.Text = el.Descr;
                tbTitle.Text = el.Title;
            }
            

                //regex101.com
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            changeRSS = new ChangeRSS();
            changeRSS.Owner = this;
            
            if(File.Exists("links.xml"))
                xmlDoc = XDocument.Load("links.xml");
            else
            {
                xmlDoc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), new XElement("links", new XElement("link", "http://24tv.ua/rss/all.xml")));
                xmlDoc.Save("links.xml");
            }

        }
    }
}
